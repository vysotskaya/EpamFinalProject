using System;

namespace DAL.Interface.DTO
{
    public class DalCategory : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int SectionRefId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual DalSection Section { get; set; }
    }
}
