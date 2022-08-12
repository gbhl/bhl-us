using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/MarcFiles")]

    public class MarcFilesController : Controller
    {
        private readonly ILogger<MarcFilesController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public MarcFilesController(ILogger<MarcFilesController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{id}/{type}", Name = "GetMarcFile")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(int id, string type)
        {
            return Ok(_bhlProvider.MarcGetFileContents(id, type));
        }

        [HttpPut("marcBibID", Name = "CreateMarcFile")]
        [ProducesResponseType(200)]
        public IActionResult MarcCreateFile(string marcBibID, string content)
        {
            _bhlProvider.MarcCreateFile(marcBibID, content);
            return Ok();
        }
    }
}
