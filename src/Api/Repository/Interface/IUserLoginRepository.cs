using OwnApt.Api.Domain.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IUserLoginRepository : IRepository<UserLoginModel>
    {
        #region Public Methods

        Task<UserLoginModel> ReadByEmailAsync(string email);

        #endregion Public Methods
    }
}
