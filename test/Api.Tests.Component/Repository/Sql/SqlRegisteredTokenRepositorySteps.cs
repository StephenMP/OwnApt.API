using OwnApt.Api.AppStart;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Sql.Core;
using OwnApt.TestEnvironment.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using Xunit;

namespace Api.Tests.Component.Repository.Sql
{
    public class SqlRegisteredTokenRepositorySteps : IDisposable
    {
        private OwnAptTestEnvironment testEnvironment;

        internal void GivenIHaveAnEnvironment()
        {
            this.testEnvironment = new OwnAptTestEnvironmentBuilder()
                                        .AddSqlContext<CoreContext>()
                                        .BuildEnvironment();
        }

        internal void GivenIHaveARegisteredTokenToRead()
        {
            this.GivenIHaveARegisteredTokenToCreate();
            var entity = new RegisteredTokenEntity
            {
                Token = this.registeredTokenToCreate.Token,
                TokenId = this.registeredTokenToCreate.TokenId
        };

            this.testEnvironment.ImportSqlDataAsync<CoreContext, RegisteredTokenEntity>(new RegisteredTokenEntity[] { entity });
        }

        internal async Task WhenIReadByIdAsync()
        {
            this.resultModel = await this.registeredTokenRepository.ReadAsync(this.currentTokenId);
        }

        internal void GivenIHaveARegisteredTokenToCreate()
        {
            this.registeredTokenToCreate = new RegisteredTokenModel
            {
                TokenId = new Random().Next(),
                Token = Guid.NewGuid().ToString()
            };

            this.currentTokenId = registeredTokenToCreate.TokenId;
            this.currentToken = registeredTokenToCreate.Token;
        }

        internal async Task WhenICreateAsync()
        {
            this.resultModel = await this.registeredTokenRepository.CreateAsync(this.registeredTokenToCreate);
        }

        internal void ThenICanVerifyICanCanCreateRegisteredTokenAsync()
        {
            Assert.Equal(this.registeredTokenToCreate.Token, resultModel.Token);
        }

        internal void ThenICanVerifyICanReadRegisteredTokenByTokenAsync()
        {
            Assert.NotNull(this.resultModel);
            Assert.Equal(this.currentTokenId, this.resultModel.TokenId);
            Assert.Equal(this.currentToken, this.resultModel.Token);
        }

        internal void GivenIHaveACoreContext()
        {
            this.coreContext = new CoreContext(this.testEnvironment.GetSqlDbContextOptions<CoreContext>());
        }

        internal async Task WhenIReadByTokenAsync()
        {
            this.resultModel = await this.registeredTokenRepository.ReadByTokenAsync(this.currentToken);
        }

        internal void GivenIHaveARegisteredTokenRepository()
        {
            this.registeredTokenRepository = new SqlRegisteredTokenRepository(this.coreContext, OwnAptStartup.BuildMapper());
        }

        #region IDisposable Support
        private bool disposedValue;
        private CoreContext coreContext;
        private IRegisteredTokenRepository registeredTokenRepository;
        private string currentToken;
        private RegisteredTokenModel resultModel;
        private int currentTokenId;
        private RegisteredTokenModel registeredTokenToCreate;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testEnvironment?.Dispose();
                    this.coreContext?.Dispose();
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
