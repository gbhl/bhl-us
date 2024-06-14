using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class GrowthStats : System.Web.UI.Page
    {
        public string itemsData = string.Empty;
        public string pagesData = string.Empty;
        public string namesData = string.Empty;
        public string segmentsData = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();

            if (!this.IsPostBack)
            {
                List<Institution> institutions = provider.InstituationSelectAll();
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

        private void BuildCharts(string institutionCode, int dateRange)
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

            itemsData = GetDataString(stats, "Items");
            pagesData = GetDataString(stats, "Pages");
            namesData = GetDataString(stats, "PageNames");
            segmentsData = GetDataString(stats, "Segments");
        }

        /// <summary>
        /// Format the specified data into a fragment of a JSON array.
        /// Example data string: "['2024-1', 886],['2024-2', 1267],['2024-3', 669],['2024-4', 476],['2024-5', 740],['2024-6', 165]"
        /// </summary>
        /// <param name="stats"></param>
        /// <param name="statType"></param>
        /// <returns></returns>
        private string GetDataString(List<MonthlyStats> stats, string statType)
        {
            List<string> dataList = new List<string>();

            // Build the data for the chart
            foreach (MonthlyStats stat in stats)
            {
                if (stat.StatType == string.Format("{0} Created", statType))
                {
                    dataList.Add(string.Format("['{0}-{1}', {2}]", stat.Year.ToString(), stat.Month.ToString(), stat.StatValue.ToString()));
                }
            }

            return string.Join(",", dataList.ToArray());
        }
    }
}
