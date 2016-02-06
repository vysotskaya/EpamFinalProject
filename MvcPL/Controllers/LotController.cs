using System;
using System.Collections;
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
        private readonly IBidService _bidService;

        public LotController(ICategoryService categoryService, ISectionService sectionService,
            ILotService lotService, IUserService userService, ILotRequestService lotRequestService,
            IBidService bidService)
        {
            _sectionService = sectionService;
            _categoryService = categoryService;
            _lotService = lotService;
            _userService = userService;
            _lotRequestService = lotRequestService;
            _bidService = bidService;
        }

        [AllowAnonymous]
        [ActionName("Index")]
        public ActionResult AllLots()
        {
            ViewBag.Sections = _sectionService.GetAllSectionEntities().Select(s => s.ToSectionDetailsModel());
            var lots = _lotService.GetAllLotEntities().Where(l => l.IsConfirm).Select(l => l.ToLotRowViewModel()).OrderByDescending(lot => lot.StartDate);
            return View(lots);
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

        public ActionResult LotRows(string sectionName = null, string categoryName = null)
        {
            IList<LotRowViewModel> lots = new List<LotRowViewModel>();
            if (categoryName == null)
            {
                SectionEntity section = _sectionService.GetAllSectionEntities().FirstOrDefault(s => s.SectionName == sectionName);
                foreach (var category in section.Categories)
                {
                    var lotsTemp = _lotService.GetAllLotEntities()
                        .Where(l => l.CategoryRefId == category.Id).ToList();
                    foreach (var l in lotsTemp)
                    {
                        lots.Add(l.ToLotRowViewModel());
                    }
                }
            }
            else
            {
                lots = _lotService.GetAllLotEntities().Where(l => l.CategoryName == categoryName)
                        .Select(l => l.ToLotRowViewModel())
                        .ToList();
            }
            return PartialView("_LotRows", lots);
        }

        public ActionResult LotDetails(int id)
        {
            var lot = _lotService.GetLotEntity(id).ToLotDetailsViewModel();
            var bids = _bidService.GetAllBidEntities().Where(b => b.LotRefId == lot.Id).Select(b => b.ToBidViewModel()).ToList();
            if (bids.Count != 0)
            {
                lot.Bids = bids;
                lot.CurrentBid = bids.Max(b => b.BidAmount);
            }
            else
            {
                lot.CurrentBid = lot.StartingBid;
            }
            return View(lot);
        }


        public ActionResult ModeratorUnconfirmedLots(int categoryId = 0)
        {
            var lots = _lotRequestService.GetAllLotRequestEntities()
                    .Where(r => r.CategoryRefId == categoryId).ToList();
            var lotRowView = new List<LotRowViewModel>();
            int idx = 0;
            foreach (var lot in lots)
            {
                lotRowView.Add(_lotService.GetLotEntity(lot.LotRefId).ToLotRowViewModel());
            }
            return View(lotRowView);
        }

        public ActionResult MakeBid(BidViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
            }
            return RedirectToAction("Index", viewModel.LotRefId);
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