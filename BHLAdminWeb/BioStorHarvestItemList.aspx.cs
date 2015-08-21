using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.AdminWeb.BHLImportWebService;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb
{
    public partial class BioStorHarvestItemList : System.Web.UI.Page
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
                BHLImportWSSoapClient ws = new BHLImportWSSoapClient();
                BSItemStatus[] statuses = ws.BSItemStatusSelectAll();

                ddlStatusView.Items.Add(new ListItem("", "0"));
                foreach (BSItemStatus status in statuses)
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

            litDisplayed.Text = (ddlStatusView.SelectedValue == BSITEMSTATUS_NONESELECTED) ? string.Empty : "Items in <b>" + ddlStatusView.SelectedItem.Text + "</b> status.";
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

            BHLImportWSSoapClient ws = null;

            try
            {
                ws = new BHLImportWSSoapClient();

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
                            string[] wsResponse = ws.BSItemUpdateStatus(
                                Convert.ToInt32(id), Convert.ToInt32(ddlStatusChange.SelectedValue));

                            // Check for any errors
                            if (wsResponse[0] != "true")
                            {
                                updateMsg += string.Format("Error updating item {0} ({1})<br/>", id, wsResponse[1]);
                            }
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

        private const string BSITEMSTATUS_NONESELECTED = "0";
        private const string BSITEMSTATUS_NEW = "10";
        private const string BSITEMSTATUS_HARVESTED = "20";
        private const string BSITEMSTATUS_PREPROCESSED = "30";
        private const string BSITEMSTATUS_PUBLISHED = "40";
        private const string BSITEMSTATUS_ITEMUNAVAILABLE = "90";
        private const string BSITEMSTATUS_HARVESTERROR = "91";
        private const string BSITEMSTATUS_PUBLISHERROR = "92";

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
                case BSITEMSTATUS_NONESELECTED:
                    break;
                case BSITEMSTATUS_NEW:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(BSITEMSTATUS_ITEMUNAVAILABLE).Text, BSITEMSTATUS_ITEMUNAVAILABLE));
                    break;
                case BSITEMSTATUS_HARVESTED:
                case BSITEMSTATUS_PREPROCESSED:
                case BSITEMSTATUS_PUBLISHED:
                    ddlStatusChange.Enabled = false;
                    btnChange.Enabled = false;
                    break;
                case BSITEMSTATUS_PUBLISHERROR:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(BSITEMSTATUS_HARVESTED).Text, BSITEMSTATUS_HARVESTED));
                    break;
                case BSITEMSTATUS_ITEMUNAVAILABLE:
                case BSITEMSTATUS_HARVESTERROR:
                default:
                    ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(BSITEMSTATUS_NEW).Text, BSITEMSTATUS_NEW));
                    break;
            }
        }
    }
}