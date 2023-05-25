using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Text;
using System.Web;

namespace MOBOT.BHL.Web2.Services
{
    /// <summary>
    /// Summary description for SearchDownloadService
    /// </summary>
    public class SearchDownloadService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
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
            string downloadType = string.Empty;

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
            if (context.Request["dltype"] != null) downloadType = context.Request["dltype"].ToString();

            switch (downloadType)
            {
                case "T":
                    // Make sure we have valid search terms
                    if ((searchTerm != string.Empty || searchLastName != string.Empty || searchCollection != string.Empty) &&
                        (Int32.TryParse((searchYear == string.Empty ? "0" : searchYear), out int searchYearInt) &&
                        Int32.TryParse((searchCollection == string.Empty ? "0" : searchCollection), out int searchCollectionInt)))
                    {
                        PerformTitleDownload(context, searchCat, searchTerm, searchLang, searchLastName, searchVolume, searchEdition,
                            searchYearInt, searchSubject, searchCollectionInt);
                    }
                    break;
                case "S":
                    // Make sure we have valid search terms
                    if ((searchTerm != string.Empty || searchLastName != string.Empty || searchContainerTitle != string.Empty) &&
                        (Int32.TryParse((searchYear == string.Empty ? "0" : searchYear), out _)))
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
        /// <param name="downloadType"></param>
        private void PerformTitleDownload(HttpContext context, string searchCat, string searchTerm, string searchLang, 
            string searchLastName, string searchVolume, string searchEdition, int searchYear, string searchSubject, 
            int searchCollection)
        {
            List<SearchBookResult> searchResult;

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
        /// <param name="searchLastName"></param>
        /// <param name="searchContainerTitle"></param>
        /// <param name="searchYear"></param>
        private void PerformSegmentDownload(HttpContext context, string searchCat, string searchTerm, string searchContainerTitle,
            string searchLastName, string searchYear)
        {
            List<Segment> searchResult;

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
        private void GetBookCSVString(HttpContext context, List<SearchBookResult> searchResult)
        {
            var data = new List<dynamic>();
            foreach (SearchBookResult book in searchResult)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("TitleId", book.TitleID.ToString());
                record.Add("TitleUrl", string.Format(ConfigurationManager.AppSettings["BibPageUrl"].ToString(), book.TitleID.ToString()));
                record.Add("ItemId", book.ItemID.ToString() );
                record.Add("ItemUrl", string.Format(ConfigurationManager.AppSettings["ItemPageUrl"].ToString(), book.ItemID.ToString()));
                record.Add("Title", book.FullTitle);
                record.Add("PartNumber", book.PartNumber);
                record.Add("PartName", book.PartName);
                record.Add("Edition", book.EditionStatement);
                record.Add("Publication Details", book.PublicationDetails);
                record.Add("Volume", book.Volume);
                record.Add("Authors", book.Authors);
                record.Add("Collections", book.Collections);
                record.Add("Holding Institution", book.InstitutionName);
                data.Add(record);
            }

            byte[] csvBytes = new CSV().FormatCSVData(data);
            context.Response.Write(Encoding.UTF8.GetString(csvBytes, 0, csvBytes.Length));
            context.Response.Flush();
        }

        /// <summary>
        /// Write the search results into CSV
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private void GetSegmentCSVString(HttpContext context, List<Segment> searchResult)
        {
            var data = new List<dynamic>();
            foreach (Segment segment in searchResult)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("PartId", segment.SegmentID.ToString());
                record.Add("PartUrl", string.Format(ConfigurationManager.AppSettings["PartPageUrl"].ToString(), segment.SegmentID.ToString()));
                record.Add("Type", segment.GenreName);
                record.Add("Title", segment.Title);
                record.Add("Container", segment.ContainerTitle);
                record.Add("Volume", segment.Volume);
                record.Add("Date", segment.Date);
                record.Add("Start Page", segment.StartPageNumber);
                record.Add("End Page", segment.EndPageNumber);
                record.Add("Authors", segment.Authors);
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