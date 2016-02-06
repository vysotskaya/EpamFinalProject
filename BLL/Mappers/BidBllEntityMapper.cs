using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static DalBid ToDalBid(this BidEntity bidEntity)
        {
            return new DalBid()
            {
                Id = bidEntity.Id,
                UserRefId = bidEntity.UserRefId,
                LotRefId = bidEntity.LotRefId,
                LotName = bidEntity.LotName,
                UserLogin = bidEntity.UserLogin,
                BidTime = bidEntity.BidTime,
                BidAmount = bidEntity.BidAmount
            };
        }

        public static BidEntity ToBidEntity(this DalBid dalBid)
        {
            return new BidEntity()
            {
                Id = dalBid.Id,
                UserRefId = dalBid.UserRefId,
                LotRefId = dalBid.LotRefId,
                LotName = dalBid.LotName,
                UserLogin = dalBid.UserLogin,
                BidTime = dalBid.BidTime,
                BidAmount = dalBid.BidAmount
            };
        }
    }
}
