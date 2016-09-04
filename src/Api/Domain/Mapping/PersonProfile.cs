using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Domain.Mapping
{
    public class PersonProfile : Profile
    {
        #region Public Constructors

        public PersonProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Public Constructors

        #region Private Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<PersonEntity, PersonModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<PersonModel, PersonEntity>();
        }

        #endregion Private Methods
    }
}
