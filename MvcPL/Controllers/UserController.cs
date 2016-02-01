using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles="Administrator")]
        public ActionResult Details(int userId = 7)
        {
            var userInfo = _userService.GetUserEntity(userId).ToUserDetailsModel();
            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRegisterViewModel userViewModel)
        {
            ////UserViewModel model = new UserViewModel() {Email = "vys@mail.ru"};
            //UserRegisterViewModel model1 = new UserRegisterViewModel() { Email = "testMulti@mail.ru", Roles = new []{Role.Guest, Role.User} };
            //_userService.CreateUser(model1.ToBllUser());
            return RedirectToAction("Index");
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

        //etc.
    }
}