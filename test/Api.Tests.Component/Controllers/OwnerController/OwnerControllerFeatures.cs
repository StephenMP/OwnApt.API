using OwnApt.Api.Contract.Model;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.OwnerControllerTests
{
    public class OwnerControllerFeatures : IDisposable
    {
        #region Fields

        public OwnerControllerSteps steps;

        private bool disposedValue;

        #endregion Fields

        #region Constructors

        public OwnerControllerFeatures()
        {
            this.steps = new OwnerControllerSteps();
        }

        #endregion Constructors

        #region Methods

        [Fact]
        public async Task CanCreateOwnerAsync()
        {
            this.steps.GivenIHaveAOwnerEnvironment();
            this.steps.GivenIHaveAOwnerRepository();
            this.steps.GivenIHaveAOwnerService();
            this.steps.GivenIHaveAnOwnerController();
            this.steps.GivenIHaveAOwnerToCreate();
            await this.steps.WhenICreateOwnerAsync();
            this.steps.ThenICanVerifyIReceived<OwnerModel>(HttpStatusCode.Created);
            this.steps.ThenICanVerifyICanCreateOwner();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        #endregion Methods
    }
}
