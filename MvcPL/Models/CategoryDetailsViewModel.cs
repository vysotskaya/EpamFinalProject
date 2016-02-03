using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class CategoryDetailsViewModel : CategoryCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Section status")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Confirm status")]
        public bool IsConfirmed { get; set; }

    }
}