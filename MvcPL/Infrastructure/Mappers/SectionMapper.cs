using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static partial class MvcMapper
    {
        public static SectionDetailsViewModel ToSectionDetailsModel(this SectionEntity entity)
        {
            var viewModel = new SectionDetailsViewModel()
            {
                CreationDate = entity.CreationDate,
                IsBlocked = entity.IsBlocked,
                Id = entity.Id,
                SectionName = entity.SectionName,
                UserRefId = entity.UserRefId,
                SettedModeratorLogin = entity.ModeratorLogin
            };
            foreach (var category in entity.Categories)
            {
                viewModel.Categories.Add(category.CategoryName);
            }
            return viewModel;
        }

        public static SectionEntity ToBllSectionEntity(this SectionCreateViewModel viewModel)
        {
            return new SectionEntity()
            {
                CreationDate = DateTime.Now,
                UserRefId = viewModel.UserRefId,
                IsBlocked = true,
                SectionName = viewModel.SectionName
            };
        }
    }
}