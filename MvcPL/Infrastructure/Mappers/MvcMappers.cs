using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserRegisterViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserRegisterViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Login = userEntity.Login,
                Password = userEntity.Password,
                Roles = userEntity.Roles.ToMvcRoleCollection()
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
                Roles = userViewModel.Roles.ToRoleEntityCollection()
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