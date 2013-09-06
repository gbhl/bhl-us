using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
	public class NamePageComparer : System.Collections.IComparer
	{
		public enum CompareEnum
		{
			NameString,
            ResolvedNameString,
			NameBankID,
			IsActive
		}

		private CompareEnum _compareEnum;
		private SortOrder _sortOrder;

		public NamePageComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

		public int Compare( object obj1, object obj2 )
		{
            NamePage namePage1 = (NamePage)obj1;
            NamePage namePage2 = (NamePage)obj2;

			int ret = 0;

			switch ( _compareEnum )
			{
				case CompareEnum.IsActive:
					{
						ret = namePage1.IsActive.CompareTo( namePage2.IsActive );
						break;
					}
				case CompareEnum.NameString:
					{
						ret = TypeHelper.EmptyIfNull( namePage1.NameString ).CompareTo(
							TypeHelper.EmptyIfNull( namePage2.NameString ) );
						break;
					}
				case CompareEnum.ResolvedNameString:
					{
						ret = TypeHelper.EmptyIfNull( namePage1.ResolvedNameString ).CompareTo(
							TypeHelper.EmptyIfNull( namePage2.ResolvedNameString ) );
						break;
					}
				case CompareEnum.NameBankID:
					{
						ret = ( TypeHelper.EmptyIfNull ( namePage1.NameBankID ) ).CompareTo(
							( TypeHelper.EmptyIfNull ( namePage2.NameBankID ) ) );
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
