using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using RestSharp;
using CustomDataAccess;

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
                Page p = provider.PageSelectAuto(pageID);
                String outText = string.Empty;

                // Make sure we found an active page
                if (p != null)
                {
                    CustomGenericList<Title> titles = provider.TitleSelectByItem(p.ItemID);
                    if (titles == null)
                    {
                        return;
                    }
                    String title = String.Empty;
                    foreach(Title t in titles)
                    {
                        title = t.ShortTitle;
                        break;
                    }

                    IRestResponse response;
                    //first create the forum - doesn't matter if it already exists
                    Dictionary<String, String> APIParameters = new Dictionary<string, string>();
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

                    //vote on this thread to add to BHL's disqus curation
                    APIParameters = new Dictionary<string, string>();
                    APIParameters.Add("vote", "1");
                    APIParameters.Add("forum", "bhl-item-" + p.ItemID);
                    APIParameters.Add("thread:ident", "bhl-page-" + p.PageID);
                    response = this.DiqsusCall("threads/vote.json", Method.POST, APIParameters);

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
        private List<string> _bots;
        public List<string> bots
        {
            get
            {
                if (_bots == null)
                {
                    _bots = new List<string>(){"008","ABACHOBot","Accoona-AI-Agent","AddSugarSpiderBot","AnyApexBot","Arachmo","B-l-i-t-z-B-O-T","Baiduspider","BecomeBot",
    "BeslistBot","BillyBobBot","Bimbot","Bingbot","BlitzBOT","boitho.com-dc","boitho.com-robot","btbot","CatchBot","Cerberian Drtrs",
    "Charlotte","ConveraCrawler","cosmos","Covario IDS","DataparkSearch","DiamondBot","Discobot","Dotbot","EARTHCOM.info","EmeraldShield.com WebBot",
    "envolk[ITS]spider","EsperanzaBot","Exabot","FAST Enterprise Crawler","FAST-WebCrawler","FDSE robot","FindLinks","FurlBot","FyberSpider",
    "g2crawler","Gaisbot","GalaxyBot","genieBot","Gigabot","Girafabot","Googlebot-Image","Googlebot","GurujiBot","HappyFunBot","hl_ftien_spider",
    "Holmes","htdig","iaskspider","ia_archiver","iCCrawler","ichiro","igdeSpyder","IRLbot","IssueCrawler","Jaxified Bot","Jyxobot","KoepaBot",
    "L.webis","LapozzBot","Larbin","LDSpider","LexxeBot","Linguee Bot","LinkWalker","lmspider","lwp-trivial","mabontland","magpie-crawler",
    "Mediapartners-Google","MJ12bot","MLBot","Mnogosearch","mogimogi","MojeekBot","Moreoverbot","Morning Paper","msnbot","MSRBot","MVAClient",
    "mxbot","NetResearchServer","NetSeer Crawler","NewsGator","NG-Search","nicebot","noxtrumbot","Nusearch Spider","NutchCVS","Nymesis","obot",
    "oegp","omgilibot","OmniExplorer_Bot","OOZBOT","Orbiter","PageBitesHyperBot","Peew","polybot","Pompos","PostPost","Psbot","PycURL","Qseero",
    "Radian6","RAMPyBot","RufusBot","SandCrawler","SBIder","ScoutJet","Scrubby","SearchSight","Seekbot","semanticdiscovery","Sensis Web Crawler",
    "SEOChat::Bot","SeznamBot","Shim-Crawler","ShopWiki","Shoula robot","silk","Sitebot","Snappy","sogou spider","Sosospider","Speedy Spider",
    "Sqworm","StackRambler","suggybot","SurveyBot","SynooBot","Teoma","TerrawizBot","TheSuBot","Thumbnail.CZ robot","TinEye","truwoGPS","TurnitinBot",
    "TweetedTimes Bot","TwengaBot","updated","Urlfilebot","Vagabondo","VoilaBot","Vortex","voyager","VYU2","webcollage","Websquash.com","wf84",
    "WoFindeIch Robot","WomlpeFactory","Xaldon_WebSpider","yacy","Yahoo! Slurp","Yahoo! Slurp China","YahooSeeker","YahooSeeker-Testing","YandexBot",
    "YandexImages","YandexMetrika","Yasaklibot","Yeti","YodaoBot","yoogliFetchAgent","YoudaoBot","Zao","Zealbot","zspider","ZyBorg", "teoma" ,"bot", "spieder", "crawler", "walker", "ask", "bing", "msn"};
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
