using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

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
            Response.AppendHeader("Content-Disposition", "attachment; filename=BHLGrowthStats" + institutionCode + ".xls");

            List<MonthlyStats> titleStats = new List<MonthlyStats>();
            List<MonthlyStats> itemStats = new List<MonthlyStats>();
            List<MonthlyStats> pageStats = new List<MonthlyStats>();
            List<MonthlyStats> nameStats = new List<MonthlyStats>();
            List<MonthlyStats> segmentStats = new List<MonthlyStats>();

            BHLProvider provider = new BHLProvider();

            Institution institution = provider.InstitutionSelectAuto(institutionCode);
            String institutionName = institution == null ? "(All)" : institution.InstitutionName;

            List<MonthlyStats> stats = provider.MonthlyStatsSelectByDateAndInstitution(2000, 1, 2099, 12, institutionCode);
            foreach(MonthlyStats stat in stats)
            {
                if (stat.StatValue > 0)
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
