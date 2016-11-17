using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Domain.Service
{
    public class RepositoryService<TModel, TPrimaryKey, TRepository> where TRepository : IRepository<TModel, TPrimaryKey>
    {
        protected readonly TRepository repository;

        public RepositoryService(TRepository repository)
        {
            this.repository = repository;
        }

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
    }
}
