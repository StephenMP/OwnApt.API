using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class OwnerService : IOwnerService
    {
        #region Private Fields

        private readonly IOwnerRepository ownerRepository;

        #endregion Private Fields

        #region Public Constructors

        public OwnerService(IOwnerRepository ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<OwnerModel> CreateAsync(OwnerModel model)
        {
            return await this.ownerRepository.CreateAsync(model);
        }

        public async Task DeleteAsync(string id)
        {
            await this.ownerRepository.DeleteAsync(id);
        }

        public async Task<OwnerModel> ReadAsync(string id)
        {
            return await this.ownerRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(OwnerModel model)
        {
            await this.ownerRepository.UpdateAsync(model);
        }

        #endregion Public Methods
    }
}
