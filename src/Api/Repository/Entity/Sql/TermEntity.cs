using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("Term")]
    public class TermEntity
    {
        #region Public Properties

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Column("PropertyId")]
        public string PropertyId { get; set; }

        [Column("Rent")]
        public decimal Rent { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Key, Column("TermId")]
        public string TermId { get; set; }

        #endregion Public Properties
    }
}
