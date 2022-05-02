using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/DOIFiles")]

    public class DOIFilesController : Controller
    {
        private readonly ILogger<DOIFilesController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public DOIFilesController(ILogger<DOIFilesController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{batchId}/{type}", Name = "GetDOIFile")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(string batchId, string type)
        {
            return Ok(_bhlProvider.DOIGetFileContents(batchId, type));
        }
    }
}
