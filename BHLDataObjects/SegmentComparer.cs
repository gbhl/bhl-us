using System;
using System.Collections.Generic;
using System.Text;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class SegmentComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            SequenceOrder,
            Title,
            Author,
            Year
        }

        private CompareEnum _compareEnum;
        private SortOrder _sortOrder;

		public SegmentComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

        #region IComparer Members

        public int Compare(object obj1, object obj2)
        {
            Segment item1 = (Segment)obj1;
            Segment item2 = (Segment)obj2;

            int ret = 0;

            switch (_compareEnum)
            {
                case CompareEnum.SequenceOrder:
                    {
                        ret = TypeHelper.ZeroIfNull((int)item1.SequenceOrder).CompareTo(
                            TypeHelper.ZeroIfNull((int)item2.SequenceOrder));
                        break;
                    }
                case CompareEnum.Title:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.SortTitle).CompareTo(
                            TypeHelper.EmptyIfNull(item2.SortTitle));
                        break;
                    }
                case CompareEnum.Author:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.Authors).CompareTo(
                            TypeHelper.EmptyIfNull(item2.Authors));
                        break;
                    }
                case CompareEnum.Year:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.Date).CompareTo(
                            TypeHelper.EmptyIfNull(item2.Date));
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
