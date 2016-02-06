namespace DAL.Interface.DTO
{
    public class DalLotRequest : IEntity
    {
        public int Id { get; set; }
        public int CategoryRefId { get; set; }
        public string CategoryName { get; set; }
        public int LotRefId { get; set; }
        public string LotName { get; set; }
        public bool ToConfirm { get; set; }
    }
}
