using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("LeaseTerm")]
    public class LeaseTermEntity
    {
        #region Public Properties

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Key, Column("LeaseTermId")]
        public int LeaseTermId { get; set; }

        [Column("PropertyId")]
        public string PropertyId { get; set; }

        [Column("Rent")]
        public decimal Rent { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

        public List<LeasePeriodEntity> LeasePeriods { get; set; }

        #endregion Public Properties
    }
}
