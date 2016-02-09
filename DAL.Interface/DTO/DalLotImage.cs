using System;

namespace DAL.Interface.DTO
{
    public class DalLotImage : IEntity
    {
        public int Id { get; set; }
        public Byte[] Content { get; set; }
        public int LotRefId { get; set; }
    }
}
