using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        ICollection<DalRole> GetRolesByUserId(int userId);
    }
}
