using MongoDB.Driver;
using OwnApt.Api.Repository.Entity.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo.Core
{
    public interface IMongoCoreContext
    {
        IMongoCollection<OwnerEntity> OwnerCollection { get; }
        IMongoCollection<PropertyEntity> PropertiesCollection { get; }
    }

    public class MongoCoreContext : IMongoCoreContext
    {
        private readonly IMongoDatabase coreDatabase;

        public MongoCoreContext(IMongoClient mongoClient)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
        }

        public IMongoCollection<OwnerEntity> OwnerCollection => this.coreDatabase.GetCollection<OwnerEntity>("Owner");
        public IMongoCollection<PropertyEntity> PropertiesCollection => this.coreDatabase.GetCollection<PropertyEntity>("Property");
    }
}
