using System;
using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using DAL.Interface.Repositories;
using DAL.Interface.Repository;

namespace BLL.ConcreteServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
            
        public IEnumerable<CategoryEntity> GetAllCategoryEntities()
        {
            throw new NotImplementedException();
        }

        public CategoryEntity GetCategoryEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateCategory(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
