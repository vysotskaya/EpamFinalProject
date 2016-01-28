using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface IRoleService
    {
        IEnumerable<RoleEntity> GetAllRoleEntities();
        RoleEntity GetRoleEntity(int id);
        IEnumerable<RoleEntity> GetAllRolesByUserId(int id);
        void CreateRole(RoleEntity role);
        void UpdateRole(RoleEntity role);
        void DeleteRole(RoleEntity role);
    }
}
