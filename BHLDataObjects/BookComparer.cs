using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
	public class BookComparer : System.Collections.IComparer
	{
		public enum CompareEnum
		{
			BookID,
			BarCode,
			ItemSequence,
			Volume,
			PaginationStatusName,
			PaginationStatusDate
		}

		private CompareEnum _compareEnum;
		private SortOrder _sortOrder;

		public BookComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

		public int Compare( object obj1, object obj2 )
		{
			Book item1 = (Book)obj1;
			Book item2 = (Book)obj2;

			int ret = 0;

			switch ( _compareEnum )
			{
				case CompareEnum.BookID:
					{
						ret = item1.BookID.CompareTo( item2.BookID);
						break;
					}
				case CompareEnum.BarCode:
					{
						ret = TypeHelper.EmptyIfNull( item1.BarCode ).CompareTo(
							TypeHelper.EmptyIfNull( item2.BarCode ) );
						break;
					}
				case CompareEnum.ItemSequence:
					{
						ret = ( TypeHelper.ZeroIfNull( (int?)item1.ItemSequence ) ).CompareTo(
							( TypeHelper.ZeroIfNull( (int?)item2.ItemSequence ) ) );
						break;
					}
				case CompareEnum.Volume:
					{
						ret = TypeHelper.EmptyIfNull( item1.Volume ).CompareTo(
							TypeHelper.EmptyIfNull( item2.Volume ) );
						break;
					}
				case CompareEnum.PaginationStatusName:
					{
						ret = TypeHelper.EmptyIfNull( item1.PaginationStatusName ).CompareTo(
							TypeHelper.EmptyIfNull( item2.PaginationStatusName ) );
						break;
					}
				case CompareEnum.PaginationStatusDate:
					{
						ret = TypeHelper.MinDateIfNull( item1.PaginationStatusDate ).CompareTo(
							TypeHelper.MinDateIfNull( item2.PaginationStatusDate ) );
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
