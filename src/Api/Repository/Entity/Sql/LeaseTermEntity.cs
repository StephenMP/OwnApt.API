using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("LeaseTerm")]
    public class LeaseTermEntity : SqlEntity
    {
        public LeaseTermEntity()
        {
            this.LeasePeriods = new List<LeasePeriodEntity>();
        }

        #region Public Properties

        [Column("Active")]
        public bool Active { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        public override int Id => LeaseTermId;

        public List<LeasePeriodEntity> LeasePeriods { get; set; }

        [Key, Column("LeaseTermId")]
        public int LeaseTermId { get; set; }

        [Column("PropertyId")]
        public string PropertyId { get; set; }

        [Column("Rent")]
        public decimal Rent { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        #endregion Public Properties
    }
}
