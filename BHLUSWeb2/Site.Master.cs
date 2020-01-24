using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;

namespace MOBOT.BHL.Web2
{

    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string bodyID {get; set;}
        protected void Page_Load(object sender, EventArgs e)
        {
            bool debugMode = new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request);
            if (debugMode) Page.Title = "***DEBUG MODE*** " + Page.Title;

            if (!this.IsPostBack)
            {
                DisplayAlertMessage();
            }

            List<EntityCount> stats = this.GetStats();
            foreach (EntityCount stat in stats)
            {
                if (stat.EntityCountTypeID == EntityCount.EntityType.ActiveTitles) titlesOnlineLiteral.Text = stat.CountValue.ToString("0,0");
                if (stat.EntityCountTypeID == EntityCount.EntityType.ActiveItems) volumesOnlineLiteral.Text = stat.CountValue.ToString("0,0");
                if (stat.EntityCountTypeID == EntityCount.EntityType.ActivePages) pagesOnlineLiteral.Text = stat.CountValue.ToString("0,0");
            }
        }

        private List<EntityCount> GetStats()
        {
            List<EntityCount> stats = new List<EntityCount>();

            // Cache the results of the institutions query for 24 hours
            String cacheKey = "StatsSelect";
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (List<EntityCount>)Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = new BHLProvider().EntityCountSelectLatest();
                Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["StatsSelectQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return stats;
        }

        /// <summary>
        /// Reads the alert message from a text file and caches it.  Using the cache
        /// and file system is done for performance reasons (testing showed that file 
        /// system access was 4 times faster than a database lookup).
        /// </summary>
        private void DisplayAlertMessage()
        {
            String alertMessage = String.Empty;

            String cacheKey = "AlertMessage";
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                alertMessage = Cache[cacheKey].ToString();
            }
            else
            {
                // Refresh cache
                alertMessage = System.IO.File.ReadAllText(Request.PhysicalApplicationPath + "\\alertmsg.txt");
                Cache.Add(cacheKey, alertMessage, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["AlertMessageCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            // If we have a message, display it
            litAlertMessage.Text = alertMessage;
            divAlert.Visible = (alertMessage.Length > 0);
        }
    }
}
