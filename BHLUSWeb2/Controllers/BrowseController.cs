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
        public ActionResult Collection(string name, string start, string sort, int page, int numPerPage)
        {
            return View();
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
            var segmentResults = GetInstitutionSegments(model.Institution.InstitutionCode, model.Start, model.BookPage, model.NumPerPage, model.Sort);
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
                    //list = new BHLProvider().TitleSelectByInstitutionAndStartsWithout(institutionCode, "[a-z]");
                }
                else
                {
                    //list = new BHLProvider().TitleSelectByInstitutionAndStartsWith(institutionCode, startString);
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
                    //list = new BHLProvider().SegmentSelectByInstitutionAndStartsWithout(institutionCode, "[a-z]");
                }
                else
                {
                    //list = new BHLProvider().SegmentSelectByInstitutionAndStartsWith(institutionCode, startString);
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