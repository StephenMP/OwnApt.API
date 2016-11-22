using System;
using System.Threading.Tasks;
using AutoMapper;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;
using OwnApt.Api.Repository.Mongo.Metadata;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Metadata
{
    public class MongoRegisteredTokenRepositorySteps : MongoRepositorySteps<RegisteredTokenModel, RegisteredTokenEntity>
    {
        public MongoRegisteredTokenRepositorySteps() : base("Metadata", "RegisteredToken")
        {
        }

        public override void GivenIHaveARepository()
        {
            this.repository = new MongoRegisteredTokenRepository(new MongoMetadataContext(this.environment.GetMongoClient()), OwnAptStartup.BuildMapper());
        }
    }
}