using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MOBOT.BHL.AdminWeb.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UtilityService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            string year = context.Request.QueryString["year"] as string;

            switch (context.Request.QueryString["op"])
            {
                case "CleanYear":
                    {
                        context.Response.ContentType = "application/json";
                        response = this.CleanYear(year);
                        break;
                    }
                case "ValidYear":
                    {
                        context.Response.ContentType = "application/json";
                        response = this.ValidateYear(year);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }

            }

            context.Response.Write(response);
        }

        private string CleanYear(string year)
        {
            year = DataCleaner.CleanYear(year);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(year);
        }

        private string ValidateYear(string year)
        {
            bool isValid = DataCleaner.ValidateItemSimpleYear(year);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(isValid);
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