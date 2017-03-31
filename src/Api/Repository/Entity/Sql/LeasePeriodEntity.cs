using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("LeasePeriod")]
    public class LeasePeriodEntity : SqlEntity
    {
        #region Public Properties

        public override int Id
        {
            get { return this.LeasePeriodId; }
            set { this.LeasePeriodId = value; }
        }

        [Key, Column("LeasePeriodId")]
        public int LeasePeriodId { get; set; }

        [Column("LeasePeriodStatusId")]
        public int LeasePeriodStatusId { get; set; }

        [ForeignKey("fkLeaseTermId"), Column("LeaseTermId")]
        public int LeaseTermId { get; set; }

        [Column("Period")]
        public string Period { get; set; }

        #endregion Public Properties
    }
}
