using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Repository.Sql
{
    public partial class CoreContext : DbContext
    {
        #region Public Constructors

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<UserLoginEntity> UserLogin { get; set; }

        #endregion Public Properties
    }
}
