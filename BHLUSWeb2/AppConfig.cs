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
        // Feature toggles
        public static bool IsProduction => GetBool("IsProduction", true);
        public static bool ShowNewFuture => GetBool("ShowNewFuture", false);
        public static bool DebugSearch => GetBool("DebugSearch", false);
        public static bool UsePregeneratedPDFs => GetBool("UsePregeneratedPDFs", true);
        public static bool ShowAnnotations => GetBool("ShowAnnotations", true);
        public static bool EnableFullTextSearch => GetBool("EnableFullTextSearch", true);
        public static bool LogExceptions => GetBool("LogExceptions", true);
        public static bool IpThrottling => GetBool("IpThrottling", true);
        public static string IIIFState => GetString("IIIFState", "toggle");

        /// <summary>
        /// Site services URL
        /// </summary>
        public static string SiteServicesURL => GetString("SiteServicesURL");

        // URLs and paths
        public static string BaseUrl => GetString("BaseUrl");
        public static string BibPageUrl => GetString("BibPageUrl");
        public static string ItemPageUrl => GetString("ItemPageUrl");
        public static string ItemPdfUrl => GetString("ItemPdfUrl");
        public static string PagePageUrl => GetString("PagePageUrl");
        public static string PartPageUrl => GetString("PartPageUrl");
        public static string PartPdfUrl => GetString("PartPdfUrl");
        public static string ImageNotFoundPath => GetString("ImageNotFoundPath");
        public static string ImageNotFoundThumbPath => GetString("ImageNotFoundThumbPath");
        public static string PdfUrl => GetString("PdfUrl");

        // OAI
        public static string OAIBaseUrl => GetString("OAIBaseUrl");
        public static string OAIRepositoryName => GetString("OAIRepositoryName");
        public static string OAIAdminEmail => GetString("OAIAdminEmail");
        public static string OAIIdentifierNamespace => GetString("OAIIdentifierNamespace");
        public static int OAIMaxListSets => GetInt("OAIMaxListSets", 500);
        public static int OAIMaxListRecords => GetInt("OAIMaxListRecords", 100);
        public static int OAIMaxListIdentifiers => GetInt("OAIMaxListIdentifiers", 200);
        public static string OAIMetadataFormats => GetString("OAIMetadataFormats");

        // Google / analytics
        public static string GoogleAnalyticsTrackingID => GetString("GoogleAnalyticsTrackingID");

        // Gemini / issue tracking
        public static string GeminiURL => GetString("GeminiURL");
        public static string GeminiUser => GetString("GeminiUser");
        public static string GeminiPassword => GetString("GeminiPassword");
        public static string GeminiDesc => GetString("GeminiDesc");
        public static int GeminiProjectId => GetInt("GeminiProjectId", 0);
        public static int GeminiScanProjectID => GetInt("GeminiScanProjectID", 0);
        public static int GeminiScanCustomFieldIdOCLC => GetInt("GeminiScanCustomFieldIdOCLC", 0);
        public static int GeminiScanCustomFieldIdYearStart => GetInt("GeminiScanCustomFieldIdYearStart", 0);
        public static int GeminiComponentIdScanRequest => GetInt("GeminiComponentIdScanRequest", 0);
        public static int GeminiComponentIdFeedback => GetInt("GeminiComponentIdFeedback", 0);
        public static int GeminiTypeIdScanRequest => GetInt("GeminiTypeIdScanRequest", 0);
        public static int GeminiTypeIdTechFeedback => GetInt("GeminiTypeIdTechFeedback", 0);
        public static int GeminiTypeIdSuggestion => GetInt("GeminiTypeIdSuggestion", 0);
        public static int GeminiTypeIdBiblioIssue => GetInt("GeminiTypeIdBiblioIssue", 0);
        public static int GeminiTypeIdTitle => GetInt("GeminiTypeIdTitle", 0);
        public static int GeminiStatusId => GetInt("GeminiStatusId", 0);
        public static int GeminiPriorityId => GetInt("GeminiPriorityId", 0);
        public static int GeminiSeverityId => GetInt("GeminiSeverityId", 0);
        public static int GeminiResolutionId => GetInt("GeminiResolutionId", 0);
        public static int GeminiRequestSourceUserId => GetInt("GeminiRequestSourceUserId", 0);

        // Debug / logging
        public static string ExceptionLogLevel => GetString("ExceptionLogLevel");
        public static string DebugValue => GetString("DebugValue");
        public static int MonitorThreshold => GetInt("MonitorThreshold", 20);

        // Browse defaults
        public static int DefaultBrowseNumPerPage => GetInt("DefaultBrowseNumPerPage", 250);

        // Page formatting
        public static string PageTitle => GetString("PageTitle");

        // File locations
        public static string OCRTextLocation => GetString("OCRTextLocation");
        public static string ItemTextLocation => GetString("ItemTextLocation");

        // Twitter / donation / feeds
        public static string TwitterConsumerKey => GetString("TwitterConsumerKey");
        public static string TwitterConsumerSecret => GetString("TwitterConsumerSecret");
        public static string BHLTwitterFeedUrl => GetString("BHLTwitterFeedUrl");
        public static string DonateUrl => GetString("DonateUrl");
        public static string FundraiseUpCampaignCode => GetString("FundraiseUpCampaignCode");

        // New future / news / newsletters
        public static string NewFutureNewsBannerText => GetString("NewFutureNewsBannerText");
        public static string NewFutureNewsText => GetString("NewFutureNewsText");
        public static string NewFutureNewsUrl => GetString("NewFutureNewsUrl");
        public static string NewsletterSignupUrl => GetString("NewsletterSignupUrl");

        // Wiki pages and developer links
        public static string WikiPageAbout => GetString("WikiPageAbout");
        public static string WikiPagePermissions => GetString("WikiPagePermissions");
        public static string WikiPageHelp => GetString("WikiPageHelp");
        public static string WikiPageContribute => GetString("WikiPageContribute");
        public static string WikiPageDeveloper => GetString("WikiPageDeveloper");
        public static string WikiPageMembers => GetString("WikiPageMembers");
        public static string WikiPageCopyright => GetString("WikiPageCopyright");
        public static string WikiPageFAQ => GetString("WikiPageFAQ");
        public static string WikiPageTitleUnavailable => GetString("WikiPageTitleUnavailable");
        public static string WikiPageItemUnavailable => GetString("WikiPageItemUnavailable");
        public static string WikiPageSubmissionGuide => GetString("WikiPageSubmissionGuide");
        public static string WikiPageAPI => GetString("WikiPageAPI");
        public static string WikiPageDataDisclaimer => GetString("WikiPageDataDisclaimer");

        // Blog settings
        public static string ProjectUpdatesFeedLocation => GetString("projectUpdatesFeedLocation");

        // Attribution / cache times
        public static int LanguageListQueryCacheTime => GetInt("LanguageListQueryCacheTime", 1440);
        public static int CollectionListQueryCacheTime => GetInt("CollectionListQueryCacheTime", 0);
        public static int CollectionStatsQueryCacheTime => GetInt("CollectionStatsQueryCacheTime", 120);
        public static int InstitutionStatsQueryCacheTime => GetInt("InstitutionStatsQueryCacheTime", 120);
        public static int FeaturedCollectionCacheTime => GetInt("FeaturedCollectionCacheTime", 5);
        public static int TwitterFeedCacheTime => GetInt("TwitterFeedCacheTime", 10);
        public static int StatsSelectQueryCacheTime => GetInt("StatsSelectQueryCacheTime", 20);
        public static int BrowseQueryCacheTime => GetInt("BrowseQueryCacheTime", 0);
        public static int FlickrThumbListCacheTime => GetInt("FlickrThumbListCacheTime", 10);
        public static int AlertMessageCacheTime => GetInt("AlertMessageCacheTime", 1);
        public static int ItemTextCacheTime => GetInt("ItemTextCacheTime", 10);
        public static int AuthorMetadataCacheTime => GetInt("AuthorMetadataCacheTime", 5);

        // Search settings
        public static string ElasticSearchServerAddress => GetString("ElasticSearchServerAddress");
        public static string SearchProviders => GetString("SearchProviders", "BHL.Search.Elastic|BHL.Search.SQL|BHL.Search.Offline");
        public static string ESDefaultIndex => GetString("ESDefaultIndex");
        public static string ESAllIndex => GetString("ESAllIndex");
        public static string ESCatalogIndex => GetString("ESCatalogIndex");
        public static string ESItemsIndex => GetString("ESItemsIndex");
        public static string ESPagesIndex => GetString("ESPagesIndex");
        public static string ESAuthorsIndex => GetString("ESAuthorsIndex");
        public static string ESKeywordsIndex => GetString("ESKeywordsIndex");
        public static string ESNamesIndex => GetString("ESNamesIndex");
        public static int MaximumDefaultResults => GetInt("MaximumDefaultResults", 100);
        public static int MaximumExpandedResults => GetInt("MaximumExpandedResults", 500);
        public static int PublicationResultPageSize => GetInt("PublicationResultPageSize", 10);
        public static int FacetSize => GetInt("FacetSize", 100);
        public static int AuthorResultPageSize => GetInt("AuthorResultPageSize", 20);
        public static int KeywordResultPageSize => GetInt("KeywordResultPageSize", 20);
        public static int NameResultPageSize => GetInt("NameResultPageSize", 20);
        public static int PageResultPageSize => GetInt("PageResultPageSize", 5000);

        public static string PublicationResultDefaultSort => GetString("PublicationResultDefaultSort", "Score");
        public static string PublicationResultDefaultSortDirection => GetString("PublicationResultDefaultSortDirection", "Descending");
        public static string AuthorResultDefaultSort => GetString("AuthorResultDefaultSort", "PrimaryAuthor");
        public static string AuthorResultDefaultSortDirection => GetString("AuthorResultDefaultSortDirection", "Ascending");
        public static string KeywordResultDefaultSort => GetString("KeywordResultDefaultSort", "Keyword");
        public static string KeywordResultDefaultSortDirection => GetString("KeywordResultDefaultSortDirection", "Ascending");
        public static string NameResultDefaultSort => GetString("NameResultDefaultSort", "Name");
        public static string NameResultDefaultSortDirection => GetString("NameResultDefaultSortDirection", "Ascending");
        public static string PageResultDefaultSort => GetString("PageResultDefaultSort", "Sequence");
        public static string PageResultDefaultSortDirection => GetString("PageResultDefaultSortDirection", "Ascending");

        // ReCaptcha
        public static string ReCaptchaVerifyUrl => GetString("ReCaptchaVerifyUrl");
        public static string ReCaptchaSiteKey => GetString("ReCaptchaSiteKey");
        public static string ReCaptchaSecretKey => GetString("ReCaptchaSecretKey");

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