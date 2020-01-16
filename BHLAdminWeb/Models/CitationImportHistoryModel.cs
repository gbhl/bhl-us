using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class CitationImportHistoryModel
    {
        private string _institution = string.Empty;

        public string Institution
        {
            get { return _institution; }
            set { _institution = value; }
        }

        private string _importFileStatus = string.Empty;

        public string ImportFileStatus
        {
            get { return _importFileStatus; }
            set { _importFileStatus = value; }
        }

        private string _reportDateRange = string.Empty;

        public string ReportDateRange
        {
            get { return _reportDateRange; }
            set { _reportDateRange = value; }
        }

        private List<ImportFile> _importFileList = new List<ImportFile>();

        public List<ImportFile> ImportFileList
        {
            get { return _importFileList; }
            set { _importFileList = value; }
        }

        public void GetImportFileList()
        {
            int fileStatus = string.IsNullOrWhiteSpace(this.ImportFileStatus) ? 0 : Convert.ToInt32(this.ImportFileStatus);
            int numDays = string.IsNullOrWhiteSpace(this.ReportDateRange) ? 10000 : Convert.ToInt32(this.ReportDateRange);
            _importFileList = new BHLProvider().ImportFileSelectDetails(this.Institution, fileStatus, numDays);
        }
    }
}