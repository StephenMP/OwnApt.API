using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Domain.Mapping
{
    public class UserLoginProfile : Profile
    {
        #region Constructors

        public UserLoginProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Constructors

        #region Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<UserLoginEntity, UserLoginModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<UserLoginModel, UserLoginEntity>();
        }

        #endregion Methods
    }
}
