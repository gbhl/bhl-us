using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/OcrJobs")]

    public class OcrJobsController : Controller
    {
        private readonly ILogger<OcrJobsController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public OcrJobsController(ILogger<OcrJobsController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("itemID", Name = "OcrJobExists")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult Get(int itemID)
        {
            return Ok(_bhlProvider.OcrJobExists(itemID));
        }

        // Put example
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio
        [HttpPut("itemid", Name = "CreateOcrJob")]
        [ProducesResponseType(200)]
        public IActionResult OcrCreateJob(int itemID)
        {
            _bhlProvider.OcrCreateJob(itemID);
            return Ok();
        }
    }
}
