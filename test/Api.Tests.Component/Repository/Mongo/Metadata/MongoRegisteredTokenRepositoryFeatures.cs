using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepositoryFeatures : MongoRepositoryFeatures<RegisteredTokenModel, RegisteredTokenEntity>
    {
        public MongoRegisteredTokenRepositoryFeatures() : base(new MongoRegisteredTokenRepositorySteps())
        {
        }
    }
}
