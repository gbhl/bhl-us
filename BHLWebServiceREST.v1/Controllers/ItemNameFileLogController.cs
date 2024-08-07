﻿using Microsoft.AspNetCore.Mvc;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Runtime.Serialization;

namespace BHL.WebServiceREST.v1.Controllers
{
    [ApiController]
    [Route("v1/ItemNameFileLogs")]

    public class ItemNameFileLogController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IBHLProvider _bhlProvider;

        public ItemNameFileLogController(ILogger<EmailController> logger, IBHLProvider bhlProvider)
        {
            _logger = logger;
            _bhlProvider = bhlProvider;
        }

        [HttpPut("Refresh", Name = "ItemNameFileLogRefresh")]
        [ProducesResponseType(200)]
        public IActionResult ItemNameFileLogRefreshSinceDate(DateTime sinceDate)
        {
            _bhlProvider.ItemNameFileLogRefreshSinceDate(sinceDate);
            return Ok();
        }

        [HttpGet("ForCreate", Name = "GetItemNameFileLogForCreate")]
        [ProducesResponseType(200, Type = typeof(List<ItemNameFileLog>))]
        public IActionResult ItemNameFileLogSelectForCreate()
        {
            return Ok(_bhlProvider.ItemNameFileLogSelectForCreate());
        }

        [HttpGet("ForUpload", Name = "GetItemNameFileLogForUpload")]
        [ProducesResponseType(200, Type = typeof(List<ItemNameFileLog>))]
        public IActionResult ItemNameFileLogSelectForUpload()
        {
            return Ok(_bhlProvider.ItemNameFileLogSelectForUpload());
        }

        [HttpPut("{logID}/CreateDate", Name = "UpdateItemNameFileLogCreateDate")]
        [ProducesResponseType(200)]
        public IActionResult ItemNameFileLogUpdateCreateDate(int logID)
        {
            _bhlProvider.ItemNameFileLogUpdateCreateDate(logID);
            return Ok();
        }

        [HttpPut("{logID}/UploadDate", Name = "UpdateItemNameFileLogUploadDate")]
        [ProducesResponseType(200)]
        public IActionResult ItemNameFileLogUpdateUploadDate(int logID)
        {
            _bhlProvider.ItemNameFileLogUpdateUploadDate(logID);
            return Ok();
        }
    }
}
