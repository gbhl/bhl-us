using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/ServiceLogs")]
    public class ServiceLogController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public ServiceLogController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpPost("", Name = "InsertServiceLog")]
        [ProducesResponseType(200)]
        public IActionResult ServiceLogInsert(ServiceLogModel request)
        {
            _bhlProvider.ServiceLogInsert(request.logdate, request.servicename, request.serviceparam, request.severityname,
                request.errornumber, request.procedure, request.line, request.message, request.stacktrace);
            return Ok();
        }
    }
}
