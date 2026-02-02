using MOBOT.BHL.Web.Utilities;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2
{
    public class BrowseOutputCacheAttribute : OutputCacheAttribute
    {
        public BrowseOutputCacheAttribute()
        {
            this.Duration = AppConfig.BrowseQueryCacheTime * 60;
        }
    }
}