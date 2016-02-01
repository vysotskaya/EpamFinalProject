using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalDTOMapper
    {
        public static ICollection<DalRole> ToDalRoleCollection(this IEnumerable<Role> roles)
        {
            var roleList = roles.Select(r => new DalRole()
            {
                Id = r.RoleId,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }

        public static ICollection<Role> ToRoleCollection(this IEnumerable<DalRole> roles)
        {
            var roleList = roles.Select(r => new Role()
            {
                RoleId = r.Id,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }

        //public static DalUser ToDalUser(this User user)
        //{
        //    return new DalUser()
        //    {
        //        BlockReason = user.BlockReason,
        //        BlockTime = user.BlockTime,
        //        CreationDate = user.CreationDate,
        //        Email = user.Email,
        //        IsBlocked = user.IsBlocked,
        //        Login = user.Login,
        //        Id = user.UserId,
        //        Password = user.Password
        //    };
        //}

        public static User ToUser(this DalUser dalUser)
        {
            return new User()
            {
                BlockReason = dalUser.BlockReason,
                BlockTime = dalUser.BlockTime,
                CreationDate = dalUser.CreationDate,
                Email = dalUser.Email,
                IsBlocked = dalUser.IsBlocked,
                Login = dalUser.Login,
                UserId = dalUser.Id,
                Password = dalUser.Password,
                Roles = dalUser.Roles.ToRoleCollection()
            };
        }
    }
}
