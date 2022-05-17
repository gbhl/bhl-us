using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Vaults")]

    public class VaultsController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public VaultsController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("{vaultID}", Name = "GetVault")]
        [ProducesResponseType(200, Type = typeof(Vault))]

        public IActionResult GetVault(int vaultID)
        {
            return Ok(_bhlProvider.VaultSelect(vaultID));
        }
    }
}
