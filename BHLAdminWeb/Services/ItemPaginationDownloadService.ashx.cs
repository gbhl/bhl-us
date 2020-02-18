using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
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

            string paginationStatusId = context.Request.QueryString["psid"] as string;
            string startDate = context.Request.QueryString["sdate"] as string;
            string endDate = context.Request.QueryString["edate"] as string;

            // Make sure parameters are valid
            int verifyInt;
            paginationStatusId = String.IsNullOrEmpty(paginationStatusId) ? "0" : (!Int32.TryParse(paginationStatusId, out verifyInt) ? "0" : paginationStatusId);
            DateTime verifyDate;
            startDate = DateTime.TryParse(startDate, out verifyDate) ? startDate : "1/1/1980";
            endDate = DateTime.TryParse(endDate, out verifyDate) ? endDate : DateTime.Now.ToShortDateString();

            this.DoDownload(context, paginationStatusId, startDate, endDate);
        }

        private void DoDownload(HttpContext context, string paginationStatusId, string startDate, string endDate)
        {
            List<Item> searchResult = null;

            try
            {
                this.WriteHttpHeaders(context, "text/csv", "ItemPagination.csv");

                // Get the data to output
                BHLProvider provider = new BHLProvider();

                // Search terms specified individually (title, author, volume, etc)
                searchResult = provider.ItemSelectPaginationReport(Convert.ToInt32(paginationStatusId), 
                    Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), 1000000, 1, "ItemID", "asc");

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

            // Write file header
            csvString.AppendLine("\"ItemId\",\"InternetArchiveId\",\"PaginationStatusName\",\"PaginationStatusDate\",\"PaginationUserName\",\"NumberOfPages\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (Item item in searchResult)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + item.ItemID.ToString() + "\",");
                csvString.Append("\"" + item.BarCode + "\",");
                csvString.Append("\"" + item.PaginationStatusName + "\",");
                csvString.Append("\"" + item.PaginationStatusDate.ToString() + "\",");
                csvString.Append("\"" + item.PaginationUserName + "\",");
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