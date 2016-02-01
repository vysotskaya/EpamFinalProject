using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static partial class MvcMapper
    {
        public static void ToBllUserBlockInfo(this UserEntity userEntity, UserBlockViewModel viewModel)
        {
            if (viewModel != null)
            {
                userEntity.BlockTime = viewModel.BlockDate;
                userEntity.IsBlocked = viewModel.IsBlocked;
                userEntity.BlockReason = viewModel.BlockReason;
            }
        }

        public static UserDetailsViewModel ToUserDetailsModel(this UserEntity userEntity)
        {
            return new UserDetailsViewModel()
            {
                BlockDate = userEntity.BlockTime,
                Email = userEntity.Email,
                Id = userEntity.Id,
                IsBlocked = userEntity.IsBlocked,
                Login = userEntity.Login,
                CreationDate = userEntity.CreationDate,
                Roles = userEntity.Roles.ToMvcRoleCollection()
            };
        }
    }
}