using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.AppStart;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Mongo;
using OwnApt.Api.Repository.Mongo;
using OwnApt.Api.Repository.Mongo.Document;
using Xunit;

namespace Api.Tests.Component.Repository.Mongo.Document
{
    public class ManagementAgreementRepositoryFeatures : RepositoryFeatures<ManagementAgreementModel, ManagementAgreementEntity>
    {
        public ManagementAgreementRepositoryFeatures() : base(new ManagementAgreementRepositorySteps())
        {
        }


    }
}
