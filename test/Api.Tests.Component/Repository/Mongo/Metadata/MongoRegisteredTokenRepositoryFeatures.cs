using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepositoryFeatures : IClassFixture<MongoEnvironmentClassFixture>
    {
        #region Private Fields

        private readonly MongoRegisteredTokenRepositorySteps steps;

        #endregion Private Fields

        #region Public Constructors

        public MongoRegisteredTokenRepositoryFeatures(MongoEnvironmentClassFixture mongoFixture)
        {
            this.steps = new MongoRegisteredTokenRepositorySteps(mongoFixture);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreateRegisteredToken()
        {
            this.steps.GivenIHaveAMongoMetadataContext();
            this.steps.GivenIHaveARegisteredTokenRepository();
            this.steps.GivenIHaveARegisteredTokenToCreate();
            await this.steps.WhenICreateAsync();
            this.steps.ThenICanVerifyICanCreateRegisteredToken();
        }

        [Fact]
        public async Task CanReadRegisteredToken()
        {
            this.steps.GivenIHaveAMongoMetadataContext();
            this.steps.GivenIHaveARegisteredTokenRepository();
            await this.steps.GivenIHaveARegisteredTokenToRead();
            await this.steps.WhenIReadAsync();
            this.steps.ThenICanVerifyICanReadRegisteredToken();
        }

        [Fact]
        public async Task CanReadRegisteredTokenByToken()
        {
            this.steps.GivenIHaveAMongoMetadataContext();
            this.steps.GivenIHaveARegisteredTokenRepository();
            await this.steps.GivenIHaveARegisteredTokenToRead();
            await this.steps.WhenIReadByTokenAsync();
            this.steps.ThenICanVerifyICanReadRegisteredToken();
        }

        #endregion Public Methods
    }
}
