using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.AdminWeb
{
    public partial class StatsDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String institutionName = Request.QueryString["i"] as String;
            if (institutionName == null) institutionName = "";
            String m = Request.QueryString["m"] as String;
            if (m == null) m = "False";
            if (m != "True" && m != "False") m = "False";
            bool showMonthly = (m == "True" ? true : false);

            Response.Clear();
            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-Disposition", "attachment; filename=BHLExpandedStats.xls");

            BHLProvider provider = new BHLProvider();

            gvProductionTitles.Columns[1].Visible = showMonthly;
            gvProductionTitles.Columns[2].Visible = showMonthly;
            gvProductionItems.Columns[1].Visible = showMonthly;
            gvProductionItems.Columns[2].Visible = showMonthly;
            gvScannedItems.Columns[1].Visible = showMonthly;
            gvScannedItems.Columns[2].Visible = showMonthly;
            gvProductionPages.Columns[1].Visible = showMonthly;
            gvProductionPages.Columns[2].Visible = showMonthly;
            gvProductionNames.Columns[1].Visible = showMonthly;
            gvProductionNames.Columns[2].Visible = showMonthly;
            gvProductionSegments.Columns[1].Visible = showMonthly;
            gvProductionSegments.Columns[2].Visible = showMonthly;

            gvProductionTitles.DataSource = provider.MonthlyStatsSelectByStatType("Titles Created", institutionName, showMonthly);
            gvProductionItems.DataSource = provider.MonthlyStatsSelectByStatType("Items Created", institutionName, showMonthly);
            gvScannedItems.DataSource = provider.MonthlyStatsSelectByStatType("Items Scanned", institutionName, showMonthly);
            gvProductionPages.DataSource = provider.MonthlyStatsSelectByStatType("Pages Created", institutionName, showMonthly);
            gvProductionNames.DataSource = provider.MonthlyStatsSelectByStatType("PageNames Created", institutionName, showMonthly);
            gvProductionSegments.DataSource = provider.MonthlyStatsSelectByStatType("Segments Created", institutionName, showMonthly);

            gvProductionTitles.DataBind();
            gvProductionItems.DataBind();
            gvScannedItems.DataBind();
            gvProductionPages.DataBind();
            gvProductionNames.DataBind();
            gvProductionSegments.DataBind();
        }
    }
}
