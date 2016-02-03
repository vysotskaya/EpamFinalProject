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
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.NotifCount = _requestService.GetAllRequestEntities().Count();
            return View();
        }
    }
}