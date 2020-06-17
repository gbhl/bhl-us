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
    public partial class ReportItemPagination : System.Web.UI.Page
	{
        protected string publishedOnly = "1";
        protected string institutionCode = string.Empty;
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
                txtEndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
                spanStatusChange.Visible = false;

                BHLProvider bp = new BHLProvider();
                List<Institution> institutions = bp.InstituationSelectAll();
                Institution allInstitutions = new Institution();
                allInstitutions.InstitutionName = "(Any content provider)";
                allInstitutions.InstitutionCode = "";
                institutions.Insert(0, allInstitutions);
                listInstitutions.DataSource = institutions;
                listInstitutions.DataBind();
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
            // Modify ddlStatusChange list to contain items that are valid for the displayed statuses
            SetStatusChangeItems();

            if (ValidateData())
            {
                List<string> statuses = new List<string>();
                if (chkPStatNew.Checked) statuses.Add("New");
                if (chkPStatIncomplete.Checked) statuses.Add("Incomplete");
                if (chkPStatInProgress.Checked) statuses.Add("In Progress");
                if (chkPStatComplete.Checked) statuses.Add("Complete");

                litDisplayed.Text = (statuses.Count > 0) ?
                    string.Format("<b>{0}</b> Items Held By <b>{1}</b> with Pagination Status <b>{2}</b> and Pagination Status Date between <b>{3}</b> and <b>{4}</b>.",
                        chkPublished.Checked ? "Published" : "All", 
                        listInstitutions.SelectedValue == "" ? "All Content Providers" : listInstitutions.SelectedItem.Text, 
                        string.Join("|", statuses.ToArray()),
                        txtStartDate.Text, 
                        txtEndDate.Text) :
                    string.Empty;

                publishedOnly = chkPublished.Checked ? "1" : "0";
                institutionCode = listInstitutions.SelectedValue;
                statusId = GetStatusID();
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);
                hidStatus.Value = statusId;
                hidStartDate.Value = startDate.ToShortDateString();
                hidEndDate.Value = endDate.ToShortDateString();
                lnkDownloadResults.HRef = "Services/ItemPaginationDownloadService.ashx?pub=" + Server.UrlEncode(publishedOnly) + 
                    "&inst=" + Server.UrlEncode(institutionCode) + "&psid=" + Server.UrlEncode(statusId) +
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

            int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

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
                            string id = ids[x].Replace("jqg_list_", "");
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
            publishedOnly = chkPublished.Checked ? "1" : "0";
            institutionCode = listInstitutions.SelectedValue;
            statusId = GetStatusID();
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
        }

        private string GetStatusID()
        {
            string statusId = string.Format("{0}|{1}|{2}|{3}",
                (chkPStatNew.Checked ? PAGINATIONSTATUS_NEW : ""),
                (chkPStatIncomplete.Checked ? PAGINATIONSTATUS_INCOMPLETE : ""),
                (chkPStatInProgress.Checked ? PAGINATIONSTATUS_INPROGRESS : ""),
                (chkPStatComplete.Checked ? PAGINATIONSTATUS_COMPLETE : ""));

            return statusId;
        }

        private const string PAGINATIONSTATUS_NONESELECTED = "0";
        private const string PAGINATIONSTATUS_NEW = "5";
        private const string PAGINATIONSTATUS_INCOMPLETE = "10";
        private const string PAGINATIONSTATUS_INPROGRESS = "20";
        private const string PAGINATIONSTATUS_COMPLETE = "30";

        /// <summary>
        /// Add the appropriate items to the StatusChange dropdown, based on the selected status checkboxes
        /// </summary>
        private void SetStatusChangeItems()
        {
            ddlStatusChange.Items.Clear();
            spanStatusChange.Visible = false;

            if (chkPStatInProgress.Checked)
            {
                if (Helper.IsUserAuthorized(new HttpRequestWrapper(Request), Helper.SecurityRole.BHLAdminUserAdvanced)) // Only members of the BHL.Admin.SuperUser role can unlock items
                {
                    spanStatusChange.Visible = true;
                    ddlStatusChange.Items.Add(new ListItem("Incomplete", PAGINATIONSTATUS_INCOMPLETE));
                }
            }
        }

        private bool ValidateData()
        {
            bool isValid = true;

            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            if (string.IsNullOrEmpty(startDate)) { startDate = "1/1/1980"; txtStartDate.Text = startDate; }
            if (string.IsNullOrEmpty(endDate)) { endDate = DateTime.Now.ToShortDateString(); txtEndDate.Text = endDate; }

            isValid = (chkPStatNew.Checked || chkPStatIncomplete.Checked || chkPStatInProgress.Checked || chkPStatComplete.Checked);
            if (!isValid)
            {
                this.SetMessage("Select at least one pagination status to display.");
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
    }
}
