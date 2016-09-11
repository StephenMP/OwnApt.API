using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity;
using OwnApt.Api.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        #region Fields

        private readonly IMongoDatabase coreDatabase;
        private readonly IMapper mapper;

        #endregion Fields

        #region Constructors

        public MongoPropertyRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        #endregion Constructors

        #region Properties

        private IMongoCollection<PropertyEntity> PropertiesCollection => this.coreDatabase.GetCollection<PropertyEntity>("Property");

        #endregion Properties

        #region Methods

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

        public async Task MapOwnerToPropertiesAsync(string ownerId, params string[] propertyIds)
        {
            var cursor = await this.PropertiesCollection.FindAsync(p => propertyIds.Contains(p.Id));
            var properties = await cursor.ToListAsync();

            foreach (var property in properties)
            {
                property.OwnerIds.Add(ownerId);
                await this.PropertiesCollection.ReplaceOneAsync(p => p.Id == property.Id, property);
            }
        }

        public async Task<PropertyModel> ReadAsync(string id)
        {
            var asyncCursor = await this.PropertiesCollection.FindAsync(p => p.Id == id);
            var propertyEntity = await asyncCursor.FirstOrDefaultAsync();
            return this.mapper.Map<PropertyModel>(propertyEntity);
        }

        public async Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId)
        {
            var propertyEntities = await this.PropertiesCollection.FindAsync(p => p.OwnerIds.Any(o => o.Contains(ownerId)));
            var propertyModels = await propertyEntities.ToListAsync();
            return this.mapper.Map<PropertyModel[]>(propertyModels);
        }

        public async Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId)
        {
            var propertyEntities = await this.PropertiesCollection.FindAsync(p => p.TenantIds.Any(t => t.Contains(tenantId)));
            var propertyModels = await propertyEntities.ToListAsync();
            return this.mapper.Map<PropertyModel[]>(propertyModels);
        }

        public async Task UpdateAsync(PropertyModel model)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(model);
            await this.PropertiesCollection.ReplaceOneAsync(p => p.Id == model.Id, propertyEntity);
        }

        #endregion Methods
    }
}
