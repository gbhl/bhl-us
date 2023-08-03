using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<IASet> IASetSelectForDownload()
        {
            return (new IASetDAL().IASetSelectForDownload(null, null));
        }

        public IASet IASetUpdateLastDownloadDate(int setID, bool updateFullDownloadDate)
        {
            IASetDAL dal = new IASetDAL();
            IASet savedSet = dal.IASetSelectAuto(null, null, setID);
            if (savedSet != null)
            {
                savedSet.LastDownloadDate = DateTime.Now;
                if (updateFullDownloadDate) savedSet.LastFullDownloadDate = savedSet.LastDownloadDate;
                savedSet = dal.IASetUpdateAuto(null, null, savedSet);
            }
            else
            {
                throw new Exception("Could not find existing Set record.");
            }
            return savedSet;
        }

        public IASet SaveIASet(string setSpecification)
        {
            IASetDAL dal = new IASetDAL();
            IASet savedSet = dal.IASetSelectBySetSpecification(null, null, setSpecification);
            if (savedSet == null)
            {
                // Add the new set
                IASet newSet = new IASet
                {
                    SetSpecification = setSpecification,
                    DownloadAll = false
                };
                savedSet = dal.IASetInsertAuto(null, null, newSet);
            }
            return savedSet;
        }
    }
}
