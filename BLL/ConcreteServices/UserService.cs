using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.ConcreteServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWorkuow;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWorkuow = unitOfWork;
            _userRepository = userRepository;
        }


        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return _userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public UserEntity GetUserEntity(int id)
        {
            return _userRepository.GetById(id).ToBllUser();
        }

        public UserEntity GetUserEntityByLogin(string login)
        {
            var user = _userRepository.GetUserByLogin(login);
            if (user != null)
            {
                return user.ToBllUser();
            }
            return null;
        }

        public void DeleteUser(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void CreateUser(UserEntity user)
        {
            _userRepository.Create(user.ToDalUser());
            _unitOfWorkuow.Commit();
        }
    }
}
