using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOBOT.BHL.Server;
using BHLData = MOBOT.BHL.DataObjects;
using MOBOT.BHL.API.BHLApi;
using BHLApiData = MOBOT.BHL.API.BHLApiDataObjects2;
using CustomDataAccess;
using BHLApi3Web.Models;

namespace BHLApi3Web.Controllers
{
    public class TitlesController : ApiController
    {
        private Services.ITitlesService titlesService = null;

        public TitlesController()
        {
            titlesService = new Services.TitlesService();
        }

        public TitlesController(Services.ITitlesService service)
        {
            titlesService = service;
        }

        // GET api/<controller>
        public IEnumerable<Models.Title> Get()
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            Helper.LogRequest(this.ControllerContext.Request, Helper.API3RequestType.GetTitleMetadata);

            HttpResponseMessage msg = null;
            Models.Title title = titlesService.GetTitle(id);
            if (title != null)
            {
                msg = this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, title);
            }
            else
            {
                msg = this.ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            msg.Headers.Add("Access-Control-Allow-Origin", "*");
            return msg;
        }

        public HttpResponseMessage Get(string since, string until)
        {
            Helper.LogRequest(this.ControllerContext.Request, Helper.API3RequestType.GetTitleSinceUntil);

            //var queryVals = this.ControllerContext.Request.RequestUri.ParseQueryString();
            //var query = queryVals["q"];

            HttpResponseMessage msg = null;
            IEnumerable<Title> titles = titlesService.GetSearchSince(since, until);
            if (titles.Count() > 0)
            {
                msg = this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, (IEnumerable<Title>) titles);
            }
            else
            {
                msg = this.ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            msg.Headers.Add("Access-Control-Allow-Origin", "*");
            return msg;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }
    }
}