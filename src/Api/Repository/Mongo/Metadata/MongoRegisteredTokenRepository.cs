using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepository : MongoRepository<RegisteredTokenModel, RegisteredTokenEntity>, IRegisteredTokenRepository
    {
        #region Private Fields

        private readonly IMongoMetadataContext mongoMetadataContext;

        #endregion Private Fields

        #region Public Constructors

        public MongoRegisteredTokenRepository(IMongoMetadataContext mongoMetadataContext, IMapper mapper) : base(mongoMetadataContext.RegisteredTokenCollection, mapper)
        {
            this.mongoMetadataContext = mongoMetadataContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<RegisteredTokenModel> ReadByTokenAsync(string token)
        {
            var cursor = await this.mongoMetadataContext.RegisteredTokenCollection.FindAsync(t => t.Token == token);
            var entity = await cursor.FirstOrDefaultAsync();
            var model = this.mapper.Map<RegisteredTokenModel>(entity);

            return model;
        }

        #endregion Public Methods
    }
}
