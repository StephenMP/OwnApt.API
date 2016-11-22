using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Mongo.Document;

namespace Api.Tests.Component.Repository.Mongo.Document
{
    public class ManagementAgreementRepositorySteps : MongoRepositorySteps<ManagementAgreementModel, ManagementAgreementEntity>
    {
        public ManagementAgreementRepositorySteps() : base("Document", "ManagementAgreement")
        {
        }

        public override void GivenIHaveARepository()
        {
            this.repository = new ManagementAgreementRepository(this.collection, this.mapper);
        }
    }
}
