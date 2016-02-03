using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISectionService _sectionService;
        private readonly IRequestService _requestService;

        public AdminController(IUserService userService,
            ISectionService sectionService, IRequestService requestService)
        {
            _userService = userService;
            _sectionService = sectionService;
            _requestService = requestService;
        }

        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            var users = _userService.GetAllUserEntities().Select(user => user.ToUserDetailsModel());
            return View(users);
        }

        [ActionName("AllSections")]
        public ActionResult GetAllSections()
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            var sections = _sectionService.GetAllSectionEntities().Select(s => s.ToSectionDetailsModel());
            return View(sections);
        }

        public bool BlockUser(UserBlockViewModel blockModel)
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserEntity(blockModel.Id);
                user.ToBllUserBlockInfo(blockModel);
                _userService.UpdateUser(user);
                return true;
            }
            return false;
        }

        public bool BlockSection(SectionDetailsViewModel blockModel)
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            var section = _sectionService.GetSectionEntity(blockModel.Id);
            section.IsBlocked = blockModel.IsBlocked;
            _sectionService.UpdateSection(section);
            return true;
        }

        public ActionResult CreateSection()
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            var userLogins = _userService.GetAllUserEntities()
                .Where(u => !u.IsBlocked)
                .Select(u => new SelectListItem() {Value = u.Login, Text = u.Login});
            var section = new SectionCreateViewModel() {UsersLogin = userLogins};
            return View(section);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSection(SectionCreateViewModel viewModel)
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            if (ModelState.IsValid)
            {
                var moderator = _userService.GetUserEntityByLogin(viewModel.SettedModeratorLogin);
                if (moderator.Roles.All(r => r.RoleName != Role.Moderator.ToString()))
                {
                    moderator.Roles.Add(new RoleEntity()
                    {
                        RoleName = Role.Moderator.ToString(),
                        Id = (int)Role.Moderator
                    });
                }
                _userService.UpdateUser(moderator);
                viewModel.UserRefId = moderator.Id;
                var bllSection = viewModel.ToBllSectionEntity();
                _sectionService.CreateSection(bllSection);
                return RedirectToAction("AllSections", "Admin");
            }
            viewModel.UsersLogin = _userService.GetAllUserEntities()
                .Where(u => !u.IsBlocked)
                .Select(u => new SelectListItem() { Value = u.Login, Text = u.Login });
            return View(viewModel);
        }

        public ActionResult Notifications()
        {
            var requests = _requestService.GetAllRequestEntities().Select(r => new RequestViewModel()
            {
                ToConfirm = r.ToConfirm,
                Id = r.Id,
                SectionName = r.SectionName,
                SectionRefId = r.SectionRefId,
                CategoryName = r.CategoryName,
                CategoryRefId = r.CategoryRefId
            }).ToList();
            return View(requests);
        }
    }
}