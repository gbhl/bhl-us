using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Index(string subject, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(AppConfig.DefaultBrowseNumPerPage);

            SubjectModel model = new SubjectModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Keyword = Uri.UnescapeDataString(subject).Replace('+', ' ');
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;
            var bookResults = bhlProvider.TitleSelectByKeywordPaged(model.Keyword, model.BookPage, model.NumPerPage, model.Sort);
            model.TotalBooks = bookResults.Item1;
            model.BookResults = bookResults.Item2;
            var segmentResults = bhlProvider.SegmentSelectForKeywordPaged(model.Keyword, model.PartPage, model.NumPerPage, model.Sort);
            model.TotalSegments = segmentResults.Item1;
            model.SegmentResults = segmentResults.Item2;

            return View(model);
        }
    }
}