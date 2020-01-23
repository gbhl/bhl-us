using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using MOBOT.BHLImport.BHLImportEFDataModel;

namespace MOBOT.BHLImport.BHLImportEFDataService
{
    public partial class DataService
    {
        public void InsertSegment(BSSegment segment, List<BSSegmentAuthor> authors)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                BHLImportEntities context = GetDataContext();
                this.SetSegmentDefaults(segment);
                context.BSSegments.Add(segment);
                context.SaveChanges();

                int sequence = 1;
                foreach (BSSegmentAuthor author in authors)
                {
                    author.SegmentID = segment.SegmentID;
                    author.SequenceOrder = sequence;
                    this.InsertAuthor(author);
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

        public List<BSSegment> SelectSegmentsForItem(int itemID)
        {
            BHLImportEntities context = GetDataContext();
            var segments = context.BSSegments.Where(s => s.ItemID == itemID);
            return segments.ToList();
        }

        public List<BSSegment> SelectSegmentsForPublishing(int itemID)
        {
            BHLImportEntities context = GetDataContext();
            var segments = context.BSSegments.Where(s => s.ItemID == itemID && s.BHLSegmentID == null);
            return segments.ToList();
        }

        public void ResolveSegmentAuthors(int segmentID)
        {
            BHLImportEntities context = GetDataContext();
            context.BSSegmentResolveAuthors(segmentID);
        }

        public void PublishSegment(int itemID, int segmentID)
        {
            BHLImportEntities context = GetDataContext();
            context.BSSegmentPublishToProduction(itemID, segmentID);
        }
    }
}
