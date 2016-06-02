using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(MyGraduationProject.App_Start.Startup))]

namespace MyGraduationProject.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //TODO wywalic ta sekcje!
           ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = AuthenticationMode.Passive,
                LoginPath = new PathString("/Home/Login"),
                LogoutPath = new PathString("/Home/Index"),
                CookieSecure = CookieSecureOption.Always,
            });
        }
    }
}