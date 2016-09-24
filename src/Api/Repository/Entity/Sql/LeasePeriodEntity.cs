using OwnApt.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("LeasePeriod")]
    public class LeasePeriodEntity
    {
        [Key, Column("LeasePeriodId")]
        public int LeasePeriodId { get; set; }

        [ForeignKey("fkLeaseTermId"), Column("LeaseTermId")]
        public int LeaseTermId { get; set; }

        [Column("LeasePeriodStatusId")]
        public int LeasePeriodStatusId { get; set; }

        [Column("Period")]
        public string Period { get; set; }
    }
}
