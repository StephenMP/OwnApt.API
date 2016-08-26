using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PropertyController : Controller
    {
        #region Private Fields + Properties

        private readonly IPropertyService propertyService;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        #endregion Public Constructors + Destructors

        #region Public Methods

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyModel model)
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
        public async Task<IActionResult> DeleteProperty(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId))
            {
                return new BadRequestObjectResult($"{nameof(propertyId)} is null or empty");
            }

            await this.propertyService.DeleteAsync(propertyId);
            return Ok();
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> ReadPropertiesForOwner(string ownerId)
        {
            if (string.IsNullOrEmpty(ownerId))
            {
                return new BadRequestObjectResult($"{nameof(ownerId)} is null or empty");
            }

            var propertyModelList = await this.propertyService.ReadPropertiesForOwnerAsync(ownerId);
            return Ok(propertyModelList);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<IActionResult> ReadPropertiesForTenant(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                return new BadRequestObjectResult($"{nameof(tenantId)} is null or empty");
            }

            var propertyModelList = await this.propertyService.ReadPropertiesForTenantAsync(tenantId);
            return Ok(propertyModelList);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> ReadProperty(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId))
            {
                return new BadRequestObjectResult($"{nameof(propertyId)} is null or empty");
            }

            var propertyModel = await this.propertyService.ReadAsync(propertyId);
            return Ok(propertyModel);
        }

        [HttpPut("{propertyId}")]
        public async Task<IActionResult> UpdateProperty(string propertyId, [FromBody] PropertyModel model)
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

        //[HttpGet("insert")]
        //public async Task<IActionResult> InsertTestCrap()
        //{
        //    var propertyModel = new PropertyModel
        //    {
        //        Address = new AddressModel
        //        {
        //            Address1 = "12345 Test Ave",
        //            City = "Boise",
        //            County = "Ada",
        //            State = State.ID,
        //            Zip = new ZipModel
        //            {
        //                Base = "83703"
        //            }
        //        },
        //        Features = new FeaturesModel
        //        {
        //            Ammentities = new List<AmmentityModel>
        //            {
        //                new AmmentityModel
        //                {
        //                    Description = "Gas Fireplace",
        //                    Type = AmmenityType.Fireplace
        //                }
        //            },
        //            Bathrooms = 2,
        //            Levels = 1,
        //            Parking = new ParkingModel
        //            {
        //                Description = "Three Car Garage",
        //                Type = ParkingType.Garage
        //            },
        //            Rooms = 3
        //        },
        //        PropertyType = PropertyType.SingleFamilyHome
        //    };

        //    var ownerModel = new PersonModel
        //    {
        //        Age = 30,
        //        Contact = new ContactModel
        //        {
        //            Email = "john@doe.com",
        //            Phones = new List<PhoneModel>
        //            {
        //                new PhoneModel
        //                {
        //                    AreaCode = 208,
        //                    CountryCode = 1,
        //                    LineNumber = 4567,
        //                    Prefix = 123,
        //                    Type = PhoneType.Home
        //                }
        //            }
        //        },
        //        CreditScore = 700,
        //        Gender = Gender.Male,
        //        Name = new NameModel
        //        {
        //            FirstName = "John",
        //            MiddleName = "None",
        //            LastName = "Doe"
        //        },
        //        PropertyIds = new List<string>
        //        {
        //            propertyModel.Id
        //        },
        //        Type = PersonType.Owner
        //    };

        //    var tenantModel = new PersonModel
        //    {
        //        Age = 30,
        //        Contact = new ContactModel
        //        {
        //            Email = "jane@doe.com",
        //            Phones = new List<PhoneModel>
        //            {
        //                new PhoneModel
        //                {
        //                    AreaCode = 208,
        //                    CountryCode = 1,
        //                    LineNumber = 456,
        //                    Prefix = 7890,
        //                    Type = PhoneType.Home
        //                }
        //            }
        //        },
        //        CreditScore = 750,
        //        Gender = Gender.Female,
        //        Name = new NameModel
        //        {
        //            FirstName = "Jane",
        //            MiddleName = "None",
        //            LastName = "Doe"
        //        },
        //        PropertyIds = new List<string>
        //        {
        //            propertyModel.Id
        //        },
        //        Type = PersonType.Tenant
        //    };

        //    propertyModel.OwnerIds = new List<string> { ownerModel.Id };
        //    propertyModel.TenantIds = new List<string> { tenantModel.Id };

        //    await this.personService.CreateAsync(ownerModel);
        //    await this.personService.CreateAsync(tenantModel);
        //    await this.propertyService.CreateAsync(propertyModel);

        //    return Ok("Shiz be done! Yo!");
        //}
    }
}
