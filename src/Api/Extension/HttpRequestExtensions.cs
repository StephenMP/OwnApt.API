using Microsoft.AspNetCore.Http;

namespace OwnApt.Api.Extension
{
    public static class HttpRequestExtensions
    {
        #region Public Methods

        public static string GetResourcePathSafe(this HttpRequest request)
        {
            return request == null ? "" : $"{request.Host}{request.Path}";
        }

        public static string GetResourcePathSafe<TPrimaryKey>(this HttpRequest request, TPrimaryKey id)
        {
            return request == null ? "" : $"{request.Host}{request.Path}/{id}";
        }

        #endregion Public Methods
    }
}
