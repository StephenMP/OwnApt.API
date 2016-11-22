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
    public class MongoOwnerRepositorySteps : MongoRepositorySteps<OwnerModel, OwnerEntity>
    {
        public MongoOwnerRepositorySteps() : base("Core", "Owner")
        {
        }

        public override void GivenIHaveARepository()
        {
            this.repository = new MongoOwnerRepository(new MongoCoreContext(this.environment.GetMongoClient()), OwnAptStartup.BuildMapper());
        }
    }

    internal static class OwnerRandom
    {
        #region Public Methods

        public static OwnerModel OwnerModel(string ownerId) => new OwnerModel
        {
            Birthdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
            Contact = new ContactModel
            {
                Email = TestRandom.String,
                HomeAddress = new AddressModel
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
                Phones = new List<PhoneModel>
                    {
                        new PhoneModel
                        {
                            AreaCode = TestRandom.Integer,
                            CountryCode = TestRandom.Integer,
                            LineNumber = TestRandom.Integer,
                            Prefix = TestRandom.Integer,
                            Type = PhoneType.Cell
                        }
                    }
            },
            EmergencyContact = new ContactModel
            {
                Email = TestRandom.String,
                HomeAddress = new AddressModel
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
                Phones = new List<PhoneModel>
                    {
                        new PhoneModel
                        {
                            AreaCode = TestRandom.Integer,
                            CountryCode = TestRandom.Integer,
                            LineNumber = TestRandom.Integer,
                            Prefix = TestRandom.Integer,
                            Type = PhoneType.Cell
                        }
                    }
            },
            Gender = Gender.Male,
            Id = ownerId,
            Name = new NameModel
            {
                FirstName = TestRandom.String,
                LastName = TestRandom.String,
                MiddleName = TestRandom.String
            },
            PropertyIds = new List<string>
                {
                    TestRandom.String
                }
        };

        #endregion Public Methods
    }
}
