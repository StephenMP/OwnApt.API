using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Common.Dto;

namespace OwnApt.Api.Repository.Entity.Mongo
{
    public class ManagementAgreementEntity : MongoEntity
    {
        public int Version { get; set; }
        public List<ManagementAgreementSectionEntity> Sections { get; set; }

    }

    public class ManagementAgreementSectionEntity : Equatable
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
