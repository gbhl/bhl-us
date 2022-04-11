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
            List<ImportRecord> searchResult = null;

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
        private void GetReportCSVString(HttpContext context, List<ImportRecord> searchResult)
        {
            var data = new List<dynamic>();
            foreach (ImportRecord result in searchResult)
            {
                if (!string.IsNullOrWhiteSpace(result.AuthorString))
                {
                    string[] authors = result.AuthorString.Split(new string[] { "+++" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> newAuthors = new List<string>();
                    foreach (string author in authors) newAuthors.Add(author.Split('|')[1]);
                    result.AuthorString = string.Join("+++", newAuthors.ToArray());
                }

                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Status", result.StatusName);
                record.Add("Type", result.Genre);
                record.Add("ItemID", result.ItemID.ToString());
                record.Add("SegmentID", result.SegmentID.ToString());
                record.Add("Title", result.Title);
                record.Add("Translated Title", result.TranslatedTitle);
                record.Add("Authors", result.AuthorString);
                record.Add("Keywords", result.KeywordString);
                record.Add("Contributors", result.ContributorString);
                record.Add("Journal", result.JournalTitle);
                record.Add("Volume", result.Volume);
                record.Add("Series", result.Series);
                record.Add("Issue", result.Issue);
                record.Add("Edition", result.Edition);
                record.Add("Publication Details", result.PublicationDetails);
                record.Add("Publisher Name", result.PublisherName);
                record.Add("Publisher Place", result.PublisherPlace);
                record.Add("Year", result.Year);
                record.Add("Journal Start Year", (result.StartYear == null ? "" : result.StartYear.ToString()));
                record.Add("Journal End Year", (result.EndYear == null ? "" : result.EndYear.ToString()));
                record.Add("Language", result.Language);
                record.Add("Rights", result.Rights);
                record.Add("DueDiligence", result.DueDiligence);
                record.Add("CopyrightStatus", result.CopyrightStatus);
                record.Add("License", result.License);
                record.Add("LicenseUrl", result.LicenseUrl);
                record.Add("PageRange", result.PageRange);
                record.Add("StartPage", result.StartPage);
                record.Add("EndPage", result.EndPage);
                record.Add("Url", result.Url);
                record.Add("DownloadUrl", result.DownloadUrl);
                record.Add("Article DOI", result.DOI);
                record.Add("ISSN", result.ISSN);
                record.Add("ISBN", result.ISBN);
                record.Add("OCLC", result.OCLC);
                record.Add("LCCN", result.LCCN);
                record.Add("ARK", result.ARK);
                record.Add("BioStor", result.Biostor);
                record.Add("JSTOR", result.JSTOR);
                record.Add("TL2", result.TL2);
                record.Add("Wikidata", result.Wikidata);
                record.Add("Summary", result.Summary);
                record.Add("Notes", result.Notes);
                record.Add("Pages", result.PageString);
                record.Add("Errors", result.ErrorString);
                record.Add("Warnings", result.WarningString);

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