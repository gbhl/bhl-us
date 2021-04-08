using MOBOT.BHL.DataObjects.Enum;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetPartText
    /// </summary>
    public class GetPartText : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string idString = HttpContext.Current.Request.RequestContext.RouteData.Values["id"].ToString();
            if (idString == null) return;

            if (Int32.TryParse(idString, out int id))
            {
                string partText;

                string cacheKey = "PartText" + idString;
                if (context.Cache[cacheKey] != null)
                {
                    // Use cached version
                    partText = context.Cache[cacheKey].ToString();
                }
                else
                {
                    // Refresh cache
                    SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                    partText = service.GetItemText((int)ItemType.Segment, id);
                    context.Cache.Add(cacheKey, partText, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["ItemTextCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }

                context.Response.ContentType = "text/plain";
                context.Response.Cache.SetNoTransforms();
                context.Response.Write(partText);
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