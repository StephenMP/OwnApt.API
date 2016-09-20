using OwnApt.Api.Contract.Model;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.LeaseControllerTests
{
    public class LeaseControllerFeatures : IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private LeaseControllerSteps steps;

        #endregion Private Fields

        #region Public Constructors

        public LeaseControllerFeatures()
        {
            this.steps = new LeaseControllerSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreateLeaseTerm()
        {
            this.steps.GivenIHaveALeaseControllerEnvironment();
            this.steps.GivenIHaveALeaseTermToCreate();
            await this.steps.WhenICallCreateLeaseTermAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.Created);

            await this.steps.WhenICallReadLeaseTermAsync();
            this.steps.ThenICanVerifyIReceived<LeaseTermModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyICanCreateLeaseTerm();
        }

        [Fact]
        public async Task CanReadLeaseTerm()
        {
            this.steps.GivenIHaveALeaseControllerEnvironment();
            await this.steps.WhenICallReadLeaseTermAsync();
            this.steps.ThenICanVerifyIReceived<LeaseTermModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyICanReadLeaseTerm();
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
