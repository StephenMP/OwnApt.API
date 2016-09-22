using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        #region Private Fields

        private readonly IMongoDatabase coreDatabase;
        private readonly IMapper mapper;

        #endregion Private Fields

        #region Public Constructors

        public MongoPropertyRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Private Properties

        private IMongoCollection<PropertyEntity> PropertiesCollection => this.coreDatabase.GetCollection<PropertyEntity>("Property");

        #endregion Private Properties

        #region Public Methods

        public async Task<PropertyModel> CreateAsync(PropertyModel model)
        {
            model.Id = Guid.NewGuid().ToString("N");
            var propertyEntity = this.mapper.Map<PropertyEntity>(model);
            await this.PropertiesCollection.InsertOneAsync(propertyEntity);
            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await this.PropertiesCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<PropertyModel> ReadAsync(string id)
        {
            var asyncCursor = await this.PropertiesCollection.FindAsync(p => p.Id == id);
            var propertyEntity = await asyncCursor.FirstOrDefaultAsync();
            return this.mapper.Map<PropertyModel>(propertyEntity);
        }

        public async Task UpdateAsync(PropertyModel model)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(model);
            await this.PropertiesCollection.ReplaceOneAsync(p => p.Id == model.Id, propertyEntity);
        }

        #endregion Public Methods
    }
}
