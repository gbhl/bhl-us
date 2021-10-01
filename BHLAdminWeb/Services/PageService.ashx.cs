using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for PageService
    /// </summary>
    public class PageService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response;

            // Clean up inputs
            String qsBookID = context.Request.QueryString["bookID"] as String;
            int bookID;
            Int32.TryParse(qsBookID, out bookID);

            switch (context.Request.QueryString["op"])
            {
                case "PageSelectByBookID":
                    {
                        response = this.PageSelectByBookID(bookID);
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

        private string PageSelectByBookID(int bookID)
        {
            try
            {
                List<Page> pages = null;
                pages = new BHLProvider().PageSelectByBookID(bookID);
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(pages);
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