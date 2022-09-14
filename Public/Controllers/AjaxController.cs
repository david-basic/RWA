﻿using DataLayer.Repositories.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class AjaxController : Controller
    {
        public ActionResult GetApartmentNames(string wordPart)
        {
            var allNames = RepoFactory.GetRepo().LoadApartmentNames();

            var namesFound = allNames.Where(name => name.ToLower().Contains(wordPart.ToLower()));

            return Json(namesFound, JsonRequestBehavior.AllowGet);
        }
    }
}