using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DevelopmentStartup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureDevelopmentAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                SlidingExpiration = true,
                ExpireTimeSpan = System.TimeSpan.FromHours(10),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            // TODO: Replace the Google keys with the keys tied to the BHL account for the BETA site
            app.UseGoogleAuthentication(
                clientId: "688146021135-1vftn4rjc777klmt6ttbsofenrh3bq48.apps.googleusercontent.com",
                clientSecret: "PoEkIm575D-Ale7jqobQOoHQ");
        }
    }

    public partial class ProductionStartup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureProductionAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                SlidingExpiration = true,
                ExpireTimeSpan = System.TimeSpan.FromHours(10),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            // TODO: Replace the Google keys with the keys tied to the BHL account for the PRODUCTION site
            app.UseGoogleAuthentication(
                clientId: "688146021135-1vftn4rjc777klmt6ttbsofenrh3bq48.apps.googleusercontent.com",
                clientSecret: "PoEkIm575D-Ale7jqobQOoHQ");
        }
    }
}
