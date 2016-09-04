using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface IUserLoginService
    {
        #region Public Methods

        Task<bool> CreateAsync(UserLoginModel model);

        Task DeleteAsync(string id);

        Task<UserLoginModel> ReadAsync(string id);

        Task<UserLoginModel> ReadByEmailAsync(string id);

        Task<UserLoginModel> RehashUserPasswordAsync(UserLoginModel suppliedModel);

        Task UpdateAsync(UserLoginModel model);

        Task<UserLoginModel> VerifyUserAsync(UserLoginModel suppliedModel);

        #endregion Public Methods
    }
}
