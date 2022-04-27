using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/OcrJobs")]

    public class OcrJobsController : Controller
    {
        private readonly ILogger<OcrJobsController> _logger;

        public OcrJobsController(ILogger<OcrJobsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("itemID", Name = "OcrJobExists")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult Get(int itemID)
        {
            return Ok(new BHLProvider().OcrJobExists(itemID));
        }

        // Put example
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio
        [HttpPut("itemid", Name = "CreateOcrJob")]
        [ProducesResponseType(200)]
        public IActionResult OcrCreateJob(int itemID)
        {
            new BHLProvider().OcrCreateJob(itemID);
            return Ok();
        }
    }
}
