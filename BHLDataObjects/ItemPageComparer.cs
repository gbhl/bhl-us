using MOBOT.BHL.Utility;
using System.Collections.Generic;

namespace MOBOT.BHL.DataObjects
{
    public class ItemPageSequenceComparer : IComparer<ItemPage>
    {
        public int Compare(ItemPage x, ItemPage y)
        {
            int ret = TypeHelper.ZeroIfNull((int)x.SequenceOrder).CompareTo(
                TypeHelper.ZeroIfNull((int)y.SequenceOrder));
            return ret;
        }
    }

    public class ItemPagePSequenceComparer : IComparer<ItemPage>
    {
        public int Compare(ItemPage x, ItemPage y)
        {
            int ret = TypeHelper.ZeroIfNull((int)x.PageSequenceOrder).CompareTo(
                TypeHelper.ZeroIfNull((int)y.PageSequenceOrder));
            return ret;
        }
    }
}
