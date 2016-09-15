using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;

namespace OwnApt.Api.Domain.Mapping
{
    public class PropertyProfile : Profile
    {
        #region Constructors

        public PropertyProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Constructors

        #region Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<PropertyEntity, PropertyModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<PropertyModel, PropertyEntity>();
        }

        #endregion Methods
    }
}
