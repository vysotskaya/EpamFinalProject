using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.InterfaceServices;

namespace MvcPL.Controllers
{
    public class ValidateController : Controller
    {
        private readonly ISectionService _sectionService;

        public ValidateController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        public JsonResult IsSectionNameAvailable(string sectionName)
        {
            var sectionExist = _sectionService.GetAllSectionEntities().Any(s => s.SectionName == sectionName);
            return Json(sectionExist, JsonRequestBehavior.AllowGet);
        }
    }
}