using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Domain.Mapping
{
    public class PersonProfile : Profile
    {
        #region Constructors

        public PersonProfile()
        {
            ConfigureEntityToModel();
            ConfigureModelToEntity();
        }

        #endregion Constructors

        #region Methods

        private void ConfigureEntityToModel()
        {
            CreateMap<PersonEntity, PersonModel>();
        }

        private void ConfigureModelToEntity()
        {
            CreateMap<PersonModel, PersonEntity>();
        }

        #endregion Methods
    }
}
