using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class CreatorController : Controller
    {
        // GET: Creator
        public ActionResult Index(int creatorId, string sort, int page, int numPerPage)
        {
            CreatorModel model = new CreatorModel();
            BHLProvider bhlProvider = new BHLProvider();

            model.Author = bhlProvider.AuthorSelectWithNameByAuthorId(creatorId);
            if (model.Author == null) Response.Redirect("~/authornotfound");
            if (model.Author.RedirectAuthorID != null) Response.Redirect("~/creator/" + model.Author.RedirectAuthorID);

            model.Sort = sort.ToLower();
            model.Page = page;
            model.NumPerPage = numPerPage;
            model.BookResults = bhlProvider.TitleSelectByAuthor(creatorId);
            model.SegmentResults = bhlProvider.SegmentSelectForAuthorID(creatorId);

            return View(model);
        }
    }
}
