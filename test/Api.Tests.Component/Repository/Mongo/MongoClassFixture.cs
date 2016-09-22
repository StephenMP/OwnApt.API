using OwnApt.TestEnvironment.Environment;
using System;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoClassFixture : IDisposable
    {
        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        public MongoClassFixture()
        {
            this.TestEnvironment = new TestingEnvironment();
            this.TestEnvironment.AddMongo();
        }

        public TestingEnvironment TestEnvironment { get; private set; }

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
                    this.TestEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
