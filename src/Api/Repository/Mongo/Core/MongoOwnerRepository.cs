using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Common.Extension;
using OwnApt.Common.Utility.Data;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo.Core
{
    public class MongoOwnerRepository : IOwnerRepository
    {
        #region Private Fields

        private readonly IMapper mapper;
        private readonly IMongoCoreContext mongoCoreContext;

        #endregion Private Fields

        #region Public Constructors

        public MongoOwnerRepository(IMongoCoreContext mongoCoreContext, IMapper mapper)
        {
            this.mongoCoreContext = mongoCoreContext;
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<OwnerModel> CreateAsync(OwnerModel model)
        {
            model.Id = model.Id.ValueIfNullOrWhitespace(DataUtility.GenerateId());
            var ownerEntity = this.mapper.Map<OwnerEntity>(model);
            await this.mongoCoreContext.OwnerCollection.InsertOneAsync(ownerEntity);

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await this.mongoCoreContext.OwnerCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<OwnerModel> ReadAsync(string id)
        {
            var asyncCursor = await this.mongoCoreContext.OwnerCollection.FindAsync(p => p.Id == id);
            var ownerEntity = await asyncCursor.FirstOrDefaultAsync();
            return this.mapper.Map<OwnerModel>(ownerEntity);
        }

        public async Task UpdateAsync(OwnerModel model)
        {
            var ownerEntity = this.mapper.Map<OwnerEntity>(model);
            await this.mongoCoreContext.OwnerCollection.ReplaceOneAsync(p => p.Id == model.Id, ownerEntity);
        }

        #endregion Public Methods
    }
}
