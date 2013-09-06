using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOBOT.BHL.Server;
using BHLData = MOBOT.BHL.DataObjects;
using CustomDataAccess;
using BHLApi3Web.Models;

namespace BHLApi3Web.Controllers
{
    public class PartsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Part> Get()
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        // GET api/<controller>/5
        public Part Get(int id)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);

            /*
            Helper.LogRequest(this.ControllerContext.Request, Helper.API3RequestType.GetPartMetadata);

            BHLProvider provider = new BHLProvider();
            BHLData.Segment bhlPart = provider.SegmentSelectExtended(id);

            Part part = new Part();

            if (bhlPart!= null)
            {
                part.PartID = bhlPart.SegmentID;
                part.Title = bhlPart.Title;
                part.SequenceOrder = bhlPart.SequenceOrder;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
             
            return part;
             */
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