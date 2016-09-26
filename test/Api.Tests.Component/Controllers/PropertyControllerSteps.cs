using Moq;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Controllers;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Service;
using OwnApt.Api.Repository.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    internal class PropertyControllerSteps : ControllerSteps
    {
        #region Private Fields

        private readonly Random random = new Random();
        private PropertyModel mockedPropertyModel;
        private PropertyController propertyController;
        private string propertyId;
        private IPropertyRepository propertyRepository;
        private IPropertyService propertyService;

        #endregion Private Fields

        #region Internal Methods

        internal void GivenIHaveAMockedPropertyRepository()
        {
            var mockedPropertyRepository = new Mock<IPropertyRepository>();
            mockedPropertyRepository.Setup(p => p.CreateAsync(this.mockedPropertyModel)).Returns(Task.FromResult(this.mockedPropertyModel));
            mockedPropertyRepository.Setup(p => p.ReadAsync(this.propertyId)).Returns(Task.FromResult(this.mockedPropertyModel));
            mockedPropertyRepository.Setup(p => p.UpdateAsync(this.mockedPropertyModel)).Returns(Task.FromResult(true));
            mockedPropertyRepository.Setup(p => p.DeleteAsync(this.propertyId)).Returns(Task.FromResult(true));

            this.propertyRepository = mockedPropertyRepository.Object;
        }

        internal void GivenIHaveAPropertyController()
        {
            this.propertyController = new PropertyController(this.propertyService, null);
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

        internal async Task WhenICallCreatePropertyAsync()
        {
            this.controllerResponse = await this.propertyController.CreatePropertyAsync(this.mockedPropertyModel);
        }

        internal async Task WhenICallDeletePropertyAsync()
        {
            this.controllerResponse = await this.propertyController.DeletePropertyAsync(this.propertyId);
        }

        internal async Task WhenICallReadPropertyAsync()
        {
            this.controllerResponse = await this.propertyController.ReadPropertyAsync(this.propertyId);
        }

        internal async Task WhenICallUpdatePropertyAsync()
        {
            this.controllerResponse = await this.propertyController.UpdatePropertyAsync(this.mockedPropertyModel);
        }

        #endregion Internal Methods
    }
}
