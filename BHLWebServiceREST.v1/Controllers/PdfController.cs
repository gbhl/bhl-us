using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Pdfs")]

    public class PdfController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public PdfController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpGet("ForCreate", Name = "GetPdfsForCreation")]
        [ProducesResponseType(200, Type = typeof(List<PDF>))]
        public IActionResult PDFSelectForFileCreation()
        {
            return Ok(_bhlProvider.PDFSelectForFileCreation());
        }

        [HttpGet("ForDelete", Name = "GetPdfsForDeletion")]
        [ProducesResponseType(200, Type = typeof(List<PDF>))]
        public IActionResult PDFSelectForDeletion()
        {
            return Ok(_bhlProvider.PDFSelectForDeletion());
        }

        [HttpPut("{pdfID}", Name = "UpdatePdf")]
        [ProducesResponseType(200, Type = typeof(PDF))]
        public IActionResult UpdatePdf(int pdfID, PdfUpdateTarget target, PdfModel request)
        {
            PDF pdf = null;

            if (target == PdfUpdateTarget.DeletionDate)
            {
                // Update pdf deletion date
                pdf = _bhlProvider.PDFUpdateFileDeletion(pdfID);
            }
            if (target == PdfUpdateTarget.PdfStatus)
            {
                // Update pdf status
                _bhlProvider.PDFUpdatePdfStatus(pdfID, (int)request.pdfstatusid);
            }
            if (target == PdfUpdateTarget.GenerationInfo)
            {
                // Update pdf generation info
                pdf = _bhlProvider.PDFUpdateGenerationInfo(pdfID, request.fileLocation, request.fileUrl, 
                    (int)request.numberImagesMissing, (int)request.numberOcrMissing);            
            }

            return Ok(pdf);
        }
    }

    public enum PdfUpdateTarget
    {
        DeletionDate = 10,
        GenerationInfo = 20,
        PdfStatus = 30
    }
}
