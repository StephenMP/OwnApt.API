﻿using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Domain.Model;
using OwnApt.Api.Repository.Entity;
using OwnApt.Api.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        #region Private Fields + Properties

        private IMongoCollection<PropertyEntity> PropertiesCollection => this.coreDatabase.GetCollection<PropertyEntity>("Property");
        private readonly IMongoDatabase coreDatabase;
        private readonly IMapper mapper;

        #endregion Private Fields + Properties

        #region Public Constructors + Destructors

        public MongoPropertyRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        #endregion Public Constructors + Destructors

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

        public async Task<PropertyModel[]> ReadPropertiesForOwnerAsync(string ownerId)
        {
            var propertyEntities = await this.PropertiesCollection.FindAsync(p => p.OwnerIds.Where(o => o.Contains(ownerId)).Any());
            var propertyModels = await propertyEntities.ToListAsync();
            return this.mapper.Map<PropertyModel[]>(propertyModels);
        }

        public async Task<PropertyModel[]> ReadPropertiesForTenantAsync(string tenantId)
        {
            var propertyEntities = await this.PropertiesCollection.FindAsync(p => p.TenantIds.Where(t => t.Contains(tenantId)).Any());
            var propertyModels = await propertyEntities.ToListAsync();
            return this.mapper.Map<PropertyModel[]>(propertyModels);
        }

        public async Task UpdateAsync(PropertyModel model)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(model);
            await this.PropertiesCollection.ReplaceOneAsync(p => p.Id == model.Id, propertyEntity);
        }

        #endregion Public Methods
    }
}