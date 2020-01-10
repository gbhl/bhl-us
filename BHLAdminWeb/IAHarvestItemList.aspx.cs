using MOBOT.BHL.Web.Utilities;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class IAHarvestItemList : System.Web.UI.Page
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
                BHLImportProvider service = new BHLImportProvider();
                List<IAItemStatus> statuses = service.IAItemStatusSelectAll();

                ddlStatusView.Items.Add(new ListItem("", "0"));
                foreach (IAItemStatus status in statuses)
                {
                    ListItem li = new ListItem(status.Status, status.ItemStatusID.ToString());
                    ddlStatusView.Items.Add(li);
                }
            }

            litUpdateResult.Text = string.Empty;
        }

        /// <summary>
        /// Display the items that are in the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            // Modify ddlStatusChange list to contain items that are valid for the displayed status
            SetStatusChangeItems(ddlStatusView.SelectedValue, ddlStatusView.Items);

            litDisplayed.Text = (ddlStatusView.SelectedValue == ITEMSTATUS_NONESELECTED) ? string.Empty: "Items in <b>" + ddlStatusView.SelectedItem.Text + "</b> status.";
            statusId = ddlStatusView.SelectedValue;
            litUpdateResult.Text = string.Empty;
        }

        /// <summary>
        /// Update the selected items and set them to the specified status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChange_Click(object sender, EventArgs e)
        {
            string updateMsg = string.Empty;
            litUpdateResult.Text = string.Empty;

            BHLImportProvider service = null;

            try
            {
                service = new BHLImportProvider();

                // Get the list of selected items
                string selected = hidSelected.Value;
                string[] ids = selected.Split('|');

                if (string.IsNullOrEmpty(selected))
                { 
                    updateMsg = "No items selected"; 
                }
                else
                {
                    for (int x = 0; x < ids.Length; x++)
                    {
                        if (ids[x] != "cb_list")    // ignore checkbox "cb_list"
                        {
                            string id = ids[x].Replace("jqg_list_", "");
                            // Call the web service to update the item
                            string[] wsResponse = service.IAItemUpdateStatus(
                                Convert.ToInt32(id), Convert.ToInt32(ddlStatusChange.SelectedValue));

                            // Check for any errors
                            if (wsResponse[0] != "true")
                            {
                                updateMsg += string.Format("Error updating item {0} ({1})<br/>",
                                    (string.IsNullOrEmpty(wsResponse[2]) ? id : wsResponse[2]), wsResponse[1]);
                            }
                        }
                    }
                }
                if (updateMsg.Length == 0) updateMsg = "Update successful";
            }
            catch (Exception ex)
            {
                // do nothing
                updateMsg += "<font color='red'>Error: " + ex.Message+ "</font>";
            }

            // Refresh the grid
            litUpdateResult.Text = "<font color='red'>" + updateMsg + "</font>"; 
            statusId = ddlStatusView.SelectedValue;
        }

        private const string ITEMSTATUS_NONESELECTED = "0";
        private const string ITEMSTATUS_NEW = "10";
        private const string ITEMSTATUS_PENDINGAPPROVAL = "20";
        private const string ITEMSTATUS_APPROVED = "30";
        private const string ITEMSTATUS_COMPLETE = "40";
        private const string ITEMSTATUS_MARCMISSINGNEW = "80";
        private const string ITEMSTATUS_MARCMISSINGAPPROVED = "81";
        private const string ITEMSTATUS_MARCMISSINGONHOLD = "82";
        private const string ITEMSTATUS_XMLERROR = "90";
        private const string ITEMSTATUS_INAPPROPRIATE = "97";
        private const string ITEMSTATUS_IAERROR = "98";
        private const string ITEMSTATUS_ERROR = "99";

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
                case ITEMSTATUS_NONESELECTED:
                    break;
                case ITEMSTATUS_NEW:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_INAPPROPRIATE).Text, ITEMSTATUS_INAPPROPRIATE));
                    break;
                case ITEMSTATUS_PENDINGAPPROVAL:
                case ITEMSTATUS_APPROVED:
                case ITEMSTATUS_XMLERROR:
                case ITEMSTATUS_IAERROR:
                case ITEMSTATUS_ERROR:
                default:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_NEW).Text, ITEMSTATUS_NEW));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_INAPPROPRIATE).Text, ITEMSTATUS_INAPPROPRIATE));
                    break;
                case ITEMSTATUS_INAPPROPRIATE:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_NEW).Text, ITEMSTATUS_NEW));
                    break;
                case ITEMSTATUS_COMPLETE:
                    ddlStatusChange.Enabled = false;
                    btnChange.Enabled = false;
                    break;
                case ITEMSTATUS_MARCMISSINGNEW:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_NEW).Text, ITEMSTATUS_NEW));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_MARCMISSINGAPPROVED).Text, ITEMSTATUS_MARCMISSINGAPPROVED));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_MARCMISSINGONHOLD).Text, ITEMSTATUS_MARCMISSINGONHOLD));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_INAPPROPRIATE).Text, ITEMSTATUS_INAPPROPRIATE));
                    break;
                case ITEMSTATUS_MARCMISSINGAPPROVED:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_NEW).Text, ITEMSTATUS_NEW));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_MARCMISSINGONHOLD).Text, ITEMSTATUS_MARCMISSINGONHOLD));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_INAPPROPRIATE).Text, ITEMSTATUS_INAPPROPRIATE));
                    break;
                case ITEMSTATUS_MARCMISSINGONHOLD:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_NEW).Text, ITEMSTATUS_NEW));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_MARCMISSINGAPPROVED).Text, ITEMSTATUS_MARCMISSINGAPPROVED));
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(ITEMSTATUS_INAPPROPRIATE).Text, ITEMSTATUS_INAPPROPRIATE));
                    break;
            }
        }
    }
}