using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IOwnerService
    {
        #region Public Methods

        Task<OwnerModel> CreateAsync(OwnerModel model);

        Task DeleteAsync(string id);

        Task<OwnerModel> ReadAsync(string id);

        Task UpdateAsync(OwnerModel model);

        #endregion Public Methods
    }
}
