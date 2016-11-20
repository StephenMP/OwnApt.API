using MongoDB.Driver;
using OwnApt.Api.Repository.Entity.Mongo;

namespace OwnApt.Api.Repository.Mongo.Core
{
    public interface IMongoCoreContext
    {
        #region Public Properties

        IMongoCollection<OwnerEntity> OwnerCollection { get; }
        IMongoCollection<PropertyEntity> PropertyCollection { get; }

        #endregion Public Properties
    }

    public class MongoCoreContext : IMongoCoreContext
    {
        #region Private Fields

        private readonly IMongoDatabase coreDatabase;

        #endregion Private Fields

        #region Public Constructors

        public MongoCoreContext(IMongoClient mongoClient)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
        }

        #endregion Public Constructors

        #region Public Properties

        public IMongoCollection<OwnerEntity> OwnerCollection => this.coreDatabase.GetCollection<OwnerEntity>("Owner");
        public IMongoCollection<PropertyEntity> PropertyCollection => this.coreDatabase.GetCollection<PropertyEntity>("Property");

        #endregion Public Properties
    }
}
