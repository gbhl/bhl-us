using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportDOIByInstitution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();
            List<Institution> institutions = provider.InstitutionSelectDOIStats(Convert.ToInt32(rblOrderBy.SelectedValue), chkIncludeAll.Checked ? 0 : 1);

            if (chkShow.Checked)
            {
                // Only show BHL member libraries
                for (int x = institutions.Count - 1; x >= 0; x--)
                {
                    if (!institutions[x].BHLMemberLibrary) institutions.RemoveAt(x);
                }
            }

            institutionList.DataSource = institutions;
            institutionList.DataBind();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportDOIByInstitutionCSV.ashx" +
                "?s=" + rblOrderBy.SelectedValue + 
                "&i=" + (chkIncludeAll.Checked ? "0" : "1") + 
                "&b=" + (chkShow.Checked ? "1" : "0"));
        }
    }
}