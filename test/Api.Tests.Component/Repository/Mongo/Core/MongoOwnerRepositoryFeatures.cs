using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Core
{
    public class MongoOwnerRepositoryFeatures : IClassFixture<MongoEnvironmentClassFixture>
    {
        #region Private Fields

        private readonly MongoOwnerRepositorySteps steps;

        #endregion Private Fields

        #region Public Constructors

        public MongoOwnerRepositoryFeatures(MongoEnvironmentClassFixture mongoFixture)
        {
            this.steps = new MongoOwnerRepositorySteps(mongoFixture);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreateOwnerAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoOwnerRepository();
            this.steps.GivenIHaveAnOwnerToCreate();
            await this.steps.WhenICallCreateAsync();
            this.steps.ThenICanVerifyICanCreateOwnerAsync();
        }

        [Fact]
        public async Task CanDeleteOwnerAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoOwnerRepository();
            await this.steps.GivenIHaveAnOwnerToDelete();
            await this.steps.WhenICallDeleteAsync();
            await this.steps.ThenICanVerifyICanDeleteOwnerAsync();
        }

        [Fact]
        public async Task CanReadOwnerAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoOwnerRepository();
            await this.steps.GivenIHaveAnOwnerToRead();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyICanReadOwnerAsync();
        }

        [Fact]
        public async Task CanUpdateOwnerAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoOwnerRepository();
            await this.steps.GivenIHaveAnOwnerToUpdate();
            await this.steps.WhenICallUpdateAsync();
            await this.steps.ThenICanVerifyICanUpdateOwnerAsync();
        }

        #endregion Public Methods
    }
}
