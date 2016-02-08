using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static partial class MvcMapper
    {
        public static UserRegisterViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserRegisterViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Login = userEntity.Login,
                Password = userEntity.Password,
                Roles = userEntity.Roles.ToMvcRoleCollection(),
                SettedPhoto = userEntity.Photo
            };
        }

        public static UserEditViewModel ToMvcEditUserModel(this UserEntity userEntity)
        {
            return new UserEditViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Login = userEntity.Login,
                SettedPhoto = userEntity.Photo
            };
        }

        public static UserEntity ToBllUser(this UserRegisterViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                Email = userViewModel.Email,
                Login = userViewModel.Login,
                Password = userViewModel.Password,
                IsBlocked = false,
                Roles = userViewModel.Roles.ToRoleEntityCollection(),
                Photo = userViewModel.Photo != null ? Image.FromStream(userViewModel.Photo.InputStream) : null
            };
        }

        public static ICollection<Role> ToMvcRoleCollection(this ICollection<RoleEntity> roles)
        {
            var roleList = roles.Select(r => (Role)r.Id);
            return roleList.ToList();
        }

        public static ICollection<RoleEntity> ToRoleEntityCollection(this ICollection<Role> roles)
        {
            var roleList = roles.Select(r => new RoleEntity()
            {
                Id = (int)r,
                RoleName = r.ToString()
            });
            return roleList.ToList();
        }
    }
}