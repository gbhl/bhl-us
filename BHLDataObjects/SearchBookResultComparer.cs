using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class SearchBookResultComparer : System.Collections.IComparer
    {
        public enum CompareEnum
        {
            Title,
            Author,
            Year
        }

		private CompareEnum _compareEnum;
		private SortOrder _sortOrder;

		public SearchBookResultComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

        #region IComparer Members

        public int Compare(object x, object y)
        {
            SearchBookResult item1 = (SearchBookResult)x;
            SearchBookResult item2 = (SearchBookResult)y;

            int ret = 0;

            switch (_compareEnum)
            {
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
                        ret = TypeHelper.EmptyIfNull(item1.Year).CompareTo(
                            TypeHelper.EmptyIfNull(item2.Year));
                        break;
                    }
            }

            if (_sortOrder == SortOrder.Descending) ret = ret * -1;

            return ret;
        }

        #endregion
    }
}
