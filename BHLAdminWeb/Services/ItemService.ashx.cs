using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Linq;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ItemService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            string qsItemID = context.Request.QueryString["itemID"] as string;
            string barCode = context.Request.QueryString["barcode"] as string;
            string qsTitleID = context.Request.QueryString["titleID"] as string;
            string marcBibId = context.Request.QueryString["marcBibId"] as string;

            string published = context.Request.QueryString["pub"] as string;
            int publishedOnly = Int32.MinValue;
            string institutionCode = context.Request.QueryString["inst"] as string;
            string paginationStatuses = context.Request.QueryString["psid"] as string;
            string startDate = context.Request.QueryString["sdate"] as string;
            string endDate = context.Request.QueryString["edate"] as string;
            string numRows = context.Request.QueryString["rows"] as string;
            string pageNum = context.Request.QueryString["page"] as string;
            string sortColumn = context.Request.QueryString["sidx"] as string;
            string sortOrder = context.Request.QueryString["sord"] as string;

            string qsVirtual = context.Request.QueryString["virtual"] as string;

            int itemID;
            Int32.TryParse(qsItemID, out itemID);
            barCode = (barCode ?? "");
            int titleID;
            Int32.TryParse(qsTitleID, out titleID);
            marcBibId = (marcBibId ?? "");

            // Make sure paginationStatusId, startDate, endDate, numRows, and pageNum are valid values
            int verifyInt;
            paginationStatuses = String.IsNullOrEmpty(paginationStatuses) ? "" : paginationStatuses;
            if (!Int32.TryParse(published, out publishedOnly)) publishedOnly = 0;
            DateTime verifyDate;
            startDate = DateTime.TryParse(startDate, out verifyDate) ? startDate : "1/1/1980";
            endDate = DateTime.TryParse(endDate, out verifyDate) ? endDate : DateTime.Now.ToShortDateString();
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "IAIdentifier" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            int? virtualOnly = null;
            if (qsVirtual != null)
            {
                switch (qsVirtual.ToLower())
                {
                    case "true": virtualOnly = 1; break;
                    case "false": virtualOnly = 0; break;
                    default: virtualOnly = null; break;
                }
            }

            switch (context.Request.QueryString["op"])
            {
                case "ItemSearch":
                    {
                        context.Response.ContentType = "application/json";
                        response = this.ItemSearch(itemID, barCode, virtualOnly);
                        break;
                    }
                case "ItemSearchByTitle":
                    {
                        context.Response.ContentType = "application/json";
                        response = this.ItemSearchByTitle(titleID, marcBibId, virtualOnly);
                        break;
                    }
                case "ItemPaginationReport":
                    {
                        // Make sure sortColumn is a value column name
                        sortColumn = String.IsNullOrEmpty(sortColumn) ? "ItemID" : sortColumn;

                        context.Response.ContentType = "text/xml";
                        response = this.ItemPaginationReport(publishedOnly, institutionCode, paginationStatuses, Convert.ToDateTime(startDate),
                            Convert.ToDateTime(endDate), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }

            }

            context.Response.Write(response);
        }

        private string ItemSearch(int itemId, string sourceId, int? virtualOnly)
        {
            try
            {
                Book book = new BHLProvider().BookSelectByBarcodeOrItemID(itemId, sourceId);

                // If the "type" of book (virtual or non-virtual) was specified, then only return that type
                if (book != null && virtualOnly != null)
                {
                    if (book.IsVirtual != virtualOnly) book = null;
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(book);
            }
            catch
            {
                return null;
            }
        }

        private string ItemSearchByTitle(int titleId, String marcBibId, int? virtualOnly)
        {
            try
            {
                List<Book> books = null;
                if (titleId != 0)
                {
                    books = new BHLProvider().BookSelectByTitleId(titleId);
                }
                else
                {
                    books = new BHLProvider().BookSelectByMarcBibId(marcBibId);
                }

                // If the "type" of book (virtual or non-virtual) was specified, then only return that type
                if (books.Count > 0 && virtualOnly != null)
                {
                    books = books.Where(b => b.IsVirtual == virtualOnly).ToList();
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(books);
            }
            catch
            {
                return null;
            }
        }

        private string ItemPaginationReport(int publishedOnly, string institutionCode,
            string paginationStatuses, DateTime startDate, DateTime endDate, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            try
            {
                StringBuilder response = new StringBuilder();

                DataTable statusIDs = new DataTable();
                statusIDs.Columns.Add(new DataColumn("ID", typeof(int)));
                string[] statuses = paginationStatuses.Split('|');
                foreach (string status in statuses)
                {
                    if (!string.IsNullOrWhiteSpace(status)) statusIDs.Rows.Add(Convert.ToInt32(status));
                }

                List<Item> items = new BHLProvider().ItemSelectPaginationReport(publishedOnly, institutionCode, statusIDs,
                    startDate, endDate, numRows, pageNum, sortColumn, sortOrder);

                if (items.Count > 0)
                {
                    int totalPages = (int)Math.Ceiling((double)items[0].TotalItems / (double)numRows);
                    int numRecords = items[0].TotalItems;

                    // <page> = page number
                    // <total> = total pages
                    // <records> = number of records
                    response.Append("<?xml version =\"1.0\" encoding=\"utf-8\"?>");
                    response.Append("<rows>");
                    response.Append("<page>" + pageNum + "</page>");
                    response.Append("<total>" + totalPages.ToString() + "</total>");
                    response.Append("<records>" + numRecords.ToString() + "</records>");

                    foreach (Item item in items)
                    {
                        response.Append("<row id='" + (item.ItemTypeID == 10 ? "b" : "s") + item.ItemID.ToString() + "'>");
                        response.Append("<cell> <![CDATA[<a title=\"Title Info\" href=\"titleedit.aspx?id=" + item.PrimaryTitleID.ToString() + "\">" + item.PrimaryTitleID.ToString() + "</a>]]> </cell>");
                        response.Append("<cell> <![CDATA[" + item.FullTitle + "]]> </cell>");
                        response.Append("<cell> " + item.BibliographicLevel + " </cell>");
                        response.Append("<cell> <![CDATA[<a title=\"Item Info\" href=\"" + (item.ItemTypeID == 10 ? "item" : "segment") + "edit.aspx?id=" + item.ItemID.ToString() + "\">" + item.ItemID.ToString() + "</a>]]> </cell>");
                        response.Append("<cell> <![CDATA[" + item.Volume + "]]> </cell>");
                        response.Append("<cell> " + item.StartYear + " </cell>");
                        response.Append("<cell> <![CDATA[" + item.ItemStatusName + "]]> </cell>");
                        response.Append("<cell> " + item.ScanningDate.ToString() + " </cell>");
                        response.Append("<cell> <![CDATA[" + item.InstitutionStrings[0] + "]]> </cell>");
                        response.Append("<cell> " + item.PaginationStatusName  + " </cell>");
                        response.Append("<cell> " + item.PaginationStatusDate.ToString() + " </cell>");
                        response.Append("<cell> " + item.PaginationUserName + " </cell>");
                        response.Append("<cell> " + item.NumberOfPages.ToString() + " </cell>");
                        response.Append("</row>");
                    }
                    response.Append("</rows>");
                }

                return response.ToString();
            }
            catch
            {
                return string.Empty;
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
