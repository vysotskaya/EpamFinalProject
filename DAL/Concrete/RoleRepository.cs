using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _dbContext;

        public RoleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _dbContext.Set<Role>().Select(role => new DalRole()
            {
                Id = role.RoleId,
                RoleName = role.RoleName
            });
        }

        public ICollection<DalRole> GetRolesByUserId(int userId)
        {
            return _dbContext.Set<Role>()
                .Where(role => role.Users.Any(user => user.UserId == userId))
                .Select(role => new DalRole()
                {
                    Id = role.RoleId,
                    RoleName = role.RoleName
                }).ToList();
        } 

        public DalRole GetById(int key)
        {
            var role = _dbContext.Set<Role>().FirstOrDefault(r => r.RoleId == key);
            return new DalRole()
            {
                Id = role.RoleId,
                RoleName = role.RoleName
            };
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalRole entity)
        {
            var role = new Role()
            {
                RoleId = entity.Id,
                RoleName = entity.RoleName
            };
            _dbContext.Set<Role>().Add(role);
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole entity)
        {
            var role = new Role()
            {
                RoleId = entity.Id,
                RoleName = entity.RoleName
            };
            role = _dbContext.Set<Role>().Single(r => r.RoleId == role.RoleId);
            _dbContext.Set<Role>().Remove(role);
        }
    }
}
