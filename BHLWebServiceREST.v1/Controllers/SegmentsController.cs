using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Segments")]

    public class SegmentsController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public SegmentsController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{segmentID}", Name = "GetSegment")]
        [ProducesResponseType(200, Type = typeof(Segment))]
        public IActionResult GetSegment(int segmentID)
        {
            return Ok(_bhlProvider.SegmentSelectAuto(segmentID));
        }

        [HttpGet("{segmentID}/Details", Name = "GetSegmentDetails")]
        [ProducesResponseType(200, Type = typeof(Segment))]
        public IActionResult GetSegmentDetails(int segmentID)
        {
            return Ok(_bhlProvider.SegmentSelectExtended(segmentID));
        }

        [HttpGet("{segmentID}/Filenames", Name = "GetSegmentFilenames")]
        [ProducesResponseType(200, Type = typeof(Item))]
        public IActionResult Filenames(int segmentID)
        {
            return Ok(_bhlProvider.ItemSelectFilenames(ItemType.Segment, segmentID));
        }

        [HttpGet("{segmentID}/Institutions", Name = "GetSegmentInstitutionsByRole")]
        [ProducesResponseType(200, Type = typeof(List<Institution>))]
        public IActionResult GetInstitutionByRole(int segmentID, string roleName)
        {
            return Ok(_bhlProvider.InstitutionSelectBySegmentIDAndRole(segmentID, roleName));
        }

        [HttpGet("{segmentID}/Pages", Name = "GetSegmentPages")]
        [ProducesResponseType(200, Type = typeof(List<Page>))]
        public IActionResult Pages(int segmentID)
        {
            return Ok(_bhlProvider.PageMetadataSelectBySegmentID(segmentID));
        }

        [HttpGet("NoDoi", Name = "GetSegmentWithoutDois")]
        [ProducesResponseType(200, Type = typeof(List<DOI>))]
        public IActionResult SegmentSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return Ok(_bhlProvider.SegmentSelectWithoutSubmittedDOI(numberToReturn));
        }

        [HttpGet("Published", Name = "GetSegmentsPublished")]
        [ProducesResponseType(200, Type = typeof(List<Segment>))]
        public IActionResult SegmentSelectPublished()
        {
            return Ok(_bhlProvider.SegmentSelectPublished());
        }

        [HttpGet("recent", Name = "GetSegmentsRecentlyChanged")]
        [ProducesResponseType(200, Type = typeof(List<Segment>))]
        public IActionResult SegmentSelectRecentlyChanged(string sinceDate)
        {
            return Ok(_bhlProvider.SegmentSelectRecentlyChanged(sinceDate));
        }

        [HttpGet("", Name = "GetSegmentByItemID")]
        [ProducesResponseType(200, Type = typeof(Segment))]
        public IActionResult SegmentByItemID(int itemID)
        {
            return Ok(_bhlProvider.SegmentSelectByItemID(itemID));
        }
    }
}
