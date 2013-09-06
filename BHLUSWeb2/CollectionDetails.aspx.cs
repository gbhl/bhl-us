﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class CollectionDetails : BrowsePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
            int collectionID;

                CustomDataAccess.CustomGenericList<MOBOT.BHL.DataObjects.Collection> collections =
                new MOBOT.BHL.Server.BHLProvider().CollectionSelectByUrl((string)RouteData.Values["collectionid"]);
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
             //   ((Main)Page.Master).SetTweetMessage(this.GetTweetText(collection));
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
            else if (collection.CollectionTarget == "iTunes")
            {
                html = "<p>This collection is only viewable at iTunesU.</p>";
                if (!string.IsNullOrEmpty(collection.ITunesURL)) html += "<p><a href='" + collection.ITunesURL + "'>Click here to view the collection at iTunesU.</a></p>";
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