using System.Collections.Generic;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace DAL.Interface.Repositories
{
    public interface ICategoryRepository : IRepository<DalCategory>
    {
        ICollection<DalCategory> GetCategoriesBySectionId(int id);
    }
}
