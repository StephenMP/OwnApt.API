using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OwnApt.Api.Repository.Sql
{
    public interface ISqlRepositoryService
    {
        Task UpdateSqlEntityAsync<TModel, TEntity, TContext>(TModel model, TContext context) where TContext : DbContext where TEntity : class;
        Task<TModel> ReadSqlEntityAsync<TModel, TEntity, TContext>(int id, TContext context, Expression<Func<TEntity, bool>> lookupPredicate) where TEntity : class where TContext : DbContext;
        Task DeleteSqlEntityAsync<TEntity, TContext>(TEntity entity, TContext context) where TContext : DbContext where TEntity : class;
        Task<TModel> CreateSqlEntityAsync<TModel, TEntity, TContext>(TModel model, TContext context) where TEntity : class where TContext : DbContext;
    }

    public class SqlRepositoryService : ISqlRepositoryService
    {
        private readonly IMapper mapper;

        public SqlRepositoryService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task UpdateSqlEntityAsync<TModel, TEntity, TContext>(TModel model, TContext context) where TContext : DbContext where TEntity : class
        {
            var entity = this.mapper.Map<TEntity>(model);
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TModel> ReadSqlEntityAsync<TModel, TEntity, TContext>(int id, TContext context, Expression<Func<TEntity, bool>> lookupPredicate) where TEntity : class where TContext : DbContext
        {
            var entity = await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(lookupPredicate);
            var model = this.mapper.Map<TModel>(entity);
            return model;
        }

        public async Task DeleteSqlEntityAsync<TEntity, TContext>(TEntity entity, TContext context) where TContext : DbContext where TEntity : class
        {
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<TModel> CreateSqlEntityAsync<TModel, TEntity, TContext>(TModel model, TContext context) where TEntity : class where TContext : DbContext
        {
            var entity = this.mapper.Map<TEntity>(model);
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();

            return model;
        }
    }
}
