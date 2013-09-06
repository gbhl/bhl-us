using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Web2
{
    public partial class BrowseByYear : BrowsePage
    {
        protected int StartDate { get; set; }
        protected int EndDate { get; set; }
        protected int Count { get; set; }
        protected int SegmentCount { get; set; }
        protected string sortBy { get; set; }
        protected string sortBaseURL { get; set; }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            StartDate = int.Parse((string)RouteData.Values["startdate"]);
            EndDate = int.Parse((string)RouteData.Values["enddate"]);
            sortBy = (string)RouteData.Values["sort"];

            //set base URL for sorting
            if (string.IsNullOrEmpty(StartDate.ToString())) {
                sortBaseURL = "/browse/year";
            }else
            {
                sortBaseURL = string.Format("/browse/year/{0}/{1}", StartDate, EndDate);
            }

            main.Page.Title = string.Format("Browsing Titles between {0} and {1} - Biodiversity Heritage Library", StartDate, EndDate);            

            // Get the book data
            CustomGenericList<SearchBookResult> searchBookResultList = new CustomGenericList<SearchBookResult>();

            String cacheKey = "YearBookBrowse" + StartDate + EndDate;
            if (Cache[cacheKey] != null)
            {
                searchBookResultList = (CustomGenericList<SearchBookResult>)Cache[cacheKey];
            }
            else
            {
                searchBookResultList = bhlProvider.TitleSelectByDateRangeAndInstitution(StartDate, EndDate, string.Empty, string.Empty);

                Cache.Add(cacheKey, searchBookResultList, null,
                    DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            Count = searchBookResultList.Count;
            if (sortBy != string.Empty) BookBrowse.SortBy = sortBy;
            BookBrowse.Data = searchBookResultList;

            // Get the segment data
            CustomGenericList<Segment> segmentResultList = new CustomGenericList<Segment>();

            cacheKey = "YearSegmentBrowse" + StartDate + EndDate;
            if (Cache[cacheKey] != null)
            {
                segmentResultList = (CustomGenericList<Segment>)Cache[cacheKey];
            }
            else
            {
                segmentResultList = bhlProvider.SegmentSelectByDateRange(StartDate.ToString(), EndDate.ToString());

                Cache.Add(cacheKey, segmentResultList, null,
                    DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            SegmentCount = segmentResultList.Count;
            if (sortBy != string.Empty) SectionBrowse.SortBy = sortBy;
            SectionBrowse.Data = segmentResultList;
        }

        public string SetClass(string page)
        {
            Boolean isActive = false;
            string pageUrl = Request.Url.AbsolutePath.ToLower();

            if (pageUrl.Contains("1450/1580") && (page.ToLower().Equals("browse/year")))
            {
                isActive = true;
            }
            else if (pageUrl.EndsWith(page.ToLower()))
            {
                isActive = true;
            }
            else if (pageUrl.EndsWith(page.ToLower() + "/title"))
            {
                isActive = true;
            }
            else if (pageUrl.EndsWith(page.ToLower() + "/author"))
            {
                isActive = true;
            }
            else if (pageUrl.EndsWith(page.ToLower() + "/year"))
            {
                isActive = true;
            }
             
            return isActive ? "active" : string.Empty; 
        }
    
        public string SetSortClass(string sortType)
        {
            if (!string.IsNullOrEmpty(sortBy))
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