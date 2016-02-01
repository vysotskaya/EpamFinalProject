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
                    BlockReason = user.BlockReason,
                    BlockTime = user.BlockTime,
                    CreationDate = user.CreationDate,
                    Email = user.Email,
                    IsBlocked = user.IsBlocked,
                    Login = user.Login,
                    Id = user.UserId,
                    Password = user.Password
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
            var dalUser = ToDalUser(user);
            dalUser.Roles = _roleRepository.GetRolesByUserId(dalUser.Id);
            return dalUser;
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalUser entity)
        {
            var user = entity.ToUser();
            user.UserId = 0;
            foreach (var role in user.Roles)
            {
                _dbContext.Set<Role>().Attach(role);
            }
            _dbContext.Set<User>().Add(user);
        }

        public void Update(DalUser entity)
        {
            var notUpdated = _dbContext.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
            if (notUpdated !=null)
            {
                notUpdated.BlockTime = entity.BlockTime;
                notUpdated.IsBlocked = entity.IsBlocked;
                notUpdated.BlockReason = entity.BlockReason;
            }
            _dbContext.Entry(notUpdated).State = EntityState.Modified;
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
            if (user != null)
            {
                var dalUser = ToDalUser(user);
                dalUser.Roles = _roleRepository.GetRolesByUserId(user.UserId);
                return dalUser;
            }
            return null;
        }

        private DalUser ToDalUser(User user)
        {
            return new DalUser()
            {
                BlockReason = user.BlockReason,
                BlockTime = user.BlockTime,
                CreationDate = user.CreationDate,
                Email = user.Email,
                IsBlocked = user.IsBlocked,
                Login = user.Login,
                Id = user.UserId,
                Password = user.Password
            };
        }
    }
}
