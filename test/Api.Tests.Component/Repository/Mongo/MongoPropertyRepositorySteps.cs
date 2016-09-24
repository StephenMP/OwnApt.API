using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoPropertyRepositorySteps : IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private string propertyId;
        private PropertyModel propertyModel;
        private PropertyModel propertyModelToUpdate;
        private MongoPropertyRepository propertyRepository;
        private PropertyModel resultModel;
        private readonly MongoClassFixture testFixture;

        #endregion Private Fields

        #region Public Constructors

        public MongoPropertyRepositorySteps(MongoClassFixture testFixture)
        {
            this.testFixture = testFixture;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIHaveAMongoPropertyRepository()
        {
            this.propertyRepository = new MongoPropertyRepository(this.testFixture.MongoClient, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAPropertyId()
        {
            this.propertyId = TestRandom.String;
        }

        internal void GivenIHaveAPropertyToCreate()
        {
            this.propertyModel = PropertyRandom.PropertyModel(this.propertyId);
        }

        internal void GivenIHaveAPropertyToUpdate()
        {
            this.propertyModelToUpdate = PropertyRandom.PropertyModel(this.propertyId);
        }

        internal void ThenICanVerifyICreateOrReadProperty()
        {
            Assert.Equal(this.resultModel, this.propertyModel);
        }

        internal void ThenICanVerifyIDeleteProperty()
        {
            Assert.Null(this.resultModel);
        }

        internal void ThenICanVerifyIUpdateProperty()
        {
            Assert.NotEqual(this.resultModel, this.propertyModel);
            Assert.Equal(this.resultModel, this.propertyModelToUpdate);
        }

        internal async Task WhenICallCreateAsync()
        {
            this.resultModel = await this.propertyRepository.CreateAsync(this.propertyModel);
        }

        internal async Task WhenICallDeleteAsync()
        {
            await this.propertyRepository.DeleteAsync(this.propertyId);
        }

        internal async Task WhenICallReadAsync()
        {
            this.resultModel = await this.propertyRepository.ReadAsync(this.propertyId);
        }

        internal async Task WhenICallUpdateAsync()
        {
            await this.propertyRepository.UpdateAsync(this.propertyModelToUpdate);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testFixture?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }

    internal static class PropertyRandom
    {
        #region Public Methods

        public static PropertyModel PropertyModel(string propertyId) => new PropertyModel
        {
            Address = new AddressModel
            {
                Address1 = TestRandom.String,
                Address2 = TestRandom.String,
                City = TestRandom.String,
                County = TestRandom.String,
                State = State.AK,
                Zip = new ZipModel
                {
                    Base = TestRandom.String,
                    Extension = TestRandom.String
                }
            },
            Features = new FeaturesModel
            {
                Amenities = new List<AmenityModel>
                {
                    new AmenityModel
                    {
                        Description = TestRandom.String,
                        Type = TestRandom.String
                    }
                },
                Bathrooms = TestRandom.Integer,
                Levels = TestRandom.Integer,
                Parking = new List<ParkingModel>
                {
                    new ParkingModel
                    {
                        Description = TestRandom.String,
                        Type = TestRandom.String
                    }
                },
                Rooms = TestRandom.Integer,
                SqFootage = TestRandom.Integer
            },
            Id = propertyId,
            PropertyType = PropertyType.Apartment
        };

        #endregion Public Methods
    }
}
