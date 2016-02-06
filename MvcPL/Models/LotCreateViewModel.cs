using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcPL.Attributes;

namespace MvcPL.Models
{
    public class LotCreateViewModel
    {
        public LotCreateViewModel()
        {
            Sections = new HashSet<SelectListItem>();
            Categories = new HashSet<SelectListItem>();
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(200, ErrorMessage = "The name must contain no more 200 characters.")]
        public string Name { get; set; }

        [Display(Name = "Discription (no more 4000 characters)")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(4000, ErrorMessage = "The discription must contain no more 4000 characters.")]
        public string Discription { get; set; }

        [Display(Name = "Starting bid")]
        [Range(1, Double.MaxValue, ErrorMessage = "The bid must be more 1 BYR.")]
        [Required(ErrorMessage = "The field can not be empty.")]
        public double StartingBid { get; set; }

        [Display(Name = "End time")]
        [DateTimeValidation(ErrorMessage = "Incorrect end time.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Choose section")]
        public IEnumerable<SelectListItem> Sections { get; set; }
        public string SettedSectionName { get; set; }

        [Display(Name = "Choose category")]
        public IEnumerable<SelectListItem> Categories { get; set; }
        public string SettedCategoryName { get; set; }

        public string SellerLogin { get; set; }
    }
}