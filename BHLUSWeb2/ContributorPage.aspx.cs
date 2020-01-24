using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using Data = MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class ContributorPage : BrowsePage
    {
        BHLProvider provider = new BHLProvider();
        protected string Start { get; set; }
        protected string displayStart { get; set; }
        protected string sortBy { get; set; }
        protected Data.Institution contributor { get; set; }
        protected int count { get; set; }
        protected int segmentcount { get; set; }
        
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string institutionCode = (string)RouteData.Values["contributorid"];
            Start = (string)RouteData.Values["start"];
            sortBy = (string)RouteData.Values["sort"];
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "";

            contributor = (String.IsNullOrWhiteSpace(institutionCode)) ? null : provider.InstitutionSelectAuto(institutionCode);

            if (contributor != null)
            {
                ltlContributorStats.Visible = true;
                Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Browse " + contributor.InstitutionName);
                Data.Stats stats = this.GetStats(institutionCode);

                // If no start letter was specified, and we're not showing "ALL", then make sure we have no more than 500 books
                // in the collection.  If we DO, then default to showing books starting with "A".
                if (string.IsNullOrWhiteSpace(Start))
                {
                    Start = (stats.VolumeCount > 500 || stats.SegmentCount > 500) ? "A" : "All";
                }

                displayStart = (Start == "0") ? "a number" : "\"" + Start.ToUpper() + "\"";

                //build header
                litHeader.Text = string.Format(litHeader.Text, contributor.InstitutionName.Replace("(archive.org)", "").Trim());

                // Show the collection statistics
                ltlContributorStats.Text = string.Format(ltlContributorStats.Text,
                    stats.VolumeCount.ToString(), stats.TitleCount.ToString(), stats.PageCount.ToString(),
                    stats.SegmentCount.ToString());

                // Get data
                List<Data.SearchBookResult> bookList = GetInstitutionBooks(institutionCode, Start);
                List<Data.Segment> segmentList = GetInstitutionSegments(institutionCode, Start);

                count = bookList.Count;
                BookBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
                BookBrowse.Data = bookList;

                segmentcount = segmentList.Count;
                SectionBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
                SectionBrowse.Data = segmentList;
            }
            else
            {
                Response.Redirect("~/pagenotfound");
            }
        }

        private List<Data.SearchBookResult> GetInstitutionBooks(string institutionCode, string startString)
        {
            List<Data.SearchBookResult> list = new List<Data.SearchBookResult>();

            if (startString.ToUpper() == "ALL") startString = String.Empty;

            String cacheKey = "ContributorBookBrowse" + institutionCode + startString;
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                list = (List<Data.SearchBookResult>)(Cache[cacheKey]);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(institutionCode))
                {
                    if (startString == "0")
                    {
                        list = bhlProvider.TitleSelectByInstitutionAndStartsWithout(institutionCode, "[a-z]");
                    }
                    else
                    {
                        list = provider.TitleSelectByInstitutionAndStartsWith(institutionCode, startString);
                    }

                    // Cache the HTML fragment
                    Cache.Add(cacheKey, list, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            return list;
        }

        private List<Data.Segment> GetInstitutionSegments(string institutionCode, string startString)
        {
            List<Data.Segment> list = new List<Data.Segment>();

            if (startString.ToUpper() == "ALL") startString = String.Empty;

            String cacheKey = "ContributorSegmentBrowse" + institutionCode + startString;
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                list = (List<Data.Segment>)(Cache[cacheKey]);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(institutionCode))
                {
                    if (startString == "0")
                    {
                        list = bhlProvider.SegmentSelectByInstitutionAndStartsWithout(institutionCode, "[a-z]");
                    }
                    else
                    {
                        list = provider.SegmentSelectByInstitutionAndStartsWith(institutionCode, startString);
                    }

                    // Cache the HTML fragment
                    Cache.Add(cacheKey, list, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            return list;
        }

        private Data.Stats GetStats(string institutionCode)
        {
            Data.Stats stats = null;

            // Cache the results of the stats query for 24 hours
            String cacheKey = "StatsSelectContr" + institutionCode;
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (Data.Stats)Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = new BHLProvider().StatsSelectForInstitution(institutionCode);
                Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["StatsSelectQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return stats;
        }

        public string SetClass(string page)
        {
            if (Start == null) return string.Empty;
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