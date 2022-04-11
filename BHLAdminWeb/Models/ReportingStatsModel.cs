using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class ReportingStatsModel
    {
        public int ContentProviderTarget { get; set; }
        public string SortOrder { get; set; }
        public string InstitutionCode { get; set; }
        public List<Institution> Institutions { get; set; }
        public List<ReportStat> ReportStats { get; set; }
        public byte[] DownloadStats { get; set; }

        public ReportingStatsModel()
        {
            InstitutionCode = string.Empty;
            ReportStats = new List<ReportStat>();
            Institutions = new BHLProvider().InstituationSelectAll();
            Institutions.Insert(0, new Institution { InstitutionCode = "", InstitutionName = "" });
        }

        public void GetStats()
        {
            List<MonthlyStats> stats = null;

            switch (ContentProviderTarget)
            {
                case 1:
                    stats = GetDetailedStats(false);
                    break;
                case 2:
                    stats = GetSummaryStats(false);
                    break;
                case 3:
                    stats = GetGroupStats();
                    break;
                case 4:
                    stats = GetDetailedStats(true);
                    break;
                case 5:
                    stats = GetSummaryStats(true);
                    break;
                case 6:
                    stats = GetStatsForInstitution(InstitutionCode ?? string.Empty);
                    break;
            }

            if (string.Compare(SortOrder, "desc", true) == 0) stats = SortStats(stats, CustomDataAccess.SortOrder.Descending);

            ReportStats = ConvertToReportStat(stats);
        }

        public void GetCSV()
        {
            // Get the data to be formatted as CSV
            GetStats();

            var data = new List<dynamic>();
            foreach (ReportStat reportStat in ReportStats)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;

                if (this.ContentProviderTarget == 3)
                    record.Add("Content Provider Group", reportStat.InstitutionName);
                else
                    record.Add("Content Provider", reportStat.InstitutionName);

                if (this.ContentProviderTarget == 1 || this.ContentProviderTarget == 4 || this.ContentProviderTarget == 6) record.Add("Content Provider Groups", reportStat.GroupNames);
                record.Add("Year", reportStat.Year);
                record.Add("Month", reportStat.Month);
                record.Add("New Items", reportStat.Items);
                record.Add("New Pages", reportStat.Pages);
                record.Add("New Names", reportStat.Names);
                record.Add("New Segments", reportStat.Segments);
                if (this.ContentProviderTarget == 2 || this.ContentProviderTarget == 5)
                {
                    record.Add("New PDFs", reportStat.PDFs);
                    record.Add("New DOIs", reportStat.DOIs);
                }
                record.Add("Total Items", reportStat.TotalItems);
                record.Add("Total Pages", reportStat.TotalPages);
                record.Add("Total Names", reportStat.TotalNames);
                record.Add("Total Segments", reportStat.TotalSegments);
                if (this.ContentProviderTarget == 2 || this.ContentProviderTarget == 5)
                {
                    record.Add("Total PDFs", reportStat.TotalPDFs);
                    record.Add("Total DOIs", reportStat.TotalDOIs);
                }

                data.Add(record);
            }

            DownloadStats = new CSV().FormatCSVData(data);
        }

        private List<MonthlyStats> GetStatsForInstitution(string institutionCode)
        {
            return new BHLProvider().MonthlyStatsSelectByInstitution(institutionCode);
        }

        private List<MonthlyStats> GetGroupStats()
        {
            return new BHLProvider().MonthlyStatsSelectDetailedForGroup();
        }

        private List<MonthlyStats> GetDetailedStats(bool bhlMemberLibrary)
        {
            return new BHLProvider().MonthlyStatsSelectDetailed(bhlMemberLibrary);
        }

        private List<MonthlyStats> GetSummaryStats(bool bhlMemberLibrary)
        {
            return new BHLProvider().MonthlyStatsSelectSummaryStats(bhlMemberLibrary);
        }

        private List<ReportStat> ConvertToReportStat(List<MonthlyStats> stats)
        {
            List<ReportStat> reportStats = new List<ReportStat>();

            ReportStat reportStat = null;
            string prevInstitution = string.Empty;
            int prevYear = -1;
            int prevMonth = -1;
            foreach(MonthlyStats stat in stats)
            {
                if (prevInstitution != stat.InstitutionCode ||
                    prevYear != stat.Year ||
                    prevMonth != stat.Month)
                {
                    if (reportStat != null) reportStats.Add(reportStat);
                    reportStat = new ReportStat();
                    reportStat.InstitutionName = GetReportStatInstitutionName(stat.InstitutionName);
                    reportStat.GroupNames = stat.InstitutionGroupNames;
                    reportStat.Year = stat.Year;
                    reportStat.Month = stat.Month;
                }

                switch (stat.StatType)
                {
                    case "Items Created":
                        reportStat.Items = stat.StatValue;
                        reportStat.TotalItems = stat.CumulativeValue;
                        break;
                    case "Pages Created":
                        reportStat.Pages = stat.StatValue;
                        reportStat.TotalPages = stat.CumulativeValue;
                        break;
                    case "PageNames Created":
                        reportStat.Names = stat.StatValue;
                        reportStat.TotalNames = stat.CumulativeValue;
                        break;
                    case "Segments Created":
                        reportStat.Segments = stat.StatValue;
                        reportStat.TotalSegments = stat.CumulativeValue;
                        break;
                    case "PDFs Created":
                        reportStat.PDFs = stat.StatValue;
                        reportStat.TotalPDFs = stat.CumulativeValue;
                        break;
                    case "DOIs Created":
                        reportStat.DOIs = stat.StatValue;
                        reportStat.TotalDOIs = stat.CumulativeValue;
                        break;
                }

                prevInstitution = stat.InstitutionCode;
                prevYear = stat.Year;
                prevMonth = stat.Month;
            }
            if (reportStat != null) reportStats.Add(reportStat);

            return reportStats;
        }

        private string GetReportStatInstitutionName(string institutionName)
        {
            if (string.IsNullOrWhiteSpace(institutionName))
            {
                switch (ContentProviderTarget)
                {
                    case 2:
                        institutionName = "All Content Providers";
                        break;
                    case 5:
                        institutionName = "BHL Partner Content Providers";
                        break;
                }
            }

            return institutionName;
        }

        private List<MonthlyStats> SortStats(List<MonthlyStats> stats,
            CustomDataAccess.SortOrder sortOrder)
        {
            IComparer<MonthlyStats> comp;
            if (sortOrder == CustomDataAccess.SortOrder.Ascending)
                comp = new MonthlyStatsAscComparer();
            else
                comp = new MonthlyStatsDescComparer();

            stats.Sort(comp);

            return stats;
        }

        public class ReportStat
        {
            public string InstitutionName { get; set; }
            public string GroupNames { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public int? Items { get; set; }
            public int? Pages { get; set; }
            public int? Names { get; set; }
            public int? Segments { get; set; }
            public int? PDFs { get; set; }
            public int? DOIs { get; set; }
            public int? TotalItems { get; set; }
            public int? TotalPages { get; set; }
            public int? TotalNames { get; set; }
            public int? TotalSegments { get; set; }
            public int? TotalPDFs { get; set; }
            public int? TotalDOIs { get; set; }
        }
    }
}