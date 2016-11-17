using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Mongo.Core
{
    public class MongoPropertyRepository : MongoRepository<PropertyModel, PropertyEntity>, IPropertyRepository
    {
        #region Private Fields

        private readonly IMongoCoreContext mongoCoreContext;

        #endregion Private Fields

        #region Public Constructors

        public MongoPropertyRepository(IMongoCoreContext mongoCoreContext, IMapper mapper) : base(mongoCoreContext.PropertiesCollection, mapper)
        {
            this.mongoCoreContext = mongoCoreContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<PropertyModel>> ReadManyAsync(string[] propertyIds)
        {
            var asyncCursor = await this.mongoCoreContext.PropertiesCollection.FindAsync(p => propertyIds.Contains(p.Id));
            var propertyEntities = await asyncCursor.ToListAsync();
            return this.mapper.Map<List<PropertyModel>>(propertyEntities);
        }

        #endregion Public Methods
    }
}
