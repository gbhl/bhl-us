using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        private MonthlyStatsDAL monthlyStatsDal = null;

        public List<MonthlyStats> MonthlyStatsSelectCurrentYearSummary()
        {
            int year = DateTime.Today.Year;
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummary(null, null, year, 0);
        }

        public List<MonthlyStats> MonthlyStatsSelectCurrentMonthSummary()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummary(null, null, year, month);
        }

        public List<MonthlyStats> MonthlyStatsSelectPreviousMonthSummary()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month - 1;
            if (month == 0) { year--; month = 1; }
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummary(null, null, year, month);
        }

        public List<MonthlyStats> MonthlyStatsSelectByDateAndInstitution(int startYear,
            int startMonth, int endYear, int endMonth, String institutionCode)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectByDateAndInstitution(null, null, 
                startYear, startMonth, endYear, endMonth, institutionCode);
        }

        public List<MonthlyStats> MonthlyStatsSelectByInstitution(string institutionCode)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectByInstitution(null, null, institutionCode);
        }

        public List<MonthlyStats> MonthlyStatsSelectDetailedForGroup()
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectDetailedForGroup(null, null);
        }

        public List<MonthlyStats> MonthlyStatsSelectDetailed(bool bhlMemberLibraryOnly)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectDetailed(null, null, bhlMemberLibraryOnly);
        }

        public List<MonthlyStats> MonthlyStatsSelectSummaryStats(bool bhlMemberLibraryOnly)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummaryStats(null, null, bhlMemberLibraryOnly);
        }

        private MonthlyStatsDAL GetMonthlyStatsDalInstance()
        {
            if (monthlyStatsDal == null)
                monthlyStatsDal = new MonthlyStatsDAL();

            return monthlyStatsDal;
        }
    }
}
