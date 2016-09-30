using OwnApt.Api.Contract.Model;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class OwnerControllerFeatures : IDisposable
    {
        #region Public Fields

        public OwnerControllerSteps steps;

        #endregion Public Fields

        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public OwnerControllerFeatures()
        {
            this.steps = new OwnerControllerSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreateOwnerAsync()
        {
            this.steps.GivenIHaveAMockedDataLayer();
            this.steps.GivenIHaveAnOwnerService();
            this.steps.GivenIHaveARegisteredTokenService();
            this.steps.GivenIHaveAnOwnerController();
            this.steps.GivenIHaveAOwnerToCreate();
            await this.steps.WhenICreateOwnerAsync();
            this.steps.ThenICanVerifyIReceived<OwnerModel>(HttpStatusCode.Created);
            this.steps.ThenICanVerifyICanCreateOwner();
        }

        [Fact]
        public async Task CanDeleteOwnerAsync()
        {
            this.steps.GivenIHaveAMockedDataLayer();
            this.steps.GivenIHaveAnOwnerService();
            this.steps.GivenIHaveARegisteredTokenService();
            this.steps.GivenIHaveAnOwnerController();
            this.steps.GivenIHaveAOwnerToDelete();
            await this.steps.WhenIDeleteOwnerAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanReadOwnerAsync()
        {
            this.steps.GivenIHaveAMockedDataLayer();
            this.steps.GivenIHaveAnOwnerService();
            this.steps.GivenIHaveARegisteredTokenService();
            this.steps.GivenIHaveAnOwnerController();
            this.steps.GivenIHaveAOwnerToRead();
            await this.steps.WhenIReadOwnerAsync();
            this.steps.ThenICanVerifyIReceived<OwnerModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyICanReadOwner();
        }

        [Fact]
        public async Task CanUpdateOwnerAsync()
        {
            this.steps.GivenIHaveAMockedDataLayer();
            this.steps.GivenIHaveAnOwnerService();
            this.steps.GivenIHaveARegisteredTokenService();
            this.steps.GivenIHaveAnOwnerController();
            this.steps.GivenIHaveAOwnerToUpdate();
            await this.steps.WhenIUpdateOwnerAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
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
