using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class OwnerController : Controller
    {
        #region Private Fields

        private readonly IOwnerService ownerService;

        #endregion Private Fields

        #region Public Constructors

        public OwnerController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerModel model)
        {
            var ownerModel = await this.ownerService.CreateAsync(model);
            var resourceUri = Request.GetResourcePathSafe(model.Id);

            return Created(resourceUri, ownerModel);
        }

        [HttpDelete("{ownerId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteOwnerAsync(string ownerId)
        {
            await this.ownerService.DeleteAsync(ownerId);
            return Ok();
        }

        [HttpGet("{ownerId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadOwnerAsync(string ownerId)
        {
            var ownerModel = await this.ownerService.ReadAsync(ownerId);
            return Ok(ownerModel);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateOwnerAsync([FromBody] OwnerModel model)
        {
            await this.ownerService.UpdateAsync(model);
            return Ok();
        }

        #endregion Public Methods
    }
}
