using OwnApt.Api.Contract.Model;
using OwnApt.Api.Domain.Interface;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Domain.Service
{
    public class TermService : ITermService
    {
        #region Private Fields

        private readonly ITermRepository termRepository;

        #endregion Private Fields

        #region Public Constructors

        public TermService(ITermRepository termRepository)
        {
            this.termRepository = termRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<TermModel> CreateAsync(TermModel termModel)
        {
            return await this.termRepository.CreateAsync(termModel);
        }

        public async Task DeleteAsync(string termId)
        {
            await this.termRepository.DeleteAsync(termId);
        }

        public async Task<TermModel> ReadAsync(string termId)
        {
            return await this.termRepository.ReadAsync(termId);
        }

        public async Task UpdateAsync(TermModel termModel)
        {
            await this.termRepository.UpdateAsync(termModel);
        }

        #endregion Public Methods
    }
}
