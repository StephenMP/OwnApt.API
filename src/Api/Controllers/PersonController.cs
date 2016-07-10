using Microsoft.AspNet.Mvc;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PersonController : Controller
    {
        #region Private Fields + Properties

        private IPersonService personService;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        #endregion Public Constructors + Destructors

        #region Public Methods

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult($"{nameof(model)} was null");
            }

            var personModel = await this.personService.CreateAsync(model);
            var resourceUri = Request == null ? "" : $"{Request.Host}{Request.Path}/{model.Id}";

            return Created(resourceUri, personModel);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePerson(string personId)
        {
            if (string.IsNullOrEmpty(personId))
            {
                return new BadRequestObjectResult($"{nameof(personId)} is null or empty");
            }

            await this.personService.DeleteAsync(personId);
            return Ok();
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> ReadPerson(string personId)
        {
            if (string.IsNullOrWhiteSpace(personId))
            {
                return new BadRequestObjectResult($"{nameof(personId)} was null or empty");
            }

            var personModel = await this.personService.ReadAsync(personId);
            return Ok(personModel);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> UpdatePerson(string personId, [FromBody] PersonModel model)
        {
            if (string.IsNullOrEmpty(personId))
            {
                return new BadRequestObjectResult($"{nameof(personId)} is null or empty");
            }

            if (model == null)
            {
                return new BadRequestObjectResult($"{nameof(model)} was null");
            }

            await this.personService.UpdateAsync(model);
            return Ok();
        }

        #endregion Public Methods
    }
}
