using OwnApt.Api.Domain.Model;
using System.Net;
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
        public async Task CanReadProperty()
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

        #endregion Public Methods
    }
}
