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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequestService _requestService;

        public UserController(IUserService userService, IRequestService requestService)
        {
            _userService = userService;
            _requestService = requestService;
        }

        [Authorize(Roles="Administrator")]
        public ActionResult Details(int userId)
        {
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
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