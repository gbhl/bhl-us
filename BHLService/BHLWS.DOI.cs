using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Web.Services;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public List<DOI> DOISelectSubmitted(int minutesSinceSubmit)
        {
            return new BHLProvider().DOISelectSubmitted(minutesSinceSubmit);
        }

        [WebMethod]
        public List<DOI> TitleSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return new BHLProvider().TitleSelectWithoutSubmittedDOI(numberToReturn);
        }

        [WebMethod]
        public List<DOI> SegmentSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return new BHLProvider().SegmentSelectWithoutSubmittedDOI(numberToReturn);
        }

        [WebMethod]
        public DOI DOIInsertAuto(int doiEntityTypeId, int entityId, int doiStatusId, string doiBatchId,
            string doiName, string message, short isValid, int userId)
        {
            return new BHLProvider().DOInsertAuto(doiEntityTypeId, 
                entityId, doiStatusId, doiBatchId, doiName, message, isValid, userId);
        }

        [WebMethod]
        public DOI DOIUpdateStatus(int doiID, int doiStatusId, string message, short? setValid, int? userId)
        {
            return new BHLProvider().DOIUpdateStatus(doiID, doiStatusId, message, setValid, userId);
        }

        [WebMethod]
        public DOI DOIUpdateDOIName(int doiID, int doiStatusId, string doiName, int? userId)
        {
            return new BHLProvider().DOIUpdateDOIName(doiID, doiStatusId, doiName, userId);
        }

        [WebMethod]
        public DOI DOIUpdateBatchID(int doiID, int doiStatusId, string doiBatchID, int? userId)
        {
            return new BHLProvider().DOIUpdateBatchID(doiID, doiStatusId, doiBatchID, userId);
        }

        [WebMethod]
        public string DOIGetFileContents(string batchId, string type)
        {
            return new BHLProvider().DOIGetFileContents(batchId, type);
        }

        [WebMethod]
        public List<DOI> DOISelectValidForTitle(int titleID)
        {
            return new BHLProvider().DOISelectValidForTitle(titleID);
        }
    }
}