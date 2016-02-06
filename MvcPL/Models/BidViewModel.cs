using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class BidViewModel
    {
        public int Id { get; set; }
        public int UserRefId { get; set; }

        [Display(Name = "User login")]
        public string UserLogin { get; set; }
        public int LotRefId { get; set; }
        [Display(Name = "Bid amount")]
        public double BidAmount { get; set; }

        [Display(Name = "Bid time")]
        public DateTime BidTime { get; set; }
    }
}