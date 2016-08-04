using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class UserLoginService : IUserLoginService
    {
        #region Private Fields + Properties

        private IUserLoginRepository userLoginRepository;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public UserLoginService(IUserLoginRepository userLoginRepository)
        {
            this.userLoginRepository = userLoginRepository;
        }

        #endregion Public Constructors + Destructors

        #region Public Methods

        public async Task<UserLoginModel> CreateAsync(UserLoginModel model)
        {
            return await this.userLoginRepository.CreateAsync(model);
        }

        public async Task DeleteAsync(string id)
        {
            await this.userLoginRepository.DeleteAsync(id);
        }

        public async Task<UserLoginModel> ReadAsync(string id)
        {
            return await this.userLoginRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(UserLoginModel model)
        {
            await this.userLoginRepository.UpdateAsync(model);
        }

        #endregion Public Methods
    }
}
