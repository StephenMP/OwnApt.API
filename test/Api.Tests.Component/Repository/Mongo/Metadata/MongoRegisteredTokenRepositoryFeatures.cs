using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepositoryFeatures : IClassFixture<MongoEnvironmentClassFixture>
    {
        private readonly MongoRegisteredTokenRepositorySteps steps;

        public MongoRegisteredTokenRepositoryFeatures(MongoEnvironmentClassFixture mongoFixture)
        {
            this.steps = new MongoRegisteredTokenRepositorySteps(mongoFixture);
        }

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
            this.steps.GivenIHaveARegisteredTokenToRead();
            await this.steps.WhenIReadAsync();
            this.steps.ThenICanVerifyICanReadRegisteredToken();
        }

        [Fact]
        public async Task CanReadRegisteredTokenByToken()
        {
            this.steps.GivenIHaveAMongoMetadataContext();
            this.steps.GivenIHaveARegisteredTokenRepository();
            this.steps.GivenIHaveARegisteredTokenToRead();
            await this.steps.WhenIReadByTokenAsync();
            this.steps.ThenICanVerifyICanReadRegisteredToken();
        }

        [Fact]
        public async Task CannotUpdateRegisteredToken()
        {
            this.steps.GivenIHaveAMongoMetadataContext();
            this.steps.GivenIHaveARegisteredTokenRepository();
            this.steps.GivenIHaveAnUpdateRegisteredTokenAction();
            await this.steps.ThenICanVerifyICannotUpdateRegisteredToken();
        }

        [Fact]
        public async Task CannotDeleteRegisteredToken()
        {
            this.steps.GivenIHaveAMongoMetadataContext();
            this.steps.GivenIHaveARegisteredTokenRepository();
            this.steps.GivenIHaveADeleteRegisteredTokenAction();
            await this.steps.ThenICanVerifyICannotDeleteRegisteredToken();
        }
    }
}
