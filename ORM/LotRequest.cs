//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ORM
//{
//    [Table("LotRequests")]
//    public class LotRequest
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int RequestId { get; set; }

//        public int CategoryRefId { get; set; }
//        public int LotRefId { get; set; }
//        public bool ToConfirm { get; set; }

//        public virtual Category Category { get; set; }
//        public virtual Lot Lot { get; set; }
//    }
//}
