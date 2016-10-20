﻿using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class ReportingStatsModel
    {
        public int ContributorTarget { get; set; }
        public string SortOrder { get; set; }
        public string ContributorCode { get; set; }
        public CustomGenericList<Institution> Institutions { get; set; }
        public List<ReportStat> ReportStats { get; set; }
        public byte[] DownloadStats { get; set; }

        public ReportingStatsModel()
        {
            ContributorCode = string.Empty;
            ReportStats = new List<ReportStat>();
            Institutions = new BHLProvider().InstituationSelectAll();
            Institutions.Insert(0, new Institution { InstitutionCode = "", InstitutionName = "" });
        }

        public void GetStats()
        {
            CustomGenericList<MonthlyStats> stats = null;

            switch (ContributorTarget)
            {
                case 1:
                    stats = GetDetailedStats(false);
                    break;
                case 2:
                    stats = GetSummaryStats(false);
                    break;
                case 3:
                    stats = GetDetailedStats(true);
                    break;
                case 4:
                    stats = GetSummaryStats(true);
                    break;
                case 5:
                    stats = GetStatsForInstitution(ContributorCode ?? string.Empty);
                    break;
            }

            if (string.Compare(SortOrder, "desc", true) == 0) stats = SortStats(stats, CustomDataAccess.SortOrder.Descending);

            ReportStats = ConvertToReportStat(stats);
        }

        public void GetCSV()
        {
            // Get the data to be formatted as CSV
            GetStats();

            StringBuilder sb = new StringBuilder();

            // Add CSV header
            if (this.ContributorTarget == 2 || this.ContributorTarget == 4)
            {
                sb.AppendLine("\"Institution\",\"Year\",\"Month\",\"New Items\",\"New Pages\",\"New Names\",\"New Segments\",\"New PDFs\",\"New DOIs\",\"Total Items\",\"Total Pages\",\"Total Names\",\"Total Segments\",\"Total PDFs\",\"Total DOIs\"");
            }
            else
            {
                sb.AppendLine("\"Institution\",\"Year\",\"Month\",\"New Items\",\"New Pages\",\"New Names\",\"New Segments\",\"Total Items\",\"Total Pages\",\"Total Names\",\"Total Segments\"");
            }

            // Add CSV data
            foreach (ReportStat reportStat in ReportStats)
            {
                sb.Append("\"" + reportStat.InstitutionName.Replace("\"", "'") + "\"");
                sb.Append(",\"" + reportStat.Year + "\"");
                sb.Append(",\"" + reportStat.Month + "\"");
                sb.Append(",\"" + reportStat.Items + "\"");
                sb.Append(",\"" + reportStat.Pages + "\"");
                sb.Append(",\"" + reportStat.Names + "\"");
                sb.Append(",\"" + reportStat.Segments + "\"");
                if (this.ContributorTarget == 2 || this.ContributorTarget == 4)
                {
                    sb.Append(",\"" + reportStat.PDFs + "\"");
                    sb.Append(",\"" + reportStat.DOIs + "\"");
                }
                sb.Append(",\"" + reportStat.TotalItems + "\"");
                sb.Append(",\"" + reportStat.TotalPages + "\"");
                sb.Append(",\"" + reportStat.TotalNames + "\"");
                sb.Append(",\"" + reportStat.TotalSegments + "\"");
                if (this.ContributorTarget == 2 || this.ContributorTarget == 4)
                {
                    sb.Append(",\"" + reportStat.TotalPDFs + "\"");
                    sb.Append(",\"" + reportStat.TotalDOIs + "\"");
                }
                sb.AppendLine();
            }

            // Convert CSV to byte array
            DownloadStats = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        }

        private CustomGenericList<MonthlyStats> GetStatsForInstitution(string institutionCode)
        {
            return new BHLProvider().MonthlyStatsSelectByInstitution(institutionCode);
        }

        private CustomGenericList<MonthlyStats> GetDetailedStats(bool bhlMemberLibrary)
        {
            return new BHLProvider().MonthlyStatsSelectDetailed(bhlMemberLibrary);
        }

        private CustomGenericList<MonthlyStats> GetSummaryStats(bool bhlMemberLibrary)
        {
            return new BHLProvider().MonthlyStatsSelectSummaryStats(bhlMemberLibrary);
        }

        private List<ReportStat> ConvertToReportStat(CustomGenericList<MonthlyStats> stats)
        {
            List<ReportStat> reportStats = new List<ReportStat>();

            ReportStat reportStat = null;
            string prevInstitution = string.Empty;
            int prevYear = -1;
            int prevMonth = -1;
            foreach(MonthlyStats stat in stats)
            {
                if (prevInstitution != stat.InstitutionName ||
                    prevYear != stat.Year ||
                    prevMonth != stat.Month)
                {
                    if (reportStat != null) reportStats.Add(reportStat);
                    reportStat = new ReportStat();
                    reportStat.InstitutionName = GetReportStatInstitutionName(stat.InstitutionName);
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

                prevInstitution = stat.InstitutionName;
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
                switch (ContributorTarget)
                {
                    case 2:
                        institutionName = "All Contributors";
                        break;
                    case 4:
                        institutionName = "BHL Member Contributors";
                        break;
                }
            }

            return institutionName;
        }

        private CustomGenericList<MonthlyStats> SortStats(CustomGenericList<MonthlyStats> stats,
            CustomDataAccess.SortOrder sortOrder)
        {
            MonthlyStatsComparer comp = new MonthlyStatsComparer(
                MonthlyStatsComparer.CompareEnum.InstitutionYearMonth, sortOrder);
            stats.Sort(comp);

            return stats;
        }

        public class ReportStat
        {
            public string InstitutionName { get; set; }
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