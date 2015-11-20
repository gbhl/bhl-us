using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using RestSharp;
using CustomDataAccess;
using System.IO;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Communicates with Disqus API to set up discussion threads and forums for books and pages
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetPageComments : IHttpHandler
    {

        // Called by AJAX to create a thread for a page when user clicks through
        // Sets up new forum for the book if necessary, votes up the thread to appear on BHL's disqus recommended content
        public void ProcessRequest(HttpContext context)
        {
            String pageIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["pageid"].ToString();
            if (pageIDString == null) return;

            int pageID;
            if (Int32.TryParse(pageIDString, out pageID))
            {
                BHLProvider provider = new BHLProvider();
                Page p = provider.PageMetadataSelectByPageID(pageID);
                String outText = string.Empty;

                // Make sure we found an active page
                if (p != null)
                {

                    IRestResponse response;
                    Dictionary<String, String> APIParameters = new Dictionary<string, string>();
                    if (String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["vote"]))
                    {

                        CustomGenericList<Title> titles = provider.TitleSelectByItem(p.ItemID);
                        if (titles == null)
                        {
                            return;
                        }
                        String title = String.Empty;
                        foreach (Title t in titles)
                        {
                            title = t.ShortTitle;
                            break;
                        }

                        //make sure title is not too long for disqus
                        if (title.Length > 59)
                        {
                            title = title.Substring(0, 56) + "...";
                        }

                        //first create the forum - doesn't matter if it already exists
                        APIParameters.Add("name", title);
                        APIParameters.Add("short_name", "bhl-item-" + p.ItemID);
                        APIParameters.Add("website", String.Format(ConfigurationManager.AppSettings["ItemPageUrl"], p.ItemID));
                        response = this.DiqsusCall("forums/create.json", Method.POST, APIParameters);

                        //next create the thread for the page
                        APIParameters = new Dictionary<string, string>();
                        APIParameters.Add("forum", "bhl-item-" + p.ItemID);
                        APIParameters.Add("url", String.Format(ConfigurationManager.AppSettings["PagePageUrl"], p.PageID));
                        APIParameters.Add("identifier", "bhl-page-" + p.PageID);
                        APIParameters.Add("title", p.WebDisplay);
                        response = this.DiqsusCall("threads/create.json", Method.POST, APIParameters);
                        
                    }
                    else
                    {
                        //A new comments has been posted
                        //Vote up the thread
                        APIParameters = new Dictionary<string, string>();
                        APIParameters.Add("vote", "1");
                        APIParameters.Add("forum", "bhl-item-" + p.ItemID);
                        APIParameters.Add("thread:ident", "bhl-page-" + p.PageID);
                        response = this.DiqsusCall("threads/vote.json", Method.POST, APIParameters);

                        //update our cache of pages that have comments
                        IRestResponse pageComments = GetForumThreads(p.ItemID.ToString());
                        BHLProvider bhlProvider = new BHLProvider();
                        if (pageComments.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            bhlProvider.DisqusCacheDeleteByItemID(p.ItemID);
                            CustomGenericList<Page> pages = bhlProvider.PageMetadataSelectByItemID(p.ItemID);
                            foreach (Page page in pages)
                            {
                                if (pageComments.Content.Contains("\"bhl-page-" + page.PageID.ToString() + "\""))
                                {
                                    //todo: replace with a proper json deserializer, could be brittle
                                    Regex reg = new Regex(@"\[""bhl-page-" + page.PageID.ToString() + @"""\]\,(.+?)""posts""\:([0-9]+)\,");
                                    Match match = reg.Match(pageComments.Content);
                                    if (match.Success)
                                    {
                                        page.NumComments = Int32.Parse(match.Groups[2].Value);
                                    }
                                    else
                                    {
                                        page.NumComments = 1;
                                    }
                                }
                                //do insert
                                if (page.NumComments > 0)
                                {
                                    DisqusCache cachedPage = new DisqusCache();
                                    cachedPage.PageID = page.PageID;
                                    cachedPage.ItemID = p.ItemID;
                                    cachedPage.Count = page.NumComments;
                                    bhlProvider.DisqusCacheInsertAuto(cachedPage);
                                }
                            }
                        }
                    }
                    outText = "OK";
                }
                else
                {
                    outText = "Page unavailable";
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(outText);
            }
        }

        // Get threads for a forum, to enumerate pages with comments
        public IRestResponse GetForumThreads(String ItemID)
        {
            Dictionary<String, String> APIParameters = new Dictionary<string, string>();
            APIParameters.Add("forum", "bhl-item-" + ItemID);
            return this.DiqsusCall("threads/list.json", Method.GET, APIParameters);
        }

        // Perform a disqus API call
        private IRestResponse DiqsusCall(String APIPath, Method APIMethod, Dictionary<String, String> APIParameters)
        {
            if (!IsBot(HttpContext.Current.Request.UserAgent))
            {
                var disqus = new RestClient("https://disqus.com");
                var request = new RestRequest("/api/3.0/" + APIPath, APIMethod);
                request.AddParameter("api_key", ConfigurationManager.AppSettings["DisqusPublicKey"]);
                request.AddParameter("access_token", ConfigurationManager.AppSettings["DisqusAccessToken"]);
                foreach (KeyValuePair<String, String> parameter in APIParameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
                IRestResponse response = disqus.Execute(request);

                return response;
            }
            else
            {
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        // Simple bot detection to reduce Disqus API calls
        // Adapted from: https://github.com/udelblue/BotDetectionApplication
        private IEnumerable<string> _bots;
        public IEnumerable<string> bots
        {
            get
            {
                if (_bots == null)
                {
                    _bots = File.ReadLines(HttpContext.Current.Server.MapPath("../bot_list.txt"));
                }

                return _bots;
            }

        }

        private Boolean IsBot(string useragent)
        {
            foreach (string b in bots)
            {
                if (useragent.Contains(b))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
