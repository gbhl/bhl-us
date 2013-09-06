using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data = MOBOT.BHL.DataObjects;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class BrowseByTitleTag : BasePage
    {
        protected string Subject { get; set; }
        protected string sortBy { get; set; }
        protected int count { get; set; }
        protected int segmentcount { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            sortBy = (string)RouteData.Values["sort"];
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "";

            Subject = (string)RouteData.Values["subject"];
            Subject = Uri.UnescapeDataString(Subject);
            Subject = Subject.Replace('+', ' ');

            this.Title = (Subject == string.Empty ? this.Title : String.Format(ConfigurationManager.AppSettings["PageTitle"], Subject));
            ((SiteMaster)Page.Master).SetTweetMessage(Subject == string.Empty ? string.Empty : String.Format(ConfigurationManager.AppSettings["TweetMessage"], Subject));

            CustomGenericList<Data.SearchBookResult> resultList = this.GetTitleList(Subject);
            BookBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
            BookBrowse.Data = resultList;
            count = resultList.Count;

            CustomGenericList<Data.Segment> segmentList = this.GetSegmentList(Subject);
            SectionBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
            SectionBrowse.Data = segmentList;
            segmentcount = segmentList.Count;
        }

        /// <summary>
        /// Get the list of titles related to the specified subject.  First look in the cache; if not found
        /// get from the database and cache the result.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>List of SearchBookResults.</returns>
        private CustomGenericList<Data.SearchBookResult> GetTitleList(string keyword)
        {
            CustomGenericList<Data.SearchBookResult> titleList = new CustomGenericList<Data.SearchBookResult>();

            String cacheKey = "SubjectTitleBrowse" + keyword.Replace(" ", "");
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                titleList = (CustomGenericList<Data.SearchBookResult>)Cache[cacheKey];
            }
            else
            {
                // Refresh cache

                // Get the list of titles
                titleList = bhlProvider.TitleSelectByKeywordInstitutionAndLanguage(keyword, string.Empty, string.Empty);

                // Cache the list
                Cache.Add(cacheKey, titleList, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return titleList;
        }

        /// <summary>
        /// Get the list of segments related to the specified subject.  First look in the cache; if not found
        /// get from the database and cache the result.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>List of Segments.</returns>
        private CustomGenericList<Data.Segment> GetSegmentList(string keyword)
        {
            CustomGenericList<Data.Segment> segmentList = new CustomGenericList<Data.Segment>();

            String cacheKey = "SubjectSegmentBrowse" + keyword.Replace(" ", "");
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                segmentList = (CustomGenericList<Data.Segment>)Cache[cacheKey];
            }
            else
            {
                // Refresh cache

                // Get the list of segments
                segmentList = bhlProvider.SegmentSelectForKeyword(keyword);

                // Cache the list
                Cache.Add(cacheKey, segmentList, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return segmentList;
        }

        public string SetSortClass(string sortType)
        {
            if (sortBy != string.Empty)
            {
                return sortBy.Equals(sortType, StringComparison.OrdinalIgnoreCase) ? "activesort" : string.Empty;
            }
            else
            {   //set Title to active
                return sortType.Equals("title", StringComparison.OrdinalIgnoreCase) ? "activesort" : string.Empty;
            }
        }
    }
}