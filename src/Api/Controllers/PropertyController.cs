﻿using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using OwnApt.Common.Enum;
using System.Collections.Generic;
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
        [ValidateModel]
        public async Task<IActionResult> CreatePropertyAsync([FromBody] PropertyModel model)
        {
            var propertyModel = await this.propertyService.CreateAsync(model);
            var resourceUri = Request.GetResourcePathSafe(model.Id);

            return Created(resourceUri, propertyModel);
        }

        [HttpDelete("{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> DeletePropertyAsync(string propertyId)
        {
            await this.propertyService.DeleteAsync(propertyId);
            return Ok();
        }

        [HttpPost("createProperty")]
        /* Here for IT support of adding properties only. Delete once BO solution is in place */
        public async Task<IActionResult> ITSupport_CreatePropertyAsync()
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

        [HttpGet("{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadPropertyAsync(string propertyId)
        {
            var propertyModel = await this.propertyService.ReadAsync(propertyId);
            return Ok(propertyModel);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdatePropertyAsync([FromBody] PropertyModel model)
        {
            await this.propertyService.UpdateAsync(model);
            return Ok();
        }

        #endregion Public Methods
    }
}
