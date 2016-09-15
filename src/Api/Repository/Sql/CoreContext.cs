using Microsoft.EntityFrameworkCore;

namespace OwnApt.Api.Repository.Sql
{
    public class CoreContext : DbContext
    {
        #region Constructors

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        #endregion Constructors
    }
}
