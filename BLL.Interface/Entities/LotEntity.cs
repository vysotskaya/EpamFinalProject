using System;

namespace BLL.Interface.Entities
{
    public class LotEntity
    {
        public int Id { get; set; }
        public string LotName { get; set; }
        public double StartingBid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryRefId { get; set; }
        public string CategoryName { get; set; }
        public string Discription { get; set; }
        public int SellerRefId { get; set; }
        public string SellerLogin { get; set; }
        public string SellerEmail { get; set; }
        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsSold { get; set; }
    }
}
