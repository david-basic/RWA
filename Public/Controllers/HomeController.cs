using DataLayer.Repositories.Factory;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Public.Models.Authentication;
using Public.Models.ViewModels;
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
        private SignInManager _signInManager;

        public UserManager AuthManager
        {
            get { return _authManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>(); }
            set { _authManager = value; }
        }
        public SignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>(); }
            set { _signInManager = value; }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await AuthManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                await SignInManager.SignInAsync(user, true, model.RememberMe);

                ViewBag.username = user.UserName;

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(model);
            }
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