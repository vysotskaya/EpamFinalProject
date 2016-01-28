using System.Collections.Generic;
using BLL.Interface.Entities;


namespace BLL.Interface.Services
{
    public interface IUserService
    {
        IEnumerable<UserEntity> GetAllUserEntities();
        UserEntity GetUserEntity(int id);
        UserEntity GetUserEntityByLogin(string login);
        void CreateUser(UserEntity user);
        void UpdateUser(UserEntity user);
        void DeleteUser(UserEntity user);
    }
}
