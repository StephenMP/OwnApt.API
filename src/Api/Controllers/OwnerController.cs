using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using System;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class OwnerController : ApiController
    {
        private readonly IOwnerService ownerService;
        private readonly string cachePrefix;

        #region Public Constructors

        public OwnerController(IOwnerService ownerService, IMemoryCache cache) : base(cache)
        {
            this.ownerService = ownerService;
            this.cachePrefix = nameof(OwnerController);
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerModel model)
        {
            var ownerModel = await this.ownerService.CreateAsync(model);
            var resourceUri = Request.GetResourcePathSafe(model.Id);
            this.SetCache($"{this.cachePrefix}:{ownerModel.Id}", ownerModel);

            return Created(resourceUri, ownerModel);
        }

        [HttpDelete("{ownerId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteOwnerAsync(string ownerId)
        {
            await this.ownerService.DeleteAsync(ownerId);
            this.RemoveCache($"{this.cachePrefix}:{ownerId}");
            return Ok();
        }

        [HttpGet("{ownerId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadOwnerAsync(string ownerId)
        {
            OwnerModel model = null;
            if(this.CheckCache($"{this.cachePrefix}:{ownerId}", out model))
            {
                return Ok(model);
            }

            model = await this.ownerService.ReadAsync(ownerId);
            this.SetCache($"{this.cachePrefix}:{ownerId}", model);

            return Ok(model);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateOwnerAsync([FromBody] OwnerModel model)
        {
            await this.ownerService.UpdateAsync(model);
            this.SetCache($"{this.cachePrefix}:{model.Id}", model);

            return Ok();
        }

        #endregion Public Methods
    }
}
