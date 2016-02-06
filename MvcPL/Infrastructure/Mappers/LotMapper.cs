using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class LotMapper
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
    }
}