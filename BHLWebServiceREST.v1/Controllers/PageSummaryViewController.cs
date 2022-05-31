using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/PageSummaryViews")]

    public class PageSummaryViewController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public PageSummaryViewController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("", Name = "GetPageSummaryViewByPdfID")]
        [ProducesResponseType(200, Type = typeof(List<PageSummaryView>))]

        public IActionResult PageSummaryViewSelectByPdf(int pdfID)
        {
            return Ok(_bhlProvider.PDFPageSummaryViewSelectByPdfID(pdfID));
        }
    }
}
