using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class MakeBidViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Make bid")]
        public double MakeBid { get; set; }

        public double CurrentBid { get; set; }
    }
}