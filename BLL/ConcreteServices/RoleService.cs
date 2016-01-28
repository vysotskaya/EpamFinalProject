using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return _roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        public RoleEntity GetRoleEntity(int id)
        {
            return _roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllRolesByUserId(int id)
        {
            return _roleRepository.GetRolesByUserId(id).ToRoleEntityCollection();
        }

        public void CreateRole(RoleEntity role)
        {
            _roleRepository.Create(role.ToDalRole());
            _unitOfWorkuow.Commit();
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
