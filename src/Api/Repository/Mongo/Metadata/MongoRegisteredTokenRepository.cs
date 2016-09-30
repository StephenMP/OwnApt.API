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

namespace OwnApt.Api.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepository : IRegisteredTokenRepository
    {
        private readonly IMapper mapper;
        private readonly IMongoMetadataContext mongoMetadataContext;

        public MongoRegisteredTokenRepository(IMongoMetadataContext mongoMetadataContext, IMapper mapper)
        {
            this.mongoMetadataContext = mongoMetadataContext;
            this.mapper = mapper;
        }


        public async Task<RegisteredTokenModel> CreateAsync(RegisteredTokenModel model)
        {
            model.Id = model.Id.ValueIfNullOrWhitespace(DataUtility.GenerateId());
            var entity = this.mapper.Map<RegisteredTokenEntity>(model);
            await this.mongoMetadataContext.RegisteredTokenCollection.InsertOneAsync(entity);

            return model;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotSupportedException("We do not delete registered tokens");
        }

        public async Task<RegisteredTokenModel> ReadAsync(string id)
        {
            var cursor = await this.mongoMetadataContext.RegisteredTokenCollection.FindAsync(t => t.Id == id);
            var entity = await cursor.FirstOrDefaultAsync();
            var model = this.mapper.Map<RegisteredTokenModel>(entity);

            return model;
        }

        public async Task<RegisteredTokenModel> ReadByTokenAsync(string token)
        {
            var cursor = await this.mongoMetadataContext.RegisteredTokenCollection.FindAsync(t => t.Token == token);
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
