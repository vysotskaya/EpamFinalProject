using System;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public CustomRoleProvider(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            var user = _userService.GetUserEntityByLogin(login);

            if (user == null) return false;
            var userRoles = _roleService.GetAllRolesByUserId(user.Id);
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    if (role != null && role.RoleName == roleName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string[] GetRolesForUser(string login)
        {
            var roles = new string[] { };
            var user = _userService.GetUserEntityByLogin(login);

            if (user == null) return roles;
            var userRoles = _roleService.GetAllRolesByUserId(user.Id);
            int index = 0;
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    if (role != null)
                    {
                        roles[index++] = role.RoleName;
                    }
                }
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            var role = new RoleEntity() {RoleName = roleName};
            _roleService.CreateRole(role);
        }
    
        #region Stabs

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
#endregion
    }
}