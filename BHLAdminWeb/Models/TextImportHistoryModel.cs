using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class TextImportHistoryModel
    {
        private string _institution = string.Empty;

        public string Institution
        {
            get { return _institution; }
            set { _institution = value; }
        }

        private string _importBatchStatus = string.Empty;

        public string ImportBatchStatus
        {
            get { return _importBatchStatus; }
            set { _importBatchStatus = value; }
        }

        private string _reportDateRange = string.Empty;

        public string ReportDateRange
        {
            get { return _reportDateRange; }
            set { _reportDateRange = value; }
        }

        private CustomGenericList<TextImportBatch> _importBatchList = new CustomGenericList<TextImportBatch>();

        public CustomGenericList<TextImportBatch> ImportBatchList
        {
            get { return _importBatchList; }
            set { _importBatchList = value; }
        }

        public void GetImportBatchList()
        {
            int fileStatus = string.IsNullOrWhiteSpace(this.ImportBatchStatus) ? 0 : Convert.ToInt32(this.ImportBatchStatus);
            int numDays = string.IsNullOrWhiteSpace(this.ReportDateRange) ? 10000 : Convert.ToInt32(this.ReportDateRange);
            _importBatchList = new BHLProvider().TextImportBatchSelectDetails(fileStatus, numDays);
        }
    }
}