using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse/Authors
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Authors(string start, int page, int numPerPage)
        {
            return View();
        }

        // GET: Browse/Collection
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Collection(string id, string start, string sort, int? bpg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultBrowseNumPerPage"]);

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
            if (model.Collection.CanContainItems > 0) model.ShowVolume = true;
            model.Start = start;
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;

            var bookResults = GetCollectionBooks(model.Collection, model.Start, model.BookPage, model.NumPerPage, model.Sort);
            model.TotalBooks = bookResults.Item1;
            model.BookResults = bookResults.Item2;
            var stats = new BHLProvider().StatsSelectForCollection(model.Collection.CollectionID);
            model.NumTitles = stats.TitleCount;
            model.NumBooks = stats.VolumeCount;
            model.NumPages = stats.PageCount;

            return View(model);
        }

        private Tuple<int, List<SearchBookResult>> GetCollectionBooks(Collection collection, string startString, int pageNum, int numPages, string sort)
        {
            Tuple<int, List<SearchBookResult>> list = new Tuple<int, List<SearchBookResult>>(0, new List<SearchBookResult>());

            if (collection != null)
            {
                BHLProvider bhlProvider = new BHLProvider();

                if (collection.CanContainItems > 0)
                {
                    if (startString == "0")
                    {
                        //list = bhlProvider.ItemSelectByCollectionAndStartsWithout(collection.CollectionID, "[a-z]", pageNum, numPages, sort);
                    }
                    else
                    {
                        //list = bhlProvider.ItemSelectByCollectionAndStartsWith(collection.CollectionID, startString, pageNum, numPages, sort);
                    }
                }
                if (collection.CanContainTitles > 0)
                {
                    if (startString == "0")
                    {
                        //list = bhlProvider.TitleSelectByCollectionAndStartsWithout(collection.CollectionID, "[a-z]", pageNum, numPages, sort);
                    }
                    else
                    {
                        //list = bhlProvider.TitleSelectByCollectionAndStartsWith(collection.CollectionID, startString, pageNum, numPages, sort);
                    }
                }
            }

            return list;
        }

        // GET: Browse/Contributor
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Contributor(string id, string start, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultBrowseNumPerPage"]);

            ContributorBrowseModel model = new ContributorBrowseModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Institution = bhlProvider.InstitutionSelectAuto(id);
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
            var stats = new BHLProvider().StatsSelectForInstitution(model.Institution.InstitutionCode);
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
                if (startString == "0")
                {
                    list = new BHLProvider().TitleSelectByInstitutionAndStartsWithout(institutionCode, "[a-z]", pageNum, numPages, sort);
                }
                else
                {
                    list = new BHLProvider().TitleSelectByInstitutionAndStartsWith(institutionCode, startString, pageNum, numPages, sort);
                }
            }

            return list;
        }

        private Tuple<int, List<Segment>> GetInstitutionSegments(string institutionCode, string startString, int pageNum, int numPages, string sort)
        {
            Tuple < int, List<Segment>> list = new Tuple<int, List<Segment>>(0, new List<Segment>());
            if (startString.ToUpper() == "ALL") startString = String.Empty;

            if (!string.IsNullOrWhiteSpace(institutionCode))
            {
                if (startString == "0")
                {
                    list = new BHLProvider().SegmentSelectByInstitutionAndStartsWithout(institutionCode, "[a-z]", pageNum, numPages, sort);
                }
                else
                {
                    list = new BHLProvider().SegmentSelectByInstitutionAndStartsWith(institutionCode, startString, pageNum, numPages, sort);
                }
            }

            return list;
        }

        // GET: Browse/Titles
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Titles(string start, string sort, int page, int numPerPage)
        {
            return View();
        }

        // GET: Browse/Year
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Year(int start, int end, string sort, int page, int numPerPage)
        {
            return View();
        }
    }
}