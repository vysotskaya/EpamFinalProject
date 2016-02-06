using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static partial class DalDtoMapper
    {
        public static DalBid ToDalBid(this Bid bid)
        {
            return new DalBid()
            {
                Id = bid.BidId,
                UserRefId = bid.UserRefId,
                LotRefId = bid.LotRefId,
                LotName = bid.Lot.LotName,
                UserLogin = bid.User.Login
            };
        }

        public static Bid ToBid(this DalBid dalBid)
        {
            return new Bid()
            {
                BidId = dalBid.Id,
                LotRefId = dalBid.LotRefId,
                UserRefId = dalBid.UserRefId
            };
        }
    }
}
