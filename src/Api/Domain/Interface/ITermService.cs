using OwnApt.Api.Contract.Model;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Interface
{
    public interface ITermService
    {
        #region Public Methods

        Task<TermModel> CreateAsync(TermModel termId);
        Task UpdateAsync(TermModel termId);
        Task<TermModel> ReadAsync(string termId);
        Task DeleteAsync(string termId);

        #endregion Public Methods
    }
}
