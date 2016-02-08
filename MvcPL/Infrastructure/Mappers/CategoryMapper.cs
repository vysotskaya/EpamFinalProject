using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static partial class MvcMapper
    {
        public static CategoryDetailsViewModel ToCategoryDetailsViewModel(this CategoryEntity entity)
        {
            var viewModel = new CategoryDetailsViewModel()
            {
                CreationDate = entity.CreationDate,
                CategoryName = entity.CategoryName,
                IsBlocked = entity.IsBlocked,
                Id = entity.Id,
                IsConfirmed = entity.IsConfirmed,
                SectionRefId = entity.SectionRefId,
            };
            //foreach (var category in entity.Categories)
            //{
            //    viewModel.Categories.Add(category.CategoryName);
            //}
            return viewModel;
        }

        public static CategoryEntity ToBllCategoryEntity(this CategoryDetailsViewModel viewModel)
        {
            return new CategoryEntity()
            {
                CategoryName = viewModel.CategoryName,
                Id = viewModel.Id,
                IsConfirmed = viewModel.IsConfirmed,
                SectionRefId = viewModel.SectionRefId,
                CreationDate = viewModel.CreationDate,
                IsBlocked = viewModel.IsBlocked
            };
        }
    }
}