using OwnApt.Common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Repository.Entity.Sql
{
    [Table("RegisteredToken")]
    public class RegisteredTokenEntity : Equatable
    {
        #region Public Properties

        [Column("Token")]
        public string Token { get; set; }

        [Key, Column("TokenId")]
        public int TokenId { get; set; }

        #endregion Public Properties
    }
}
