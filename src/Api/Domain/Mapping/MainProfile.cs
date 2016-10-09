using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Common.Enums;
using System;

namespace OwnApt.Api.Domain.Mapping
{
    public class MainProfile : Profile
    {
        #region Public Constructors

        public MainProfile()
        {
            ConfigureRegisteredToken();
            ConfigureLeaseTerm();
            ConfigureLeasePeriod();
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

        private void ConfigureLeasePeriod()
        {
            CreateMap<LeasePeriodModel, LeasePeriodEntity>()
                .ForMember(d => d.LeasePeriodStatusId, opt => opt.MapFrom(src => (int)src.LeasePeriodStatus));

            CreateMap<LeasePeriodEntity, LeasePeriodModel>()
                .ForMember(d => d.LeasePeriodStatus, opt => opt.MapFrom(src => (LeasePeriodStatus)src.LeasePeriodStatusId));
        }

        private void ConfigureLeaseTerm()
        {
            CreateMap<LeaseTermModel, LeaseTermEntity>();
            CreateMap<LeaseTermEntity, LeaseTermModel>();
        }

        private void ConfigureName()
        {
            CreateMap<NameModel, NameEntity>();
            CreateMap<NameEntity, NameModel>();
        }

        private void ConfigureOwner()
        {
            CreateMap<OwnerModel, OwnerEntity>();
            CreateMap<OwnerEntity, OwnerModel>();
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

        private void ConfigureProperty()
        {
            CreateMap<PropertyModel, PropertyEntity>()
                .ForMember(d => d.ImageUriString, opt => opt.MapFrom(src => src.ImageUri.AbsoluteUri));

            CreateMap<PropertyEntity, PropertyModel>()
                .ForMember(d => d.ImageUri, opt => opt.MapFrom(src => new Uri(src.ImageUriString)));
        }

        private void ConfigureRegisteredToken()
        {
            CreateMap<RegisteredTokenModel, RegisteredTokenEntity>();
            CreateMap<RegisteredTokenEntity, RegisteredTokenModel>();
        }

        private void ConfigureZip()
        {
            CreateMap<ZipModel, ZipEntity>();
            CreateMap<ZipEntity, ZipModel>();
        }

        #endregion Private Methods
    }
}
