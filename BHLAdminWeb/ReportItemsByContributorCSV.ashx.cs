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

            var data = new List<dynamic>();
            foreach (Book book in books)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Content Provider", institution.InstitutionName);
                record.Add("Item ID", book.BookID.ToString());
                record.Add("IA Identifier", book.BarCode);
                record.Add("Title", (book.TitleName ?? string.Empty));
                record.Add("Volume", (book.Volume ?? String.Empty));
                record.Add("Year", (book.StartYear ?? String.Empty));
                record.Add("Authors", book.AuthorListString);
                record.Add("Copyright Status", book.CopyrightStatus);
                record.Add("Rights", book.Rights);
                record.Add("License Type", book.LicenseUrl);
                record.Add("Due Diligence", book.DueDiligence);
                record.Add("Date Added", book.CreationDate);
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