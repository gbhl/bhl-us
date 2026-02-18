using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Configuration")]

    public class ConfigurationController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public ConfigurationController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{barcode}/{fileName}", Name = "GetDjvuFilePath")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetDjvuFilePath(string barcode, string fileName)
        {
            string filePath = _bhlProvider.GetRemoteFilePath(RemoteFileType.Djvu, barcode, fileName, null, null, null);
            return Ok(filePath);
        }
    }
}
