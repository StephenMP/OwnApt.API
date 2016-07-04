
using OwnApt.Api.Domain.Model;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Controllers.PropertyControllerTests
{
    public class PropertyControllerFeatures
    {
        private PropertyControllerSteps steps = new PropertyControllerSteps();

        [Fact]
        public async Task CanReadProperty()
        {
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyEntity();
            this.steps.GivenIHaveAMockedPropertyCollection();
            this.steps.GivenIHaveAMockedMongoCoreDatabase();
            this.steps.GivenIHaveAMockedMongoClient();
            this.steps.GivenIHaveAnAutoMapper();
            this.steps.GivenIHaveAPropertyRepository();
            this.steps.GivenIHaveAPropertyService();
            this.steps.GivenIHaveAPropertyController();
            await this.steps.WhenICallReadProperty();
            this.steps.ThenICanVerifyIReceived<PropertyModel>(HttpStatusCode.OK);
            this.steps.ThenICanVerifyIReadProperty();
        }
    }
}
