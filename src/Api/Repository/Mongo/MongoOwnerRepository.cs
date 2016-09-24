using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Common.Extension;
using OwnApt.Common.Utility.Data;
using System;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo
{
    public class MongoOwnerRepository : IOwnerRepository
    {
        #region Private Fields

        private readonly IMongoDatabase coreDatabase;
        private readonly IMapper mapper;

        #endregion Private Fields

        #region Public Constructors

        public MongoOwnerRepository(IMongoClient mongoClient, IMapper mapper)
        {
            this.coreDatabase = mongoClient.GetDatabase("Core");
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Private Properties

        private IMongoCollection<OwnerEntity> OwnerCollection => this.coreDatabase.GetCollection<OwnerEntity>("Owner");

        #endregion Private Properties

        #region Public Methods

        public async Task<OwnerModel> CreateAsync(OwnerModel model)
        {
            model.Id = model.Id.ValueIfNullOrWhitespace(DataUtility.GenerateId());
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

        #endregion Public Methods
    }
}
