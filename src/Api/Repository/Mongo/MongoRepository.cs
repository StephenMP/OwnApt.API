using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Mongo
{
    public abstract class MongoRepository<TModel, TEntity> : IRepository<TModel, string> where TEntity : MongoEntity
    {
        #region Protected Fields

        protected readonly IMongoCollection<TEntity> collection;
        protected readonly IMapper mapper;

        #endregion Protected Fields

        #region Protected Constructors

        protected MongoRepository(IMongoCollection<TEntity> collection, IMapper mapper)
        {
            this.collection = collection;
            this.mapper = mapper;
        }

        #endregion Protected Constructors

        #region Public Methods

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            var entity = this.mapper.Map<TEntity>(model);
            await this.collection.InsertOneAsync(entity);
            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await this.collection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<TModel>> ReadAllAsync()
        {
            var asyncCursor= await this.collection.FindAsync(e => true);
            var entities = await asyncCursor.ToListAsync();
            var models = this.mapper.Map<List<TModel>>(entities);

            return models;
        }

        public async Task<TModel> ReadAsync(string id)
        {
            var asyncCursor = await this.collection.FindAsync(p => p.Id == id);
            var entity = await asyncCursor.FirstOrDefaultAsync();
            var model = this.mapper.Map<TModel>(entity);

            return model;
        }

        public async Task UpdateAsync(TModel model)
        {
            var entity = this.mapper.Map<TEntity>(model);
            await this.collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        #endregion Public Methods
    }
}
