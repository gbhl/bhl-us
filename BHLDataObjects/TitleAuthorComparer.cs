using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class TitleAuthorComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            SequenceOrder,
            FullName
        }

        private CompareEnum _compareEnum;
        private SortOrder _sortOrder;

        public TitleAuthorComparer(CompareEnum compareEnum, SortOrder sortOrder)
        {
            _compareEnum = compareEnum;
            _sortOrder = sortOrder;
        }

        #region IComparer Members

        public int Compare(object obj1, object obj2)
        {
            TitleAuthor item1 = (TitleAuthor)obj1;
            TitleAuthor item2 = (TitleAuthor)obj2;

            int ret = 0;

            switch (_compareEnum)
            {
                case CompareEnum.SequenceOrder:
                    {
                        ret = TypeHelper.ZeroIfNull((int)item1.SequenceOrder).CompareTo(
                            TypeHelper.ZeroIfNull((int)item2.SequenceOrder));
                        break;
                    }
                case CompareEnum.FullName:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.FullName).CompareTo(
                            TypeHelper.EmptyIfNull(item2.FullName));
                        break;
                    }
            }

            if (_sortOrder == SortOrder.Descending)
            {
                ret = ret * -1;
            }

            return ret;
        }

        #endregion
    }
}
