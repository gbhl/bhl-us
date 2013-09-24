
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class SegmentStatus : __SegmentStatus
	{
	}

    public enum SegmentStatusValue : int
    {
        New = 10,
        Published = 20,
        Removed = 30,
        Inappropriate = 40
    }
}
