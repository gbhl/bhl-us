using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class PartialController : Controller
    {
        public ActionResult _AlertMessage()
        {
            String alertMessage = String.Empty;

            String cacheKey = "AlertMessage";
            if (HttpContext.Cache[cacheKey] != null)
            {
                // Use cached version
                alertMessage = HttpContext.Cache[cacheKey].ToString();
            }
            else
            {
                // Refresh cache
                alertMessage = System.IO.File.ReadAllText(Request.PhysicalApplicationPath + "\\alertmsg.txt");
                HttpContext.Cache.Add(cacheKey, alertMessage, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["AlertMessageCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            // If we have a message, display it
            ViewData["AlertMessage"] = alertMessage;
            ViewData["DisplayStyle"] = (alertMessage.Length > 0) ? "block" : "none";

            return PartialView();
        }

        public ActionResult _CollectionStats()
        {
            CustomGenericList<EntityCount> stats = new CustomGenericList<EntityCount>();

            // Cache the results of the institutions query for 24 hours
            String cacheKey = "StatsSelect";
            if (HttpContext.Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (CustomGenericList<EntityCount>)HttpContext.Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = new BHLProvider().EntityCountSelectLatest();
                HttpContext.Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["StatsSelectQueryCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            foreach (EntityCount stat in stats)
            {
                if (stat.EntityCountTypeID == EntityCount.EntityType.ActiveTitles) ViewData["ActiveTitles"] = stat.CountValue.ToString("0,0");
                if (stat.EntityCountTypeID == EntityCount.EntityType.ActiveItems) ViewData["ActiveItems"] = stat.CountValue.ToString("0,0");
                if (stat.EntityCountTypeID == EntityCount.EntityType.ActivePages) ViewData["ActivePages"] = stat.CountValue.ToString("0,0");
            }

            return PartialView();
        }

        public ActionResult _TwitterFeed()
        {
            string cacheKey = "BioDivLibraryTwitterFeedItems";
            StringBuilder twitterFeedContents = new StringBuilder();

            if (HttpContext.Cache[cacheKey] != null)
            {
                // Used the cached version of the feed contents
                twitterFeedContents.Append(HttpContext.Cache[cacheKey].ToString());
            }
            else
            {
                // Refresh the cached twitter feed
                try
                {
                    // Retreive the latest from Twitter

                    // This code follows the application-only authentication methods described
                    // in the Twitter documentation: https://dev.twitter.com/docs/auth/application-only-auth

                    // Base64-encode the Twitter credentials
                    string bearerCredentials = string.Format("{0}:{1}",
                                                    ConfigurationManager.AppSettings["TwitterConsumerKey"],
                                                    ConfigurationManager.AppSettings["TwitterConsumerSecret"]);
                    string bearerCredentialsBase64 = Convert.ToBase64String(
                        new System.Text.ASCIIEncoding().GetBytes(bearerCredentials));
                    string accessToken = string.Empty;

                    // Request the access token to be used with subsequent requests
                    string jsonResponse = string.Empty;
                    using (WebClient webClient = new WebClient())
                    {
                        //webClient.Headers.Add("Host: api.twitter.com");
                        webClient.Headers.Add(string.Format("Authorization: Basic {0}", bearerCredentialsBase64));
                        webClient.Headers.Add("Content-Type: application/x-www-form-urlencoded;charset=UTF-8");
                        //webClient.Headers.Add("Content-Length: 29");
                        jsonResponse = webClient.UploadString("https://api.twitter.com/oauth2/token", "POST", "grant_type=client_credentials");
                    }

                    JObject json = JObject.Parse(jsonResponse);
                    accessToken = (string)json["access_token"];


                    // Use the access token to perform the Twitter search
                    using (WebClient webClient = new WebClient())
                    {
                        //webClient.Headers.Add("Host: api.twitter.com");
                        webClient.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));
                        jsonResponse = webClient.DownloadString(ConfigurationManager.AppSettings["BHLTwitterFeedURL"]);
                    }
                    json = JObject.Parse(jsonResponse);

                    // Parse the search results
                    JArray statuses = (JArray)json["statuses"];

                    // Format the feed contents for web display
                    foreach (object status in statuses)
                    {
                        // Get the data for this tweet
                        JObject jsonResult = JObject.Parse(status.ToString());

                        string createdAt = (string)jsonResult["created_at"];

                        // Reformat the date (there's probably a better way to do this)
                        string year = createdAt.Substring(createdAt.Length - 4);
                        createdAt = createdAt.Substring(0, createdAt.Length - 4);
                        int split = createdAt.IndexOf(' ', 8);
                        createdAt = string.Format("{0} {1} {2}", createdAt.Substring(0, split), year, createdAt.Substring(split + 1));

                        string age = string.Empty;
                        string fromUser = (string)jsonResult["user"]["screen_name"];
                        string fromUserLink = fromUser;
                        string id = (string)jsonResult["id_str"];
                        string text = (string)jsonResult["text"];

                        // Determine how long ago this tweet was posted
                        DateTime createdAtDate;
                        if (DateTime.TryParse(createdAt, out createdAtDate))
                        {
                            DateTime now = DateTime.Now;
                            TimeSpan ts = now - createdAtDate;
                            if (ts.Days > 0) age = ts.Days.ToString() + " days ago";
                            else if (ts.Hours > 0) age = ts.Hours.ToString() + " hour" + (ts.Hours == 1 ? "" : "s") + " ago";
                            else if (ts.Minutes > 0) age = ts.Minutes.ToString() + " minutes ago";
                            else age = ts.Seconds.ToString() + " seconds ago";
                        }

                        // Wrap all hashtags in the text in <a> tags
                        var hashTags = from h in jsonResult["entities"]["hashtags"]
                                       select h;
                        foreach (var h in hashTags)
                        {
                            string hashText = "#" + (string)h["text"];
                            string replaceHashText = string.Format(
                                "<a class='tweet-url hashtag' href='http://twitter.com/search?q={0}' target='_blank'>{1}</a>",
                                Server.UrlEncode(hashText), hashText);
                            text = text.Replace(hashText, replaceHashText);
                        }

                        // Wrap all urls in the text in <a> tags
                        var urls = from u in jsonResult["entities"]["urls"]
                                   select u;
                        foreach (var u in urls)
                        {
                            string displayUrl = (string)u["display_url"];
                            string urlText = (string)u["url"];
                            string replaceUrlText = string.Format(
                                "<a rel='nofollow' href='{0}' target='_blank'>{1}</a>",
                                urlText, displayUrl);
                            text = text.Replace(urlText, replaceUrlText);
                        }

                        // Wrap all user mentions in the text in <a> tags
                        var userMentions = from um in jsonResult["entities"]["user_mentions"]
                                           select um;
                        foreach (var um in userMentions)
                        {
                            string screenName = (string)um["screen_name"];
                            string replaceScreenName = string.Format(
                                "<a class='tweet-url username' href='http://twitter.com/{0}' target='_blank'>{1}</a>",
                                screenName, "@" + screenName);
                            text = text.Replace("@" + screenName, replaceScreenName);
                        }

                        // Format the link to the Twitter page for the user posting the tweet
                        if (fromUser.Length > 0)
                        {
                            fromUserLink = string.Format(
                                "<a class='twtr-user' href='http://twitter.com/intent/user?screen_name={0}' target='_blank'>{1}</a>",
                                fromUser, fromUser);
                        }

                        // Format the age and favorite links
                        string ageLink = string.Format(
                            "<a class='twtr-timestamp' href='http://twitter.com/{0}/status/{1}' time='{2}' target='_blank'>{3}</a>",
                            fromUser, id, createdAt, age);

                        string favLink = string.Format(
                            "<a class='twtr-fav' href='http://twitter.com/intent/favorite?tweet_id={0}' target='_blank'>favorite</a>",
                            id);

                        // Build the complete entry for this tweet
                        twitterFeedContents.Append("<div class='twtr-tweet'>").Append("<div class='twtr-tweet-wrap'>");
                        twitterFeedContents.Append("<p>");
                        twitterFeedContents.Append(fromUserLink).Append(" ").Append(text);
                        twitterFeedContents.Append("</p>");
                        twitterFeedContents.Append("<p>").Append(ageLink).Append(" · ").Append(favLink).Append("</p>");
                        twitterFeedContents.Append("</div>").Append("</div>");
                    }
                }
                catch
                {
                    twitterFeedContents.Append("<p>Unable to access <a href='http://twitter.com/BioDivLibrary' target='_blank'>BioDivLibrary</a> twitter feed.</p>");
                }

                // Cache the feed contents
                HttpContext.Cache.Add(cacheKey, twitterFeedContents, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["TwitterFeedCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            // Output the twitter feed contents
            ViewData["TwitterFeed"] = twitterFeedContents.ToString();

            return PartialView();
        }
    }
}