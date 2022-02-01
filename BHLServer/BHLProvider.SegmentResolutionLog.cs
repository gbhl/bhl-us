using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public SegmentResolutionLog SegmentResolutionLogInsertAuto(int segmentID, int matchingSegmentID, double score, int userID)
        {
            SegmentResolutionLog log = new SegmentResolutionLog();
            log.SegmentID = segmentID;
            log.MatchingSegmentID = matchingSegmentID;
            log.Score = (decimal?) score;
            log.CreationUserID = userID;
            log.LastModifiedUserID = userID;

            SegmentResolutionLogDAL dal = new SegmentResolutionLogDAL();
            log = dal.SegmentResolutionLogInsertAuto(null, null, log);
            return log;
        }
    }
}
