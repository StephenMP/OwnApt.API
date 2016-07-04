using OwnApt.Api.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Domain.Model;
using MongoDB.Driver;
using AutoMapper;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Repository
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        private IMapper mapper;
        private IMongoDatabase propertyDatabase;

        IMongoCollection<PropertyEntity> PropertiesCollection => this.propertyDatabase.GetCollection<PropertyEntity>("Property");

        public MongoPropertyRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.propertyDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        public void CreateProperty(PropertyModel property)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(property);
            this.PropertiesCollection.InsertOne(propertyEntity);
        }

        public async Task CreatePropertyAsync(PropertyModel property)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(property);
            await this.PropertiesCollection.InsertOneAsync(propertyEntity);
        }

        public void DeleteProperty(string id)
        {
            this.PropertiesCollection.DeleteOne(p => p.Id == id);
        }

        public async Task DeletePropertyAsync(string id)
        {
            await this.PropertiesCollection.DeleteOneAsync(p => p.Id == id);
        }

        public IEnumerable<PropertyModel> ReadPropertiesForOwner(string ownerId)
        {
            var propertyModels = this.PropertiesCollection.Find(p => p.Owners.Where(o => o.Id == ownerId).Any()).ToList();
            return this.mapper.Map<List<PropertyModel>>(propertyModels);
        }

        public async Task<IEnumerable<PropertyModel>> ReadPropertiesForOwnerAsync(string ownerId)
        {
            var propertyEntities = await this.PropertiesCollection.FindAsync(p => p.Owners.Where(o => o.Id == ownerId).Any());
            var propertyModels = await propertyEntities.ToListAsync();
            return this.mapper.Map<List<PropertyModel>>(propertyModels);
        }

        public IEnumerable<PropertyModel> ReadPropertiesForTenant(string tenantId)
        {
            var propertyModels = this.PropertiesCollection.Find(p => p.Owners.Where(o => o.Id == tenantId).Any()).ToList();
            return this.mapper.Map<List<PropertyModel>>(propertyModels);
        }

        public async Task<IEnumerable<PropertyModel>> ReadPropertiesForTenantAsync(string tenantId)
        {
            var propertyEntities = await this.PropertiesCollection.FindAsync(p => p.Owners.Where(o => o.Id == tenantId).Any());
            var propertyModels = await propertyEntities.ToListAsync();
            return this.mapper.Map<List<PropertyModel>>(propertyModels);
        }

        public PropertyModel ReadProperty(string id)
        {
            var propertyModel = this.PropertiesCollection.Find(p => p.Id == id);
            return this.mapper.Map<PropertyModel>(propertyModel);
        }

        public async Task<PropertyModel> ReadPropertyAsync(string id)
        {
            var property = await this.PropertiesCollection.FindAsync(p => p.Id == id);
            return this.mapper.Map<PropertyModel>(property);
        }

        public void UpdateProperty(PropertyModel property)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(property);
            this.PropertiesCollection.ReplaceOne(p => p.Id == property.Id, propertyEntity);
        }

        public async Task UpdatePropertyAsync(PropertyModel property)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(property);
            await this.PropertiesCollection.ReplaceOneAsync(p => p.Id == property.Id, propertyEntity);
        }
    }
}
