using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        private MonthlyStatsDAL monthlyStatsDal = null;

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectCurrentYearSummary()
        {
            int year = DateTime.Today.Year;
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummary(null, null, year, 0);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectCurrentMonthSummary()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummary(null, null, year, month);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectPreviousMonthSummary()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month - 1;
            if (month == 0) { year--; month = 1; }
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectSummary(null, null, year, month);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectByDateAndInstitution(int startYear,
            int startMonth, int endYear, int endMonth, String institutionCode)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectByDateAndInstitution(null, null, 
                startYear, startMonth, endYear, endMonth, institutionCode);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectByInstitution(string institutionCode)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectByInstitution(null, null, institutionCode);
        }

        public List<MonthlyStats> MonthlyStatsSelectDetailedForGroup()
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectDetailedForGroup(null, null);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectDetailed(bool bhlMemberLibraryOnly)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectDetailed(null, null, bhlMemberLibraryOnly);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectSummaryStats(bool bhlMemberLibraryOnly)
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
