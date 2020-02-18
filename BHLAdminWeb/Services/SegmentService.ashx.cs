using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for SegmentService
    /// </summary>
    public class SegmentService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response;

            // Clean up inputs
            String qsSegmentID = context.Request.QueryString["segmentID"] as String;
            int segmentID;
            Int32.TryParse(qsSegmentID, out segmentID);
            String title = context.Request.QueryString["title"] as String;
            title = (title ?? string.Empty);

            switch (context.Request.QueryString["op"])
            {
                case "SegmentSearch":
                    {
                        response = this.SegmentSearch(segmentID, title);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }

            }

            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        private string SegmentSearch(int segmentId, String title)
        {
            try
            {
                List<Segment> segments = new List<Segment>();
                if (segmentId != 0)
                {
                    segments.Add(new BHLProvider().SegmentSelectForSegmentID(segmentId));
                }
                else if (!string.IsNullOrEmpty(title))
                {
                    segments = new BHLProvider().SearchSegmentComplete(title);
                }
                for (int x = (segments.Count - 1); x >= 0; x--)
                {
                    // Remove inactive segments.
                    if (segments[x].SegmentStatusID > 20) segments.RemoveAt(x);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(segments);
            }
            catch
            {
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}