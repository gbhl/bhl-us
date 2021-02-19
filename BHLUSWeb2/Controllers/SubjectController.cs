using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using System;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Index(string subject, string sort, int page, int numPerPage)
        {
            SubjectModel model = new SubjectModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Keyword = Uri.UnescapeDataString(subject).Replace('+', ' ');
            model.Sort = sort.ToLower();
            model.Page = page;
            model.NumPerPage = numPerPage;
            model.BookResults = bhlProvider.TitleSelectByKeyword(model.Keyword);
            model.SegmentResults = bhlProvider.SegmentSelectForKeyword(model.Keyword);

            return View(model);
        }
    }
}