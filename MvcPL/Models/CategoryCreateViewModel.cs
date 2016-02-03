using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class CategoryCreateViewModel
    {
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(100, ErrorMessage = "The category name must contain 3-100 characters."), MinLength(3)]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "The discription must contain no more 500 characters.")]
        public string Discription { get; set; }

        public int SectionRefId { get; set; }
    }
}