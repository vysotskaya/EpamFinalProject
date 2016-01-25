using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Roles = userEntity.Roles.ToMvcRoleCollection()
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                Email = userViewModel.Email,
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