using Microsoft.EntityFrameworkCore;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class LeaseContext : DbContext
    {
        #region Public Constructors

        public LeaseContext(DbContextOptions<LeaseContext> options) : base(options)
        {
        }

        #endregion Public Constructors
    }
}
