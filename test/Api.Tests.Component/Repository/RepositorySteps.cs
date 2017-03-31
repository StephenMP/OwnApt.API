using System;
using System.Threading.Tasks;
using OwnApt.TestEnvironment.Environment;

namespace Api.Tests.Component.Repository
{
    public abstract class RepositorySteps<TModel, TEntity> : IDisposable
    {
        #region Protected Fields

        protected OwnAptTestEnvironment environment;

        #endregion Protected Fields

        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Protected Constructors

        protected RepositorySteps()
        {
        }

        #endregion Protected Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void GivenIHaveAnEntitiesToRead();

        public abstract void GivenIHaveAnEntityToCreate();

        public abstract void GivenIHaveAnEntityToDelete();

        public abstract void GivenIHaveAnEntityToRead();

        public abstract void GivenIHaveAnEntityToUpdate();

        public abstract void GivenIHaveARepository();

        public abstract void GivenIHaveATestEnvironment();

        public abstract void ThenICanVerifyICanCreateAsync();

        public abstract Task ThenICanVerifyICanDeleteAsync();

        public abstract void ThenICanVerifyICanReadAllAsync();

        public abstract void ThenICanVerifyICanReadAsync();

        public abstract Task ThenICanVerifyICanUpdateAsync();

        public abstract Task WhenICallCreateAsync();

        public abstract Task WhenICallDeleteAsync();

        public abstract Task WhenICallReadAllAsync();

        public abstract Task WhenICallReadAsync();

        public abstract Task WhenICallUpdateAsyncAction();

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.environment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
