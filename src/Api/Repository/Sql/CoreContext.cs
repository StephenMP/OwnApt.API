using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Repository.Entity;

namespace OwnApt.Api.Repository.Sql
{
    public partial class CoreContext : DbContext
    {
        #region Public Fields + Properties

        public DbSet<UserLoginEntity> UserLogin { get; set; }

        #endregion Public Fields + Properties

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }
    }
}
