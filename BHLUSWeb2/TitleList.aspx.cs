using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;


namespace MOBOT.BHL.Web2
{
    public partial class TitleList : BrowsePage
    {
        protected string Start { get; set; }
        protected string displayStart { get; set; }
        protected string sortBy { get; set; }
        protected int Count { get; set; }
        protected int SegmentCount { get; set; }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Start = (string)RouteData.Values["start"];
            sortBy = (string)RouteData.Values["sort"];
            if (string.IsNullOrWhiteSpace(Start) || Start.Length != 1 || !Regex.IsMatch(Start, @"[A-Za-z0]")) Response.Redirect("~/browse/titles/a");
            displayStart = (Start == "0") ? "a number" : "\"" + Start.ToUpper() + "\"";
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "";

            main.Page.Title = string.Format("Titles beginning with {0} - Biodiversity Heritage Library", displayStart);

            // Get the book data
            List<SearchBookResult> searchBookResultList = new List<SearchBookResult>();

            String cacheKey = "TitleBookBrowse" + Start ;
            if (Cache[cacheKey] != null)
            {
                searchBookResultList = (List<SearchBookResult>)Cache[cacheKey];
            }
            else
            {
                if (Start == "0")
                {
                    searchBookResultList = bhlProvider.TitleSelectByNameNotLike("[a-z]");
                }
                else
                {
                    searchBookResultList = bhlProvider.TitleSelectByNameLike(Start);
                }
                Cache.Add(cacheKey, searchBookResultList, null,
                    DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            BookBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
            BookBrowse.Data = searchBookResultList;
            Count = searchBookResultList.Count;

            // Get the segment data
            List<Segment> segmentResultList = new List<Segment>();

            cacheKey = "TitleSegmentBrowse" + Start;
            if (Cache[cacheKey] != null)
            {
                segmentResultList = (List<Segment>)Cache[cacheKey];
            }
            else
            {
                if (Start == "0")
                {
                    segmentResultList = bhlProvider.SegmentSelectByTitleNotLike("[a-z]");
                }
                else
                {
                    segmentResultList = bhlProvider.SegmentSelectByTitleLike(Start);
                }
                Cache.Add(cacheKey, segmentResultList, null,
                    DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            SectionBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
            SectionBrowse.Data = segmentResultList;
            SegmentCount = segmentResultList.Count;
        }

        public string SetClass(string page)
        {
            return Start.Equals(page, StringComparison.OrdinalIgnoreCase) ? "active" : string.Empty;
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