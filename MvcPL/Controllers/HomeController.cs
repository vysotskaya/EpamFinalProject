using System.Linq;
using System.Web.Mvc;
using BLL.Interface.InterfaceServices;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRequestService _requestService;

        public HomeController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public ActionResult Index()
        {
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