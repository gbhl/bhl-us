using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class GrowthStatsDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String institutionCode = Request.QueryString["i"] as String;
            if (institutionCode == null) institutionCode = "";

            Response.Clear();
            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-Disposition", "attachment; filename=BHLGrowthStats.xls");

            CustomGenericList<MonthlyStats> titleStats = new CustomGenericList<MonthlyStats>();
            CustomGenericList<MonthlyStats> itemStats = new CustomGenericList<MonthlyStats>();
            CustomGenericList<MonthlyStats> pageStats = new CustomGenericList<MonthlyStats>();
            CustomGenericList<MonthlyStats> nameStats = new CustomGenericList<MonthlyStats>();
            CustomGenericList<MonthlyStats> segmentStats = new CustomGenericList<MonthlyStats>();

            BHLProvider provider = new BHLProvider();

            Institution institution = provider.InstitutionSelectAuto(institutionCode);
            String institutionName = institution == null ? "(All)" : institution.InstitutionName;

            CustomGenericList<MonthlyStats> stats = provider.MonthlyStatsSelectByDateAndInstitution(2000, 1, 2099, 12, institutionCode);
            foreach(MonthlyStats stat in stats)
            {
                switch (stat.StatType)
                {
                    case "Titles Created":
                        titleStats.Add(stat);
                        break;
                    case "Items Created":
                        itemStats.Add(stat);
                        break;
                    case "Pages Created":
                        pageStats.Add(stat);
                        break;
                    case "PageNames Created":
                        nameStats.Add(stat);
                        break;
                    case "Segments Created":
                        segmentStats.Add(stat);
                        break;
                }
            }

            litInstitution.Text = institutionName;
            gvTitles.DataSource = titleStats;
            gvItems.DataSource = itemStats;
            gvPages.DataSource = pageStats;
            gvNames.DataSource = nameStats;
            gvSegments.DataSource = segmentStats;
            gvTitles.DataBind();
            gvItems.DataBind();
            gvPages.DataBind();
            gvNames.DataBind();
            gvSegments.DataBind();
        }
    }
}
