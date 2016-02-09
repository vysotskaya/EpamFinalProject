using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.InterfaceServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryEntity> GetAllCategoryEntities();
        CategoryEntity GetCategoryEntity(int id);
        IEnumerable<CategoryEntity> GetAllCategoriesBySectionId(int id);
        void CreateCategory(CategoryEntity entity);
        void UpdateCategory(CategoryEntity entity);
        void DeleteCategory(CategoryEntity entity);
        CategoryEntity GetByCategoryName(string name);
    }
}
