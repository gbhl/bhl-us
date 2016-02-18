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
    public partial class TitleImportHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
                BHLProvider bp = new BHLProvider();

                CustomGenericList<Institution> institutions = bp.InstituationSelectAll();

                Institution emptyInstitution = new Institution();
                emptyInstitution.InstitutionName = "(select contributor)";
                emptyInstitution.InstitutionCode = "";
                institutions.Insert(0, emptyInstitution);
                listInstitutions.DataSource = institutions;
                listInstitutions.DataBind();
            }

			Page.SetFocus( listInstitutions);
			Page.Title = "BHL Admin - Title Import History";
        }

        #region Event Handlers

        protected void gvwImportStats_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("batchLink");
                hl.NavigateUrl = "/TitleImport.aspx?id=" + hl.Text;
                int pending = Convert.ToInt32(e.Row.Cells[4].Text);
                if (pending == 0) hl.Enabled = false;
            }
        }

        protected void listInstitutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string code = listInstitutions.SelectedValue;
            if (code.Length > 0)
            {
                BHLProvider bp = new BHLProvider();

                // Load gridview with import stats for the institution
                CustomGenericList<MarcImportBatch> batches = bp.MarcImportBatchSelectStatsByInstitution(code);
                gvwImportStats.DataSource = batches;
                gvwImportStats.DataBind();
                litNoRecords.Visible = (batches.Count == 0) ? true : false;
            }
        }

        #endregion Event Handlers
    }
}
