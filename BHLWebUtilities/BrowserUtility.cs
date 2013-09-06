using System;
using System.Web;

namespace MOBOT.BHL.Web.Utilities
{
    public class BrowserUtility
    {
        protected BrowserUtility()
        {
        }

        public static bool IsBrowserIE6OrBelow(HttpRequest request)
        {
            return (IsBrowserIE(request) && request.Browser.MajorVersion <= 6);
        }

        public static bool IsBrowserIE(HttpRequest request)
        {
            return (request.Browser.Browser.Trim().ToUpper() == "IE");
        }
    }
}
