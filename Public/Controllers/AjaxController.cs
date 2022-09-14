using DataLayer.Repositories.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class AjaxController : Controller
    {
        public ActionResult GetApartmentNames(string term)
        {
            var allNames = RepoFactory.GetRepo().LoadApartmentNames();

            var found = allNames.Where(
                name => name.ToLower().Contains(term.ToLower())
            );

            return Json(found, JsonRequestBehavior.AllowGet);
        }
    }
}