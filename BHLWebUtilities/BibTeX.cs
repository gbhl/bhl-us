using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.Web.Utilities
{
    public class BibTeX
    {
        #region Attributes

        private String _type = BibTeXRefType.BOOK;

        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private String _citationKey = String.Empty;

        public String CitationKey
        {
            get { return _citationKey; }
            set { _citationKey = value; }
        }

        private Dictionary<String, String> _elements = new Dictionary<String, String>();

        public Dictionary<String, String> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        #endregion Attributes

        #region Constructors

        public BibTeX()
        {
        }

        public BibTeX(String type, String citationKey, Dictionary<String, String> elements)
        {
            this.Type = type;
            this.CitationKey = citationKey;
            this.Elements = elements;
        }

        #endregion Constructors

        #region Methods

        public String GenerateReference()
        {
            StringBuilder reference = new StringBuilder();

            // Validate properties
            if (this.CitationKey == String.Empty)
                throw new ArgumentException("Please specify a citation key value for the BibTex reference.");

            switch (this.Type)
            {
                case BibTeXRefType.MISC:
                    if (this.Elements.Count == 0)
                    {
                        throw new ArgumentException("Please specify at least one element for a '" + this.Type + "' BibTex reference.");
                    }
                    break;
                case BibTeXRefType.BOOK:
                    if (!((this.Elements.ContainsKey(BibTeXRefElementName.AUTHOR) || this.Elements.ContainsKey(BibTeXRefElementName.EDITOR)) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.TITLE) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.PUBLISHER) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.YEAR)))
                    {
                        throw new ArgumentException("A reference of type '" + this.Type + 
                            "' must include at least the following elements: '" + 
                            BibTeXRefElementName.AUTHOR + "/" + BibTeXRefElementName.EDITOR + "', '" + 
                            BibTeXRefElementName.TITLE + "', '" + 
                            BibTeXRefElementName.PUBLISHER + "', '" + 
                            BibTeXRefElementName.YEAR + "'.");
                    }
                    break;
                case BibTeXRefType.ARTICLE:
                    if (!(this.Elements.ContainsKey(BibTeXRefElementName.AUTHOR) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.TITLE) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.JOURNAL) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.YEAR)))
                    {
                        throw new ArgumentException("A reference of type '" + this.Type + 
                            "' must include at least the following elements: '" + 
                            BibTeXRefElementName.AUTHOR + "', '" + 
                            BibTeXRefElementName.TITLE + "', '" + 
                            BibTeXRefElementName.JOURNAL + "', '" + 
                            BibTeXRefElementName.YEAR + "'.");
                    }
                    break;
                case BibTeXRefType.INBOOK:
                    if (!((this.Elements.ContainsKey(BibTeXRefElementName.AUTHOR) || this.Elements.ContainsKey(BibTeXRefElementName.EDITOR)) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.TITLE) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.PAGES) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.PUBLISHER) &&
                        this.Elements.ContainsKey(BibTeXRefElementName.YEAR)))
                    {
                        throw new ArgumentException("A reference of type '" + this.Type + 
                            "' must include at least the following elements: '" + 
                            BibTeXRefElementName.AUTHOR + "/" + BibTeXRefElementName.EDITOR + "', '" + 
                            BibTeXRefElementName.TITLE + "', '" + 
                            BibTeXRefElementName.PAGES + "', '" +
                            BibTeXRefElementName.PUBLISHER + "', '" + 
                            BibTeXRefElementName.YEAR + "'.");
                    }
                    break;
                case "@booklet":
                case "@conference":
                case "@incollection":
                case "@inproceedings":
                case "@manual":
                case "@mastersthesis":
                case "@phdthesis":
                case "@proceedings":
                case "@techreport":
                case "@unpublished":
                    throw new NotImplementedException("'" + this.Type + "' references have not been implemented.");
                default:
                    throw new ArgumentException("'" + this.Type + "' is not a valid BibTeX Type.");
            }

            // Build the reference
            reference.Append(this.Type);
            reference.Append("{");
            reference.Append(this.CitationKey);
            reference.Append(",\n");
            foreach (String key in this.Elements.Keys)
            {
                //// Need a separate BibTex element for each keyword
                //if (key == BibTeXRefElementName.KEYWORDS)
                //{
                //    String[] keywords = this.Elements[key].Split('|');
                //    foreach (String keyword in keywords)
                //    {
                //        if (keyword.Trim() != String.Empty)
                //        {
                //            reference.Append("\t");
                //            reference.Append(key);
                //            reference.Append(" = {");
                //            reference.Append(keyword);
                //            reference.Append("},\n");
                //        }
                //    }
                //}
                //else
                //{
                    reference.Append("\t");
                    reference.Append(key);
                    reference.Append(" = {");
                    reference.Append(this.Elements[key]);
                    reference.Append("},\n");
                //}
            }
            reference.Append("}\n\n");

            return reference.ToString();
        }

        #endregion

    }

    public static class BibTeXRefType
    {
        public const String ARTICLE = "@article";
        public const String BOOK = "@book";
        public const String INBOOK = "@inbook";
        public const String MISC = "@misc";

        // We haven't implemented the following types.  Uncomment these 
        // when they are implemented.
        /*
        public const String BOOKLET = "@booklet";
        public const String CONFERENCE = "@conference";
        public const String INCOLLECTION = "@incollection";
        public const String INPROCEEDINGS = "@inproceedings";
        public const String MANUAL = "@manual";
        public const String MASTERSTHESIS = "@mastersthesis";
        public const String PHDTHESIS = "@phdthesis";
        public const String PROCEEDINGS = "@proceedings";
        public const String TECHREPORT = "@techreport";
        public const String UNPUBLISHED = "@unpublished";
         */
    }

    public static class BibTeXRefElementName
    {
        public const String AUTHOR = "author";
        public const String EDITOR = "editor";
        public const String BOOKTITLE = "booktitle";
        public const String TITLE = "title";
        public const String JOURNAL = "journal";
        public const String PUBLISHER = "publisher";
        public const String SCHOOL = "school";
        public const String YEAR = "year";
        public const String VOLUME = "volume";
        public const String CHAPTER = "chapter";
        public const String NUMBER = "number";
        public const String SERIES = "series";
        public const String ADDRESS = "address";
        public const String EDITION = "edition";
        public const String MONTH = "month";
        public const String NOTE = "note";
        public const String KEY = "key";
        public const String PAGES = "pages";
        public const String ABSTRACT = "abstract";
        public const String KEYWORDS = "keywords";
        public const String ISBN = "isbn";
        public const String TYPEOFWORK = "type";
        public const String COPYRIGHT = "copyright";
        public const String HOWPUBLISHED = "howpublished";
        public const String ORGANIZATION = "organization";
        public const String INSTITUTION = "institution";
        public const String URL = "url";
    }
}
