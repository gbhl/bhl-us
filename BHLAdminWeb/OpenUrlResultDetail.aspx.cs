using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Utility;

namespace MOBOT.BHL.AdminWeb
{
    public partial class OpenUrlResultDetail : System.Web.UI.Page
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

            int resultType = 1; // By default show searches returning exactly one result
            if (Request["resulttype"] != null) resultType = int.Parse(Request["resulttype"]);

            ViewState["OrderBy"] = _orderBy;
            ViewState["SortOrder"] = _sortOrder;

            List<RequestLogRecord> rlList = new MOBOT.BHL.Utility.RequestLog().SelectStatDetails(applicationID, startDate, endDate, null,
                null, null, (int)_orderBy * (_sortOrder == RequestLogSortOrder.Descending ? -1 : 1));

            // Use LINQ to Entities to combine the detailed stats with the API Keys.  This will ensure that we
            // display the correct user name.
            IEnumerable<RequestLogRecord> query;
            switch (resultType)
            {
                case 0:
                    {
                        query = from detail in rlList
                                    where detail.Detail.EndsWith("|found:0")
                                    select detail;
                        break;
                    }
                case 2:
                    {
                        query = from detail in rlList
                                    where (!detail.Detail.EndsWith("|found:0") && !detail.Detail.EndsWith("|found:1"))
                                    select detail;
                        break;
                    }
                case 1:
                default:
                    {
                        query = from detail in rlList
                                    where detail.Detail.EndsWith("|found:1")
                                    select detail;
                        break;
                    }
            }

            statsList.DataSource = query;
            statsList.DataBind();

            var sb = new StringBuilder();
            sb.Append("<b>");
            sb.Append(query.Count().ToString());
            sb.Append("</b> total records ");
            if (rlList.Count > 0)
            {
                sb.AppendFormat("<br/>Dates: {0} - {1}", startDate.ToString(), endDate.ToString());
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