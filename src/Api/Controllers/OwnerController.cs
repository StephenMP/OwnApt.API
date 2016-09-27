using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using OwnApt.Common.Security;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class OwnerController : ApiController
    {
        private readonly IOwnerService ownerService;
        readonly IRegisteredTokenService registeredTokenService;

        #region Public Constructors

        public OwnerController
        (
            IOwnerService ownerService, 
            IRegisteredTokenService registeredTokenService, 
            IMemoryCache cache
        ) : base(cache)
        {
            this.ownerService = ownerService;
            this.registeredTokenService = registeredTokenService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerModel model)
        {
            var resultModel = await this.ownerService.CreateAsync(model);
            this.SetCache(resultModel.Id, resultModel);

            return Created(this.Request.GetResourcePathSafe(model.Id), resultModel);
        }

        [HttpDelete("{ownerId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteOwnerAsync(string ownerId)
        {
            await this.ownerService.DeleteAsync(ownerId);
            this.RemoveCache(ownerId);
            return Ok();
        }

        [HttpGet("{ownerId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadOwnerAsync(string ownerId)
        {
            OwnerModel model = null;
            if(this.CheckCache(ownerId, out model))
            {
                return Ok(model);
            }

            model = await this.ownerService.ReadAsync(ownerId);
            this.SetCache(ownerId, model);

            return Ok(model);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateOwnerAsync([FromBody] OwnerModel model)
        {
            await this.ownerService.UpdateAsync(model);
            this.SetCache(model.Id, model);

            return Ok();
        }

        [HttpGet("signup/token/id/{tokenId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadRegisteredTokenAsync(int tokenId)
        {
            RegisteredTokenModel model = null;
            if(this.CheckCache(tokenId, out model))
            {
                return Ok(model);
            }

            model = await this.registeredTokenService.ReadAsync(tokenId);
            this.SetCache(tokenId, model);

            return Ok(model);
        }

        [HttpPost("signup/token/register")]
        [ValidateModel]
        public async Task<IActionResult> CreateRegisteredTokenAsync([FromBody] RegisteredTokenModel model)
        {
            var resultModel = await this.registeredTokenService.CreateAsync(model);
            this.SetCache(resultModel.Token, resultModel);

            return Created(new Uri(this.Request.GetResourcePathSafe(model.Token).Replace("/register", "")), resultModel);
        }

        [HttpPost("signup/token")]
        [ValidateModel]
        public async Task<IActionResult> ReadRegisteredTokenByTokenAsync([FromBody] RegisteredTokenModel tokenModel)
        {
            var token = tokenModel.Token;

            RegisteredTokenModel model = null;
            if (this.CheckCache(token, out model))
            {
                return Ok(model);
            }

            model = await this.registeredTokenService.ReadByTokenAsync(token);
            this.SetCache(token, model);

            return Ok(model);
        }

        #endregion Public Methods
    }
}
