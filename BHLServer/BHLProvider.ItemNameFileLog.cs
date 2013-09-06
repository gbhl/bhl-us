using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public void ItemNameFileLogRefreshSinceDate(DateTime startDate)
        {
            new ItemNameFileLogDAL().ItemNameFileLogRefreshSinceDate(null, null, startDate);
        }

        public CustomGenericList<ItemNameFileLog> ItemNameFileLogSelectForCreate()
        {
            return new ItemNameFileLogDAL().ItemNameFileLogSelectForCreate(null, null);
        }

        public CustomGenericList<ItemNameFileLog> ItemNameFileLogSelectForUpload()
        {
            return new ItemNameFileLogDAL().ItemNameFileLogSelectForUpload(null, null);
        }

        public void ItemNameFileLogUpdateCreateDate(int logID)
        {
            ItemNameFileLogDAL dal = new ItemNameFileLogDAL();
            ItemNameFileLog log = dal.ItemNameFileLogSelectAuto(null, null, logID);
            if (log != null)
            {
                log.DoCreate = false;
                log.LastCreateDate = DateTime.Now;
                dal.ItemNameFileLogUpdateAuto(null, null, log);
            }
            else
            {
                throw new Exception("Could not find existing ItemNameFileLog record.");
            }
        }

        public void ItemNameFileLogUpdateUploadDate(int logID)
        {
            ItemNameFileLogDAL dal = new ItemNameFileLogDAL();
            ItemNameFileLog log = dal.ItemNameFileLogSelectAuto(null, null, logID);
            if (log != null)
            {
                log.DoUpload = false;
                log.LastUploadDate = DateTime.Now;
                dal.ItemNameFileLogUpdateAuto(null, null, log);
            }
            else
            {
                throw new Exception("Could not find existing ItemNameFileLog record.");
            }
        }
    }
}
