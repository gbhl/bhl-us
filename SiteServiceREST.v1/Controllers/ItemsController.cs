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
        private readonly IBHLProvider _bhlProvider;

        public ItemsController(ILogger<ItemsController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{itemID}/Text", Name = "GetItemText")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Text(int itemID)
        {
            ItemType itemType = ItemType.Book;
            return Ok(_bhlProvider.GetItemText(itemType, itemID));
        }

        [HttpGet("{itemID}/Pdf", Name = "GetItemPdf")]
        [ProducesResponseType(200, Type = typeof(FileStream))]
        public IActionResult Pdf(int itemID)
        {
            string pdfLocation = _bhlProvider.GetItemPdfPath(ItemType.Book, itemID);
            var stream = new FileStream(pdfLocation, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpPut("{itemID}/ImageDimensions", Name = "GetItemPageImageDimensions")]
        [ProducesResponseType(200, Type = typeof(List<ViewerPageModel>))]
        public async Task<IActionResult> ImageDimensions(int itemID, List<ViewerPageModel> pageModels)
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

            pages = await _bhlProvider.PageGetImageDimensions(pages, ItemType.Book, itemID);

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
