﻿using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.services;
using MOBOT.BHL.Web2.Services;
using System;
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

            routes.Add("OpenUrl", new Route("openurl", new HttpHandlerRouteHandler<OpenUrlResolver>()));

            routes.MapPageRoute("OpenUrl-None", "openurlnone", "~/OpenUrlNone.aspx");

            routes.MapPageRoute("OpenUrl-Multiple", "openurlmultiple", "~/OpenUrlMultiple.aspx");

            routes.Add("API3", new Route("api3", new HttpHandlerRouteHandler<api3.api3>()));

            routes.Add("OAI", new Route("oai", new HttpHandlerRouteHandler<oai2>()));

            routes.Add("itunes", new Route("itunesurss/collection/{id}", new HttpHandlerRouteHandler<MOBOT.BHL.Web2.Handlers.ITunesRSS>()));

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

            routes.MapPageRoute("Name", "name/{name}", "~/NameList.aspx");

            routes.Add("NameList", new Route("namelist", new HttpHandlerRouteHandler<NameListService>()));

            routes.MapPageRoute("NameDetail", "namedetail/{name}", "~/NameDetail.aspx");

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

            routes.Add("PageThumb", new Route("pagethumb/{pageid},{w},{h}", new HttpHandlerRouteHandler<GetPageThumb>()));

            routes.Add("PageThumb-Default", new Route("pagethumb/{pageid}", new HttpHandlerRouteHandler<GetPageThumb>()));

            routes.Add("PageImage", new Route("pageimage/{pageid}", new HttpHandlerRouteHandler<GetPageImage>()));

            routes.Add("PageText", new Route("pagetext/{pageid}", new HttpHandlerRouteHandler<GetPageOcr>()));

            routes.Add("PageOCR", new Route("pageocr/{pageid}", new HttpHandlerRouteHandler<GetPageOcrRedirect>()));

            routes.Add("ItemText", new Route("itemtext/{itemid}", new HttpHandlerRouteHandler<GetItemText>()));

            routes.Add("ItemPdf", new Route("itempdf/{itemid}", new HttpHandlerRouteHandler<GetItemPdf>()));

            routes.Add("ItemImages", new Route("itemimages/{itemid}", new HttpHandlerRouteHandler<GetItemImages>()));

            routes.Add("PartText", new Route("parttext/{id}", new HttpHandlerRouteHandler<GetPartText>()));

            routes.Add("PartPdf", new Route("partpdf/{id}", new HttpHandlerRouteHandler<GetPartPdf>()));

            routes.Add("PartImages", new Route("partimages/{id}", new HttpHandlerRouteHandler<GetPartImages>()));

            routes.MapPageRoute("Bibliography", "bibliography/{titleid}", "~/bibliography.aspx");

            routes.Add("MODSDownload", new Route("modsdownload/{type}/{id}", new HttpHandlerRouteHandler<MODSDownload>()));
            routes.Add("MODSDownloadOld", new Route("modsdownload/{id}", new HttpHandlerRouteHandler<MODSDownload>()));

            routes.Add("BibTeXDownload", new Route("bibtexdownload/{type}/{id}", new HttpHandlerRouteHandler<BibTeXDownload>()));
            routes.Add("BibTeXDownloadOld", new Route("bibtexdownload/{id}", new HttpHandlerRouteHandler<BibTeXDownload>()));

            routes.Add("RISDownload", new Route("risdownload/{type}/{id}", new HttpHandlerRouteHandler<RISDownload>()));
            routes.Add("RISDownloadOld", new Route("risdownload/{id}", new HttpHandlerRouteHandler<RISDownload>()));

            routes.Add("CSLDownload", new Route("csldownload/{type}/{id}", new HttpHandlerRouteHandler<CSLDownload>()));

            routes.Add("NameListDownload", new Route("namelistdownload", new HttpHandlerRouteHandler<NameListDownloadService>()));

            routes.Add("PDF", new Route("pdf{folder}/{filename}", new HttpHandlerRouteHandler<PDFDownload>()));

            routes.MapPageRoute("Biblioselect", "biblioselect/{itemid}", "~/BiblioSelect.aspx");

            routes.MapPageRoute("Item-Detail", "itemdetails/{itemid}", "~/ItemPage.aspx");

            routes.MapPageRoute("Segment-Detail", "section/{segmentid}", "~/SectionPage.aspx");

            routes.MapPageRoute("Part-Detail", "part/{segmentid}", "~/SectionPage.aspx");

            routes.Add("GeneratePdf", new Route("generatepdf", new HttpHandlerRouteHandler<GeneratePdf>()));

            routes.MapPageRoute("Recent", "recent/{top}/{lang}/{inst}", "~/recent.aspx", false, new RouteValueDictionary { { "top", "100" }, { "lang", "" }, { "inst", "" } });
            routes.MapPageRoute("RecentRSS", "recentrss/{top}/{lang}/{inst}", "~/recentrss.aspx", false, new RouteValueDictionary { { "top", "100" }, { "lang", "" }, { "inst", "" } });

            routes.MapRoute("BrowseCreator", "creator/{creatorid}/{sort}", new { controller = "Creator", action = "Index", sort = "title" });
            routes.MapRoute("BrowseSubject", "subject/{subject}/{sort}", new { controller = "Subject", action = "Index", sort = "title" });

            routes.MapPageRoute("Error-TitleNotFound", "titlenotfound", "~/TitleNotFound.aspx");
            routes.MapPageRoute("Error-TitleUnavailable", "titleunavailable", "~/TitleUnavailable.aspx");
            routes.MapPageRoute("Error-AuthorNotFound", "authornotfound", "~/AuthorNotFound.aspx");
            routes.MapPageRoute("Error-ItemNotFound", "itemnotfound", "~/ItemNotFound.aspx");
            routes.MapPageRoute("Error-ItemUnavailable", "itemunavailable", "~/ItemUnavailable.aspx");
            routes.MapPageRoute("Error-PageNotFound", "pagenotfound", "~/PageNotFound.aspx");
            routes.MapPageRoute("Error-General", "error", "~/Error.aspx");

            routes.MapPageRoute("CatchAll", "{*url}", "~/PageNotFound.aspx");
        }
    }
}