using MOBOT.BHL.DataObjects.Enum;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class GetItemText : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string itemIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["itemid"].ToString();
            if (itemIDString == null) return;

            int itemID;
            if (Int32.TryParse(itemIDString, out itemID))
            {
                string itemText = String.Empty;

                string cacheKey = "ItemText" + itemIDString;
                if (context.Cache[cacheKey] != null)
                {
                    // Use cached version
                    itemText = context.Cache[cacheKey].ToString();
                }
                else
                {
                    // Refresh cache
                    SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                    itemText = service.GetItemText((int)ItemType.Book, itemID);
                    context.Cache.Add(cacheKey, itemText, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["ItemTextCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }

                context.Response.ContentType = "text/plain";
                context.Response.Cache.SetNoTransforms();
                context.Response.Write(itemText);
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