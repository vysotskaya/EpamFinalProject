using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class LotController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ICategoryService _categoryService;
        private readonly ILotService _lotService;
        private readonly IUserService _userService;
        private readonly ILotRequestService _lotRequestService;

        public LotController(ICategoryService categoryService, ISectionService sectionService,
            ILotService lotService, IUserService userService, ILotRequestService lotRequestService)
        {
            _sectionService = sectionService;
            _categoryService = categoryService;
            _lotService = lotService;
            _userService = userService;
            _lotRequestService = lotRequestService;
        }

        [AllowAnonymous]
        [ActionName("Index")]
        public ActionResult AllLots()
        {
            return View();
        }

       
        public ActionResult CreateLot()
        {
            var sections = LoadSections();
            var lotViewModel = new LotCreateViewModel() {Sections = sections,
                Categories = LoadCategories(sections.First().Text)};
            return View(lotViewModel);
        }

        [HttpPost]
        public ActionResult CreateLot(LotCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var seller = _userService.GetUserEntityByLogin(User.Identity.Name);
                var section =
                    _sectionService.GetAllSectionEntities()
                        .FirstOrDefault(s => s.SectionName == viewModel.SettedSectionName);
                var category =
                    _categoryService.GetAllCategoryEntities()
                        .FirstOrDefault(c => c.CategoryName == viewModel.SettedCategoryName);
                var lot = viewModel.ToLotEntity();
                lot.BlockReason = String.Empty;
                lot.CategoryRefId = category.Id;
                lot.IsBlocked = false;
                lot.IsConfirm = false;
                lot.IsSold = false;
                lot.StartDate = DateTime.Now;
                lot.SellerLogin = seller.Login;
                lot.SellerEmail = seller.Email;
                lot.SellerRefId = seller.Id;

                int lotId = _lotService.CreateLot(lot);
                var lotRequest = new LotRequestEntity()
                {
                    CategoryRefId = category.Id,
                    LotRefId =lotId,
                    ToConfirm = true
                };
                _lotRequestService.CreateLotRequest(lotRequest);
                return RedirectToAction("Index", "Lot");
            }
            viewModel.Sections = LoadSections();
            viewModel.Categories = LoadCategories(viewModel.SettedSectionName);
            return View(viewModel);
        }

        public ActionResult CategoryInSectionPartial(string sectionName)
        {
            var lotViewModel = new LotCreateViewModel() {Categories = LoadCategories(sectionName)};
            return PartialView("_CategoryInSection", lotViewModel);
        }

        private IEnumerable<SelectListItem> LoadSections()
        {
            var sections = _sectionService.GetAllSectionEntities()
               .Where(s => !s.IsBlocked)
               .Select(s => new SelectListItem() { Value = s.SectionName, Text = s.SectionName });
            return sections;
        }

        private IEnumerable<SelectListItem> LoadCategories(string sectionName)
        {
            var section = _sectionService.GetAllSectionEntities()
                .FirstOrDefault(s => s.SectionName == sectionName);
            var categories = section.Categories.
                Select(c => new SelectListItem() { Value = c.CategoryName, Text = c.CategoryName });
            return categories;
        } 
    }
}