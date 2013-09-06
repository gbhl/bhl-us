using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<DOI> DOISelectSubmitted(int minutesSinceSubmit)
        {
            return new DOIDAL().DOISelectSubmitted(null, null, minutesSinceSubmit);
        }

        public CustomGenericList<DOI> DOISelectValidForTitle(int titleID)
        {
            return new DOIDAL().DOISelectValidForTitle(null, null, titleID);
        }

        public CustomGenericList<DOI> DOISelectValidForSegment(int segmentID)
        {
            return new DOIDAL().DOISelectValidForSegment(null, null, segmentID);
        }

        public CustomGenericList<DOI> TitleSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return new TitleDAL().TitleSelectWithoutSubmittedDOI(null, null, numberToReturn);
        }

        public DOI DOInsertAuto(int doiEntityTypeId, int entityId, int doiStatusId, 
            string doiBatchId, string doiName, string message, short isValid)
        {
            return new DOIDAL().DOIInsertAuto(null, null, 
                doiEntityTypeId, entityId, doiStatusId, doiBatchId, doiName, DateTime.Now, message, isValid);
        }

        public CustomGenericList<DOI> DOISelectByStatus(int doiStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            return new DOIDAL().DOISelectByStatus(null, null, doiStatusId, numRows, pageNum, sortColumn, sortOrder);
        }

        public DOI DOIUpdateStatus(int doiID, int doiStatusId)
        {
            return this.DOIUpdateStatus(doiID, doiStatusId, string.Empty, null, null, null);
        }

        public DOI DOIUpdateStatus(int doiID, int doiStatusId, string message, short? setValid)
        {
            return this.DOIUpdateStatus(doiID, doiStatusId, message, null, null, setValid);
        }

        private DOI DOIUpdateStatus(int doiID, int doiStatusId, string message, string doiName, string doiBatchID, short? isValid)
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
                doi = dal.DOIUpdateAuto(null, null, doi);
            }
            else
            {
                throw new Exception("Could not find existing DOI record.");
            }
            return doi;
        }

        public DOI DOIUpdateDOIName(int doiID, int doiStatusId, string doiName)
        {
            return this.DOIUpdateStatus(doiID, doiStatusId, null, doiName, null, null);
        }

        public DOI DOIUpdateBatchID(int doiID, int doiStatusID, string doiBatchID)
        {
            return this.DOIUpdateStatus(doiID, doiStatusID, null, null, doiBatchID, null);
        }

        public CustomGenericList<DOIStatus> DOIStatusSelectAll()
        {
            return new DOIDAL().DOIStatusSelectAll(null, null);
        }
    }
}
