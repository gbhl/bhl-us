using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.services;
using MOBOT.BHL.Web2.Services;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace MOBOT.BHL.Web2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("glimpse.axd");

            routes.MapPageRoute("Default",
                "",
                "~/default.aspx");

            routes.MapPageRoute("Browse-Default",
                "browse",
                "~/default.aspx");

            routes.MapPageRoute("Contact",
                "contact",
                "~/Feedback.aspx");

            routes.Add("OpenUrl",
                new Route("openurl", new HttpHandlerRouteHandler<OpenUrlResolver>()));

            routes.MapPageRoute("OpenUrl-None",
                "openurlnone",
                "~/OpenUrlNone.aspx");

            routes.MapPageRoute("OpenUrl-Multiple",
                "openurlmultiple",
                "~/OpenUrlMultiple.aspx");

            routes.Add("OAI",
                new Route("oai", new HttpHandlerRouteHandler<oai2>()));

            routes.Add("itunes", new Route("itunesurss/collection/{id}", new HttpHandlerRouteHandler<MOBOT.BHL.Web2.Handlers.ITunesRSS>()));

            routes.MapPageRoute("Search",
                "search",
                "~/Search.aspx");

            routes.MapPageRoute("Advanced-Search-Tab",
                "advsearch/{searchtype}",
                "~/AdvancedSearch.aspx", false,
                new RouteValueDictionary
                        {
                            {"searchtype","book"}
                        }
                );

            routes.MapPageRoute("Advanced-Search",
                "advsearch",
                "~/AdvancedSearch.aspx");

            routes.MapPageRoute("Browse-TitleList",
                "browse/titles/{start}/{*sort}",
                "~/TitleList.aspx", false,
                new RouteValueDictionary
                {
                    {"start", "a"},
                    {"sort",""}
                }
                );

            routes.MapPageRoute("Browse-CreatorList",
                "browse/authors/{*start}",
                "~/CreatorList.aspx");

            routes.MapPageRoute("Browse-ContributorList",
                "browse/contributors",
                "~/BrowseContributors.aspx");

            routes.MapPageRoute("Contributor-Titles",
                "browse/contributor/{contributorid}/{start}/{sort}/",
                "~/ContributorPage.aspx",
                false,
                new RouteValueDictionary
                {
                    {"start", ""},
                    {"sort", ""},
                    {"contributorid", "-1"}
                }
                );

            routes.MapPageRoute("Browse-CollectionList",
                "browse/collections",
                "~/BrowseCollections.aspx");

            routes.MapPageRoute("Collection - Titles",
                "browse/collection/{collectionID}/{start}/{sort}/",
                "~/CollectionPage.aspx",
                false,
                new RouteValueDictionary
                    {
                        {"start", ""},
                        {"sort", ""},
                        {"collectionID", "-1"}
                    }
                );
            routes.MapPageRoute("Collection-details",
                "collection/{collectionid}",
                "~/CollectionDetails.aspx");

            routes.MapPageRoute("Browse-Year",
                "browse/year/{startdate}/{enddate}/{sort}",
                "~/BrowseByYear.aspx",
                false,
                new RouteValueDictionary
                    {
                        { "startdate", ConfigurationManager.AppSettings["browseByYearDefaultStart"] },
                        { "enddate", ConfigurationManager.AppSettings["browseByYearDefaultEnd"] },
                        { "sort", "title"}
                    },
                new RouteValueDictionary
                    {
                        { "startdate", @"\d{4}" },
                        { "enddate", @"\d{4}" }
                    });

            routes.MapPageRoute("Name",
                "name/{name}",
                "~/NameList.aspx");

            routes.Add("NameList",
                new Route("namelist", new HttpHandlerRouteHandler<NameListService>()));

            routes.MapPageRoute("NameDetail",
                "namedetail/{name}",
                "~/NameDetail.aspx");

            routes.Add("PageSummary",
                new Route("pagesummary", new HttpHandlerRouteHandler<PageSummaryService1>()));

            routes.MapPageRoute("IA",
                "ia/{iabarcode}",
                "~/TitlePage.aspx");

            routes.MapPageRoute("Title",
                "title/{titleid}",
                "~/TitlePage.aspx");

            routes.MapPageRoute("Item",
                "item/{itemid}",
                "~/TitlePage.aspx");

            routes.MapPageRoute("Page",
                "page/{pageid}",
                "~/TitlePage.aspx");

            routes.Add("PageThumb",
               new Route("pagethumb/{pageid},{w},{h}", new HttpHandlerRouteHandler<GetPageThumb>()));

            routes.Add("PageThumb-Default",
                new Route("pagethumb/{pageid}", new HttpHandlerRouteHandler<GetPageThumb>()));

            routes.Add("PageImage",
                new Route("pageimage/{pageid}", new HttpHandlerRouteHandler<GetPageImage>()));

            routes.Add("PageOCR",
                new Route("pageocr/{pageid}", new HttpHandlerRouteHandler<GetPageOcr>()));

            routes.MapPageRoute("Bibliography",
                "bibliography/{titleid}",
                "~/bibliography.aspx");

            routes.Add("MODSDownload",
                new Route("modsdownload/{id}", new HttpHandlerRouteHandler<MODSDownload>()));

            routes.Add("BibTeXDownload",
                new Route("bibtexdownload/{id}", new HttpHandlerRouteHandler<BibTeXDownload>()));

            routes.Add("EndNoteDownload",
                new Route("endnotedownload/{id}", new HttpHandlerRouteHandler<EndNoteDownload>()));

            routes.Add("NameListDownload",
                new Route("namelistdownload", new HttpHandlerRouteHandler<NameListDownloadService>()));

            routes.Add("PDF",
                    new Route("pdf{folder}/{filename}", new HttpHandlerRouteHandler<PDFDownload>()));

            routes.MapPageRoute("Biblioselect",
                "biblioselect/{itemid}",
                "~/BiblioSelect.aspx");

            routes.MapPageRoute("Item-Detail",
                "itemdetails/{itemid}",
                "~/ItemPage.aspx");

            routes.MapPageRoute("Segment-Detail",
                "section/{segmentid}",
                "~/SectionPage.aspx");

            routes.MapPageRoute("Part-Detail",
                "part/{segmentid}",
                "~/SectionPage.aspx");

            routes.Add("GeneratePdf",
                new Route("generatepdf", new HttpHandlerRouteHandler<GeneratePdf>()));

            routes.MapPageRoute("Recent", "recent/{top}/{lang}/{inst}", "~/recent.aspx", false, new RouteValueDictionary { { "top", "100" }, { "lang", "" }, { "inst", "" } });
            routes.MapPageRoute("RecentRSS", "recentrss/{top}/{lang}/{inst}", "~/recentrss.aspx", false, new RouteValueDictionary { { "top", "100" }, { "lang", "" }, { "inst", "" } });

            routes.MapPageRoute("Creator",
                "creator/{creatorid}/{sort}",
                "~/CreatorPage.aspx", false,
                new RouteValueDictionary
                    {
                        {"creatorid",0},
                        {"sort",""}
                    }
                );

            routes.MapPageRoute("Subject",
                "subject/{subject}/{sort}",
                "~/BrowseByTitleTag.aspx", false,
                new RouteValueDictionary
                    {
                        {"subject",""},
                        {"sort",""}
                    }
                );

            routes.MapPageRoute("Error-TitleNotFound",
                "titlenotfound",
                "~/TitleNotFound.aspx");

            routes.MapPageRoute("Error-AuthorNotFound",
                "authornotfound",
                "~/AuthorNotFound.aspx");

            routes.MapPageRoute("Error-ItemNotFound",
                "itemnotfound",
                "~/ItemNotFound.aspx");

            routes.MapPageRoute("Error-ItemUnavailable",
                "itemunavailable",
                "~/ItemUnavailable.aspx");

            routes.MapPageRoute("Error-PageNotFound",
                "pagenotfound",
                "~/PageNotFound.aspx");

            routes.MapPageRoute("Error-General",
                "error",
                "~/Error.aspx");

            routes.MapPageRoute("CatchAll",
                "{*url}",
                "~/PageNotFound.aspx");
        }
    }
}