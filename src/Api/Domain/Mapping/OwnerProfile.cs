using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;

namespace OwnApt.Api.Domain.Mapping
{
    public class OwnerProfile : Profile
    {
        #region Public Constructors

        public OwnerProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Public Constructors

        #region Private Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<OwnerEntity, OwnerModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<OwnerModel, OwnerEntity>();
        }

        #endregion Private Methods
    }
}
