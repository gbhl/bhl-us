using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        private MonthlyStatsDAL monthlyStatsDal = null;

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectByStatType(string statType, string institutionName, bool showMonthly)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectByStatType(null, null, statType, institutionName, showMonthly);
        }

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
            int startMonth, int endYear, int endMonth, String institutionName)
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectByDateAndInstitution(null, null, 
                startYear, startMonth, endYear, endMonth, institutionName);
        }

        private MonthlyStatsDAL GetMonthlyStatsDalInstance()
        {
            if (monthlyStatsDal == null)
                monthlyStatsDal = new MonthlyStatsDAL();

            return monthlyStatsDal;
        }
    }
}
