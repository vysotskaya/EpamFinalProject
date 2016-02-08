using System;
using System.Collections.Generic;
using System.Linq;
using AuctionLog;
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
            try
            {
                return _categoryRepository.GetAll().Select(c => c.ToBllCategory());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<CategoryEntity>();
            }
        }

        public CategoryEntity GetCategoryEntity(int id)
        {
            try
            {
                return _categoryRepository.GetById(id).ToBllCategory();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return null;
            }
        }

        public IEnumerable<CategoryEntity> GetAllCategoriesBySectionId(int id)
        {
            try
            {
                return _categoryRepository.GetCategoriesBySectionId(id).Select(c => c.ToBllCategory());
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
                return new List<CategoryEntity>();
            }
        }

        public void CreateCategory(CategoryEntity entity)
        {
            try
            {
                _categoryRepository.Create(entity.ToDalCategory());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void UpdateCategory(CategoryEntity entity)
        {
            try
            {
                _categoryRepository.Update(entity.ToDalCategory());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }

        public void DeleteCategory(CategoryEntity entity)
        {
            try
            {
                _categoryRepository.Delete(entity.ToDalCategory());
                _unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                Log.LogError(exception);
            }
        }
    }
}
