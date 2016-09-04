using Microsoft.AspNetCore.Mvc;
using OwnApt.Api.AppStart;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Dto;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Sql;
using OwnApt.Authentication.Client.Security;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.PersonControllerTests
{
    public class PersonControllerSteps : IDisposable
    {
        #region Private Fields

        private CoreContext coreContext;
        private bool disposedValue;
        private PersonController personController;
        private IActionResult personControllerActionResult;
        private object personControllerContent;
        private PersonModel personModel;
        private IPersonRepository personRepository;
        private IPersonService personService;
        private UserLoginModel suppliedUserLoginModel;
        private TestingEnvironment testEnvironment;
        private IUserLoginRepository userLoginRepository;
        private IUserLoginService userLoginService;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveACoreContext()
        {
            this.coreContext = new CoreContext(this.testEnvironment.SqlDbContextOptions<CoreContext>());
        }

        internal void GivenIHaveAPersonController()
        {
            this.personController = new PersonController(this.personService, this.userLoginService);
        }

        internal void GivenIHaveAPersonEnvironment()
        {
            this.testEnvironment = new TestingEnvironment();

            // Add Sql Dependencies
            this.testEnvironment.AddSqlContext<CoreContext>();

            // Add Mongo Dependency
            this.testEnvironment.AddMongo();
        }

        internal void GivenIHaveAPersonRepository()
        {
            this.personRepository = new MongoPersonRepository(this.testEnvironment.MongoClient(), OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAPersonService()
        {
            this.personService = new PersonService(this.personRepository);
        }

        internal void GivenIHaveAPersonToCreate()
        {
            this.personModel = new PersonModel
            {
                Name = new NameDto { FirstName = "John", MiddleName = "C", LastName = "Doe" }
            };
        }

        internal void GivenIHaveAUserLoginRepository()
        {
            this.userLoginRepository = new UserLoginRepository(this.coreContext, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAUserLoginService()
        {
            this.userLoginService = new UserLoginService(this.userLoginRepository);
        }

        internal void GivenIHaveAUserLoginToCreate()
        {
            this.suppliedUserLoginModel = new UserLoginModel
            {
                Email = "test@email.com",
                Password = CryptoProvider.Encrypt("TestPassword")
            };
        }

        internal void ThenICanVerifyICanCreatePerson()
        {
            var person = this.personControllerContent as PersonModel;
            Assert.NotNull(person);
            Assert.Equal("John", person.Name.FirstName);
            Assert.Equal("C", person.Name.MiddleName);
            Assert.Equal("Doe", person.Name.LastName);
        }

        internal void ThenICanVerifyICanLoginUser()
        {
            var userLoginModel = this.personControllerContent as UserLoginModel;

            Assert.NotNull(userLoginModel);
            Assert.Equal(this.suppliedUserLoginModel.Email, userLoginModel.Email);
            Assert.Equal("", userLoginModel.Password);
        }

        internal void ThenICanVerifyIReceived<TModel>(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (typeof(TModel) != typeof(Missing))
                {
                    var content = Assert.IsType<OkObjectResult>(this.personControllerActionResult);
                    this.personControllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
                else
                {
                    Assert.IsType<OkResult>(this.personControllerActionResult);
                }
            }
            else if (statusCode == HttpStatusCode.Created)
            {
                var content = Assert.IsType<CreatedResult>(this.personControllerActionResult);
                this.personControllerContent = content.Value;
                Assert.Equal((int)statusCode, content.StatusCode.Value);
            }
        }

        internal async Task WhenICreatePerson()
        {
            personControllerActionResult = await this.personController.CreatePerson(this.personModel);
        }

        internal async Task WhenICreateUser()
        {
            this.personControllerActionResult = await this.personController.CreateUser(this.suppliedUserLoginModel);
        }

        internal async Task WhenILoginUser()
        {
            this.personControllerActionResult = await this.personController.UserLogin(this.suppliedUserLoginModel);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
