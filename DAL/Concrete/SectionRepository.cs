using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class SectionRepository : ISectionReposytory
    {
        private readonly DbContext _dbContext;
        private readonly ICategoryRepository _categoryRepository;

        public SectionRepository(DbContext dbContext, ICategoryRepository categoryRepository)
        {
            _dbContext = dbContext;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<DalSection> GetAll()
        {
            var sections = _dbContext.Set<Section>()
                .Select(section => new DalSection()
                {
                    Id = section.SectionId,
                    CreationDate = section.CreationDate,
                    IsBlocked = section.IsBlocked,
                    SectionName = section.SectionName,
                    UserRefId = section.UserRefId,
                    ModeratorLogin = section.User.Login
                }).ToList();
            //User 
            foreach (var section in sections)
            {
                section.Categories = _categoryRepository.GetCategoriesBySectionId(section.Id);
            }
            return sections;
        }

        public DalSection GetById(int key)
        {
            var section = _dbContext.Set<Section>().FirstOrDefault(s => s.SectionId == key);
            var dalSection = ToDalSection(section);
            dalSection.Categories = _categoryRepository.GetCategoriesBySectionId(section.SectionId);
            return dalSection;
        }

        public DalSection GetByPredicate(Expression<Func<DalSection, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(DalSection entity)
        {
            var section = entity.ToSection();
            section.SectionId = 0;
            _dbContext.Set<Section>().Add(section);
        }

        public void Update(DalSection entity)
        {
            var notUpdated = _dbContext.Set<Section>().FirstOrDefault(s => s.SectionId == entity.Id);
            if (notUpdated != null)
            {
                notUpdated.IsBlocked = entity.IsBlocked;
                notUpdated.UserRefId = entity.UserRefId;
                notUpdated.Categories = entity.Categories.ToCategoryCollection();
            }
            _dbContext.Entry(notUpdated).State = EntityState.Modified;
        }

        public void Delete(DalSection entity)
        {
            var section = entity.ToSection();
            section.Categories = _categoryRepository.GetCategoriesBySectionId(entity.Id).ToCategoryCollection();
            section = _dbContext.Set<Section>().Single(s => s.SectionId == section.SectionId);
            _dbContext.Set<Section>().Remove(section);
        }

        private DalSection ToDalSection(Section section)
        {
            return new DalSection()
            {
                CreationDate = section.CreationDate,
                Id = section.SectionId,
                IsBlocked = section.IsBlocked,
                SectionName = section.SectionName,
                UserRefId = section.UserRefId
            };
        }
    }
}
