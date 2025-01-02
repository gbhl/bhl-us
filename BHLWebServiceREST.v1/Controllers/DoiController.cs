using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Runtime.Serialization;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Dois")]

    public class DoiController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public DoiController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpPost("", Name = "AddDoi")]
        [ProducesResponseType(200)]
        public IActionResult AddDoi(DoiModel request)
        {
            _bhlProvider.DOIInsert(request.entitytypeid ?? 0, request.entityid ?? 0, request.doistatusid ?? 0, request.doiname, 
                request.isvalid ?? 1, request.doibatchid, request.message, request.userid ?? 0, request.excludebhldoi ?? 0);
            return Ok();
        }

        [HttpPut("{doiID}/BatchID", Name="UpdateDoiBatchID")]
        [ProducesResponseType(200)]
        public IActionResult UpdateDoiBatchID(int doiID, DoiModel request)
        {
            _bhlProvider.DOIUpdateBatchID(doiID, request.doistatusid ?? 0, request.doibatchid, request.userid);
            return Ok();
        }

        [HttpPut("{doiID}/DOIName", Name = "UpdateDoiName")]
        [ProducesResponseType(200)]
        public IActionResult UpdateDoiName(int doiID, DoiModel request)
        {
            _bhlProvider.DOIUpdateDOIName(doiID, request.doistatusid ?? 0, request.doiname, request.userid);
            return Ok();
        }

        [HttpPut("{doiID}/Status", Name = "UpdateDoiStatus")]
        [ProducesResponseType(200)]
        public IActionResult UpdateDoiStatus(int doiID, DoiModel request)
        {
            DOI doi = _bhlProvider.DOISelectAuto(doiID);
            _bhlProvider.DOIUpdateStatus(doiID, request.doistatusid ?? 0, request.message ?? string.Empty, request.isvalid ?? doi.IsValid, request.userid);
            return Ok();
        }

        [HttpPost("Identifiers", Name = "AddDoiIdentifier")]
        [ProducesResponseType(200)]
        public IActionResult AddDoiIdentifier(DoiModel request)
        {
            _bhlProvider.DOIInsertIdentifier(request.entitytypeid ?? 0, request.entityid ?? 0, request.doiname, request.userid);
            return Ok();
        }

        [HttpGet("Submitted", Name = "GetSubmittedDois")]
        [ProducesResponseType(200, Type = typeof(List<DOI>))]
        public IActionResult GetSubmittedDois(int minutesSinceSubmit)
        {
            return Ok(_bhlProvider.DOISelectSubmitted(minutesSinceSubmit));
        }
    }
}
