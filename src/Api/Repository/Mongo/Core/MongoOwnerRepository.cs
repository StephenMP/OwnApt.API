using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Mongo.Core
{
    public class MongoOwnerRepository : MongoRepository<OwnerModel, OwnerEntity>, IOwnerRepository
    {
        #region Public Constructors

        public MongoOwnerRepository(IMongoCoreContext mongoCoreContext, IMapper mapper) : base(mongoCoreContext.OwnerCollection, mapper)
        {
        }

        #endregion Public Constructors
    }
}
