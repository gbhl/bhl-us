using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
    public partial class OpenUrlResultStats : System.Web.UI.Page
    {
        private int _total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string appIDString = Request.QueryString["id"] ?? string.Empty;
                int appID = 0;
                string menuID = Request.QueryString["mid"] ?? string.Empty;

                startDateTextBox.Text = DateTime.Today.Date.ToShortDateString();
                endDateTextBox.Text = DateTime.Today.Date.AddDays(1).ToShortDateString();
                if (Int32.TryParse(appIDString, out appID))
                {
                    ViewState["appID"] = appID.ToString();
                    ViewState["menuID"] = menuID;
                    webStatsNavControl.ApplicationID = appID;
                    webStatsNavControl.Menu = menuID;
                    fillStatList(appID);
                }
            }

            errorControl.Visible = false;
        }

        protected string GetBar(object dataItem)
        {
            string totalString = DataBinder.Eval(dataItem, "IntColumn01").ToString();
            int total = int.Parse(totalString);

            var sb = new StringBuilder();
            if (_total > 0)
            {
                float fwid = ((float)total / (float)_total) * 100;
                string wid = fwid.ToString() + "px";
                sb.Append("<table width=\"100px\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: 1px black solid\"><tr>");
                sb.Append("<td style=\"background-color: green;height:10px;width:");
                sb.Append(wid);
                sb.Append("\">");
                sb.Append("<img src=\"/images/blank.gif\" /></td>");
                sb.Append("<td style=\"background-color: white;height:10px;\"><img src=\"/images/blank.gif\" /></td>");
                sb.Append("</tr></table>");
            }

            return sb.ToString();
        }

        private void fillStatList(int applicationID)
        {
            DateTime startDate = DateTime.Parse(startDateTextBox.Text.Trim());
            DateTime endDate = DateTime.Parse(endDateTextBox.Text.Trim());

            RequestLog requestLog = new RequestLog();

            _total = requestLog.SelectDateRangeTotal(applicationID, startDate, endDate);

            StringBuilder sb = new StringBuilder();
            sb.Append("<b>");
            sb.Append(_total.ToString());
            sb.Append("</b> total requests between ");
            sb.Append(startDate.ToShortDateString());
            sb.Append(" and ");
            sb.Append(endDate.ToShortDateString());

            totalStatsLabel.Text = sb.ToString();

            // Get the stat details
            List<RequestLogRecord> rlList = requestLog.SelectStatDetails(applicationID, startDate, endDate, null, null, null, 4);

            // Count the number of searches returning no results, one result, or more than one result
            int numNoneFound = rlList.Count(record => record.Detail.EndsWith("|found:0"));
            int numOneFound = rlList.Count(record => record.Detail.EndsWith("|found:1"));
            int numMultipleFound = rlList.Count() - numNoneFound - numOneFound;

            // Build the list to be displayed
            List<RequestLogStat> stats = new List<RequestLogStat>();
            stats.Add(new RequestLogStat(numNoneFound, 0, "No results.", null));
            stats.Add(new RequestLogStat(numOneFound, 1, "One match found.", null));
            stats.Add(new RequestLogStat(numMultipleFound, 2, "Multiple matches found.", null));

            statsList.DataSource = stats;
            statsList.DataBind();
        }

        private bool validate()
        {
            bool flag = false;

            DateTime startDate;
            if (DateTime.TryParse(startDateTextBox.Text.Trim(), out startDate) == false)
            {
                flag = true;
                errorControl.AddErrorText("Start date is not in correct date format");
            }

            DateTime endDate;
            if (DateTime.TryParse(endDateTextBox.Text.Trim(), out endDate) == false)
            {
                flag = true;
                errorControl.AddErrorText("End date is not in correct date format");
            }

            if (flag == false && startDate.CompareTo(endDate) > 0)
            {
                flag = true;
                errorControl.AddErrorText("Start date must be less than end date");
            }

            if (flag == false && ((TimeSpan)(endDate.Subtract(startDate))).Days > 90)
            {
                flag = true;
                errorControl.AddErrorText("You are only allowed to view a maximum of 90 days");
            }

            errorControl.Visible = flag;

            return !flag;
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                fillStatList(Convert.ToInt32(ViewState["appID"].ToString()));
            }
        }

        protected void gsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            RequestLogStat requestLogStat = (RequestLogStat)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink total = (HyperLink)e.Row.FindControl("Total");
                total.Text = requestLogStat.IntColumn01.ToString();
                string detailUrl = "OpenUrlResultDetail.aspx?id=" + ViewState["appID"].ToString() + "&mid=" + ViewState["menuID"].ToString() +
                    "&startdate=" + Server.UrlEncode(startDateTextBox.Text) + "&enddate=" + Server.UrlEncode(endDateTextBox.Text) + 
                    "&resulttype=" + requestLogStat.IntColumn02.ToString();
                total.NavigateUrl = detailUrl;
            }
        }
    }
}