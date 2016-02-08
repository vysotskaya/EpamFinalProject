using System;
using System.Collections.Generic;
using System.Linq;
using AuctionLog;
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
            try
            {
                return _userRepository.GetAll().Select(user => user?.ToBllUser());
            }
            catch (Exception e)
            {
                Log.LogError(e);
                return new List<UserEntity>();
            }
        }

        public UserEntity GetUserEntity(int id)
        {
            try
            {
                return _userRepository.GetById(id).ToBllUser();
            }
            catch (Exception e)
            {
                Log.LogError(e);
                return null;
            }
        }

        public UserEntity GetUserEntityByLogin(string login)
        {
            try
            {
                return _userRepository.GetUserByLogin(login)?.ToBllUser();
            }
            catch (Exception e)
            {
                Log.LogError(e);
                return null;
            }
        }

        public void DeleteUser(UserEntity user)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(UserEntity user)
        {
            try
            {
                _userRepository.Update(user.ToDalUser());
                _unitOfWorkuow.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }

        public void CreateUser(UserEntity user)
        {
            try
            {
                _userRepository.Create(user.ToDalUser());
                _unitOfWorkuow.Commit();
            }
            catch (Exception e)
            {
                Log.LogError(e);
            }
        }
    }
}
