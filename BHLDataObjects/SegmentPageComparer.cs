using System;
using System.Collections.Generic;
using System.Text;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class SegmentPageComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            SequenceOrder,
            PageSequenceOrder
        }

        private CompareEnum _compareEnum;
        private SortOrder _sortOrder;

		public SegmentPageComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

        #region IComparer Members

        public int Compare(object obj1, object obj2)
        {
            SegmentPage item1 = (SegmentPage)obj1;
            SegmentPage item2 = (SegmentPage)obj2;

            int ret = 0;

            switch (_compareEnum)
            {
                case CompareEnum.SequenceOrder:
                    {
                        ret = TypeHelper.ZeroIfNull((int)item1.SequenceOrder).CompareTo(
                            TypeHelper.ZeroIfNull((int)item2.SequenceOrder));
                        break;
                    }
                case CompareEnum.PageSequenceOrder:
                    {
                        ret = TypeHelper.ZeroIfNull((int)item1.PageSequenceOrder).CompareTo(
                            TypeHelper.ZeroIfNull((int)item2.PageSequenceOrder));
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
