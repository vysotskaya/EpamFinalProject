using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    [Table("Sections")]
    public class Section
    {
        public Section()
        {
            Categories = new HashSet<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionId { get; set; }

        [Required]
        public string SectionName { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsBlocked { get; set; }

        public int UserRefId { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual User User { get; set; }
    }
}
