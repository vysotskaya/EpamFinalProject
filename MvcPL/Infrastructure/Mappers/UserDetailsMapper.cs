using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                Roles = userEntity.Roles.ToMvcRoleCollection(),
                Photo = userEntity.Photo,
                BlockReason = userEntity.BlockReason
            };
        }

        public static Byte[] ImageToByteArray(this Image imageIn)
        {
            if (imageIn != null)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            return null;
        }

        public static Image ByteArrayToImage(this Byte[] byteArrayIn)
        {
            if (byteArrayIn != null)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            return null;
        }
    }
}