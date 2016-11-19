using System.Collections.Generic;
using System.Threading.Tasks;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public class RepositoryService<TModel, TPrimaryKey, TRepository> where TRepository : IRepository<TModel, TPrimaryKey>
    {
        #region Protected Fields

        protected readonly TRepository repository;

        #endregion Protected Fields

        #region Public Constructors

        public RepositoryService(TRepository repository)
        {
            this.repository = repository;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            return await this.repository.CreateAsync(model);
        }

        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            await this.repository.DeleteAsync(id);
        }

        public virtual async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            return await this.repository.ReadAllAsync();
        }

        public virtual async Task<TModel> ReadAsync(TPrimaryKey id)
        {
            return await this.repository.ReadAsync(id);
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            await this.repository.UpdateAsync(model);
        }

        #endregion Public Methods
    }
}
