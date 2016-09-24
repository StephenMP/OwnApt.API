using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Interface
{
    public interface IRepository<TModelIn, TModelOut, TPrimaryKey>
    {
        #region Public Methods

        Task<TModelOut> CreateAsync(TModelIn model);

        Task DeleteAsync(TPrimaryKey id);

        Task<TModelOut> ReadAsync(TPrimaryKey id);

        Task UpdateAsync(TModelIn model);

        #endregion Public Methods
    }
}
