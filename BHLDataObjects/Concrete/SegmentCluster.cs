
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class SegmentCluster : __SegmentCluster
	{
	}

    /// <summary>
    /// Possible types for each segment cluster.
    /// </summary>
    /// <remarks>
    /// Corresponds to the key values of the SegmentClusterType database table.
    /// </remarks>
    public enum SegmentClusterTypes : int
    {
        SameAs = 10,
        Continued = 20,
        Related = 30,
        PartOf = 40
    }
}
