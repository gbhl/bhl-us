using System;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2
{
    public class BrowseOutputCacheAttribute : OutputCacheAttribute
    {
        public BrowseOutputCacheAttribute()
        {
            this.Duration = Convert.ToInt32(ConfigurationManager.AppSettings["BrowseQueryCacheTime"]) * 60;
        }
    }
}