using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class SectionEntity
    {
        public SectionEntity()
        {
            Categories = new HashSet<CategoryEntity>();
        }
        public int Id { get; set; }
        public string SectionName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public int UserRefId { get; set; }

        public virtual ICollection<CategoryEntity> Categories { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
