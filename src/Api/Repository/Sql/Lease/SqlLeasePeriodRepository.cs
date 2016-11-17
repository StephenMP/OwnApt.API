using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class SqlLeasePeriodRepository : SqlRepository<LeasePeriodModel, LeasePeriodEntity, LeaseContext>, ILeasePeriodRepository
    {
        #region Public Constructors

        public SqlLeasePeriodRepository(LeaseContext leaseContext, IMapper mapper) : base(leaseContext, mapper)
        {
        }

        #endregion Public Constructors
    }
}
