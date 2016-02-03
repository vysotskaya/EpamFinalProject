namespace BLL.Interface.Entities
{
    public class RequestEntity
    {
        public int Id { get; set; }
        public int SectionRefId { get; set; }
        public int CategoryRefId { get; set; }
        public bool ToConfirm { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
    }
}
