using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Mongo.Document
{
    public class ManagementAgreementRepository : MongoRepository<ManagementAgreementModel, ManagementAgreementEntity>, IManagementAgreementRepository
    {
        public ManagementAgreementRepository(IMongoCollection<ManagementAgreementEntity> collection, IMapper mapper) : base(collection, mapper)
        {
        }
    }
}
