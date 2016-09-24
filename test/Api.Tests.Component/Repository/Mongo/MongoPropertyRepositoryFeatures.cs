using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoPropertyRepositoryFeatures : IClassFixture<MongoClassFixture>, IDisposable
    {
        #region Private Fields

        private readonly MongoPropertyRepositorySteps steps;

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MongoPropertyRepositoryFeatures(MongoClassFixture testFixture)
        {
            this.steps = new MongoPropertyRepositorySteps(testFixture);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public async Task CanPerformRepositoryOperations()
        {
            this.steps.GivenIHaveAMongoPropertyRepository();
            this.steps.GivenIHaveAPropertyId();
            this.steps.GivenIHaveAPropertyToCreate();
            await this.steps.WhenICallCreateAsync();
            this.steps.ThenICanVerifyICreateOrReadProperty();

            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyICreateOrReadProperty();

            this.steps.GivenIHaveAPropertyToUpdate();
            await this.steps.WhenICallUpdateAsync();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyIUpdateProperty();

            await this.steps.WhenICallDeleteAsync();
            await this.steps.WhenICallReadAsync();
            this.steps.ThenICanVerifyIDeleteProperty();
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
