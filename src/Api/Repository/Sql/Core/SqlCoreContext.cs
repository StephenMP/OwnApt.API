using Microsoft.EntityFrameworkCore;

namespace OwnApt.Api.Repository.Sql.Core
{
    public class SqlCoreContext : DbContext
    {
        #region Public Constructors

        public SqlCoreContext(DbContextOptions<SqlCoreContext> options) : base(options)
        {
        }

        #endregion Public Constructors
    }
}
