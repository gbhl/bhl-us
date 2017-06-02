using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportItemsByContributor : System.Web.UI.Page
    {
        private string _MsgFormat = "<font color='red'>{0}</font>";

        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider bp = new BHLProvider();
            CustomGenericList<Item> items = new CustomGenericList<Item>();
            litMsg.Text = string.Empty;

            if (!IsPostBack)
            {
                CustomGenericList<Institution> institutions = bp.InstituationSelectAll();
                Institution emptyInstitution = new Institution();
                emptyInstitution.InstitutionName = "(select content provider)";
                emptyInstitution.InstitutionCode = "";
                institutions.Insert(0, emptyInstitution);
                listInstitutions.DataSource = institutions;
                listInstitutions.DataBind();
            }
            else
            {
                string msg = string.Empty;
                if (this.Validate(out msg))
                {
                    items = bp.ItemSelectByInstitution(listInstitutions.SelectedValue, 
                        Convert.ToInt32(selSince.Value), selSortBy.Value);

                    if (items.Count == 0) litMsg.Text = string.Format(_MsgFormat, "No items found.");
                }
                else
                {
                    litMsg.Text = string.Format(_MsgFormat, msg);
                }
            }

            itemList.DataSource = items;
            itemList.DataBind();

            Page.SetFocus(listInstitutions);
            Page.Title = "BHL Admin - Items By Content Provider";
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (this.Validate(out msg))
            {
                Response.Redirect("ReportItemsByContributorCSV.ashx?inst=" + listInstitutions.SelectedValue);
            }
            else
            {
                litMsg.Text = string.Format(_MsgFormat, msg);
            }
        }

        private bool Validate(out string msg)
        {
            msg = string.Empty;

            bool valid = !string.IsNullOrEmpty(listInstitutions.SelectedValue);
            if (!valid) msg = "Please select a content provider";

            return valid;
        }
    }
}