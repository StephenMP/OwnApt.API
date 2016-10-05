using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Common.Extension;
using OwnApt.Common.Utility.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Mongo.Core
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        #region Private Fields

        private readonly IMapper mapper;
        private readonly IMongoCoreContext mongoCoreContext;

        #endregion Private Fields

        #region Public Constructors

        public MongoPropertyRepository(IMongoCoreContext mongoCoreContext, IMapper mapper)
        {
            this.mongoCoreContext = mongoCoreContext;
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PropertyModel> CreateAsync(PropertyModel model)
        {
            model.Id = model.Id.ValueIfNullOrWhitespace(DataUtility.GenerateId());
            var propertyEntity = this.mapper.Map<PropertyEntity>(model);
            await this.mongoCoreContext.PropertiesCollection.InsertOneAsync(propertyEntity);
            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await this.mongoCoreContext.PropertiesCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<PropertyModel> ReadAsync(string id)
        {
            var asyncCursor = await this.mongoCoreContext.PropertiesCollection.FindAsync(p => p.Id == id);
            var propertyEntity = await asyncCursor.FirstOrDefaultAsync();
            return this.mapper.Map<PropertyModel>(propertyEntity);
        }

        public async Task<PropertyModel[]> ReadManyAsync(string[] propertyIds)
        {
            var asyncCursor = await this.mongoCoreContext.PropertiesCollection.FindAsync(p => propertyIds.Contains(p.Id));
            var propertyEntity = await asyncCursor.ToListAsync();
            return this.mapper.Map<PropertyModel[]>(propertyEntity);
        }

        public async Task UpdateAsync(PropertyModel model)
        {
            var propertyEntity = this.mapper.Map<PropertyEntity>(model);
            await this.mongoCoreContext.PropertiesCollection.ReplaceOneAsync(p => p.Id == model.Id, propertyEntity);
        }

        #endregion Public Methods
    }
}
