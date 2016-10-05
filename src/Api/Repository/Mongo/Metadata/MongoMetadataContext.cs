using MongoDB.Driver;
using OwnApt.Api.Repository.Entity.Mongo;

namespace OwnApt.Api.Repository.Mongo.Metadata
{
    public interface IMongoMetadataContext
    {
        #region Public Properties

        IMongoCollection<RegisteredTokenEntity> RegisteredTokenCollection { get; }

        #endregion Public Properties
    }

    public class MongoMetadataContext : IMongoMetadataContext
    {
        #region Private Fields

        private readonly IMongoDatabase metadataDatabase;

        #endregion Private Fields

        #region Public Constructors

        public MongoMetadataContext(IMongoClient mongoClient)
        {
            this.metadataDatabase = mongoClient.GetDatabase("Metadata");
        }

        #endregion Public Constructors

        #region Public Properties

        public IMongoCollection<RegisteredTokenEntity> RegisteredTokenCollection => this.metadataDatabase.GetCollection<RegisteredTokenEntity>("RegisteredToken");

        #endregion Public Properties
    }
}
