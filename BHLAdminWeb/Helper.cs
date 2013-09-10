using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.Security.Client;
using MOBOT.Security.Client.MOBOTSecurity;

namespace MOBOT.BHL.AdminWeb
{
    public class Helper
    {
        private static SecProvider _secProvider = null;

        public static bool IsAdmin(HttpRequest request)
        {
            HttpCookie tokenCookie = request.Cookies["MOBOTSecurityToken"];

            if (tokenCookie != null && tokenCookie.Value.Length > 0)
            {
                MethodResult result = Helper.GetSecProvider().IsUserAuthorized(tokenCookie.Value, "BHL_Admin");
                return (result.ResultStatus == ResultStatusEnum.Success);
            }

            return false;
        }

        public static bool IsAdminSuperUser(HttpRequest request)
        {
            HttpCookie tokenCookie = request.Cookies["MOBOTSecurityToken"];

            if (tokenCookie != null && tokenCookie.Value.Length > 0)
            {
                MethodResult result = Helper.GetSecProvider().IsUserAuthorized(tokenCookie.Value, "BHL_Admin_SuperUser");
                return (result.ResultStatus == ResultStatusEnum.Success);
            }

            return false;
        }

        public static SecProvider GetSecProvider()
        {
            if (_secProvider == null)
            {
                string url = ConfigurationManager.AppSettings["MOBOTSecurityWSUrl"];
                _secProvider = new SecProvider(url, false);
            }

            return _secProvider;
        }
    }
}