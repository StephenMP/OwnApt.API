using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface ILeaseTermService
    {
        #region Public Methods

        Task<LeaseTermModel> CreateAsync(LeaseTermModel termId);

        Task<LeaseTermModel> ReadAsync(string termId);

        Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId);

        #endregion Public Methods
    }
}
