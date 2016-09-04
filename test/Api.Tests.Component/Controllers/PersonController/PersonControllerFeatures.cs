using OwnApt.Api.Contract.Model;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.PersonControllerTests
{
    public class PersonControllerFeatures : IDisposable
    {
        #region Public Fields

        public PersonControllerSteps steps;

        #endregion Public Fields

        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public PersonControllerFeatures()
        {
            this.steps = new PersonControllerSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreatePersonAsync()
        {
            this.steps.GivenIHaveAPersonEnvironment();
            this.steps.GivenIHaveAPersonRepository();
            this.steps.GivenIHaveAPersonService();
            this.steps.GivenIHaveAPersonController();
            this.steps.GivenIHaveAPersonToCreate();
            await this.steps.WhenICreatePersonAsync();
            this.steps.ThenICanVerifyIReceived<PersonModel>(HttpStatusCode.Created);
            this.steps.ThenICanVerifyICanCreatePerson();
        }

        [Fact]
        public async Task CanCreateUserAsync()
        {
            this.steps.GivenIHaveAPersonEnvironment();
            this.steps.GivenIHaveACoreContext();
            this.steps.GivenIHaveAUserLoginRepository();
            this.steps.GivenIHaveAUserLoginService();
            this.steps.GivenIHaveAPersonController();
            this.steps.GivenIHaveAUserLoginToCreate();
            await this.steps.WhenICreateUserAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanLoginUserAsync()
        {
            this.steps.GivenIHaveAPersonEnvironment();
            this.steps.GivenIHaveACoreContext();
            this.steps.GivenIHaveAUserLoginRepository();
            this.steps.GivenIHaveAUserLoginService();
            this.steps.GivenIHaveAPersonController();
            this.steps.GivenIHaveAUserLoginToCreate();
            await this.steps.WhenICreateUserAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);

            await this.steps.WhenILoginUserAsync();
            this.steps.ThenICanVerifyIReceived<UserLoginModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyICanLoginUser();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.steps?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
