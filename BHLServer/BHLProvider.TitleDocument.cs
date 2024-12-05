using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleDocument> TitleDocumentSelectByTitleID(int titleId)
        {
            return new TitleDocumentDAL().TitleDocumentSelectByTitleID(null, null, titleId);
        }

        public List<TitleDocument> TitleDocumentSelectByBookID(int bookId)
        {
            return new TitleDocumentDAL().TitleDocumentSelectByBookID(null, null, bookId);
        }

        public List<TitleDocument> TitleDocumentSelectBySegmentID(int segmentId)
        {
            return new TitleDocumentDAL().TitleDocumentSelectBySegmentID(null, null, segmentId);
        }
    }
}
