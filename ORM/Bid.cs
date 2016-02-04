using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Bids")]
    public class Bid
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidId { get; set; }

        public int UserRefId { get; set; }
        public int LotRefId { get; set; }

        public virtual User User { get; set; }
        public virtual Lot Lot { get; set; }
    }
}
