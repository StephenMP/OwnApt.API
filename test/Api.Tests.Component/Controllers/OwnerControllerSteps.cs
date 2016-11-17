using System;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class OwnerControllerSteps : ControllerSteps, IDisposable
    {
        #region Private Fields

        private string currentOwnerId;
        private bool disposedValue;
        private OwnerController ownerController;
        private OwnerModel ownerModel;

        private IOwnerService ownerService;

        private IRegisteredTokenService registeredTokenService;

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveAMockedDataLayer()
        {
            var dataLayerMockOptions = new DataLayerMockOptions()
                                            .MockOwnerRepository()
                                            .MockRegisteredTokenRepository();

            this.GivenIHaveAMockedDataLayer(dataLayerMockOptions);
        }

        internal void GivenIHaveAnOwnerController()
        {
            this.ownerController = new OwnerController(this.ownerService, this.registeredTokenService, null);
        }

        internal void GivenIHaveAnOwnerService()
        {
            this.ownerService = new OwnerService(this.ownerRepository);
        }

        internal void GivenIHaveAOwnerToCreate()
        {
            this.ownerModel = this.mockedOwnerModel;
        }

        internal void GivenIHaveAOwnerToDelete()
        {
            this.currentOwnerId = this.mockedOwnerEntity.Id;
        }

        internal void GivenIHaveAOwnerToRead()
        {
            this.currentOwnerId = this.mockedOwnerEntity.Id;
        }

        internal void GivenIHaveAOwnerToUpdate()
        {
            this.ownerModel = this.mockedOwnerModel;
        }

        internal void GivenIHaveARegisteredTokenService()
        {
            this.registeredTokenService = new RegisteredTokenService(this.registeredTokenRepository);
        }

        internal void ThenICanVerifyICanCreateOwner()
        {
            var owner = this.controllerContent as OwnerModel;
            Assert.NotNull(owner);
            Assert.Equal("John", owner.Name.FirstName);
            Assert.Equal("C", owner.Name.MiddleName);
            Assert.Equal("Doe", owner.Name.LastName);
        }

        internal void ThenICanVerifyICanReadOwner()
        {
            var model = this.controllerResponse as OwnerModel;
            Assert.Equal(this.mockedOwnerModel.Id, this.mockedOwnerModel.Id);
            Assert.Equal(this.mockedOwnerModel.Name, this.mockedOwnerModel.Name);
        }

        internal async Task WhenICreateOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.CreateOwnerAsync(this.ownerModel);
        }

        internal async Task WhenIDeleteOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.DeleteOwnerAsync(this.currentOwnerId);
        }

        internal async Task WhenIReadOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.ReadOwnerAsync(this.currentOwnerId);
        }

        internal async Task WhenIUpdateOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.UpdateOwnerAsync(this.ownerModel);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.ownerController?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
