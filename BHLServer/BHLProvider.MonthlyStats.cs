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
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectCurrentYearSummary(null, null);
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectCurrentMonthSummary()
        {
            return GetMonthlyStatsDalInstance().MonthlyStatsSelectCurrentMonthSummary(null, null);
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
