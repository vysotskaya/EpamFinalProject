using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Mappers;
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
            return _categoryRepository.GetAll().Select(c => c.ToBllCategory());
        }

        public CategoryEntity GetCategoryEntity(int id)
        {
            return _categoryRepository.GetById(id).ToBllCategory();
        }

        public IEnumerable<CategoryEntity> GetAllCategoriesBySectionId(int id)
        {
            return _categoryRepository.GetCategoriesBySectionId(id).Select(c => c.ToBllCategory());
        }

        public void CreateCategory(CategoryEntity entity)
        {
            _categoryRepository.Create(entity.ToDalCategory());
            _unitOfWork.Commit();
        }

        public void UpdateCategory(CategoryEntity entity)
        {
            _categoryRepository.Update(entity.ToDalCategory());
            _unitOfWork.Commit();
        }

        public void DeleteCategory(CategoryEntity entity)
        {
            _categoryRepository.Delete(entity.ToDalCategory());
            _unitOfWork.Commit();
        }
    }
}
