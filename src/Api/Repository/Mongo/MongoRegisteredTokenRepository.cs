using OwnApt.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using MongoDB.Driver;
using AutoMapper;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Common.Utility.Data;
using OwnApt.Common.Extension;

namespace OwnApt.Api.Repository.Mongo
{
    public class MongoRegisteredTokenRepository : IRegisteredTokenRepository
    {
        private IMapper mapper;
        private IMongoDatabase metadataDatabase;

        public MongoRegisteredTokenRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.metadataDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        private IMongoCollection<RegisteredTokenEntity> RegisteredTokenCollection => this.metadataDatabase.GetCollection<RegisteredTokenEntity>("RegisteredToken");

        public async Task<RegisteredTokenModel> CreateAsync(RegisteredTokenModel model)
        {
            model.Id = model.Id.ValueIfNullOrWhitespace(DataUtility.GenerateId());
            var entity = this.mapper.Map<RegisteredTokenEntity>(model);
            await this.RegisteredTokenCollection.InsertOneAsync(entity);

            return model;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotSupportedException("We do not delete registered tokens");
        }

        public async Task<RegisteredTokenModel> ReadAsync(string id)
        {
            var cursor = await this.RegisteredTokenCollection.FindAsync(t => t.Id == id);
            var entity = await cursor.FirstOrDefaultAsync();
            var model = this.mapper.Map<RegisteredTokenModel>(entity);

            return model;
        }

        public async Task<RegisteredTokenModel> ReadByTokenAsync(string token)
        {
            var cursor = await this.RegisteredTokenCollection.FindAsync(t => t.Token == token);
            var entity = await cursor.FirstOrDefaultAsync();
            var model = this.mapper.Map<RegisteredTokenModel>(entity);

            return model;
        }

        public Task UpdateAsync(RegisteredTokenModel model)
        {
            throw new NotSupportedException("We do not update registered tokens");
        }
    }
}
