using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb
{
    /// <summary>
    /// Summary description for ReportDOIByInstitutionCSV
    /// </summary>
    public class ReportDOIByInstitutionCSV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sort = context.Request.QueryString["s"] as string;
            string includeAll = context.Request.QueryString["i"] as string;
            string bhlOnly = context.Request.QueryString["b"] as string;

            BHLProvider provider = new BHLProvider();
            List<Institution> institutions = provider.InstitutionSelectDOIStats(Convert.ToInt32(sort), Convert.ToInt32(includeAll));

            if (bhlOnly == "1")
            {
                // Only show BHL member libraries
                for (int x = institutions.Count - 1; x >= 0; x--)
                {
                    if (!institutions[x].BHLMemberLibrary) institutions.RemoveAt(x);
                }
            }

            this.WriteHttpHeaders(context, "text/csv", "DOIsByInstitution" + DateTime.Now.ToString("yyyyMMdd") + ".csv");

            var data = new List<dynamic>();
            foreach (Institution institution in institutions)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Content Provider", institution.InstitutionName);
                record.Add("BHL-Minted Title DOIs", institution.TitleMinted.ToString());
                record.Add("BHL-Acquired Title DOIs", institution.TitleAcquired.ToString());
                record.Add("Non-BHL Title DOIs", institution.TitleNonBHL.ToString());
                record.Add("Total Title DOIs", institution.TitleTotalDOIs.ToString());
                record.Add("BHL-Minted Segment DOIs", institution.SegmentMinted.ToString());
                record.Add("BHL-Acquired Segment DOIs", institution.SegmentAcquired.ToString());
                record.Add("Non-BHL Segment DOIs", institution.SegmentNonBHL.ToString());
                record.Add("Total Segment DOIs", institution.SegmentTotalDOIs.ToString());
                record.Add("Total DOIs", institution.TotalDOIs.ToString());
                data.Add(record);
            }

            byte[] csvBytes = new CSV().FormatCSVData(data);
            context.Response.Write(Encoding.UTF8.GetString(csvBytes, 0, csvBytes.Length));
            context.Response.Flush();
            context.Response.End();
        }

        /// <summary>
        /// Write the HTTP header information for the download
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        public void WriteHttpHeaders(HttpContext context, string contentType, string fileName)
        {
            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = contentType;
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
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