using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UserLoginViewModel
    {
        [Display(Name = "User login")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(30, ErrorMessage = "The login must contain 3-30 characters.", MinimumLength = 3)]
        public string Login { get; set; }

        [Display(Name = "User password")]
        [Required(ErrorMessage = "Enter your password.")]
        [StringLength(100, ErrorMessage = "The password must contain at least 8 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember the password?")]
        public bool RememberMe { get; set; }
    }
}