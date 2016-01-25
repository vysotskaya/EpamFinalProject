using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Roles = userEntity.Roles.ToDalRoleCollection()
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Roles = dalUser.Roles.ToRoleEntityCollection()
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
    }
}
