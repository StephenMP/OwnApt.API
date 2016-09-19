using Microsoft.EntityFrameworkCore;

namespace OwnApt.Api.Repository.Sql.Core
{
    public class CoreContext : DbContext
    {
        #region Public Constructors

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        #endregion Public Constructors
    }
}
