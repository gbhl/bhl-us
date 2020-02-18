using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class WebStatsDetail : System.Web.UI.Page
    {
        private RequestLogSortOrder _sortOrder = RequestLogSortOrder.Ascending;
        private RequestLogSearchOrderBy _orderBy = RequestLogSearchOrderBy.CreationDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string appIDString = Request.QueryString["id"] ?? string.Empty;
                int appID = 0;
                string menuID = Request.QueryString["mid"] ?? string.Empty;

                if (Int32.TryParse(appIDString, out appID))
                {
                    webStatsNavControl.ApplicationID = appID;
                    webStatsNavControl.Menu = menuID;
                    fillStatList(appID);
                }
                ViewState["appID"] = appID.ToString();
                ViewState["menuID"] = menuID;
            }
            else
            {
                _orderBy = (RequestLogSearchOrderBy)ViewState["OrderBy"];
                _sortOrder = (RequestLogSortOrder)ViewState["SortOrder"];
            }

            errorControl.Visible = false;
        }

        private void fillStatList(int applicationID)
        {

            DateTime startDate = new DateTime();
            if (Request["startdate"] != null)
            {
                startDate = DateTime.Parse(Server.UrlDecode(Request["startdate"]));
            }

            DateTime endDate = new DateTime();
            if (Request["enddate"] != null)
            {
                endDate = DateTime.Parse(Server.UrlDecode(Request["enddate"]));
            }

            int? requestTypeId = null;
            if (Request["requesttypeid"] != null)
                requestTypeId = int.Parse(Request["requesttypeid"]);

            string ipAddress = null;
            if (Request["ipAddress"] != null)
            {
                ipAddress = Server.UrlDecode(Request["ipAddress"]);
            }

            int? userId = null;
            if (Request["userid"] != null)
                userId = int.Parse(Request["userid"]);

            ViewState["OrderBy"] = _orderBy;
            ViewState["SortOrder"] = _sortOrder;

            List<RequestLogRecord> rlList = new BHL.Web.Utilities.RequestLog().SelectStatDetails(applicationID, startDate, endDate, requestTypeId, 
                ipAddress, userId, (int)_orderBy * (_sortOrder == RequestLogSortOrder.Descending ? -1 : 1));

            List<APIKey> apiKeys = new BHLProvider().ApiKeySelectAll();

            // Use LINQ to Entities to combine the detailed stats with the API Keys.  This will ensure that we
            // display the correct user name.
            var query = from detail in rlList
                        join apiKey in apiKeys
                        on detail.UserID equals apiKey.ApiKeyID
                        into listGroup
                        from apiKey in listGroup.DefaultIfEmpty()
                        select new RequestLogRecord
                        {
                            ApplicationID = detail.ApplicationID,
                            CreationDate = detail.CreationDate,
                            Detail = detail.Detail,
                            IPAddress = detail.IPAddress,
                            RequestLogID = detail.RequestLogID,
                            RequestTypeID = detail.RequestTypeID,
                            UserID = detail.UserID,
                            RequestTypeName = detail.RequestTypeName,
                            UserName = (apiKey == null) ? string.Empty : apiKey.ContactName + "(" + (apiKey.EmailAddress ?? string.Empty) + ")"
                        };

            statsList.DataSource = query;
            statsList.DataBind();

            var sb = new StringBuilder();
            sb.Append("<b>");
            sb.Append(query.Count().ToString());
            sb.Append("</b> total records ");
            if (query.Count() > 0)
            {
                sb.AppendFormat("<br/>Dates: {0} - {1}", startDate.ToString(), endDate.ToString());
                sb.AppendFormat("<br/>Function: {0}", (requestTypeId == null ? "All" : query.First().RequestTypeName));
                sb.Append("<br/>IP Address: ");
                if (ipAddress == null)
                    sb.Append("Unknown");
                else if (ipAddress.Length == 0)
                    sb.Append("All");
                else
                    sb.Append(ipAddress);
                sb.Append("<br/>User: ");
                if (userId == null)
                    sb.Append("Unknown");
                else if (userId == -1)
                    sb.Append("All");
                else
                    sb.AppendFormat(query.First().UserName);
            }

            totalStatsLabel.Text = sb.ToString();
        }

        protected void statsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            RequestLogRecord requestLog = (RequestLogRecord)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label userName = (Label)e.Row.FindControl("UserName");
                userName.Text = requestLog.UserName;

                Label creationDate = (Label)e.Row.FindControl("CreationDate");
                creationDate.Text = requestLog.CreationDate.ToString();

                Label requestType = (Label)e.Row.FindControl("RequestType");
                requestType.Text = requestLog.RequestTypeName;

                Label detail = (Label)e.Row.FindControl("Detail");
                detail.Text = requestLog.Detail;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                Image img = new Image();
                img.Attributes["style"] = "padding-bottom:2px";
                if (_sortOrder == RequestLogSortOrder.Ascending)
                    img.ImageUrl = "/images/up.gif";
                else
                    img.ImageUrl = "/images/down.gif";

                // Find cell that is being sorted
                int ix = 0;
                foreach (DataControlField dcf in statsList.Columns)
                {
                    if (dcf.SortExpression.ToLower().Equals(_orderBy.ToString().ToLower()))
                    {
                        break;
                    }
                    ix++;
                }

                e.Row.Cells[ix].Controls.Add(new LiteralControl(" "));
                e.Row.Cells[ix].Controls.Add(img);
            }
        }

        protected void statsList_OnSorting(object sender, GridViewSortEventArgs e)
        {
            RequestLogSearchOrderBy origOrderBy = _orderBy;

            if (e.SortExpression.Equals("CreationDate"))
                _orderBy = RequestLogSearchOrderBy.CreationDate;
            else if (e.SortExpression.Equals("IPAddress"))
                _orderBy = RequestLogSearchOrderBy.IPAddress;
            else if (e.SortExpression.Equals("UserName"))
                _orderBy = RequestLogSearchOrderBy.UserName;
            else if (e.SortExpression.Equals("RequestTypeName"))
                _orderBy = RequestLogSearchOrderBy.RequestTypeName;
            else if (e.SortExpression.Equals("Detail"))
                _orderBy = RequestLogSearchOrderBy.Detail;

            if (origOrderBy == _orderBy)
            {
                if (_sortOrder == RequestLogSortOrder.Descending)
                    _sortOrder = RequestLogSortOrder.Ascending;
                else
                    _sortOrder = RequestLogSortOrder.Descending;
            }
            else
                _sortOrder = RequestLogSortOrder.Ascending;

            fillStatList(Convert.ToInt32(ViewState["appID"].ToString()));
        }
    }
}