using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Dto;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PropertyController : Controller
    {
        #region Fields

        private readonly IPropertyService propertyService;

        #endregion Fields

        #region Constructors

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        #endregion Constructors

        #region Methods

        [HttpPost("owner/addToProperties")]
        public async Task<IActionResult> AddOwnerToProperties([FromBody] MapOwnerToPropertiesDto mapOwnerToPropertiesDto)
        {
            if (mapOwnerToPropertiesDto == null)
            {
                return new BadRequestObjectResult($"{nameof(mapOwnerToPropertiesDto)} is null or empty");
            }

            await this.propertyService.MapOwnerToPropertiesAsync(mapOwnerToPropertiesDto);

            return Ok();
        }

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

        [HttpPost("createProperty")]
        public async Task<IActionResult> CreateProperty()
        {
            var model = new PropertyModel
            {
                Address = new AddressModel
                {
                    Address1 = "3467 N. Arrowwood Way",
                    City = "Meridian",
                    County = "Ada",
                    State = State.ID,
                    Zip = new ZipModel
                    {
                        Base = "83646"
                    }
                },
                Features = new FeaturesModel
                {
                    Amenities = new List<AmenityModel>
                    {
                        new AmenityModel { Type = "Two Car Garage", Description = "Two car garage with added storage space." },
                        new AmenityModel { Type = "W/D Hookups", Description = "Washer and dryer hookups." },
                        new AmenityModel { Type = "Hardwood Floors", Description = "Dark hardwood flooring." },
                        new AmenityModel { Type = "Refridgerator", Description = "Stainless steel refridgerator." },
                        new AmenityModel { Type = "Microwave & Hood", Description = "Stainless steel microwave and hood." },
                        new AmenityModel { Type = "Gas Heating", Description = "Forced air gas heating." },
                        new AmenityModel { Type = "Open Concept", Description = "Fully open concept." },
                        new AmenityModel { Type = "Natural Lighting", Description = "Numerous windows allowing for plenty of natural light." },
                        new AmenityModel { Type = "Gas Range", Description = "Stainless steel gas stovetop/range." },
                        new AmenityModel { Type = "Central AC", Description = "Central air conditioning (electric)." },
                        new AmenityModel { Type = "Automatic Sprinklers", Description = "Fully automatic sprinkers" },
                        new AmenityModel { Type = "Pressurized Irrigation", Description = "The sprinklers run off of pressurized irrigation." }
                    },
                    Bathrooms = 2,
                    Levels = 1,
                    Parking = new List<ParkingModel>
                    {
                        new ParkingModel { Type = "Garage", Description = "Two car garage." },
                        new ParkingModel { Type = "Driveway", Description = "Driveway can fit up to three cars." },
                        new ParkingModel { Type = "Street", Description = "Street parking available on opposite side of street." }
                    },
                    Rooms = 3,
                    SqFootage = 1415
                },
                PropertyType = PropertyType.SingleFamilyHome
            };

            var createdModel = await this.propertyService.CreateAsync(model);

            return Ok(createdModel.Id);
        }

        #endregion Methods
    }
}
