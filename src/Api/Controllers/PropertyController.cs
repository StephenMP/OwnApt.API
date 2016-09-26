using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using OwnApt.Common.Enum;
using OwnApt.Common.Extension;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PropertyController : ApiController
    {
        #region Private Fields

        private readonly IPropertyService propertyService;
        private readonly string cachePrefix;

        #endregion Private Fields

        #region Public Constructors

        public PropertyController(IPropertyService propertyService, IMemoryCache cache) : base(cache)
        {
            this.propertyService = propertyService;
            this.cachePrefix = nameof(PropertyController);
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreatePropertyAsync([FromBody] PropertyModel model)
        {
            var propertyModel = await this.propertyService.CreateAsync(model);
            var resourceUri = Request.GetResourcePathSafe(model.Id);
            this.SetCache($"{this.cachePrefix}:{propertyModel.Id}", propertyModel);

            return Created(resourceUri, propertyModel);
        }

        [HttpDelete("{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> DeletePropertyAsync(string propertyId)
        {
            this.RemoveCache($"{this.cachePrefix}:{propertyId}");
            await this.propertyService.DeleteAsync(propertyId);
            return Ok();
        }

        [HttpPost("updateProperty")]
        /* Here for IT support of adding properties only. Delete once BO solution is in place */
        public async Task<IActionResult> ITSupport_UpdatePropertyAsync()
        {
            var propertyId = "f45cf61f92c448ebbeb4f63ff8d7e0f3";
            var propertyModel = await this.propertyService.ReadAsync(propertyId);

            // Add any update stuff here to the model //
            propertyModel.ImageUri = new Uri("https://lh3.googleusercontent.com/-igsJobY4sO5SiKosAqvdRpOpIIXc6kLlRw1PbVrBT2SqbZo3xobzy1qBokPnWxljfUbybDd=w600-h337-no");
            ////////////////////////////////////////////

            await this.propertyService.UpdateAsync(propertyModel);
            return Ok(propertyModel);
        }

        [HttpPost("createProperty")]
        /* Here for IT support of adding properties only. Delete once BO solution is in place */
        public async Task<IActionResult> ITSupport_CreatePropertyAsync()
        {
            var model = new PropertyModel
            {
                Address = new AddressModel
                {
                    Address1 = "1234 W Test Ave",
                    City = "Test Town",
                    County = "Test County",
                    State = State.WY,
                    Zip = new ZipModel
                    {
                        Base = "12345",
                        Extension = "6789"
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
                    SqFootage = 2000
                },
                PropertyType = PropertyType.SingleFamilyHome
            };

            var createdModel = await this.propertyService.CreateAsync(model);

            return Ok(createdModel.Id);
        }

        [HttpGet("{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadPropertyAsync(string propertyId)
        {
            PropertyModel model = null;

            if (this.CheckCache($"{this.cachePrefix}:{propertyId}", out model))
            {
                return Ok(model);
            }

            model = await this.propertyService.ReadAsync(propertyId);
            this.SetCache($"{this.cachePrefix}:{model.Id}", model);
            return Ok(model);
        }

        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> ReadPropertiesAsync([FromQuery] string[] propertyIds)
        {
            PropertyModel[] models = null;

            if (this.CheckCache($"{this.cachePrefix}:{propertyIds.GetHashCodeSafe()}", out models))
            {
                return Ok(models);
            }

            models = await this.propertyService.ReadManyAsync(propertyIds);
            this.SetCache($"{this.cachePrefix}:{propertyIds.GetHashCodeSafe()}", models);
            return Ok(models);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdatePropertyAsync([FromBody] PropertyModel model)
        {
            await this.propertyService.UpdateAsync(model);
            this.SetCache($"{this.cachePrefix}:{model.Id}", model);
            return Ok();
        }

        #endregion Public Methods
    }
}
