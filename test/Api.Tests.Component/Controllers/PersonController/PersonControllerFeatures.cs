using OwnApt.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.PersonControllerTests
{
    public class PersonControllerFeatures : IDisposable
    {
        public PersonControllerSteps steps;

        public PersonControllerFeatures()
        {
            this.steps = new PersonControllerSteps();
        }

        [Fact]
        public async Task CanCreatePerson()
        {
            this.steps.GivenIHaveAPersonEnvironment();
            this.steps.GivenIHaveAPersonRepository();
            this.steps.GivenIHaveAPersonService();
            this.steps.GivenIHaveAPersonController();
            this.steps.GivenIHaveAPersonToCreate();
            await this.steps.WhenICreatePerson();
            this.steps.ThenICanVerifyIReceived<PersonModel>(HttpStatusCode.Created);
            this.steps.ThenICanVerifyICanCreatePerson();
        }

        [Fact]
        public async Task CanCreateUser()
        {
            this.steps.GivenIHaveAPersonEnvironment();
            this.steps.GivenIHaveACoreContext();
            this.steps.GivenIHaveAUserLoginRepository();
            this.steps.GivenIHaveAUserLoginService();
            this.steps.GivenIHaveAPersonController();
            this.steps.GivenIHaveAUserLoginToCreate();
            await this.steps.WhenICreateUser();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanLoginUser()
        {
            this.steps.GivenIHaveAPersonEnvironment();
            this.steps.GivenIHaveACoreContext();
            this.steps.GivenIHaveAUserLoginRepository();
            this.steps.GivenIHaveAUserLoginService();
            this.steps.GivenIHaveAPersonController();
            this.steps.GivenIHaveAUserLoginToCreate();
            await this.steps.WhenICreateUser();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);

            await this.steps.WhenILoginUser();
            this.steps.ThenICanVerifyIReceived<UserLoginModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyICanLoginUser();
        }

        #region IDisposable Support
        private bool disposedValue;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
