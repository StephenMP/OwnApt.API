using Microsoft.AspNetCore.Mvc;
using Moq;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
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
        #region Fields

        private readonly Random random = new Random();
        private object controllerContent;
        private IActionResult controllerIActionResult;
        private PropertyModel mockedPropertyModel;
        private PropertyController propertyController;
        private string propertyId;
        private IPropertyRepository propertyRepository;
        private IPropertyService propertyService;

        #endregion Fields

        #region Methods

        internal void GivenIHaveAMockedPropertyRepository()
        {
            var mockedPropertyRepository = new Mock<IPropertyRepository>();
            mockedPropertyRepository.Setup(p => p.CreateAsync(this.mockedPropertyModel)).Returns(Task.FromResult(this.mockedPropertyModel));
            mockedPropertyRepository.Setup(p => p.ReadAsync(this.propertyId)).Returns(Task.FromResult(this.mockedPropertyModel));
            mockedPropertyRepository.Setup(p => p.ReadPropertiesForOwnerAsync(this.propertyId)).Returns(Task.FromResult(new PropertyModel[] { this.mockedPropertyModel }));
            mockedPropertyRepository.Setup(p => p.ReadPropertiesForTenantAsync(this.propertyId)).Returns(Task.FromResult(new PropertyModel[] { this.mockedPropertyModel }));
            mockedPropertyRepository.Setup(p => p.UpdateAsync(this.mockedPropertyModel)).Returns(Task.FromResult(true));
            mockedPropertyRepository.Setup(p => p.DeleteAsync(this.propertyId)).Returns(Task.FromResult(true));

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

        internal void ThenICanVerifyICreateProperty()
        {
            var propertyModel = this.controllerContent as PropertyModel;

            Assert.NotNull(propertyModel);
            Assert.Equal(this.propertyId, propertyModel.Id);
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
                    var content = Assert.IsType<OkObjectResult>(this.controllerIActionResult);
                    this.controllerContent = content.Value;
                    Assert.Equal((int)statusCode, content.StatusCode.Value);
                }
                else
                {
                    Assert.IsType<OkResult>(this.controllerIActionResult);
                }
            }
            else if (statusCode == HttpStatusCode.Created)
            {
                var content = Assert.IsType<CreatedResult>(this.controllerIActionResult);
                this.controllerContent = content.Value;
                Assert.Equal((int)statusCode, content.StatusCode.Value);
            }
        }

        internal async Task WhenICallCreatePropertyAsync()
        {
            this.controllerIActionResult = await this.propertyController.CreatePropertyAsync(this.mockedPropertyModel);
        }

        internal async Task WhenICallDeletePropertyAsync()
        {
            this.controllerIActionResult = await this.propertyController.DeletePropertyAsync(this.propertyId);
        }

        internal async Task WhenICallReadPropertyAsync()
        {
            this.controllerIActionResult = await this.propertyController.ReadPropertyAsync(this.propertyId);
        }

        internal async Task WhenICallUpdatePropertyAsync()
        {
            this.controllerIActionResult = await this.propertyController.UpdatePropertyAsync(this.propertyId, this.mockedPropertyModel);
        }

        #endregion Methods
    }
}
