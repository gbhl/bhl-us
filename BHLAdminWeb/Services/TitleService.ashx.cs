using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TitleService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response;

            // Clean up inputs
            String qsTitleID = context.Request.QueryString["titleID"] as String;
            String title = context.Request.QueryString["title"] as String;

            int? virtualOnly = null;
            string qsVirtual = context.Request.QueryString["virtual"] as string;
            if (qsVirtual != null)
            {
                switch (qsVirtual.ToLower())
                {
                    case "true": virtualOnly = 1; break;
                    case "false": virtualOnly = 0; break;
                    default: virtualOnly = null; break;
                }
            }

            int titleID;
            Int32.TryParse(qsTitleID, out titleID);
            title = (title == null) ? "" : title;

            switch (context.Request.QueryString["op"])
            {
                case "TitleSearch":
                    {
                        response = this.TitleSearch(titleID, title, virtualOnly);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }

            }

            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        private string TitleSearch(int titleId, string title, int? virtualOnly)
        {
            try
            {
                TitleSearchCriteria criteria = new TitleSearchCriteria();
                if (titleId != 0) criteria.TitleID = titleId;
                criteria.Title = title + '%';
                criteria.StartRow = 1;
                criteria.PageSize = 10000;
                criteria.VirtualOnly = virtualOnly;
                criteria.OrderBy = TitleSearchOrderBy.Title;
                criteria.SortOrder = SortOrder.Ascending;
                List<Title> titles = new BHLProvider().TitleSearchPaging(criteria);
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(titles);
            }
            catch
            {
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
