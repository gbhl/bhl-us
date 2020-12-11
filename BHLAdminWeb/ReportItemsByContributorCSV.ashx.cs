using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb
{
    /// <summary>
    /// Summary description for ReportItemsByContributorCSV
    /// </summary>
    public class ReportItemsByContributorCSV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string institutionCode = context.Request.QueryString["inst"] as string;

            BHLProvider provider = new BHLProvider();
            Institution institution = provider.InstitutionSelectAuto(institutionCode);
            List<Book> books = provider.BookSelectByInstitution(institutionCode, 1000000, "Date");

            this.WriteHttpHeaders(context, "text/csv", "ItemsByContentProvider" + institutionCode + DateTime.Now.ToString("yyyyMMdd") + ".csv");

            // Write file header
            StringBuilder csvString = new StringBuilder();
            csvString.AppendLine("\"Content Provider\",\"Item ID\",\"IA Identifier\",\"Title\",\"Volume\",\"Year\",\"Authors\",\"Copyright Status\",\"Rights\",\"License Type\",\"Due Diligence\",\"Date Added\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (Book book in books)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + institution.InstitutionName + "\",");
                csvString.Append("\"" + book.ItemID.ToString() + "\",");
                csvString.Append("\"" + book.BarCode + "\",");
                csvString.Append("\"" + (book.TitleName ?? string.Empty).Replace('"', '\'') + "\",");
                csvString.Append("\"" + (book.Volume ?? string.Empty).Replace('"', '\'') + "\",");
                csvString.Append("\"" + (book.StartYear ?? string.Empty).Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.AuthorListString + "\",");
                csvString.Append("\"" + book.CopyrightStatus + "\",");
                csvString.Append("\"" + book.Rights + "\",");
                csvString.Append("\"" + book.LicenseUrl + "\",");
                csvString.Append("\"" + book.DueDiligence + "\",");
                csvString.AppendLine("\"" + book.CreationDate + "\"");

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