using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [Route("v1/Monitor")]
    [ApiController]
    public class MonitorController : Controller
    {
        private readonly ILogger<MonitorController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public MonitorController(ILogger<MonitorController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("Server/Search", Name = "GetSearchServerStats")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult SearchServerStats()
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);   // Don't wait long for server to respond
                string url = System.Configuration.ConfigurationManager.AppSettings["SearchServerStatsUrl"] ?? string.Empty;
                string serverStats = client.GetStringAsync(url).GetAwaiter().GetResult();
                return Ok(serverStats);
            }
        }
    }
}
