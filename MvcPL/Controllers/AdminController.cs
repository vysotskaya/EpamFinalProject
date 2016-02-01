using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
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
    }
}