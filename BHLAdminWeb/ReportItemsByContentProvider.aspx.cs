using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportItemsByContentProvider : System.Web.UI.Page
	{
        private string _MsgFormat = "<font color='red'>{0}</font>";
        public string selectedInstitutionCode = string.Empty;
        public string selectedRoleID = string.Empty;

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

            BHLProvider bp = new BHLProvider();
            CustomGenericList<Item> items = new CustomGenericList<Item>();
            litDisplayed.Text = string.Empty;
            litUpdateResult.Text = string.Empty;

            if (!IsPostBack)
            {
                CustomGenericList<Institution> institutions = bp.InstituationSelectAll();
                ddlInstitutions.Items.Add(new ListItem("(select content provider)", "^^^^^^^^"));
                ddlInstitutions.Items.Add(new ListItem("- UNASSIGNED -", ""));
                ddlInstitutionChange.Items.Add(new ListItem("(select content provider)", "^^^^^^^^"));
                ddlInstitutionChange.Items.Add(new ListItem("- UNASSIGNED -", ""));
                foreach (Institution institution in institutions)
                {
                    ListItem li = new ListItem(institution.InstitutionName, institution.InstitutionCode);
                    ddlInstitutions.Items.Add(li);
                    ddlInstitutionChange.Items.Add(li);
                }

                CustomGenericList<InstitutionRole> roles = bp.InstitutionRoleSelectAll();
                ddlInstitutionRoles.Items.Add(new ListItem("(select role)", ""));
                ddlInstitutionRoleChange.Items.Add(new ListItem("(select field to update)", ""));
                foreach (InstitutionRole role in roles)
                {
                    if (role.InstitutionRoleName != "Contributor" && role.InstitutionRoleName != "External Content Holder")
                    {
                        ListItem li = new ListItem(role.InstitutionRoleName, role.InstitutionRoleID.ToString());
                        ddlInstitutionRoles.Items.Add(li);
                        ddlInstitutionRoleChange.Items.Add(li);
                    }
                }
            }

            Page.SetFocus(ddlInstitutions);
            Page.Title = "BHL Admin - Items By Content Provider";
        }

        /// <summary>
        /// Display the items that are from the selected contributor and in the selected role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (Validate(out msg))
            {
                litDisplayed.Text =
                    (ddlInstitutions.SelectedValue == "^^^^^^^^" || ddlInstitutionRoles.SelectedValue == string.Empty) ?
                    string.Empty :
                    (string.IsNullOrWhiteSpace(ddlInstitutions.SelectedValue)) ?
                        string.Format("Items where the <b>{1}</b> role is <b>{0}</b>.", ddlInstitutions.SelectedItem.Text, ddlInstitutionRoles.SelectedItem.Text) :
                        string.Format("Items from <b>{0}</b> in the <b>{1}</b> role.", ddlInstitutions.SelectedItem.Text, ddlInstitutionRoles.SelectedItem.Text);
                selectedInstitutionCode = ddlInstitutions.SelectedValue;
                selectedRoleID = ddlInstitutionRoles.SelectedValue;
                ddlInstitutionChange.SelectedIndex = 0;
                ddlInstitutionRoleChange.SelectedIndex = 0;
                litUpdateResult.Text = string.Empty;
            }
            else
            {
                litDisplayed.Text = string.Format(_MsgFormat, msg);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (this.Validate(out msg))
            {
                Response.Redirect("ReportItemsByContentProviderCSV.ashx?id=" + ddlInstitutions.SelectedValue + "&role=" + ddlInstitutionRoles.SelectedValue);
            }
            else
            {
                litDisplayed.Text = string.Format(_MsgFormat, msg);
            }
        }

        private bool Validate(out string msg)
        {
            msg = string.Empty;

            bool valid = ddlInstitutions.SelectedValue != "^^^^^^^^" && !string.IsNullOrEmpty(ddlInstitutionRoles.SelectedValue);
            if (!valid) msg = "Please select a content provider and a role";

            return valid;
        }

        /// <summary>
        /// Update the selected items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChange_Click(object sender, EventArgs e)
        {
            string updateMsg = string.Empty;
            litUpdateResult.Text = string.Empty;

            if (string.IsNullOrEmpty(ddlInstitutionRoleChange.SelectedValue))
            {
                updateMsg = "Please select a field to update in the selected items";
            }
            else
            if (ddlInstitutionChange.SelectedValue == "^^^^^^^^")
            {
                updateMsg = "Please select a new content provider to assign to the selected items";
            }
            else
            {
                try
                {
                    int userID = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                    BHLProvider service = new BHLProvider();

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
                                string[] wsResponse = service.ItemUpdateInstitution(Convert.ToInt32(id), 
                                    ddlInstitutionChange.SelectedValue, Convert.ToInt32(ddlInstitutionRoleChange.SelectedValue), userID);

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
                    updateMsg += "Error: " + ex.Message;
                }
            }

            // Refresh the grid
            litUpdateResult.Text = string.Format(_MsgFormat, updateMsg);
            selectedInstitutionCode = ddlInstitutions.SelectedValue;
            selectedRoleID = ddlInstitutionRoles.SelectedValue;
        }
    }
}