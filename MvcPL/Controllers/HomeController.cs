using System.Data.Entity;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //DbContext db = new EntityModelContext();
            //db.Set<Role>().Add(new Role()
            //{
            //    RoleId = 1,
            //    RoleName = "Admin"
            //});
            //db.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}