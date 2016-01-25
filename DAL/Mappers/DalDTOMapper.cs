using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalDTOMapper
    {
        public static ICollection<DalRole> ToDalRoleCollection(this IQueryable<Role> roles)
        {
            var roleList = roles.Select(r => new DalRole()
            {
                Id = r.RoleId,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }

        public static ICollection<Role> ToRoleCollection(this ICollection<DalRole> roles)
        {
            var roleList = roles.Select(r => new Role()
            {
                RoleId = r.Id,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }
    }
}
