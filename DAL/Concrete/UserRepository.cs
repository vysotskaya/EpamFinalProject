using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Interface.Visitor;
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
                    BlockReason = user.BlockReason,
                    BlockTime = user.BlockTime,
                    CreationDate = user.CreationDate,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                    Login = user.Login,
                    Id = user.UserId,
                    Password = user.Password,
                    Photo = user.Photo
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
            var dalUser = user ?. ToDalUser();
            if (dalUser == null)
            {
                return null;
            }
            dalUser.Roles = _roleRepository.GetRolesByUserId(dalUser.Id);
            return dalUser;
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> expression)
        {
            var visitor = new ParameterTypeVisitor<DalUser, User>(expression);
            var expr = visitor.Convert();
            var user = _dbContext.Set<User>().FirstOrDefault(expr);
            var dalUser = user?.ToDalUser();
            if (dalUser == null)
            {
                return null;
            }
            dalUser.Roles = _roleRepository.GetRolesByUserId(user.UserId);
            return dalUser;
        }

        public void Create(DalUser entity)
        {
            var user = entity.ToUser();
            user.Roles = user.Roles.Select(t => _dbContext.Set<Role>().Find(t.RoleId)).ToList();
            AttachRoles(user.Roles);
            _dbContext.Set<User>().Add(user);
        }

        public void Update(DalUser entity)
        {
            var updatedUser = entity.ToUser();
            var existedUser = _dbContext.Entry<User>(_dbContext.Set<User>().Find(updatedUser.UserId));
            if (existedUser == null)
            {
                return;
            }
            existedUser.State = EntityState.Modified;
            existedUser.Collection(u => u.Roles).Load();

            existedUser.Entity.Roles.Clear();

            foreach (Role role in updatedUser.Roles)
            {
                var loaded = _dbContext.Set<Role>().Find(role.RoleId);
                existedUser.Entity.Roles.Add(loaded);
            }

            existedUser.Entity.BlockReason = entity.BlockReason;
            existedUser.Entity.BlockTime = entity.BlockTime;
            existedUser.Entity.IsBlocked = entity.IsBlocked;
            existedUser.Entity.Photo = entity.Photo;
            existedUser.Entity.Login = entity.Login;
            existedUser.Entity.Email = entity.Email;
            existedUser.Entity.Password = entity.Password;
        }


        public void Delete(DalUser entity)
        {
            var user = entity.ToUser();
            user.Roles = _roleRepository.GetRolesByUserId(user.UserId).ToRoleCollection();
            user = _dbContext.Set<User>().Single(u => u.UserId == user.UserId);

            _dbContext.Set<User>().Remove(user);
        }

        public DalUser GetUserByLogin(string login)
        {
            var user = _dbContext.Set<User>().FirstOrDefault(u => u.Login == login);
            var dalUser = user?.ToDalUser();
            if (dalUser == null)
            {
                return null;
            }
            dalUser.Roles = _roleRepository.GetRolesByUserId(user.UserId);
            return dalUser;
        }
        
        private void AttachRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                var existedRole = _dbContext.Set<Role>().Find(role.RoleId);
                _dbContext.Set<Role>().Attach(existedRole);
            }
        }
    }
}
