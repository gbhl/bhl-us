using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.OAI2;
using MODS = MOBOT.BHL.OAIMODS;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Exports")]

    public class ExportsController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public ExportsController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("BibTeX/Titles", Name = "GetTitleBibTexCitations")]
        [ProducesResponseType(200, Type = typeof(List<TitleBibTeX>))]
        public IActionResult TitleBibTeXSelectAllTitleCitations()
        {
            return Ok(_bhlProvider.TitleBibTeXSelectAllTitleCitations());
        }

        [HttpGet("BibTeX/Items", Name = "GetItemBibTexCitations")]
        [ProducesResponseType(200, Type = typeof(List<TitleBibTeX>))]
        public IActionResult TitleBibTeXSelectAllItemCitations()
        {
            return Ok(_bhlProvider.TitleBibTeXSelectAllItemCitations());
        }

        [HttpGet("BibTeX/Segments", Name = "GetSegmentBibTexCitations")]
        [ProducesResponseType(200, Type = typeof(List<TitleBibTeX>))]
        public IActionResult SegmentSelectAllBibTeXCitations()
        {
            return Ok(_bhlProvider.SegmentSelectAllBibTeXCitations());
        }

        [HttpGet("MODS/Titles/{titleID}", Name = "GetTitleMODS")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetMODSRecordForTitle(int titleID)
        {
            OAIRecord record = new OAIRecord("oai:biodiversitylibrary.org:title/" + titleID.ToString());
            MODS.Convert mods = new MODS.Convert(record);
            return Ok(mods.ToString());
        }

        [HttpGet("MODS/Items/{itemID}", Name = "GetItemMODS")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetMODSRecordForItem(int itemID)
        {
            OAIRecord record = new OAIRecord("oai:biodiversitylibrary.org:item/" + itemID.ToString());
            MODS.Convert mods = new MODS.Convert(record);
            return Ok(mods.ToString());
        }

        [HttpGet("MODS/Segments/{segmentID}", Name = "GetSegmentMODS")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetMODSRecordForSegment(int segmentID)
        {
            OAIRecord record = new OAIRecord("oai:biodiversitylibrary.org:part/" + segmentID.ToString());
            MODS.Convert mods = new MODS.Convert(record);
            return Ok(mods.ToString());
        }

        [HttpGet("RIS/Titles", Name = "GetTitleRISCitations")]
        [ProducesResponseType(200, Type = typeof(List<RISCitation>))]
        public IActionResult TitleSelectAllRISCitations()
        {
            return Ok(_bhlProvider.TitleSelectAllRISCitations());
        }

        [HttpGet("RIS/Items", Name = "GetItemRISCitations")]
        [ProducesResponseType(200, Type = typeof(List<RISCitation>))]
        public IActionResult ItemSelectAllRISCitations()
        {
            return Ok(_bhlProvider.ItemSelectAllRISCitations());
        }

        [HttpGet("RIS/Segments", Name = "GetSegmentRISCitations")]
        [ProducesResponseType(200, Type = typeof(List<RISCitation>))]
        public IActionResult SegmentSelectAllRISCitations()
        {
            return Ok(_bhlProvider.SegmentSelectAllRISCitations());
        }

        [HttpPost("RIS", Name = "GetRISCitationString")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GenerateRISCitation(RISCitation citation)
        {
            return Ok(_bhlProvider.GenerateRISCitation(citation));
        }

        [HttpGet("IA", Name = "GetIAIdentifiers")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        public IActionResult ExportIAIdentifiers()
        {
            List<Item> items = _bhlProvider.ItemSelectBarcodes();
            List<string> barcodes = new List<string>();
            foreach (Item item in items) barcodes.Add(item.BarCode);
            return Ok(barcodes);
        }

        [HttpGet("KBART", Name = "GetKBART")]
        [ProducesResponseType(200, Type = typeof(List<KBART>))]
        public IActionResult ExportKBART(string urlRoot)
        {
            return Ok(_bhlProvider.ExportKBART(urlRoot));
        }
    }
}
