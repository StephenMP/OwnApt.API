using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using System.Net;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PersonController : Controller
    {
        #region Private Fields + Properties

        private IPersonService personService;
        private IUserLoginService userLoginService;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public PersonController(IPersonService personService, IUserLoginService userLoginService)
        {
            this.personService = personService;
            this.userLoginService = userLoginService;
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

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserLoginModel suppliedModel)
        {
            if (suppliedModel == null)
            {
                return new BadRequestObjectResult($"{nameof(suppliedModel)} was null");
            }

            var meh = JsonConvert.SerializeObject(suppliedModel);
            var userCreated = await this.userLoginService.CreateAsync(suppliedModel);

            if (userCreated)
            {
                return Ok();
            }

            return new HttpStatusCodeResult((int)HttpStatusCode.Conflict);
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

        [HttpPost("userLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginModel suppliedModel)
        {
            if (suppliedModel == null)
            {
                return new BadRequestObjectResult($"{nameof(suppliedModel)} was null");
            }

            var loginModel = await this.userLoginService.VerifyUser(suppliedModel);

            if (loginModel.VerificationResult == PasswordVerificationResult.Failed)
            {
                return new HttpUnauthorizedResult();
            }
            else if (loginModel.VerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                var updatedLoginModel = await this.userLoginService.RehashUserPassword(suppliedModel);
                return Ok(updatedLoginModel);
            }

            return Ok(loginModel);
        }

        #endregion Public Methods
    }
}
