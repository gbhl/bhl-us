using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Ocr")]

    public class OcrController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public OcrController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("Pages/{pageID}/Names", Name = "GetNamesFromPageOcr")]
        [ProducesResponseType(200, Type = typeof(List<NameFinderResponse>))]
        public IActionResult GetNamesFromPageOcr(int pageID)
        {
            return Ok(_bhlProvider.GetNamesFromOcr(
                System.Configuration.ConfigurationManager.AppSettings["NameFinderService"],
                pageID,
                System.Configuration.ConfigurationManager.AppSettings["UsePreferredNameResults"] == "true",
                Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxReadAttempts"])));
        }

        [HttpGet("Items/{itemID}/Exists", Name = "ItemOcrExists")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult ItemCheckForOcrText(int itemID, string ocrTextPath)
        {
            return Ok(_bhlProvider.ItemCheckForOcrText(itemID, ocrTextPath));
        }

        [HttpGet("Pages/{pageID}/Exists", Name = "PageOcrExists")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult PageCheckForOcrText(int pageID)
        {
            
            return Ok(_bhlProvider.PageCheckForOcrText(pageID));
        }
    }
}
