using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<BSSegment> BSSegmentSelectByItem(int itemId)
        {
            return new BSSegmentDAL().BSSegmentSelectByItem(null, null, itemId);
        }

        public void InsertSegment(BSSegment segment, List<BSSegmentAuthor> authors)
        {
            new BSSegmentDAL().Save(null, null, segment, authors);
        }

        public List<BSSegment> SelectSegmentsForPublishing(int itemID)
        {
            return new BSSegmentDAL().BSSegmentSelectHarvestedByItem(null, null, itemID);
        }

        public void ResolveSegmentAuthors(int segmentID)
        {
            new BSSegmentDAL().BSSegmentResolveAuthors(null, null, segmentID);
        }

        public int PublishSegment(int itemID, int segmentID)
        {
            int newStatusID = int.MinValue;
            new BSSegmentDAL().BSSegmentPublishToProduction(null, null, itemID, segmentID, out newStatusID);
            return newStatusID;
        }
    }
}
