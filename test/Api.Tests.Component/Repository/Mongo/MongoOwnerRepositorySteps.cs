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
    public class MongoOwnerRepositorySteps : IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private string ownerId;
        private OwnerModel ownerModel;
        private OwnerModel ownerModelToUpdate;
        private MongoOwnerRepository ownerRepository;
        private OwnerModel resultModel;
        private readonly MongoClassFixture testFixture;

        #endregion Private Fields

        #region Public Constructors

        public MongoOwnerRepositorySteps(MongoClassFixture testFixture)
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

        internal void GivenIHaveAMongoOwnerRepository()
        {
            this.ownerRepository = new MongoOwnerRepository(this.testFixture.MongoClient, OwnAptStartup.BuildMapper());
        }

        internal void GivenIHaveAnOwnerId()
        {
            this.ownerId = TestRandom.String;
        }

        internal void GivenIHaveAnOwnerToCreate()
        {
            this.ownerModel = OwnerRandom.OwnerModel(this.ownerId);
        }

        internal void GivenIHaveAnOwnerToUpdate()
        {
            this.ownerModelToUpdate = OwnerRandom.OwnerModel(this.ownerId);
        }

        internal void ThenICanVerifyICreateOrReadOwner()
        {
            this.resultModel.Birthdate = this.resultModel.Birthdate.Date;
            this.ownerModel.Birthdate = this.ownerModel.Birthdate.Date;
            Assert.Equal(this.resultModel, this.ownerModel);
        }

        internal void ThenICanVerifyIDeleteOwner()
        {
            Assert.Null(this.resultModel);
        }

        internal void ThenICanVerifyIUpdateOwner()
        {
            this.resultModel.Birthdate = this.resultModel.Birthdate.Date;
            this.ownerModel.Birthdate = this.ownerModel.Birthdate.Date;
            this.ownerModelToUpdate.Birthdate = this.ownerModelToUpdate.Birthdate.Date;

            Assert.NotEqual(this.resultModel, this.ownerModel);
            Assert.Equal(this.resultModel, this.ownerModelToUpdate);
        }

        internal async Task WhenICallCreateAsync()
        {
            this.resultModel = await this.ownerRepository.CreateAsync(this.ownerModel);
        }

        internal async Task WhenICallDeleteAsync()
        {
            await this.ownerRepository.DeleteAsync(this.ownerId);
        }

        internal async Task WhenICallReadAsync()
        {
            this.resultModel = await this.ownerRepository.ReadAsync(this.ownerId);
        }

        internal async Task WhenICallUpdateAsync()
        {
            await this.ownerRepository.UpdateAsync(this.ownerModelToUpdate);
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
