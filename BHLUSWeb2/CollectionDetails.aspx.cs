using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class CollectionDetails : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int collectionID;

                List<Collection> collections = new BHLProvider().CollectionSelectByUrl((string)RouteData.Values["collectionid"]);
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

            

            Collection collection = new MOBOT.BHL.Server.BHLProvider().CollectionSelectAuto(collectionID);

           if (collection != null)
           {
                ltlHtmlContent.Text = this.GetHtmlContent(collection);
                this.Title = this.GetPageTitle(collection);
           }
        }

        /// <summary>
        /// Get the collection data from the cache or (if necessary) the database
        /// </summary>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        private Collection GetCollection(int collectionID)
        {
            Collection collection = null;

            String cacheKey = "CollectionPage" + collectionID.ToString();
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                collection = (Collection)Cache[cacheKey];
            }
            else
            {
                // Refresh cache

                // Get the collection
                collection = bhlProvider.CollectionSelectAuto(collectionID);

                // Cache the html content
                if (collection != null)
                {
                    Cache.Add(cacheKey, collection, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["BrowseQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            return collection;
        }

        private string GetHtmlContent(Collection collection)
        {
            string html = string.Empty;

            if (collection == null)
            {
                html = "Collection not found";
            }
            else
            {
                html = collection.HtmlContent;
            }

            return html;
        }

        private string GetPageTitle(Collection collection)
        {
            string title = (collection == null ? this.Title : String.Format(ConfigurationManager.AppSettings["PageTitle"], collection.CollectionName));
            return title;
        }

        private string GetTweetText(Collection collection)
        {
            string tweet = (collection == null ? string.Empty : string.Format(ConfigurationManager.AppSettings["TweetMessage"], collection.CollectionName));
            return tweet;
        }
    }
}