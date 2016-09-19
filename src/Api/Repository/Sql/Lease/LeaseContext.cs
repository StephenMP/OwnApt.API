using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using OwnApt.Api.Repository.Entity.Sql;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Sql.Lease
{
    public class LeaseContext : DbContext
    {
        #region Public Constructors

        public LeaseContext(DbContextOptions<LeaseContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<TermEntity> Term { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<TermEntity> UspReadTermAsync(string id)
        {
            var termParams = new object[]
            {
                new MySqlParameter("termId", id)
            };

            var result = await this.Set<TermEntity>().FromSql("CALL uspTermRead(@termId)", termParams).FirstOrDefaultAsync();

            return result;
        }

        #endregion Public Methods
    }
}
