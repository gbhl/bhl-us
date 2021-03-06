﻿using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<DOI> DOISelectSubmitted(int minutesSinceSubmit)
        {
            return new DOIDAL().DOISelectSubmitted(null, null, minutesSinceSubmit);
        }

        public List<DOI> DOISelectValidForTitle(int titleID)
        {
            return new DOIDAL().DOISelectValidForTitle(null, null, titleID);
        }

        public List<DOI> DOISelectValidForSegment(int segmentID)
        {
            return new DOIDAL().DOISelectValidForSegment(null, null, segmentID);
        }

        public List<DOI> TitleSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return new TitleDAL().TitleSelectWithoutSubmittedDOI(null, null, numberToReturn);
        }

        public List<DOI> SegmentSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return new SegmentDAL().SegmentSelectWithoutSubmittedDOI(null, null, numberToReturn);
        }

        public DOI DOInsertAuto(int doiEntityTypeId, int entityId, int doiStatusId, 
            string doiBatchId, string doiName, string message, short isValid, int userId)
        {
            return new DOIDAL().DOIInsertAuto(null, null, 
                doiEntityTypeId, entityId, doiStatusId, doiBatchId, doiName, DateTime.Now, message, isValid, userId, userId);
        }

        public List<DOI> DOISelectByStatus(int doiStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            return new DOIDAL().DOISelectByStatus(null, null, doiStatusId, numRows, pageNum, sortColumn, sortOrder);
        }

        public DOI DOIUpdateStatus(int doiID, int doiStatusId, int? userId)
        {
            return this.DOIUpdateStatus(doiID, doiStatusId, string.Empty, null, null, null, userId);
        }

        public DOI DOIUpdateStatus(int doiID, int doiStatusId, string message, short? setValid, int? userId)
        {
            return this.DOIUpdateStatus(doiID, doiStatusId, message, null, null, setValid, userId);
        }

        private DOI DOIUpdateStatus(int doiID, int doiStatusId, string message, string doiName, string doiBatchID, short? isValid, int? userId)
        {
            DOIDAL dal = new DOIDAL();
            DOI doi = dal.DOISelectAuto(null, null, doiID);

            if (doi != null)
            {
                doi.DOIStatusID = doiStatusId;
                doi.StatusDate = DateTime.Now;
                doi.StatusMessage = (message ?? doi.StatusMessage);
                doi.DOIName = (doiName ?? doi.DOIName);
                doi.DOIBatchID = (doiBatchID ?? doi.DOIBatchID);
                doi.IsValid = (isValid ?? doi.IsValid);
                doi.LastModifiedUserID = (userId ?? doi.LastModifiedUserID);
                doi = dal.DOIUpdateAuto(null, null, doi);
            }
            else
            {
                throw new Exception("Could not find existing DOI record.");
            }
            return doi;
        }

        public DOI DOIUpdateDOIName(int doiID, int doiStatusId, string doiName, int? userId)
        {
            return this.DOIUpdateStatus(doiID, doiStatusId, null, doiName, null, null, userId);
        }

        public DOI DOIUpdateBatchID(int doiID, int doiStatusID, string doiBatchID, int? userId)
        {
            return this.DOIUpdateStatus(doiID, doiStatusID, null, null, doiBatchID, null, userId);
        }

        public List<DOIStatus> DOIStatusSelectAll()
        {
            return new DOIDAL().DOIStatusSelectAll(null, null);
        }

        public string DOIGetFileContents(string batchId, string type)
        {
            string fileContents = "File not found.";
            string filepath = string.Empty;

            if (type == "d")    // Deposit
            {
                filepath = String.Format(ConfigurationManager.AppSettings["DOIDepositFileLocation"], batchId);
            }

            if (type == "s")    // Submission log
            {
                filepath = String.Format(ConfigurationManager.AppSettings["DOISubmitLogFileLocation"], batchId);
            }

            BHLProvider service = new BHLProvider();
            if (service.GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true").FileExists(filepath))
            {
                fileContents = new BHLProvider().GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true").GetFileText(filepath);
            }

            return fileContents;
        }
    }
}
