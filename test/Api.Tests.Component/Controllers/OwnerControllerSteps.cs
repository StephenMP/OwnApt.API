using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Sql.Core;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class OwnerControllerSteps : ControllerSteps, IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private OwnerController ownerController;
        private OwnerModel ownerModel;
        private IOwnerRepository ownerRepository;
        private IOwnerService ownerService;
        private OwnAptTestEnvironment testEnvironment;

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
            this.ownerController = new OwnerController(this.ownerService);
        }

        internal void GivenIHaveAOwnerEnvironment()
        {
            this.testEnvironment = OwnAptTestEnvironment
                                    .CreateEnvironment()
                                    .UseMongo();
        }

        internal void GivenIHaveAOwnerRepository()
        {
            this.ownerRepository = new MongoOwnerRepository(this.testEnvironment.GetMongoClient(), OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAOwnerService()
        {
            this.ownerService = new OwnerService(this.ownerRepository);
        }

        internal void GivenIHaveAOwnerToCreate()
        {
            this.ownerModel = new OwnerModel
            {
                Name = new NameModel { FirstName = "John", MiddleName = "C", LastName = "Doe" }
            };
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
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
