using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class PersonService : IPersonService
    {
        #region Private Fields + Properties

        private IPersonRepository personRepository;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        #endregion Public Constructors + Destructors

        #region Public Methods

        public async Task<PersonModel> CreateAsync(PersonModel personModel)
        {
            return await this.personRepository.CreateAsync(personModel);
        }

        public async Task DeleteAsync(string id)
        {
            await this.personRepository.DeleteAsync(id);
        }

        public async Task<PersonModel> ReadAsync(string id)
        {
            return await this.personRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(PersonModel personModel)
        {
            await this.personRepository.UpdateAsync(personModel);
        }

        #endregion Public Methods
    }
}
