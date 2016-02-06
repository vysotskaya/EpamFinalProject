using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcPL.Attributes;

namespace MvcPL.Models
{
    public class LotCreateViewModel : LotRowViewModel
    {
        public LotCreateViewModel()
        {
            Sections = new HashSet<SelectListItem>();
            Categories = new HashSet<SelectListItem>();
        }

        [Display(Name = "Discription (no more 4000 characters)")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(4000, ErrorMessage = "The discription must contain no more 4000 characters.")]
        public string Discription { get; set; }

        [Display(Name = "Choose section")]
        public IEnumerable<SelectListItem> Sections { get; set; }
        public string SettedSectionName { get; set; }

        [Display(Name = "Choose category")]
        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}