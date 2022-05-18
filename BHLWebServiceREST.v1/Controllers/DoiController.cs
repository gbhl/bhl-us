using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

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
            _bhlProvider.DOIInsert((int)request.entitytypeid, (int)request.entityid, (int)request.doistatusid, request.doiname, 
                (short)request.isvalid, request.doibatchid, request.message, (int)request.userid, (int)request.excludebhldoi);
            return Ok();
        }

        [HttpPut("{doiID}", Name="UpdateDoi")]
        [ProducesResponseType(200)]
        public IActionResult UpdateDoi(int doiID, DoiModel request)
        {
            DOI doi = _bhlProvider.DOISelectAuto(doiID);

            if (string.Compare(doi.DOIBatchID, request.doibatchid, true) != 0)
            {
                // Update batch ID
                _bhlProvider.DOIUpdateBatchID(doiID, (int)request.doistatusid, request.doibatchid, request.userid);
            }
            else if (string.Compare(doi.DOIName, request.doiname, true) != 0)
            {
                // Update DOI Name
                _bhlProvider.DOIUpdateDOIName(doiID, (int)request.doistatusid, request.doiname, request.userid);
            }
            else if (doi.DOIStatusID != request.doistatusid)
            {
                // Update status
                _bhlProvider.DOIUpdateStatus(doiID, (int)request.doistatusid, request.message ?? string.Empty, request.isvalid ?? doi.IsValid, request.userid);
            }

            return Ok();
        }

        [HttpPost("Identifiers", Name = "AddDoiIdentifier")]
        [ProducesResponseType(200)]
        public IActionResult AddDoiIdentifier(DoiModel request)
        {
            _bhlProvider.DOIInsertIdentifier((int)request.entitytypeid, (int)request.entityid, request.doiname, request.userid);
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
