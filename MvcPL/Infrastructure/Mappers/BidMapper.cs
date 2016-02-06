using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static partial class MvcMapper
    {
        public static BidViewModel ToBidViewModel(this BidEntity bidEntity)
        {
            return new BidViewModel()
            {
                Id = bidEntity.Id,
                UserRefId = bidEntity.UserRefId,
                BidTime = bidEntity.BidTime,
                BidAmount = bidEntity.BidAmount,
                LotRefId = bidEntity.LotRefId,
                UserLogin = bidEntity.UserLogin
            };
        }
    }
}