using System;
using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalSection : IEntity
    {
        public DalSection()
        {
            Categories = new HashSet<DalCategory>();
        }
        public int Id { get; set; }
        public string SectionName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public int UserRefId { get; set; }
        public string ModeratorLogin { get; set; }
        public string Discription { get; set; }
        public int CategoriesCount { get; set; }

        public virtual ICollection<DalCategory> Categories { get; set; }
    }
}
