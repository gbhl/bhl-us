using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using BHL.SiteServicesREST.v1.Models;
using System.Text;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Segments")]

    public class SegmentsController : Controller
    {
        private readonly ILogger<SegmentsController> _logger;

        public SegmentsController(ILogger<SegmentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{segmentID}/Text", Name = "GetSegmentText")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Text(int segmentID)
        {
            ItemType itemType = ItemType.Segment;
            return Ok(new BHLProvider().GetItemText(itemType, segmentID));
        }

        [HttpGet("{segmentID}/Pdf", Name = "GetSegmentPdf")]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        public IActionResult Pdf(int segmentID)
        {
            ItemType itemType = ItemType.Segment;
            return Ok(new BHLProvider().GetItemPdf(itemType, segmentID));
        }

        [HttpPut("{segmentID}/ImageDimensions", Name = "GetSegmentPageImageDimensions")]
        [ProducesResponseType(200, Type = typeof(List<ViewerPageModel>))]
        public IActionResult ImageDimensions(int segmentID, List<ViewerPageModel> pageModels)
        {
            List<BHLProvider.ViewerPage> pages = new List<BHLProvider.ViewerPage>();
            foreach (ViewerPageModel model in pageModels) pages.Add(new BHLProvider.ViewerPage
            {
                AltExternalUrl = model.AltExternalUrl,
                ExternalBaseUrl = model.ExternalBaseUrl,
                BarCode = model.BarCode,
                FlickrUrl = model.FlickrUrl,
                SequenceOrder = model.SequenceOrder,
                Height = model.Height,
                Width = model.Width
            });

            pages = new BHLProvider().PageGetImageDimensions(pages, ItemType.Segment, segmentID);

            List<ViewerPageModel> returnPages = new List<ViewerPageModel>();
            foreach (BHLProvider.ViewerPage page in pages) returnPages.Add(new ViewerPageModel
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
