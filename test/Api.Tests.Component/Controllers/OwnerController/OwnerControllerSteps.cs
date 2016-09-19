using Microsoft.AspNetCore.Mvc;
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
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.OwnerControllerTests
{
    public class OwnerControllerSteps : IDisposable
    {
        #region Private Fields

        private CoreContext coreContext;
        private bool disposedValue;
        private OwnerController ownerController;
        private IActionResult ownerControllerActionResult;
        private object ownerControllerContent;
        private OwnerModel ownerModel;
        private IOwnerRepository ownerRepository;
        private IOwnerService ownerService;
        private TestingEnvironment testEnvironment;

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

        internal void GivenIHaveAnOwnerController()
        {
            this.ownerController = new OwnerController(this.ownerService);
        }

        internal void GivenIHaveAOwnerEnvironment()
        {
            this.testEnvironment = new TestingEnvironment();

            // Add Mongo Dependency
            this.testEnvironment.AddMongo();
        }

        internal void GivenIHaveAOwnerRepository()
        {
            this.ownerRepository = new MongoOwnerRepository(this.testEnvironment.MongoClient(), OwnAptStartup.BuildMapper());
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
            var owner = this.ownerControllerContent as OwnerModel;
            Assert.NotNull(owner);
            Assert.Equal("John", owner.Name.FirstName);
            Assert.Equal("C", owner.Name.MiddleName);
            Assert.Equal("Doe", owner.Name.LastName);
        }

        internal void ThenICanVerifyIReceived<TModel>(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (typeof(TModel) != typeof(Missing))
                {
                    var content = Assert.IsType<OkObjectResult>(this.ownerControllerActionResult);
                    this.ownerControllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
                else
                {
                    Assert.IsType<OkResult>(this.ownerControllerActionResult);
                }
            }
            else if (statusCode == HttpStatusCode.Created)
            {
                var content = Assert.IsType<CreatedResult>(this.ownerControllerActionResult);
                this.ownerControllerContent = content.Value;
                Assert.Equal((int)statusCode, content.StatusCode.Value);
            }
        }

        internal async Task WhenICreateOwnerAsync()
        {
            ownerControllerActionResult = await this.ownerController.CreateOwnerAsync(this.ownerModel);
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
                    this.coreContext?.Dispose();
                    this.ownerController?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
