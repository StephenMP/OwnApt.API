using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Filters
{
    // appId:hashedSecret:createdDate:randomGUID
    public class AuthenticationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        #region Private Fields + Properties

        private Dictionary<string, string> allowedApps;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public AuthenticationFilter()
        {
            this.allowedApps = new Dictionary<string, string>
            {
                {"abcd1234", "1234abcd"}
            };
        }

        #endregion Public Constructors + Destructors

        #region Public Methods

        public async Task OnAuthorizationAsync(AuthorizationContext context)
        {
            // Get and check the authorization header
            var authHeader = context.HttpContext.Request.Headers["authorization"];
            if (authHeader == default(StringValues))
            {
                context.Result = new HttpUnauthorizedResult();
                return;
            }

            // Parse the auth header values and check they are valid
            var authHeaderValues = authHeader[0].Split(' ');
            var authScheme = authHeaderValues[0];
            if (authScheme != "amx")
            {
                context.Result = new HttpUnauthorizedResult();
                return;
            }

            var authValues = ParseAuthHeaderValues(authHeaderValues[1]);
            if (authValues == null)
            {
                context.Result = new HttpUnauthorizedResult();
                return;
            }

            // Run the values through our HMAC algorithm
            await ValidateHmac(context, authValues);
        }

        #endregion Public Methods

        #region Private Methods

        private string[] ParseAuthHeaderValues(string authHeader)
        {
            var values = authHeader.Split(':');
            return (values.Length == 4 || values.Length == 5) ? values : null;
        }

        private async Task<bool> ValidateAppId(string appId)
        {
            return await Task.FromResult(this.allowedApps.ContainsKey(appId));
        }

        private async Task<bool> ValidateBody(Stream body, string[] authValues)
        {
            string requestBody;
            body.Position = 0;
            using (var reader = new StreamReader(body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            // No body, nothing to do
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                return await Task.FromResult(true);
            }

            // If there is a body, caller must provide the MD5 signed body
            if (authValues.Length < 5)
            {
                return await Task.FromResult(false);
            }

            var providedBase64SignedBody = authValues[4];
            var byteBodyString = Encoding.UTF8.GetBytes(requestBody);
            byte[] md5SignedBody;

            using (var md5 = MD5.Create())
            {
                md5SignedBody = md5.ComputeHash(byteBodyString);
            }

            var computedBase64SignedBody = Convert.ToBase64String(md5SignedBody);
            return await Task.FromResult(computedBase64SignedBody == providedBase64SignedBody);
        }

        private async Task ValidateHmac(AuthorizationContext context, string[] authValues)
        {
            /* 1. Parse the values from the auth header
             * 2. Check if there is a body (all bodies must be signed using an MD5 hash), and if so, validate it's signature
             * 3. Check if the appId is allowed
             * 4. Validate time to live
             * 5. Check that the combined signed hash using the secret matches */
            var appId = authValues[0];
            var providedSignedSecretKey = authValues[1];
            var timeStamp = authValues[2];
            var guidSignature = authValues[3];

            var isValid = await ValidateBody(context.HttpContext.Request.Body, authValues)
                       && await ValidateAppId(appId)
                       && await ValidateTimeToLive(timeStamp)
                       && await ValidateSignedSecretKey(appId, timeStamp, guidSignature, providedSignedSecretKey);

            if (!isValid)
            {
                context.Result = new HttpUnauthorizedResult();
                return;
            }
        }

        private async Task<bool> ValidateSignedSecretKey(string appId, string timeStamp, string guidSignature, string providedSignedSecretKey)
        {
            var secretKey = this.allowedApps[appId];
            var byteSecretKey = Encoding.UTF8.GetBytes(secretKey);
            byte[] hashedSecretKey;

            using (var hmac = HMACSHA1.Create())
            {
                hashedSecretKey = hmac.ComputeHash(byteSecretKey);
            }

            var secretKeyCombined = $"{hashedSecretKey}:{timeStamp}:{guidSignature}";
            var byteSecretKeyCombined = Encoding.UTF8.GetBytes(secretKeyCombined);
            var computedSignedSecretKey = Convert.ToBase64String(byteSecretKeyCombined);

            return await Task.FromResult(computedSignedSecretKey == providedSignedSecretKey);
        }

        private async Task<bool> ValidateTimeToLive(string timeStamp)
        {
            var timeStampDateTime = DateTime.FromFileTimeUtc(long.Parse(timeStamp));
            return await Task.FromResult(timeStampDateTime.AddSeconds(10) > DateTime.UtcNow);
        }

        #endregion Private Methods
    }
}
