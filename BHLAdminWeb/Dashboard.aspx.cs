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
using MOBOT.BHLImport.Server;

namespace MOBOT.BHL.AdminWeb
{
	public partial class Dashboard : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{
            int ageLimit = Convert.ToInt32(ConfigurationManager.AppSettings["StatsPendingApprovalDownloadLimit"]);
            
            BHLProvider bp = new BHLProvider();
            BHLImportProvider bip = new BHLImportProvider();
            try
            {
                gvItemStatus.DataSource = bip.StatsSelectIAItemGroupByStatus();
                gvItemStatus.DataBind();

                gvBSItemStatus.DataSource = bip.StatsSelectBSItemGroupByStatus();
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
            apiv3StatsLink.HRef = string.Format(apiv3StatsLink.HRef, ConfigurationManager.AppSettings["APIV3StatsAppID"]);
            openurlStatsLink.HRef = string.Format(openurlStatsLink.HRef, ConfigurationManager.AppSettings["OpenUrlStatsAppID"]);
            trafficStatsMenu.Visible = new BHL.Web.Utilities.RequestLog().Loaded; // Show the web stats menu if stats are available

            // Get the PDF generation stats
            gvPDFGeneration.DataSource = bp.PDFStatsSelectOverview();
            gvPDFGeneration.DataBind();

            // Get the data harvest stats
            litNumDays.Text = ageLimit.ToString();
            hypNumItems.NavigateUrl += ageLimit.ToString();
            try
            {
                BHLImport.DataObjects.Stats importStats = bip.StatsCountIAItemPendingApproval(ageLimit);
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
            CustomGenericList<MonthlyStats> growthPrevMonth = bp.MonthlyStatsSelectPreviousMonthSummary();
            foreach (MonthlyStats stat in growthYear)
            {
                switch (stat.StatType)
                {
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

            foreach (MonthlyStats stat in growthPrevMonth)
            {
                switch (stat.StatType)
                {
                    case "Items Created":
                        itemsPrevMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Pages Created":
                        pagesPrevMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "PageNames Created":
                        namesPrevMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                    case "Segments Created":
                        segmentsPrevMonth.InnerHtml = stat.StatValue.ToString();
                        break;
                }

            }

            MenuSetup(Request);
        }

        protected void MenuSetup(HttpRequest request)
        {
            // To disable a menu item, replace the menu anchor control with just the text of the control
            if (!Helper.IsUserAuthorized(new HttpRequestWrapper(request), Helper.SecurityRole.BHLAdminPortalEditor))
            {
                tdTitles.InnerHtml = GetMenuText(tdTitles.InnerHtml);
                tdItems.InnerHtml = GetMenuText(tdItems.InnerHtml);
                tdSegments.InnerHtml = GetMenuText(tdSegments.InnerHtml);
                tdPagination.InnerHtml = GetMenuText(tdPagination.InnerHtml);
                tdAuthors.InnerHtml = GetMenuText(tdAuthors.InnerHtml);
                tdNames.InnerHtml = GetMenuText(tdNames.InnerHtml);
                tdRptItemPagination.InnerHtml = GetMenuText(tdRptItemPagination.InnerHtml);
                tdRptTitleImportHistory.InnerHtml = GetMenuText(tdRptTitleImportHistory.InnerHtml);
            }
            if (!Helper.IsUserAuthorized(new HttpRequestWrapper(request), Helper.SecurityRole.BHLAdminUserBasic))
            {
                tdCollections.InnerHtml = GetMenuText(tdCollections.InnerHtml);
                tdInstitutions.InnerHtml = GetMenuText(tdInstitutions.InnerHtml);
                tdLanguages.InnerHtml = GetMenuText(tdLanguages.InnerHtml);
                tdNoteTypes.InnerHtml = GetMenuText(tdNoteTypes.InnerHtml);
                tdPageTypes.InnerHtml = GetMenuText(tdPageTypes.InnerHtml);
                tdPDFRequests.InnerHtml = GetMenuText(tdPDFRequests.InnerHtml);
                tdSegmentTypes.InnerHtml = GetMenuText(tdSegmentTypes.InnerHtml);

                tdShowNames.InnerHtml = GetMenuText(tdShowNames.InnerHtml);
                tdShowUniqueNames.InnerHtml = GetMenuText(tdShowUniqueNames.InnerHtml);
                tdShowVerifiedNames.InnerHtml = GetMenuText(tdShowVerifiedNames.InnerHtml);
                tdShowEOLNames.InnerHtml = GetMenuText(tdShowEOLNames.InnerHtml);
                tdShowEOLPages.InnerHtml = GetMenuText(tdShowEOLPages.InnerHtml);

                tdExpandedGrowthStats.InnerHtml = GetMenuText(tdExpandedGrowthStats.InnerHtml);
                tdExpandedPDFStats.InnerHtml = GetMenuText(tdExpandedPDFStats.InnerHtml);

                tdIAPendingItems.InnerHtml = hypNumItems.Text;
                tdViewUpdateIA.InnerHtml = GetMenuText(tdViewUpdateIA.InnerHtml);
                tdIAHarvestDash.InnerHtml = GetMenuText(tdIAHarvestDash.InnerHtml);
                tdViewUpdateBioStor.InnerHtml = GetMenuText(tdViewUpdateBioStor.InnerHtml);
                tdViewUpdateDoi.InnerHtml = GetMenuText(tdViewUpdateDoi.InnerHtml);

                tdRptReportingStats.InnerHtml = GetMenuText(tdRptReportingStats.InnerHtml);
                tdRptCharEncoding.InnerHtml = GetMenuText(tdRptCharEncoding.InnerHtml);
                tdRptDoiByInstitution.InnerHtml = GetMenuText(tdRptDoiByInstitution.InnerHtml);
                tdRptMonoContributions.InnerHtml = GetMenuText(tdRptMonoContributions.InnerHtml);
                tdRptItemsByContributor.InnerHtml = GetMenuText(tdRptItemsByContributor.InnerHtml);
                tdRptRecentlyClustered.InnerHtml = GetMenuText(tdRptRecentlyClustered.InnerHtml);
                tdRptOrphan.InnerHtml = GetMenuText(tdRptOrphan.InnerHtml);
                tdDLExtContent.InnerHtml = GetMenuText(tdDLExtContent.InnerHtml);

                tdImportCitations.InnerHtml = GetMenuText(tdImportCitations.InnerHtml);
                tdImportCitationHistory.InnerHtml = GetMenuText(tdImportCitationHistory.InnerHtml);
                tdImportItemText.InnerHtml = GetMenuText(tdImportItemText.InnerHtml);
                tdImportTextHistory.InnerHtml = GetMenuText(tdImportTextHistory.InnerHtml);
            }
            if (!Helper.IsUserAuthorized(new HttpRequestWrapper(request), Helper.SecurityRole.BHLAdminUserAdvanced))
            {
                tdAlertMessage.InnerHtml = GetMenuText(tdAlertMessage.InnerHtml);
                tdApiV2Stats.InnerHtml = apiv2StatsLink.InnerText;
                tdApiV3Stats.InnerHtml = apiv3StatsLink.InnerText;
                tdOpenUrlStats.InnerHtml = openurlStatsLink.InnerText;
                tdUserAccounts.InnerHtml = GetMenuText(tdUserAccounts.InnerHtml);
            }
            if (!Helper.IsUserAuthorized(new HttpRequestWrapper(request), Helper.SecurityRole.BHLAdminSysAdmin))
            {
                spnMonitor.Visible = false;
                //tdImageServer.InnerHtml = GetMenuText(tdImageServer.InnerHtml);
                //tdVaults.InnerHtml = GetMenuText(tdVaults.InnerHtml);
            }
        }

        protected string GetMenuText(string html)
        {
            string cleanHtml = html.Replace("\n", "").Replace("\r", "").Replace("\t", "");
            return System.Text.RegularExpressions.Regex.Replace(cleanHtml, "(<[a|A][^>]*>|</[a|A]>)", "");
        }
    }
}
