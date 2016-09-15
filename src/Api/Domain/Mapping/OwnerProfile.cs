using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;

namespace OwnApt.Api.Domain.Mapping
{
    public class OwnerProfile : Profile
    {
        #region Constructors

        public OwnerProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Constructors

        #region Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<OwnerEntity, OwnerModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<OwnerModel, OwnerEntity>();
        }

        #endregion Methods
    }
}
