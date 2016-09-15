using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Mapping
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            ConfigureAddress();
            ConfigureAmenity();
            ConfigureBirthdate();
            ConfigureContact();
            ConfigureFeatures();
            ConfigureName();
            ConfigureParking();
            ConfigurePhone();
            ConfigureZip();
        }

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

        private void ConfigureBirthdate()
        {
            CreateMap<BirthdateModel, BirthdateEntity>();
            CreateMap<BirthdateEntity, BirthdateModel>();
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

        private void ConfigureZip()
        {
            CreateMap<ZipModel, ZipEntity>();
            CreateMap<ZipEntity, ZipModel>();
        }
    }
}
