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
            AttachRoles(user.Roles);
            _dbContext.Set<User>().Add(user);
        }

        public void Update(DalUser entity)
        {
            var updatedUser = entity.ToUser();
            var existedUser = _dbContext.Entry<User>(_dbContext.Set<User>().Find(updatedUser.UserId));
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

            #region update

            //    var updated = entity.ToUser();
            //    //var notUpdated = _dbContext.Set<User>().Include("Roles").FirstOrDefault(u => u.UserId == entity.Id);
            //    var notUpdated = _dbContext.Set<User>().Include(u => u.Roles).SingleOrDefault(u => u.UserId == entity.Id);
            //    if (notUpdated !=null)
            //    {
            //        notUpdated.BlockTime = entity.BlockTime;
            //        notUpdated.IsBlocked = entity.IsBlocked;
            //        notUpdated.BlockReason = entity.BlockReason;
            //        //notUpdated.Roles = entity.Roles.ToRoleCollection();
            //        //AttachRoles(notUpdated.Roles);
            //        var addedRoles = updated.Roles.Except(notUpdated.Roles, new RoleEqualityComparer()).ToList<Role>();
            //        //5- Add new courses
            //        foreach (Role r in addedRoles)
            //        {
            //            /*6- Attach courses because it came from client 
            //            as detached state in disconnected scenario*/
            //            if (_dbContext.Entry(r).State == EntityState.Detached)
            //            {
            //                notUpdated.Roles.Add(r);
            //                var loadRole = _dbContext.Set<Role>().FirstOrDefault(role => role.RoleId == r.RoleId);
            //                _dbContext.Set<Role>().Attach(loadRole);
            //            }
            //            //7- Add course in existing student's course collection

            //        }
            //    }

            //_dbContext.Entry(notUpdated).State = EntityState.Modified;

            #endregion
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

        private void AttachRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                _dbContext.Set<Role>().Attach(role);
            }
        }
    }

    class RoleEqualityComparer : IEqualityComparer<Role>
    {
        public bool Equals(Role x, Role y)
        {
            return x.RoleId == y.RoleId;
        }

        public int GetHashCode(Role obj)
        {
            return obj.RoleId.GetHashCode();
        }
    }
}
