using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static partial class DalDtoMapper
    {
        public static DalLot ToDalLot(this Lot lot)
        {
            return new DalLot()
            {
                Id = lot.LotId,
                IsBlocked = lot.IsBlocked,
                CategoryName = lot.Category.CategoryName,
                CategoryRefId = lot.CategoryRefId,
                IsConfirm = lot.IsConfirm,
                Discription = lot.Discription,
                BlockReason = lot.BlockReason,
                LotName = lot.LotName,
                SellerRefId = lot.SellerRefId,
                EndDate = lot.EndDate,
                IsSold = lot.IsSold,
                SellerEmail = lot.Seller.Email,
                SellerLogin = lot.Seller.Login,
                StartDate = lot.StartDate,
                StartingBid = lot.StartingBid
            };
        }

        public static Lot ToLot(this DalLot dalLot)
        {
            return new Lot()
            {
                LotId = dalLot.Id,
                IsBlocked = dalLot.IsBlocked,
                CategoryRefId = dalLot.CategoryRefId,
                IsConfirm = dalLot.IsConfirm,
                Discription = dalLot.Discription,
                BlockReason = dalLot.BlockReason,
                LotName = dalLot.LotName,
                SellerRefId = dalLot.SellerRefId,
                EndDate = dalLot.EndDate,
                IsSold = dalLot.IsSold,
                StartDate = dalLot.StartDate,
                StartingBid = dalLot.StartingBid
            };
        }
    }
}
