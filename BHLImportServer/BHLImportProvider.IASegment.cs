using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IASegment SaveIASegment(int itemID, int sequence, string title, string volume, string issue, string series, string date, string language,
            int genreId, string genreName, string doi)
        {
            IASegmentDAL dal = new IASegmentDAL();
            IASegment savedSegment = dal.IASegmentSelectByItemAndSequence(null, null, itemID, sequence);

            if (savedSegment == null)
            {
                IASegment newIASegment = new IASegment();
                newIASegment.ItemID = itemID;
                newIASegment.Sequence = sequence;
                newIASegment.Title = title;
                newIASegment.Volume = volume;
                newIASegment.Issue= issue;
                newIASegment.Series = series;
                newIASegment.Date = date;
                newIASegment.LanguageCode = language;
                newIASegment.BHLSegmentGenreID = genreId;
                newIASegment.BHLSegmentGenreName = genreName;
                newIASegment.DOI = doi;
                savedSegment = dal.IASegmentInsertAuto(null, null, newIASegment);
            }
            else
            {
                bool updated = false;
                if (savedSegment.Title != title) { savedSegment.Title = title; updated = true; }
                if (savedSegment.Volume != volume) { savedSegment.Volume = volume; updated = true; }
                if (savedSegment.Issue != issue) { savedSegment.Issue = issue; updated = true; }
                if (savedSegment.Series != series) { savedSegment.Series = series; updated = true; }
                if (savedSegment.Date != date) { savedSegment.Date = date; updated = true; }
                if (savedSegment.LanguageCode != language) { savedSegment.LanguageCode = language; updated = true; }
                if (savedSegment.BHLSegmentGenreID != genreId) { savedSegment.BHLSegmentGenreID = genreId; updated = true; }
                if (savedSegment.BHLSegmentGenreName != genreName) { savedSegment.BHLSegmentGenreName = genreName; updated = true; }
                if (savedSegment.DOI != doi) { savedSegment.DOI= doi; updated = true; }
                if (updated) dal.IASegmentUpdateAuto(null, null, savedSegment);
            }

            return savedSegment;
        }

        public IASegmentAuthor SaveIASegmentAuthor(int segmentID, int sequence, int? authorId, string fullName, string lastName, string firstName,
            string startDate, string endDate, int? identifierId, string identifierValue)
        {
            IASegmentAuthorDAL dal = new IASegmentAuthorDAL();
            IASegmentAuthor savedSegmentAuthor = dal.IASegmentAuthorSelectBySegmentAndSequence(null, null, segmentID, sequence);

            if (savedSegmentAuthor == null)
            {
                IASegmentAuthor newIASegmentAuthor = new IASegmentAuthor();
                newIASegmentAuthor.SegmentID = segmentID;
                newIASegmentAuthor.Sequence = sequence;
                newIASegmentAuthor.BHLAuthorID = authorId;
                newIASegmentAuthor.FullName = fullName;
                newIASegmentAuthor.LastName = lastName;
                newIASegmentAuthor.FirstName = firstName;
                newIASegmentAuthor.StartDate = startDate;
                newIASegmentAuthor.EndDate = endDate;
                newIASegmentAuthor.BHLIdentifierID = identifierId;
                newIASegmentAuthor.IdentifierValue = identifierValue;
                savedSegmentAuthor = dal.IASegmentAuthorInsertAuto(null, null, newIASegmentAuthor);
            }
            else
            {
                bool updated = false;
                if (savedSegmentAuthor.BHLAuthorID != authorId) { savedSegmentAuthor.BHLAuthorID = authorId; updated = true; }
                if (savedSegmentAuthor.FullName != fullName) { savedSegmentAuthor.FullName = fullName; updated = true; }
                if (savedSegmentAuthor.LastName != lastName) { savedSegmentAuthor.LastName = lastName; updated = true; }
                if (savedSegmentAuthor.FirstName != firstName) { savedSegmentAuthor.FirstName = firstName; updated = true; }
                if (savedSegmentAuthor.StartDate != startDate) { savedSegmentAuthor.StartDate = startDate; updated = true; }
                if (savedSegmentAuthor.EndDate != endDate) { savedSegmentAuthor.EndDate = endDate; updated = true; }
                if (savedSegmentAuthor.BHLIdentifierID != identifierId) { savedSegmentAuthor.BHLIdentifierID = identifierId; updated = true; }
                if (savedSegmentAuthor.IdentifierValue != identifierValue) { savedSegmentAuthor.IdentifierValue = identifierValue; updated = true; }
                if (updated) dal.IASegmentAuthorUpdateAuto(null, null, savedSegmentAuthor);
            }

            return savedSegmentAuthor;
        }

        public IASegmentPage SaveIASegmentPage(int segmentID, int pageSequence)
        {
            IASegmentPageDAL dal = new IASegmentPageDAL();
            IASegmentPage savedSegmentPage = dal.IASegmentPageSelectBySegmentAndSequence(null, null, segmentID, pageSequence);

            if (savedSegmentPage == null)
            {
                IASegmentPage newIASegmentPage = new IASegmentPage();
                newIASegmentPage.SegmentID = segmentID;
                newIASegmentPage.PageSequence = pageSequence;
                savedSegmentPage = dal.IASegmentPageInsertAuto(null, null, newIASegmentPage);
            }
            else
            {
                // Nothing to do; we already have this page
            }

            return savedSegmentPage;
        }

        public void IASegmentDeleteByItem(int itemID)
        {
            new IASegmentDAL().IASegmentDeleteByItem(null, null, itemID);
        }
    }
}
