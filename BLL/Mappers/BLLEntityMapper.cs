using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Login = userEntity.Login,
                Password = userEntity.Password,
                CreationDate = userEntity.CreationDate,
                IsBlocked = userEntity.IsBlocked,
                BlockTime = userEntity.BlockTime,
                BlockReason = userEntity.BlockReason,
                Roles = userEntity.Roles.ToDalRoleCollection(),
                Photo = userEntity.Photo.ImageToByteArray()
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Login = dalUser.Login,
                Password = dalUser.Password,
                CreationDate = dalUser.CreationDate,
                IsBlocked = dalUser.IsBlocked,
                BlockTime = dalUser.BlockTime,
                BlockReason = dalUser.BlockReason,
                Roles = dalUser.Roles.ToRoleEntityCollection(),
                Photo = dalUser.Photo.ByteArrayToImage()
            };
        }

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.Id,
                RoleName = roleEntity.RoleName
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            return new RoleEntity()
            {
                Id = dalRole.Id,
                RoleName = dalRole.RoleName
            };
        }

        public static ICollection<DalRole> ToDalRoleCollection(this ICollection<RoleEntity> roles)
        {
            var roleList = roles.Select(r => new DalRole()
            {
                Id = r.Id,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }

        public static ICollection<RoleEntity> ToRoleEntityCollection(this ICollection<DalRole> roles)
        {
            var roleList = roles.Select(r => new RoleEntity()
            {
                Id = r.Id,
                RoleName = r.RoleName
            });
            return roleList.ToList();
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
