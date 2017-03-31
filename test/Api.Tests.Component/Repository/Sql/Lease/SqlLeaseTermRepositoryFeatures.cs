using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;

namespace Api.Tests.Component.Repository.Sql.Lease
{
    public class SqlLeaseTermRepositoryFeatures : RepositoryFeatures<LeaseTermModel, LeaseTermEntity>
    {
        public SqlLeaseTermRepositoryFeatures() : base(new SqlLeaseTermRepositorySteps())
        {
        }
    }
}
