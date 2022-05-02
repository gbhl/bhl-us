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
        private readonly IBHLProvider _bhlProvider;

        public PagesController(ILogger<PagesController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{pageID}/Text", Name = "GetPageText")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Text(int pageID)
        {
            return Ok(_bhlProvider.GetOcrText(pageID));
        }
    }
}
