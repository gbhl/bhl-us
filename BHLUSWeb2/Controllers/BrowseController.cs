using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse/Authors
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Authors(string start, int? bpg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(AppConfig.DefaultBrowseNumPerPage);

            AuthorsBrowseModel model = new AuthorsBrowseModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Start = start;
            model.AuthorPage = bpg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;

            var authorResults = bhlProvider.AuthorSelectByNameLikePaged(model.Start, model.AuthorPage, model.NumPerPage);
            model.TotalAuthors = authorResults.Item1;
            model.AuthorResults = authorResults.Item2;

            return View(model);
        }

        // GET: Browse/Collection
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Collection(string id, string start, string sort, int? bpg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(AppConfig.DefaultBrowseNumPerPage);

            CollectionBrowseModel model = new CollectionBrowseModel();
            BHLProvider bhlProvider = new BHLProvider();

            int collectionID;
            List<Collection> collections = (string.IsNullOrWhiteSpace(id) ? new List<Collection>() : bhlProvider.CollectionSelectByUrl(id));
            if (collections.Count > 0)
            {
                int.TryParse(collections[0].CollectionID.ToString(), out collectionID);
            }
            else
            {
                if (!int.TryParse(id, out collectionID))
                { 
                    return new RedirectResult("~/collectionnotfound");
                }
            }

            model.Collection = bhlProvider.CollectionSelectAuto(collectionID);
            if (model.Collection == null)
            {
                return new RedirectResult("~/collectionnotfound");
            }

            if (model.Collection.CanContainItems > 0) model.ShowVolume = true;
            model.Start = start;
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;

            var bookResults = GetCollectionBooks(model.Collection, model.Start, model.BookPage, model.NumPerPage, model.Sort);
            model.TotalBooks = bookResults.Item1;
            model.BookResults = bookResults.Item2;

            Stats stats;
            string cacheKey = "CollectionStats" + model.Collection.CollectionID.ToString();
            if (HttpContext.Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (Stats)HttpContext.Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = new BHLProvider().StatsSelectForCollection(model.Collection.CollectionID);
                HttpContext.Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(AppConfig.CollectionStatsQueryCacheTime),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            model.NumTitles = stats.TitleCount;
            model.NumBooks = stats.VolumeCount;
            model.NumPages = stats.PageCount;

            return View(model);
        }

        private Tuple<int, List<SearchBookResult>> GetCollectionBooks(Collection collection, string startString, int pageNum, int numPages, string sort)
        {
            Tuple<int, List<SearchBookResult>> list = new Tuple<int, List<SearchBookResult>>(0, new List<SearchBookResult>());
            if (startString.ToUpper() == "ALL") startString = string.Empty;

            if (collection != null)
            {
                BHLProvider bhlProvider = new BHLProvider();

                if (collection.CanContainItems > 0)
                {
                    list = bhlProvider.ItemSelectByCollectionAndStartsWith(collection.CollectionID, (startString == "0" ? "[^a-z]" : startString), pageNum, numPages, sort);
                }
                if (collection.CanContainTitles > 0)
                {
                    list = bhlProvider.TitleSelectByCollectionAndStartsWith(collection.CollectionID, (startString == "0" ? "[^a-z]" : startString), pageNum, numPages, sort);
                }
            }

            return list;
        }

        // GET: Browse/Contributor
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Contributor(string id, string start, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(AppConfig.DefaultBrowseNumPerPage);

            ContributorBrowseModel model = new ContributorBrowseModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Institution = bhlProvider.InstitutionSelectAuto(id);

            if (model.Institution == null)
            {
                return new RedirectResult("~/pagenotfound");
            }

            model.Start = start;
            model.DisplayStart = (model.Start == "0") ? "a number" : "\"" + model.Start.ToUpper() + "\"";
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;
            var bookResults = GetInstitutionBooks(model.Institution.InstitutionCode, model.Start, model.BookPage, model.NumPerPage, model.Sort);
            model.TotalBooks = bookResults.Item1;
            model.BookResults = bookResults.Item2;
            var segmentResults = GetInstitutionSegments(model.Institution.InstitutionCode, model.Start, model.PartPage, model.NumPerPage, model.Sort);
            model.TotalSegments = segmentResults.Item1;
            model.SegmentResults = segmentResults.Item2;

            Stats stats;
            string cacheKey = "InstitutionStats" + model.Institution.InstitutionCode;
            if (HttpContext.Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (Stats)HttpContext.Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = new BHLProvider().StatsSelectForInstitution(model.Institution.InstitutionCode);
                HttpContext.Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(AppConfig.InstitutionStatsQueryCacheTime),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            model.NumTitles = stats.TitleCount;
            model.NumBooks = stats.VolumeCount;
            model.NumPages = stats.PageCount;
            model.NumSegments = stats.SegmentCount;

            return View(model);
        }

        private Tuple<int, List<SearchBookResult>> GetInstitutionBooks(string institutionCode, string startString, int pageNum, int numPages, string sort)
        {
            Tuple<int, List<SearchBookResult>> list = new Tuple<int, List<SearchBookResult>>(0, new List<SearchBookResult>());
            if (startString.ToUpper() == "ALL") startString = String.Empty;

            if (!string.IsNullOrWhiteSpace(institutionCode))
            {
                list = new BHLProvider().TitleSelectByInstitutionAndStartsWith(institutionCode, (startString == "0" ? "[^a-z]" : startString), pageNum, numPages, sort);
            }

            return list;
        }

        private Tuple<int, List<Segment>> GetInstitutionSegments(string institutionCode, string startString, int pageNum, int numPages, string sort)
        {
            Tuple < int, List<Segment>> list = new Tuple<int, List<Segment>>(0, new List<Segment>());
            if (startString.ToUpper() == "ALL") startString = String.Empty;

            if (!string.IsNullOrWhiteSpace(institutionCode))
            {
                list = new BHLProvider().SegmentSelectByInstitutionAndStartsWith(institutionCode, (startString == "0" ? "[^a-z]" : startString), pageNum, numPages, sort);
            }

            return list;
        }

        // GET: Browse/Titles
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Titles(string start, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(AppConfig.DefaultBrowseNumPerPage);

            TitleBrowseModel model = new TitleBrowseModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Start = start;
            model.DisplayStart = (model.Start == "0") ? "a number" : "\"" + model.Start.ToUpper() + "\"";
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;
            var bookResults = bhlProvider.TitleSelectByNameLike((model.Start == "0" ? "[^a-z]" : model.Start), model.BookPage, model.NumPerPage, model.Sort);
            model.TotalBooks = bookResults.Item1;
            model.BookResults = bookResults.Item2;
            var segmentResults = bhlProvider.SegmentSelectByTitleLike((model.Start == "0" ? "[^a-z]" : model.Start), model.PartPage, model.NumPerPage, model.Sort);
            model.TotalSegments = segmentResults.Item1;
            model.SegmentResults = segmentResults.Item2;

            return View(model);
        }

        // GET: Browse/Year
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Year(string start, string end, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(AppConfig.DefaultBrowseNumPerPage);

            YearBrowseModel model = new YearBrowseModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.StartYear = start;
            model.EndYear = end;
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;

            if (Int32.TryParse(model.StartYear, out int sYear) && Int32.TryParse(model.EndYear, out int eYear))
            {
                var bookResults = bhlProvider.TitleSelectByDateRange(sYear, eYear, model.BookPage, model.NumPerPage, model.Sort);
                model.TotalBooks = bookResults.Item1;
                model.BookResults = bookResults.Item2;
                var segmentResults = bhlProvider.SegmentSelectByDateRange(sYear, eYear, model.PartPage, model.NumPerPage, model.Sort);
                model.TotalSegments = segmentResults.Item1;
                model.SegmentResults = segmentResults.Item2;
            }

            return View(model);
        }
    }
}