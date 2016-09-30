using OwnApt.TestEnvironment.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoEnvironmentClassFixture : IDisposable
    {
        public MongoEnvironmentClassFixture()
        {
            this.Environment = new OwnAptTestEnvironmentBuilder()
                                        .AddMongo()
                                        .BuildEnvironment();
        }

        public OwnAptTestEnvironment Environment { get; private set; }

        #region IDisposable Support
        private bool disposedValue;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
