using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

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
            String qsItemID = context.Request.QueryString["itemID"] as String;
            int itemID;
            Int32.TryParse(qsItemID, out itemID);

            switch (context.Request.QueryString["op"])
            {
                case "PageSelectByItemID":
                    {
                        response = this.PageSelectByItemID(itemID);
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

        private string PageSelectByItemID(int itemId)
        {
            try
            {
                CustomGenericList<Page> pages = null;
                pages = new BHLProvider().PageSelectByItemID(itemId);
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