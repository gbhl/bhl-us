using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using Data = MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class CollectionPage : BrowsePage
    {
        BHLProvider provider = new BHLProvider();
        protected string Start { get; set; }
        protected string sortBy { get; set; }
        protected Data.Collection collection { get; set; }
        //protected int count { get; set; }
        protected string collectionPath { get; set; } // returns friendly or ID

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Start = (string)RouteData.Values["start"];
            sortBy = (string)RouteData.Values["sort"];

            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "";
            }

            int collectionID;
            // Parse CollectionId - may be "nice" name

            List<Data.Collection> collections = null;
            collections = (string.IsNullOrWhiteSpace((string)RouteData.Values["collectionid"])) ?
                new List<Data.Collection>() :
                provider.CollectionSelectByUrl((string)RouteData.Values["collectionid"]);

            if (collections.Count > 0)
            {
                int.TryParse(collections[0].CollectionID.ToString(), out collectionID);
            }
            else
            {
                if (!int.TryParse((string)RouteData.Values["collectionid"], out collectionID))
                {
                    Response.Redirect("~/collectionnotfound");
                }
            }

            if (string.IsNullOrEmpty(Start)) Start = "All";

            collection = provider.CollectionSelectAuto(collectionID);

            if (collection != null)
            {
                ltlCollectionStats.Visible = true;

                if (collection.CollectionTarget == "iTunes")
                {
                    ltlCollectionStats.Text = "<p>The contents of this collection are only viewable at iTunesU.</p>";
                    if (!string.IsNullOrEmpty(collection.ITunesURL)) ltlCollectionStats.Text += "<p><a href='" + collection.ITunesURL + "'>Click here to view the collection at iTunesU.</a></p>";
                }
                else
                {
                    Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Browse " + collection.CollectionName);

                    // If no start letter was specified, and we're not showing "ALL", then make sure we have no more than 500 books
                    // in the collection.  If we DO, then default to showing books starting with "A".
                    //if (Start == string.Empty && !forceShowAll)
                    if (Start == "All")
                    {
                        int contentCount = 0;
                        if (collection.CanContainTitles == 1) contentCount = provider.TitleCountByCollection(collection.CollectionID);
                        if (collection.CanContainItems == 1) contentCount = provider.ItemCountByCollection(collection.CollectionID);
                        if (contentCount > 500) Start = "A";
                    }

                    List<Data.SearchBookResult> list = null;
                    list = GetCollectionList(collection, collectionID, Start);
                    //count = list.Count;

                    //build header
                    System.Text.StringBuilder html = new System.Text.StringBuilder();
                    if (Start.ToLower() == "all") html.Append("All ");
                    html.Append(list.Count).
                        Append((collection.CanContainTitles == 1) ? " title" : " item").Append(list.Count == 1 ? " " : "s ").
                        Append(" in ").Append(collection.CollectionName);
                    if (Start != String.Empty && Start.ToLower() != "all")
                    {
                        html.Append(" beginning with ").Append((Start == "0") ? "a number" : Start.ToUpper());
                    }
                    ltlCollectionHeader.Text = html.ToString();

                    // Show the collection statistics
                    Data.Stats stats = this.GetStats(collectionID);
                    ltlCollectionStats.Text = string.Format(ltlCollectionStats.Text,
                        stats.VolumeCount.ToString(), stats.TitleCount.ToString(), stats.PageCount.ToString());

                    if (sortBy != string.Empty)
                    {
                        System.Collections.Generic.IComparer<Data.SearchBookResult> comp = new Data.SearchBookResultTitleComparer();
                        switch (sortBy)
                        {
                            case "title": comp = new Data.SearchBookResultTitleComparer(); break;
                            case "author": comp = new Data.SearchBookResultAuthorComparer(); break;
                            case "year": comp = new Data.SearchBookResultYearComparer(); break;
                        }

                        list.Sort(comp);
                    }

                    BookBrowse.Data = list;
                    BookBrowse.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/collectionnotfound");
            }
        }

        private List<Data.SearchBookResult> GetCollectionList(Data.Collection collection, int collectionID, string startString)
        {
            List<Data.SearchBookResult> list = new List<Data.SearchBookResult>();

            if (startString.ToUpper() == "ALL") startString = String.Empty;
            if (collection.CanContainItems > 0) BookBrowse.ShowVolume = true;

            String cacheKey = "CollectionBrowse" + collectionID + startString;
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                list = (List<Data.SearchBookResult>)(Cache[cacheKey]);
            }
            else
            {
                if (collection != null)
                {
                    // We don't expect a Collection to contain both items & titles, but allow for both
                    if (collection.CanContainItems > 0)
                    {
                        if (startString == "0")
                        {
                            list = bhlProvider.ItemSelectByCollectionAndStartsWithout(collectionID, "[a-z]");
                        }
                        else
                        {
                            list = provider.ItemSelectByCollectionAndStartsWith(collectionID, startString);
                        }
                    }

                    if (collection.CanContainTitles > 0)
                    {
                        if (startString == "0")
                        {
                            list = bhlProvider.TitleSelectByCollectionAndStartsWithout(collectionID, "[a-z]");
                        }
                        else
                        {
                            list = provider.TitleSelectByCollectionAndStartsWith(collectionID, startString);
                        }
                    }

                    ////// Cache the HTML fragment
                    Cache.Add(cacheKey, list, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            return list;
        }

        private Data.Stats GetStats(int collectionID)
        {
            Data.Stats stats = null;

            // Cache the results of the stats query for 24 hours
            String cacheKey = "StatsSelectColl" + collectionID.ToString();
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (Data.Stats)Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = new BHLProvider().StatsSelectForCollection(collectionID);
                Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["StatsSelectQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return stats;
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