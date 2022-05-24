using BHL.WebServiceREST.v1.Models;
using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Runtime.Serialization;

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

        [HttpPut("{pdfID}/DeletionDate", Name = "UpdatePdfDeletionDate")]
        [ProducesResponseType(200, Type = typeof(PDF))]
        public IActionResult UpdatePdfDeletionDate(int pdfID, PdfModel request)
        {
            PDF pdf = _bhlProvider.PDFUpdateFileDeletion(pdfID);
            return Ok(pdf);
        }

        [HttpPut("{pdfID}/Status", Name = "UpdatePdfStatus")]
        [ProducesResponseType(200)]
        public IActionResult UpdatePdfStatus(int pdfID, PdfModel request)
        {
            _bhlProvider.PDFUpdatePdfStatus(pdfID, (int)request.pdfstatusid);
            return Ok();
        }

        [HttpPut("{pdfID}/GenerationInfo", Name = "UpdatePdfGenerationInfo")]
        [ProducesResponseType(200, Type = typeof(PDF))]
        public IActionResult UpdatePdfGenerationInfo(int pdfID, PdfModel request)
        {
            PDF pdf = _bhlProvider.PDFUpdateGenerationInfo(pdfID, request.fileLocation, request.fileUrl,
                    (int)request.numberImagesMissing, (int)request.numberOcrMissing);
            return Ok(pdf);
        }
    }
}
