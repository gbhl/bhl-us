using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
		public List<Book> BookSelectByTitleId(int titleID)
		{
			return new BookDAL().BookSelectByTitleID(null, null, titleID);
		}

		public Book BookSelectAuto(int itemID)
		{
			BookDAL dal = new BookDAL();
			return dal.BookSelectAuto(null, null, itemID);
		}

		public Book BookSelectOAIDetail(int bookID)
		{
			return new BookDAL().BookSelectOAIDetail(null, null, bookID);
		}

		public Book BookSelectTextPathForItemID(int itemID)
		{
			return new BookDAL().BookSelectTextPathForItemID(null, null, itemID);
		}

		public Book BookSelectTextPathForSegmentID(int segmentID)
		{
			return new BookDAL().BookSelectTextPathForSegmentID(null, null, segmentID);
		}

		public List<Book> BookSelectRecent(int top, string languageCode, string institutionCode)
		{
			return new BookDAL().BookSelectRecent(null, null, top, languageCode, institutionCode);
		}

		public List<Book> BookSelectByCollection(int collectionID)
		{
			return new BookDAL().BookSelectByCollection(null, null, collectionID);
		}

		public Book BookSelectByItemID(int itemID)
		{
			return new BookDAL().BookSelectByItemID(null, null, itemID);
		}

		public Book BookSelectByPageID(int pageID)
		{
			return new BookDAL().BookSelectByPageID(null, null, pageID);
		}

		public Book BookUpdatePaginationStatus(int bookID, int paginationStatusID, int userID)
		{
			BookDAL dal = new BookDAL();
			Book savedBook = dal.BookSelectAuto(null, null, bookID);
			if (savedBook != null)
			{
				savedBook.PaginationStatusID = paginationStatusID;
				savedBook.PaginationStatusUserID = userID;
				savedBook.PaginationStatusDate = DateTime.Now;
				savedBook = dal.BookUpdateAuto(null, null, savedBook);
			}
			else
			{
				throw new Exception("Could not find existing Book record.");
			}
			return savedBook;
		}

		public List<Book> BookSelectRecentlyChanged(string startDate)
		{
			return new BookDAL().BookSelectRecentlyChanged(null, null, startDate);
		}

		public List<Book> BookSelectByInstitution(string institutionCode, int returnCode, string sortBy)
		{
			return new BookDAL().BookSelectByInstitution(null, null, institutionCode, returnCode, sortBy);
		}

		public List<Book> BookSelectByInstitutionAndRole(string institutionCode, int institutionRoleID, string barcode, int numRows, int pageNum, string sortColumn, string sortOrder)
		{
			return new BookDAL().BookSelectByInstitutionAndRole(null, null, institutionCode, institutionRoleID, barcode, numRows, pageNum, sortColumn, sortOrder);
		}

		public Item BookSelectPagination(int bookID)
		{
			return new BookDAL().BookSelectPagination(null, null, bookID);
		}

		public string BookSelectRISCitationStringForBookID(int bookID)
		{
			System.Text.StringBuilder risString = new System.Text.StringBuilder("");
			List<RISCitation> citations = new BookDAL().BookSelectRISCitationsForItemID(null, null, bookID);
			foreach (RISCitation citation in citations)
			{
				risString.Append(this.GenerateRISCitation(citation));
			}
			return risString.ToString();
		}

		public List<TitleBibTeX> BookBibTeXSelectForBookID(int bookID)
		{
			return (new BookDAL().BookBibTeXSelectForBookID(null, null, bookID));
		}

		public String BookBibTeXGetCitationStringForBookID(int bookID)
		{
			System.Text.StringBuilder bibtexString = new System.Text.StringBuilder("");
			List<TitleBibTeX> citations = this.BookBibTeXSelectForBookID(bookID);
			foreach (TitleBibTeX citation in citations)
			{
				//List<TitleNote> titleNotes = this.TitleNoteSelectByTitleID(titleID);

				String volume = citation.Volume;
				String copyrightStatus = citation.CopyrightStatus;
				String url = citation.Url;
				String note = citation.Note;
				String pages = citation.Pages.ToString();
				String keywords = citation.Keywords;

				Dictionary<String, String> elements = new Dictionary<string, string>();
				elements.Add(BibTeXRefElementName.TITLE, citation.Title);
				if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
				if (copyrightStatus != String.Empty) elements.Add(BibTeXRefElementName.COPYRIGHT, copyrightStatus);
				if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
				/*
				foreach (TitleNote titleNote in titleNotes)
				{
					if (note != string.Empty) note += " --- ";
					note += titleNote.NoteText;
				}
				*/
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

		public void BookSave(Book book, int userId)
		{
			book.StartYear = DataCleaner.CleanYear(book.StartYear);
			book.EndYear = DataCleaner.CleanYear(book.EndYear);

			// Parse the volume into its component parts.
			// NOTE: Once a UI for the component parts of the volume string is available, the parsing should probably be removed from here.
			VolumeData volumeData = DataCleaner.ParseVolumeString(book.Volume);
			book.StartYear = string.IsNullOrWhiteSpace(book.StartYear) && string.IsNullOrWhiteSpace(book.EndYear) ? volumeData.StartYear : book.StartYear;
			book.EndYear = string.IsNullOrWhiteSpace(book.StartYear) && string.IsNullOrWhiteSpace(book.EndYear) ? volumeData.EndYear : book.EndYear;
			book.StartVolume = volumeData.StartVolume;
			book.EndVolume = volumeData.EndVolume;
			book.StartIssue = volumeData.StartIssue;
			book.EndIssue = volumeData.EndIssue;
			book.StartPart = volumeData.StartPart;
			book.EndPart = volumeData.EndPart;
			book.StartNumber = volumeData.StartNumber;
			book.EndNumber = volumeData.EndNumber;
			book.StartSeries = volumeData.StartSeries;
			book.EndSeries = volumeData.EndSeries;

			new BookDAL().Save(null, null, book, userId);
		}
	}
}
