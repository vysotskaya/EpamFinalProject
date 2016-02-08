using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.InterfaceServices;
using BLL.Interface.Services;
using MvcPL.Attributes;
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
            var user = _userService.GetUserEntityByLogin(User.Identity.Name);
            return View(user.ToMvcUser());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(UserRegisterViewModel viewModel)
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
                    if (viewModel.Photo == null)
                    {
                        user.Photo = user.Photo;
                    }
                    user.Photo = Image.FromStream(viewModel.Photo.InputStream);
                }
                else
                {
                    user.Photo = Image.FromStream(viewModel.Photo.InputStream);
                }
                _userService.UpdateUser(user);
                //FormsAuthentication.SignOut();
                FormsAuthentication.SetAuthCookie(viewModel.Login, true);
                return RedirectToAction("Index", "Lot");
            }
            return View(viewModel);
        }

        //public ActionResult UserBlocked()
        //{
        //    var user = _userService.GetUserEntityByLogin(User.Identity.Name);
        //    var blockModel = new UserBlockViewModel()
        //    {
        //        Id = user.Id,
        //        BlockDate = user.BlockTime,
        //        BlockReason = user.BlockReason
        //    };
        //    return View(blockModel);
        //}

        [Authorize(Roles="Administrator")]
        public ActionResult Details(int userId)
        {
            var userInfo = _userService.GetUserEntity(userId).ToUserDetailsModel();
            return View(userInfo);
        }

        //GET-запрос к методу Delete несет потенциальную уязвимость!
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

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserEntity user)
        {
            _userService.DeleteUser(user);
            return RedirectToAction("Index");
        }

    }
}