using System;
using System.Collections.Generic;
using System.Text;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class SegmentAuthorComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            SequenceOrder,
            FullName
        }

        private CompareEnum _compareEnum;
        private SortOrder _sortOrder;

		public SegmentAuthorComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

        #region IComparer Members

        public int Compare(object obj1, object obj2)
        {
            SegmentAuthor item1 = (SegmentAuthor)obj1;
            SegmentAuthor item2 = (SegmentAuthor)obj2;

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
