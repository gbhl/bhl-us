using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ImportFileStatus> ImportFileStatusSelectAll()
        {
            return new ImportFileStatusDAL().SelectAll(null, null);
        }

        public ImportFile ImportFileSelectAuto(int importFileID)
        {
            return new ImportFileDAL().ImportFileSelectAuto(null, null, importFileID);
        }

        public ImportFile ImportFileSelectById(int importFileID)
        {
            return new ImportFileDAL().ImportFileSelectByID(null, null, importFileID);
        }

        public ImportFile ImportFileInsertAuto(int fileStatusID, string fileName, string contributor, int userID, int? segmentGenreID)
        {
            return new ImportFileDAL().ImportFileInsertAuto(null, null, fileStatusID, fileName, contributor, segmentGenreID, userID, userID);
        }

        public ImportFile ImportFileUpdateAuto(ImportFile importFile)
        {
            return new ImportFileDAL().ImportFileUpdateAuto(null, null, importFile);
        }

        public void ImportFileDelete(int importFileID)
        {
            new ImportFileDAL().ImportFileDeleteByImportFileID(null, null, importFileID);
        }

        public void ImportFilePublishToProduction(int importFileID, int userID)
        {
            new ImportFileDAL().ImportFilePublishToProduction(null, null, importFileID, userID);
        }

        public void ImportFileRejectFile(int importFileID, int userID)
        {
            new ImportFileDAL().ImportFileRejectFile(null, null, importFileID, userID);
        }

        public List<ImportFile> ImportFileSelectDetails(string userId, int fileStatusID, int numberOfDays)
        {
            return new ImportFileDAL().ImportFileSelectDetails(null, null, Convert.ToInt32(string.IsNullOrWhiteSpace(userId) ? "0" : userId), fileStatusID, numberOfDays);
        }

        public ImportFile ImportFileSelectByFileName(string importFileName)
        {
            return new ImportFileDAL().ImportFileSelectByFileName(null, null, importFileName);
        }
    }
}
