using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRegisteredTokenRepository : IRepository<RegisteredTokenModel, RegisteredTokenModel, string>
    {
        #region Public Methods

        Task<RegisteredTokenModel> ReadByTokenAsync(string token);

        #endregion Public Methods
    }
}
