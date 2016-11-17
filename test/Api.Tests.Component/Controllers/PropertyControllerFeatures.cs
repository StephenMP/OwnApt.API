using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using Xunit;

namespace Api.Tests.Component.Controllers
{
    public class PropertyControllerFeatures
    {
        #region Private Fields

        private readonly PropertyControllerSteps steps;

        #endregion Private Fields

        #region Public Constructors

        public PropertyControllerFeatures()
        {
            this.steps = new PropertyControllerSteps();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreatePropertyAsync()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallCreatePropertyAsync();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.Created);
            this.steps.ThenICanVerifyICreateProperty();
        }

        [Fact]
        public async Task CanDeletePropertyAsync()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadPropertyAsync();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }

        [Fact]
        public async Task CanReadPropertyAsync()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallDeletePropertyAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CanReadPropertyForOwnerAsync()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadPropertyAsync();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }

        [Fact]
        public async Task CanReadPropertyForTenantAsync()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadPropertyAsync();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }

        [Fact]
        public async Task CanUpdatePropertyAsync()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyModel();
            this.steps.GivenIHaveAMockedPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallUpdatePropertyAsync();
            this.steps.ThenICanVerifyIReceived<Missing>(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}
