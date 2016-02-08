using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;

namespace MvcPL.Controllers
{
    public class ValidateController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly IUserService _userService;

        public ValidateController(ISectionService sectionService, IUserService userService)
        {
            _sectionService = sectionService;
            _userService = userService;
        }

        public JsonResult IsSectionNameAvailable(string sectionName, int Id)
       {
            var section = _sectionService.GetAllSectionEntities().FirstOrDefault(s => s.SectionName == sectionName);
            return Json(!(section != null && section.Id != Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPasswordsMatch(string confirmPassword, string password)
        {
            return Json(String.Compare(confirmPassword, password) == 0, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUserLoginExist(string Login)
        {
            var userExist = _userService.GetAllUserEntities().Any(u => u.Login == Login);
            if (String.Compare(User.Identity.Name, Login) == 0)
            {
                userExist = false;
            }
            return Json(!userExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUserOldPasswordMatch(string oldPassword, int Id)
        {
            var user = _userService.GetUserEntity(Id);
            var result = user != null && Crypto.VerifyHashedPassword(user.Password, oldPassword);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}