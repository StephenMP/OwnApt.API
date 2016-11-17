using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Sql
{
    public abstract class SqlRepository<TModel, TEntity, TContext> : IRepository<TModel, int> where TEntity : SqlEntity where TContext : DbContext
    {
        #region Protected Fields

        protected readonly TContext context;
        protected readonly IMapper mapper;

        #endregion Protected Fields

        #region Protected Constructors

        protected SqlRepository(TContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #endregion Protected Constructors

        #region Public Methods

        public async Task<TModel> CreateAsync(TModel model)
        {
            var entity = this.mapper.Map<TEntity>(model);
            this.context.Set<TEntity>().Add(entity);
            await this.context.SaveChangesAsync();

            model = this.mapper.Map<TModel>(entity);
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await this.ReadAsync(id);
            var entity = this.mapper.Map<TEntity>(model);
            this.context.Set<TEntity>().Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            var entities = await this.context
                                     .Set<TEntity>()
                                     .AsNoTracking()
                                     .AsQueryable()
                                     .ToListAsync();

            var models = this.mapper.Map<List<TModel>>(entities);

            return models;
        }

        public async Task<TModel> ReadAsync(int id)
        {
            var entity = await this.context
                                   .Set<TEntity>()
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(e => e.Id == id);

            var model = this.mapper.Map<TModel>(entity);

            return model;
        }

        public async Task UpdateAsync(TModel model)
        {
            var entity = this.mapper.Map<TEntity>(model);
            this.context.Update(entity);
            await this.context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}
