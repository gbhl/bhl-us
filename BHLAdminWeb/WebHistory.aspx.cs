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
    public partial class WebHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("GoogleMapJS", "http://www.google.com/jsapi");

            if (!IsPostBack)
            {
                string appIDString = Request.QueryString["id"] ?? string.Empty;
                int appID = 0;
                string menuID = Request.QueryString["mid"] ?? string.Empty;

                startDateTextBox.Text = DateTime.Today.Date.AddDays(-365).ToShortDateString();
                endDateTextBox.Text = DateTime.Today.Date.AddDays(-1).ToShortDateString();
                if (Int32.TryParse(appIDString, out appID))
                {
                    fillRequestTypeCombo(appID);
                    RequestTypeList.SelectedValue = "0";

                    webStatsNavControl.ApplicationID = appID;
                    webStatsNavControl.Menu = menuID;
                    getStats(appID);
                }
                ViewState["appID"] = appID.ToString();
                ViewState["menuID"] = menuID;

            }

            errorControl.Visible = false;
        }

        protected void showButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                getStats(Convert.ToInt32(ViewState["appID"].ToString()));
            }
        }

        private void getStats(int applicationID)
        {
            DateTime startDate = DateTime.Parse(startDateTextBox.Text.Trim());
            DateTime endDate = DateTime.Parse(endDateTextBox.Text.Trim());

            int requestTypeID = 0;
            if (RequestTypeList.SelectedValue.Length > 0 && !RequestTypeList.SelectedValue.Equals("0"))
            {
                requestTypeID = (int.Parse(RequestTypeList.SelectedValue));
            }

            List<RequestLogHistoryStat> stats = new RequestLog().RequestHistorySelectByDateRangeAndRequestType(applicationID,
                startDate, endDate, requestTypeID);

            Page.ClientScript.RegisterStartupScript(typeof(WebHistory), "WebHistoryJS", createInitJS(stats));
        }

        private void fillRequestTypeCombo(int applicationID)
        {
            RequestLog requestLog = new RequestLog();

            List<RequestType> requestTypes = requestLog.RequestTypeSelectByApplication(applicationID);

            var empty = new RequestType();
            empty.RequestTypeID = 0;
            empty.RequestTypeName = "All";
            requestTypes.Insert(0, empty);

            RequestTypeList.DataSource = requestTypes;
            RequestTypeList.DataTextField = "RequestTypeName";
            RequestTypeList.DataValueField = "RequestTypeID";
            RequestTypeList.DataBind();
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

            //if (flag == false && ((TimeSpan)(endDate.Subtract(startDate))).Days > 90)
            //{
            //    flag = true;
            //    errorControl.AddErrorText("You are only allowed to view a maximum of 90 days");
            //}

            errorControl.Visible = flag;

            return !flag;
        }

        private string createInitJS(List<RequestLogHistoryStat> gsList)
        {
            /*
                    <script type='text/javascript'>
                        google.load('visualization', '1', {'packages':['annotatedtimeline']});
                        google.setOnLoadCallback(drawChart);
                        function drawChart() {
                        var data = new google.visualization.DataTable();
                        data.addColumn('date', 'Date');
                        data.addColumn('number', 'Requests');
                            data.addRows([
                            [new Date(2009, 9, 22), 60761],  
                            [new Date(2009, 9, 23), 40560],  
                            [new Date(2009, 9, 24), 22147],  
                            [new Date(2010, 9, 20), 80482]
                        ]);

                        var chart = new google.visualization.AnnotatedTimeLine(document.getElementById('chart_div'));
                        chart.draw(data, {displayAnnotations: true});
                        }
                    </script>
           */

            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">\n");
            sb.Append("    google.load(\"visualization\", \"1\", {\"packages\":[\"annotatedtimeline\"]});\n");
            sb.Append("    google.setOnLoadCallback(drawChart);\n");
            sb.Append("    function drawChart() {\n");
            sb.Append("    var data = new google.visualization.DataTable();\n");
            sb.Append("    data.addColumn(\"date\", \"Date\");\n");
            sb.Append("    data.addColumn(\"number\", \"Requests\");\n");
            sb.Append("         data.addRows([\n");

            int currentRow = 1;
            int totalRows = gsList.Count;

            foreach (RequestLogHistoryStat h in gsList)
            {
                //sb.Append("         [new Date(2009, 9, 22), 60761],\n");
                //sb.Append("         [new Date(2009, 9, 22), 60761]\n");

                sb.Append("         [new Date(" + h.Year.ToString() + ", " +
                                                (h.Month - 1).ToString() + ", " +
                                                h.Day.ToString() + "), " +
                                                h.NumRequests.ToString() + "]");
                if (currentRow == totalRows)
                {
                    sb.Append("\n");    // don't put comma on last row
                }
                else
                {
                    sb.Append(",\n");
                    currentRow++;
                }
            }

            sb.Append("     ]);\n");
            sb.Append("    var chart = new google.visualization.AnnotatedTimeLine(document.getElementById(\"chart_div\"));\n");
            sb.Append("    chart.draw(data, {displayAnnotations: true, wmode: 'opaque'});\n");
            sb.Append("    }\n");
            sb.Append("</script>\n");

            return sb.ToString();
        }
    }
}