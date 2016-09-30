using Microsoft.EntityFrameworkCore;
using OwnApt.Api.Repository.Entity.Sql;

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
