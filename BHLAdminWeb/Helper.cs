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

        public static bool IsUserAuthenticated(HttpRequestBase request)
        {
            HttpCookie tokenCookie = request.Cookies["MOBOTSecurityToken"];
            return (tokenCookie != null && tokenCookie.Value.Length > 0);
        }

        public static bool IsUserAuthorized(HttpRequestBase request, SecurityFunction securityFunction)
        {
            bool authorized = false;
            HttpCookie tokenCookie = request.Cookies["MOBOTSecurityToken"];

            if (tokenCookie != null && tokenCookie.Value.Length > 0)
            {
                MethodResult result = Helper.GetSecProvider().IsUserAuthorized(tokenCookie.Value, securityFunction.Value());
                authorized = (result.ResultStatus == ResultStatusEnum.Success);
            }

            return authorized;
        }

        /// <summary>
        /// Verify that the authenticated user is authorized to access the requested URL
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsUserAuthorized(HttpRequestBase request)
        {
            bool authorized = false;
            string path = request.Url.AbsolutePath.ToLower();

            // URLs available to any authenticated user
            if (path.Contains("accessdenied")) authorized = true;
            else if (path.Contains("error")) authorized = true;

            // URLs that require at least some rights
            else if (path == "/") authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminLogin);
            else if (path.Contains("dashboard")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminLogin);
            else if (path.Contains("pagenotfound")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminLogin);

            // URLs available to "Portal Editor" users
            else if (path.Contains("titlesearch")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("titleedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("titleassociationedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("titleitemmarc")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("titleimport")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("marcdetails")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("itemedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("segmentsearch")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("segmentedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("paginator")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("reportitempagination")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("ajaxflickrupload")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("flickrloginredirect")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("flickrupload")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("authorsearch")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("authoredit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("authorsegmentslist")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("authortitleslist")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);
            else if (path.Contains("namepageedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminPortalEditor);

            // URLs available to "Basic User" users (BHL Admin Site basic users)
            else if (path.Contains("collectionedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("collectionbulkadd")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("institutionedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("languageedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("notetypeedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("pagetypeedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("pdfedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("segmenttypeedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);

            else if (path.Contains("citationimport")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("report/")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("biostorharvest")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("biostorsegmentsforitem")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("doilist")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("doistatusabout")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("doisubmissiondetail")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("growthstats")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("iaharvest")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("queueforharvest")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("pdfedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("pdfstats")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("pdfweeklystats")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("stats.aspx")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("statsdownload")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);

            else if (path.Contains("reportcharacterencodingproblems")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportdoibyinstitution")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportiaitemspendingapproval")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportitemsbycontributor")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportitemsbycontributorcsv")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportmonographiccontributions")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportnonmembermonographscsv")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);
            else if (path.Contains("reportrecentlyclusteredsegments")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserBasic);

            // URLs available to "Advanced User" users (BHL Admin Site admins)
            else if (path.Contains("alertedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserAdvanced);
            else if (path.Contains("webstats")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserAdvanced);
            else if (path.Contains("webhistory")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserAdvanced);
            else if (path.Contains("openurlresult")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminUserAdvanced);

            // URLs available to "SysAdmin" users
            else if (path.Contains("imageserveredit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminSysAdmin);
            else if (path.Contains("vaultedit")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminSysAdmin);
            else if (path.Contains("library/align")) authorized = IsUserAuthorized(request, SecurityFunction.BHLAdminSysAdmin);

            return authorized;
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