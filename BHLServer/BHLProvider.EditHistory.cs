using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<EditHistory> EditHistorySelectByTitleID(int titleID)
        {
            return new EditHistoryDAL().EditHistorySelectByTitleID(null, null, titleID);
        }

        public CustomGenericList<EditHistory> EditHistorySelectByItemID(int itemID)
        {
            return new EditHistoryDAL().EditHistorySelectByItemID(null, null, itemID);
        }

        public CustomGenericList<EditHistory> EditHistorySelectBySegmentID(int segmentID)
        {
            return new EditHistoryDAL().EditHistorySelectBySegmentID(null, null, segmentID);
        }

        public CustomGenericList<EditHistory> EditHistorySelectByAuthorID(int authorID)
        {
            return new EditHistoryDAL().EditHistorySelectByAuthorID(null, null, authorID);
        }

        public CustomGenericList<EditHistory> EditHistorySelectByEntityAndID(string entitySchema, string entityName, string entityID)
        {
            return new EditHistoryDAL().EditHistorySelectByEntityAndID(null, null, entitySchema, entityName, entityID);
        }

        public CustomGenericList<EditHistory> EditHistorySelectNameByPageID(int pageID)
        {
            return new EditHistoryDAL().EditHistorySelectNameByPageID(null, null, pageID);
        }

        public CustomGenericList<EditHistory> EditHistorySelectPageByItemID(int itemID)
        {
            return new EditHistoryDAL().EditHistorySelectPageByItemID(null, null, itemID);
        }
    }
}
