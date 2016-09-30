using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Mongo.Metadata;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;

namespace Api.Tests.Component.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepositorySteps
    {
        private MongoEnvironmentClassFixture mongoFixture;
        private IMongoMetadataContext mongoMetadataContext;
        private IRegisteredTokenRepository mongoRegisteredTokenRepository;
        private string currentRegisteredTokenId;
        private RegisteredTokenModel registeredTokenModelToCreate;
        private RegisteredTokenModel resultModel;
        private RegisteredTokenModel registeredTokenModelToRead;
        private IMapper mapper;
        private string currentRegisteredToken;
        private Func<Task> action;

        public MongoRegisteredTokenRepositorySteps(MongoEnvironmentClassFixture mongoFixture)
        {
            this.mongoFixture = mongoFixture;
            this.mapper = OwnAptStartup.BuildMapper();
        }

        internal void ThenICanVerifyICanCreateRegisteredToken()
        {
            Assert.Equal(this.registeredTokenModelToCreate, this.resultModel);
        }

        internal void ThenICanVerifyICanReadRegisteredToken()
        {
            Assert.Equal(this.registeredTokenModelToRead, this.resultModel);
        }

        internal async Task WhenIReadAsync()
        {
            this.resultModel = await this.mongoRegisteredTokenRepository.ReadAsync(this.currentRegisteredTokenId);
        }

        internal async Task WhenIReadByTokenAsync()
        {
            this.resultModel = await this.mongoRegisteredTokenRepository.ReadByTokenAsync(this.currentRegisteredToken);
        }

        internal void GivenIHaveAnUpdateRegisteredTokenAction()
        {
            this.action = async () => { await this.mongoRegisteredTokenRepository.UpdateAsync(RegisteredTokenRandom.RegisteredTokenModel()); };
        }

        internal void GivenIHaveARegisteredTokenToRead()
        {
            this.currentRegisteredTokenId = TestRandom.String;
            this.registeredTokenModelToRead = RegisteredTokenRandom.RegisteredTokenModel(this.currentRegisteredTokenId);
            this.currentRegisteredToken = this.registeredTokenModelToRead.Token;
            var registeredTokenEntityToRead = this.mapper.Map<RegisteredTokenEntity>(this.registeredTokenModelToRead);
            this.mongoFixture.Environment.ImportMongoDataAsync("Metadata", "RegisteredToken", new[] { registeredTokenEntityToRead });
        }

        internal void GivenIHaveADeleteRegisteredTokenAction()
        {
            this.action = async () => { await this.mongoRegisteredTokenRepository.UpdateAsync(RegisteredTokenRandom.RegisteredTokenModel()); };
        }

        internal async Task ThenICanVerifyICannotDeleteRegisteredToken()
        {
            await Assert.ThrowsAsync<NotSupportedException>(this.action);
        }

        internal async Task ThenICanVerifyICannotUpdateRegisteredToken()
        {
            await Assert.ThrowsAsync<NotSupportedException>(this.action);
        }

        internal async Task WhenICreateAsync()
        {
            this.resultModel = await this.mongoRegisteredTokenRepository.CreateAsync(this.registeredTokenModelToCreate);
        }

        internal void GivenIHaveARegisteredTokenToCreate()
        {
            this.currentRegisteredTokenId = TestRandom.String;
            this.registeredTokenModelToCreate = RegisteredTokenRandom.RegisteredTokenModel(this.currentRegisteredTokenId);
        }

        internal void GivenIHaveARegisteredTokenRepository()
        {
            this.mongoRegisteredTokenRepository = new MongoRegisteredTokenRepository(this.mongoMetadataContext, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAMongoMetadataContext()
        {
            this.mongoMetadataContext = new MongoMetadataContext(this.mongoFixture.Environment.GetMongoClient());
        }
    }

    internal static class RegisteredTokenRandom
    {
        public static RegisteredTokenModel RegisteredTokenModel(string registeredTokenId = "1234567890abcdefg") => new RegisteredTokenModel
        {
            Id = registeredTokenId,
            Token = TestRandom.String
        };
    }
}
