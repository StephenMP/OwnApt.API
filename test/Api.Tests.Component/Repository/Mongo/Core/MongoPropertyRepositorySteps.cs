using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo.Core;
using OwnApt.Common.Enums;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Core
{
    public class MongoPropertyRepositorySteps : MongoRepositorySteps<PropertyModel, PropertyEntity>
    {
        public MongoPropertyRepositorySteps() : base("Core", "Property")
        {
        }

        public override void GivenIHaveARepository()
        {
            this.repository = new MongoPropertyRepository(new MongoCoreContext(this.environment.GetMongoClient()), OwnAptStartup.BuildMapper());
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
