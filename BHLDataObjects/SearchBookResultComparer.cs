using MOBOT.BHL.Utility;

namespace MOBOT.BHL.DataObjects
{
    /*
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
    */

    public class SearchBookResultTitleComparer : System.Collections.Generic.IComparer<SearchBookResult>
    {
        public int Compare(SearchBookResult x, SearchBookResult y)
        {
            int ret = TypeHelper.EmptyIfNull(x.SortTitle).CompareTo(
                TypeHelper.EmptyIfNull(y.SortTitle));
            return ret;
        }
    }

    public class SearchBookResultAuthorComparer : System.Collections.Generic.IComparer<SearchBookResult>
    {
        public int Compare(SearchBookResult x, SearchBookResult y)
        {
            int ret = TypeHelper.EmptyIfNull(x.Authors).CompareTo(
                TypeHelper.EmptyIfNull(y.Authors));
            return ret;
        }
    }

    public class SearchBookResultYearComparer : System.Collections.Generic.IComparer<SearchBookResult>
    {
        public int Compare(SearchBookResult x, SearchBookResult y)
        {
            int ret = TypeHelper.EmptyIfNull(x.Year).CompareTo(
                TypeHelper.EmptyIfNull(y.Year));
            return ret;
        }
    }
}
