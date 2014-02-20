using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public ImportRecordStatus ImportRecordStatusSelectAuto(int importRecordStatusID)
        {
            return new ImportRecordStatusDAL().ImportRecordStatusSelectAuto(null, null, importRecordStatusID);
        }

        public CustomGenericList<ImportRecordStatus> ImportRecordStatusSelectAll()
        {
            return new ImportRecordStatusDAL().SelectAll(null, null);
        }

        public CustomGenericList<ImportRecord> ImportRecordSelectByImportFileID(int importFileID, int numRows, int startRow,
            string sortColumn, string sortDirection)
        {
            return new ImportRecordDAL().ImportRecordSelectByImportFileID(null, null, importFileID, numRows, startRow,
                sortColumn, sortDirection);
        }

        public ImportRecord ImportRecordUpdateRecordStatus(int importRecordID, int importRecordStatusID, int userID)
		{
            ImportRecordDAL dal = new ImportRecordDAL();
			ImportRecord savedImportRecord = dal.ImportRecordSelectAuto(null, null, importRecordID);
			if ( savedImportRecord != null )
			{
				savedImportRecord.ImportRecordStatusID = importRecordStatusID;
                savedImportRecord.LastModifiedDate = DateTime.Now;
				savedImportRecord.LastModifiedUserID = userID;
				savedImportRecord = dal.ImportRecordUpdateAuto(null, null, savedImportRecord);
			}
			else
			{
				throw new Exception( "Could not find existing ImportRecord." );
			}
			return savedImportRecord;
		}
    }
}
