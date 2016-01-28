﻿using System.Collections.Generic;
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

    public class UserRegisterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User E-mail")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect E-mail.")]
        [StringLength(200, ErrorMessage = "The email must contain no more 200 characters.")]
        public string Email { get; set; }

        [Display(Name = "User password")]
        [Required(ErrorMessage = "Enter your password.")]
        [StringLength(100, ErrorMessage = "The password must contain at least 8 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User login")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]", ErrorMessage = "Incorrect login.")]
        [StringLength(30, ErrorMessage = "The login must contain 3-30 characters.", MinimumLength = 3)]
        public string Login { get; set; }

        [Display(Name = "User roles")]
        public ICollection<Role> Roles { get; set; }
    }
}