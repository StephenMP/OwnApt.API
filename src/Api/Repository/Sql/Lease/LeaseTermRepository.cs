using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class LeaseTermRepository : ILeaseTermRepository
    {
        #region Private Fields

        private readonly LeaseContext leaseContex;
        private readonly IMapper mapper;

        #endregion Private Fields

        #region Public Constructors

        public LeaseTermRepository(LeaseContext leaseContext, IMapper mapper)
        {
            this.leaseContex = leaseContext;
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<LeaseTermModel> CreateAsync(LeaseTermModel model)
        {
            var entity = this.mapper.Map<LeaseTermEntity>(model);
            this.leaseContex.Term.Add(entity);

            await this.leaseContex.SaveChangesAsync();

            return model;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotSupportedException();
        }

        public async Task<LeaseTermModel> ReadAsync(string id)
        {
            var entity = await this.leaseContex.Term.SingleOrDefaultAsync(e => e.LeaseTermId == id);
            var model = this.mapper.Map<LeaseTermModel>(entity);

            return model;
        }

        public Task UpdateAsync(LeaseTermModel model)
        {
            throw new NotSupportedException();
        }

        #endregion Public Methods
    }
}
