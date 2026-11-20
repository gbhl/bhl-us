using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Handler for requests coming from the /DataHarvestItemList.aspx page, in particular from the jqGrid object
    /// contained on that page.
    /// </summary>
    public class DataHarvestService : IHttpHandler
    {
        private const int PageSize = 5000;

        private static readonly string[] IADownloadColumns =
        {
            "IAIdentifier", "IAStatus", "BHLStatus", "Title", "Volume", "Year", "ImageCount",
            "HoldingInstitution", "Sponsor", "ScanningInstitution", "RightsHolder", "LicenseUrl",
            "Rights", "DueDiligence", "CopyrightStatus", "VirtualTitleID", "VirtualVolume",
            "IACreated", "IAScanned", "IALastModified", "BHLCreated", "BHLLastDownload",
            "BHLProduction", "BHLLastModified", "CreatedUser", "LastModifiedUser"
        };

        public void ProcessRequest(HttpContext context)
        {
            // Clean up inputs
            string itemStatusId = context.Request.QueryString["id"];
            string iaId = context.Request.QueryString["iaid"];
            string download = context.Request.QueryString["dl"];
            string type = context.Request.QueryString["type"];
            string numRows = context.Request.QueryString["rows"];
            string pageNum = context.Request.QueryString["page"];
            string sortColumn = context.Request.QueryString["sidx"];
            string sortOrder = context.Request.QueryString["sord"];

            int verifyInt;
            // Make sure itemStatusId, numRows, and pageNum are valid integer values
            itemStatusId = string.IsNullOrEmpty(itemStatusId) ? "0" : (!Int32.TryParse(itemStatusId, out verifyInt) ? "0" : itemStatusId);
            numRows = string.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = string.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "IAIdentifier" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            string xmlResponse;
            switch (type)
            {
                case "bsstatus":
                    // Make sure sortColumn is a value column name
                    sortColumn = string.IsNullOrEmpty(sortColumn) ? "CreationDate" : sortColumn;

                    List<BioStorHarvestItem> biostorSearchResult = this.BioStorHarvestItemSelectByStatus(
                        Convert.ToInt32(itemStatusId), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

                    xmlResponse = GetBioStorHarvestItemXmlResponse(biostorSearchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows));

                    context.Response.ContentType = "text/xml";
                    context.Response.Write(xmlResponse);

                    break;
                case "iastatus":
                default:
                    if (download == "1")
                    {
                        DoIAHarvestDownload(context, Convert.ToInt32(itemStatusId), iaId);
                    }
                    else
                    {
                        // Make sure sortColumn is a value column name
                        sortColumn = String.IsNullOrEmpty(sortColumn) ? "IAIdentifier" : sortColumn;

                        List<IAHarvestItem> iaSearchResult = this.IAHarvestItemSelectByStatus(
                            Convert.ToInt32(itemStatusId), iaId, Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

                        xmlResponse = GetIAHarvestItemXmlResponse(iaSearchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows));

                        context.Response.ContentType = "text/xml";
                        context.Response.Write(xmlResponse);
                    }
                    break;
            }
        }

        private void DoIAHarvestDownload(HttpContext context, int statusId, string iaId)
        {
            HttpResponse response = context.Response;

            response.Clear();
            response.ClearHeaders();
            response.BufferOutput = false;
            response.ContentType = "text/csv";
            response.AddHeader("Cache-Control", "no-cache, no-store");

            try
            {
                using (CSVStreamWriter csv = new CSV().CreateStreamWriter(response.OutputStream))
                {
                    // Header row goes out immediately, before the first database call
                    csv.WriteHeader(IADownloadColumns);
                    csv.Flush();
                    response.Flush();

                    BHLImportProvider provider = new BHLImportProvider();
                    int page = 1;

                    while (response.IsClientConnected)
                    {
                        List<IAItem> items = provider.IAItemSelectByStatus(statusId, iaId, PageSize, page, "IAIdentifier", "asc");

                        if (items == null || items.Count == 0) break;

                        foreach (IAItem item in items) csv.WriteRow(GetIADownloadRow(item));

                        csv.Flush();
                        response.Flush();   // push this page downstream

                        if (items.Count < PageSize) break;
                        page++;
                    }
                }
            }
            catch (Exception ex)
            {
                // Headers already sent; can't change the status code.
                // Logger.Error(ex);
                if (response.IsClientConnected)
                {
                    byte[] err = Encoding.UTF8.GetBytes("\r\n#ERROR: export failed or was truncated\r\n");
                    response.OutputStream.Write(err, 0, err.Length);
                }
            }
            finally
            {
                try { if (response.IsClientConnected) response.Flush(); } catch (HttpException) { }
                context.ApplicationInstance.CompleteRequest();
            }
        }

        // Same order as IADownloadColumns. Dates keep the original's .ToString(); other values are passed through as-is
        // and formatted by the CSV class (InvariantCulture by default), as before.
        private static object[] GetIADownloadRow(IAItem item)
        {
            return new object[]
            {
                item.IAIdentifier, item.ExternalStatus, item.Status, item.ShortTitle, item.Volume,
                item.Year, item.ImageCount, item.HoldingInstitution, item.Sponsor,
                item.ScanningInstitution, item.RightsHolder, item.LicenseUrl, item.Rights,
                item.DueDiligence, item.PossibleCopyrightStatus, item.VirtualTitleID, item.VirtualVolume,
                item.IAAddedDate.ToString(), item.ScanDate, item.IADateStamp.ToString(),
                item.CreatedDate.ToString(), item.LastXMLDataHarvestDate.ToString(),
                item.LastProductionDate.ToString(), item.LastModifiedDate.ToString(),
                item.CreatedUser, item.LastModifiedUser
            };
        }

        /// <summary>
        /// Call the BHLImport web service to get the list of IA items that match the specified criteria.
        /// </summary>
        /// <param name="itemStatusId"></param>
        /// <param name="iaId"></param>
        /// <param name="numRows"></param>
        /// <param name="pageNum"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private List<IAHarvestItem> IAHarvestItemSelectByStatus(
            int itemStatusId, string iaId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            List<IAHarvestItem> items = new List<IAHarvestItem>();
            BHLImportProvider service = null;

            try
            {
                service = new BHLImportProvider();
                List<IAItem> iaItems = service.IAItemSelectByStatus(itemStatusId, iaId, numRows, pageNum, sortColumn, sortOrder);

                foreach (IAItem iaItem in iaItems)
                {
                    IAHarvestItem item = new IAHarvestItem();
                    item.ItemId = iaItem.ItemID;
                    item.IAIdentifier = iaItem.IAIdentifier;
                    item.ExternalStatus = iaItem.ExternalStatus;
                    item.Status = iaItem.Status;
                    item.HoldingInstitution = iaItem.HoldingInstitution;
                    item.IAAddedDate= iaItem.IAAddedDate;
                    item.ScanDate = iaItem.ScanDate;
                    item.IADateStamp = iaItem.IADateStamp;
                    item.LastXMLDataHarvestDate = iaItem.LastXMLDataHarvestDate;
                    item.LastProductionDate = iaItem.LastProductionDate;
                    item.CreatedDate = iaItem.CreatedDate;
                    item.LastModifiedDate = iaItem.LastModifiedDate;
                    item.CreatedUser = iaItem.CreatedUser;
                    item.LastModifiedUser = iaItem.LastModifiedUser;
                    item.TotalItems = iaItem.TotalItems;
                    items.Add(item);
                }
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetIAHarvestItemXmlResponse(List<IAHarvestItem> searchResult, int pageNum, int numRows)
        {
            StringBuilder response = new StringBuilder(); ;

            if (searchResult.Count > 0)
            {
                int totalPages = (int)Math.Ceiling((double)searchResult[0].TotalItems / (double)numRows);
                int numRecords = searchResult[0].TotalItems;

                // <page> = page number
                // <total> = total pages
                // <records> = number of records
                response.Append("<?xml version =\"1.0\" encoding=\"utf-8\"?>");
                response.Append("<rows>");
                response.Append("<page>" + pageNum + "</page>");
                response.Append("<total>" + totalPages.ToString() + "</total>");
                response.Append("<records>" + numRecords.ToString() + "</records>");

                for (int x = 0; x < searchResult.Count; x++)
                {
                    response.Append("<row id='" + searchResult[x].ItemId.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" rel=\"noopener noreferrer\" target=\"_blank\" href=\"http://www.archive.org/details/" + searchResult[x].IAIdentifier + "\">" + searchResult[x].IAIdentifier + "</a>]]> </cell>");
                    //response.Append(
                    //    "<cell> <![CDATA[" + searchResult[x].IAIdentifier  + 
                    //    " <a title=\"Info\" rel=\"noopener noreferrer\" target=\"_blank\" href=\"http://www.archive.org/details/" + searchResult[x].IAIdentifier + "\">IA</a> " + 
                    //    "]]> </cell>"
                    //    );
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].ExternalStatus) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Status) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].HoldingInstitution) + " </cell>");
                    response.Append("<cell> " + (searchResult[x].IAAddedDate == null ? string.Empty : SecurityElement.Escape(((DateTime)searchResult[x].IAAddedDate).ToString("yyyy/MM/dd HH:mm:ss"))) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].ScanDate) + " </cell>");
                    response.Append("<cell> " + (searchResult[x].IADateStamp == null ? string.Empty : SecurityElement.Escape(((DateTime)searchResult[x].IADateStamp).ToString("yyyy/MM/dd HH:mm:ss"))) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(((DateTime)searchResult[x].CreatedDate).ToString("yyyy/MM/dd HH:mm:ss")) + " </cell>");
                    response.Append("<cell> " + (searchResult[x].LastXMLDataHarvestDate == null ? string.Empty : SecurityElement.Escape(((DateTime)searchResult[x].LastXMLDataHarvestDate).ToString("yyyy/MM/dd HH:mm:ss"))) + " </cell>");
                    response.Append("<cell> " + (searchResult[x].LastProductionDate == null ? string.Empty : SecurityElement.Escape(((DateTime)searchResult[x].LastProductionDate).ToString("yyyy/MM/dd HH:mm:ss"))) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(((DateTime)searchResult[x].LastModifiedDate).ToString("yyyy/MM/dd HH:mm:ss")) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].CreatedUser) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].LastModifiedUser) + " </cell>");
                    response.Append("</row>");
                }
                response.Append("</rows>");
            }

            return response.ToString();
        }

        private List<BioStorHarvestItem> BioStorHarvestItemSelectByStatus(
            int itemStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            List<BioStorHarvestItem> items = new List<BioStorHarvestItem>();
            BHLImportProvider service = null;

            try
            {
                service = new BHLImportProvider();
                List<BSItem> bsItems = service.BSItemSelectByStatus(itemStatusId, numRows, pageNum, sortColumn, sortOrder);

                foreach (BSItem bsItem in bsItems)
                {
                    BioStorHarvestItem item = new BioStorHarvestItem();
                    item.ItemId = bsItem.ItemID;
                    item.BHLItemId = bsItem.BHLItemID;
                    item.Title = bsItem.Title;
                    item.Volume = bsItem.Volume;
                    item.TotalSegments = bsItem.TotalSegments;
                    item.PublishedSegments = bsItem.PublishedSegments;
                    item.SkippedSegments = bsItem.SkippedSegments;
                    item.CreationDate = bsItem.CreationDate;
                    item.TotalItems = bsItem.TotalItems;
                    items.Add(item);
                }
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetBioStorHarvestItemXmlResponse(List<BioStorHarvestItem> searchResult, int pageNum, int numRows)
        {
            StringBuilder response = new StringBuilder(); ;

            if (searchResult.Count > 0)
            {
                int totalPages = (int)Math.Ceiling((double)searchResult[0].TotalItems / (double)numRows);
                int numRecords = searchResult[0].TotalItems;

                // <page> = page number
                // <total> = total pages
                // <records> = number of records
                response.Append("<?xml version =\"1.0\" encoding=\"utf-8\"?>");
                response.Append("<rows>");
                response.Append("<page>" + pageNum + "</page>");
                response.Append("<total>" + totalPages.ToString() + "</total>");
                response.Append("<records>" + numRecords.ToString() + "</records>");

                for (int x = 0; x < searchResult.Count; x++)
                {
                    response.Append("<row id='" + searchResult[x].ItemId.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" rel=\"noopener noreferrer\" href=\"/ItemEdit.aspx?id=" + searchResult[x].BHLItemId + "\">" + searchResult[x].BHLItemId + "</a>]]> </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Title) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Volume) + " </cell>");
                    response.Append("<cell> <![CDATA[<a href=\"#\" title=\"Segments\" onclick=\"window.open('BioStorSegmentsForItem.aspx?id=" + searchResult[x].ItemId.ToString() + "', 'Segments', 'resizeable=0,scrollbars=1,height=500,width=500,status=0,toolbar=0,menubar=0,location=0');\">" + searchResult[x].TotalSegments + "</a>]]> </cell>");
                    response.Append("<cell> " + searchResult[x].PublishedSegments + " </cell>");
                    response.Append("<cell> " + searchResult[x].SkippedSegments + " </cell>");
                    response.Append("<cell> " + searchResult[x].CreationDate + " </cell>");
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