﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult Default()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}