using MOBOT.BHL.OAI2;
using MOBOT.BHL.Web.Utilities;
using System.Web;
using System.Web.Services;

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
                AppConfig.OAIBaseUrl,
                AppConfig.OAIRepositoryName,
                AppConfig.OAIAdminEmail,
                AppConfig.OAIIdentifierNamespace,
                AppConfig.OAIMetadataFormats,
                AppConfig.OAIMaxListSets,
                AppConfig.OAIMaxListIdentifiers,
                AppConfig.OAIMaxListRecords
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
