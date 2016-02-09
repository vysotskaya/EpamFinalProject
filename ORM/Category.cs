using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public int SectionRefId { get; set; }

        [MaxLength(500)]
        public string Discription { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsConfirmed { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Request> Requests { get; set; } 
        public virtual ICollection<Lot> Lots { get; set; } 
    }
}
