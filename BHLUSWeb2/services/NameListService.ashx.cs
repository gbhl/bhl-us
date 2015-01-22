using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.Web2.Services
{
    /// <summary>
    /// Handler for requests coming from the NameList.aspx page, in particular from the jqGrid object
    /// contained on that page.
    /// </summary>
    public class NameListService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            String name = context.Request.QueryString["name"] as String;
            name = name.Replace('_', ' ').Replace('$', '.').Replace('^', '?').Replace('~', '&');
            String numRows = context.Request.QueryString["rows"] as String;
            String pageNum = context.Request.QueryString["page"] as String;
            String sortColumn = context.Request.QueryString["sidx"] as String;
            String sortOrder = context.Request.QueryString["sord"] as String;

            int verifyInt;
            // Make sure numRows and pageNum are valid integer values
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "ShortTitle" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            BHLProvider service = new BHLProvider();
            NameSearchResult searchResult = null;
            
            if (!String.IsNullOrEmpty(name)) searchResult = service.NameResolvedSearchForPages(
                name, Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

            context.Response.ContentType = "text/xml";
            context.Response.Write(GetXmlResponse(searchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows)));
        }

        private string GetXmlResponse(NameSearchResult searchResult, int pageNum, int numRows)
        {
            StringBuilder response = new StringBuilder();;

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
                    //response.Append("<cell> " + searchResult.Pages[x].BibliographicLevelName + " </cell>");
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}