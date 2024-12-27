using System;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;

namespace MOBOT.BHL.Server
{
	public partial class BHLProvider
	{
		private TitleDAL titleDal = null;

		#region Select methods

		/// <summary>
		/// Select all values from Title.
		/// </summary>
		/// <returns>Objects of type Title.</returns>
		public List<Title> TitleSelectAll()
		{
			return ( new TitleDAL().TitleSelectAll( null, null ) );
		}

		public Title TitleSelectAuto( int titleID )
		{
			return GetTitleDalInstance().TitleSelectAuto( null, null, titleID );
		}

		/// <summary>
		/// Select all values from Title.
		/// </summary>
		/// <returns>Objects of type Title.</returns>
		public List<Title> TitleSelectAllNonPublished()
		{
			return ( new TitleDAL().TitleSelectAllNonPublished( null, null ) );
		}

		/// <summary>
		/// Select all values from Title.
		/// </summary>
		/// <returns>Objects of type Title.</returns>
		public List<Title> TitleSelectAllPublished()
		{
			return ( new TitleDAL().TitleSelectAllPublished( null, null ) );
		}

        /// <summary>
		/// Select all values from Title like a particular string.
		/// </summary>
		/// <returns>Objects of type Title.</returns>
		public List<Title> TitleSelectSearchName( string name, string languageCode, int returnCount )
		{
			return ( new TitleDAL().TitleSelectSearchName( null, null, name, languageCode, returnCount ) );
		}

		/// <summary>
		/// Select Title 
		/// </summary>
		/// <param name="titleID"></param>
		/// <returns>Object of type Title.</returns>
		public Title TitleSelect( int titleID )
		{
			return ( new TitleDAL().TitleSelectAuto( null, null, titleID ) );
		}

		public Title TitleSelectExtended( int titleId )
		{
			return new TitleDAL().TitleSelectExtended( null, null, titleId );
		}

        public List<CreatorTitle> TitleSimpleSelectByAuthor(int authorId)
        {
            return (new TitleDAL().TitleSimpleSelectByAuthor(null, null, authorId));
        }

        /// <summary>
        /// Select all Titles for the specified Item.
        /// </summary>
        /// <returns>Objects of type Title.</returns>
        public List<Title> TitleSelectByItem(int itemID)
        {
            return (new TitleDAL().TitleSelectByItem(null, null, itemID));
        }

        public List<TitleBibTeX> TitleBibTeXSelectAllTitleCitations()
        {
            return (new TitleDAL().TitleBibTeXSelectAllTitleCitations(null, null));
        }

        public List<TitleBibTeX> TitleBibTeXSelectAllItemCitations()
        {
            return (new TitleDAL().TitleBibTeXSelectAllItemCitations(null, null));
        }

        public List<TitleBibTeX> TitleBibTeXSelectForTitleID(int titleID)
        {
            return (new TitleDAL().TitleBibTeXSelectForTitleID(null, null, titleID));
        }

        public String TitleBibTeXGetCitationStringForTitleID(int titleID)
        {
            System.Text.StringBuilder bibtexString = new System.Text.StringBuilder("");
            List<TitleBibTeX> citations = this.TitleBibTeXSelectForTitleID(titleID);
            foreach (TitleBibTeX citation in citations)
            {
                List<TitleNote> titleNotes = this.TitleNoteSelectByTitleID(titleID);

                String volume = citation.Volume;
                String copyrightStatus = citation.CopyrightStatus;
                String url = citation.Url;
                String note = citation.Note;
                String pages = citation.Pages.ToString();
                String keywords = citation.Keywords;

                System.Collections.Generic.Dictionary<String, String> elements = new System.Collections.Generic.Dictionary<string, string>();
                elements.Add(BibTeXRefElementName.TITLE, citation.Title);
                if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
                if (copyrightStatus != String.Empty) elements.Add(BibTeXRefElementName.COPYRIGHT, copyrightStatus);
                if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
                foreach (TitleNote titleNote in titleNotes)
                {
                    if (note != string.Empty) note += " --- ";
                    note += titleNote.NoteText;
                }
                if (note != String.Empty) elements.Add(BibTeXRefElementName.NOTE, note);
                elements.Add(BibTeXRefElementName.PUBLISHER, citation.Publisher);
                elements.Add(BibTeXRefElementName.AUTHOR, citation.Authors.Replace("|", " and "));
                elements.Add(BibTeXRefElementName.YEAR, citation.Year);
                if (pages != String.Empty) elements.Add(BibTeXRefElementName.PAGES, pages);
                if (keywords != String.Empty) elements.Add(BibTeXRefElementName.KEYWORDS, keywords);

                BibTeX bibTex = new BibTeX(BibTeXRefType.BOOK, citation.CitationKey, elements);
                bibtexString.Append(bibTex.GenerateReference());
            }
            return bibtexString.ToString();
        }

        public List<RISCitation> TitleSelectAllRISCitations()
        {
            return new TitleDAL().TitleSelectAllRISCitations(null, null);
        }

        #endregion

