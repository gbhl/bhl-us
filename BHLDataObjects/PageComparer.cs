using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
	public class PageComparer : System.Collections.IComparer
	{
		public enum CompareEnum
		{
			PageID,
			FileNamePrefix,
			SequenceOrder
		}

		private CompareEnum _compareEnum;
		private SortOrder _sortOrder;

		public PageComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

		public int Compare( object obj1, object obj2 )
		{
			Page page1 = (Page)obj1;
			Page page2 = (Page)obj2;

			int ret = 0;

			switch ( _compareEnum )
			{
				case CompareEnum.PageID:
					{
						ret = page1.PageID.CompareTo( page2.PageID );
						break;
					}
				case CompareEnum.FileNamePrefix:
					{
						ret = TypeHelper.EmptyIfNull( page1.FileNamePrefix ).CompareTo(
							TypeHelper.EmptyIfNull( page2.FileNamePrefix ) );
						break;
					}
				case CompareEnum.SequenceOrder:
					{
						ret = ( TypeHelper.ZeroIfNull( (int?)page1.SequenceOrder ) ).CompareTo(
							( TypeHelper.ZeroIfNull( (int?)page2.SequenceOrder ) ) );
						break;
					}
			}

			if ( _sortOrder == SortOrder.Descending )
			{
				ret = ret * -1;
			}

			return ret;
		}
	}
}
