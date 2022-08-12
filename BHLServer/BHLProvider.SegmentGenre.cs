using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<SegmentGenre> SegmentGenreSelectAll()
        {
            return (new SegmentGenreDAL().SegmentGenreSelectAll(null, null));
        }

        public SegmentGenre SegmentGenreSelectAuto(int segmentGenreID)
        {
            return (new SegmentGenreDAL().SegmentGenreSelectAuto(null, null, segmentGenreID));
        }

        public void SaveSegmentGenre(SegmentGenre segmentGenre)
        {
            new SegmentGenreDAL().Save(null, null, segmentGenre);
        }
    }
}
