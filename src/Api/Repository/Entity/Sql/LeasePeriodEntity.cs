using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("LeasePeriod")]
    public class LeasePeriodEntity
    {
        #region Public Properties

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
