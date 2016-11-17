using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRegisteredTokenRepository : IRepository<RegisteredTokenModel, string>
    {
        #region Public Methods

        Task<RegisteredTokenModel> ReadByTokenAsync(string token);

        #endregion Public Methods
    }
}
