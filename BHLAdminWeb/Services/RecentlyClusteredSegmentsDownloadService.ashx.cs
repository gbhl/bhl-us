using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

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
            CustomGenericList<Segment> segments = null;

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
        private void GetReportCSVString(HttpContext context, CustomGenericList<Segment> segments)
        {
            StringBuilder csvString = new StringBuilder();

            // Write file header
            csvString.AppendLine("\"ClusterID\",\"Date\",\"SegmentID\",\"Relationship\",\"Edit By\",\"ItemID\",\"StartPageID\",\"Genre\",\"Title\",\"Container\",\"Volume\",\"Date\",\"Authors\",\"DOI\",\"StartPage\",\"EndPage\",\"PageRange\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (Segment segment in segments)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + segment.SegmentClusterId.ToString() + "\",");
                csvString.Append("\"" + segment.CreationDate.ToString() + "\",");
                csvString.Append("\"" + segment.SegmentID.ToString() + "\",");
                csvString.Append("\"" + segment.SegmentClusterTypeLabel.ToString() + "\",");
                if (segment.CreationUserID == 1)
                    csvString.Append("\"System\",");
                else 
                    csvString.Append("\"User\",");
                csvString.Append("\"" + segment.ItemID.ToString() + "\",");
                csvString.Append("\"" + segment.StartPageID.ToString() + "\",");
                csvString.Append("\"" + segment.GenreName + "\",");
                csvString.Append("\"" + segment.Title+ "\",");
                csvString.Append("\"" + segment.ContainerTitle + "\",");
                csvString.Append("\"" + segment.Volume + "\",");
                csvString.Append("\"" + segment.Date + "\",");
                csvString.Append("\"" + segment.Authors.Replace("|", " - ") + "\",");
                csvString.Append("\"" + segment.DOIName + "\",");
                csvString.Append("\"" + segment.StartPageNumber + "\",");
                csvString.Append("\"" + segment.EndPageNumber + "\",");
                csvString.AppendLine("\"" + segment.PageRange + "\",");

                context.Response.Write(csvString.ToString());
                context.Response.Flush();
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