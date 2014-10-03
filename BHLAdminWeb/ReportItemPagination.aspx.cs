using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;
using MOBOT.Security.Client.MOBOTSecurity;

namespace MOBOT.BHL.AdminWeb
{
	public partial class ReportItemPagination : System.Web.UI.Page
	{
        protected string statusId = "0";
        protected DateTime startDate;
        protected DateTime endDate;

		protected void Page_Load( object sender, EventArgs e )
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
                txtEndDate.Text = DateTime.Now.ToShortDateString();
                ddlStatusView.Items.Add(new ListItem("New", "5"));
                ddlStatusView.Items.Add(new ListItem("Incomplete", "10"));
                ddlStatusView.Items.Add(new ListItem("In Progress", "20"));
                ddlStatusView.Items.Add(new ListItem("Complete", "30"));
                ddlStatusView.SelectedIndex = 2;
                spanStatusChange.Visible = false;
            }

            this.SetMessage(string.Empty);
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

            if (ValidateData())
            {
                litDisplayed.Text = (ddlStatusView.SelectedValue == PAGINATIONSTATUS_NONESELECTED) ? string.Empty : "Items in <b>" + ddlStatusView.SelectedItem.Text + "</b> status with a Status Date between <b>" + txtStartDate.Text + "</b> and <b>" + txtEndDate.Text + "</b>.";
                statusId = ddlStatusView.SelectedValue;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);
                hidStatus.Value = statusId;
                hidStartDate.Value = startDate.ToShortDateString();
                hidEndDate.Value = endDate.ToShortDateString();
                lnkDownloadResults.HRef = "Services/ItemPaginationDownloadService.ashx?psid=" + Server.UrlEncode(statusId) +
                    "&sdate=" + Server.UrlEncode(hidStartDate.Value) + "&edate=" + Server.UrlEncode(hidEndDate.Value);
                lnkDownloadResults.Visible = true;
                this.SetMessage(string.Empty);
            }
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

            SecUser secUser = this.getSecUser();
            int userId = secUser.UserID;

            try
            {
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
                            string id = ids[x].Replace("jqg_", "");
                            // Call the web service to update the item
                            new BHLProvider().ItemUpdatePaginationStatus(Convert.ToInt32(id),
                                Convert.ToInt32(ddlStatusChange.SelectedValue), userId);
                        }
                    }
                }
                if (updateMsg.Length == 0) updateMsg = "Update successful";
            }
            catch (Exception ex)
            {
                // do nothing
                updateMsg += "Error: " + ex.Message;
            }

            // Refresh the grid
            this.SetMessage(updateMsg);
            statusId = ddlStatusView.SelectedValue;
        }

        private const string PAGINATIONSTATUS_NONESELECTED = "0";
        private const string PAGINATIONSTATUS_NEW = "5";
        private const string PAGINATIONSTATUS_INCOMPLETE = "10";
        private const string PAGINATIONSTATUS_INPROGRESS = "20";
        private const string PAGINATIONSTATUS_COMPLETE = "30";

        /// <summary>
        /// Add the appropriate items to the StatusChange dropdown, based on the status of the currently displayed items
        /// </summary>
        /// <param name="selectedStatus"></param>
        /// <param name="statuses"></param>
        private void SetStatusChangeItems(string selectedStatus, ListItemCollection statuses)
        {
            ddlStatusChange.Items.Clear();
            spanStatusChange.Visible = false;

            switch (selectedStatus)
            {
                case PAGINATIONSTATUS_NONESELECTED:
                case PAGINATIONSTATUS_NEW:
                case PAGINATIONSTATUS_INCOMPLETE:
                case PAGINATIONSTATUS_COMPLETE:
                default:
                    break;
                case PAGINATIONSTATUS_INPROGRESS:
                    if (Helper.IsUserAuthorized(new HttpRequestWrapper(Request), Helper.SecurityFunction.BHLAdminUserAdvanced)) // Only members of the BHL.Admin.SuperUser role can unlock items
                    {
                        spanStatusChange.Visible = true;
                        ddlStatusChange.Items.Add(new ListItem(statuses.FindByValue(PAGINATIONSTATUS_INCOMPLETE).Text, PAGINATIONSTATUS_INCOMPLETE));
                    }
                    break;
            }
        }

        private bool ValidateData()
        {
            bool isValid = true;

            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            if (string.IsNullOrEmpty(startDate)) { startDate = "1/1/1980"; txtStartDate.Text = startDate; }
            if (string.IsNullOrEmpty(endDate)) { endDate = DateTime.Now.ToShortDateString(); txtEndDate.Text = endDate; }

            isValid = !(ddlStatusView.SelectedValue == PAGINATIONSTATUS_NONESELECTED);
            if (!isValid)
            {
                this.SetMessage("Select a status to display.");
            }
            else
            {
                DateTime verifyDate;
                isValid = DateTime.TryParse(startDate, out verifyDate);
                if (isValid) isValid = DateTime.TryParse(endDate, out verifyDate);
                if (!isValid) this.SetMessage("Enter valid Start and End dates formatted as MM/DD/YYYY.");
            }

            return isValid;
        }

        private void SetMessage(string message)
        {
            litUpdateResult.Text = "<font color='red'>" + message + "</font>";
        }

        private SecUser getSecUser()
        {
            HttpCookie tokenCookie = Request.Cookies["MOBOTSecurityToken"];
            return Helper.GetSecProvider().SecUserSelect(tokenCookie.Value);
        }
    }
}
