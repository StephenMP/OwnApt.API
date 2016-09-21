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

        public DbSet<LeaseTermEntity> LeaseTerm { get; set; }

        #endregion Public Properties

        #region Public Methods

        public async Task<LeaseTermEntity> UspLeaseTermReadAsync(string id)
        {
            var paramters = new object[]
            {
                new MySqlParameter("leaseTermId", id)
            };

            var result = await this.Set<LeaseTermEntity>().FromSql("CALL uspLeaseTermRead(@leaseTermId)", paramters).FirstOrDefaultAsync();

            return result;
        }

        #endregion Public Methods
    }
}
