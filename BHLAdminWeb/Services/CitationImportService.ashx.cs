using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for CitationImportService
    /// </summary>
    public class CitationImportService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string response = String.Empty;

            string importFileID = context.Request.QueryString["fid"] as string;

            // Make sure parameters are valid
            int verifyInt;
            importFileID = String.IsNullOrEmpty(importFileID) ? "0" : (!Int32.TryParse(importFileID, out verifyInt) ? "0" : importFileID);

            this.DoDownload(context, importFileID);
        }

        private void DoDownload(HttpContext context, string importFileID)
        {
            CustomGenericList<ImportRecord> searchResult = null;

            try
            {
                this.WriteHttpHeaders(context, "text/csv", "CitationImport" + importFileID + ".csv");

                // Get the data to output
                BHLProvider provider = new BHLProvider();

                // Search terms specified individually (title, author, volume, etc)
                searchResult = provider.ImportRecordSelectByImportFileID(Convert.ToInt32(importFileID), 100000, 1, "Title", "asc", 1);

                // Output the data as CSV
                this.GetReportCSVString(context, searchResult);
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
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private void GetReportCSVString(HttpContext context, CustomGenericList<ImportRecord> searchResult)
        {
            StringBuilder csvString = new StringBuilder();

            // Start the response content with a UTF-8 Byte Order Mark
            context.Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            context.Response.Flush();

            // Write file header
            csvString.AppendLine("\"Status\",\"Genre\",\"Title\",\"Translated Title\",\"Authors\",\"Keywords\",\"Journal\",\"Volume\",\"Series\",\"Issue\",\"Edition\",\"Publication Details\",\"Publisher Name\",\"Publisher Place\",\"Year\",\"Journal Start Year\",\"Journal End Year\",\"Language\",\"Rights\",\"DueDiligence\",\"CopyrightStatus\",\"License\",\"LicenseUrl\",\"PageRange\",\"StartPage\",\"EndPage\",\"Url\",\"DownloadUrl\",\"DOI\",\"ISSN\",\"ISBN\",\"OCLC\",\"LCCN\",\"Summary\",\"Notes\"");

            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (ImportRecord record in searchResult)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + record.StatusName + "\",");
                csvString.Append("\"" + record.Genre + "\",");
                csvString.Append("\"" + record.Title.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.TranslatedTitle.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.AuthorString.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.KeywordString.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.JournalTitle.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.Volume.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.Series.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.Issue.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.Edition.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.PublicationDetails.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.PublisherName.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.PublisherPlace.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.Year + "\",");
                csvString.Append("\"" + (record.StartYear == null ? "" : record.StartYear.ToString()) + "\",");
                csvString.Append("\"" + (record.EndYear == null ? "" : record.EndYear.ToString()) + "\",");
                csvString.Append("\"" + record.Language.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.Rights.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.DueDiligence.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.CopyrightStatus.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.License.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.LicenseUrl.Replace("\"", "'") + "\",");
                csvString.Append("\"" + record.PageRange + "\",");
                csvString.Append("\"" + record.StartPage + "\",");
                csvString.Append("\"" + record.EndPage + "\",");
                csvString.Append("\"" + record.Url + "\",");
                csvString.Append("\"" + record.DownloadUrl + "\",");
                csvString.Append("\"" + record.DOI + "\",");
                csvString.Append("\"" + record.ISSN + "\",");
                csvString.Append("\"" + record.ISBN + "\",");
                csvString.Append("\"" + record.OCLC + "\",");
                csvString.Append("\"" + record.LCCN + "\",");
                csvString.Append("\"" + record.Summary.Replace('\n', ' ').Replace('\r', ' ').Replace("\"", "'") + "\",");
                csvString.AppendLine("\"" + record.Notes.Replace('\n', ' ').Replace('\r', ' ').Replace("\"", "'") + "\",");

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