using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class TitleImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Save the batch id
                String batchID = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();

                // Get the list of titles in this batch
                int batchInt;
                if (Int32.TryParse(batchID, out batchInt))
                {
                    this.ViewState.Add("BatchID", batchID);

                    BHLProvider provider = new BHLProvider();
                    titlesList.DataSource = provider.MarcSelectPendingImport(Convert.ToInt32(batchID));
                    titlesList.DataBind();
                }
            }

            errorControl.Visible = false;
        }

        protected void creatorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                int marcID = (int)titlesList.DataKeys[rowNum].Value;

                // Delete the Marc record
                if (new BHLProvider().MarcDeleteAuto(marcID))
                {
                    String batchID = this.ViewState["BatchID"] == null ? "" : this.ViewState["BatchID"].ToString();
                    Response.Redirect("TitleImport.aspx?id=" + batchID);
                }
                else
                {
                    errorControl.AddErrorText("Unable to delete this title.");
                }
            }
        }

        protected void importButton_Click(object sender, EventArgs e)
        {
            // Redirect to the page that will perform the import
            String batchID = this.ViewState["BatchID"] == null ? "" : this.ViewState["BatchID"].ToString();
            Response.Redirect("TitleImportResults.aspx?id=" + batchID);
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            // Cancel the import of this batch (delete all records)
            int batchID = this.ViewState["BatchID"] == null ? -1 : Convert.ToInt32(this.ViewState["BatchID"].ToString());
            if (new BHLProvider().MarcImportBatchDeleteAuto(batchID))
            {
                Response.Redirect("TitleSearch.aspx");
            }
            else
            {
                errorControl.AddErrorText("Unable to delete this batch of titles.");
            }
        }

    }
}
