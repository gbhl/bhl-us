using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
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

            // Write file header
            StringBuilder csvString = new StringBuilder();
            csvString.AppendLine("\"Title ID\",\"OCLC\",\"Full Title\",\"Authors\",\"Volume\",\"Start Year\",\"Call Number\",\"Publisher\",\"Publisher Place\",\"Item ID\",\"Identifier Bib\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (NonMemberMonograph monograph in monographs)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + monograph.TitleID.ToString() + "\",");
                csvString.Append("\"" + monograph.Oclc + "\",");
                csvString.Append("\"" + monograph.FullTitle.Replace('"', '\'') + "\",");
                csvString.Append("\"" + monograph.Authors.Replace('"', '\'') + "\",");
                csvString.Append("\"" + monograph.Volume.Replace('"', '\'') + "\",");
                csvString.Append("\"" + monograph.StartYear + "\",");
                csvString.Append("\"" + monograph.CallNumber.Replace('"', '\'') + "\",");
                csvString.Append("\"" + monograph.Publisher.Replace('"', '\'') + "\",");
                csvString.Append("\"" + monograph.PublisherPlace.Replace('"', '\'') + "\",");
                csvString.Append("\"" + monograph.ItemID.ToString() + "\",");
                csvString.AppendLine("\"" + monograph.IdentifierBib.Replace('"', '\'') + "\",");

                context.Response.Write(csvString.ToString());
                context.Response.Flush();
            }

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