using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOBOT.BHLImport.BHLImportEFDataModel;

namespace MOBOT.BHLImport.BHLImportEFDataService
{
    public partial class DataService
    {
        public void InsertAuthor(SegmentAuthor author)
        {
            BHLImportEntities context = GetDataContext();
            this.SetSegmentAuthorDefaults(author);
            context.SegmentAuthors.Add(author);
            context.SaveChanges();
        }

        private void SetSegmentAuthorDefaults(SegmentAuthor author)
        {
            DateTime date = DateTime.Now;

            author.BioStorID = author.BioStorID ?? string.Empty;
            author.LastName = author.LastName ?? string.Empty;
            author.FirstName = author.FirstName ?? string.Empty;
            author.SequenceOrder = (author.SequenceOrder == int.MinValue ? 1 : author.SequenceOrder);
            author.VIAFIdentifier = author.VIAFIdentifier ?? string.Empty;
            author.CreationDate = (author.CreationDate == DateTime.MinValue ? date : author.CreationDate);
            author.LastModifiedDate = (author.LastModifiedDate == DateTime.MinValue ? date : author.LastModifiedDate);
        }

        public List<SegmentAuthor> GetSegmentAuthorsForSegment(int importSourceID, int segmentID)
        {
            BHLImportEntities context = GetDataContext();
            var authors = context.SegmentAuthors.Where(i => i.ImportSourceID == importSourceID && i.SegmentID == segmentID);
            return authors.ToList();
        }
    }
}
