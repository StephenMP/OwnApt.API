using OwnApt.TestEnvironment.Environment;
using System;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoEnvironmentClassFixture : IDisposable
    {
        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongoEnvironmentClassFixture()
        {
            this.Environment = new OwnAptTestEnvironmentBuilder()
                                        .AddMongo()
                                        .BuildEnvironment();
        }

        #endregion Public Constructors

        #region Public Properties

        public OwnAptTestEnvironment Environment { get; private set; }

        #endregion Public Properties

        #region Public Methods

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
                    this.Environment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
