using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    [Table("Requests")]
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        public int SectionRefId { get; set; }
        public int CategoryRefId { get; set; }
        public bool ToConfirm { get; set; }

        public virtual Section Section { get; set; }
        public virtual Category Category { get; set; }
    }
}
