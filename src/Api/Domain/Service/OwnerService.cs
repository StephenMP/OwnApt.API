using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public class OwnerService : RepositoryService<OwnerModel, string, IOwnerRepository>, IOwnerService
    {
        #region Public Constructors

        public OwnerService(IOwnerRepository ownerRepository) : base(ownerRepository)
        {
        }

        #endregion Public Constructors
    }
}
