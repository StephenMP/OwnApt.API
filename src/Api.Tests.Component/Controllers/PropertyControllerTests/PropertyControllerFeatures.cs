using OwnApt.Api.Domain.Model;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.PropertyControllerTests
{
    public class PropertyControllerFeatures
    {
        #region Private Fields

        private PropertyControllerSteps steps = new PropertyControllerSteps();

        #endregion Private Fields

        #region Public Methods

        [Fact]
        public async Task CanCreateProperty()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallCreateProperty();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.Created);
            this.steps.ThenICanVerifyICreateProperty();
        }

        [Fact]
        public async Task CanDeleteProperty()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadProperty();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }

        [Fact]
        public async Task CanReadProperty()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallDeleteProperty();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanReadPropertyForOwner()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadProperty();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }

        [Fact]
        public async Task CanReadPropertyForTenant()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadProperty();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }

        [Fact]
        public async Task CanUpdateProperty()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallUpdateProperty();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}
