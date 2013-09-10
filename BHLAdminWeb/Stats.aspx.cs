using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.AdminWeb
{
    public partial class Stats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string institutionName = String.Empty;
            bool showMonthly = false;
            BHLProvider provider = new BHLProvider();

            if (!this.IsPostBack)
            {
                CustomGenericList<Institution> institutions = provider.InstituationSelectAll();
                foreach (Institution institution in institutions)
                {
                    ddlInstitutions.Items.Add(new ListItem(institution.InstitutionName, institution.InstitutionName));
                }
            }

            institutionName = ddlInstitutions.SelectedValue;
            showMonthly = chkShowMonthly.Checked;

            if (showMonthly)
            {
                gvProductionTitlesByMonth.DataSource = provider.MonthlyStatsSelectByStatType("Titles Created", institutionName, showMonthly);
                gvProductionSegmentsByMonth.DataSource = provider.MonthlyStatsSelectByStatType("Segments Created", institutionName, showMonthly);
                gvProductionItemsByMonth.DataSource = provider.MonthlyStatsSelectByStatType("Items Created", institutionName, showMonthly);
                gvScannedItemsByMonth.DataSource = provider.MonthlyStatsSelectByStatType("Items Scanned", institutionName, showMonthly);
                gvProductionPagesByMonth.DataSource = provider.MonthlyStatsSelectByStatType("Pages Created", institutionName, showMonthly);
                gvProductionNamesByMonth.DataSource = provider.MonthlyStatsSelectByStatType("PageNames Created", institutionName, showMonthly);
                gvProductionTitlesByMonth.DataBind();
                gvProductionSegmentsByMonth.DataBind();
                gvProductionItemsByMonth.DataBind();
                gvScannedItemsByMonth.DataBind();
                gvProductionPagesByMonth.DataBind();
                gvProductionNamesByMonth.DataBind();
            }
            else
            {
                gvProductionTitles.DataSource = provider.MonthlyStatsSelectByStatType("Titles Created", institutionName, showMonthly);
                gvProductionSegments.DataSource = provider.MonthlyStatsSelectByStatType("Segments Created", institutionName, showMonthly);
                gvProductionItems.DataSource = provider.MonthlyStatsSelectByStatType("Items Created", institutionName, showMonthly);
                gvScannedItems.DataSource = provider.MonthlyStatsSelectByStatType("Items Scanned", institutionName, showMonthly);
                gvProductionPages.DataSource = provider.MonthlyStatsSelectByStatType("Pages Created", institutionName, showMonthly);
                gvProductionNames.DataSource = provider.MonthlyStatsSelectByStatType("PageNames Created", institutionName, showMonthly);
                gvProductionTitles.DataBind();
                gvProductionSegments.DataBind();
                gvProductionItems.DataBind();
                gvScannedItems.DataBind();
                gvProductionPages.DataBind();
                gvProductionNames.DataBind();
            }
        }

        private int _totalTitles = 0;
        private int _totalSegments = 0;
        private int _totalItems = 0;
        private int _totalScanned = 0;
        private int _totalPages = 0;
        private int _totalNames = 0;

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (((GridView)sender).ID)
            {
                case "gvProductionTitles":
                case "gvProductionTitlesByMonth":
                    {
                        this.CalculateTotal(e, ref _totalTitles);
                        break;
                    }
                case "gvProductionSegments":
                case "gvProductionSegmentsByMonth":
                    {
                        this.CalculateTotal(e, ref _totalSegments);
                        break;
                    }
                case "gvProductionItems": 
                case "gvProductionItemsByMonth":
                    {
                        this.CalculateTotal(e, ref _totalItems);
                        break;
                    }
                case "gvScannedItems":
                case "gvScannedItemsByMonth":
                    {
                        this.CalculateTotal(e, ref _totalScanned);
                        break;
                    }
                case "gvProductionPages":
                case "gvProductionPagesByMonth":
                    {
                        this.CalculateTotal(e, ref _totalPages);
                        break;
                    }
                case "gvProductionNames":
                case "gvProductionNamesByMonth":
                    {
                        this.CalculateTotal(e, ref _totalNames);
                        break;
                    }
            }
        }

        private void CalculateTotal(GridViewRowEventArgs e, ref int total)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MonthlyStats stats = (MonthlyStats)e.Row.DataItem;
                total += stats.StatValue;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[e.Row.Cells.Count - 1].Text = total.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool showMonthly = chkShowMonthly.Checked;
            if (showMonthly)
            {
                gvProductionTitles.Visible = false;
                gvProductionSegments.Visible = false;
                gvProductionItems.Visible = false;
                gvScannedItems.Visible = false;
                gvProductionPages.Visible = false;
                gvProductionNames.Visible = false;
                gvProductionTitlesByMonth.Visible = true;
                gvProductionSegmentsByMonth.Visible = true;
                gvProductionItemsByMonth.Visible = true;
                gvScannedItemsByMonth.Visible = true;
                gvProductionPagesByMonth.Visible = true;
                gvProductionNamesByMonth.Visible = true;
            }
            else
            {
                gvProductionTitles.Visible = true;
                gvProductionSegments.Visible = true;
                gvProductionItems.Visible = true;
                gvScannedItems.Visible = true;
                gvProductionPages.Visible = true;
                gvProductionNames.Visible = true;
                gvProductionTitlesByMonth.Visible = false;
                gvProductionSegmentsByMonth.Visible = false;
                gvProductionItemsByMonth.Visible = false;
                gvScannedItemsByMonth.Visible = false;
                gvProductionPagesByMonth.Visible = false;
                gvProductionNamesByMonth.Visible = false;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatsDownload.aspx?i=" +
                                Server.UrlEncode(ddlInstitutions.SelectedValue) +
                                "&m=" + Server.UrlEncode(chkShowMonthly.Checked.ToString()));
        }
    }
}
