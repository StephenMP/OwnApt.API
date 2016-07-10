using OwnApt.Api.Domain.Dto;
using OwnApt.Api.Domain.Enum;
using System;
using Xunit;

namespace Api.Tests.Component.Equatable
{
    public class EquatableTests
    {
        #region Public Methods

        [Fact]
        public void Address()
        {
            var orig = new AddressDto
            {
                Address1 = "Address1",
                Address2 = "Address2",
                City = "City",
                County = "County",
                State = State.AK,
                Zip = new ZipDto
                {
                    Base = "Base",
                    Extension = "Extension"
                }
            };

            var copy = new AddressDto
            {
                Address1 = "Address1",
                Address2 = "Address2",
                City = "City",
                County = "County",
                State = State.AK,
                Zip = new ZipDto
                {
                    Base = "Base",
                    Extension = "Extension"
                }
            };

            DoAsserts(orig, copy);
        }

        [Fact]
        public void Ammenity()
        {
            var orig = new AmmenityDto
            {
                Description = "Description",
                Type = AmmenityType.Fireplace
            };

            var copy = new AmmenityDto
            {
                Description = "Description",
                Type = AmmenityType.Fireplace
            };

            DoAsserts(orig, copy);
        }

        [Fact]
        public void Contact()
        {
            var orig = new ContactDto
            {
                Email = "Email",
                Phones = new PhoneDto[]
                {
                    new PhoneDto
                    {
                        AreaCode = 1234,
                        CountryCode = 2345,
                        LineNumber = 3456,
                        Prefix = 4567,
                        Type = PhoneType.Cell
                    }
                }
            };

            var copy = new ContactDto
            {
                Email = "Email",
                Phones = new PhoneDto[]
                {
                    new PhoneDto
                    {
                        AreaCode = 1234,
                        CountryCode = 2345,
                        LineNumber = 3456,
                        Prefix = 4567,
                        Type = PhoneType.Cell
                    }
                }
            };

            DoAsserts(orig, copy);
        }

        [Fact]
        public void Features()
        {
            var orig = new FeaturesDto
            {
                Ammentities = new AmmenityDto[]
                {
                    new AmmenityDto
                    {
                        Description = "Description",
                        Type = AmmenityType.Fireplace
                    }
                },
                Bathrooms = 1234,
                Levels = 1234,
                Parking = new ParkingDto
                {
                    Description = "Description",
                    Type = ParkingType.Garage
                },
                Rooms = 1234
            };

            var copy = new FeaturesDto
            {
                Ammentities = new AmmenityDto[]
                {
                    new AmmenityDto
                    {
                        Description = "Description",
                        Type = AmmenityType.Fireplace
                    }
                },
                Bathrooms = 1234,
                Levels = 1234,
                Parking = new ParkingDto
                {
                    Description = "Description",
                    Type = ParkingType.Garage
                },
                Rooms = 1234
            };

            DoAsserts(orig, copy);
        }

        [Fact]
        public void Name()
        {
            var orig = new NameDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName"
            };

            var copy = new NameDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName"
            };

            DoAsserts(orig, copy);
        }

        [Fact]
        public void ParkingDto()
        {
            var orig = new ParkingDto
            {
                Description = "Description",
                Type = ParkingType.Garage
            };

            var copy = new ParkingDto
            {
                Description = "Description",
                Type = ParkingType.Garage
            };

            DoAsserts(orig, copy);
        }

        #endregion Public Methods

        #region Private Methods

        private static void DoAsserts<T>(T orig, T copy)
        {
            Assert.NotNull(orig);
            Assert.NotNull(copy);
            Assert.Equal(orig, copy);
        }

        #endregion Private Methods
    }
}
