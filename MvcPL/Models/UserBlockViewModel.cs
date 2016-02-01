using System;
using System.ComponentModel.DataAnnotations;
using MvcPL.Attributes;

namespace MvcPL.Models
{
    public class UserBlockViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User status")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Block until")]
        [DateTimeValidation]
        [DataType(DataType.Date)]
        public DateTime BlockDate { get; set; }

        [Display(Name = "Block reason")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(200, ErrorMessage = "The reason must contain no more 200 characters.")]
        public string BlockReason { get; set; }
    }
}