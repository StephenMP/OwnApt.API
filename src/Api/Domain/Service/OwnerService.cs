using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class OwnerService : IOwnerService
    {
        #region Fields

        private readonly IOwnerRepository ownerRepository;

        #endregion Fields

        #region Constructors

        public OwnerService(IOwnerRepository ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}
