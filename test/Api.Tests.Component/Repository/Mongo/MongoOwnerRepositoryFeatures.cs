using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoOwnerRepositoryFeatures : IClassFixture<MongoClassFixture>, IDisposable
    {
        private readonly MongoOwnerRepositorySteps steps;

        public MongoOwnerRepositoryFeatures(MongoClassFixture testFixture)
        {
            this.steps = new MongoOwnerRepositorySteps(testFixture);
        }

        [Fact]
        public async Task CanPerformRepositoryOperations()
        {
            this.steps.GivenIHaveAMongoOwnerRepository();
            this.steps.GivenIHaveAnOwnerId();
            this.steps.GivenIHaveAnOwnerToCreate();
            await this.steps.WhenICallCreateAsync();
            this.steps.ThenICanVerifyICreateOwner();

            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyICreateOwner();

            this.steps.GivenIHaveAnOwnerToUpdate();
            await this.steps.WhenICallUpdateAsync();

            await this.steps.WhenICallDeleteAsync();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyIDeleteOwner();
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
