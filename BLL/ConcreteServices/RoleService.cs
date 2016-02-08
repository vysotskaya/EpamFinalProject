using System;
using System.Collections.Generic;
using System.Linq;
using AuctionLog;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.ConcreteServices
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWorkuow;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWorkuow = unitOfWork;
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleEntity> GetAllRoleEntities()
        {
            try
            {
                return _roleRepository.GetAll().Select(role => role.ToBllRole());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<RoleEntity>();
            }
        }

        public RoleEntity GetRoleEntity(int id)
        {
            try
            {
                return _roleRepository.GetById(id).ToBllRole();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return null;
            }
        }

        public IEnumerable<RoleEntity> GetAllRolesByUserId(int id)
        {
            try
            {
                return _roleRepository.GetRolesByUserId(id).ToRoleEntityCollection();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<RoleEntity>();
            }
        }

        public void CreateRole(RoleEntity role)
        {
            try
            {
                _roleRepository.Create(role.ToDalRole());
                _unitOfWorkuow.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void UpdateRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }
    }
}
