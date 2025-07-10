using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class NameController : Controller
    {
        // GET: Name
        [EnableThrottling]
        [BrowseOutputCache(VaryByParam = "*")]
        public ActionResult Index(string name)
        {
            NameModel model = new NameModel();

            // Read the parameters passed to the page
            if (name != null)
            {
                model.SearchName = name;
                model.NameParam = Server.UrlEncode(model.SearchName);
                model.SearchName = Server.HtmlEncode(model.SearchName).Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
            }

            NameResolved nameResolved = new BHLProvider().NameResolvedSelectByResolvedName(model.SearchName);
            if (nameResolved != null)
            {
                model.SearchName = nameResolved.CanonicalNameString;
                model.NameParam = Server.UrlEncode(nameResolved.CanonicalNameString.Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~'));
            }

            return View(model);
        }

        /// <summary>
        /// Provides the name bibiography data for the /name page
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <returns></returns>
        [EnableThrottling]
        public ActionResult NameList(string name, string rows, string page, string sidx, string sord)
        {
            string sortColumn = sidx;
            string sortOrder = sord;

            int verifyInt;
            // Make sure rows and page are valid integer values
            rows = string.IsNullOrEmpty(rows) ? "100" : (!Int32.TryParse(rows, out verifyInt) ? "100" : rows);
            page = string.IsNullOrEmpty(page) ? "1" : (!Int32.TryParse(page, out verifyInt) ? "1" : page);

            // Make sure sortColumn is a value column name
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "ShortTitle" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            BHLProvider service = new BHLProvider();
            NameSearchResult searchResult = null;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
                searchResult = service.NameResolvedSearchForPages(name, Convert.ToInt32(rows),
                    Convert.ToInt32(page), sortColumn, sortOrder);
            }

            string xmlString = GetXmlResponse(searchResult, Convert.ToInt32(page), Convert.ToInt32(rows));
            return this.Content(xmlString, "text/xml");
        }

        private string GetXmlResponse(NameSearchResult searchResult, int pageNum, int numRows)
        {
            StringBuilder response = new StringBuilder(); ;

            if (searchResult != null)
            {
                int totalPages = (int)Math.Ceiling((double)searchResult.PageCount / (double)numRows);
                int numRecords = searchResult.PageCount;

                // <page> = page number
                // <total> = total pages
                // <records> = number of records
                response.Append("<?xml version =\"1.0\" encoding=\"utf-8\"?>");
                response.Append("<rows>");
                response.Append("<page>" + pageNum + "</page>");
                response.Append("<total>" + totalPages.ToString() + "</total>");
                response.Append("<records>" + numRecords.ToString() + "</records>");

                for (int x = 0; x < searchResult.Pages.Count; x++)
                {
                    response.Append("<row id='" + searchResult.Pages[x].TitleID.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" href=\"/bibliography/" + searchResult.Pages[x].TitleID.ToString() + "\">" + searchResult.Pages[x].FullTitle + "</a>]]> </cell>");
                    response.Append("<cell> <![CDATA[" + searchResult.Pages[x].Authors.Replace("|", "<br>") + "]]> </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"View Book\" href=\"/item/" + searchResult.Pages[x].ItemID.ToString() + "\">");
                    if (string.IsNullOrEmpty(searchResult.Pages[x].Volume))
                    {
                        response.Append("(go to volume)");
                    }
                    else
                    {
                        response.Append(searchResult.Pages[x].Volume);
                    }
                    response.Append("</a>]]> </cell>");
                    response.Append("<cell> " + searchResult.Pages[x].Date + " </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"View Page\" href=\"/page/" + searchResult.Pages[x].PageID.ToString() + "\">");
                    if (string.IsNullOrEmpty(searchResult.Pages[x].IndicatedPages))
                    {
                        response.Append("(go to page)");
                    }
                    else
                    {
                        response.Append(searchResult.Pages[x].IndicatedPages);
                    }
                    response.Append("</a>]]> </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"Preview Page\" href=\"#\" onclick=\"showPreview(" + searchResult.Pages[x].PageID.ToString() + ");\"><img src=\"/images/view_page.gif\" alt=\"Preview Page\"></a>]]> </cell>");
                    response.Append("</row>");
                }
                response.Append("</rows>");
            }

            return response.ToString();
        }

        [EnableThrottling]
        public FileResult NameListDownload(string name, string type)
        {
            type = string.IsNullOrEmpty(type) ? "" : type;

            NameSearchResult searchResult = null;

            if (!string.IsNullOrWhiteSpace(name) && (type == "c" || type == "b"))
            {
                byte[] fileArray;
                string fileName;
                string contentType;
                name = name.Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');

                // Get the data to output
                BHLProvider provider = new BHLProvider();
                searchResult = provider.NameResolvedSearchForPagesDownload(name);

                switch (type)
                {
                    case "b":   // BiBTeX
                        {
                            fileName = "names.bib";
                            contentType = "application/x-bibtex";
                            fileArray = this.GetBibTeXString(searchResult);
                            break;
                        }
                    case "c":   // CSV
                    default:
                        {
                            fileName = "names.csv";
                            contentType = "text/csv";
                            fileArray = this.GetCSVString(searchResult);
                            break;
                        }
                }

                // Finish the download
                return File(fileArray, contentType, fileName);
            }

            return null;
        }

        /// <summary>
        /// Write the search results into CSV
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private byte[] GetCSVString(NameSearchResult searchResult)
        {
            var data = new List<dynamic>();
            foreach (NameSearchPage page in searchResult.Pages)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Url", string.Format(ConfigurationManager.AppSettings["PagePageUrl"].ToString(), page.PageID.ToString()));
                record.Add("Type", page.BibliographicLevelLabel);
                record.Add("Title", page.FullTitle);
                record.Add("Publisher Place", page.PublisherPlace);
                record.Add("Publisher", page.Publisher);
                record.Add("Date", page.Date);
                record.Add("Authors", page.Authors);
                record.Add("Volume", page.Volume);
                record.Add("Language", page.LanguageName);
                record.Add("Pages", page.IndicatedPages);
                data.Add(record);
            }

            byte[] csvBytes = new CSV().FormatCSVData(data);
            return csvBytes;
        }

        /// <summary>
        /// Write the search results into the BibTeX format
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private byte[] GetBibTeXString(NameSearchResult searchResult)
        {
            StringBuilder bibtexString = new StringBuilder("");

            foreach (NameSearchPage page in searchResult.Pages)
            {
                string volume = page.Volume;
                string url = string.Format(ConfigurationManager.AppSettings["PagePageUrl"].ToString(), page.PageID.ToString());
                string pages = page.IndicatedPages;

                Dictionary<string, string> elements = new Dictionary<string, string>();
                elements.Add(BibTeXRefElementName.TITLE, page.FullTitle);
                if (volume != string.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
                if (url != string.Empty) elements.Add(BibTeXRefElementName.URL, url);
                elements.Add(BibTeXRefElementName.PUBLISHER, page.Publisher);
                elements.Add(BibTeXRefElementName.AUTHOR, page.Authors.Replace("|", " and "));
                elements.Add(BibTeXRefElementName.YEAR, page.Date);
                if (pages != string.Empty) elements.Add(BibTeXRefElementName.PAGES, pages);

                BibTeX bibTex = new BibTeX(BibTeXRefType.BOOK, "bhlpg" + page.PageID.ToString(), elements);

                bibtexString.Append(bibTex.GenerateReference());
            }

            byte[] bibBytes = Encoding.UTF8.GetBytes(bibtexString.ToString());
            return bibBytes;
        }
    }
}