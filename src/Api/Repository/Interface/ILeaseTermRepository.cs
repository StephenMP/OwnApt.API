using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface ILeaseTermRepository : IRepository<LeaseTermModel>
    {
        #region Public Methods

        Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId);

        #endregion Public Methods
    }
}
