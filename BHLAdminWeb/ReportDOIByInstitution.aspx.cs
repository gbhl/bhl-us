using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportDOIByInstitution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();
            CustomGenericList<Institution> institutions = provider.InstitutionSelectDOIStats(Convert.ToInt32(rblOrderBy.SelectedValue));

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
    }
}