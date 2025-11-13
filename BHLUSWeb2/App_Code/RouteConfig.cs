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
            routes.MapPageRoute("Default", "", "~/default.aspx");

            routes.MapPageRoute("Browse-Default", "browse", "~/default.aspx");

            routes.MapPageRoute("Contact", "contact", "~/Feedback.aspx");

            routes.MapRoute("PermissionsRedirect", "permissions", new { controller = "Redirect", action = "Permissions" });
            routes.MapRoute("AboutRedirect", "about", new { controller = "Redirect", action = "About" });

            routes.MapRoute("OpenUrl", "openurl", new { controller = "OpenUrl", action = "OpenUrlResolver" });
            routes.MapPageRoute("OpenUrl-None", "openurlnone", "~/OpenUrlNone.aspx");
            routes.MapPageRoute("OpenUrl-Multiple", "openurlmultiple", "~/OpenUrlMultiple.aspx");

            routes.MapRoute("API3", "api3", new { controller = "Api", action = "Api3Handler" });
            routes.MapRoute("API2-http", "api2/httpQuery.ashx", new { controller = "Api", action = "Api2Handler" });
            routes.MapRoute("API2", "api2", new { controller = "Api", action = "Api2Handler" });

            routes.Add("OAI", new Route("oai", new HttpHandlerRouteHandler<oai2>()));

            routes.MapRoute("ServiceDefault", "service/{action}", new { controller = "Service" });

            if (ConfigurationManager.AppSettings["UseElasticSearch"] == "true")
            {
                // Use new routes to MVC views if elasticsearch is enabled
                routes.MapRoute("Search", "search", new { controller = "Search", action = "Index" });
                routes.MapRoute("SearchPage", "search/pages", new { controller = "Search", action = "Pages" });

                routes.MapRoute("Advanced-Search-Tab", "advsearch/{searchtype}",
                    new { controller = "Search", action = "Advanced" },
                    new RouteValueDictionary { { "searchtype", "book" } });
                routes.MapRoute("Advanced-Search", "advsearch", new { controller = "Search", action = "Advanced" });
            }
            else
            {
                // Elasticsearch is turned off, so use the old routes
                routes.MapPageRoute("Search", "search", "~/Search.aspx");

                routes.MapPageRoute("Advanced-Search-Tab", "advsearch/{searchtype}", "~/AdvancedSearch.aspx", false,
                new RouteValueDictionary { {"searchtype","book"} } );

                routes.MapPageRoute("Advanced-Search", "advsearch", "~/AdvancedSearch.aspx");
            }

            routes.MapRoute("BrowseTitles", "browse/titles/{start}/{sort}", new { controller = "Browse", action = "Titles", start = "a", sort="title" });
            routes.MapRoute("BrowseAuthors", "browse/authors/{start}", new { controller = "Browse", action = "Authors", start = "a" });
            routes.MapRoute("BrowseYear", "browse/year/{start}/{end}/{sort}", new { controller = "Browse", action = "Year", start = 1450, end = 1580, sort = "title" });
            routes.MapRoute("BrowseCollection", "browse/collection/{id}/{start}/{sort}", new { controller = "Browse", action = "Collection", start = "all", sort = "title" });
            routes.MapRoute("BrowseContributor", "browse/contributor/{id}/{start}/{sort}", new { controller = "Browse", action = "Contributor", start = "all", sort = "title" });

            routes.MapPageRoute("Browse-ContributorList", "browse/contributors", "~/BrowseContributors.aspx");
            routes.MapPageRoute("Browse-CollectionList", "browse/collections", "~/BrowseCollections.aspx");
            routes.MapPageRoute("Collection-details", "collection/{collectionid}", "~/CollectionDetails.aspx");

            routes.MapRoute("Name", "name/{name}", new { controller = "Name", action = "Index" });
            routes.MapRoute("NameList", "namelist", new { controller = "Name", action = "NameList" });
            routes.MapRoute("NameListDownload", "namelistdownload", new { controller = "Name", action = "NameListDownload" });
            routes.MapRoute("NameDetail", "namedetail/{name}", new { controller = "NameDetail", action = "Index" });

            routes.Add("PageSummary", new Route("pagesummary", new HttpHandlerRouteHandler<PageSummaryService1>()));

            routes.MapPageRoute("IA", "ia/{iabarcode}", "~/TitlePage.aspx");

            routes.MapPageRoute("Title", "title/{titleid}", "~/TitlePage.aspx");

            routes.MapPageRoute("Segment", "segment/{segmentid}", "~/TitlePage.aspx");

            if (ConfigurationManager.AppSettings["IIIFState"] == "off") // IIIF disabled
            {
                routes.MapPageRoute("Item", "item/{itemid}", "~/TitlePage.aspx");
                routes.MapPageRoute("Page", "page/{pageid}", "~/TitlePage.aspx");
            }
            else if (ConfigurationManager.AppSettings["IIIFState"] == "on")  // IIIF enabled
            {
                routes.MapRoute("IIIFManifest", "iiif/{itemId}/manifest", new { controller = "IIIF", action = "Manifest" });
                routes.MapRoute("IIIFTextManifest", "iiif/{itemId}/text/{pageSeq}", new { controller = "IIIF", action = "TextManifest" });
                routes.MapRoute("IIIFNameManifest", "iiif/{itemId}/names/{pageSeq}", new { controller = "IIIF", action = "NameManifest" });
                routes.MapRoute("Item", "item/{itemId}", new { controller = "IIIF", action = "Item" });
                routes.MapRoute("Page", "page/{pageId}", new { controller = "IIIF", action = "Page" });
            }
            else // toggle
            {
                routes.MapRoute("IIIFManifest", "iiif/{itemId}/manifest", new { controller = "IIIF", action = "Manifest" });
                routes.MapRoute("IIIFTextManifest", "iiif/{itemId}/text/{pageSeq}", new { controller = "IIIF", action = "TextManifest" });
                routes.MapRoute("IIIFNameManifest", "iiif/{itemId}/names/{pageSeq}", new { controller = "IIIF", action = "NameManifest" });
                routes.MapRoute("IIIFItem", "iiif/item/{itemId}", new { controller = "IIIF", action = "Item" });
                routes.MapRoute("IIIFPage", "iiif/page/{pageId}", new { controller = "IIIF", action = "Page" });
                routes.MapPageRoute("Item", "item/{itemid}", "~/TitlePage.aspx");
                routes.MapPageRoute("Page", "page/{pageid}", "~/TitlePage.aspx");
            }

            routes.MapRoute("PageThumb", "pagethumb/{pageid},{w},{h}", new { controller = "Page", action = "GetPageThumb" });
            routes.MapRoute("PageThumb-Default", "pagethumb/{pageid}", new { controller = "Page", action = "GetPageThumb" });
            routes.MapRoute("PageImage", "pageimage/{pageid}", new { controller = "Page", action = "GetPageImage" });

            routes.MapRoute("PageText", "pagetext/{pageid}", new { controller = "Page", action = "GetPageText" });
            routes.MapRoute("PageOCR", "pageocr/{pageid}", new { controller = "Page", action = "GetPageText" });

            routes.MapRoute("ItemText", "itemtext/{itemid}", new { controller = "Item", action = "GetItemText" });
            routes.MapRoute("ItemPdf", "itempdf/{itemid}", new { controller = "Item", action = "GetItemPdf" });
            routes.MapRoute("ItemImages", "itemimages/{itemid}", new { controller = "Item", action = "GetItemImages" });

            routes.MapRoute("PartText", "parttext/{partid}", new { controller = "Part", action = "GetPartText" });
            routes.MapRoute("PartPdf", "partpdf/{id}", new { controller = "Part", action = "GetPartPdf" });
            routes.MapRoute("PartImages", "partimages/{id}", new { controller = "Part", action = "GetPartImages" });
            routes.MapRoute("Part-Detail", "part/{partid}", new { controller = "Part", action = "Index" });

            routes.MapRoute("Bibliography", "bibliography/{titleid}", new { controller = "Bibliography", action = "Index" });

            routes.MapRoute("MODSDownload", "modsdownload/{type}/{id}", new { controller = "Download", action = "MODS" });
            routes.MapRoute("MODSDownloadOld", "modsdownload/{id}", new { controller = "Download", action = "MODS" });

            routes.MapRoute("BibTeXDownload", "bibtexdownload/{type}/{id}", new { controller = "Download", action = "BibTeX" });
            routes.MapRoute("BibTeXDownloadOld", "bibtexdownload/{id}", new { controller = "Download", action = "BibTeX" });

            routes.MapRoute("RISDownload", "risdownload/{type}/{id}", new { controller = "Download", action = "RIS" });
            routes.MapRoute("RISDownloadOld", "risdownload/{id}", new { controller = "Download", action = "RIS" });

            routes.MapRoute("CSLDownload", "csldownload/{type}/{id}", new { controller = "Download", action = "CSL" });

            routes.MapRoute("PDF", "pdf{folder}/{filename}", new { controller = "Download", action = "PDF" });

            routes.MapPageRoute("Biblioselect", "biblioselect/{itemid}", "~/BiblioSelect.aspx");

            routes.MapPageRoute("Item-Detail", "itemdetails/{itemid}", "~/ItemPage.aspx");

            routes.Add("GeneratePdf", new Route("generatepdf", new HttpHandlerRouteHandler<GeneratePdf>()));

            routes.MapPageRoute("Recent", "recent/{top}/{lang}/{inst}", "~/recent.aspx", false, new RouteValueDictionary { { "top", "100" }, { "lang", "" }, { "inst", "" } });
            routes.MapPageRoute("RecentRSS", "recentrss/{top}/{lang}/{inst}", "~/recentrss.aspx", false, new RouteValueDictionary { { "top", "100" }, { "lang", "" }, { "inst", "" } });

            routes.MapRoute("BrowseCreator", "creator/{creatorid}/{sort}", new { controller = "Creator", action = "Index", sort = "title" });
            routes.MapRoute("BrowseSubject", "subject/{subject}/{sort}", new { controller = "Subject", action = "Index", sort = "title" });

            routes.MapRoute("Error-TitleNotFound", "titlenotfound", new { controller = "TitleNotFound", action = "Index" });
            routes.MapRoute("Error-TitleUnavailable", "titleunavailable", new { controller = "TitleUnavailable", action = "Index" });
            routes.MapRoute("Error-AuthorNotFound", "authornotfound", new { controller = "AuthorNotFound", action = "Index" });
            routes.MapRoute("Error-ItemNotFound", "itemnotfound", new { controller = "ItemNotFound", action = "Index" });
            routes.MapRoute("Error-ItemUnavailable", "itemunavailable", new { controller = "ItemUnavailable", action = "Index" });
            routes.MapRoute("Error-PageNotFound", "pagenotfound", new { controller = "PageNotFound", action = "Index"});
            routes.MapRoute("Error-General", "error", new { controller = "Error", action = "Index" });

            routes.MapRoute("CatchAll", "{*url}", new { controller = "PageNotFound", action = "Index" });
        }
    }
}