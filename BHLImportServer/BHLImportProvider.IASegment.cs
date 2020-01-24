using MOBOT.BHLImport.DataObjects;
using System;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IASegment SaveIASegment(int itemID, int sequence, string title, string volume, string issue, string series, string date, string language,
            int? genreId, string genreName, string doi)
        {
            throw new NotImplementedException();
        }

        public IASegmentAuthor SaveIASegmentAuthor(int segmentID, int sequence, int? authorId, string fullName, string lastName, string firstName,
            string startDate, string endDate, int? identifierId, string identifierValue)
        {
            throw new NotImplementedException();
        }

        public IASegmentPage SaveIASegmentPage(int segmentID, int pageSequence)
        {
            throw new NotImplementedException();
        }
    }
}
