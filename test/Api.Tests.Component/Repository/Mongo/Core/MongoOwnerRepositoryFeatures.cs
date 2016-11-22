using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Core
{
    public class MongoOwnerRepositoryFeatures : MongoRepositoryFeatures<OwnerModel, OwnerEntity>
    {
        public MongoOwnerRepositoryFeatures() : base(new MongoOwnerRepositorySteps())
        {
        }
    }
}
