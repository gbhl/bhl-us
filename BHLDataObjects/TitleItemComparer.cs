using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class TitleItemComparer : System.Collections.IComparer
    {
		public enum CompareEnum
		{
			ItemID,
			BarCode,
			ItemSequence,
			Volume
		}

		private CompareEnum _compareEnum;
		private SortOrder _sortOrder;

		public TitleItemComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

        public int Compare(object obj1, object obj2)
        {
            TitleItem item1 = (TitleItem)obj1;
            TitleItem item2 = (TitleItem)obj2;

            int ret = 0;

            switch (_compareEnum)
            {
                case CompareEnum.ItemID:
                    {
                        ret = item1.ItemID.CompareTo(item2.ItemID);
                        break;
                    }
                case CompareEnum.BarCode:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.BarCode).CompareTo(
                            TypeHelper.EmptyIfNull(item2.BarCode));
                        break;
                    }
                case CompareEnum.ItemSequence:
                    {
                        ret = (TypeHelper.ZeroIfNull((int?)item1.ItemSequence)).CompareTo(
                            (TypeHelper.ZeroIfNull((int?)item2.ItemSequence)));
                        break;
                    }
                case CompareEnum.Volume:
                    {
                        ret = TypeHelper.EmptyIfNull(item1.Volume).CompareTo(
                            TypeHelper.EmptyIfNull(item2.Volume));
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
}
