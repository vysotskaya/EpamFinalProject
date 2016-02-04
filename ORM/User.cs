using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace ORM
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Sections = new HashSet<Section>();
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

        public DateTime CreationDate { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime BlockTime { get; set; }

        [MaxLength(300)]
        public string BlockReason { get; set; } 

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Section> Sections { get; set; } 
        public virtual ICollection<Lot> Lots { get; set; } 
        public virtual ICollection<Bid> Bids { get; set; } 
    }
}
