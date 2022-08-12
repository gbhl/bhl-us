using MOBOT.BHL.Utility;
using System.Collections.Generic;

namespace MOBOT.BHL.DataObjects
{
    public class ItemAuthorNameComparer : IComparer<ItemAuthor>
    {
        public int Compare(ItemAuthor x, ItemAuthor y)
        {
            int ret = TypeHelper.EmptyIfNull(x.FullName).CompareTo(
                TypeHelper.EmptyIfNull(y.FullName));
            return ret;
        }
    }

    public class ItemAuthorSequenceComparer : IComparer<ItemAuthor>
    {
        public int Compare(ItemAuthor x, ItemAuthor y)
        {
            int ret = TypeHelper.ZeroIfNull((int)x.SequenceOrder).CompareTo(
                TypeHelper.ZeroIfNull((int)y.SequenceOrder));
            return ret;
        }
    }
}
