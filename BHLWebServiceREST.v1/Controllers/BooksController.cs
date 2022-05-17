using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Books")]

    public class BooksController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public BooksController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{bookID}", Name = "GetBook")]
        [ProducesResponseType(200, Type = typeof(Book))]
        public IActionResult Book(int bookID)
        {
            return Ok(_bhlProvider.BookSelectAuto(bookID));
        }

        [HttpGet("", Name = "GetBookByItemID")]
        [ProducesResponseType(200, Type = typeof(Book))]
        public IActionResult BookByItemID(int itemID)
        {
            return Ok(_bhlProvider.BookSelectByItemID(itemID));
        }

        [HttpGet("recent", Name = "GetBooksRecentlyChanged")]
        [ProducesResponseType(200, Type = typeof(List<Book>))]
        public IActionResult BookSelectRecentlyChanged(string sinceDate)
        {
            return Ok(_bhlProvider.BookSelectRecentlyChanged(sinceDate));
        }

        [HttpGet("{bookID}/Filenames", Name = "GetBookFilenames")]
        [ProducesResponseType(200, Type = typeof(Item))]
        public IActionResult Filenames(int bookID)
        {
            return Ok(_bhlProvider.ItemSelectFilenames(ItemType.Book, bookID));
        }

        [HttpGet("{bookID}/Pages", Name = "GetBookPages")]
        [ProducesResponseType(200, Type = typeof(List<Page>))]
        public IActionResult Pages(int bookID)
        {
            return Ok(_bhlProvider.PageMetadataSelectByItemID(bookID));
        }
    }
}
