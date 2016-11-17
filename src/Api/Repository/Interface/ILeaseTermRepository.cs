using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;

namespace OwnApt.Api.Repository.Interface
{
    public interface ILeaseTermRepository : IRepository<LeaseTermModel, int>
    {
        #region Public Methods

        Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId);

        #endregion Public Methods
    }
}
