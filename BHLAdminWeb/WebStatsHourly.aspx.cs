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
    public partial class WebStatsHourly : System.Web.UI.Page
    {
        private int _total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string appIDString = Request.QueryString["id"] ?? string.Empty;
                int appID = 0;
                string menuID = Request.QueryString["mid"] ?? string.Empty;

                dateTextBox.Text = DateTime.Today.ToShortDateString();
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
            string totalString = DataBinder.Eval(dataItem, "IntColumn02").ToString();
            int total = int.Parse(totalString);

            StringBuilder sb = new StringBuilder();
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

        protected string GetHour(object dataItem)
        {
            string hourString = DataBinder.Eval(dataItem, "IntColumn01").ToString();

            int hour = int.Parse(hourString);
            if (hour == 0)
            {
                return "12:00 am";
            }
            else if (hour < 12)
            {
                return hourString + ":00 am";
            }
            else if (hour == 12)
            {
                return "12:00 pm";
            }
            else
            {
                return (hour - 12).ToString() + ":00 pm";
            }
        }

        private void fillStatList(int applicationID)
        {
            DateTime date = DateTime.Parse(dateTextBox.Text.Trim());

            RequestLog requestLog = new RequestLog();

            _total = requestLog.SelectDateRangeTotal(applicationID, date, date.AddDays(1));

            StringBuilder sb = new StringBuilder();
            sb.Append("<b>");
            sb.Append(_total.ToString());
            sb.Append("</b> total requests for ");
            sb.Append(date.ToShortDateString());

            totalStatsLabel.Text = sb.ToString();

            List<RequestLogStat> rlList = requestLog.SelectHourRangeTotal(applicationID, date.AddDays(1));
            statsList.DataSource = rlList;
            statsList.DataBind();
        }

        private bool validate()
        {
            bool flag = false;

            DateTime date;
            if (DateTime.TryParse(dateTextBox.Text.Trim(), out date) == false)
            {
                flag = true;
                errorControl.AddErrorText("Date is not in correct date format");
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
                total.Text = requestLogStat.IntColumn02.ToString();
                DateTime startDate = DateTime.Parse(dateTextBox.Text + " " + requestLogStat.IntColumn01 + ":00");
                DateTime endDate = startDate.AddHours(1);
                string detailUrl = "WebStatsDetail.aspx?id=" + ViewState["appID"].ToString() + "&mid=" + ViewState["menuID"].ToString() + 
                    "&startdate=" + Server.UrlEncode(startDate.ToString()) + "&enddate=" + Server.UrlEncode(endDate.ToString());
                total.NavigateUrl = detailUrl;
            }
        }
    }
}