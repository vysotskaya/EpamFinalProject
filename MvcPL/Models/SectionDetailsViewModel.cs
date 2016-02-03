using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class SectionDetailsViewModel : SectionCreateViewModel
    {
        public SectionDetailsViewModel()
        {
            Categories = new HashSet<string>();
        }

        public int Id { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Section status")]
        public bool IsBlocked { get; set; }

        public ICollection<string> Categories { get; set; }
    }
}