using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportMonographicContributions : System.Web.UI.Page
    {
        private string _MsgFormat = "<font color='red'>{0}</font>";

        protected void Page_Load(object sender, EventArgs e)
        {
            // jQuery runtime
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            // jQuery UI library
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

            //CustomGenericList<NonMemberMonograph> contributions = new CustomGenericList<NonMemberMonograph>();
            litMsg.Text = string.Empty;

            BHLProvider provider = new BHLProvider();
            if (this.IsPostBack)
            {
                /*
                string msg = string.Empty;

                string institutionCode = string.Empty;
                ddlNonMembers.Enabled = false;
                ddlMembers.Enabled = false;
                if (rdoMember.Checked)
                {
                    institutionCode = ddlMembers.SelectedValue;
                    ddlMembers.Enabled = true;
                }
                if (rdoNonMember.Checked)
                {
                    institutionCode = ddlNonMembers.SelectedValue;
                    ddlNonMembers.Enabled = true;
                }

                if (this.ValidateDate(txtSinceDate.Text, out msg))
                {
                    contributions = provider.ItemSelectNonMemberMonograph(txtSinceDate.Text, 
                        (rdoNonMember.Checked || rdoAllNonMembers.Checked ? 0 : 1), 
                        institutionCode);
                    if (contributions.Count == 0) litMsg.Text = string.Format(_MsgFormat, "No results");
                }
                else
                {
                    litMsg.Text = string.Format(_MsgFormat, msg);
                }
                 */
            }
            else
            {
                CustomGenericList<Institution> institutions = provider.InstituationSelectAll();
                foreach (Institution institution in institutions)
                {
                    if (institution.BHLMemberLibrary) 
                        ddlMembers.Items.Add(new ListItem(institution.InstitutionName, institution.InstitutionCode));
                    else
                        ddlNonMembers.Items.Add(new ListItem(institution.InstitutionName, institution.InstitutionCode));
                }
            }
            /*
            monographList.DataSource = contributions;
            monographList.DataBind();
             */
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (this.ValidateDate(txtSinceDate.Text, out msg))
            {
                string isMember = "0";
                string institutionCode = string.Empty;

                if (rdoAllMembers.Checked || rdoMember.Checked) isMember = "1";
                if (rdoMember.Checked) institutionCode = ddlMembers.SelectedValue;
                if (rdoNonMember.Checked) institutionCode = ddlNonMembers.SelectedValue;

                Response.Redirect("ReportNonMemberMonographsCSV.ashx?date=" + txtSinceDate.Text +
                    "&member=" + isMember + "&inst=" + institutionCode);
            }
            else
            {
                litMsg.Text = string.Format(_MsgFormat, msg);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            CustomGenericList<NonMemberMonograph> contributions = new CustomGenericList<NonMemberMonograph>();

            string institutionCode = string.Empty;
            ddlNonMembers.Enabled = false;
            ddlMembers.Enabled = false;
            if (rdoMember.Checked)
            {
                institutionCode = ddlMembers.SelectedValue;
                ddlMembers.Enabled = true;
            }
            if (rdoNonMember.Checked)
            {
                institutionCode = ddlNonMembers.SelectedValue;
                ddlNonMembers.Enabled = true;
            }

            if (this.ValidateDate(txtSinceDate.Text, out msg))
            {
                contributions = new BHLProvider().ItemSelectNonMemberMonograph(txtSinceDate.Text,
                    (rdoNonMember.Checked || rdoAllNonMembers.Checked ? 0 : 1),
                    institutionCode);
                if (contributions.Count == 0) litMsg.Text = string.Format(_MsgFormat, "No results");
            }
            else
            {
                litMsg.Text = string.Format(_MsgFormat, msg);
            }

            monographList.DataSource = contributions;
            monographList.DataBind();
        }

        protected void btnClearResults_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path);
        }

        private bool ValidateDate(string sinceDateString, out string msg)
        {
            bool valid = false;
            DateTime sinceDate;
            msg = string.Empty;

            // Make sure the specified string is a valid date
            if (DateTime.TryParse(sinceDateString, out sinceDate))
            {
                // Make sure the date is between 1/1/1753 and 12/31/9999.
                if (sinceDate >= DateTime.Parse("1/1/1753") && sinceDate <= DateTime.Parse("12/31/9999"))
                    valid = true;
                else
                    msg = "Please specify a date between 1/1/1753 and 12/31/9999.";
            }
            else
            {
                msg = "Please specify a valid date";
            }

            return valid;
        }
    }
}