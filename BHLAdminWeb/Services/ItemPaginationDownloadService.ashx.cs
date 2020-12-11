using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for ItemPaginationDownloadService
    /// </summary>
    public class ItemPaginationDownloadService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string response = String.Empty;

            string published = context.Request.QueryString["pub"] as string;
            int publishedOnly = Int32.MinValue;
            string institutionCode = context.Request.QueryString["inst"] as string;
            string paginationStatuses = context.Request.QueryString["psid"] as string;
            string startDate = context.Request.QueryString["sdate"] as string;
            string endDate = context.Request.QueryString["edate"] as string;

            // Make sure parameters are valid
            int verifyInt;
            paginationStatuses = String.IsNullOrEmpty(paginationStatuses) ? "" : paginationStatuses;
            if (!Int32.TryParse(published, out publishedOnly)) publishedOnly = 0;
            DateTime verifyDate;
            startDate = DateTime.TryParse(startDate, out verifyDate) ? startDate : "1/1/1980";
            endDate = DateTime.TryParse(endDate, out verifyDate) ? endDate : DateTime.Now.ToShortDateString();

            this.DoDownload(context, publishedOnly, institutionCode, paginationStatuses, startDate, endDate);
        }

        private void DoDownload(HttpContext context, int publishedOnly, string institutionCode, string paginationStatuses, string startDate, string endDate)
        {
            List<Item> searchResult = null;

            try
            {
                this.WriteHttpHeaders(context, "text/csv", "ItemPagination.csv");

                DataTable statusIDs = new DataTable();
                statusIDs.Columns.Add(new DataColumn("ID", typeof(int)));
                string[] statuses = paginationStatuses.Split('|');
                foreach (string status in statuses)
                {
                    if (!string.IsNullOrWhiteSpace(status)) statusIDs.Rows.Add(Convert.ToInt32(status));
                }

                // Get the data to output
                BHLProvider provider = new BHLProvider();

                // Search terms specified individually (title, author, volume, etc)
                searchResult = provider.ItemSelectPaginationReport(publishedOnly, institutionCode, statusIDs, 
                    Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), 1000000, 1, "InstitutionName, ItemID", "asc");

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
        private void GetReportCSVString(HttpContext context, List<Item> searchResult)
        {
            StringBuilder csvString = new StringBuilder();

            // Add a BOM (Byte-Order Mark) to the output
            csvString.Append(Encoding.UTF8.GetString(new UTF8Encoding(true).GetPreamble()));

            // Write file header
            csvString.AppendLine("\"TitleId\",\"Title\",\"BibliographicLevel\",\"ItemId\",\"InternetArchiveId\",\"Volume\",\"Year\",\"Item Status\",\"ScanningDate\",\"Holding Institution\",\"PaginationStatusName\",\"PaginationStatusDate\",\"PaginationUserName\",\"NumberOfPages\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (Item item in searchResult)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + item.PrimaryTitleID.ToString() + "\",");
                csvString.Append("\"" + item.FullTitle.Replace("\"", "\"\"") + "\",");
                csvString.Append("\"" + item.BibliographicLevel + "\",");
                csvString.Append("\"" + item.ItemID.ToString() + "\",");
                csvString.Append("\"" + item.BarCode + "\",");
                csvString.Append("\"" + item.Volume.Replace("\"", "\"\"") + "\",");
                csvString.Append("\"" + item.StartYear + "\",");
                csvString.Append("\"" + item.ItemStatusName.Replace("\"", "\"\"") + "\",");
                csvString.Append("\"" + item.ScanningDate.ToString() + "\",");
                csvString.Append("\"" + item.InstitutionStrings[0].Replace("\"", "\"\"") + "\",");
                csvString.Append("\"" + item.PaginationStatusName + "\",");
                csvString.Append("\"" + item.PaginationStatusDate.ToString() + "\",");
                csvString.Append("\"" + item.PaginationUserName.Replace("\"", "\"\"") + "\",");
                csvString.AppendLine("\"" + item.NumberOfPages.ToString() + "\",");

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