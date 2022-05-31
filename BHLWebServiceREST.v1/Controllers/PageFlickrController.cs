using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/PageFlickr")]

    public class PageFlickrController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public PageFlickrController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("PageFlickr/Random", Name = "GetPageFlickrRandom")]
        [ProducesResponseType(200, Type = typeof(List<PageFlickr>))]

        public IActionResult PageFlickrSelectRandom(int numberToReturn)
        {
            return Ok(_bhlProvider.PageFlickrSelectRandom(numberToReturn));
        }
    }
}
