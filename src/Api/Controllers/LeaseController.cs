using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LeaseController : Controller
    {
        #region Private Fields

        private readonly ILeaseTermService leaseTermService;

        #endregion Private Fields

        #region Public Constructors

        public LeaseController(ILeaseTermService leaseTermService)
        {
            this.leaseTermService = leaseTermService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateLeaseTermAsync([FromBody] LeaseTermModel termModel)
        {
            var model = await this.leaseTermService.CreateAsync(termModel);
            var resourceUri = Request.GetResourcePathSafe(model.LeaseTermId);

            return Created(resourceUri, model);
        }

        [HttpGet("{leaseTermId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadLeaseTermAsync(string leaseTermId)
        {
            var model = await this.leaseTermService.ReadAsync(leaseTermId);
            return Ok(model);
        }

        [HttpGet("property/{propertyId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadLeaseTermByPropertyAsync(string propertyId)
        {
            var model = await this.leaseTermService.ReadByPropertyIdAsync(propertyId);
            return Ok(model);
        }

        #endregion Public Methods
    }
}
