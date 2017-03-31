using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository
{
    public abstract class RepositoryFeatures<TModel, TEntity> : IDisposable
    {
        #region Protected Fields

        protected readonly RepositorySteps<TModel, TEntity> steps;

        #endregion Protected Fields

        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Protected Constructors

        protected RepositoryFeatures(RepositorySteps<TModel, TEntity> steps)
        {
            this.steps = steps;
        }

        #endregion Protected Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreateAsync()
        {
            this.steps.GivenIHaveATestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToCreate();
            await this.steps.WhenICallCreateAsync();
            this.steps.ThenICanVerifyICanCreateAsync();
        }

        [Fact]
        public async Task CanDeleteAsync()
        {
            this.steps.GivenIHaveATestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToDelete();
            await this.steps.WhenICallDeleteAsync();
            await this.steps.ThenICanVerifyICanDeleteAsync();
        }

        [Fact]
        public async Task CanReadAllAsync()
        {
            this.steps.GivenIHaveATestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntitiesToRead();
            await this.steps.WhenICallReadAllAsync();
            this.steps.ThenICanVerifyICanReadAllAsync();
        }

        [Fact]
        public async Task CanReadAsync()
        {
            this.steps.GivenIHaveATestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToRead();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyICanReadAsync();
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            this.steps.GivenIHaveATestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToUpdate();
            await this.steps.WhenICallUpdateAsyncAction();
            await this.steps.ThenICanVerifyICanUpdateAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods
    }
}
