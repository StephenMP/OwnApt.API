using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public interface IRegisteredTokenService
    {
        #region Public Methods

        Task<RegisteredTokenModel> CreateAsync(RegisteredTokenModel model);

        Task<RegisteredTokenModel> ReadAsync(string id);

        Task<RegisteredTokenModel> ReadByTokenAsync(string token);

        #endregion Public Methods
    }

    public class RegisteredTokenService : RepositoryService<RegisteredTokenModel, string, IRegisteredTokenRepository>, IRegisteredTokenService
    {
        #region Public Constructors

        public RegisteredTokenService(IRegisteredTokenRepository registeredTokenRepository) : base(registeredTokenRepository)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<RegisteredTokenModel> ReadByTokenAsync(string token)
        {
            return await this.repository.ReadByTokenAsync(token);
        }

        #endregion Public Methods
    }
}
