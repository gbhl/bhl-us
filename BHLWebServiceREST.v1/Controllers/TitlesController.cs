using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Titles")]

    public class TitlesController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public TitlesController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{titleID}", Name = "GetTitle")]
        [ProducesResponseType(200, Type = typeof(Title))]

        public IActionResult GetTitle(int titleID)
        {
            return Ok(_bhlProvider.TitleSelectAuto(titleID));
        }

        [HttpGet("{titleID}/Details", Name = "GetTitleDetails")]
        [ProducesResponseType(200, Type = typeof(Title))]
        public IActionResult TitleSelectDetailByTitleID(int titleID)
        {
            return Ok(_bhlProvider.TitleSelectExtended(titleID));
        }

        [HttpGet("{titleID}/Identifiers", Name = "GetTitleIdentifiers")]
        [ProducesResponseType(200, Type = typeof(List<Title_Identifier>))]
        public IActionResult TitleIdentifierSelectByTitleID(int titleID)
        {
            return Ok(_bhlProvider.Title_IdentifierSelectByTitleID(titleID));
        }

        [HttpGet("{titleID}/Doi", Name = "GetTitleDois")]
        [ProducesResponseType(200, Type = typeof(List<Title_Identifier>))]
        public IActionResult DOISelectValidForTitle(int titleID)
        {
            return Ok(_bhlProvider.DOISelectValidForTitle(titleID));
        }

        [HttpGet("NoDoi", Name = "GetTitleWithoutDois")]
        [ProducesResponseType(200, Type = typeof(List<DOI>))]
        public IActionResult TitleSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return Ok(_bhlProvider.TitleSelectWithoutSubmittedDOI(numberToReturn));
        }

        [HttpGet("Published", Name = "GetTitlesPublished")]
        [ProducesResponseType(200, Type = typeof(List<Title>))]
        public IActionResult TitleSelectAllPublished()
        {
            return Ok(_bhlProvider.TitleSelectAllPublished());
        }
    }
}
