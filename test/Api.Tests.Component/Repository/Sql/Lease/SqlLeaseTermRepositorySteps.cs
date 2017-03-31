using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Sql.Lease;

namespace Api.Tests.Component.Repository.Sql.Lease
{
    public class SqlLeaseTermRepositorySteps : SqlRepositorySteps<LeaseTermModel, LeaseTermEntity, LeaseContext>
    {
        public override void GivenIHaveARepository()
        {
            this.repository = new SqlLeaseTermRepository(new LeaseContext(this.environment.GetSqlDbContextOptions<LeaseContext>()), this.mapper);
        }
    }
}
