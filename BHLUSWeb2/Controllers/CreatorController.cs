using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class CreatorController : Controller
    {
        // GET: Creator
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Index(int creatorId, string sort, int? bpg, int? ppg, int? psize)
        {
            int browseNumPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultBrowseNumPerPage"]);

            CreatorModel model = new CreatorModel();
            BHLProvider bhlProvider = new BHLProvider();

            //model.Author = bhlProvider.AuthorSelectWithNameByAuthorId(creatorId);

            String cacheKey = "AuthorMetadata" + creatorId.ToString();
            if (HttpContext.Cache[cacheKey] != null)
            {
                // Use cached version
                model.Author = (Author)HttpContext.Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                model.Author = bhlProvider.AuthorSelectExtended(creatorId);
                if (model.Author != null)
                {
                    HttpContext.Cache.Add(cacheKey, model.Author, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["AuthorMetadataCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            if (model.Author == null) return Redirect("~/authornotfound");
            if (model.Author.RedirectAuthorID != null) Response.Redirect("~/creator/" + model.Author.RedirectAuthorID);

            // Remove any author identifiers not intended for display
            model.Author.AuthorIdentifiers.RemoveAll(x => x.Display == 0);

            foreach (AuthorName name in model.Author.AuthorNames)
            {
                if (name.IsPreferredName == 1)
                {
                    model.Author.FullName = name.FullName;
                    model.Author.FullerForm = name.FullerForm;
                    break;
                }
            }
            model.Sort = sort.ToLower();
            model.BookPage = bpg ?? 1;
            model.PartPage = ppg ?? 1;
            model.NumPerPage = psize ?? browseNumPerPage;
            var bookResults = bhlProvider.TitleSelectByAuthorPaged(creatorId, model.BookPage, model.NumPerPage, model.Sort);
            model.TotalBooks = bookResults.Item1;
            model.BookResults = bookResults.Item2;
            var segmentResults = bhlProvider.SegmentSelectForAuthorIDPaged(creatorId, model.PartPage, model.NumPerPage, model.Sort);
            model.TotalSegments = segmentResults.Item1;
            model.SegmentResults = segmentResults.Item2;

            return View(model);
        }
    }
}
