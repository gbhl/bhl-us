using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.OAI2;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class oai2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            OAI2Publisher oai = new OAI2Publisher(
                ConfigurationManager.AppSettings["OAIBaseUrl"],
                ConfigurationManager.AppSettings["OAIRepositoryName"],
                ConfigurationManager.AppSettings["OAIAdminEmail"],
                ConfigurationManager.AppSettings["OAIIdentifierNamespace"],
                ConfigurationManager.AppSettings["OAIMetadataFormats"],
                ConfigurationManager.AppSettings["OAIMaxListSets"],
                ConfigurationManager.AppSettings["OAIMaxListIdentifiers"],
                ConfigurationManager.AppSettings["OAIMaxListRecords"]
                );

            context.Response.ContentType = "text/xml";
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "private");

            context.Response.Write(oai.Request(context.Request.QueryString));
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
