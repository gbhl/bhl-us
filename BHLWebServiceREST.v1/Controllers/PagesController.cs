using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Pages")]

    public class PagesController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public PagesController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("ExpiredNames", Name = "GetPagesWithExpiredNames")]
        [ProducesResponseType(200, Type = typeof(List<Page>))]
        public IActionResult PageSelectWithExpiredPageNamesByItemID(int itemID, int maxAge)
        {
            return Ok(_bhlProvider.PageSelectWithExpiredPageNamesByItemID(itemID, maxAge));
        }

        [HttpGet("NoNames", Name = "GetPageWithoutNames")]
        [ProducesResponseType(200, Type = typeof(List<Page>))]
        public IActionResult PageSelectWithoutPageNames(int? itemID = null)
        {
            if (itemID == null)
                return Ok(_bhlProvider.PageSelectWithoutPageNames());
            else
                return (Ok(_bhlProvider.PageSelectWithoutPageNamesByItemID((int)itemID)));
        }

        [HttpPut("{pageID}", Name = "UpdatePageLastPageNameLookupDate")]
        [ProducesResponseType(200, Type = typeof(Page))]
        public IActionResult PageUpdateLastPageNameLookupDate(int pageID)
        {
            return Ok(_bhlProvider.PageUpdateLastPageNameLookupDate(pageID));
        }

        [HttpPut("{pageID}/Names", Name = "UpdatePageNames")]
        [ProducesResponseType(200, Type = typeof(int[]))]
        public IActionResult UpdatePageNames(int pageID, PageNameModel request)
        {
            return Ok(_bhlProvider.PageNameUpdateList(pageID, request.nameinfo, request.sourcename));
        }
    }
}
