using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public class OwnerService : RepositoryService<OwnerModel, string, IOwnerRepository>, IOwnerService
    {
        public OwnerService(IOwnerRepository ownerRepository) : base(ownerRepository)
        {
        }
    }
}
