using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<BSSegment> BSSegmentSelectByItem(int itemId)
        {
            return new BSSegmentDAL().BSSegmentSelectByItem(null, null, itemId);
        }

        public void InsertSegment(BSSegment segment, List<BSSegmentAuthor> authors)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                this.SetSegmentDefaults(segment);
                BSSegment newSegment = new BSSegmentDAL().BSSegmentInsertAuto(null, null, segment);

                foreach(BSSegmentPage page in segment.BSSegmentPages)
                {
                    page.SegmentID = newSegment.SegmentID;
                    new BSSegmentPageDAL().BSSegmentPageInsertAuto(null, null, page);
                }

                int sequence = 1;
                foreach (BSSegmentAuthor author in authors)
                {
                    author.SegmentID = newSegment.SegmentID;
                    author.SequenceOrder = sequence;
                    this.InsertSegmentAuthor(author);
                    sequence++;
                }

                transaction.Complete();
            }
        }

        /// <summary>
        /// Set the defaults for any required fields that are null.
        /// </summary>
        /// <param name="segment"></param>
        private void SetSegmentDefaults(BSSegment segment)
        {
            DateTime date = DateTime.Now;

            segment.Title = segment.Title ?? string.Empty;
            segment.BioStorReferenceID = segment.BioStorReferenceID ?? string.Empty;
            segment.SequenceOrder = (segment.SequenceOrder == short.MinValue ? (short)0 : segment.SequenceOrder);
            segment.Genre = segment.Genre ?? string.Empty;
            segment.ContainerTitle = segment.ContainerTitle ?? string.Empty;
            segment.Volume = segment.Volume ?? string.Empty;
            segment.Series = segment.Series ?? string.Empty;
            segment.Issue = segment.Issue ?? string.Empty;
            segment.Year = segment.Year ?? string.Empty;
            segment.Date = segment.Date ?? string.Empty;
            segment.ISSN = segment.ISSN ?? string.Empty;
            segment.DOI = segment.DOI ?? string.Empty;
            segment.StartPageNumber = segment.StartPageNumber ?? string.Empty;
            segment.EndPageNumber = segment.EndPageNumber ?? string.Empty;
            segment.CreationDate = (segment.CreationDate == DateTime.MinValue ? date : segment.CreationDate);
            segment.LastModifiedDate = (segment.LastModifiedDate == DateTime.MinValue ? date : segment.LastModifiedDate);
            segment.ContributorName = segment.ContributorName ?? string.Empty;

            foreach (BSSegmentPage page in segment.BSSegmentPages)
            {
                page.CreationDate = (page.CreationDate == DateTime.MinValue ? date : page.CreationDate);
            }
        }

        public void InsertSegmentAuthor(BSSegmentAuthor author)
        {
            this.SetSegmentAuthorDefaults(author);
            new BSSegmentAuthorDAL().BSSegmentAuthorInsertAuto(null, null, author);
        }

        private void SetSegmentAuthorDefaults(BSSegmentAuthor author)
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

        public List<BSSegment> SelectSegmentsForPublishing(int itemID)
        {
            return new BSSegmentDAL().SelectSegmentsForPublishing(null, null, itemID);
            //var segments = context.BSSegments.Where(s => s.ItemID == itemID && s.BHLSegmentID == null);
        }

        public void ResolveSegmentAuthors(int segmentID)
        {
            new BSSegmentDAL().BSSegmentResolveAuthors(null, null, segmentID);
        }

        public int PublishSegment(int itemID, int segmentID)
        {
            int newStatusID = int.MinValue;
            new BSSegmentDAL().BSSegmentPublishToProduction(null, null, itemID, segmentID, newStatusID);
            return newStatusID;
        }
    }
}
