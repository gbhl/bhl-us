using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

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
            int titleID;
            Int32.TryParse(qsTitleID, out titleID);
            title = (title == null) ? "" : title;

            switch (context.Request.QueryString["op"])
            {
                case "TitleSearch":
                    {
                        response = this.TitleSearch(titleID, title);
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

        private string TitleSearch(int titleId, String title)
        {
            try
            {
                TitleSearchCriteria criteria = new TitleSearchCriteria();
                if (titleId != 0) criteria.TitleID = titleId;
                criteria.Title = title + '%';
                criteria.StartRow = 1;
                criteria.PageSize = 10000;
                criteria.OrderBy = TitleSearchOrderBy.Title;
                criteria.SortOrder = SortOrder.Ascending;
                CustomGenericList<Title> titles = new BHLProvider().TitleSearchPaging(criteria);
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
