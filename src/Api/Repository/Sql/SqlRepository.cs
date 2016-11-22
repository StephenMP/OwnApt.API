using System;
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
        #region Protected Constructors

        protected SqlRepository(TContext context, IMapper mapper)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
            this.Mapper = mapper;
        }

        #endregion Protected Constructors

        #region Protected Properties

        protected TContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }
        protected IMapper Mapper { get; }

        #endregion Protected Properties

        #region Public Methods

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            var entity = this.Mapper.Map<TEntity>(model);
            this.DbSet.Add(entity);

            await this.Context.SaveChangesAsync();

            model = this.Mapper.Map<TModel>(entity);
            return model;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var model = await this.ReadAsync(id);
            var entity = this.Mapper.Map<TEntity>(model);

            if (entity != null)
            {
                this.DbSet.Remove(entity);
                await this.Context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            var entities = await this.DbSet
                                     .AsNoTracking()
                                     .AsQueryable()
                                     .ToListAsync();

            var models = this.Mapper.Map<List<TModel>>(entities);

            return models;
        }

        public virtual async Task<TModel> ReadAsync(int id)
        {
            var entity = await this.DbSet
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(e => e.Id == id);

            var model = this.Mapper.Map<TModel>(entity);

            return model;
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            var entity = this.Mapper.Map<TEntity>(model);
            this.DbSet.Update(entity);
            await this.Context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}
