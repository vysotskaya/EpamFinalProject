using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext _dbContext;

        public CategoryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DalCategory> GetAll()
        {
            return _dbContext.Set<Category>().Select(category => new DalCategory()
            {
                Id = category.CategoryId,
                CategoryName = category.CategoryName,
                CreationDate = category.CreationDate,
                IsBlocked = category.IsBlocked,
                IsConfirmed = category.IsConfirmed,
                SectionRefId = category.SectionRefId
            });
        }

        public DalCategory GetById(int key)
        {
            var category = _dbContext.Set<Category>().FirstOrDefault(c => c.CategoryId == key);
            return category.ToDalCategory();
        }

        public DalCategory GetByPredicate(Expression<Func<DalCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalCategory entity)
        {
            var category = entity.ToCategory();
            _dbContext.Set<Category>().Add(category);
        }

        public void Update(DalCategory entity)
        {
            var updatedCategory = entity.ToCategory();
            var existedCategory = _dbContext.Entry<Category>(_dbContext.Set<Category>().Find(updatedCategory.CategoryId));
            existedCategory.State = EntityState.Modified;
            existedCategory.Entity.IsBlocked = entity.IsBlocked;
            existedCategory.Entity.IsConfirmed = entity.IsConfirmed;
        }

        public void Delete(DalCategory entity)
        {
            var category = entity.ToCategory();
            category = _dbContext.Set<Category>().Single(c => c.CategoryId == category.CategoryId);
            _dbContext.Set<Category>().Remove(category);
        }

        public ICollection<DalCategory> GetCategoriesBySectionId(int id)
        {
            var categories = _dbContext.Set<Section>()
               .Where(s => s.SectionId == id)
               .SelectMany(s => s.Categories)
               .ToDalCategoryCollection();
            return categories;
        }
    }
}
