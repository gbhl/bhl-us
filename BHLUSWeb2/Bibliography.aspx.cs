using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class Bibliography : BasePage
    {
        public string Barcode { get; set; }
        public string Genre { get; set; }
        public string Material { get; set; }
        public string DDC { get; set; }
        public string LanguageName { get; set; }
        public string LocalLibraryUrl { get; set; }
        protected int TitleId { get; set; }

        protected Title BhlTitle { get; set; }
        protected IList<TitleVariant> TitleVariants { get; set; }
        protected IList<TitleAssociation> TitleAssociations { get; set; }
        protected List<TitleKeyword> TitleKeywords { get; set; }
        protected IList<Title_Identifier> TitleIdentifiers { get; set; }
        protected IList<TitleNote> TitleNotes { get; set; }
        protected IList<TitleExternalResource> TitleExternalResources { get; set; }
        protected IList<Author> Authors { get; set; }
        protected IList<Author> AdditionalAuthors { get; set; }
        protected IList<Author> AuthorsDetail { get; set; }
        protected IList<Author> AdditionalAuthorsDetail { get; set; }
        protected IList<BibliographyItem> BibliographyItems { get; set; }
        protected IList<Collection> Collections { get; set; }
        //protected IList<Institution> Institutions { get; set; }

        public class BibliographyItem
        {
            public DataObjects.Book Book { get; set; }
            public string ThumbUrl { get; set; }
            public List<Institution> institutions { get; set; }

            public BibliographyItem(DataObjects.Book book, string thumbUrl)
            {
                Book = book;
                ThumbUrl = thumbUrl;
                institutions = new List<Institution>();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Parse TitleID
            int titleId;
            if (!int.TryParse((string)RouteData.Values["titleid"], out titleId))
            {
                Response.Redirect("~/titlenotfound");
            }
            else
            {
                TitleId = titleId;
            }

            // Find Title
            BhlTitle = bhlProvider.TitleSelect(titleId);
            if (BhlTitle == null)
            {
                Response.Redirect("~/titlenotfound");
            }
            else
            {
                // Check to make sure this title hasn't been replaced.  If it has, redirect
                // to the appropriate titleid.
                if (BhlTitle.RedirectTitleID != null)
                {
                    Response.Redirect("~/bibliography/" + BhlTitle.RedirectTitleID);
                }

                // Make sure the title is published.
                if (!BhlTitle.PublishReady)
                {
                    Response.Redirect("~/titleunavailable");
                }
            }

            List<DataObjects.Book> Books = bhlProvider.BookSelectByTitleId(titleId);
            if (Books == null)
            {
                Response.Redirect("~/titleunavailable");
            }
            if (Books.Count == 0)
            {
                Response.Redirect("~/titleunavailable");
            }
            else
            {
                Barcode = Books[0].BarCode;
            }

            // Set the title for the COinS
            COinS.TitleID = titleId;

            // Assign Authors
            Authors = new List<Author>();
            AuthorsDetail = new List<Author>();
            AdditionalAuthors = new List<Author>();
            AdditionalAuthorsDetail = new List<Author>();
            foreach (Author author in bhlProvider.AuthorSelectByTitleId(titleId))
            {
                if (author.AuthorRoleID >= 1 && author.AuthorRoleID <= 3)
                {
                    AuthorsDetail.Add(author);
                    if (!ListContainsAuthor(Authors, author.AuthorID, author.Relationship)) Authors.Add(author);
                }
                else
                {
                    AdditionalAuthorsDetail.Add(author);
                    if (!ListContainsAuthor(Authors, author.AuthorID, author.Relationship) &&
                        !ListContainsAuthor(AdditionalAuthors, author.AuthorID, author.Relationship)) AdditionalAuthors.Add(author);
                }
            }

            TitleKeywords = bhlProvider.TitleKeywordSelectKeywordByTitle(titleId);
            TitleIdentifiers = bhlProvider.Title_IdentifierSelectForDisplayByTitleID(titleId);
            TitleVariants = bhlProvider.TitleVariantSelectByTitleID(titleId);
            TitleAssociations = bhlProvider.TitleAssociationSelectByTitleId(titleId, true);
            TitleNotes = bhlProvider.TitleNoteSelectByTitleID(titleId);
            TitleExternalResources = bhlProvider.TitleExternalResourceSelectByTitleID(titleId);
            Collections = bhlProvider.CollectionSelectAllForTitle(titleId);
            //Institutions = bhlProvider.InstitutionSelectByTitleID(titleId);
            if (!string.IsNullOrEmpty(BhlTitle.LanguageCode)) LanguageName = bhlProvider.LanguageSelectAuto(BhlTitle.LanguageCode).LanguageName;

            BibliographicLevel bibliographicLevel = bhlProvider.BibliographicLevelSelect(BhlTitle.BibliographicLevelID ?? 0);
            Genre = (bibliographicLevel == null) ? string.Empty : bibliographicLevel.BibliographicLevelLabel;

            MaterialType materialType = bhlProvider.MaterialTypeSelect(BhlTitle.MaterialTypeID ?? 0);
            Material = (materialType == null) ? string.Empty : materialType.MaterialTypeLabel;

            main.Page.Title = string.Format("Details - {0} - Biodiversity Heritage Library", BhlTitle.FullTitle);

            BibliographyItems = new List<BibliographyItem>();
            foreach (DataObjects.Book book in Books)
            {
                // Populate empty volume descriptions with default text
                if (string.IsNullOrWhiteSpace(book.Volume)) book.Volume = "Volume details";

                string externalUrl = (book.FirstPageID == null) ? "" : string.Format("/pagethumb/{0},100,100", book.FirstPageID.ToString());
                BibliographyItem bibliographyItem = new BibliographyItem(book, externalUrl);
                bibliographyItem.institutions = bhlProvider.InstitutionSelectByItemID((int)book.ItemID);

                BibliographyItems.Add(bibliographyItem);
            }

            // Look for an OCLC identifier (use the first one... might need to account for multiple at some point)
            bool oclcFound = false;
            LocalLibraryUrl = @"https://worldcat.org/";
            foreach (Title_Identifier titleIdentifier in TitleIdentifiers)
            {
                if (string.Equals(titleIdentifier.IdentifierName, "OCLC", StringComparison.OrdinalIgnoreCase))
                {
                    LocalLibraryUrl += "title/";
                    if (titleIdentifier.IdentifierValue.ToLower().StartsWith("ocm"))
                    {
                        //strip the "ocm" from the beginning of the value.
                        LocalLibraryUrl += titleIdentifier.IdentifierValue.Substring(3);
                    }
                    else
                    {
                        LocalLibraryUrl += titleIdentifier.IdentifierValue;
                    }
                    oclcFound = true;
                    break;
                }
            }
            if (!oclcFound)
            {
                string truncatedTitle = BhlTitle.FullTitle.Length > 220 ? BhlTitle.FullTitle.Substring(0, 220).Trim() : BhlTitle.FullTitle;
                LocalLibraryUrl += "search?q=" + truncatedTitle.Replace(" ", "+") + "&qt=owc_search";
            }

            // Look for any DDC call numbers for this title
            DDC = string.Empty;
            for (int x = TitleIdentifiers.Count - 1; x >= 0; x--)
            {
                Title_Identifier titleIdentifier = TitleIdentifiers[x];
                if (String.Compare(titleIdentifier.IdentifierName, "DDC", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    if (DDC.Length > 0) DDC += ", ";
                    DDC += titleIdentifier.IdentifierValue;

                    // Don't show this in the identifier list
                    TitleIdentifiers.Remove(titleIdentifier);
                }
            }
        }

        private bool ListContainsAuthor(IList<Author> list, int authorID, string relationship)
        {
            bool containsAuthor = false;

            foreach (Author author in list)
            {
                if (author.AuthorID == authorID && author.Relationship == relationship)
                {
                    containsAuthor = true;
                    break;
                }
            }

            return containsAuthor;
        }
    }
}