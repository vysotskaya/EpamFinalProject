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

        public AdminController(IUserService userService, ISectionService sectionService)
        {
            _userService = userService;
            _sectionService = sectionService;
        }

        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUserEntities().Select(user => user.ToUserDetailsModel());
            return View(users);
        }

        public bool BlockUser(UserBlockViewModel blockModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserEntity(blockModel.Id);
                user.ToBllUserBlockInfo(blockModel);
                _userService.UpdateUser(user);
                return true;
            }
            return false;
        }

        public ActionResult CreateSection()
        {
            var section = new SectionEntity()
            {
                CreationDate = DateTime.Now,
                IsBlocked = false,
                SectionName = "Stars",
                UserRefId = 1
            };
            _sectionService.CreateSection(section);
            return RedirectToAction("Index");
        }
    }
}