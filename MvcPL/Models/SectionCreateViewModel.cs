using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;

namespace MvcPL.Models
{
    public class SectionCreateViewModel
    {
        public SectionCreateViewModel()
        {
            UsersLogin = new HashSet<SelectListItem>();
        }

        public int Id { get; set; }

        [Display(Name = "Section name")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(100, ErrorMessage = "The section name must contain 3-100 characters."), MinLength(3)]
        [Remote("IsSectionNameAvailable", "Validate", AdditionalFields = "Id", ErrorMessage = "This name has already exist.")]
        public string SectionName { get; set; }

        [StringLength(500, ErrorMessage = "The discription must contain no more 500 characters.")]
        public string Discription { get; set; }

        [Display(Name = "Set moderator")]
        public IEnumerable<SelectListItem> UsersLogin { get; set; }

        [Display(Name = "Moderator login")]
        public string SettedModeratorLogin { get; set; }

        public int UserRefId { get; set; }

    }
}