using DataLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Public.Models.Authentication;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Public.App_Start.Startup))]

namespace Public.App_Start
{
    public class Startup
    {
        public string DefaultAuthType { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(DataBase.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<SignInManager>(SignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Apartments/Index"),
                    Provider = new CookieAuthenticationProvider {
                        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager, User>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)
                        )
                    }
                }
            );

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
