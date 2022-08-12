using BHL.WebServiceREST.v1.Models;
using BHL.WebServiceREST.v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/Email")]

    public class EmailController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IMailService _mailService;

        public EmailController(ILogger<EmailController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpPost("Send", Name = "EmailSend")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Send(MailRequestModel request)
        {
            await _mailService.SendEmailAsync(request);
            return Ok();
        }
    }
}
