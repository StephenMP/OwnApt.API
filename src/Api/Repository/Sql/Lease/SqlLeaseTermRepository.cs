using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class SqlLeaseTermRepository : SqlRepository<LeaseTermModel, LeaseTermEntity, LeaseContext>, ILeaseTermRepository
    {
        #region Public Constructors

        public SqlLeaseTermRepository(LeaseContext leaseContext, IMapper mapper) : base(leaseContext, mapper)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task<LeaseTermModel> ReadAsync(int id)
        {
            var entity = await this.DbSet
                                   .AsNoTracking()
                                   .Include(e => e.LeasePeriods)
                                   .SingleOrDefaultAsync(e => e.LeaseTermId == id);

            var model = this.Mapper.Map<LeaseTermModel>(entity);

            return model;
        }

        public async Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId)
        {
            var entity = await this.DbSet
                                   .AsNoTracking()
                                   .Include(e => e.LeasePeriods)
                                   .SingleOrDefaultAsync
                                   (e => e.StartDate <= DateTime.UtcNow
                                         && e.EndDate > DateTime.UtcNow
                                         && e.PropertyId == propertyId
                                    );

            var model = this.Mapper.Map<LeaseTermModel>(entity);

            return model;
        }

        #endregion Public Methods
    }
}
