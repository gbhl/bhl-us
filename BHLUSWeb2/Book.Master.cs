using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Web2
{
    public partial class Book : MasterPage
    {
        public string searchTerm { get; set; }
        public string holdingInstitution { get; set; }
        public string itemID { get; set; }
        public string sponsor { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool debugMode = new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).IsDebugMode(Response, Request);
            if (debugMode) Page.Title = "***DEBUG MODE*** " + Page.Title;

            if (!this.IsPostBack)
            {
                DisplayAlertMessage();
            }
        }

        protected void btnSearchSubmit_Click(object sender, EventArgs e)
        {
            string searchType = (rdoSearchTypeF.Checked) ? rdoSearchTypeF.Value : rdoSearchTypeC.Value;
            Response.Redirect(string.Format("~/search?searchTerm={0}&stype={1}", Server.UrlEncode(tbSearchTerm.Text), Server.UrlEncode(searchType)));
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