using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class BibliographyModel
    {
        public string Barcode { get; set; }
        public string Genre { get; set; }
        public string Material { get; set; }
        public string DDC { get; set; }
        public string LanguageName { get; set; }
        public string LocalLibraryUrl { get; set; }

        public Title Title { get; set; }
        public IList<TitleVariant> TitleVariants { get; set; }
        public IList<TitleAssociation> TitleAssociations { get; set; }
        public List<TitleKeyword> TitleKeywords { get; set; }
        public List<Title_Identifier> TitleIdentifiers { get; set; }
        public IList<TitleNote> TitleNotes { get; set; }
        public IList<TitleExternalResource> TitleExternalResources { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Author> AdditionalAuthors { get; set; }
        public IList<Author> AuthorsDetail { get; set; }
        public IList<Author> AdditionalAuthorsDetail { get; set; }
        public IList<BibliographyItem> BibliographyItems { get; set; }
        public IList<Collection> Collections { get; set; }
        public COinSModel COinS { get; set; } = new COinSModel();
    }
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

}