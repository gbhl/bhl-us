using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.Web.Utilities
{
    public class EndNote
    {
        #region Attributes

        private String _type = EndNoteRefType.BOOK;

        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private Dictionary<String, String> _elements = new Dictionary<String, String>();

        public Dictionary<String, String> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        #endregion Attributes

        #region Constructors

        public EndNote()
        {
        }

        public EndNote(String type, Dictionary<String, String> elements)
        {
            this.Type = type;
            this.Elements = elements;
        }

        #endregion Constructors

        #region Methods

        public String GenerateReference()
        {
            StringBuilder reference = new StringBuilder();

            // Validate properties
            if (this.Elements.Count == 0)
            {
                throw new ArgumentException("Please specify at least one element for a '" + this.Type + "' EndNote reference.");
            }

            switch (this.Type)
            {
                case EndNoteRefType.GENERIC:
                    break;
                case EndNoteRefType.BOOK:
                    break;
                case EndNoteRefType.SERIAL:
                    break;
                case EndNoteRefType.ARTICLE:
                    break;
                case EndNoteRefType.BOOKSECTION:
                    break;
                case "Edited Book":
                case "Electronic Article":
                case "Electronic Book":
                case "Conference Paper":
                case "Conference Proceedings":
                case "Magazine Article":
                case "Report":
                case "Thesis":
                case "Unpublished Work":
                    throw new NotImplementedException("'" + this.Type + "' references have not been implemented.");
                default:
                    throw new ArgumentException("'" + this.Type + "' is not a valid EndNote Reference Type.");
            }

            // Build the reference
            reference.Append(EndNoteRefElementName.REFERENCETYPE);
            reference.Append(this.Type);
            reference.Append("\n");
            foreach (String key in this.Elements.Keys)
            {
                // Need a separate EndNote element for each keyword and author
                if (key == EndNoteRefElementName.KEYWORDS || key == EndNoteRefElementName.AUTHORS)
                {
                    String[] values = this.Elements[key].Split('|');
                    foreach (String value in values)
                    {
                        if (value.Trim() != String.Empty)
                        {
                            reference.Append(key);
                            reference.Append(value);
                            reference.Append("\n");
                        }
                    }
                }
                else
                {
                    reference.Append(key);
                    reference.Append(this.Elements[key]);
                    reference.Append("\n");
                }
            }
            reference.Append("\n\n\n");

            return reference.ToString();
        }

        #endregion

    }

    public static class EndNoteRefType
    {
        public const String ARTICLE = "Journal Article";
        public const String BOOKSECTION = "Book Section";
        public const String BOOK = "Book";
        public const String SERIAL = "Serial";
        public const String GENERIC = "Generic";

        // We haven't implemented the following types.  Uncomment these 
        // when they are implemented.
        /*
        public const String EDITEDBOOK = "Edited Book";
        public const String ELECTRONICARTICLE = "Electronic Article";
        public const String ELECTRONICBOOK = "Electronic Book";
        public const String CONFERENCEPAPER = "Conference Paper";
        public const String CONFERENCEPROCEEDINGS = "Conference Proceedings";
        public const String MAGAZINEARTICLE = "Magazine Article";
        public const String REPORT = "Report";
        public const String THESIS = "Thesis";
        public const String UNPUBLISHEDWORK = "Unpublished Work";
         */
    }

    public static class EndNoteRefElementName
    {
        public const String REFERENCETYPE = "%0 ";
        public const String AUTHORS = "%A ";
        public const String TITLE = "%T ";
        public const String SECONDARYTITLE = "%B ";
        public const String SHORTTITLE = "%! ";
        public const String YEAR = "%D ";                   // year published
        public const String CITY = "%C ";                   // publisher place
        public const String PUBLISHER = "%I ";              // publisher name
        public const String VOLUME = "%V ";
        public const String ABBREVIATION = "%O ";
        public const String ISBNISSN = "%@ ";
        public const String CALLNUMBER = "%L ";
        public const String KEYWORDS = "%K ";
        public const String LANGUAGE = "%G ";
        public const String URL = "%U ";
        public const String NOTE = "%Z ";
        public const String PAGES = "%P ";
        public const String ABSTRACT = "%X ";
        public const String DOI = "%R ";
        public const String JOURNAL = "%J ";
        public const String ISSUE = "%N ";
        public const String STARTPAGE = "%& ";              // article start page
        public const String EPUBDATE = "%7 ";               // journal article e-publishing date
        public const String ARTICLEDATE = "%8 ";            // article publication date
        public const String ACCESSIONNUMBER = "%M ";
        public const String LABEL = "%F ";
        public const String EDITOR = "%E ";
        public const String TYPEOFWORK = "%9 ";
        public const String CHAPTER = "%& ";
        public const String SERIESEDITOR = "%Y ";
        public const String SERIESTITLE = "%S ";
        public const String EDITION = "%7 ";
        public const String SERIALDATE = "%8 ";
        public const String VOLUMEEDITOR = "%? ";
        public const String BOOKTRANSLATOR = "%? ";
        public const String SECTION = "%1 ";
    }
}
