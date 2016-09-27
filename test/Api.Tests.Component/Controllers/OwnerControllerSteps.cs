using Moq;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Sql.Core;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;

namespace Api.Tests.Component.Controllers
{
    public class OwnerControllerSteps : ControllerSteps, IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private OwnerController ownerController;
        private OwnerModel ownerModel;

        internal void GivenIHaveAMockedDataLayer()
        {
            var dataLayerMockOptions = new DataLayerMockOptions()
                                            .MockOwnerRepository()
                                            .MockRegisteredTokenRepository();

            this.GivenIHaveAMockedDataLayer(dataLayerMockOptions);
        }

        private IOwnerService ownerService;
        private OwnAptTestEnvironment testEnvironment;
        private CoreContext coreContext;
        private IRegisteredTokenService registeredTokenService;
        private string currentOwnerId;

        internal void GivenIHaveARegisteredTokenRepository()
        {
            this.coreContext = new CoreContext(this.testEnvironment.GetSqlDbContextOptions<CoreContext>());
            this.registeredTokenRepository = new SqlRegisteredTokenRepository(coreContext, OwnAptStartup.BuildMapper());
        }

        internal async Task WhenIReadOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.ReadOwnerAsync(this.currentOwnerId);
        }

        internal void ThenICanVerifyICanReadOwner()
        {
            var model = this.controllerResponse as OwnerModel;
            Assert.Equal(this.mockedOwnerModel.Id, this.mockedOwnerModel.Id);
            Assert.Equal(this.mockedOwnerModel.Name, this.mockedOwnerModel.Name);
        }

        internal void GivenIHaveAOwnerToRead()
        {
            this.currentOwnerId = this.mockedOwnerEntity.Id;
        }

        internal async Task WhenIUpdateOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.UpdateOwnerAsync(this.ownerModel);
        }

        internal void ThenICanVerifyICanUpdateOwner()
        {
            this.mockedOwnerRepository.Verify(m => m.UpdateAsync(this.mockedOwnerModel));
        }

        internal void GivenIHaveAOwnerToUpdate()
        {
            this.ownerModel = this.mockedOwnerModel;
        }

        internal void GivenIHaveARegisteredTokenService()
        {
            this.registeredTokenService = new RegisteredTokenService(this.registeredTokenRepository);
        }

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveAnOwnerController()
        {
            this.ownerController = new OwnerController(this.ownerService, this.registeredTokenService, null);
        }

        internal void GivenIHaveAOwnerEnvironment()
        {
            this.testEnvironment = new OwnAptTestEnvironmentBuilder()
                                    .AddMongo()
                                    .AddSqlContext<CoreContext>()
                                    .BuildEnvironment();
        }

        internal void GivenIHaveAOwnerRepository()
        {
            this.ownerRepository = new MongoOwnerRepository(this.testEnvironment.GetMongoClient(), OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAnOwnerService()
        {
            this.ownerService = new OwnerService(this.ownerRepository);
        }

        internal void GivenIHaveAOwnerToCreate()
        {
            this.ownerModel = this.mockedOwnerModel;
        }

        internal void ThenICanVerifyICanCreateOwner()
        {
            var owner = this.controllerContent as OwnerModel;
            Assert.NotNull(owner);
            Assert.Equal("John", owner.Name.FirstName);
            Assert.Equal("C", owner.Name.MiddleName);
            Assert.Equal("Doe", owner.Name.LastName);
        }

        internal async Task WhenICreateOwnerAsync()
        {
            this.controllerResponse = await this.ownerController.CreateOwnerAsync(this.ownerModel);
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
                    this.ownerController?.Dispose();
                    this.coreContext?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
