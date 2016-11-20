using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Repository.Entity.Sql;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class LeaseContext : DbContext
    {
        #region Public Constructors

        public LeaseContext(DbContextOptions<LeaseContext> options) : base(options)
        {
        }

        DbSet<LeaseTermEntity> LeaseTerm { get; set; }
        DbSet<LeasePeriodEntity> LeasePeriod { get; set; }

        #endregion Public Constructors
    }
}
