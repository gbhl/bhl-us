using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    /*
    public class MonthlyStatsComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            InstitutionYearMonth
        }

        private CompareEnum _compareEnum;
        private SortOrder _sortOrder;

        public MonthlyStatsComparer(CompareEnum compareEnum, SortOrder sortOrder)
        {
            _compareEnum = compareEnum;
            _sortOrder = sortOrder;
        }

        public int Compare(object obj1, object obj2)
        {
            MonthlyStats page1 = (MonthlyStats)obj1;
            MonthlyStats page2 = (MonthlyStats)obj2;

            int ret = 0;
            bool institutionNameChanged = false;

            switch (_compareEnum)
            {
                case CompareEnum.InstitutionYearMonth:
                    {
                        ret = page1.InstitutionName.CompareTo(page2.InstitutionName);
                        institutionNameChanged = (ret != 0);
                        if (ret == 0) ret = page1.Year.CompareTo(page2.Year);
                        if (ret == 0) ret = page1.Month.CompareTo(page2.Month);
                        break;
                    }
            }

            // Institution Name always sorts Ascending.  Year and Month can be either sort order.
            if (_sortOrder == SortOrder.Descending && !institutionNameChanged)
            {
                ret = ret * -1;
            }

            return ret;
        }
    }
    */

    public class MonthlyStatsAscComparer : IComparer<MonthlyStats>
    {
        public int Compare(MonthlyStats x, MonthlyStats y)
        {
            int ret = x.InstitutionName.CompareTo(y.InstitutionName);
            bool institutionNameChanged = (ret != 0);
            if (ret == 0) ret = x.Year.CompareTo(y.Year);
            if (ret == 0) ret = x.Month.CompareTo(y.Month);
            return ret;
        }
    }

    public class MonthlyStatsDescComparer : IComparer<MonthlyStats>
    {
        public int Compare(MonthlyStats x, MonthlyStats y)
        {
            int ret = x.InstitutionName.CompareTo(y.InstitutionName);
            bool institutionNameChanged = (ret != 0);
            if (ret == 0) ret = x.Year.CompareTo(y.Year);
            if (ret == 0) ret = x.Month.CompareTo(y.Month);
            if (!institutionNameChanged) ret = ret * -1;
            return ret;
        }
    }    
}
