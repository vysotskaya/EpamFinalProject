using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        private readonly IRoleRepository _roleRepository; 

        public UserRepository(DbContext dbContext, IRoleRepository roleRepository)
        {
            _dbContext = dbContext;
            _roleRepository = roleRepository;
        }

        public IEnumerable<DalUser> GetAll()
        {
            var users = _dbContext.Set<User>()
                .Select(user => new DalUser()
                {
                    Id = user.UserId,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                }).ToList();
            foreach (var user in users)
            {
                user.Roles = _roleRepository.GetRolesByUserId(user.Id);
            }
            return users;
        }

        public DalUser GetById(int key)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(u => u.UserId == key);
            return new DalUser()
            {
                Id = user.UserId,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                Roles = _roleRepository.GetRolesByUserId(user.UserId)   
            };
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalUser entity)
        {
            var user = new User()
            {
                Email = entity.Email,
                Login = entity.Login,
                Password = entity.Password,
                Roles = entity.Roles.ToRoleCollection()
            };
            foreach (var role in user.Roles)
            {
                _dbContext.Set<Role>().Attach(role);
            }
            _dbContext.Set<User>().Add(user);
            
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalUser entity)
        {
            var user = new User()
            {
                UserId = entity.Id,
                Email = entity.Email,
                Roles = entity.Roles.ToRoleCollection()
            };
            user = _dbContext.Set<User>().Single(u => u.UserId == user.UserId);
            _dbContext.Set<User>().Remove(user);
        }

        public DalUser GetUserByLogin(string login)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                return new DalUser()
                {
                    Id = user.UserId,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                    Roles = _roleRepository.GetRolesByUserId(user.UserId)
                };
            }
            return null;
        }
    }
}
