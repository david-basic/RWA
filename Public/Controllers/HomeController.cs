using DataLayer.Repositories.Factory;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Public.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager _authManager;

        public UserManager AuthManager
        {
            get { return _authManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
            set { _authManager = value; }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> UserView()
        {
            var user = await AuthManager.FindByNameAsync(User.Identity.Name);

            var model = RepoFactory.GetRepo().LoadUserById(int.Parse(user.Id));

            return View(model);
        }
    }
}