using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
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
        public async Task<IActionResult> CreateOwnerAsync([FromBody] OwnerModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult($"{nameof(model)} was null");
            }

            var ownerModel = await this.ownerService.CreateAsync(model);
            var resourceUri = Request.GetResourcePathSafe(model.Id);

            return Created(resourceUri, ownerModel);
        }

        [HttpDelete("{ownerId}")]
        public async Task<IActionResult> DeleteOwnerAsync(string ownerId)
        {
            if (string.IsNullOrEmpty(ownerId))
            {
                return new BadRequestObjectResult($"{nameof(ownerId)} is null or empty");
            }

            await this.ownerService.DeleteAsync(ownerId);
            return Ok();
        }

        [HttpGet("{ownerId}")]
        public async Task<IActionResult> ReadOwnerAsync(string ownerId)
        {
            if (string.IsNullOrWhiteSpace(ownerId))
            {
                return new BadRequestObjectResult($"{nameof(ownerId)} was null or empty");
            }

            var ownerModel = await this.ownerService.ReadAsync(ownerId);
            return Ok(ownerModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOwnerAsync([FromBody] OwnerModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult($"{nameof(model)} was null");
            }

            await this.ownerService.UpdateAsync(model);
            return Ok();
        }

        #endregion Public Methods
    }
}
