using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;
using MvcPL.Attributes;
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
            var lots = _lotService.GetAllLotEntities().Where(l => l.IsConfirm && !l.IsBlocked)
                .Select(l => l.ToLotRowViewModel()).OrderByDescending(lot => lot.StartDate);
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
        public ActionResult CreateLot(LotCreateViewModel viewModel, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                var seller = _userService.GetUserEntityByLogin(User.Identity.Name);
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

        public ActionResult EditLot(int id)
        {
            var lot = _lotService.GetLotEntity(id);
            var sectionId = _categoryService.GetCategoryEntity(lot.CategoryRefId).SectionRefId;
            var sections = LoadSections();
            var section =
                _sectionService.GetSectionEntity(_categoryService.GetCategoryEntity(lot.CategoryRefId).SectionRefId);
            var lotViewModel = new LotCreateViewModel()
            {
                Sections = sections,
                Categories = LoadCategories(section.SectionName),
                Id = lot.Id,
                Name = lot.LotName,
                SettedSectionName = _sectionService.GetSectionEntity(sectionId).SectionName,
                Discription = lot.Discription,
                EndDate = lot.EndDate,
                SettedCategoryName = lot.CategoryName,
                StartingBid = lot.StartingBid
            };
            return View(lotViewModel);
        }

        [HttpPost]
        public ActionResult EditLot(LotCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lot = _lotService.GetLotEntity(viewModel.Id);
                var category =
                    _categoryService.GetAllCategoryEntities()
                        .FirstOrDefault(c => c.CategoryName == viewModel.SettedCategoryName);
                lot.CategoryRefId = category.Id;
                lot.LotName = viewModel.Name;
                lot.Discription = viewModel.Discription;
                lot.EndDate = viewModel.EndDate;
                lot.IsConfirm = false;
                _lotService.UpdateLot(lot);
                return RedirectToAction("LotDetails", "Lot", new { id = viewModel.Id });
            }
            return View(viewModel);

        }

        public ActionResult CategoryInSectionPartial(string sectionName)
        {
            var lotViewModel = new LotCreateViewModel() {Categories = LoadCategories(sectionName)};
            return PartialView("_CategoryInSection", lotViewModel);
        }

        [AllowAnonymous]
        public ActionResult LotRows(string sectionName = null, string categoryName = null)
        {
            IList<LotRowViewModel> lots = new List<LotRowViewModel>();
            if (categoryName == null)
            {
                SectionEntity section = _sectionService.GetAllSectionEntities().FirstOrDefault(s => s.SectionName == sectionName);
                foreach (var category in section.Categories)
                {
                    var lotsTemp = _lotService.GetAllLotEntities()
                        .Where(l => l.CategoryRefId == category.Id).Where(l => !l.IsBlocked && l.IsConfirm).ToList();
                    foreach (var l in lotsTemp)
                    {
                        lots.Add(l.ToLotRowViewModel());
                    }
                }
            }
            else
            {
                lots = _lotService.GetAllLotEntities().Where(l => l.CategoryName == categoryName && !l.IsBlocked && l.IsConfirm)
                        .Select(l => l.ToLotRowViewModel())
                        .ToList();
            }
            return PartialView("_LotRows", lots);
        }

        [AllowAnonymous]
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
            var sectionId = _categoryService.GetCategoryEntity(lot.CategoryRefId).SectionRefId;
            lot.ModeratorLogin = _sectionService.GetAllSectionEntities()
                .FirstOrDefault(s => s.Id == sectionId).ModeratorLogin;
            return View(lot);
        }

        public ActionResult ModeratorTotalLots(int categoryId)
        {
            var lots = _lotService.GetAllLotEntities()
                .Where(l => l.CategoryRefId == categoryId)
                .Select(l => l.ToLotRowViewModel());
            return View(lots);
        }

        public ActionResult UserLots()
        {
            var lots = _lotService.GetAllLotEntities()
                .Where(l => l.SellerLogin == User.Identity.Name)
                .Select(l => l.ToLotRowViewModel());
            return View(lots);
        }

        public ActionResult ModeratorUnconfirmedLots(int categoryId)
        {
            var lots = _lotService.GetAllLotEntities()
                .Where(l => l.CategoryRefId == categoryId && !l.IsConfirm)
                .Select(l => l.ToLotRowViewModel());
            return View(lots);
        }

        [HttpPost]
        public ActionResult MakeBid(MakeBidViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var lot = _lotService.GetLotEntity(viewModel.Id);
                if (lot.EndDate > DateTime.Now && viewModel.MakeBid > viewModel.CurrentBid)
                {
                    _bidService.CreateBid(new BidEntity()
                    {
                        BidAmount = viewModel.MakeBid,
                        BidTime = DateTime.Now,
                        LotRefId = viewModel.Id,
                        UserRefId = _userService.GetUserEntityByLogin(User.Identity.Name).Id
                    });
                }
                return RedirectToAction("LotDetails", new {id =  viewModel.Id});
            }
            return RedirectToAction("LotDetails", new { id = viewModel.Id });
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