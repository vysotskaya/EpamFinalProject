using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static partial class MvcMapper
    {
        public static LotEntity ToLotEntity(this LotCreateViewModel viewModel)
        {
            return new LotEntity()
            {
                Id = viewModel.Id,
                IsBlocked = false,
                CategoryName = viewModel.SettedCategoryName,
                Discription = viewModel.Discription,
                EndDate = viewModel.EndDate,
                LotName = viewModel.Name,
                SellerLogin = viewModel.SellerLogin,
                StartingBid = viewModel.StartingBid
            };
        }

        public static LotRowViewModel ToLotRowViewModel(this LotEntity lotEntity)
        {
            return new LotRowViewModel()
            {
                Id = lotEntity.Id,
                Name = lotEntity.LotName,
                EndDate = lotEntity.EndDate,
                StartingBid = lotEntity.StartingBid,
                SettedCategoryName = lotEntity.CategoryName,
                SellerLogin = lotEntity.SellerLogin,
                StartDate = lotEntity.StartDate,
                IsSold = lotEntity.IsSold
            };
        }

        public static LotDetailsViewModel ToLotDetailsViewModel(this LotEntity lotEntity)
        {
            return new LotDetailsViewModel()
            {
                Id = lotEntity.Id,
                BlockReason = lotEntity.BlockReason,
                CategoryRefId = lotEntity.CategoryRefId,
                Discription = lotEntity.Discription,
                EndDate = lotEntity.EndDate,
                IsBlocked = lotEntity.IsBlocked,
                IsConfirm = lotEntity.IsConfirm,
                IsSold = lotEntity.IsSold,
                Name = lotEntity.LotName,
                SellerEmail = lotEntity.SellerEmail,
                SellerLogin = lotEntity.SellerLogin,
                SellerRefId = lotEntity.SellerRefId,
                SettedCategoryName = lotEntity.CategoryName,
                StartDate = lotEntity.StartDate,
                StartingBid = lotEntity.StartingBid
            };
        }
    }
}