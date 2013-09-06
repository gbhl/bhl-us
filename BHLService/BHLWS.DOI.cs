using System;
using System.Web.Services;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public CustomGenericList<DOI> DOISelectSubmitted(int minutesSinceSubmit)
        {
            return new BHLProvider().DOISelectSubmitted(minutesSinceSubmit);
        }

        [WebMethod]
        public CustomGenericList<DOI> TitleSelectWithoutSubmittedDOI(int numberToReturn)
        {
            return new BHLProvider().TitleSelectWithoutSubmittedDOI(numberToReturn);
        }

        [WebMethod]
        public DOI DOIInsertAuto(int doiEntityTypeId, int entityId, int doiStatusId, string doiBatchId,
            string doiName, string message, short isValid)
        {
            return new BHLProvider().DOInsertAuto(doiEntityTypeId, 
                entityId, doiStatusId, doiBatchId, doiName, message, isValid);
        }

        [WebMethod]
        public DOI DOIUpdateStatus(int doiID, int doiStatusId, string message, short? setValid)
        {
            return new BHLProvider().DOIUpdateStatus(doiID, doiStatusId, message, setValid);
        }

        [WebMethod]
        public DOI DOIUpdateDOIName(int doiID, int doiStatusId, string doiName)
        {
            return new BHLProvider().DOIUpdateDOIName(doiID, doiStatusId, doiName);
        }

        [WebMethod]
        public DOI DOIUpdateBatchID(int doiID, int doiStatusId, string doiBatchID)
        {
            return new BHLProvider().DOIUpdateBatchID(doiID, doiStatusId, doiBatchID);
        }
    }
}