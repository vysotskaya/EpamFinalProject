using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static partial class BllEntityMapper
    {
        public static DalCategory ToDalCategory(this CategoryEntity entity)
        {
            return new DalCategory()
            {
                CategoryName = entity.CategoryName,
                CreationDate = entity.CreationDate,
                Id = entity.Id,
                IsBlocked = entity.IsBlocked,
                IsConfirmed = entity.IsConfirmed,
                SectionRefId = entity.SectionRefId
            };
        }

        public static CategoryEntity ToBllCategory(this DalCategory dalCategory)
        {
            return new CategoryEntity()
            {
                CategoryName = dalCategory.CategoryName,
                CreationDate = dalCategory.CreationDate,
                Id = dalCategory.Id,
                IsBlocked = dalCategory.IsBlocked,
                IsConfirmed = dalCategory.IsConfirmed,
                SectionRefId = dalCategory.SectionRefId
            };
        }

        public static ICollection<DalCategory> ToDalCategoryCollection(this ICollection<CategoryEntity> categories)
        {
            var categoryList = categories.Select(c => c.ToDalCategory());
            return categoryList.ToList();
        }

        public static ICollection<CategoryEntity> ToCategoryEntityCollection(this ICollection<DalCategory> categories)
        {
            var categoryList = categories.Select(c => c.ToBllCategory());
            return categoryList.ToList();
        }

        public static DalSection ToDalSection(this SectionEntity sectionEntity)
        {
            return new DalSection()
            {
                CreationDate = sectionEntity.CreationDate,
                Id = sectionEntity.Id,
                IsBlocked = sectionEntity.IsBlocked,
                SectionName = sectionEntity.SectionName,
                UserRefId = sectionEntity.UserRefId,
                ModeratorLogin = sectionEntity.ModeratorLogin,
                Discription = sectionEntity.Discription,
                Categories = sectionEntity.Categories.ToDalCategoryCollection()
            };
        }

        public static SectionEntity ToBllSection(this DalSection dalSection)
        {
            return new SectionEntity()
            {
                CreationDate = dalSection.CreationDate,
                Id = dalSection.Id,
                IsBlocked = dalSection.IsBlocked,
                SectionName = dalSection.SectionName,
                UserRefId = dalSection.UserRefId,
                ModeratorLogin = dalSection.ModeratorLogin,
                Discription = dalSection.Discription,
                Categories = dalSection.Categories.ToCategoryEntityCollection()
            };
        }
    }
}
