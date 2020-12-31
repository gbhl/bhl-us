using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<EditHistory> EditHistorySelectByTitleID(int titleID)
        {
            return new EditHistoryDAL().EditHistorySelectByTitleID(null, null, titleID);
        }

        public List<EditHistory> EditHistorySelectByItemID(int itemID)
        {
            return new EditHistoryDAL().EditHistorySelectByItemID(null, null, itemID);
        }

        public List<EditHistory> EditHistorySelectBySegmentID(int segmentID)
        {
            return new EditHistoryDAL().EditHistorySelectBySegmentID(null, null, segmentID);
        }

        public List<EditHistory> EditHistorySelectByAuthorID(int authorID)
        {
            return new EditHistoryDAL().EditHistorySelectByAuthorID(null, null, authorID);
        }

        public List<EditHistory> EditHistorySelectByEntityAndID(string entitySchema, string entityName, string entityID)
        {
            return new EditHistoryDAL().EditHistorySelectByEntityAndID(null, null, entitySchema, entityName, entityID);
        }

        public List<EditHistory> EditHistorySelectNameByPageID(int pageID)
        {
            return new EditHistoryDAL().EditHistorySelectNameByPageID(null, null, pageID);
        }

        public List<EditHistory> EditHistorySelectPageByItemID(int itemID)
        {
            return new EditHistoryDAL().EditHistorySelectPageByItemID(null, null, itemID);
        }
    }
}
