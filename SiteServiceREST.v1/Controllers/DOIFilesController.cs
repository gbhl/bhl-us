using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/DOIFiles")]

    public class DOIFilesController : Controller
    {
        private readonly ILogger<DOIFilesController> _logger;

        public DOIFilesController(ILogger<DOIFilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{batchId}/{type}", Name = "GetDOIFile")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(string batchId, string type)
        {
            return Ok(new BHLProvider().DOIGetFileContents(batchId, type));
        }
    }
}
