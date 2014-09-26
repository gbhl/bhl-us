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
        public sealed class SecurityFunction
        {
            private readonly String name;
            private readonly int value;

            public static readonly SecurityFunction BHLAdminUserBasic = new SecurityFunction(7, "BHL_Admin_UserBasic");
            public static readonly SecurityFunction BHLAdminUserAdvanced = new SecurityFunction(277, "BHL_Admin_UserAdvanced");
            public static readonly SecurityFunction BHLAdminPortalEditor = new SecurityFunction(8, "BHL_Admin_PortalEditor");
            public static readonly SecurityFunction BHLAdminSysAdmin = new SecurityFunction(304, "BHL_Admin_SysAdmin");
            public static readonly SecurityFunction BHLAdminLogin = new SecurityFunction(305, "BHL_Admin_Login");

            private SecurityFunction(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString() { return name; }
            public int Value() { return value; }
        }

        public static bool IsUserAuthorized(HttpRequest request, SecurityFunction securityFunction)
        {
            HttpCookie tokenCookie = request.Cookies["MOBOTSecurityToken"];

            if (tokenCookie != null && tokenCookie.Value.Length > 0)
            {
                MethodResult result = Helper.GetSecProvider().IsUserAuthorized(tokenCookie.Value, securityFunction.Value());

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