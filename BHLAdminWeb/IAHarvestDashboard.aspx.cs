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
using MOBOT.BHL.AdminWeb.BHLImportWebService;

namespace MOBOT.BHL.AdminWeb
{
    public partial class IAHarvestDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int numLogsToDisplay = Convert.ToInt32(ConfigurationManager.AppSettings["StatsNumberOfLogItemsToDisplay"]);
            int ageLimit = Convert.ToInt32(ConfigurationManager.AppSettings["StatsPendingApprovalDownloadLimit"]);
            int ageDisplay = Convert.ToInt32(ConfigurationManager.AppSettings["StatsPendingApprovalMinimimDisplayAge"]);

            BHLImportWSSoapClient ws = new BHLImportWSSoapClient();
            gvItemCountByStatus.DataSource = ws.StatsSelectIAItemGroupByStatus();
            gvItemCountByStatus.DataBind();

            hypNumItems.NavigateUrl += ageLimit.ToString();

            gvIAReadyToPublish.DataSource = ws.StatsSelectReadyForProductionBySource(1);
            gvIAReadyToPublish.DataBind();

            gvLatestPubToProdLogs.DataSource = ws.ImportLogSelectRecent(numLogsToDisplay);
            gvLatestPubToProdLogs.DataBind();

            gvLatestPubToProdErrors.DataSource = ws.ImportErrorSelectRecent(numLogsToDisplay);
            gvLatestPubToProdErrors.DataBind();

            gvIAItemErrors.DataSource = ws.IAItemErrorSelectRecent(numLogsToDisplay);
            gvIAItemErrors.DataBind();
        }

    }
}
