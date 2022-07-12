using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOIList : System.Web.UI.Page
    {
        protected string userId = "0";
        protected string statusId = "0";
        protected string typeId = "0";
        protected string startDate = "";
        protected string endDate = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // The jQuery UI theme that will be used by the grid
            HtmlLink cssLnk = new HtmlLink();
            cssLnk.Attributes.Add("rel", "stylesheet");
            cssLnk.Attributes.Add("type", "text/css");
            cssLnk.Href = ConfigurationManager.AppSettings["jQueryUICSSPath"];
            Page.Header.Controls.Add(cssLnk);

            // The jQuery UI theme extension jqGrid needs
            HtmlLink cssLnk2 = new HtmlLink();
            cssLnk2.Attributes.Add("rel", "stylesheet");
            cssLnk2.Attributes.Add("type", "text/css");
            cssLnk2.Href = ConfigurationManager.AppSettings["jqGridCSSPath"];
            Page.Header.Controls.Add(cssLnk2);

            // jQuery runtime
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            // The localization file we need (English)
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jqGridLocalePath"]);
            // jqGrid runtime
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jqGridPath"]);
            // jQuery UI library
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

            if (!IsPostBack)
            {
                txtStartDate.Text = "1/1/1980";
                txtEndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();

                Dictionary<int, string> users = new BHLProvider().AspNetUserSelectWithDoi();
                ddlQueuedBy.Items.Add(new ListItem("(Anyone)", "0"));
                foreach(KeyValuePair<int, string> user in users)
                {
                    ddlQueuedBy.Items.Add(new ListItem(user.Value, user.Key.ToString()));
                }

                List<DOIStatus> statuses = new BHLProvider().DOIStatusSelectAll();
                foreach (DOIStatus status in statuses)
                {
                    if (status.DOIStatusID.ToString() != DOISTATUS_EXTERNAL)
                    {
                        ListItem li = new ListItem(status.DOIStatusName, status.DOIStatusID.ToString());
                        ddlStatusView.Items.Add(li);
                    }
                }

                List<DOIEntityType> types = new BHLProvider().DOIEntityTypeSelectWithDoi();
                ddlEntityType.Items.Add(new ListItem("(All Types)", "0"));
                foreach(DOIEntityType type in types)
                {
                    ddlEntityType.Items.Add(new ListItem(type.DOIEntityTypeName, type.DOIEntityTypeID.ToString()));
                }
            }

            litUpdateResult.Text = string.Empty;
        }

        /// <summary>
        /// Display the DOIs that are in the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            // Modify ddlStatusChange list to contain items that are valid for the displayed status
            SetStatusChangeItems(ddlStatusView.SelectedValue, ddlStatusView.Items);

            userId = ddlQueuedBy.SelectedValue;
            statusId = ddlStatusView.SelectedValue;
            typeId = ddlEntityType.SelectedValue;
            startDate = txtStartDate.Text;
            endDate = txtEndDate.Text;

            litDisplayed.Text = (ddlStatusView.SelectedValue == DOISTATUS_NONESELECTED) ? string.Empty : "DOIs in <b>" + ddlStatusView.SelectedItem.Text + "</b> status.";
            lnkDownloadResults.Visible = true;
            lnkDownloadResults.HRef = String.Format(
                "/services/doiservice.ashx?uid={0}&sid={1}&tid={2}&sdate={3}&edate={4}&dl=1",
                userId, statusId, typeId, startDate, endDate);

            litUpdateResult.Text = string.Empty;
        }

        /// <summary>
        /// Update the selected DOIs and set them to the specified status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChange_Click(object sender, EventArgs e)
        {
            int? userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
            string updateMsg = string.Empty;
            litUpdateResult.Text = string.Empty;

            BHLProvider service = new BHLProvider();

            try
            {
                // Get the list of selected DOIs
                string selected = hidSelected.Value;
                string[] ids = selected.Split('|');

                if (string.IsNullOrEmpty(selected))
                {
                    updateMsg = "No DOIs selected";
                }
                else
                {
                    for (int x = 0; x < ids.Length; x++)
                    {
                        if (ids[x] != "cb_list")    // ignore checkbox "cb_list"
                        {
                            // Update the DOI
                            string id = ids[x].Replace("jqg_list_", "");
                            service.DOIUpdateStatus(Convert.ToInt32(id), Convert.ToInt32(ddlStatusChange.SelectedValue), userId);
                        }
                    }
                }
                if (updateMsg.Length == 0) updateMsg = "Update successful";
            }
            catch (Exception ex)
            {
                // do nothing
                updateMsg += "<font color='red'>Error: " + ex.Message + "</font>";
            }

            // Refresh the grid
            litUpdateResult.Text = "<font color='red'>" + updateMsg + "</font>";
            statusId = ddlStatusView.SelectedValue;
        }

        private const string DOISTATUS_NONESELECTED = "0";
        private const string DOISTATUS_QUEUED = "30";
        private const string DOISTATUS_REJECTED = "40";
        private const string DOISTATUS_SUBMITTED = "50";
        private const string DOISTATUS_ERROR = "80";
        private const string DOISTATUS_DOIAPPROVED = "100";
        private const string DOISTATUS_EXTERNAL = "200";

        /// <summary>
        /// Add the appropriate items to the StatusChange dropdown, based on the status of the currently displayed items
        /// </summary>
        /// <param name="selectedStatus"></param>
        /// <param name="statuses"></param>
        private void SetStatusChangeItems(string selectedStatus, ListItemCollection statuses)
        {
            ddlStatusChange.Items.Clear();
            ddlStatusChange.Enabled = true;
            btnChange.Enabled = true;

            switch (selectedStatus)
            {
                case DOISTATUS_NONESELECTED:
                case DOISTATUS_QUEUED:
                case DOISTATUS_EXTERNAL:
                    ddlStatusChange.Enabled = false;
                    btnChange.Enabled = false;
                    break;
                case DOISTATUS_REJECTED:
                case DOISTATUS_SUBMITTED:
                case DOISTATUS_ERROR:
                case DOISTATUS_DOIAPPROVED:
                default:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(DOISTATUS_QUEUED).Text, DOISTATUS_QUEUED));
                    break;
            }
        }
    }
}