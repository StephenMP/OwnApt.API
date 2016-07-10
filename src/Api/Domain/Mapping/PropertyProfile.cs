using AutoMapper;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Domain.Mapping
{
    public class PropertyProfile : Profile
    {
        #region Public Constructors + Destructors

        public PropertyProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Public Constructors + Destructors

        #region Private Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<PropertyEntity, PropertyModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<PropertyModel, PropertyEntity>();
        }

        #endregion Private Methods
    }
}
