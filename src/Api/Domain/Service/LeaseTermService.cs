using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class LeaseTermService : ILeaseTermService
    {
        #region Private Fields

        private readonly ILeaseTermRepository termRepository;

        #endregion Private Fields

        #region Public Constructors

        public LeaseTermService(ILeaseTermRepository leaseTermRepository)
        {
            this.termRepository = leaseTermRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<LeaseTermModel> CreateAsync(LeaseTermModel termModel)
        {
            return await this.termRepository.CreateAsync(termModel);
        }

        public async Task<LeaseTermModel> ReadAsync(int leaseTermId)
        {
            return await this.termRepository.ReadAsync(leaseTermId);
        }

        public async Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId)
        {
            return await this.termRepository.ReadByPropertyIdAsync(propertyId);
        }

        #endregion Public Methods
    }
}
