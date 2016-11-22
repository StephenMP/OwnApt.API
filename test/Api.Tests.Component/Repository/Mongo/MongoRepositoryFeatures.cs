using System;
using System.Threading.Tasks;
using OwnApt.Api.Repository.Entity.Mongo;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public abstract class MongoRepositoryFeatures<TModel, TEntity> : IDisposable where TEntity : MongoEntity
    {
        #region Protected Fields

        protected readonly MongoRepositorySteps<TModel, TEntity> steps;

        #endregion Protected Fields

        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Protected Constructors

        protected MongoRepositoryFeatures(MongoRepositorySteps<TModel, TEntity> steps)
        {
            this.steps = steps;
        }

        #endregion Protected Constructors

        #region Public Methods

        [Fact]
        public async Task CanCreateAsync()
        {
            this.steps.GivenIHaveAMongoTestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToCreate();
            await this.steps.WhenICallCreateAsync();
            this.steps.ThenICanVerifyICanCreateAsync();
        }

        [Fact]
        public async Task CanDeleteAsync()
        {
            this.steps.GivenIHaveAMongoTestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToDelete();
            await this.steps.WhenICallDeleteAsync();
            await this.steps.ThenICanVerifyICanDeleteAsync();
        }

        [Fact]
        public async Task CanReadAllAsync()
        {
            this.steps.GivenIHaveAMongoTestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntitiesToRead();
            await this.steps.WhenICallReadAllAsync();
            this.steps.ThenICanVerifyICanReadAllAsync();
        }

        [Fact]
        public async Task CanReadAsync()
        {
            this.steps.GivenIHaveAMongoTestEnvironment();
            this.steps.GivenIHaveARepository();
            this.steps.GivenIHaveAnEntityToRead();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyICanReadAsync();
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            this.steps.GivenIHaveAMongoTestEnvironment();
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
