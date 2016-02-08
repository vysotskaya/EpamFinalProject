using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UserDetailsViewModel : UserBlockViewModel
    {
        [Display(Name = "User E-mail")]
        public string Email { get; set; }

        [Display(Name = "User login")]
        public string Login { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "User roles")]
        public ICollection<Role> Roles { get; set; }

        public Image Photo { get; set; }
    }
}