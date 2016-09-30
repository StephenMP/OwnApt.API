using MongoDB.Driver;
using OwnApt.Api.Repository.Entity.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo.Metadata
{
    public interface IMongoMetadataContext
    {
        IMongoCollection<RegisteredTokenEntity> RegisteredTokenCollection { get; }
    }
    public class MongoMetadataContext : IMongoMetadataContext
    {
        private readonly IMongoDatabase metadataDatabase;

        public MongoMetadataContext(IMongoClient mongoClient)
        {
            this.metadataDatabase = mongoClient.GetDatabase("Metadata");
        }
        public IMongoCollection<RegisteredTokenEntity> RegisteredTokenCollection => this.metadataDatabase.GetCollection<RegisteredTokenEntity>("RegisteredToken");
    }
}
