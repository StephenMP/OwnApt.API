using OwnApt.Api.Domain.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IUserLoginService
    {
        #region Public Methods

        Task<UserLoginModel> CreateAsync(UserLoginModel model);

        Task DeleteAsync(string id);

        Task<UserLoginModel> ReadAsync(string id);

        Task UpdateAsync(UserLoginModel model);

        #endregion Public Methods
    }
}
