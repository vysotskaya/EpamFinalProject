using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class PasswordChangeViewModel
    {
        [Display(Name = "New password")]
        [Required(ErrorMessage = "Enter your password.")]
        [StringLength(100, ErrorMessage = "The password must contain at least 8 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the new password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the new password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter old password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }
    }
}