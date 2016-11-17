using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRepository<TModel, TPrimaryKey>
    {
        #region Public Methods

        Task<TModel> CreateAsync(TModel model);

        Task DeleteAsync(TPrimaryKey model);

        Task<IEnumerable<TModel>> ReadAllAsync();

        Task<TModel> ReadAsync(TPrimaryKey id);

        Task UpdateAsync(TModel model);

        #endregion Public Methods
    }
}
