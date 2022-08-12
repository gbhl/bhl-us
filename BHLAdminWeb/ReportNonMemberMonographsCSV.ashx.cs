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
    /// Summary description for ReportNonMemberMonographsCSV
    /// </summary>
    public class ReportNonMemberMonographsCSV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sinceDate = context.Request.QueryString["date"] as string;
            int isMember = Int32.Parse(context.Request.QueryString["member"]);
            string institutionCode = context.Request.QueryString["inst"] as string;

            BHLProvider provider = new BHLProvider();
            List<NonMemberMonograph> monographs = provider.ItemSelectNonMemberMonograph(sinceDate, isMember, institutionCode);

            this.WriteHttpHeaders(context, "text/csv", "MonographContributions" + DateTime.Now.ToString("yyyyMMdd") + ".csv");

            var data = new List<dynamic>();
            foreach (NonMemberMonograph monograph in monographs)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Title ID", monograph.TitleID.ToString());
                record.Add("OCLC", monograph.Oclc);
                record.Add("Full Title", monograph.FullTitle);
                record.Add("Authors", monograph.Authors);
                record.Add("Volume", monograph.Volume);
                record.Add("Start Year", monograph.StartYear);
                record.Add("Call Number", monograph.CallNumber);
                record.Add("Publisher", monograph.Publisher);
                record.Add("Publisher Place", monograph.PublisherPlace);
                record.Add("Item ID", monograph.ItemID.ToString());
                record.Add("Identifier Bib", monograph.IdentifierBib);
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