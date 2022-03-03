using MOBOT.BHL.DAL;
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

        public List<DOI> DOISelectQueued()
        {
            return new DOIDAL().DOISelectQueued(null, null);
        }

        public List<Title_Identifier> DOISelectValidForTitle(int titleID)
        {
            return new DOIDAL().DOISelectValidForTitle(null, null, titleID);
        }

        public List<ItemIdentifier> DOISelectValidForSegment(int segmentID)
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

        public void DOIInsertQueue(int doiEntityTypeId, List<int> entityIds, int userId)
        {
            TransactionController transactionController = new TransactionController();
            try
            {
                transactionController.BeginTransaction();

                DOIDAL dal = new DOIDAL();
                foreach (int entityID in entityIds)
                {
                    dal.DOIInsertQueue(transactionController.Connection, transactionController.Transaction, 
                        doiEntityTypeId, entityID, userId, userId);
                }

                transactionController.CommitTransaction();
            }
            catch
            {
                transactionController.RollbackTransaction();
            }
            finally
            {
                transactionController.Dispose();
            }
        }

        public void DOIInsert(int doiEntityTypeId, int entityID, int doiStatusId, string doiName, short isValid,
            string doiBatchId, string message, int userId, int excludeBHLDOI)
        {
            new DOIDAL().DOIInsert(null, null, doiEntityTypeId, entityID, doiStatusId, doiName, isValid, doiBatchId, 
                message, userId, excludeBHLDOI);
        }

        public DOI DOInsertAuto(int doiEntityTypeId, int entityId, int doiStatusId, 
            string doiBatchId, string doiName, string message, short isValid, int userId)
        {
            return new DOIDAL().DOIInsertAuto(null, null, 
                doiEntityTypeId, entityId, doiStatusId, doiBatchId, doiName, DateTime.Now, message, isValid, userId, userId);
        }

        public void DOIInsertIdentifier(int doiEntityTypeId, int entityID, string doiName, int? userId)
        {
            new DOIDAL().DOIInsertIdentifier(null, null, doiEntityTypeId, entityID, doiName, userId);
        }

        public List<DOI> DOISelectByStatus(int doiStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            return new DOIDAL().DOISelectByStatus(null, null, doiStatusId, numRows, pageNum, sortColumn, sortOrder);
        }

        public DOI DOISelectQueuedByTypeAndID(string doiEntityTypeName, int entityID)
        {
            return new DOIDAL().DOISelectQueuedByTypeAndID(null, null, doiEntityTypeName, entityID);
        }

        public void DOIDeleteQueuedByTypeAndID(int doiEntityTypeID, int entityID)
        {
            new DOIDAL().DOIDeleteQueuedByTypeAndID(null, null, doiEntityTypeID, entityID);
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

        public List<DOIEntityType> DOIEntityTypeSelectAll()
        {
            return new DOIDAL().DOIEntityTypeSelectAll(null, null);
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
            if (service.GetFileAccessProvider().FileExists(filepath))
            {
                fileContents = new BHLProvider().GetFileAccessProvider().GetFileText(filepath);
            }

            return fileContents;
        }
    }
}
