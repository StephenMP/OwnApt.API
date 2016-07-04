using AutoMapper;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Mapping
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        private void ConfigureEntityToModel()
        {
            CreateMap<PropertyEntity, PropertyModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<PropertyModel, PropertyEntity>();
        }
    }
}
