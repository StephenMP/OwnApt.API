using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class SqlLeaseTermRepository : ILeaseTermRepository
    {
        #region Private Fields

        private readonly LeaseContext leaseContex;
        private readonly IMapper mapper;

        #endregion Private Fields

        #region Public Constructors

        public SqlLeaseTermRepository(LeaseContext leaseContext, IMapper mapper)
        {
            this.leaseContex = leaseContext;
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<LeaseTermModel> CreateAsync(LeaseTermModel model)
        {
            var entity = this.mapper.Map<LeaseTermEntity>(model);
            this.leaseContex.LeaseTerm.Add(entity);

            await this.leaseContex.SaveChangesAsync();

            return model;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotSupportedException("We do not delete lease terms");
        }

        public async Task<LeaseTermModel> ReadAsync(int id)
        {
            var entity = await this.leaseContex
                                   .LeaseTerm
                                   .AsNoTracking()
                                   .Include(e => e.LeasePeriods)
                                   .SingleOrDefaultAsync(e => e.LeaseTermId == id);

            var model = this.mapper.Map<LeaseTermModel>(entity);

            return model;
        }

        public async Task<LeaseTermModel> ReadByPropertyIdAsync(string propertyId)
        {
            var entity = await this.leaseContex
                                   .LeaseTerm
                                   .AsNoTracking()
                                   .Include(e => e.LeasePeriods)
                                   .SingleOrDefaultAsync
                                   (e => e.StartDate <= DateTime.UtcNow
                                         && e.EndDate > DateTime.UtcNow
                                         && e.PropertyId == propertyId
                                    );

            var model = this.mapper.Map<LeaseTermModel>(entity);

            return model;
        }

        public Task UpdateAsync(LeaseTermModel model)
        {
            throw new NotSupportedException("We do not update lease terms");
        }

        #endregion Public Methods
    }
}
