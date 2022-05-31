using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/PageTextLogs")]

    public class PageTextLogController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public PageTextLogController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpPost("", Name = "InsertPageTextLog")]
        [ProducesResponseType(200)]
        public IActionResult PageTextLogInsertForItem(PageTextLogModel request)
        {
            _bhlProvider.PageTextLogInsertForItem(request.itemid, request.textsource, request.userid);
            return Ok();
        }

    }
}
