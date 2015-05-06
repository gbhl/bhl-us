using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class WebStatsUser : System.Web.UI.Page
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

        private void fillStatList(int applicationID)
        {
            DateTime startDate = DateTime.Parse(startDateTextBox.Text.Trim());
            DateTime endDate = DateTime.Parse(endDateTextBox.Text.Trim());

            BHL.Web.Utilities.RequestLog requestLog = new BHL.Web.Utilities.RequestLog();

            _total = requestLog.SelectDateRangeTotal(applicationID, startDate, endDate);

            StringBuilder sb = new StringBuilder();
            sb.Append("<b>");
            sb.Append(_total.ToString());
            sb.Append("</b> total requests between ");
            sb.Append(startDate.ToShortDateString());
            sb.Append(" and ");
            sb.Append(endDate.ToShortDateString());

            totalStatsLabel.Text = sb.ToString();

            List<RequestLogStat> gsList = requestLog.SelectUserTotal(applicationID, startDate, endDate);

            // Get the CustomGenericList of APIKeys and convert it to a "regular" List
            CustomGenericList<APIKey> apiKeysCustom = new CustomGenericList<APIKey>();
            apiKeysCustom = new BHLProvider().ApiKeySelectAll();
            List<APIKey> apiKeys = new List<APIKey>();
            foreach (APIKey key in apiKeysCustom) apiKeys.Add(key);

            // Use LINQ to Entities to combine the detailed stats with the API Keys.  This will ensure that we
            // display the correct user name.
            var query = from detail in gsList
                        join apiKey in apiKeys
                        on detail.IntColumn02 equals apiKey.ApiKeyID
                        into listGroup
                        from apiKey in listGroup.DefaultIfEmpty()
                        select new RequestLogStat
                        {
                            IntColumn01 = detail.IntColumn01,
                            IntColumn02 = detail.IntColumn02,
                            StringColumn01 = (apiKey == null) ? string.Empty : apiKey.ContactName + "(" + (apiKey.EmailAddress ?? string.Empty) + ")"
                        };

            statsList.DataSource = query;
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

        protected void gsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            RequestLogStat requestLogStat = (RequestLogStat)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label userName = (Label)e.Row.FindControl("UserName");
                userName.Text = requestLogStat.StringColumn01;

                HyperLink total = (HyperLink)e.Row.FindControl("Total");
                total.Text = requestLogStat.IntColumn01.ToString();

                string detailUrl = "WebStatsDetail.aspx?id=" + ViewState["appID"].ToString() + "&mid=" + ViewState["menuID"].ToString() + 
                    "&startdate=" + Server.UrlEncode(startDateTextBox.Text) + "&enddate=" + Server.UrlEncode(endDateTextBox.Text);

                if (requestLogStat.IntColumn02 != null)
                    detailUrl += "&userId=" + requestLogStat.IntColumn02;

                total.NavigateUrl = detailUrl;
            }
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                fillStatList(Convert.ToInt32(ViewState["appID"].ToString()));
            }
        }
    }
}