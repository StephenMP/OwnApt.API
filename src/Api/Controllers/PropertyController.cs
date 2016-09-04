using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PropertyController : Controller
    {
        #region Private Fields

        private readonly IPropertyService propertyService;

        #endregion Private Fields

        #region Public Constructors

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public async Task<IActionResult> CreatePropertyAsync([FromBody] PropertyModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult($"{nameof(model)} was null");
            }

            var propertyModel = await this.propertyService.CreateAsync(model);
            var resourceUri = Request == null ? "" : $"{Request.Host}{Request.Path}/{model.Id}";

            return Created(resourceUri, propertyModel);
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> DeletePropertyAsync(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId))
            {
                return new BadRequestObjectResult($"{nameof(propertyId)} is null or empty");
            }

            await this.propertyService.DeleteAsync(propertyId);
            return Ok();
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> ReadPropertiesForOwnerAsync(string ownerId)
        {
            if (string.IsNullOrEmpty(ownerId))
            {
                return new BadRequestObjectResult($"{nameof(ownerId)} is null or empty");
            }

            var propertyModelList = await this.propertyService.ReadPropertiesForOwnerAsync(ownerId);
            return Ok(propertyModelList);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<IActionResult> ReadPropertiesForTenantAsync(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                return new BadRequestObjectResult($"{nameof(tenantId)} is null or empty");
            }

            var propertyModelList = await this.propertyService.ReadPropertiesForTenantAsync(tenantId);
            return Ok(propertyModelList);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> ReadPropertyAsync(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId))
            {
                return new BadRequestObjectResult($"{nameof(propertyId)} is null or empty");
            }

            var propertyModel = await this.propertyService.ReadAsync(propertyId);
            return Ok(propertyModel);
        }

        [HttpPut("{propertyId}")]
        public async Task<IActionResult> UpdatePropertyAsync(string propertyId, [FromBody] PropertyModel model)
        {
            if (string.IsNullOrEmpty(propertyId))
            {
                return new BadRequestObjectResult($"{nameof(propertyId)} is null or empty");
            }

            if (model == null)
            {
                return new BadRequestObjectResult($"{nameof(model)} was null");
            }

            await this.propertyService.UpdateAsync(model);
            return Ok();
        }

        #endregion Public Methods
    }
}
