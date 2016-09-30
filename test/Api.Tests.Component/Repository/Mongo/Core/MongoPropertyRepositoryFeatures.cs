using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Core
{
    public class MongoPropertyRepositoryFeatures : IClassFixture<MongoEnvironmentClassFixture>
    {
        #region Private Fields

        private readonly MongoPropertyRepositorySteps steps;

        #endregion Private Fields

        #region Public Constructors

        public MongoPropertyRepositoryFeatures(MongoEnvironmentClassFixture mongoFixture)
        {
            this.steps = new MongoPropertyRepositorySteps(mongoFixture);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreatePropertyAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoPropertyRepository();
            this.steps.GivenIHaveAnPropertyToCreate();
            await this.steps.WhenICallCreateAsync();
            this.steps.ThenICanVerifyICanCreatePropertyAsync();
        }

        [Fact]
        public async Task CanDeletePropertyAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoPropertyRepository();
            await this.steps.GivenIHaveAnPropertyToDelete();
            await this.steps.WhenICallDeleteAsync();
            await this.steps.ThenICanVerifyICanDeletePropertyAsync();
        }

        [Fact]
        public async Task CanReadPropertyAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoPropertyRepository();
            await this.steps.GivenIHaveAPropertyToRead();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyICanReadPropertyAsync();
        }

        [Fact]
        public async Task CanReadManyPropertyAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoPropertyRepository();
            await this.steps.GivenIHaveManyPropertiesToRead();
            await this.steps.WhenICallReadManyAsync();
            this.steps.ThenICanVerifyICanReadManyPropertyAsync();
        }

        [Fact]
        public async Task CanUpdatePropertyAsync()
        {
            this.steps.GivenIHaveAMongoCoreContext();
            this.steps.GivenIHaveAMongoPropertyRepository();
            await this.steps.GivenIHaveAnPropertyToUpdate();
            await this.steps.WhenICallUpdateAsync();
            await this.steps.ThenICanVerifyICanUpdatePropertyAsync();
        }

        #endregion Public Methods
    }
}
