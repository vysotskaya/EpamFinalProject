using System;
using System.Drawing;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequestService _requestService;

        public UserController(IUserService userService, IRequestService requestService)
        {
            _userService = userService;
            _requestService = requestService;
        }

        public ActionResult UserBlocked(string userLogin)
        {
            var user = _userService.GetUserEntityByLogin(userLogin);
            var blockModel = new UserBlockViewModel()
            {
                Id = user.Id,
                BlockDate = user.BlockTime,
                BlockReason = user.BlockReason
            };
            return View(blockModel);
        }

        [Authorize]
        public ActionResult EditUserProfile()
        {
            var user = _userService.GetUserEntityByLogin(User.Identity.Name).ToMvcEditUserModel();
            return View(user);
        }

        public ActionResult EditUserPassword(PasswordChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserEntityByLogin(User.Identity.Name);
                if (Crypto.VerifyHashedPassword(user.Password, model.OldPassword)
                    && String.Compare(model.Password, model.ConfirmPassword) == 0)
                {
                    user.Password = Crypto.HashPassword(model.Password);
                    _userService.UpdateUser(user);
                    return RedirectToAction("Index", "Lot"); 
                }
            }
            return RedirectToAction("EditUserProfile", "User");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(UserEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userWithSameLogin = _userService.GetAllUserEntities().FirstOrDefault(u => u.Login.Contains(viewModel.Login));

                if (userWithSameLogin != null && userWithSameLogin.Id != viewModel.Id)
                {
                    ModelState.AddModelError("", "User with this login already exist.");
                    return View(viewModel);
                }
                var user = _userService.GetUserEntityByLogin(User.Identity.Name);
                user.Login = viewModel.Login;
                user.Email = viewModel.Email;
                if (user.Photo != null)
                {
                    if (viewModel.Photo != null)
                    {
                        user.Photo = Image.FromStream(viewModel.Photo.InputStream);
                    }
                }
                else
                {
                    user.Photo = viewModel.Photo != null ? Image.FromStream(viewModel.Photo.InputStream) : null;
                }
                _userService.UpdateUser(user);
                FormsAuthentication.SetAuthCookie(viewModel.Login, true);
                return RedirectToAction("Index", "Lot");
            }
            return View(viewModel);
        }
        
        [Authorize(Roles="Administrator")]
        public ActionResult Details(int userId)
        {
            var userInfo = _userService.GetUserEntity(userId).ToUserDetailsModel();
            return View(userInfo);
        }

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            UserEntity user = _userService.GetUserEntity(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToMvcUser());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserEntity user)
        {
            _userService.DeleteUser(user);
            return RedirectToAction("Index");
        }

    }
}