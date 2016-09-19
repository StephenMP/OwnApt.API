using AutoMapper;
using OwnApt.Api.Contract.Model;
using OwnApt.Api.Repository.Entity.Sql;
using OwnApt.Api.Repository.Interface;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class TermRepository : ITermRepository
    {
        #region Private Fields

        private readonly LeaseContext leaseContex;
        private readonly IMapper mapper;

        #endregion Private Fields

        #region Public Constructors

        public TermRepository(LeaseContext leaseContext, IMapper mapper)
        {
            this.leaseContex = leaseContext;
            this.mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<TermModel> CreateAsync(TermModel model)
        {
            var entity = this.mapper.Map<TermEntity>(model);
            this.leaseContex.Term.Add(entity);

            await this.leaseContex.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = new TermEntity { TermId = id };
            this.leaseContex.Term.Attach(entity);
            this.leaseContex.Term.Remove(entity);
            await this.leaseContex.SaveChangesAsync();
        }

        public async Task<TermModel> ReadAsync(string id)
        {
            var entity = await this.leaseContex.UspReadTermAsync(id);
            var model = this.mapper.Map<TermModel>(entity);

            return model;
        }

        public async Task UpdateAsync(TermModel model)
        {
            var entity = new TermEntity
            {
                EndDate = model.EndDate,
                PropertyId = model.PropertyId,
                Rent = model.Rent,
                StartDate = model.StartDate,
                TermId = model.TermId
            };

            this.leaseContex.Term.Update(entity);
            await this.leaseContex.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}
