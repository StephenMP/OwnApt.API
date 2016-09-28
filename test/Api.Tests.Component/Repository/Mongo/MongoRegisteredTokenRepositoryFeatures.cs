using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public class SqlRegisteredTokenRepositoryFeatures : IDisposable
    {
        private readonly MongoRegisteredTokenRepositorySteps steps;

        public SqlRegisteredTokenRepositoryFeatures()
        {
            this.steps = new MongoRegisteredTokenRepositorySteps();
        }

        [Fact]
        public async Task CanReadRegisteredTokenByTokenAsync()
        {
            this.steps.GivenIHaveAnEnvironment();
            this.steps.GivenIHaveARegisteredTokenToRead();
            this.steps.GivenIHaveARegisteredTokenRepository();
            await this.steps.WhenIReadByTokenAsync();
            this.steps.ThenICanVerifyICanReadRegisteredTokenByTokenAsync();
        }

        [Fact]
        public async Task CanReadRegisteredTokenByIdAsync()
        {
            this.steps.GivenIHaveAnEnvironment();
            this.steps.GivenIHaveARegisteredTokenToRead();
            this.steps.GivenIHaveARegisteredTokenRepository();
            await this.steps.WhenIReadByIdAsync();
            this.steps.ThenICanVerifyICanReadRegisteredTokenByTokenAsync();
        }

        [Fact]
        public async Task CanCreateRegisteredTokenAsync()
        {
            this.steps.GivenIHaveAnEnvironment();
            this.steps.GivenIHaveARegisteredTokenToCreate();
            this.steps.GivenIHaveARegisteredTokenRepository();
            await this.steps.WhenICreateAsync();
            this.steps.ThenICanVerifyICanCanCreateRegisteredTokenAsync();
        }

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.steps?.Dispose();
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
