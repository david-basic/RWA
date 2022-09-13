using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Index(string error)
        {
            return View();
        }
    }
}