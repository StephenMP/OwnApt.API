using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoRegisteredTokenRepositorySteps : IDisposable
    {
        private OwnAptTestEnvironment testEnvironment;

        internal void GivenIHaveAnEnvironment()
        {
            this.testEnvironment = new OwnAptTestEnvironmentBuilder()
                                        .AddMongo()
                                        .BuildEnvironment();
        }

        internal async Task WhenIReadByTokenAsync()
        {
            this.resultModel = await this.registeredTokenRepository.ReadByTokenAsync(this.registeredTokenEntity.Token);
        }

        internal void ThenICanVerifyICanReadRegisteredTokenByTokenAsync()
        {
            Assert.Equal(this.currentRegisteredTokenId, this.resultModel.Id);
            Assert.Equal(this.currentRegisteredToken, this.resultModel.Token);
        }

        internal async Task WhenIReadByIdAsync()
        {
            this.resultModel = await this.registeredTokenRepository.ReadAsync(this.registeredTokenEntity.Id);
        }

        internal void GivenIHaveARegisteredTokenToCreate()
        {
            this.registeredTokenModelToCreate = new RegisteredTokenModel

            {
                Id = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString()
            };

            this.currentRegisteredTokenId = this.registeredTokenModelToCreate.Id;
            this.currentRegisteredToken = this.registeredTokenModelToCreate.Token;
        }

        internal void ThenICanVerifyICanCanCreateRegisteredTokenAsync()
        {
            Assert.Equal(this.currentRegisteredTokenId, this.resultModel.Id);
            Assert.Equal(this.currentRegisteredToken, this.resultModel.Token);
        }

        internal async Task WhenICreateAsync()
        {
            this.resultModel = await this.registeredTokenRepository.CreateAsync(this.registeredTokenModelToCreate);
        }

        internal void GivenIHaveARegisteredTokenToRead()
        {
            this.registeredTokenEntity = new RegisteredTokenEntity
            {
                Id = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString()
            };

            this.currentRegisteredTokenId = this.registeredTokenEntity.Id;
            this.currentRegisteredToken = this.registeredTokenEntity.Token;

            this.testEnvironment.ImportMongoDataAsync("Core", "Metadata", new RegisteredTokenEntity[] { this.registeredTokenEntity });
        }

        internal void GivenIHaveARegisteredTokenRepository()
        {
            this.registeredTokenRepository = new MongoRegisteredTokenRepository(this.testEnvironment.GetMongoClient(), OwnAptStartup.BuildMapper());
        }

        #region IDisposable Support
        private bool disposedValue;
        private RegisteredTokenEntity registeredTokenEntity;
        private IRegisteredTokenRepository registeredTokenRepository;
        private RegisteredTokenModel resultModel;
        private RegisteredTokenModel registeredTokenModelToCreate;
        private string currentRegisteredToken;
        private string currentRegisteredTokenId;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
