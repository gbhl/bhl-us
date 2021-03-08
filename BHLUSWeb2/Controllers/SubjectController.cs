using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Index(string subject, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultBrowseNumPerPage"]);

            SubjectModel model = new SubjectModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Keyword = Uri.UnescapeDataString(subject).Replace('+', ' ');
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;
            model.BookResults = bhlProvider.TitleSelectByKeyword(model.Keyword);
            model.SegmentResults = bhlProvider.SegmentSelectForKeyword(model.Keyword);

            return View(model);
        }
    }
}