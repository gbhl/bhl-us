using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    public partial class GrowthStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();

            if (!this.IsPostBack)
            {
                CustomGenericList<Institution> institutions = provider.InstitutionSelectWithPublishedItems(false);
                foreach (Institution institution in institutions)
                {
                    ddlInstitutions.Items.Add(new ListItem(institution.InstitutionName, institution.InstitutionName));
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

        private void BuildCharts(String institutionName, int dateRange)
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
            CustomGenericList<MonthlyStats> stats = provider.MonthlyStatsSelectByDateAndInstitution(
                startYear, startMonth, endYear, endMonth, institutionName);

            int maxValue = 0;
            int maxCumulativeValue = 0;
            int cumulativeValue = 0;
            String data = String.Empty; //153,113,120|60,70,80|52,60,40|30,50,45
            String cumulativeData = String.Empty;
            String legend = String.Empty; //|Jan%2009|Feb%2009|Mar%2009

            String statType = String.Empty;
            foreach (MonthlyStats stat in stats)
            {
                if (stat.StatType == "Items Created")   // Remove this conditional to include titles, pages, and names on the chart
                {
                    if (statType != stat.StatType)
                    {
                        if (statType != String.Empty)
                        {
                            data = data.Substring(0, data.Length - 1) + "|";
                            cumulativeData = cumulativeData.Substring(0, cumulativeData.Length - 1) + "|";
                        }
                        statType = stat.StatType;
                        cumulativeValue = 0;
                    }

                    // Get the cumulative value
                    cumulativeValue += stat.StatValue;

                    // Capture the largest data value
                    if (stat.StatValue > maxValue) maxValue = stat.StatValue;
                    if (cumulativeValue > maxCumulativeValue) maxCumulativeValue = cumulativeValue;

                    // Build the legend for the chart
                    if (stat.StatType == "Items Created")
                    {
                        legend += "|" + stat.Month.ToString() + "-" + stat.Year.ToString().Substring(2, 2);
                    }

                    // Build the data for the chart
                    data += stat.StatValue.ToString() + ",";
                    cumulativeData += cumulativeValue.ToString() + ",";
                }
            }

            // trim off final commas or separators
            if (data.Length > 0) data = data.Substring(0, data.Length - 1);
            if (cumulativeData.Length > 0) cumulativeData = cumulativeData.Substring(0, cumulativeData.Length - 1);

            String monthlyChartUrl = String.Format(ConfigurationManager.AppSettings["GoogleMonthlyChartUrl"].ToString(), 
                data, maxValue.ToString(), maxValue.ToString(), maxValue.ToString(), legend);
            imgMonthly.Src = monthlyChartUrl;

            String cumulativeChartUrl = String.Format(ConfigurationManager.AppSettings["GoogleCumulativeChartUrl"].ToString(),
                cumulativeData, maxCumulativeValue.ToString(), maxCumulativeValue.ToString(),
                maxCumulativeValue.ToString(), legend);
            imgCumulative.Src = cumulativeChartUrl;
        }

    }
}
