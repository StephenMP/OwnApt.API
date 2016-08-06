using AutoMapper;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Mapping
{
    public class UserLoginProfile : Profile
    {
        #region Public Constructors + Destructors

        public UserLoginProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Public Constructors + Destructors

        #region Private Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<UserLoginEntity, UserLoginModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<UserLoginModel, UserLoginEntity>();
        }

        #endregion Private Methods
    }
}
