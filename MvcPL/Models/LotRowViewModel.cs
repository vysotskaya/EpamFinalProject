using System;
using System.ComponentModel.DataAnnotations;
using MvcPL.Attributes;

namespace MvcPL.Models
{
    public class LotRowViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(200, ErrorMessage = "The name must contain no more 200 characters.")]
        public string Name { get; set; }

        [Display(Name = "Starting bid")]
        [Range(1, Double.MaxValue, ErrorMessage = "The bid must be more 1 BYR.")]
        [Required(ErrorMessage = "The field can not be empty.")]
        public double StartingBid { get; set; }

        [Display(Name = "End time")]
        [DateTimeValidation(ErrorMessage = "Incorrect end time.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "Start time")]
        public DateTime StartDate { get; set; }

        public string SettedCategoryName { get; set; }

        public string SellerLogin { get; set; }
        public bool IsSold { get; set; }
    }
}