using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo
{
    public class MongoOwnerRepositorySteps : IDisposable
    {
        private MongoClassFixture testFixture;

        public MongoOwnerRepositorySteps(MongoClassFixture testFixture)
        {
            this.testFixture = testFixture;
        }

        #region IDisposable Support
        private bool disposedValue;

        internal void GivenIHaveAnOwnerId()
        {
            this.ownerId = TestRandom.String;
        }

        private MongoOwnerRepository ownerRepository;
        private OwnerModel ownerModel;
        private OwnerModel resultModel;
        private string ownerId;
        private OwnerModel ownerModelToUpdate;

        internal async Task WhenICallCreateAsync()
        {
            this.resultModel = await this.ownerRepository.CreateAsync(this.ownerModel);
        }

        internal async Task WhenICallReadAsync()
        {
            this.resultModel = await this.ownerRepository.ReadAsync(this.ownerId);
        }

        internal void GivenIHaveAnOwnerToUpdate()
        {
            this.ownerModelToUpdate = OwnerRandom.OwnerModel(this.ownerId);
        }

        internal void ThenICanVerifyIUpdateOwner()
        {
            Assert.NotEqual(this.resultModel.Birthdate.Date, this.ownerModel.Birthdate.Date);
            Assert.NotEqual(this.resultModel.Contact, this.ownerModel.Contact);
            Assert.NotEqual(this.resultModel.EmergencyContact, this.ownerModel.EmergencyContact);
            Assert.NotEqual(this.resultModel.Gender, this.ownerModel.Gender);
            Assert.NotEqual(this.resultModel.Id, this.ownerModel.Id);
            Assert.NotEqual(this.resultModel.Name, this.ownerModel.Name);

            Assert.Equal(this.resultModel.Birthdate.Date, this.ownerModel.Birthdate.Date);
            Assert.Equal(this.resultModel.Contact, this.ownerModel.Contact);
            Assert.Equal(this.resultModel.EmergencyContact, this.ownerModel.EmergencyContact);
            Assert.Equal(this.resultModel.Gender, this.ownerModel.Gender);
            Assert.Equal(this.resultModel.Id, this.ownerModel.Id);
            Assert.Equal(this.resultModel.Name, this.ownerModel.Name);
        }

        internal async Task WhenICallUpdateAsync()
        {
            await this.ownerRepository.UpdateAsync(this.ownerModelToUpdate);
        }

        internal async Task WhenICallDeleteAsync()
        {
            await this.ownerRepository.DeleteAsync(this.ownerId);
        }

        internal void ThenICanVerifyIDeleteOwner()
        {
            Assert.Null(this.resultModel);
        }

        internal void ThenICanVerifyICreateOwner()
        {
            Assert.Equal(this.resultModel.Birthdate.Date, this.ownerModel.Birthdate.Date);
            Assert.Equal(this.resultModel.Contact, this.ownerModel.Contact);
            Assert.Equal(this.resultModel.EmergencyContact, this.ownerModel.EmergencyContact);
            Assert.Equal(this.resultModel.Gender, this.ownerModel.Gender);
            Assert.Equal(this.resultModel.Id, this.ownerModel.Id);
            Assert.Equal(this.resultModel.Name, this.ownerModel.Name);
        }

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

        internal void GivenIHaveAnOwnerToCreate()
        {
            this.ownerModel = OwnerRandom.OwnerModel(this.ownerId);
        }

        internal void GivenIHaveAMongoOwnerRepository()
        {
            this.ownerRepository = new MongoOwnerRepository(this.testFixture.TestEnvironment.MongoClient(), OwnAptStartup.BuildMapper());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    internal static class OwnerRandom {
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
    }
}
