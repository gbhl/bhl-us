﻿using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class GrowthStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();

            if (!this.IsPostBack)
            {
                List<Institution> institutions = provider.InstitutionSelectForMonthlyStats();
                foreach (Institution institution in institutions)
                {
                    ddlInstitutions.Items.Add(new ListItem(institution.InstitutionName, institution.InstitutionCode));
                }

                this.BuildCharts("", -6);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            this.BuildCharts(ddlInstitutions.SelectedValue, Convert.ToInt32(ddlTimespan.SelectedValue));
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect("GrowthStatsDownload.aspx?i=" +
                                Server.UrlEncode(ddlInstitutions.SelectedValue));
        }

        private void BuildCharts(String institutionCode, int dateRange)
        {
            DateTime now = DateTime.Now;
            int startYear = 0;
            int startMonth = 0;
            int endYear = now.Year;
            int endMonth = now.Month;

            switch (dateRange)
            {
                case 0: // Current month
                case -3: // Last 3 months
                case -6: // Last 6 months
                case -12: // Last 12 months
                case -24: // Last 24 months
                case -36: // Last 36 months
                    now = now.AddMonths(dateRange);
                    startYear = now.Year;
                    startMonth = now.Month;
                    break;
                case 1: // Current year
                    startYear = now.Year;
                    startMonth = 1;
                    break;
                case 2: // Since inception
                    startYear = 2000;
                    startMonth = 1;
                    break;
            }

            BHLProvider provider = new BHLProvider();
            List<MonthlyStats> stats = provider.MonthlyStatsSelectByDateAndInstitution(
                startYear, startMonth, endYear, endMonth, institutionCode);

            imgMonthlyItems.Src = GetMonthlyChartUrl(stats, "Items", "Items");
            imgMonthlyPages.Src = GetMonthlyChartUrl(stats, "Pages", "Pages");
            imgMonthlyNames.Src = GetMonthlyChartUrl(stats, "PageNames", "Names");
            imgMonthlySegments.Src = GetMonthlyChartUrl(stats, "Segments", "Segments");
            //imgCumulative.Src = GetCumulativeChartUrl(stats);
        }

        private string GetMonthlyChartUrl(List<MonthlyStats> stats, string statType, string statLabel)
        {
            int maxValue = 0;
            String data = String.Empty; //153,113,120|60,70,80|52,60,40|30,50,45
            String legend = String.Empty; //|Jan%2009|Feb%2009|Mar%2009

            String lastStatType = String.Empty;
            foreach (MonthlyStats stat in stats)
            {
                if (stat.StatType == string.Format("{0} Created", statType))
                {
                    if (lastStatType != stat.StatType)
                    {
                        if (lastStatType != String.Empty)
                        {
                            data = data.Substring(0, data.Length - 1) + "|";
                        }
                        lastStatType = stat.StatType;
                    }

                    // Capture the largest data value
                    if (stat.StatValue > maxValue) maxValue = stat.StatValue;

                    // Build the legend for the chart
                    if (stat.StatType == string.Format("{0} Created", statType))
                    {
                        legend += "|" + stat.Month.ToString() + "-" + stat.Year.ToString().Substring(2, 2);
                    }

                    // Build the data for the chart
                    data += stat.StatValue.ToString() + ",";
                }
            }

            // trim off final commas or separators
            if (data.Length > 0) data = data.Substring(0, data.Length - 1);

            string monthlyChartUrl = String.Format(ConfigurationManager.AppSettings["GoogleMonthlyChartUrl"].ToString(),
                statLabel, data, maxValue.ToString(), maxValue.ToString(), maxValue.ToString(), legend);

            return monthlyChartUrl;
        }

        private string GetCumulativeChartUrl(List<MonthlyStats> stats)
        {
            int maxCumulativeValue = 0;
            int cumulativeValue = 0;
            String cumulativeData = String.Empty;
            String legend = String.Empty; //|Jan%2009|Feb%2009|Mar%2009

            String lastStatType = String.Empty;
            foreach (MonthlyStats stat in stats)
            {
                if (stat.StatType != "Titles Created")
                {
                    if (lastStatType != stat.StatType)
                    {
                        if (lastStatType != String.Empty)
                        {
                            cumulativeData = cumulativeData.Substring(0, cumulativeData.Length - 1) + "|";
                        }
                        lastStatType = stat.StatType;
                        cumulativeValue = 0;
                    }

                    // Get the cumulative value
                    cumulativeValue += stat.StatValue;

                    // Capture the largest data value
                    if (cumulativeValue > maxCumulativeValue) maxCumulativeValue = cumulativeValue;

                    // Build the legend for the chart
                    if (stat.StatType == "Items Created")
                    {
                        legend += "|" + stat.Month.ToString() + "-" + stat.Year.ToString().Substring(2, 2);
                    }

                    // Build the data for the chart
                    cumulativeData += cumulativeValue.ToString() + ",";
                }
            }

            // trim off final commas or separators
            if (cumulativeData.Length > 0) cumulativeData = cumulativeData.Substring(0, cumulativeData.Length - 1);

            string cumulativeChartUrl = String.Format(ConfigurationManager.AppSettings["GoogleCumulativeChartUrl"].ToString(),
                "Items|Pages|Names|Segments", cumulativeData, maxCumulativeValue.ToString(), maxCumulativeValue.ToString(),
                maxCumulativeValue.ToString(), legend);

            return cumulativeChartUrl;
        }
    }
}
