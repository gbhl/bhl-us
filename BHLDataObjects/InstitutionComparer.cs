using MOBOT.BHL.Utility;

namespace MOBOT.BHL.DataObjects
{
    /*
    public class InstitutionComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            InstitutionCode,
            InstitutionName
        }

        private CompareEnum _compareEnum;
        private SortOrder _sortOrder;

        public InstitutionComparer(CompareEnum compareEnum, SortOrder sortOrder)
        {
            _compareEnum = compareEnum;
            _sortOrder = sortOrder;
        }

        public int Compare(object obj1, object obj2)
        {
            Institution item1 = (Institution)obj1;
            Institution item2 = (Institution)obj2;

            int ret = 0;

            switch (_compareEnum)
            {
                case CompareEnum.InstitutionCode:
                    {
                        ret = item1.InstitutionCode.CompareTo(item2.InstitutionCode);
                        break;
                    }
                case CompareEnum.InstitutionName:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.InstitutionName).CompareTo(
                            TypeHelper.EmptyIfNull(item2.InstitutionName));
                        break;
                    }
            }

            if (_sortOrder == SortOrder.Descending)
            {
                ret = ret * -1;
            }

            return ret;
        }
    }
    */

    public class InstitutionCodeComparer : System.Collections.Generic.IComparer<Institution>
    {
        public int Compare(Institution x, Institution y)
        {
            int ret = TypeHelper.EmptyIfNull(x.InstitutionCode).CompareTo(
                TypeHelper.EmptyIfNull(y.InstitutionCode));
            return ret;
        }
    }

    public class InstitutionNameComparer : System.Collections.Generic.IComparer<Institution>
    {
        public int Compare(Institution x, Institution y)
        {
            int ret = TypeHelper.EmptyIfNull(x.InstitutionName).CompareTo(
                TypeHelper.EmptyIfNull(y.InstitutionName));
            return ret;
        }
    }
}
