using Microsoft.AspNetCore.Mvc;
using BHL.SiteServicesREST.v1.Services;

namespace BHL.SiteServicesREST.v1.Controllers
{
    [ApiController]
    [Route("v1/QueueMessages")]

    public class QueueMessagesController : Controller
    {
        private readonly ILogger<QueueMessagesController> _logger;
        private readonly IQueueService _queueService;


        public QueueMessagesController(ILogger<QueueMessagesController> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        [HttpPut(Name = "PutQueueMessages")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> QueueMessages(string queueName, List<string> messages)
        {
            await _queueService.AddQueueMessages(queueName, messages);
            return Ok();
        }

        [HttpGet("Count", Name = "GetQueueMessageCount")]
        [ProducesResponseType(200, Type = typeof(uint))]
        public async Task<IActionResult> GetQueueMessageCount(string queueName)
        {
            uint numMessages = await _queueService.GetQueueMessageCount(queueName);
            return Ok(numMessages);
        }
    }
}
