using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<OAIIdentifier> OAIIdentifierSelectItems(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate, Int16 includeLocalContent, Int16 includeExternalContent)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectItems(null, null, maxIdentifiers, startId, fromDate, untilDate, includeLocalContent, includeExternalContent);
        }

        public List<OAIIdentifier> OAIIdentifierSelectPDFs(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectPDFs(null, null, maxIdentifiers, startId, fromDate, untilDate);
        }

        public List<OAIIdentifier> OAIIdentifierSelectTitles(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectTitles(null, null, maxIdentifiers, startId, fromDate, untilDate);
        }

        public List<OAIIdentifier> OAIIdentifierSelectSegments(int maxIdentifiers, int startId,
            DateTime? fromDate, DateTime? untilDate, Int16 includeLocalContent, Int16 includeExternalContent)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectSegments(null, null, maxIdentifiers, startId, fromDate, untilDate, includeLocalContent, includeExternalContent);
        }

        public List<OAIIdentifier> OAIIdentifierSelectAll(int maxIdentifiers, int startId,
            String set, DateTime? fromDate, DateTime? untilDate)
        {
            return new OAIIdentifierDAL().OAIIdentifierSelectAll(null, null, maxIdentifiers, startId, set, fromDate, untilDate);
        }
    }
}
