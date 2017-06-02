using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Web2.Services
{
    /// <summary>
    /// Summary description for SearchDownloadService
    /// </summary>
    public class SearchDownloadService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string response = String.Empty;

            string searchTerm = string.Empty;
            string searchCat = string.Empty;
            string searchContainerTitle = string.Empty;
            string searchLang = string.Empty;
            string searchLastName = string.Empty;
            string searchVolume = string.Empty;
            string searchEdition = string.Empty;
            string searchYear = string.Empty;
            string searchSubject = string.Empty;
            string searchCollection = string.Empty;
            string searchIssue = string.Empty;
            string searchStartPage = string.Empty;
            string searchArticleTitle = string.Empty;
            string downloadType = string.Empty;
            int searchCollectionInt;
            int searchYearInt;

            // Get all of the possible arguements
            if (context.Request["SearchTerm"] != null) searchTerm = context.Request["SearchTerm"].ToString();
            if (context.Request["SearchCat"] != null) searchCat = context.Request["SearchCat"].ToString();
            if (context.Request["cont"] != null) searchContainerTitle = context.Request["cont"].ToString();
            if (context.Request["lang"] != null) searchLang = context.Request["lang"].ToString();
            if (context.Request["lname"] != null) searchLastName = context.Request["lname"].ToString();
            if (context.Request["vol"] != null) searchVolume = context.Request["vol"].ToString();
            if (context.Request["ed"] != null) searchEdition = context.Request["ed"].ToString();
            if (context.Request["yr"] != null) searchYear = context.Request["yr"].ToString();
            if (context.Request["subj"] != null) searchSubject = context.Request["subj"].ToString();
            if (context.Request["col"] != null) searchCollection = context.Request["col"].ToString();
            if (context.Request["iss"] != null) searchIssue = context.Request["iss"].ToString();
            if (context.Request["spage"] != null) searchStartPage = context.Request["spage"].ToString();
            if (context.Request["atitle"] != null) searchArticleTitle = context.Request["atitle"].ToString();
            if (context.Request["dltype"] != null) downloadType = context.Request["dltype"].ToString();

            switch (downloadType)
            {
                case "T":
                    // Make sure we have valid search terms
                    if ((searchTerm != string.Empty || searchLastName != string.Empty || searchCollection != string.Empty) &&
                        (Int32.TryParse((searchYear == string.Empty ? "0" : searchYear), out searchYearInt) &&
                        Int32.TryParse((searchCollection == string.Empty ? "0" : searchCollection), out searchCollectionInt)))
                    {
                        PerformTitleDownload(context, searchCat, searchTerm, searchLang, searchLastName, searchVolume, searchEdition,
                            searchYearInt, searchSubject, searchCollectionInt, searchIssue, searchStartPage, searchArticleTitle);
                    }
                    break;
                case "S":
                    // Make sure we have valid search terms
                    if ((searchTerm != string.Empty || searchLastName != string.Empty || searchContainerTitle != string.Empty) &&
                        (Int32.TryParse((searchYear == string.Empty ? "0" : searchYear), out searchYearInt)))
                    {
                        PerformSegmentDownload(context, searchCat, searchTerm, searchContainerTitle, searchLastName, searchYear);
                    }
                    break;
            }
        }

        /// <summary>
        /// Execute the book query and write the results to the output stream
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchCat"></param>
        /// <param name="searchTerm"></param>
        /// <param name="searchLang"></param>
        /// <param name="searchLastName"></param>
        /// <param name="searchVolume"></param>
        /// <param name="searchEdition"></param>
        /// <param name="searchYear"></param>
        /// <param name="searchSubject"></param>
        /// <param name="searchCollection"></param>
        /// <param name="searchIssue"></param>
        /// <param name="searchStartPage"></param>
        /// <param name="searchArticleTitle"></param>
        /// <param name="downloadType"></param>
        private void PerformTitleDownload(HttpContext context, string searchCat, string searchTerm, string searchLang, 
            string searchLastName, string searchVolume, string searchEdition, int searchYear, string searchSubject, 
            int searchCollection, string searchIssue, string searchStartPage, string searchArticleTitle)
        {
            CustomGenericList<SearchBookResult> searchResult = null;

            try
            {
                this.WriteHttpHeaders(context, "text/csv", "BHLBookSearch.csv");

                // Get the data to output
                BHLProvider provider = new BHLProvider();

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]))
                {
                    if (searchCat.Length > 0)
                    {
                        // Search terms specified individually (title, author, volume, etc)
                        searchResult = provider.SearchBookFullText(searchTerm, searchLastName, searchVolume, searchEdition,
                            (searchYear == 0 ? null : (int?)searchYear), searchSubject, searchLang,
                            (searchCollection == 0 ? null : (int?)searchCollection), 500, "rank");
                    }
                    else
                    {
                        // Single search box
                        searchResult = provider.SearchBookFullText(searchTerm, 500, "rank");
                    }
                }
                else
                {
                    searchResult = provider.SearchBook(searchTerm, searchLastName, searchVolume, searchEdition,
                        (searchYear == 0 ? null : (int?)searchYear), searchSubject, searchLang,
                        (searchCollection == 0 ? null : (int?)searchCollection), 500, "title");
                }

                if (searchResult != null)
                {
                    foreach (SearchBookResult result in searchResult)
                    {
                        if (result.InstitutionName.Contains("|")) result.InstitutionName = "Multiple institutions";
                    }
                }

                // Output the data as CSV
                this.GetBookCSVString(context, searchResult);
            }
            finally
            {
                // Finish the download
                context.Response.End();
            }
        }

        /// <summary>
        /// Execute the segment query and write the results to the output stream
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchCat"></param>
        /// <param name="searchTerm"></param>
        /// <param name="searchLang"></param>
        /// <param name="searchLastName"></param>
        /// <param name="searchVolume"></param>
        /// <param name="searchEdition"></param>
        /// <param name="searchYear"></param>
        /// <param name="searchSubject"></param>
        /// <param name="searchCollection"></param>
        /// <param name="searchIssue"></param>
        /// <param name="searchStartPage"></param>
        /// <param name="searchArticleTitle"></param>
        private void PerformSegmentDownload(HttpContext context, string searchCat, string searchTerm, string searchContainerTitle,
            string searchLastName, string searchYear)
        {
            CustomGenericList<Segment> searchResult = null;

            try
            {
                this.WriteHttpHeaders(context, "text/csv", "BHLArticleSearch.csv");

                // Get the data to output
                BHLProvider provider = new BHLProvider();

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]))
                {
                    if (searchCat.Length > 0)
                    {
                        // Search terms specified individually (title, author, volume, etc)
                        searchResult = provider.SearchSegmentAdvancedFullText(searchTerm, searchContainerTitle, searchLastName, 
                            searchYear, string.Empty, string.Empty, string.Empty, 500, "rank");
                    }
                    else
                    {
                        // Single search box
                        searchResult = provider.SearchSegmentFullText(searchTerm, 500, "rank");
                    }
                }
                else
                {
                    searchResult = provider.SearchSegment(searchTerm, searchContainerTitle, searchLastName, searchYear, 
                        string.Empty, string.Empty, string.Empty, 500, "title");
                }

                // Output the data as CSV
                this.GetSegmentCSVString(context, searchResult);
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
        private void GetBookCSVString(HttpContext context, CustomGenericList<SearchBookResult> searchResult)
        {
            StringBuilder csvString = new StringBuilder();

            // Write file header
            csvString.AppendLine("\"TitleId\",\"TitleUrl\",\"ItemId\",\"ItemUrl\",\"Title\",\"PartNumber\",\"PartName\",\"Edition\",\"Publication Details\",\"Volume\",\"Authors\",\"Collections\",\"Holding Institution\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (SearchBookResult book in searchResult)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + book.TitleID.ToString() + "\",");
                csvString.Append("\"" + String.Format(ConfigurationManager.AppSettings["BibPageUrl"].ToString(), book.TitleID.ToString()) + "\",");
                csvString.Append("\"" + book.ItemID.ToString() + "\",");
                csvString.Append("\"" + String.Format(ConfigurationManager.AppSettings["ItemPageUrl"].ToString(), book.ItemID.ToString()) + "\",");
                csvString.Append("\"" + book.FullTitle.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.PartNumber.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.PartName.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.EditionStatement.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.PublicationDetails.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.Volume.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.Authors.Replace('"', '\'') + "\",");
                csvString.Append("\"" + book.Collections.Replace('"', '\'') + "\",");
                csvString.AppendLine("\"" + book.InstitutionName.Replace('"', '\'') + "\"");

                context.Response.Write(csvString.ToString());
                context.Response.Flush();
            }
        }

        /// <summary>
        /// Write the search results into CSV
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private void GetSegmentCSVString(HttpContext context, CustomGenericList<Segment> searchResult)
        {
            StringBuilder csvString = new StringBuilder();

            // Write file header
            csvString.AppendLine("\"PartId\",\"PartUrl\",\"Type\",\"Title\",\"Container\",\"Volume\",\"Date\",\"Start Page\",\"End Page\",\"Authors\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (Segment segment in searchResult)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + segment.SegmentID.ToString() + "\",");
                csvString.Append("\"" + String.Format(ConfigurationManager.AppSettings["PartPageUrl"].ToString(), segment.SegmentID.ToString()) + "\",");
                csvString.Append("\"" + segment.GenreName.Replace('"', '\'') + "\",");
                csvString.Append("\"" + segment.Title.Replace('"', '\'') + "\",");
                csvString.Append("\"" + segment.ContainerTitle.Replace('"', '\'') + "\",");
                csvString.Append("\"" + segment.Volume.Replace('"', '\'') + "\",");
                csvString.Append("\"" + segment.Date.Replace('"', '\'') + "\",");
                csvString.Append("\"" + segment.StartPageNumber.Replace('"', '\'') + "\",");
                csvString.Append("\"" + segment.EndPageNumber.Replace('"', '\'') + "\",");
                csvString.AppendLine("\"" + segment.Authors.Replace('"', '\'') + "\",");

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