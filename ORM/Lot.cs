using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Lots")]
    public class Lot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LotId { get; set; }

        [Required]
        [MaxLength(200)]
        public string LotName { get; set; }

        [Required]
        public double StartingBid { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CategoryRefId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Discription { get; set; }

        public int SellerRefId { get; set; }

        public bool IsBlocked { get; set; }

        [MaxLength(300)]
        public string BlockReason { get; set; }

        public bool IsConfirm { get; set; }
        public bool IsSold { get; set; }

        public virtual Category Category { get; set; }
        public virtual User Seller { get; set; }
        public virtual ICollection<LotRequest> LotRequests { get; set; }
        public virtual ICollection<Bid> Bids { get; set;} 
        public virtual ICollection<LotImage> Images { get; set;} 
    }
}
