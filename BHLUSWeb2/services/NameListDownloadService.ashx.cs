using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Web2.Services
{
    /// <summary>
    /// Handler for download requests coming from the NameList.aspx page
    /// </summary>
    public class NameListDownloadService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string response = String.Empty;

            string name = context.Request.QueryString["name"] as String;
            string type = context.Request.QueryString["type"] as String;
            type = string.IsNullOrEmpty(type) ? "" : type;

            NameSearchResult searchResult = null;

            if (!string.IsNullOrWhiteSpace(name) && (type == "c" || type == "b" || type == "e"))
            {
                name = name.Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');

                // Get the data to output
                BHLProvider provider = new BHLProvider();
                searchResult = provider.NameResolvedSearchForPagesDownload(name);

                switch (type)
                {
                    case "c":   // CSV
                        {
                            this.WriteHttpHeaders(context, "text/csv", "names.csv");
                            this.GetCSVString(context, searchResult);
                            break;
                        }
                    case "b":   // BiBTeX
                        {
                            this.WriteHttpHeaders(context, "application/x-bibtex", "names.bib");
                            this.GetBibTeXString(context, searchResult);
                            break;
                        }
                    case "r":   // RIS
                        {
                            this.WriteHttpHeaders(context, "application/x-research-info-systems", "names.ris");
                            this.GetRISString(context, searchResult);
                            break;
                        }
                }

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
        public void WriteHttpHeaders(HttpContext context, string contentType, string fileName)
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
        private void GetCSVString(HttpContext context, NameSearchResult searchResult)
        {
            StringBuilder csvString = new StringBuilder();

            // Write file header
            csvString.AppendLine("\"Url\",\"Type\",\"Title\",\"Publisher Place\",\"Publisher\",\"Date\",\"Authors\",\"Volume\",\"Language\",\"Pages\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (NameSearchPage page in searchResult.Pages)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + String.Format(ConfigurationManager.AppSettings["PagePageUrl"].ToString(), page.PageID.ToString()) + "\",");
                csvString.Append("\"" + page.BibliographicLevelName + "\",");
                csvString.Append("\"" + page.FullTitle.Replace('"', '\'') + "\",");
                csvString.Append("\"" + page.PublisherPlace.Replace('"', '\'') + "\",");
                csvString.Append("\"" + page.Publisher.Replace('"', '\'') + "\",");
                csvString.Append("\"" + page.Date.Replace('"', '\'') + "\",");
                csvString.Append("\"" + page.Authors.Replace('"', '\'') + "\",");
                csvString.Append("\"" + page.Volume.Replace('"', '\'') + "\",");
                csvString.Append("\"" + page.LanguageName.Replace('"', '\'') + "\",");
                csvString.AppendLine("\"" + page.IndicatedPages.Replace('"', '\'') + "\"");

                context.Response.Write(csvString.ToString());
                context.Response.Flush();
            }
        }

        /// <summary>
        /// Write the search results into the BibTeX format
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private void GetBibTeXString(HttpContext context, NameSearchResult searchResult)
        {
            StringBuilder bibtexString = new StringBuilder("");

            foreach (NameSearchPage page in searchResult.Pages)
            {
                String volume = page.Volume;
                String url = String.Format(ConfigurationManager.AppSettings["PagePageUrl"].ToString(), page.PageID.ToString());
                String pages = page.IndicatedPages;

                System.Collections.Generic.Dictionary<String, String> elements = new System.Collections.Generic.Dictionary<string, string>();
                elements.Add(BibTeXRefElementName.TITLE, page.FullTitle);
                if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
                if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
                elements.Add(BibTeXRefElementName.PUBLISHER, page.Publisher);
                elements.Add(BibTeXRefElementName.AUTHOR, page.Authors.Replace("|", " and "));
                elements.Add(BibTeXRefElementName.YEAR, page.Date);
                if (pages != String.Empty) elements.Add(BibTeXRefElementName.PAGES, pages);

                BibTeX bibTex = new BibTeX(BibTeXRefType.BOOK, "bhlpg" + page.PageID.ToString(), elements);

                bibtexString.Remove(0, bibtexString.Length);

                bibtexString.Append(bibTex.GenerateReference());
                context.Response.Write(bibtexString.ToString());
                context.Response.Flush();
            }
        }

        /// <summary>
        /// Write the search results into the RIS format
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private void GetRISString(HttpContext context, NameSearchResult searchResult)
        {
            StringBuilder risString = new StringBuilder("");

            foreach (NameSearchPage page in searchResult.Pages)
            {
                /*
                 * TODO: Complete this when RIS format is supported
                 *
                String type = page.BibliographicLevelName.Contains("Serial") ? "Serial" : "Book";
                String authors = page.Authors;
                String year = page.Date;
                String title = page.FullTitle;
                String shortTitle = page.ShortTitle;
                String secondaryTitle = (page.PartNumber + ' ' + page.PartName).Trim();
                String publisherPlace = page.PublisherPlace;
                String publisherName = page.Publisher;
                String volume = page.Volume;
                String callNumber = page.CallNumber;
                String language = page.LanguageName;
                String pages = page.IndicatedPages;
                String url = String.Format(ConfigurationManager.AppSettings["PagePageUrl"].ToString(), page.PageID.ToString());

                System.Collections.Generic.Dictionary<String, String> elements = new System.Collections.Generic.Dictionary<string, string>();

                elements.Add(RISRefElementName.REFERENCETYPE, type);
                if (authors != String.Empty) elements.Add(RISRefElementName.AUTHORS, authors);
                if (year != String.Empty) elements.Add(RISRefElementName.YEAR, year);
                if (title != String.Empty) elements.Add(RISRefElementName.TITLE, title);
                if (shortTitle != String.Empty) elements.Add(RISRefElementName.SHORTTITLE, shortTitle);
                if (secondaryTitle != String.Empty) elements.Add(RISRefElementName.SECONDARYTITLE, secondaryTitle);
                if (publisherPlace != String.Empty) elements.Add(RISRefElementName.CITY, publisherPlace);
                if (publisherName != String.Empty) elements.Add(RISRefElementName.PUBLISHER, publisherName);
                if (volume != String.Empty) elements.Add(RISRefElementName.VOLUME, volume);
                if (callNumber != String.Empty) elements.Add(RISRefElementName.CALLNUMBER, callNumber);
                if (language != String.Empty) elements.Add(RISRefElementName.LANGUAGE, language);
                if (pages != String.Empty) elements.Add(RISRefElementName.PAGES, pages);
                if (url != String.Empty) elements.Add(RISRefElementName.URL, url);

                RIS ris = new RIS(type, elements);
                risString.Remove(0, risString.Length);
                risString.Append(ris.GenerateReference());
                */
                context.Response.Write(risString.ToString());
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