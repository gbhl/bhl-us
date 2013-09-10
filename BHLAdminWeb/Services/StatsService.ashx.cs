using System;
using System.Collections.Generic;
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
    public class StatsService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response;

            switch (context.Request.QueryString["op"])
            {
                case "SelectNames":
                    {
                        response = this.SelectNames();
                        break;
                    }
                case "SelectUniqueNames":
                    {
                        response = this.SelectUniqueNames();
                        break;
                    }
                case "SelectVerifiedNames":
                    {
                        response = this.SelectVerifiedNames();
                        break;
                    }
                case "SelectEOLNames":
                    {
                        response = this.SelectEOLNames();
                        break;
                    }
                case "SelectEOLPages":
                    {
                        response = this.SelectEOLPages();
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

        private string SelectNames()
        {
            try
            {
                MOBOT.BHL.DataObjects.Stats stats = null;
                stats = new BHLProvider().StatsSelectNames();

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(stats);
            }
            catch
            {
                return null;
            }
        }

        private string SelectUniqueNames()
        {
            try
            {
                MOBOT.BHL.DataObjects.Stats stats = null;
                stats = new BHLProvider().StatsSelectUniqueNames();

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(stats);
            }
            catch
            {
                return null;
            }
        }

        private string SelectVerifiedNames()
        {
            try
            {
                MOBOT.BHL.DataObjects.Stats stats = null;
                stats = new BHLProvider().StatsSelectVerifiedNames();

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(stats);
            }
            catch
            {
                return null;
            }
        }

        private string SelectEOLNames()
        {
            try
            {
                MOBOT.BHL.DataObjects.Stats stats = null;
                stats = new BHLProvider().StatsSelectEOLNames();

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(stats);
            }
            catch
            {
                return null;
            }
        }

        private string SelectEOLPages()
        {
            try
            {
                MOBOT.BHL.DataObjects.Stats stats = null;
                stats = new BHLProvider().StatsSelectEOLPages();

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(stats);
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
