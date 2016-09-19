using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Extension;
using OwnApt.Api.Filters;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class LeaseController : Controller
    {
        #region Private Fields

        private readonly ITermService termService;

        #endregion Private Fields

        #region Public Constructors

        public LeaseController(ITermService termService)
        {
            this.termService = termService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateTermAsync([FromBody] TermModel termModel)
        {
            var model = await this.termService.CreateAsync(termModel);
            var resourceUri = Request.GetResourcePathSafe(model.TermId);

            return Created(resourceUri, model);
        }

        [HttpDelete("{termId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteTermAsync(string termId)
        {
            await this.termService.DeleteAsync(termId);
            return Ok();
        }

        [HttpGet("{termId}")]
        [ValidateModel]
        public async Task<IActionResult> ReadTermAsync(string termId)
        {
            var model = await this.termService.ReadAsync(termId);
            return Ok(model);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateTermAsync([FromBody] TermModel termModel)
        {
            await this.termService.UpdateAsync(termModel);
            return Ok();
        }

        #endregion Public Methods
    }
}
