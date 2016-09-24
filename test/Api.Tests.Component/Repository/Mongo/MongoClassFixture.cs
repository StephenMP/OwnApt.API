using OwnApt.TestEnvironment.Environment;
using System;
using MongoDB.Driver;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoClassFixture : IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private readonly OwnAptTestEnvironment testEnvironment;

        #endregion Private Fields

        public MongoClassFixture()
        {
            this.testEnvironment = new OwnAptTestEnvironmentBuilder()
                                        .AddMongo()
                                        .BuildEnvironment();

            this.MongoClient = this.testEnvironment.GetMongoClient();
        }

        public IMongoClient MongoClient { get; private set; }

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
                    this.testEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
