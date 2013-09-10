using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportIAItemsPendingApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ageInDays = Request.QueryString["age"] as string;
            if (ageInDays == null) ageInDays = "45";

            Response.Clear();
            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-Disposition", "attachment; filename=ItemsPendingApproval.xls");

            BHLImportWebService.BHLImportWSSoapClient ws = new BHLImportWebService.BHLImportWSSoapClient();
            gvPendingApproval.DataSource = ws.IAItemSelectPendingApproval(Convert.ToInt32(ageInDays));
            gvPendingApproval.DataBind();
        }
    }
}
