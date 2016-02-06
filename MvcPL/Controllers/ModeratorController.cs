using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class ModeratorController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ICategoryService _categoryService;
        private readonly IRequestService _requestService;
        private readonly ILotRequestService _lotRequestService;
        private readonly ILotService _lotService;

        public ModeratorController(ISectionService sectionService, 
            ICategoryService categoryService, IRequestService requestService,
            ILotRequestService lotRequestService, ILotService lotService)
        {
            _sectionService = sectionService;
            _categoryService = categoryService;
            _requestService = requestService;
            _lotRequestService = lotRequestService;
            _lotService = lotService;
        }

        public ActionResult Sections()
        {
            var sections = _sectionService.GetAllSectionEntities()
                    .Where(s => s.ModeratorLogin == User.Identity.Name)
                    .Select(s => s.ToSectionDetailsModel()).ToList();
            return View(sections);
        }

        public ActionResult Categories(int id = 0)
        {
            var categories = _categoryService.GetAllCategoriesBySectionId(id).Select(c => c.ToCategoryDetailsViewModel()).ToList();
            ViewBag.SectionId = id;
            for (int i = 0; i < categories.Count; i ++)
            {
                var lots = _lotRequestService.GetAllLotRequestEntities()
                    .Where(r => r.CategoryRefId == categories[i].Id);
                categories[i].UnconfirmedLots = lots.Count();
                categories[i].TotalLotNumber =
                    _lotService.GetAllLotEntities().Count(l => l.CategoryRefId == categories[i].Id);
            }
            return View(categories.ToList());
        }

        public ActionResult CreateCategory(int id)
        {
            var viewModel = new CategoryCreateViewModel() {SectionRefId = id};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var bllCategory = new CategoryEntity()
                {
                    CategoryName = viewModel.CategoryName,
                    CreationDate = DateTime.Now,
                    IsBlocked = false,
                    IsConfirmed = false,
                    SectionRefId = viewModel.SectionRefId
                };
                if (User.IsInRole("Administrator"))
                {
                    bllCategory.IsConfirmed = true;
                }
                _categoryService.CreateCategory(bllCategory);
                if (!User.IsInRole("Administrator"))
                {
                    var request = new RequestEntity()
                    {
                        ToConfirm = true,
                        SectionRefId = bllCategory.SectionRefId
                    };
                    request.CategoryRefId =_categoryService.GetAllCategoryEntities()
                            .FirstOrDefault(c => c.CategoryName == bllCategory.CategoryName).Id;
                    _requestService.CreateRequest(request);
                }
                return RedirectToAction("Categories", "Moderator", new {id = viewModel.SectionRefId});
            }
            return View(viewModel);
        }
    }
}