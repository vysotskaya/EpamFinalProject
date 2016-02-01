using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalDTOMapper
    {
        public static ICollection<DalRole> ToDalRoleCollection(this IEnumerable<Role> roles)
        {
            var roleList = roles.Select(r => new DalRole()
            {
                Id = r.RoleId,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }

        public static ICollection<Role> ToRoleCollection(this IEnumerable<DalRole> roles)
        {
            var roleList = roles.Select(r => new Role()
            {
                RoleId = r.Id,
                RoleName = r.RoleName
            });
            return roleList.ToList();
        }

        public static ICollection<DalCategory> ToDalCategoryCollection(this IEnumerable<Category> categories)
        {
            var categoryList = categories.Select(c => new DalCategory()
            {
                Id = c.CategoryId,
                CategoryName = c.CategoryName,
                CreationDate = c.CreationDate,
                IsBlocked = c.IsBlocked,
                IsConfirmed = c.IsConfirmed,
                SectionRefId = c.SectionRefId
            });
            return categoryList.ToList();
        }

        public static ICollection<Category> ToCategoryCollection(this IEnumerable<DalCategory> categories)
        {
            var categoryList = categories.Select(c => new Category()
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName,
                CreationDate = c.CreationDate,
                IsBlocked = c.IsBlocked,
                IsConfirmed = c.IsConfirmed,
                SectionRefId = c.SectionRefId
            });
            return categoryList.ToList();
        }

        //public static DalUser ToDalUser(this User user)
        //{
        //    return new DalUser()
        //    {
        //        BlockReason = user.BlockReason,
        //        BlockTime = user.BlockTime,
        //        CreationDate = user.CreationDate,
        //        Email = user.Email,
        //        IsBlocked = user.IsBlocked,
        //        Login = user.Login,
        //        Id = user.UserId,
        //        Password = user.Password
        //    };
        //}

        public static User ToUser(this DalUser dalUser)
        {
            return new User()
            {
                BlockReason = dalUser.BlockReason,
                BlockTime = dalUser.BlockTime,
                CreationDate = dalUser.CreationDate,
                Email = dalUser.Email,
                IsBlocked = dalUser.IsBlocked,
                Login = dalUser.Login,
                UserId = dalUser.Id,
                Password = dalUser.Password,
                Roles = dalUser.Roles.ToRoleCollection()
            };
        }

        public static Section ToSection(this DalSection dalSection)
        {
            return new Section()
            {
                CreationDate = dalSection.CreationDate,
                SectionId = dalSection.Id,
                IsBlocked = dalSection.IsBlocked,
                SectionName = dalSection.SectionName,
                UserRefId = dalSection.UserRefId,
                Categories = dalSection.Categories.ToCategoryCollection()
            };
        }

        public static Category ToCategory(this DalCategory dalCategory)
        {
            return new Category()
            {
                CategoryId = dalCategory.Id,
                CategoryName = dalCategory.CategoryName,
                CreationDate = dalCategory.CreationDate,
                IsBlocked = dalCategory.IsBlocked,
                IsConfirmed = dalCategory.IsConfirmed,
                SectionRefId = dalCategory.SectionRefId
            };
        }

        public static DalCategory ToDalCategory(this Category category)
        {
            return new DalCategory()
            {
                Id = category.CategoryId,
                CategoryName = category.CategoryName,
                CreationDate = category.CreationDate,
                IsBlocked = category.IsBlocked,
                IsConfirmed = category.IsConfirmed,
                SectionRefId = category.SectionRefId
            };
        }
    }
}
