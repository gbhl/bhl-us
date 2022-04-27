using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/MarcFiles")]

    public class MarcFilesController : Controller
    {
        private readonly ILogger<MarcFilesController> _logger;

        public MarcFilesController(ILogger<MarcFilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}/{type}", Name = "GetMarcFile")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(int id, string type)
        {
            return Ok(new BHLProvider().MarcGetFileContents(id, type));
        }

        [HttpPut("marcBibID", Name = "CreateMarcFile")]
        [ProducesResponseType(200)]
        public IActionResult MarcCreateFile(string marcBibID, string content)
        {
            new BHLProvider().MarcCreateFile(marcBibID, content);
            return Ok();
        }
    }
}
