﻿using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo
{
    public class MongoOwnerRepository : IOwnerRepository
    {
        #region Fields

        private readonly IMongoDatabase coreDatabase;
        private readonly IMapper mapper;

        #endregion Fields

        #region Constructors

        public MongoOwnerRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        #endregion Constructors

        #region Properties

        private IMongoCollection<OwnerEntity> OwnerCollection => this.coreDatabase.GetCollection<OwnerEntity>("Owner");

        #endregion Properties

        #region Methods

        public async Task<OwnerModel> CreateAsync(OwnerModel model)
        {
            model.Id = Guid.NewGuid().ToString("N");
            var ownerEntity = this.mapper.Map<OwnerEntity>(model);
            await this.OwnerCollection.InsertOneAsync(ownerEntity);

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await this.OwnerCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<OwnerModel> ReadAsync(string id)
        {
            var asyncCursor = await this.OwnerCollection.FindAsync(p => p.Id == id);
            var ownerEntity = await asyncCursor.FirstOrDefaultAsync();
            return this.mapper.Map<OwnerModel>(ownerEntity);
        }

        public async Task UpdateAsync(OwnerModel model)
        {
            var ownerEntity = this.mapper.Map<OwnerEntity>(model);
            await this.OwnerCollection.ReplaceOneAsync(p => p.Id == model.Id, ownerEntity);
        }

        #endregion Methods
    }
}