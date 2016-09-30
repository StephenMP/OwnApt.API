using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Mongo.Core;
using OwnApt.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using OwnApt.Api.Repository.Entity.Mongo;

namespace Api.Tests.Component.Repository.Mongo.Core
{
    public class MongoPropertyRepositorySteps
    {
        private IMongoCoreContext mongoCoreContext;
        private readonly MongoEnvironmentClassFixture mongoFixture;
        private IPropertyRepository mongoPropertyRepository;
        private string currentPropertyId;
        private PropertyModel propertyModelToCreate;
        private PropertyModel resultModel;
        private IMapper mapper;
        private PropertyModel propertyModelToDelete;
        private PropertyModel propertyModelToRead;
        private string[] currentPropertyIds;
        private PropertyModel[] propertyModelsToRead;
        private PropertyModel[] resultModels;
        private PropertyModel propertyModelToUpdate;
        private PropertyEntity propertyEntityToUpdate;

        public MongoPropertyRepositorySteps(MongoEnvironmentClassFixture mongoFixture)
        {
            this.mongoFixture = mongoFixture;
            this.mapper = OwnAptStartup.BuildMapper();
        }

        internal void GivenIHaveAMongoCoreContext()
        {
            this.mongoCoreContext = new MongoCoreContext(this.mongoFixture.Environment.GetMongoClient());
        }

        internal void ThenICanVerifyICanCreatePropertyAsync()
        {
            Assert.Equal(this.propertyModelToCreate, this.resultModel);
        }

        internal async Task ThenICanVerifyICanDeletePropertyAsync()
        {
            this.resultModel = await this.mongoPropertyRepository.ReadAsync(this.currentPropertyId);
            Assert.Null(this.resultModel);
        }

        internal async Task WhenICallReadAsync()
        {
            this.resultModel = await this.mongoPropertyRepository.ReadAsync(this.currentPropertyId);
        }

        internal void ThenICanVerifyICanReadPropertyAsync()
        {
            Assert.Equal(this.propertyModelToRead, this.resultModel);
        }

        internal void ThenICanVerifyICanReadManyPropertyAsync()
        {
            foreach (var result in this.resultModels)
            {
                Assert.Contains(result, this.propertyModelsToRead);
            }
        }

        internal async Task ThenICanVerifyICanUpdatePropertyAsync()
        {
            this.resultModel = await this.mongoPropertyRepository.ReadAsync(this.currentPropertyId);
            var originalModel = this.mapper.Map<PropertyModel>(this.propertyEntityToUpdate);

            Assert.NotEqual(originalModel.ImageUri, this.resultModel.ImageUri);

            this.resultModel.ImageUri = originalModel.ImageUri;

            Assert.Equal(originalModel, this.resultModel);
        }

        internal async Task WhenICallUpdateAsync()
        {
            this.propertyModelToUpdate.ImageUri = new Uri("http://this.is.new");
            await this.mongoPropertyRepository.UpdateAsync(this.propertyModelToUpdate);
        }

        internal async Task GivenIHaveAnPropertyToUpdate()
        {
            this.currentPropertyId = TestRandom.String;
            this.propertyModelToUpdate = PropertyRandom.PropertyModel(this.currentPropertyId);
            this.propertyEntityToUpdate = this.mapper.Map<PropertyEntity>(this.propertyModelToUpdate);
            await this.ImportData(this.propertyEntityToUpdate);
        }

        internal async Task WhenICallReadManyAsync()
        {
            this.resultModels = await this.mongoPropertyRepository.ReadManyAsync(this.currentPropertyIds);
        }

        internal async Task GivenIHaveManyPropertiesToRead()
        {
            this.currentPropertyIds = new string[]
            {
                TestRandom.String,
                TestRandom.String,
                TestRandom.String
            };

            this.propertyModelsToRead = new PropertyModel[]
            {
                PropertyRandom.PropertyModel(this.currentPropertyIds[0]),
                PropertyRandom.PropertyModel(this.currentPropertyIds[1]),
                PropertyRandom.PropertyModel(this.currentPropertyIds[2])
            };

            var propertyEntitiesToRead = this.mapper.Map<PropertyEntity[]>(this.propertyModelsToRead);
            await this.ImportData(propertyEntitiesToRead);
        }

        internal async Task GivenIHaveAPropertyToRead()
        {
            this.currentPropertyId = TestRandom.String;
            this.propertyModelToRead = PropertyRandom.PropertyModel(this.currentPropertyId);
            var propertyEntityToRead = this.mapper.Map<PropertyEntity>(this.propertyModelToRead);
            await this.ImportData(propertyEntityToRead);
        }

        internal async Task WhenICallDeleteAsync()
        {
            await this.mongoPropertyRepository.DeleteAsync(this.currentPropertyId);
        }

        internal async Task WhenICallCreateAsync()
        {
            this.resultModel = await this.mongoPropertyRepository.CreateAsync(this.propertyModelToCreate);
        }

        internal async Task GivenIHaveAnPropertyToDelete()
        {
            this.currentPropertyId = TestRandom.String;
            this.propertyModelToDelete = PropertyRandom.PropertyModel(this.currentPropertyId);
            var propertyEntityToDelete = this.mapper.Map<PropertyEntity>(this.propertyModelToDelete);
            await this.ImportData(propertyEntityToDelete);
        }

        private async Task ImportData(params PropertyEntity[] propertyEntities)
        {
            await this.mongoFixture.Environment.ImportMongoDataAsync("Core", "Property", propertyEntities);
        }

        internal void GivenIHaveAnPropertyToCreate()
        {
            this.currentPropertyId = TestRandom.String;
            this.propertyModelToCreate = PropertyRandom.PropertyModel(this.currentPropertyId);
        }

        internal void GivenIHaveAMongoPropertyRepository()
        {
            this.mongoPropertyRepository = new MongoPropertyRepository(this.mongoCoreContext, OwnAptStartup.BuildMapper());
        }
    }

    internal static class PropertyRandom
    {
        #region Public Methods

        public static PropertyModel PropertyModel(string propertyId) => new PropertyModel
        {
            PropertyDescription = TestRandom.String,
            ImageUri = new Uri($"http://test.random.uri/{TestRandom.String}"),
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
