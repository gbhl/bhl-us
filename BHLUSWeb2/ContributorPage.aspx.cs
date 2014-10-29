using CustomDataAccess;
using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.Web.UI;
using Data = MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class ContributorPage : BrowsePage
    {
        BHLProvider provider = new BHLProvider();
        protected string Start { get; set; }
        protected string sortBy { get; set; }
        protected Data.Institution contributor { get; set; }
        protected int count { get; set; }
        
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string institutionCode = (string)RouteData.Values["contributorid"];
            Start = (string)RouteData.Values["start"];
            sortBy = (string)RouteData.Values["sort"];
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "";
            if (string.IsNullOrEmpty(Start)) Start = "All";

            contributor = provider.InstitutionSelectAuto(institutionCode);
            if (contributor != null)
            {
                ltlContributorStats.Visible = true;

                Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Browse " + contributor.InstitutionName);

                // If no start letter was specified, and we're not showing "ALL", then make sure we have no more than 500 books
                // in the collection.  If we DO, then default to showing books starting with "A".
                if (Start == "All")
                {
                    int contentCount = 0;
                    contentCount = provider.ItemCountByInstitution(contributor.InstitutionCode);
                    if (contentCount > 500) Start = "A";
                }

                CustomGenericList<Data.SearchBookResult> list = null;
                list = GetInstitutionList(contributor, institutionCode, Start);
                count = list.Count;

                //build header
                System.Text.StringBuilder html = new System.Text.StringBuilder();
                if (Start == "All") html.Append("All ");
                html.Append(list.Count).
                    Append(" title").Append(list.Count == 1 ? " " : "s ").
                    Append(" from ").Append(contributor.InstitutionName.Replace("(archive.org)", "").Trim());
                if (Start != String.Empty && Start != "All") html.Append(" beginning with ").Append(Start.ToUpper());
                ltlContributorHeader.Text = html.ToString();

                // Show the collection statistics
                Data.Stats stats = this.GetStats(institutionCode);
                ltlContributorStats.Text = string.Format(ltlContributorStats.Text,
                    stats.VolumeCount.ToString(), stats.TitleCount.ToString(), stats.PageCount.ToString());

                if (sortBy != string.Empty)
                {
                    Data.SearchBookResultComparer.CompareEnum sortByEnum = Data.SearchBookResultComparer.CompareEnum.Title;
                    switch (sortBy)
                    {
                        case "title": sortByEnum = Data.SearchBookResultComparer.CompareEnum.Title; break;
                        case "author": sortByEnum = Data.SearchBookResultComparer.CompareEnum.Author; break;
                        case "year": sortByEnum = Data.SearchBookResultComparer.CompareEnum.Year; break;
                    }
                    Data.SearchBookResultComparer comp = new Data.SearchBookResultComparer(sortByEnum, SortOrder.Ascending);
                    list.Sort(comp);
                }

                BookBrowse.Data = list;
                BookBrowse.DataBind();
            }
        }

        private CustomGenericList<Data.SearchBookResult> GetInstitutionList(Data.Institution contributor, string institutionCode, string startString)
        {
            CustomGenericList<Data.SearchBookResult> list = new CustomGenericList<Data.SearchBookResult>();

            if (startString.ToUpper() == "ALL") startString = String.Empty;

            String cacheKey = "ContributorBrowse" + institutionCode + startString;
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                list = (CustomGenericList<Data.SearchBookResult>)(Cache[cacheKey]);
            }
            else
            {
                if (contributor != null)
                {
                    list = provider.TitleSelectByInstitutionAndStartsWith(institutionCode, startString);

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