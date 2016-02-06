using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static LotEntity ToLotEntity(this DalLot dalLot)
        {
            return new LotEntity()
            {
                Id = dalLot.Id,
                IsBlocked = dalLot.IsBlocked,
                CategoryName = dalLot.CategoryName,
                CategoryRefId = dalLot.CategoryRefId,
                IsConfirm = dalLot.IsConfirm,
                Discription = dalLot.Discription,
                BlockReason = dalLot.BlockReason,
                LotName = dalLot.LotName,
                SellerRefId = dalLot.SellerRefId,
                EndDate = dalLot.EndDate,
                IsSold = dalLot.IsSold,
                SellerEmail = dalLot.SellerEmail,
                SellerLogin = dalLot.SellerLogin,
                StartDate = dalLot.StartDate,
                StartingBid = dalLot.StartingBid
            };
        }

        public static DalLot ToDalLot(this LotEntity lotEntity)
        {
            return new DalLot()
            {
                Id = lotEntity.Id,
                IsBlocked = lotEntity.IsBlocked,
                CategoryName = lotEntity.CategoryName,
                CategoryRefId = lotEntity.CategoryRefId,
                IsConfirm = lotEntity.IsConfirm,
                Discription = lotEntity.Discription,
                BlockReason = lotEntity.BlockReason,
                LotName = lotEntity.LotName,
                SellerRefId = lotEntity.SellerRefId,
                EndDate = lotEntity.EndDate,
                IsSold = lotEntity.IsSold,
                SellerEmail = lotEntity.SellerEmail,
                SellerLogin = lotEntity.SellerLogin,
                StartDate = lotEntity.StartDate,
                StartingBid = lotEntity.StartingBid
            };
        }
    }
}
