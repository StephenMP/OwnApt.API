using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class ControllerSteps
    {
        public ControllerSteps()
        {
            this.mapper = OwnAptStartup.BuildMapper();
        }
        #region Protected Fields

        protected object controllerContent;
        protected IActionResult controllerResponse;
        protected OwnerEntity mockedOwnerEntity;
        protected OwnerModel mockedOwnerModel;
        protected RegisteredTokenEntity mockedRegisteredTokenEntity;
        protected RegisteredTokenModel mockedRegisteredTokenModel;
        protected IOwnerRepository ownerRepository;
        protected IRegisteredTokenRepository registeredTokenRepository;

        #endregion Protected Fields

        #region Private Fields

        private IMapper mapper;
        protected Mock<IOwnerRepository> mockedOwnerRepository;
        protected Mock<IRegisteredTokenRepository> mockedRegisteredTokenRepository;

        #endregion Private Fields

        #region Public Methods

        public void ThenICanVerifyIReceived<TModel>(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (typeof(TModel) != typeof(Missing))
                {
                    var content = Assert.IsType<OkObjectResult>(this.controllerResponse);
                    this.controllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
                else
                {
                    Assert.IsType<OkResult>(this.controllerResponse);
                }
            }
            else if (statusCode == HttpStatusCode.Created)
            {
                var content = Assert.IsType<CreatedResult>(this.controllerResponse);
                this.controllerContent = content.Value;
                Assert.Equal((int)statusCode, content.StatusCode.Value);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected void GivenIHaveAMockedDataLayer(DataLayerMockOptions options)
        {
            if (options.MockRegisteredToken)
            {
                this.MockRegisteredTokenRepository();
            }

            if (options.MockOwner)
            {
                this.MockOwnerRepository();
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void MockOwnerRepository()
        {
            this.mockedOwnerEntity = new OwnerEntity { Id = Guid.NewGuid().ToString("N"), Name = new NameEntity { FirstName = "John", MiddleName = "C", LastName = "Doe" } };
            this.mockedOwnerModel = this.mapper.Map<OwnerModel>(this.mockedOwnerEntity);

            this.mockedOwnerRepository = new Mock<IOwnerRepository>();
            mockedOwnerRepository.Setup(s => s.CreateAsync(this.mockedOwnerModel)).Returns(Task.FromResult(this.mockedOwnerModel));
            mockedOwnerRepository.Setup(s => s.DeleteAsync(this.mockedOwnerModel.Id)).Returns(Task.CompletedTask);
            mockedOwnerRepository.Setup(s => s.ReadAsync(this.mockedOwnerModel.Id)).Returns(Task.FromResult(this.mockedOwnerModel));
            mockedOwnerRepository.Setup(s => s.UpdateAsync(this.mockedOwnerModel)).Returns(Task.CompletedTask);

            this.ownerRepository = mockedOwnerRepository.Object;
        }

        private void MockRegisteredTokenRepository()
        {
            this.mockedRegisteredTokenEntity = new RegisteredTokenEntity { Id = Guid.NewGuid().ToString(), Token = Guid.NewGuid().ToString() };
            this.mockedRegisteredTokenModel = this.mapper.Map<RegisteredTokenModel>(this.mockedRegisteredTokenEntity);

            this.mockedRegisteredTokenRepository = new Mock<IRegisteredTokenRepository>();
            mockedRegisteredTokenRepository.Setup(s => s.CreateAsync(this.mockedRegisteredTokenModel)).Returns(Task.FromResult(this.mockedRegisteredTokenModel));
            mockedRegisteredTokenRepository.Setup(s => s.ReadAsync(this.mockedRegisteredTokenEntity.Id)).Returns(Task.FromResult(this.mockedRegisteredTokenModel));
            mockedRegisteredTokenRepository.Setup(s => s.ReadByTokenAsync(this.mockedRegisteredTokenEntity.Token)).Returns(Task.FromResult(this.mockedRegisteredTokenModel));

            this.registeredTokenRepository = mockedRegisteredTokenRepository.Object;
        }

        #endregion Private Methods
    }

    public class DataLayerMockOptions
    {
        #region Public Properties

        public bool MockOwner { get; private set; }
        public bool MockRegisteredToken { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public DataLayerMockOptions MockOwnerRepository()
        {
            this.MockOwner = true;
            return this;
        }

        public DataLayerMockOptions MockRegisteredTokenRepository()
        {
            this.MockRegisteredToken = true;
            return this;
        }

        #endregion Public Methods
    }
}
