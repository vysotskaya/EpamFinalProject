﻿using System;
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
        private readonly ICategoryService _categoryService;

        public AdminController(IUserService userService,
            ISectionService sectionService, IRequestService requestService, ICategoryService categoryService)
        {
            _userService = userService;
            _sectionService = sectionService;
            _requestService = requestService;
            _categoryService = categoryService;
        }

        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUserEntities().Select(user => user.ToUserDetailsModel());
            return View(users);
        }

        [ActionName("AllSections")]
        public ActionResult GetAllSections()
        {
            var sections = _sectionService.GetAllSectionEntities().Select(s => s.ToSectionDetailsModel());
            return View(sections);
        }

        [HttpPost]
        public bool BlockUser(UserBlockViewModel blockModel)
        {
            if (!ModelState.IsValid) return false;
            var user = _userService.GetUserEntity(blockModel.Id);
            if (user == null)
            {
                return false;
            }
            user.ToBllUserBlockInfo(blockModel);
            _userService.UpdateUser(user);
            return true;
        }

        [HttpPost]
        public bool BlockSection(SectionDetailsViewModel blockModel)
        {
            var section = _sectionService.GetSectionEntity(blockModel.Id);
            if (section == null)
            {
                return false;
            }
            section.IsBlocked = blockModel.IsBlocked;
            _sectionService.UpdateSection(section);
            return true;
        }

        public ActionResult CreateSection()
        {
            var userLogins = _userService.GetAllUserEntities()
                .Where(u => !u.IsBlocked)
                .Select(u => new SelectListItem() {Value = u.Login, Text = u.Login});
            var section = new SectionCreateViewModel() {UsersLogin = userLogins};
            return View(section);
        }

        public ActionResult EditSection(int id)
        {
            var userLogins = _userService.GetAllUserEntities()
                .Where(u => !u.IsBlocked)
                .Select(u => new SelectListItem() { Value = u.Login, Text = u.Login });
            var section = _sectionService.GetSectionEntity(id)?.ToSectionViewModel();
            if (section == null)
            {
                return RedirectToAction("AllSections");
            }
            section.UsersLogin = userLogins;
            return View(section);
        }

        [HttpPost]
        public ActionResult EditSection(SectionCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existedSection = _sectionService.GetSectionEntity(viewModel.Id);
                if (existedSection.ModeratorLogin.CompareTo(viewModel.SettedModeratorLogin) != 0)
                {
                    var moderator = _userService.GetUserEntityByLogin(viewModel.SettedModeratorLogin);
                    if (!moderator.Roles.Any(r => r.RoleName == Role.Moderator.ToString()))
                    {
                        moderator.Roles.Add(new RoleEntity()
                        {
                            RoleName = Role.Moderator.ToString(),
                            Id = (int)Role.Moderator
                        });
                        _userService.UpdateUser(moderator);
                    }
                    existedSection.UserRefId = moderator.Id;

                    var oldModerator = _userService.GetUserEntityByLogin(existedSection.ModeratorLogin);
                    var oldRole = oldModerator.Roles.FirstOrDefault(t => t.Id == (int) Role.Moderator);
                    if (oldRole != null)
                    {
                        var result = oldModerator.Roles.Remove(oldRole);
                    }
                    _userService.UpdateUser(oldModerator);
                }
                existedSection.SectionName = viewModel.SectionName;
                existedSection.Discription = viewModel.Discription;
                _sectionService.UpdateSection(existedSection);
                return RedirectToAction("AllSections", "Admin");
            }
            viewModel.UsersLogin = _userService.GetAllUserEntities()
                .Where(u => !u.IsBlocked)
                .Select(u => new SelectListItem() { Value = u.Login, Text = u.Login });
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSection(SectionCreateViewModel viewModel)
        {
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

        public int NotificationCount()
        {
            return _requestService.GetAllRequestEntities().Count();
        }

        public bool HandleRequest(RequestViewModel requestModel)
        {
            var request = _requestService.GetRequestEntity(requestModel.Id);
            _requestService.DeleteRequest(request);
            var category = _categoryService.GetCategoryEntity(request.CategoryRefId);
            if (request.ToConfirm)
            {
                if (requestModel.Result)
                {
                    category.IsConfirmed = true;
                }
                else
                {
                    _categoryService.DeleteCategory(category);
                    return true;
                }
            }
            else
            {
                if (requestModel.Result)
                {
                    category.IsBlocked = true;
                }
            } 
            _categoryService.UpdateCategory(category);
            return true;
        }
    }
}