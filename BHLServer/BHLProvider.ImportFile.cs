using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using System;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<ImportFileStatus> ImportFileStatusSelectAll()
        {
            return new ImportFileStatusDAL().SelectAll(null, null);
        }

        public ImportFile ImportFileSelectAuto(int importFileID)
        {
            return new ImportFileDAL().ImportFileSelectAuto(null, null, importFileID);
        }

        public ImportFile ImportFileInsertAuto(int fileStatusID, string fileName, string contributor, int userID)
        {
            return new ImportFileDAL().ImportFileInsertAuto(null, null, fileStatusID, fileName, contributor, userID, userID);
        }

        public ImportFile ImportFileUpdateAuto(ImportFile importFile)
        {
            return new ImportFileDAL().ImportFileUpdateAuto(null, null, importFile);
        }

        public void ImportFileDelete(int importFileID)
        {
            new ImportFileDAL().ImportFileDeleteByImportFileID(null, null, importFileID);
        }

        public void ImportRecordSave(ImportRecord citation, int userID)
        {
            citation.Year = DataCleaner.CleanYear(citation.Year);
            new ImportRecordDAL().ImportRecordSave(null, null, citation, userID);
        }

        public void ImportFilePublishToProduction(int importFileID, int userID)
        {
            new ImportFileDAL().ImportFilePublishToProduction(null, null, importFileID, userID);
        }

        public void ImportFileRejectFile(int importFileID, int userID)
        {
            new ImportFileDAL().ImportFileRejectFile(null, null, importFileID, userID);
        }

        public CustomGenericList<ImportFile> ImportFileSelectDetails(string institutionCode, int fileStatusID, int numberOfDays)
        {
            return new ImportFileDAL().ImportFileSelectDetails(null, null, institutionCode, fileStatusID, numberOfDays);
        }

        public ImportFile ImportFileSelectByFileName(string importFileName)
        {
            return new ImportFileDAL().ImportFileSelectByFileName(null, null, importFileName);
        }
    }
}
