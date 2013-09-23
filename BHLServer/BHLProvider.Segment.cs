using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public Segment SegmentSelectAuto(int segmentID)
        {
            return new SegmentDAL().SegmentSelectAuto(null, null, segmentID);
        }

        public CustomGenericList<Segment> SegmentSimpleSelectByAuthor(int authorID)
        {
            return new SegmentDAL().SegmentSimpleSelectByAuthor(null, null, authorID);
        }

        public CustomGenericList<Segment> SegmentSelectForAuthorID(int authorID)
        {
            return new SegmentDAL().SegmentSelectForAuthorID(null, null, authorID);
        }

        public CustomGenericList<Segment> SegmentSelectByDateRange(string startDate, string endDate)
        {
            return new SegmentDAL().SegmentSelectByDateRange(null, null, startDate, endDate);
        }

        public CustomGenericList<Segment> SegmentSelectByTitleLike(string title)
        {
            return new SegmentDAL().SegmentSelectByTitleLike(null, null, title);
        }

        public CustomGenericList<Segment> SegmentSelectByTitleNotLike(string title)
        {
            return new SegmentDAL().SegmentSelectByTitleNotLike(null, null, title);
        }

        public CustomGenericList<Segment> SegmentSelectForKeyword(string keyword)
        {
            return new SegmentDAL().SegmentSelectForKeyword(null, null, keyword);
        }

        public CustomGenericList<Segment> SegmentSelectByItemID(int itemID)
        {
            return new SegmentDAL().SegmentSelectByItemID(null, null, itemID, 0);
        }

        public CustomGenericList<TitleBibTeX> SegmentSelectAllBibTeXCitations()
        {
            return new SegmentDAL().SegmentSelectAllBibTeXCitations(null, null);
        }

        public CustomGenericList<TitleEndNote> SegmentSelectAllEndNoteCitations()
        {
            return new SegmentDAL().SegmentSelectAllEndNoteCitations(null, null);
        }

        public CustomGenericList<TitleBibTeX> SegmentSelectBibTeXForSegmentID(int segmentID, short includeNoContent)
        {
            return new SegmentDAL().SegmentSelectBibTexForSegmentID(null, null, segmentID, includeNoContent);
        }

        public CustomGenericList<TitleEndNote> SegmentSelectEndNoteForSegmentID(int segmentID, short includeNoContent)
        {
            return new SegmentDAL().SegmentSelectEndNoteForSegmentID(null, null, segmentID, includeNoContent);
        }

        public String SegmentBibTeXGetCitationStringForSegmentID(int segmentID, bool includeNoContent)
        {
            System.Text.StringBuilder bibtexString = new System.Text.StringBuilder("");
            CustomGenericList<TitleBibTeX> citations = this.SegmentSelectBibTeXForSegmentID(segmentID, (short)(includeNoContent ? 1 : 0));
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
                String pages = citation.Pages.ToString();
                String keywords = citation.Keywords;

                System.Collections.Generic.Dictionary<String, String> elements = new System.Collections.Generic.Dictionary<string, string>();
                elements.Add(BibTeXRefElementName.TITLE, citation.Title);
                if (journal != String.Empty) elements.Add(BibTeXRefElementName.JOURNAL, journal);
                if (bookTitle != String.Empty) elements.Add(BibTeXRefElementName.BOOKTITLE, bookTitle);
                if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
                if (copyrightStatus != String.Empty) elements.Add(BibTeXRefElementName.COPYRIGHT, copyrightStatus);
                if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
                if (note != String.Empty) elements.Add(BibTeXRefElementName.NOTE, note);
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

        public String SegmentEndNoteGetCitationStringForSegmentID(int segmentID, String segmentUrl, bool includeNoContent)
        {
            System.Text.StringBuilder endnoteString = new System.Text.StringBuilder("");
            CustomGenericList<TitleEndNote> citations = this.SegmentSelectEndNoteForSegmentID(segmentID, (short)(includeNoContent ? 1 : 0));
            foreach (TitleEndNote citation in citations)
            {
                String type = citation.PublicationType;
                String authors = citation.Authors;
                String year = citation.Year;
                String title = citation.Title;
                String journal = citation.Journal;
                String secondaryTitle = citation.SecondaryTitle;
                String publisherPlace = citation.PublisherPlace;
                String publisherName = citation.PublisherName;
                String volume = citation.Volume;
                String shortTitle = citation.ShortTitle;
                String abbreviation = citation.Abbreviation;
                String isbnissn = citation.Isbn;
                String callNumber = citation.CallNumber;
                String keywords = citation.Keywords;
                String language = citation.LanguageName;
                String note = citation.Note;
                String edition = citation.EditionStatement;
                String url = String.Format(segmentUrl, citation.SegmentID.ToString());
                String doi = citation.Doi;

                System.Collections.Generic.Dictionary<String, String> elements = new System.Collections.Generic.Dictionary<string, string>();
                if (authors != String.Empty) elements.Add(EndNoteRefElementName.AUTHORS, authors);
                if (year != String.Empty) elements.Add(EndNoteRefElementName.YEAR, year);
                if (title != String.Empty) elements.Add(EndNoteRefElementName.TITLE, title);
                if (journal != String.Empty) elements.Add(EndNoteRefElementName.JOURNAL, journal);
                if (secondaryTitle != String.Empty) elements.Add(EndNoteRefElementName.SECONDARYTITLE, secondaryTitle);
                if (publisherPlace != String.Empty) elements.Add(EndNoteRefElementName.CITY, publisherPlace);
                if (publisherName != String.Empty) elements.Add(EndNoteRefElementName.PUBLISHER, publisherName);
                if (volume != String.Empty) elements.Add(EndNoteRefElementName.VOLUME, volume);
                if (shortTitle != String.Empty) elements.Add(EndNoteRefElementName.SHORTTITLE, shortTitle);
                if (abbreviation != String.Empty) elements.Add(EndNoteRefElementName.ABBREVIATION, abbreviation);
                if (isbnissn != String.Empty) elements.Add(EndNoteRefElementName.ISBNISSN, isbnissn);
                if (callNumber != String.Empty) elements.Add(EndNoteRefElementName.CALLNUMBER, callNumber);
                if (keywords != String.Empty) elements.Add(EndNoteRefElementName.KEYWORDS, keywords);
                if (language != String.Empty) elements.Add(EndNoteRefElementName.LANGUAGE, language);
                if (note != String.Empty) elements.Add(EndNoteRefElementName.NOTE, note);
                if (edition != String.Empty) elements.Add(EndNoteRefElementName.EDITION, edition);
                if (url != String.Empty) elements.Add(EndNoteRefElementName.URL, url);
                if (doi != String.Empty) elements.Add(EndNoteRefElementName.DOI, doi);

                EndNote endnote = new EndNote(type, elements);
                endnoteString.Append(endnote.GenerateReference());
            }
            return endnoteString.ToString();
        }

        public Segment SegmentSelectForSegmentID(int segmentID)
        {
            return new SegmentDAL().SegmentSelectForSegmentID(null, null, segmentID);
        }

        public CustomGenericList<Segment> SegmentSelectPublished()
        {
            return new SegmentDAL().SegmentSelectPublished(null, null);
        }

        public Segment SegmentSelectExtended(int segmentID)
        {
            return new SegmentDAL().SegmentSelectExtended(null, null, segmentID);
        }

        public CustomGenericList<Segment> SegmentSelectRelated(int segmentID)
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
                else if (thirdChar == "an " || thirdChar == "de " || thirdChar == "el " || 
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

            return new SegmentDAL().Save(null, null, segment, userId);
        }

        public Segment SegmentUpdateStatus(int segmentID, int segmentStatusID)
        {
            SegmentDAL dal = new SegmentDAL();
            Segment segment = dal.SegmentSelectAuto(null, null, segmentID);
            if (segment != null)
            {
                segment.SegmentStatusID = segmentStatusID;
                segment = dal.SegmentUpdateAuto(null, null, segment);
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

        public CustomGenericList<Segment> SegmentSelectByStatusID(int segmentStatusID)
        {
            return new SegmentDAL().SegmentSelectByStatusID(null, null, segmentStatusID);
        }

        public CustomGenericList<Segment> SegmentSelectRecentlyClustered(int numberOfClusters)
        {
            return new SegmentDAL().SegmentSelectRecentlyClustered(null, null, numberOfClusters);
        }
    }
}
