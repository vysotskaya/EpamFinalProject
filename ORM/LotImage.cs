using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Images")]
    public class LotImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        public Byte[] Content { get; set; }
        public int LotRefId { get; set; }

        public virtual Lot Lot { get; set; }
    }
}
