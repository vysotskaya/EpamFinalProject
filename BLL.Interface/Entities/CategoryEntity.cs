using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int SectionRefId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual SectionEntity Section { get; set; }
    }
}
