using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for NameService
    /// </summary>
    public class NameService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response;

            // Clean up inputs
            String name = context.Request.QueryString["name"] as String;
            name = (name ?? string.Empty);

            switch (context.Request.QueryString["op"])
            {
                case "NameSearch":
                    {
                        response = this.NameSearch(name);
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

        private string NameSearch(String name)
        {
            try
            {
                List<Name> names = new BHLProvider().NameSelectByNameString(name);

                for (int x = (names.Count - 1); x >= 0; x--)
                {
                    // Remove inactive names.
                    if (names[x].IsActive == 0) names.RemoveAt(x);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(names);
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