using DataLayer.Models;
using DataLayer.Repositories.Factory;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Public.Models.Authentication;
using Public.Models.ViewModels;
using Recaptcha.Web.Mvc;
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
        public SignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>(); }
            set { _signInManager = value; }
        }
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
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM model)
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
            Predicate<Apartment> avaliabilityFilter = a => a.IsAvailable;
            Predicate<Apartment> nameFilter = a => true;
            Predicate<Apartment> cityFilter = a => true;
            Predicate<Apartment> statusFilter = a => true;

            if (search != null && search != string.Empty)
            {
                nameFilter = a => a.NameEng.Contains(search);
            }

            if (cityId != null && cityId != 0)
            {
                cityFilter = a => a.CityId.Equals(cityId);
            }

            if (statusId != null && statusId != string.Empty)
            {
                if ("all".Equals(statusId))
                {
                    avaliabilityFilter = a => true;
                }
                else if ("unavaliable".Equals(statusId))
                {
                    avaliabilityFilter = a => !a.IsAvailable;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ViewApartment(int id)
        {
            var loggedUser = await AuthManager.FindByNameAsync(User.Identity.Name);

            var model = new ViewApartmentVM
            {
                Apartment = RepoFactory.GetRepo().LoadApartmentById(id),
                ApartmentId = id,
                UserId = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                PhoneNumber = string.Empty,
                Details = string.Empty,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };

            if (loggedUser != null)
            {
                model.UserId = loggedUser.Id;
                model.FirstName = loggedUser.FirstName;
                model.LastName = loggedUser.LastName;
                model.Email = loggedUser.Email;
                model.PhoneNumber = loggedUser.PhoneNumber;
                model.ShowReviewForm = true;
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewApartment(ViewApartmentVM model)
        {
            model.Apartment = RepoFactory.GetRepo().LoadApartmentById(model.ApartmentId);

            if (string.IsNullOrEmpty(model.UserId))
            {
                var recaptchaHelper = this.GetRecaptchaVerificationHelper(secretKey: "6Ld0Ya0gAAAAAP0oJWaYw1iafuD_aEXB_GUn7iGS");
                if (string.IsNullOrEmpty(recaptchaHelper.Response))
                {
                    ModelState.AddModelError(
                    "",
                    "Captcha answer cannot be empty.");

                    return View(model);
                }

                var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                if (!recaptchaResult.Success)
                {
                    ModelState.AddModelError(
                    "",
                    "Incorrect captcha answer.");
                }
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.UserId))
                {
                    RepoFactory.GetRepo().GenerateApartmentReservation(new ApartmentReservation
                    {
                        Guid = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        ApartmentId = model.ApartmentId,
                        Details = $"{model.StartDate.ToShortDateString()} - {model.EndDate.ToShortDateString()}, {model.Details}",
                        UserEmail = model.Email,
                        UserName = $"{model.FirstName} {model.LastName}",
                        UserPhone = model.PhoneNumber
                    });
                }
                else
                {
                    RepoFactory.GetRepo().GenerateApartmentReservation(new ApartmentReservation
                    {
                        Guid = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        ApartmentId = model.ApartmentId,
                        Details = $"{model.StartDate.ToShortDateString()} - {model.EndDate.ToShortDateString()}, {model.Details}",
                        UserId = int.Parse(model.UserId),
                    });
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SubmitApartmentReview(ApartmentReview review)
        {
            RepoFactory.GetRepo().InsertApartmentReview(review);

            return new EmptyResult();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoadApartmentReviewsListView(int apartmentId)
        {
            var reviews = RepoFactory.GetRepo().LoadApartmentReviewsByApartmentId(apartmentId);

            return PartialView("_ReviewListView", new ApartmentReviewListVM { Reviews = reviews.Reverse() });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterVM model)
        {
            var recaptchaHelper = this.GetRecaptchaVerificationHelper(secretKey: "6Ld0Ya0gAAAAAP0oJWaYw1iafuD_aEXB_GUn7iGS");
            if (string.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError(
                "",
                "Captcha answer cannot be empty.");

                return View(model);
            }

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError(
                "",
                "Incorrect captcha answer.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newRegisteredUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = DataLayer.Encryption.SHA512(model.Password),
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                CreatedAt = DateTime.Now,
                Guid = Guid.NewGuid(),
            };

            RepoFactory.GetRepo().InsertUser(newRegisteredUser);

            return RedirectToAction(actionName: "LoginAfterRegistrationAsync", controllerName: "Home", routeValues: new { email = newRegisteredUser.Email, password = model.Password });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAfterRegistrationAsync(string email, string password)
        {
            var user = await AuthManager.FindAsync(email, password);
            await SignInManager.SignInAsync(user, true, true);

            ViewBag.username = user.UserName;

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}