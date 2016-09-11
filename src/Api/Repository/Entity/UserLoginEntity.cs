using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnApt.Api.Repository.Entity
{
    [Table("UserLogin")]
    public class UserLoginEntity
    {
        #region Properties

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Key, Column("UserId")]
        public string UserId { get; set; }

        #endregion Properties
    }
}
