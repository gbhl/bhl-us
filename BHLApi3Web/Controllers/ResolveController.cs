using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BHLApi3Web.Services;

namespace BHLApi3Web.Controllers
{
    /// <summary>
    /// This class implements the segment/title disambiguation service.
    /// </summary>
    public class ResolveController : ApiController
    {
        private IResolverService resolverService = null;

        public ResolveController()
        {
            resolverService = new ResolverService();
        }

        public ResolveController(IResolverService service)
        {
            resolverService = service;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            //return "value";
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        public HttpResponseMessage Get(string title, string authors, string year)
        {
            Helper.LogRequest(this.ControllerContext.Request, Helper.API3RequestType.GetResolve);

            List<Models.ResolutionResult> results = new List<Models.ResolutionResult>();
            // Resolve against segments and titles
            results = resolverService.Resolve(title, authors, year, "part", results);
            results = resolverService.Resolve(title, authors, year, "title", results);

            // Return results
            HttpResponseMessage msg = null;
            if (results.Count == 0)
            {
                msg = this.ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                msg = this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, (IEnumerable<Models.ResolutionResult>)results);
            }

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