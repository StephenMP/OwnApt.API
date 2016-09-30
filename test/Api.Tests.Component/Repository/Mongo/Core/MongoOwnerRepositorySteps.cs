using AutoMapper;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo.Core;
using OwnApt.Common.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Core
{
    public class MongoOwnerRepositorySteps
    {
        #region Private Fields

        private readonly IMapper mapper;
        private readonly MongoEnvironmentClassFixture mongoFixture;
        private string currentOwnerId;
        private IMongoCoreContext mongoCoreContext;
        private OwnerEntity ownerEntityToUpdate;
        private OwnerModel ownerModelToCreate;
        private OwnerModel ownerModelToRead;
        private OwnerModel ownerModelToUpdate;
        private IOwnerRepository ownerRepository;
        private OwnerModel resultModel;

        #endregion Private Fields

        #region Public Constructors

        public MongoOwnerRepositorySteps(MongoEnvironmentClassFixture mongoFixture)
        {
            this.mongoFixture = mongoFixture;
            this.mapper = OwnAptStartup.BuildMapper();
        }

        #endregion Public Constructors

        #region Internal Methods

        internal void GivenIHaveAMongoCoreContext()
        {
            this.mongoCoreContext = new MongoCoreContext(this.mongoFixture.Environment.GetMongoClient());
        }

        internal void GivenIHaveAMongoOwnerRepository()
        {
            this.ownerRepository = new MongoOwnerRepository(this.mongoCoreContext, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAnOwnerToCreate()
        {
            this.currentOwnerId = TestRandom.String;
            this.ownerModelToCreate = OwnerRandom.OwnerModel(this.currentOwnerId);
        }

        internal async Task GivenIHaveAnOwnerToDelete()
        {
            this.currentOwnerId = TestRandom.String;
            var ownerModelToDelete = OwnerRandom.OwnerModel(this.currentOwnerId);
            var ownerEntityToDelete = this.mapper.Map<OwnerEntity>(ownerModelToDelete);
            await this.ImportData(ownerEntityToDelete);
        }

        internal async Task GivenIHaveAnOwnerToRead()
        {
            this.currentOwnerId = TestRandom.String;
            this.ownerModelToRead = OwnerRandom.OwnerModel(this.currentOwnerId);
            var ownerEntityToRead = this.mapper.Map<OwnerEntity>(this.ownerModelToRead);
            await this.ImportData(ownerEntityToRead);
        }

        internal async Task GivenIHaveAnOwnerToUpdate()
        {
            this.currentOwnerId = TestRandom.String;
            this.ownerModelToUpdate = OwnerRandom.OwnerModel(this.currentOwnerId);
            this.ownerEntityToUpdate = this.mapper.Map<OwnerEntity>(this.ownerModelToUpdate);
            await this.ImportData(this.ownerEntityToUpdate);
        }

        internal void ThenICanVerifyICanCreateOwnerAsync()
        {
            AssertEqual(this.ownerModelToCreate, this.resultModel);
        }

        internal async Task ThenICanVerifyICanDeleteOwnerAsync()
        {
            this.resultModel = await this.ownerRepository.ReadAsync(this.currentOwnerId);
            Assert.Null(this.resultModel);
        }

        internal void ThenICanVerifyICanReadOwnerAsync()
        {
            AssertEqual(this.ownerModelToRead, this.resultModel);
        }

        internal async Task ThenICanVerifyICanUpdateOwnerAsync()
        {
            this.resultModel = await this.ownerRepository.ReadAsync(this.currentOwnerId);
            var originalOwnerModel = this.mapper.Map<OwnerModel>(this.ownerEntityToUpdate);

            Assert.NotEqual(this.ownerModelToUpdate.Birthdate.Date, originalOwnerModel.Birthdate.Date);
            Assert.Equal(this.ownerModelToUpdate.Contact, originalOwnerModel.Contact);
            Assert.Equal(this.ownerModelToUpdate.EmergencyContact, originalOwnerModel.EmergencyContact);
            Assert.Equal(this.ownerModelToUpdate.Gender, originalOwnerModel.Gender);
            Assert.Equal(this.ownerModelToUpdate.Id, originalOwnerModel.Id);
            Assert.Equal(this.ownerModelToUpdate.Name, originalOwnerModel.Name);
            Assert.Equal(this.ownerModelToUpdate.PropertyIds, originalOwnerModel.PropertyIds);
        }

        internal void ThenICanVerifyICreateOrReadOwner()
        {
            this.resultModel.Birthdate = this.resultModel.Birthdate.Date;
            this.ownerModelToCreate.Birthdate = this.ownerModelToCreate.Birthdate.Date;
            Assert.Equal(this.resultModel, this.ownerModelToCreate);
        }

        internal void ThenICanVerifyIDeleteOwner()
        {
            Assert.Null(this.resultModel);
        }

        internal void ThenICanVerifyIUpdateOwner()
        {
            this.resultModel.Birthdate = this.resultModel.Birthdate.Date;
            this.ownerModelToCreate.Birthdate = this.ownerModelToCreate.Birthdate.Date;
            this.ownerModelToUpdate.Birthdate = this.ownerModelToUpdate.Birthdate.Date;

            Assert.NotEqual(this.resultModel, this.ownerModelToCreate);
            Assert.Equal(this.resultModel, this.ownerModelToUpdate);
        }

        internal async Task WhenICallCreateAsync()
        {
            this.resultModel = await this.ownerRepository.CreateAsync(this.ownerModelToCreate);
        }

        internal async Task WhenICallDeleteAsync()
        {
            await this.ownerRepository.DeleteAsync(this.currentOwnerId);
        }

        internal async Task WhenICallReadAsync()
        {
            this.resultModel = await this.ownerRepository.ReadAsync(this.currentOwnerId);
        }

        internal async Task WhenICallUpdateAsync()
        {
            this.ownerModelToUpdate.Birthdate = DateTime.UtcNow.AddDays(10);
            await this.ownerRepository.UpdateAsync(this.ownerModelToUpdate);
        }

        #endregion Internal Methods

        #region Private Methods

        private void AssertEqual(OwnerModel expected, OwnerModel actual)
        {
            Assert.Equal(expected.Birthdate.Date, actual.Birthdate.Date);
            Assert.Equal(expected.Contact, actual.Contact);
            Assert.Equal(expected.EmergencyContact, actual.EmergencyContact);
            Assert.Equal(expected.Gender, actual.Gender);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.PropertyIds, actual.PropertyIds);
        }

        private async Task ImportData(params OwnerEntity[] ownerEntities)
        {
            await this.mongoFixture.Environment.ImportMongoDataAsync("Core", "Owner", ownerEntities);
        }

        #endregion Private Methods
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