        public List<Title> TitleSearchPaging(TitleSearchCriteria criteria)
        {
            if (criteria.SearchType == TitleSearchCriteria.SearchTarget.Item)
            {
                List<Title> titles = new List<Title>();

                if (criteria.ItemID != null || !string.IsNullOrWhiteSpace(criteria.SourceID))
                { 
                    Book book  = new BookDAL().BookSelectByBarCodeOrItemID(null, null, criteria.ItemID, criteria.SourceID);
                    if (book != null)
                    {
                        if (book.PrimaryTitleID != null)
                        {
                            Title title = new TitleDAL().TitleSelectAuto(null, null, (int)book.PrimaryTitleID);
                            if (title != null)
                            {
                                title.Books.Add(book);
                                titles.Add(title);
                            }
                        }
                    }
                }

                return titles;
            }
            else if (criteria.SearchType == TitleSearchCriteria.SearchTarget.Title)
            {
                return new TitleDAL().TitleSearch(null, null, criteria);
            }
            else
            {
                return new List<Title>();
            }
		}

		public int TitleSearchCount( TitleSearchCriteria criteria )
		{
			return new TitleDAL().TitleSearchCount( null, null, criteria );
		}

		public Title TitleUpdatePublishReady( int titleID, bool publishReady )
		{
			TitleDAL dal = new TitleDAL();
			Title title = dal.TitleSelectAuto( null, null, titleID );
			if ( title != null )
			{
				title.PublishReady = publishReady;
				title = dal.TitleUpdateAuto( null, null, title );
			}
			else
			{
				throw new Exception( "Could not find existing title record" );
			}
			return title;
		}

		public string NullIfEmpty( string value )
		{
			if ( value.Trim().Length == 0 )
			{
				return null;
			}
			else
			{
				return value;
			}
		}

		private TitleDAL GetTitleDalInstance()
		{
			if ( titleDal == null )
				titleDal = new TitleDAL();

			return titleDal;
		}

		public Title TitleSave( Title title, int userId, string userDescription = null)
		{
            if (!title.IsNew)
            {
                // Get existing title record
                Title existingTitle = TitleSelectAuto(title.TitleID);

                // Is title being deactivated and redirected?
                if ((!title.PublishReady && existingTitle.PublishReady) &&
                    (title.RedirectTitleID != null) &&
                    (title.RedirectTitleID != existingTitle.RedirectTitleID))
                {
                    title.Note += string.Format("{0}Replaced by {1}. {2} by {3}",
                        Environment.NewLine,
                        title.RedirectTitleID.ToString(),
                        DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        string.IsNullOrWhiteSpace(userDescription) ? "unknown" : userDescription);

                    Title targetTitle = TitleSelectAuto((int)title.RedirectTitleID);
                    targetTitle.Note += string.Format("{0}This title replaces {1}. {2} by {3}",
                        Environment.NewLine,
                        title.TitleID.ToString(),
                        DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        string.IsNullOrWhiteSpace(userDescription) ? "unknown" : userDescription);

                    // Copy TitleDocuments to the related title
                    if (title.TitleDocuments.Count > 0)
                    {
                        List<TitleDocument> targetTitleDocuments = new TitleDocumentDAL().TitleDocumentSelectByTitleID(null, null, targetTitle.TitleID);
                        foreach (TitleDocument doc in title.TitleDocuments)
                        {
                            bool exists = false;
                            foreach (TitleDocument targetDoc in targetTitleDocuments)
                            {
                                if (doc.Url == targetDoc.Url) { exists = true; break; }
                            }
                            if (!exists)
                            {
                                targetTitle.TitleDocuments.Add(new TitleDocument
                                {
                                    TitleID = targetTitle.TitleID,
                                    DocumentTypeID = doc.DocumentTypeID,
                                    Name = doc.Name,
                                    Url = doc.Url
                                });
                            }
                        }
                    }

                    // Copy Collections to the related title
                    if (title.TitleCollections.Count > 0)
                    {
                        List<TitleCollection> targetTitleCollections = new TitleCollectionDAL().SelectByTitle(null, null, targetTitle.TitleID);
                        foreach(TitleCollection tc in title.TitleCollections)
                        {
                            bool exists = false;
                            foreach(TitleCollection targetTC in targetTitleCollections)
                            {
                                if (tc.CollectionID == targetTC.CollectionID) { exists = true; break; }
                            }
                            if (!exists)
                            {
                                targetTitle.TitleCollections.Add(new TitleCollection
                                {
                                    TitleID = targetTitle.TitleID,
                                    CollectionID = tc.CollectionID
                                });
                            }
                        }
                    }

                    new TitleDAL().Save(null, null, targetTitle, userId);
                }
            }

            // Clean up the sort title
            title.SortTitle = DataCleaner.CleanSortTitle(title.SortTitle);

            return new TitleDAL().Save( null, null, title, userId );
		}

        public List<TitleSuspectCharacter> TitleSelectWithSuspectCharacters(String institutionCode, int maxAge)
        {
            return new TitleDAL().TitleSelectWithSuspectCharacters(null, null, institutionCode, maxAge);
        }

        public List<Title> TitleSelectByCollection(int collectionID)
        {
            return new TitleDAL().TitleSelectByCollection(null, null, collectionID);
        }
	}
}
