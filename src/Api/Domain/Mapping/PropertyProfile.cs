using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Domain.Mapping
{
    public class PropertyProfile : Profile
    {
        #region Public Constructors

        public PropertyProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Public Constructors

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
