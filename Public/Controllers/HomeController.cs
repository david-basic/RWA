using DataLayer.Models;
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
        public async Task<ActionResult> Index(LoginVM model)
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoadApartmentListPartialView(string search, int? cityId, string statusId, string filterCode)
        {
            Predicate<Apartment> avaliabilityFilter = a => a.Availability;
            Predicate<Apartment> nameFilter = a => true;
            Predicate<Apartment> cityFilter = a => true;
            Predicate<Apartment> statusFilter = a => true;

            if (search != null && search != string.Empty)
            {
                nameFilter = (a => a.NameEng.Contains(search));
            }

            if (cityId != null && cityId != 0)
            {
                cityFilter = (a => a.CityId.Equals(cityId));
            }

            if (statusId != null && statusId != string.Empty)
            {
                if ("all".Equals(statusId))
                {
                    avaliabilityFilter = a => true;
                }
                else if ("unavaliable".Equals(statusId))
                {
                    avaliabilityFilter = a => !a.Availability;
                }
            }

            List<Apartment> apartments = (List<Apartment>)RepoFactory.GetRepo().LoadApartments(avaliabilityFilter, nameFilter, cityFilter, statusFilter);

            if (filterCode != null && filterCode != string.Empty)
            {
                if (Apartment.ComparisonDicitionary.Keys.Any(key => key.Equals(filterCode)))
                {
                    apartments.Sort(Apartment.ComparisonDicitionary[filterCode]);
                }
            }
            else
            {
                apartments.Sort(Apartment.PriceLowToHighComp);
            }

            var model = new ApartmentListVM
            {
                Apartments = apartments
            };

            return PartialView("_ApartmentListView", model);
        }
    }
}