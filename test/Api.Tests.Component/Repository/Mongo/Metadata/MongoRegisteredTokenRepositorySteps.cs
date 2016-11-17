using System.Threading.Tasks;
using AutoMapper;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo.Metadata;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepositorySteps
    {
        #region Private Fields

        private readonly MongoEnvironmentClassFixture mongoFixture;
        private string currentRegisteredToken;
        private string currentRegisteredTokenId;
        private IMapper mapper;
        private IMongoMetadataContext mongoMetadataContext;
        private IRegisteredTokenRepository mongoRegisteredTokenRepository;
        private RegisteredTokenModel registeredTokenModelToCreate;
        private RegisteredTokenModel registeredTokenModelToRead;
        private RegisteredTokenModel resultModel;

        #endregion Private Fields

        #region Public Constructors

        public MongoRegisteredTokenRepositorySteps(MongoEnvironmentClassFixture mongoFixture)
        {
            this.mongoFixture = mongoFixture;
            this.mapper = OwnAptStartup.BuildMapper();
        }

        #endregion Public Constructors

        #region Internal Methods

        internal void GivenIHaveAMongoMetadataContext()
        {
            this.mongoMetadataContext = new MongoMetadataContext(this.mongoFixture.Environment.GetMongoClient());
        }

        internal void GivenIHaveARegisteredTokenRepository()
        {
            this.mongoRegisteredTokenRepository = new MongoRegisteredTokenRepository(this.mongoMetadataContext, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveARegisteredTokenToCreate()
        {
            this.currentRegisteredTokenId = TestRandom.String;
            this.registeredTokenModelToCreate = RegisteredTokenRandom.RegisteredTokenModel(this.currentRegisteredTokenId);
        }

        internal async Task GivenIHaveARegisteredTokenToRead()
        {
            this.currentRegisteredTokenId = TestRandom.String;
            this.registeredTokenModelToRead = RegisteredTokenRandom.RegisteredTokenModel(this.currentRegisteredTokenId);
            this.currentRegisteredToken = this.registeredTokenModelToRead.Token;
            var registeredTokenEntityToRead = this.mapper.Map<RegisteredTokenEntity>(this.registeredTokenModelToRead);
            await this.mongoFixture.Environment.ImportMongoDataAsync("Metadata", "RegisteredToken", new[] { registeredTokenEntityToRead });
        }

        internal void ThenICanVerifyICanCreateRegisteredToken()
        {
            Assert.Equal(this.registeredTokenModelToCreate, this.resultModel);
        }

        internal void ThenICanVerifyICanReadRegisteredToken()
        {
            Assert.Equal(this.registeredTokenModelToRead, this.resultModel);
        }

        internal async Task WhenICreateAsync()
        {
            this.resultModel = await this.mongoRegisteredTokenRepository.CreateAsync(this.registeredTokenModelToCreate);
        }

        internal async Task WhenIReadAsync()
        {
            this.resultModel = await this.mongoRegisteredTokenRepository.ReadAsync(this.currentRegisteredTokenId);
        }

        internal async Task WhenIReadByTokenAsync()
        {
            this.resultModel = await this.mongoRegisteredTokenRepository.ReadByTokenAsync(this.currentRegisteredToken);
        }

        #endregion Internal Methods
    }

    internal static class RegisteredTokenRandom
    {
        #region Public Methods

        public static RegisteredTokenModel RegisteredTokenModel(string registeredTokenId = "1234567890abcdefg") => new RegisteredTokenModel
        {
            Id = registeredTokenId,
            Token = TestRandom.String
        };

        #endregion Public Methods
    }
}
