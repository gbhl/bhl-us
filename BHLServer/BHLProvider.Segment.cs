using System;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public Segment SegmentSelectAuto(int segmentID)
        {
            return new SegmentDAL().SegmentSelectAuto(null, null, segmentID);
        }

        public List<Segment> SegmentSimpleSelectByAuthor(int authorID)
        {
            return new SegmentDAL().SegmentSimpleSelectByAuthor(null, null, authorID);
        }

        public List<Segment> SegmentSelectForAuthorID(int authorID)
        {
            return new SegmentDAL().SegmentSelectForAuthorID(null, null, authorID);
        }

        public Tuple<int, List<Segment>> SegmentSelectForAuthorIDPaged(int authorID, int pageNum, int numPages, string sort)
        {
            return new SegmentDAL().SegmentSelectForAuthorIDPaged(null, null, authorID, pageNum, numPages, sort);
        }

        public Tuple<int, List<Segment>> SegmentSelectByDateRange(int startDate, int endDate, int pageNum, int numPages, string sort)
        {
            return new SegmentDAL().SegmentSelectByDateRange(null, null, startDate.ToString(), endDate.ToString(), pageNum, numPages, sort);
        }

        public Tuple<int, List<Segment>> SegmentSelectByTitleLike(string title, int pageNum, int numPages, string sort)
        {
            return new SegmentDAL().SegmentSelectByTitleLike(null, null, title, pageNum, numPages, sort);
        }

        public List<Segment> SegmentSelectByTitleNotLike(string title)
        {
            return new SegmentDAL().SegmentSelectByTitleNotLike(null, null, title);
        }

        public List<Segment> SegmentSelectForKeyword(string keyword)
        {
            return new SegmentDAL().SegmentSelectForKeyword(null, null, keyword);
        }

        public Tuple<int, List<Segment>> SegmentSelectForKeywordPaged(string keyword, int pageNum, int numPages, string sort)
        {
            return new SegmentDAL().SegmentSelectForKeywordPaged(null, null, keyword, pageNum, numPages, sort);
        }

        public List<Segment> SegmentSelectByItemID(int itemID)
        {
            return new SegmentDAL().SegmentSelectByItemID(null, null, itemID, 0);
        }

        public Segment SegmentSelectByBarCode(string barcode)
        {
            return new SegmentDAL().SegmentSelectByBarCode(null, null, barcode);
        }

        public List<Segment> SegmentSelectWithoutDOIByItemID(int itemID)
        {
            return new SegmentDAL().SegmentSelectWithoutDOIByItemID(null, null, itemID);
        }

        public List<Segment> SegmentSelectWithoutDOIByTitleID(int titleID)
        {
            return new SegmentDAL().SegmentSelectWithoutDOIByTitleID(null, null, titleID);
        }

        public List<TitleBibTeX> SegmentSelectAllBibTeXCitations()
        {
            return new SegmentDAL().SegmentSelectAllBibTeXCitations(null, null);
        }

        public List<TitleBibTeX> SegmentSelectBibTeXForSegmentID(int segmentID, short includeNoContent)
        {
            return new SegmentDAL().SegmentSelectBibTexForSegmentID(null, null, segmentID, includeNoContent);
        }

        public String SegmentBibTeXGetCitationStringForSegmentID(int segmentID, bool includeNoContent)
        {
            System.Text.StringBuilder bibtexString = new System.Text.StringBuilder("");
            List<TitleBibTeX> citations = this.SegmentSelectBibTeXForSegmentID(segmentID, (short)(includeNoContent ? 1 : 0));
            foreach (TitleBibTeX citation in citations)
            {
                string journal = String.Empty;
                string bookTitle = String.Empty;

                string type = BibTeXRefType.BOOK;
                if (citation.Type == "Article")
                {
                    type = BibTeXRefType.ARTICLE;
                    journal = citation.Journal;
                }
                if (citation.Type == "BookItem" || citation.Type == "Chapter" || citation.Type == "Treatment")
                {
                    type = BibTeXRefType.INBOOK;
                    bookTitle = citation.Journal;
                }

                String volume = citation.Volume;
                String copyrightStatus = citation.CopyrightStatus;
                String url = citation.Url;
                String note = citation.Note;
                String pages = citation.PageRange;
                String keywords = citation.Keywords;

                System.Collections.Generic.Dictionary<String, String> elements = new System.Collections.Generic.Dictionary<string, string>();
                elements.Add(BibTeXRefElementName.TITLE, citation.Title);
                if (journal != String.Empty) elements.Add(BibTeXRefElementName.JOURNAL, journal);
                if (bookTitle != String.Empty) elements.Add(BibTeXRefElementName.BOOKTITLE, bookTitle);
                if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
                if (copyrightStatus != String.Empty) elements.Add(BibTeXRefElementName.COPYRIGHT, copyrightStatus);
                if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
                if (note != String.Empty) elements.Add(BibTeXRefElementName.NOTE, note.Replace("\n", " ").Replace("\r", " "));
                elements.Add(BibTeXRefElementName.PUBLISHER, citation.Publisher);
                elements.Add(BibTeXRefElementName.AUTHOR, citation.Authors.Replace("|", " and "));
                elements.Add(BibTeXRefElementName.YEAR, citation.Year);
                if (pages != String.Empty) elements.Add(BibTeXRefElementName.PAGES, pages);
                if (keywords != String.Empty) elements.Add(BibTeXRefElementName.KEYWORDS, keywords);

                BibTeX bibTex = new BibTeX(type, citation.CitationKey, elements);
                bibtexString.Append(bibTex.GenerateReference());
            }
            return bibtexString.ToString();
        }

        public List<RISCitation> SegmentSelectAllRISCitations()
        {
            return new SegmentDAL().SegmentSelectAllRISCitations(null, null);
        }

        public String SegmentGetRISCitationStringForSegmentID(int segmentID)
        {
            System.Text.StringBuilder risString = new System.Text.StringBuilder("");
            List<RISCitation> citations = new SegmentDAL().SegmentSelectRISCitationForSegmentID(null, null, segmentID);
            foreach (RISCitation citation in citations)
            {
                risString.Append(this.GenerateRISCitation(citation));
            }
            return risString.ToString();
        }

        public Segment SegmentSelectForSegmentID(int segmentID)
        {
            return new SegmentDAL().SegmentSelectForSegmentID(null, null, segmentID);
        }

        public List<Segment> SegmentSelectPublished()
        {
            return new SegmentDAL().SegmentSelectPublished(null, null);
        }

        public Segment SegmentSelectForEdit(int segmentID)
        {
            return new SegmentDAL().SegmentSelectForEdit(null, null, segmentID);
        }

        public Segment SegmentSelectExtended(int segmentID)
        {
            return new SegmentDAL().SegmentSelectExtended(null, null, segmentID);
        }

        public List<Segment> SegmentSelectRelated(int segmentID)
        {
            return new SegmentDAL().SegmentSelectRelated(null, null, segmentID);
        }

        public int SegmentSave(Segment segment, int userId)
        {
            // If no SortTitle has been supplied, create one
            if (string.IsNullOrWhiteSpace(segment.SortTitle) && segment.Title.Length >= 4)
            {
                string firstChar = segment.Title.Substring(0, 1).ToLower();
                string secondChar = segment.Title.Substring(0, 2).ToLower();
                string thirdChar = segment.Title.Substring(0, 3).ToLower();
                string fourthChar = segment.Title.Substring(0, 4).ToLower();

                if (firstChar == "\"" || firstChar == "'" || firstChar == "[" || firstChar == "(" || firstChar == "|")
                {
                    segment.SortTitle = segment.Title.Substring(1);
                }
                else if (secondChar == "a ")
                {
                    segment.SortTitle = segment.Title.Substring(2);
                }
                else if (thirdChar == "an " || thirdChar == "el " || 
                        thirdChar == "il " || thirdChar == "la " || thirdChar == "le ")
                {
                    segment.SortTitle = segment.Title.Substring(3);
                }
                else if (fourthChar == "das " || fourthChar == "der " || fourthChar == "die " || fourthChar == "ein " ||
                        fourthChar == "las " || fourthChar == "les " || fourthChar == "los " || fourthChar == "the ")
                {
                    segment.SortTitle = segment.Title.Substring(4);
                }
                else
                {
                    segment.SortTitle = segment.Title;
                }
            }

            // Clean up the sort title
            segment.SortTitle = DataCleaner.CleanSortTitle(segment.SortTitle);

            return new SegmentDAL().Save(null, null, segment, userId);
        }

        public Segment SegmentUpdateStatus(int segmentID, int segmentStatusID)
        {
            SegmentDAL dal = new SegmentDAL();
            Segment segment = dal.SegmentSelectAuto(null, null, segmentID);
            if (segment != null)
            {
                this.ItemUpdateStatus((int)segment.ItemID, segmentStatusID);
            }
            else
            {
                throw new Exception("Could not find existing segment record");
            }
            return segment;
        }

        public SegmentCluster SegmentClusterInsertAuto(int userID)
        {
            SegmentClusterDAL dal = new SegmentClusterDAL();
            return dal.SegmentClusterInsertAuto(null, null, userID, userID, (int)SegmentClusterTypes.SameAs);
        }

        public SegmentClusterSegment SegmentClusterSegmentAuto(int segmentID, int clusterID, int userID)
        {
            SegmentClusterSegmentDAL dal = new SegmentClusterSegmentDAL();
            return dal.SegmentClusterSegmentInsertAuto(null, null, segmentID, clusterID, 0, userID, userID);
        }

        public List<Segment> SegmentSelectByStatusID(int segmentStatusID)
        {
            return new SegmentDAL().SegmentSelectByStatusID(null, null, segmentStatusID);
        }

        public List<Segment> SegmentSelectRecentlyClustered(int numberOfClusters)
        {
            return new SegmentDAL().SegmentSelectRecentlyClustered(null, null, numberOfClusters);
        }

        public List<Segment> SegmentSelectChildSegmentsBySegmentID(int segmentID)
        {
            return new SegmentDAL().SegmentSelectChildSegmentsBySegmentID(null, null, segmentID);
        }

        public List<Segment> SegmentSelectSiblingSegmentsBySegmentID(int segmentID)
        {
            return new SegmentDAL().SegmentSelectSiblingSegmentsBySegmentID(null, null, segmentID);
        }

        public List<ItemAuthor> SegmentAuthorSelectBySegmentID(int segmentID)
        {
            return new ItemAuthorDAL().ItemAuthorSelectBySegmentID(null, null, segmentID);
        }

        /// <summary>
        /// Select only those identifiers for a given segment that have been designated for display
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public List<ItemIdentifier> ItemIdentifierSelectForDisplayBySegmentID(int segmentID)
        {
            return (new ItemIdentifierDAL().ItemIdentifierSelectBySegmentID(null, null, segmentID, 1));
        }

        public Tuple<int, List<Segment>> SegmentSelectByInstitutionAndStartsWith(string institutionCode, string startsWith, int pageNum, int numPages, string sort)
        {
            return new SegmentDAL().SegmentSelectByInstitutionAndStartsWith(null, null, institutionCode, startsWith, pageNum, numPages, sort);
        }

        public Tuple<int, List<Segment>> SegmentSelectByInstitutionAndStartsWithout(string institutionCode, string startsWith, int pageNum, int numPages, string sort)
        {
            return new SegmentDAL().SegmentSelectByInstitutionAndStartsWithout(null, null, institutionCode, startsWith, pageNum, numPages, sort);
        }

        public List<Segment> SegmentResolve(string doi, int startPageID)
        {
            return new SegmentDAL().SegmentResolve(null, null, doi, startPageID);
        }
    }
}
