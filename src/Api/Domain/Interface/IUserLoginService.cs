using Microsoft.AspNet.Identity;
using OwnApt.Api.Domain.Model;
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

        Task UpdateAsync(UserLoginModel model);

        Task<UserLoginModel> VerifyUser(UserLoginModel suppliedModel);

        Task<UserLoginModel> RehashUserPassword(UserLoginModel suppliedModel);

        #endregion Public Methods
    }
}
