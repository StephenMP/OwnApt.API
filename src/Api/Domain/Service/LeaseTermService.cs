using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public class LeaseTermService : RepositoryService<LeaseTermModel, int, ILeaseTermRepository>, ILeaseTermService
    {
        #region Public Constructors

        public LeaseTermService(ILeaseTermRepository leaseTermRepository) : base(leaseTermRepository)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId)
        {
            return await this.repository.ReadByPropertyIdAsync(propertyId);
        }

        #endregion Public Methods
    }
}
