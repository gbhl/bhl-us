using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using BHL.SiteServicesREST.v1.Models;
using System.Text;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Items")]

    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{itemID}/Text", Name = "GetItemText")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Text(int itemID)
        {
            ItemType itemType = ItemType.Book;
            return Ok(new BHLProvider().GetItemText(itemType, itemID));
        }

        [HttpGet("{itemID}/Pdf", Name = "GetItemPdf")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Pdf(int itemID)
        {
            ItemType itemType = ItemType.Book;
            return Ok(Encoding.UTF8.GetString(new BHLProvider().GetItemPdf(itemType, itemID)));
        }

        [HttpPut("{itemID}/ImageDimensions", Name = "GetItemPageImageDimensions")]
        [ProducesResponseType(200, Type = typeof(List<ViewerPageModel>))]
        public IActionResult ImageDimensions(int itemID, List<ViewerPageModel> pageModels)
        {
            List<BHLProvider.ViewerPage> pages = new List<BHLProvider.ViewerPage>();
            foreach (var model in pageModels) pages.Add(new BHLProvider.ViewerPage
            {
                AltExternalUrl = model.AltExternalUrl,
                ExternalBaseUrl = model.ExternalBaseUrl,
                BarCode = model.BarCode,
                FlickrUrl = model.FlickrUrl,
                SequenceOrder = model.SequenceOrder,
                Height = model.Height,
                Width = model.Width
            });

            pages = new BHLProvider().PageGetImageDimensions(pages, ItemType.Book, itemID);

            List<ViewerPageModel> returnPages = new List<ViewerPageModel>();
            foreach (var page in pages) returnPages.Add(new ViewerPageModel
            {
                AltExternalUrl = page.AltExternalUrl,
                ExternalBaseUrl = page.ExternalBaseUrl,
                BarCode = page.BarCode,
                FlickrUrl = page.FlickrUrl,
                SequenceOrder = page.SequenceOrder,
                Height = page.Height,
                Width = page.Width
            });

            return Ok(returnPages);
        }
    }
}
