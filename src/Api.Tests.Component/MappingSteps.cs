using OwnApt.Api.AppStart;
using OwnApt.Api.Domain.Enum;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using System;
using AutoMapper;
using Xunit;
using OwnApt.Api.Domain.Dto;

namespace Api.Tests.Component.Mapping
{
    internal class MappingSteps
    {
        #region Private Fields

        private IMapper mapper;

        private PersonEntity personEntity;
        private PersonModel personModel;
        private PropertyEntity propertyEntity;
        private PropertyModel propertyModel;
        private Random random = new Random();

        #endregion Private Fields

        #region Internal Methods

        internal void GivenIHaveAMapper()
        {
            this.mapper = StartupExtensions.BuildMapper();
        }

        internal void GivenIHaveAPersonEntity()
        {
            this.personEntity = new PersonEntity
            {
                Age = 30,
                Contact = new ContactDto
                {
                    Email = "john@doe.com",
                    Phones = new PhoneDto[]
                    {
                        new PhoneDto
                        {
                            AreaCode = 208,
                            CountryCode = 1,
                            LineNumber = 4567,
                            Prefix = 123,
                            Type = PhoneType.Home
                        }
                    }
                },
                CreditScore = 700,
                Gender = Gender.Male,
                Name = new NameDto
                {
                    FirstName = "John",
                    MiddleName = "None",
                    LastName = "Doe"
                },
                PropertyIds = new string[]
                {
                    random.Next().ToString()
                },
                Type = PersonType.Owner
            };
        }

        internal void GivenIHaveAPersonModel()
        {
            this.personModel = new PersonModel
            {
                Age = 30,
                Contact = new ContactDto
                {
                    Email = "john@doe.com",
                    Phones = new PhoneDto[]
                    {
                        new PhoneDto
                        {
                            AreaCode = 208,
                            CountryCode = 1,
                            LineNumber = 4567,
                            Prefix = 123,
                            Type = PhoneType.Home
                        }
                    }
                },
                CreditScore = 700,
                Gender = Gender.Male,
                Name = new NameDto
                {
                    FirstName = "John",
                    MiddleName = "None",
                    LastName = "Doe"
                },
                PropertyIds = new string[]
                {
                    random.Next().ToString()
                },
                Type = PersonType.Owner
            };
        }

        internal void GivenIHaveAPropertyEntity()
        {
            this.propertyEntity = new PropertyEntity
            {
                Address = new AddressDto
                {
                    Address1 = "12345 Test Ave",
                    City = "Boise",
                    County = "Ada",
                    State = State.ID,
                    Zip = new ZipDto
                    {
                        Base = "83703"
                    }
                },
                Features = new FeaturesDto
                {
                    Ammentities = new AmmenityDto[]
                    {
                        new AmmenityDto
                        {
                            Description = "Gas Fireplace",
                            Type = AmmenityType.Fireplace
                        }
                    },
                    Bathrooms = 2,
                    Levels = 1,
                    Parking = new ParkingDto
                    {
                        Description = "Three Car Garage",
                        Type = ParkingType.Garage
                    },
                    Rooms = 3
                },
                Id = random.Next().ToString(),
                OwnerIds = new string[] { random.Next().ToString() },
                TenantIds = new string[] { random.Next().ToString() },
                PropertyType = PropertyType.SingleFamilyHome
            };
        }

        internal void GivenIHaveAPropertyModel()
        {
            this.propertyModel = new PropertyModel
            {
                Address = new AddressDto
                {
                    Address1 = "12345 Test Ave",
                    City = "Boise",
                    County = "Ada",
                    State = State.ID,
                    Zip = new ZipDto
                    {
                        Base = "83703"
                    }
                },
                Features = new FeaturesDto
                {
                    Ammentities = new AmmenityDto[]
                    {
                        new AmmenityDto
                        {
                            Description = "Gas Fireplace",
                            Type = AmmenityType.Fireplace
                        }
                    },
                    Bathrooms = 2,
                    Levels = 1,
                    Parking = new ParkingDto
                    {
                        Description = "Three Car Garage",
                        Type = ParkingType.Garage
                    },
                    Rooms = 3
                },
                Id = random.Next().ToString(),
                OwnerIds = new string[] { random.Next().ToString() },
                TenantIds = new string[] { random.Next().ToString() },
                PropertyType = PropertyType.SingleFamilyHome
            };
        }

        internal void ThenICanVerifyIMappedPersonSuccessfully()
        {
            Assert.Equal(this.personModel.Age, this.personEntity.Age);
            Assert.Equal(this.personModel.Contact, this.personEntity.Contact);

            for (int i = 0; i < this.personModel.Contact.Phones.Length; i++)
            {
                Assert.Equal(this.personModel.Contact.Phones[i], this.personEntity.Contact.Phones[i]);
            }

            Assert.Equal(this.personModel.CreditScore, this.personEntity.CreditScore);
            Assert.Equal(this.personModel.Gender, this.personEntity.Gender);
            Assert.Equal(this.personModel.Id, this.personEntity.Id);
            Assert.Equal(this.personModel.Name, this.personEntity.Name);
            Assert.Equal(this.personModel.PropertyIds.Length, this.personEntity.PropertyIds.Length);

            for (int i = 0; i < this.personModel.PropertyIds.Length; i++)
            {
                Assert.Equal(this.personModel.PropertyIds[i], this.personEntity.PropertyIds[i]);
            }

            Assert.Equal(this.personModel.Type, this.personEntity.Type);
        }

        internal void ThenICanVerifyIMappedPropertySuccessfully()
        {
            Assert.Equal(this.propertyModel.Address, this.propertyEntity.Address);
            Assert.Equal(this.propertyModel.Features.Ammentities.Length, this.propertyEntity.Features.Ammentities.Length);

            for (int i = 0; i < this.propertyModel.Features.Ammentities.Length; i++)
            {
                Assert.Equal(this.propertyModel.Features.Ammentities[i], this.propertyEntity.Features.Ammentities[i]);
            }

            Assert.Equal(this.propertyModel.Features, this.propertyEntity.Features);
            Assert.Equal(this.propertyModel.Id, this.propertyEntity.Id);
            Assert.Equal(this.propertyModel.OwnerIds.Length, this.propertyEntity.OwnerIds.Length);
            Assert.Equal(this.propertyModel.TenantIds.Length, this.propertyEntity.TenantIds.Length);

            for (int i = 0; i < this.propertyModel.OwnerIds.Length; i++)
            {
                Assert.Equal(this.propertyModel.OwnerIds[i], this.propertyEntity.OwnerIds[i]);
            }

            for (int i = 0; i < this.propertyModel.TenantIds.Length; i++)
            {
                Assert.Equal(this.propertyModel.TenantIds[i], this.propertyEntity.TenantIds[i]);
            }

            Assert.Equal(this.propertyModel.PropertyType, this.propertyEntity.PropertyType);
        }

        internal void WhenIMapPersonEntityToModel()
        {
            this.personModel = mapper.Map<PersonModel>(this.personEntity);
        }

        internal void WhenIMapPersonModelToEntity()
        {
            this.personEntity = this.mapper.Map<PersonEntity>(this.personModel);
        }

        internal void WhenIMapPropertyEntityToModel()
        {
            this.propertyModel = this.mapper.Map<PropertyModel>(this.propertyEntity);
        }

        internal void WhenIMapPropertyModelToEntity()
        {
            this.propertyEntity = this.mapper.Map<PropertyEntity>(this.propertyModel);
        }

        #endregion Internal Methods
    }
}
