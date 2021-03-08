using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class CreatorController : Controller
    {
        // GET: Creator
        public ActionResult Index(int creatorId, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultBrowseNumPerPage"]);

            CreatorModel model = new CreatorModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Author = bhlProvider.AuthorSelectWithNameByAuthorId(creatorId);
            if (model.Author == null) Response.Redirect("~/authornotfound");
            if (model.Author.RedirectAuthorID != null) Response.Redirect("~/creator/" + model.Author.RedirectAuthorID);

            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;
            model.BookResults = bhlProvider.TitleSelectByAuthor(creatorId);
            model.SegmentResults = bhlProvider.SegmentSelectForAuthorID(creatorId);

            return View(model);
        }
    }
}
