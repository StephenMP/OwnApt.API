using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LeaseController : ApiController
    {
        #region Private Fields

        private readonly string cachePrefix;
        private readonly ILeaseTermService leaseTermService;

        #endregion Private Fields

        #region Public Constructors

        public LeaseController(ILeaseTermService leaseTermService, IMemoryCacheService cache) : base(cache)
        {
            this.leaseTermService = leaseTermService;
            this.cachePrefix = nameof(LeaseController);
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateLeaseTermAsync([FromBody] LeaseTermModel termModel)
        {
            var model = await this.leaseTermService.CreateAsync(termModel);
            var resourceUri = Request.GetResourcePathSafe(model.LeaseTermId);
            this.SetCache($"{this.cachePrefix}:{model.LeaseTermId}", model);

            return Created(resourceUri, model);
        }

        [HttpGet("{leaseTermId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadLeaseTermAsync(int leaseTermId)
        {
            LeaseTermModel model = null;
            if (this.CheckCache($"{cachePrefix}:{leaseTermId}", out model))
            {
                return Ok(model);
            }

            model = await this.leaseTermService.ReadAsync(leaseTermId);
            this.SetCache($"{cachePrefix}:{leaseTermId}", model);

            return Ok(model);
        }

        [HttpGet("property/{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadLeaseTermByPropertyAsync(string propertyId)
        {
            LeaseTermModel model = null;
            if (this.CheckCache($"{cachePrefix}:{propertyId}", out model))
            {
                return Ok(model);
            }

            model = await this.leaseTermService.ReadByPropertyIdAsync(propertyId);
            this.SetCache($"{cachePrefix}:{propertyId}", model);

            return Ok(model);
        }

        #endregion Public Methods
    }
}
