using System;
using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Entity.Sql;

namespace OwnApt.Api.Domain.Mapping
{
    public class MainProfile : Profile
    {
        #region Public Constructors

        public MainProfile()
        {
            ConfigureTerm();
            ConfigureAddress();
            ConfigureAmenity();
            ConfigureContact();
            ConfigureFeatures();
            ConfigureName();
            ConfigureParking();
            ConfigurePhone();
            ConfigureZip();
            ConfigureProperty();
            ConfigureOwner();
        }

        private void ConfigureOwner()
        {
            CreateMap<OwnerModel, OwnerEntity>();
            CreateMap<OwnerEntity, OwnerModel>();
        }

        private void ConfigureProperty()
        {
            CreateMap<PropertyModel, PropertyEntity>();
            CreateMap<PropertyEntity, PropertyModel>();
        }

        #endregion Public Constructors

        #region Private Methods

        private void ConfigureAddress()
        {
            CreateMap<AddressModel, AddressEntity>();
            CreateMap<AddressEntity, AddressModel>();
        }

        private void ConfigureAmenity()
        {
            CreateMap<AmenityModel, AmenityEntity>();
            CreateMap<AmenityEntity, AmenityModel>();
        }

        private void ConfigureContact()
        {
            CreateMap<ContactModel, ContactEntity>();
            CreateMap<ContactEntity, ContactModel>();
        }

        private void ConfigureFeatures()
        {
            CreateMap<FeaturesModel, FeaturesEntity>();
            CreateMap<FeaturesEntity, FeaturesModel>();
        }

        private void ConfigureName()
        {
            CreateMap<NameModel, NameEntity>();
            CreateMap<NameEntity, NameModel>();
        }

        private void ConfigureParking()
        {
            CreateMap<ParkingModel, ParkingEntity>();
            CreateMap<ParkingEntity, ParkingModel>();
        }

        private void ConfigurePhone()
        {
            CreateMap<PhoneModel, PhoneEntity>();
            CreateMap<PhoneEntity, PhoneModel>();
        }

        private void ConfigureTerm()
        {
            CreateMap<LeaseTermModel, LeaseTermEntity>();
            CreateMap<LeaseTermEntity, LeaseTermModel>();
        }

        private void ConfigureZip()
        {
            CreateMap<ZipModel, ZipEntity>();
            CreateMap<ZipEntity, ZipModel>();
        }

        #endregion Private Methods
    }
}
