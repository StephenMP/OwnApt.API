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

        #endregion Private Fields

        #region Public Constructors

        public PropertyController(IPropertyService propertyService, IMemoryCache cache) : base(cache)
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
            this.SetCache(propertyModel.Id, propertyModel);

            return Created(resourceUri, propertyModel);
        }

        [HttpDelete("{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> DeletePropertyAsync(string propertyId)
        {
            this.RemoveCache(propertyId);
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
                PropertyDescription = "The Rubalcava Plaza",
                ImageUri = new Uri("https://lh3.googleusercontent.com/i-GqPkBfyoxGdA0aBDjJMvUnnw3H67oHdeYRneUka5P4qqr97jpOkyXj_wK09EiE9wqzQ1XjvzyaxAD9eI_zsElATr4MW9FT9Vxfqr6uRSjcKclWmFveWYWC0-8e82QR-j5lHIKS81qu82NUDLHBLDbg7hVcFnFcn5RqNf-qcPSe-5QbNJdWQ4f9wh1BGcAhG4lPXeDpE5bE46SQbSHNBzNLdYHlpBwM6JOoRS_qsPItRixLxOITwXO9UHm_jqOC_XsvE3naUzwn-sQCjksaDj66e2Icp9C-iQQOJgtojQv5Xuq_mnoXdacj66K_PJfRpTB2UCNKDQrjMhjHL6Du4Ct5EE5xwPzqxOWmpyl_HYZ5EONG1bhmPWUW6HDXuMahcpfe613XRXkzGx55ryqjkD0MKPfUnEoN5_W3YuvQypx9CpeW5gNXf0815K6dKgLyySIibVuEOMwv-wKfjelKKPivCLCN6jcU9a8_lR4a-9PPYmWZVZ69DB8xWLnXxB8ngbPlWnuXKjEhUP1mdQqqNZr2Zs-Iqmh_yhq7E-9OrxmLkCQsdVU_eAvyQi5fZSAJt9iuqs5PcqGyHDpZM1pncz4LkkqikKqKc9v0h7iWD_fenyw=w500-h334-no"),
                Address = new AddressModel
                {
                    Address1 = "8214 Princess Ct.",
                    City = "Neverland",
                    County = "Lost Boys County",
                    State = State.HI,
                    Zip = new ZipModel
                    {
                        Base = "00000",
                        Extension = "9999"
                    }
                },
                Features = new FeaturesModel
                {
                    Amenities = new List<AmenityModel>
                    {
                        new AmenityModel { Type = "Magical Garage", Description = "A garage magically appears for each car!" },
                        new AmenityModel { Type = "Magic Walkin Closets", Description = "They grow larger along with your wordrobe" },
                        new AmenityModel { Type = "Solid Glass Floors", Description = "Just like Cinderella's slippers!" }
                    },
                    Bathrooms = 20,
                    Levels = 6,
                    Parking = new List<ParkingModel>
                    {
                        new ParkingModel { Type = "Magical Garage", Description = "A garage magically appears for each car!" }
                    },
                    Rooms = 20,
                    SqFootage = 10000
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

            if (this.CheckCache(propertyId, out model))
            {
                return Ok(model);
            }

            model = await this.propertyService.ReadAsync(propertyId);
            this.SetCache(model.Id, model);
            return Ok(model);
        }

        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> ReadPropertiesAsync([FromQuery] string[] propertyIds)
        {
            PropertyModel[] models = null;

            if (this.CheckCache(propertyIds.GetHashCodeSafe(), out models))
            {
                return Ok(models);
            }

            models = await this.propertyService.ReadManyAsync(propertyIds);
            this.SetCache(propertyIds.GetHashCodeSafe(), models);
            return Ok(models);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdatePropertyAsync([FromBody] PropertyModel model)
        {
            await this.propertyService.UpdateAsync(model);
            this.SetCache(model.Id, model);
            return Ok();
        }

        #endregion Public Methods
    }
}
