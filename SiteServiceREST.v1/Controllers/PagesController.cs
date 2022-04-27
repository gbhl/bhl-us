using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Pages")]

    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;

        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{pageID}/Text", Name = "GetPageText")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Text(int pageID)
        {
            return Ok(new BHLProvider().GetOcrText(pageID));
        }
    }
}
