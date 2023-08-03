using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        /// <summary>
        /// Insert or update a Marc record for the specified item.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="leader"></param>
        /// <returns></returns>
        public IAMarc SaveIAMarc(int itemID, string leader)
        {
            IAMarcDAL dal = new IAMarcDAL();
            IAMarc savedMarc = dal.IAMarcSelectByItem(null, null, itemID);

            if (savedMarc == null)
            {
                IAMarc newMarc = new IAMarc
                {
                    ItemID = itemID,
                    Leader = leader
                };
                savedMarc = dal.IAMarcInsertAuto(null, null, newMarc);
            }
            else
            {
                if (savedMarc.Leader != leader)
                {
                    savedMarc.Leader = leader;
                    savedMarc = dal.IAMarcUpdateAuto(null, null, savedMarc);
                }
            }

            return savedMarc;
        }

        /// <summary>
        /// Delete the Marc record and all associated Marc control, data field, and
        /// sub field records for the specified item.
        /// </summary>
        /// <param name="itemID"></param>
        public void IAMarcDeleteAllByItem(int itemID)
        {
            IAMarcDAL dal = new IAMarcDAL();
            IAMarc savedMarc = dal.IAMarcSelectByItem(null, null, itemID);

            if (savedMarc != null)
            {
                new IAMarcControlDAL().IAMarcControlDeleteByMarcID(null, null, savedMarc.MarcID);
                new IAMarcDataFieldDAL().IAMarcDataFieldDeleteByMarcID(null, null, savedMarc.MarcID);
                dal.IAMarcDeleteAuto(null, null, savedMarc.MarcID);
            }
        }
    }
}
