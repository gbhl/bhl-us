using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for RecentlyClusteredSegmentsDownloadService
    /// </summary>
    public class RecentlyClusteredSegmentsDownloadService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            this.DoDownload(context);
        }

        private void DoDownload(HttpContext context)
        {
            List<Segment> segments = null;

            try
            {
                this.WriteHttpHeaders(context, "text/csv", "RecentlyClusteredSegments.csv");

                // Get the data to output
                BHLProvider provider = new BHLProvider();

                // Search terms specified individually (title, author, volume, etc)
                segments = provider.SegmentSelectRecentlyClustered(10000000);

                // Output the data as CSV
                this.GetReportCSVString(context, segments);
            }
            finally
            {
                // Finish the download
                context.Response.End();
            }
        }

        /// <summary>
        /// Write the HTTP header information for the download
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        private void WriteHttpHeaders(HttpContext context, string contentType, string fileName)
        {
            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = contentType;
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        }

        /// <summary>
        /// Write the search results into CSV
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        private void GetReportCSVString(HttpContext context, List<Segment> segments)
        {
            var data = new List<dynamic>();
            foreach (Segment segment in segments)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("ClusterID", segment.SegmentClusterId.ToString());
                record.Add("CreationDate", segment.CreationDate.ToString());
                record.Add("SegmentID", segment.SegmentID.ToString());
                record.Add("Relationship", segment.SegmentClusterTypeLabel.ToString());

                if (segment.CreationUserID == 1)
                    record.Add("Edit By", "System");
                else
                    record.Add("Edit By", "User");

                record.Add("ItemID", segment.BookID.ToString());
                record.Add("StartPageID", segment.StartPageID.ToString());
                record.Add("Type", segment.GenreName);
                record.Add("Title", segment.Title);
                record.Add("Container", segment.ContainerTitle);
                record.Add("Volume", segment.Volume);
                record.Add("Date", segment.Date);
                record.Add("Authors", segment.Authors.Replace("|", " - "));
                record.Add("DOI", segment.DOIName);
                record.Add("StartPage", segment.StartPageNumber);
                record.Add("EndPage", segment.EndPageNumber);
                record.Add("PageRange", segment.PageRange);
                data.Add(record);
            }

            byte[] csvBytes = new CSV().FormatCSVData(data);
            context.Response.Write(Encoding.UTF8.GetString(csvBytes, 0, csvBytes.Length));
            context.Response.Flush();
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