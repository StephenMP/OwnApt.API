using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;
using System;

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

    public class RegisteredTokenService : IRegisteredTokenService
    {
        #region Private Fields

        private readonly IRegisteredTokenRepository registeredTokenRepository;

        #endregion Private Fields

        #region Public Constructors

        public RegisteredTokenService(IRegisteredTokenRepository registeredTokenRepository)
        {
            this.registeredTokenRepository = registeredTokenRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<RegisteredTokenModel> CreateAsync(RegisteredTokenModel model)
        {
            return await this.registeredTokenRepository.CreateAsync(model);
        }

        public async Task<RegisteredTokenModel> ReadAsync(string id)
        {
            return await this.registeredTokenRepository.ReadAsync(id);
        }

        public async Task<RegisteredTokenModel> ReadByTokenAsync(string token)
        {
            return await this.registeredTokenRepository.ReadByTokenAsync(token);
        }

        #endregion Public Methods
    }
}
