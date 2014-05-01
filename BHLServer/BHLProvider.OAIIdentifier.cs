using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<OAIIdentifier> OAIIdentifierSelectItems(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate, Int16 includeLocalContent, Int16 includeExternalContent)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectItems(null, null, maxIdentifiers, startId, fromDate, untilDate, includeLocalContent, includeExternalContent);
        }

        public CustomGenericList<OAIIdentifier> OAIIdentifierSelectPDFs(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectPDFs(null, null, maxIdentifiers, startId, fromDate, untilDate);
        }

        public CustomGenericList<OAIIdentifier> OAIIdentifierSelectTitles(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectTitles(null, null, maxIdentifiers, startId, fromDate, untilDate);
        }

        public CustomGenericList<OAIIdentifier> OAIIdentifierSelectSegments(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate, Int16 includeLocalContent, Int16 includeExternalContent)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectSegments(null, null, maxIdentifiers, startId, fromDate, untilDate, includeLocalContent, includeExternalContent);
        }

        public CustomGenericList<OAIIdentifier> OAIIdentifierSelectAll(int maxIdentifiers, int startId,
            String set, DateTime? fromDate, DateTime? untilDate)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectAll(null, null, maxIdentifiers, startId, set, fromDate, untilDate);
        }
    }
}
