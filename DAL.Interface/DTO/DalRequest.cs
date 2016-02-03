namespace DAL.Interface.DTO
{
    public class DalRequest : IEntity
    {
        public int Id { get; set; }
        public int SectionRefId { get; set; }
        public int CategoryRefId { get; set; }
        public bool IsConfirm { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
    }
}
