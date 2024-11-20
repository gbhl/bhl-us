using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
    public class Helper
    {
        public sealed class SecurityRole
        {
            private readonly String name;

            public static readonly SecurityRole BHLAdminUserBasic = new SecurityRole("BHL.Admin.User");
            public static readonly SecurityRole BHLAdminUserAdvanced = new SecurityRole("BHL.Admin.Admin");
            public static readonly SecurityRole BHLAdminPortalEditor = new SecurityRole("BHL.Admin.PortalEditor");
            public static readonly SecurityRole BHLAdminSysAdmin = new SecurityRole("BHL.Admin.SysAdmin");

            private SecurityRole(String name)
            {
                this.name = name;
            }

            public override String ToString() { return name; }
        }

        public static bool IsUserAuthenticated(HttpRequestBase request)
        {
            return request.GetOwinContext().Authentication.User.Identity.IsAuthenticated;
        }

        public static bool IsUserAuthorized(HttpRequestBase request, SecurityRole securityRole)
        {
            bool authorized = false;
            authorized = request.GetOwinContext().Authentication.User.IsInRole(securityRole.ToString());
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
            else if (path == "/") authorized = IsUserAuthenticated(request); // IsUserAuthorized(request, SecurityFunction.BHLAdminLogin);
            else if (path.Contains("dashboard")) authorized = IsUserAuthenticated(request); // IsUserAuthorized(request, SecurityFunction.BHLAdminLogin);
            else if (path.Contains("pagenotfound")) authorized = IsUserAuthenticated(request); // IsUserAuthorized(request, SecurityFunction.BHLAdminLogin);

            // URLs available to "Portal Editor" users
            else if (path.Contains("titlesearch")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("titleedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("titleassociationedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("titleitemmarc")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("titleimport")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("marcdetails")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("itemedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("segmentsearch")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("segmentedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("paginator")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("reportitempagination")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("ajaxflickrupload")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("flickrloginredirect")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("flickrupload")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("authorsearch")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("authoredit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("authorsegmentslist")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("authortitleslist")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);
            else if (path.Contains("namepageedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminPortalEditor);

            // URLs available to "Basic User" users (BHL Admin Site basic users)
            else if (path.Contains("collectionedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("collectionbulkadd")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("institutionedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("admin/group")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserAdvanced);
            else if (path.Contains("languageedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("notetypeedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("pdfedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("segmenttypeedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);

            else if (path.Contains("citationimport")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("textimport")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("report/")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("biostorharvest")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("biostorsegmentsforitem")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("doi/queue")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("doilist")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("doistatusabout")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("doisubmissiondetail")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("growthstats")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("iaharvest")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("queueforharvest")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("pdfedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("pdfstats")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("pdfweeklystats")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("stats.aspx")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("statsdownload")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);

            else if (path.Contains("reportcharacterencodingproblems")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportdoibyinstitution")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportiaitemspendingapproval")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportitemsbycontentprovider")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportitemsbycontributor")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportitemsbycontributorcsv")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportmonographiccontributions")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportnonmembermonographscsv")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("reportrecentlyclusteredsegments")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("report/orphans")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);
            else if (path.Contains("downloads/ExternalContent")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserBasic);

            // URLs available to "Advanced User" users (BHL Admin Site admins)
            else if (path.Contains("alertedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserAdvanced);
            else if (path.Contains("webstats")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserAdvanced);
            else if (path.Contains("webhistory")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserAdvanced);
            else if (path.Contains("openurlresult")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminUserAdvanced);

            // URLs available to "SysAdmin" users
            else if (path.Contains("pagetypeedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminSysAdmin);
            else if (path.Contains("webresourcepaths")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminSysAdmin);
            else if (path.Contains("vaultedit")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminSysAdmin);
            else if (path.Contains("library/align")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminSysAdmin);
            else if (path.Contains("monitor")) authorized = IsUserAuthorized(request, SecurityRole.BHLAdminSysAdmin);

            return authorized;
        }

        public static int GetCurrentUserUID(HttpRequestBase request)
        {
            string userName = request.GetOwinContext().Authentication.User.Identity.Name;
            var Db = new MOBOT.BHL.AdminWeb.Models.ApplicationDbContext();
            var uid = (from u in Db.Users where u.UserName == userName select u.Id).Single();
            return uid;
        }

        public static Models.ApplicationUser GetCurrentUserDetail(HttpRequestBase request)
        {
            string userName = request.GetOwinContext().Authentication.User.Identity.Name;
            var Db = new MOBOT.BHL.AdminWeb.Models.ApplicationDbContext();
            var user = (from u in Db.Users where u.UserName == userName select u).Single();
            return user;
        }
    }
}