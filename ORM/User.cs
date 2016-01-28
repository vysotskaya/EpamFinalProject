using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100), MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30), MinLength(3)]
        public string Login { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
         
    }
}
