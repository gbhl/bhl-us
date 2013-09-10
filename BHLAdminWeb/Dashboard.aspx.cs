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
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.AdminWeb.BHLImportWebService;

namespace MOBOT.BHL.AdminWeb
{
	public partial class Dashboard : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
            int ageLimit = Convert.ToInt32(ConfigurationManager.AppSettings["StatsPendingApprovalDownloadLimit"]);
            
            BHLProvider bp = new BHLProvider();
            BHLImportWSSoapClient ws = new BHLImportWSSoapClient();
            try
            {
                gvItemStatus.DataSource = ws.StatsSelectIAItemGroupByStatus();
                gvItemStatus.DataBind();

                gvBSItemStatus.DataSource = ws.StatsSelectBSItemGroupByStatus();
                gvBSItemStatus.DataBind();

                gvDOIStatus.DataSource = bp.DOIStatusSelectAll();
                gvDOIStatus.DataBind();
            }
            catch
            {
                // Do nothing
            }

            // Set the links for the web traffic stats
            apiv2StatsLink.HRef = string.Format(apiv2StatsLink.HRef, ConfigurationManager.AppSettings["APIStatsAppID"]);
            openurlStatsLink.HRef = string.Format(openurlStatsLink.HRef, ConfigurationManager.AppSettings["OpenUrlStatsAppID"]);
            trafficStatsMenu.Visible = new RequestLog().Loaded; // Show the web stats menu if stats are available

            // Get the PDF generation stats
            gvPDFGeneration.DataSource = bp.PDFStatsSelectOverview();
            gvPDFGeneration.DataBind();

            // Get the data harvest stats
            litNumDays.Text = ageLimit.ToString();
            hypNumItems.NavigateUrl += ageLimit.ToString();
            try
            {
                BHLImportWebService.Stats importStats = ws.StatsCountIAItemPendingApproval(ageLimit);
                hypNumItems.Text = importStats.NumberOfItems.ToString();
            }
            catch
            {
                // Do nothing
            }

            // Get the production stats
            String cacheKey = "DashboardStats";
            MOBOT.BHL.DataObjects.Stats stats = null;
            if (Cache[cacheKey] != null)
            {
                // Use cached version
                stats = (MOBOT.BHL.DataObjects.Stats)Cache[cacheKey];
            }
            else
            {
                // Refresh cache
                stats = bp.StatsSelectExpanded();
                Cache.Add(cacheKey, stats, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["DashboardStatsCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

			titlesAllCell.InnerHtml = stats.TitleTotal.ToString();
			titlesActiveCell.InnerHtml = stats.TitleCount.ToString();
			itemsActiveCell.InnerHtml = stats.VolumeCount.ToString();
			itemsAllCell.InnerHtml = stats.VolumeTotal.ToString();
			pagesActiveCell.InnerHtml = stats.PageCount.ToString();
			pagesAllCell.InnerHtml = stats.PageTotal.ToString();
            segmentsAllCell.InnerHtml = stats.SegmentTotal.ToString();
            segmentsActiveCell.InnerHtml = stats.SegmentCount.ToString();
            itemSegmentsAllCell.InnerHtml = stats.ItemSegmentTotal.ToString();
            itemSegmentsActiveCell.InnerHtml = stats.ItemSegmentCount.ToString();

            // Get the growth stats
            CustomGenericList<MonthlyStats> growthYear = bp.MonthlyStatsSelectCurrentYearSummary();
            CustomGenericList<MonthlyStats> growthMonth = bp.MonthlyStatsSelectCurrentMonthSummary();
            foreach (MonthlyStats stat in growthYear)
            {
                switch (stat.StatType)
                {
                    case "Titles Created":
                        titlesThisYear.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Items Created":
                        itemsThisYear.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Pages Created":
                        pagesThisYear.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "PageNames Created":
                        namesThisYear.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Segments Created":
                        segmentsThisYear.InnerHtml = stat.StatValue.ToString();
                        break;
                }
            }
            foreach (MonthlyStats stat in growthMonth)
            {
                switch (stat.StatType)
                {
                    case "Titles Created":
                        titlesThisMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Items Created":
                        itemsThisMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Pages Created":
                        pagesThisMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "PageNames Created":
                        namesThisMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Segments Created":
                        segmentsThisMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                }
            }
        }
	}
}
