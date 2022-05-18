using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Items")]

    public class ItemsController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public ItemsController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{itemID}", Name = "GetItem")]
        [ProducesResponseType(200, Type = typeof(Item))]

        public IActionResult GetItem(int itemID)
        {
            return Ok(_bhlProvider.ItemSelectAuto(itemID));
        }

        [HttpGet("ExpiredNames", Name = "GetItemWithExpiredNames")]
        [ProducesResponseType(200, Type = typeof(List<Item>))]
        public IActionResult ItemSelectWithExpiredPageNames(int maxAge)
        {
            return Ok(_bhlProvider.ItemSelectWithExpiredPageNames(maxAge));
        }

        [HttpGet("NoNames", Name = "GetItemWithoutNames")]
        [ProducesResponseType(200, Type = typeof(List<Item>))]
        public IActionResult ItemSelectWithoutPageNames()
        {
            return Ok(_bhlProvider.ItemSelectWithoutPageNames());
        }

        [HttpGet("Published", Name = "GetItemsPublished")]
        [ProducesResponseType(200, Type = typeof(List<Title>))]
        public IActionResult ItemSelectPublished()
        {
            return Ok(_bhlProvider.ItemSelectPublished());
        }
    }
}
