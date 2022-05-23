using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
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

        [HttpGet("{itemID}/Filenames", Name = "GetItemFilenames")]
        [ProducesResponseType(200, Type = typeof(Item))]
        public IActionResult Filenames(int itemID)
        {
            return Ok(_bhlProvider.ItemSelectFilenames(ItemType.Item, itemID));
        }

        [HttpGet("{itemID}/Pages", Name = "GetItemPages")]
        [ProducesResponseType(200, Type = typeof(List<Page>))]
        public IActionResult Pages(int itemID)
        {
            return Ok(_bhlProvider.PageSelectFileNameByItemID(itemID));
        }

        [HttpGet("{itemID}/Institutions", Name = "GetItemInstitutionsByRole")]
        [ProducesResponseType(200, Type = typeof(List<Institution>))]
        public IActionResult GetInstitutionByRole(int itemID, string roleName)
        {
            return Ok(_bhlProvider.InstitutionSelectByItemIDAndRole(itemID, roleName));
        }

        [HttpGet("{itemID}/NamesXml", Name = "GetItemNamesXml")]
        [ProducesResponseType(200, Type = typeof(List<Institution>))]
        public IActionResult GetNamesXML(int itemID)
        {
            return Ok(_bhlProvider.ItemGetNamesXMLByItemID(itemID));
        }

        [HttpDelete("{itemID}/Names", Name = "DeleteItemNames")]
        [ProducesResponseType(200)]
        public IActionResult NamePageDeleteByItemID(int itemID)
        {
            _bhlProvider.NamePageDeleteByItemID(itemID);
            return Ok();
        }

        [HttpPut("{itemID}", Name = "UpdateItemLastPageNameLookupDate")]
        [ProducesResponseType(200)]
        public IActionResult ItemUpdateLastPageNameLookupDate(int itemID)
        {
            return Ok(_bhlProvider.ItemUpdateLastPageNameLookupDate(itemID));
        }
    }
}
