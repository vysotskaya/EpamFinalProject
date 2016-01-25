using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public enum Role
    {
        Administrator = 1,
        Moderator,
        User,
        Guest
    }

    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User email")]
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}