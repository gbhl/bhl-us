using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;
using MOBOT.BHL.Utility;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.DataObjects
{
    public class TitleNoteComparer : System.Collections.IComparer
    {
		public enum CompareEnum
		{
			NoteText,
			NoteSequence
		}

		private CompareEnum _compareEnum;
		private SortOrder _sortOrder;

		public TitleNoteComparer( CompareEnum compareEnum, SortOrder sortOrder )
		{
			_compareEnum = compareEnum;
			_sortOrder = sortOrder;
		}

        public int Compare(object obj1, object obj2)
        {
            TitleNote note1 = (TitleNote)obj1;
            TitleNote note2 = (TitleNote)obj2;

            int ret = 0;

            switch (_compareEnum)
            {
                case CompareEnum.NoteText:
                    {
                        ret = TypeHelper.EmptyIfNull(note1.NoteText).CompareTo(
                            TypeHelper.EmptyIfNull(note2.NoteText));
                        break;
                    }
                case CompareEnum.NoteSequence:
                    {
                        ret = (TypeHelper.ZeroIfNull((int?)note1.NoteSequence)).CompareTo(
                            (TypeHelper.ZeroIfNull((int?)note2.NoteSequence)));
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
