using System;
using System.Configuration;
using System.Linq;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Static class providing strongly-typed access to appSettings from web.config
    /// </summary>
    public static class AppConfig
    {
        #region Boolean Settings

        /// <summary>
        /// Indicates whether the application is running in production mode
        /// </summary>
        public static bool IsProduction
        {
            get { return GetBool("IsProduction", true); }
        }

        /// <summary>
        /// Indicates whether to show new future features
        /// </summary>
        public static bool ShowNewFuture
        {
            get { return GetBool("ShowNewFuture", true); }
        }

        /// <summary>
        /// Indicates whether debug mode is enabled for search
        /// </summary>
        public static bool DebugSearch
        {
            get { return GetBool("DebugSearch", false); }
        }

        /// <summary>
        /// Indicates whether to use pre-generated PDFs
        /// </summary>
        public static bool UsePregeneratedPDFs
        {
            get { return GetBool("UsePregeneratedPDFs", true); }
        }

        /// <summary>
        /// Indicates whether to show annotations
        /// </summary>
        public static bool ShowAnnotations
        {
            get { return GetBool("ShowAnnotations", true); }
        }

        /// <summary>
        /// Indicates whether full-text search is enabled
        /// </summary>
        public static bool EnableFullTextSearch
        {
            get { return GetBool("EnableFullTextSearch", true); }
        }

        /// <summary>
        /// Indicates whether exceptions should be logged
        /// </summary>
        public static bool LogExceptions
        {
            get { return GetBool("LogExceptions", true); }
        }

        /// <summary>
        /// Indicates whether IP throttling is enabled
        /// </summary>
        public static bool IpThrottling
        {
            get { return GetBool("IpThrottling", true); }
        }

        #endregion

        #region String Settings

        /// <summary>
        /// Gets the IIIF state (e.g., "toggle")
        /// </summary>
        public static string IIIFState
        {
            get { return GetString("IIIFState", "toggle"); }
        }

        /// <summary>
        /// Gets the Elasticsearch server address
        /// </summary>
        public static string ElasticSearchServerAddress
        {
            get { return GetString("ElasticSearchServerAddress", "http://localhost:9200"); }
        }

        /// <summary>
        /// Gets the pipe-delimited list of search providers
        /// </summary>
        public static string SearchProviders
        {
            get { return GetString("SearchProviders", "BHL.Search.Elastic|BHL.Search.SQL|BHL.Search.Offline"); }
        }

        /// <summary>
        /// Gets the site services URL
        /// </summary>
        public static string SiteServicesURL
        {
            get { return GetString("SiteServicesURL", "https://localhost:7041"); }
        }

        #endregion

        #region Integer Settings

        /// <summary>
        /// Gets the monitoring threshold value
        /// </summary>
        public static int MonitorThreshold
        {
            get { return GetInt("MonitorThreshold", 20); }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets an array of search provider class names
        /// </summary>
        /// <returns>Array of search provider names</returns>
        public static string[] GetSearchProvidersArray()
        {
            return SearchProviders.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Indicates whether the site is configured to use ElasticSearch
        /// </summary>
        /// <returns>True if ElasticSearch is in use, False if not</returns>
        public static bool UseElasticSearch()
        {
            return (GetSearchProvidersArray().Contains<string>("BHL.Search.Elastic"));
        }

        #endregion

        #region Private Helper Methods

        private static string GetString(string key, string defaultValue = null)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        private static bool GetBool(string key, bool defaultValue = false)
        {
            string value = ConfigurationManager.AppSettings[key];
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            return defaultValue;
        }

        private static int GetInt(string key, int defaultValue = 0)
        {
            string value = ConfigurationManager.AppSettings[key];
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion
    }
}