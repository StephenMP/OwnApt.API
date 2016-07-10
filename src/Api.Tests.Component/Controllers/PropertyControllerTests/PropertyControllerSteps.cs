using Microsoft.AspNet.Mvc;
using Moq;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Entity;
using OwnApt.Api.Repository.Interface;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.PropertyControllerTests
{
    internal class PropertyControllerSteps
    {
        #region Private Fields

        private object controllerContent;
        private IActionResult controllerIActionResult;
        private PropertyModel mockedPropertyModel;
        private PropertyController propertyController;
        private PropertyEntity propertyEntity;
        private string propertyId;
        private PropertyModel propertyModelResponse;
        private IPropertyRepository propertyRepository;
        private IPropertyService propertyService;
        private Random random = new Random();

        #endregion Private Fields

        #region Internal Methods

        internal void GivenIHaveAMockedPropertyRepository()
        {
            var mockedPropertyRepository = new Mock<IPropertyRepository>();
            mockedPropertyRepository.Setup(p => p.ReadAsync(this.propertyId)).Returns(Task.FromResult(this.mockedPropertyModel));

            this.propertyRepository = mockedPropertyRepository.Object;
        }

        internal void GivenIHaveAPropertyController()
        {
            this.propertyController = new PropertyController(this.propertyService);
        }

        internal void GivenIHaveAPropertyId()
        {
            this.propertyId = random.Next().ToString();
        }

        internal void GivenIHaveAPropertyModel()
        {
            this.mockedPropertyModel = new PropertyModel
            {
                Id = this.propertyId
            };
        }

        internal void GivenIHaveAPropertyService()
        {
            this.propertyService = new PropertyService(this.propertyRepository);
        }

        internal void ThenICanVerifyIReadProperty()
        {
            var propertyModel = this.controllerContent as PropertyModel;

            Assert.NotNull(propertyModel);
            Assert.Equal(this.propertyId, propertyModel.Id);
        }

        internal void ThenICanVerifyIReceived<T>(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (typeof(T) != typeof(Missing))
                {
                    var content = Assert.IsType<HttpOkObjectResult>(this.controllerIActionResult);
                    this.controllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
            }
        }

        internal async Task WhenICallReadProperty()
        {
            this.controllerIActionResult = await this.propertyController.ReadProperty(this.propertyId);
        }

        #endregion Internal Methods
    }
}
