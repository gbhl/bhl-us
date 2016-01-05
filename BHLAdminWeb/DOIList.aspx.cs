using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.Web.Utilities;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class DOIList : System.Web.UI.Page
    {
        protected string statusId = "0";

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
                CustomGenericList<DOIStatus> statuses = new BHLProvider().DOIStatusSelectAll();

                ddlStatusView.Items.Add(new ListItem("", "0"));
                foreach (DOIStatus status in statuses)
                {
                    ListItem li = new ListItem(status.DOIStatusName, status.DOIStatusID.ToString());
                    ddlStatusView.Items.Add(li);
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

            litDisplayed.Text = (ddlStatusView.SelectedValue == DOISTATUS_NONESELECTED) ? string.Empty : "DOIs in <b>" + ddlStatusView.SelectedItem.Text + "</b> status.";
            statusId = ddlStatusView.SelectedValue;
            litUpdateResult.Text = string.Empty;
        }

        /// <summary>
        /// Update the selected DOIs and set them to the specified status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChange_Click(object sender, EventArgs e)
        {
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
                            service.DOIUpdateStatus(Convert.ToInt32(id), Convert.ToInt32(ddlStatusChange.SelectedValue));
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
        private const string DOISTATUS_NODOI = "10";
        private const string DOISTATUS_DOIASSIGNED = "20";
        private const string DOISTATUS_PENDINGRESUBMIT = "30";
        private const string DOISTATUS_BATCHIDASSSIGNED = "40";
        private const string DOISTATUS_SUBMITTED = "50";
        private const string DOISTATUS_SUBMITERROR = "60";
        private const string DOISTATUS_GETSUBMITLOGERROR = "70";
        private const string DOISTATUS_CROSSREFERROR = "80";
        private const string DOISTATUS_CROSSREFWARNING = "90";
        private const string DOISTATUS_DOIAPPROVED = "100";

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
                case DOISTATUS_NODOI:
                case DOISTATUS_DOIASSIGNED:
                case DOISTATUS_PENDINGRESUBMIT:
                case DOISTATUS_BATCHIDASSSIGNED:
                    ddlStatusChange.Enabled = false;
                    btnChange.Enabled = false;
                    break;
                case DOISTATUS_SUBMITTED:
                case DOISTATUS_SUBMITERROR:
                case DOISTATUS_GETSUBMITLOGERROR:
                case DOISTATUS_CROSSREFERROR:
                case DOISTATUS_CROSSREFWARNING:
                case DOISTATUS_DOIAPPROVED:
                default:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(DOISTATUS_PENDINGRESUBMIT).Text, DOISTATUS_PENDINGRESUBMIT));
                    break;
            }
        }
    }
}