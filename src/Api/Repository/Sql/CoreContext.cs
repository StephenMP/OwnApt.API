using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Repository.Sql
{
    public class CoreContext : DbContext
    {
        #region Constructors

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        #endregion Constructors

        #region Properties

        public DbSet<UserLoginEntity> UserLogin { get; set; }

        #endregion Properties
    }
}
