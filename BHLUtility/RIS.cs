using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.Utility
{
    public class RIS
    {
        #region Attributes

        private String _type = RISRefType.BOOK;

        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private List<Tuple<String, String>> _elements = new List<Tuple<string, string>>();

        public List<Tuple<String, String>> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        #endregion Attributes

        #region Constructors

        public RIS()
        {
        }

        public RIS(String type, List<Tuple<String, String>> elements)
        {
            this.Type = type;
            this.Elements = elements;
        }

        #endregion Constructors

        #region Methods

        public String GenerateReference()
        {
            StringBuilder reference = new StringBuilder();

            // Build the reference
            reference.Append(string.Format("{0}  - {1}\r\n", RISElementName.TYPEOFREFERENCE, this.Type));
            foreach (Tuple<string,string> element in this.Elements)
            {
                reference.Append(string.Format("{0}  - {1}\r\n", element.Item1, element.Item2));
            }
            reference.Append(string.Format("{0}  - \r\n", RISElementName.ENDOFREFERENCE));

            return reference.ToString();
        }

        #endregion
    }

    public static class RISRefType
    {
        public const string ARTICLE = "JOUR";
        public const string BOOK = "BOOK";
        public const string SERIAL = "SER";
        public const string FULLJOURNAL = "JFULL";
        public const string BOOKSECTION = "CHAP";
        public const string CONFERENCEPAPER = "CPAPER";
        public const string CONFERENCEPROCEEDING = "CONF";
        public const string MANUSCRIPT = "MANSCPT";
        public const string THESIS = "THES";
        public const string UNPUBLISHED = "UNPB";
    }

    public static class RISElementName
    {
        public const string TYPEOFREFERENCE = "TY";
        public const string ABSTRACT = "AB";
        public const string AUTHOR = "AU";
        public const string CALLNUMBER = "CN";
        public const string DATE = "DA";
        public const string DATABASEPROVIDER = "DP";
        public const string DOI = "DO";
        public const string EDITOR = "ED";
        public const string EDITION = "ET";
        public const string ENDPAGE = "EP";
        public const string ISSNISBN = "SN";
        public const string ISSUE = "IS";
        public const string KEYWORD = "KW";
        public const string LANGUAGE = "LA";
        public const string NOTES = "N1";
        public const string PUBLISHER = "PB";
        public const string PUBLISHINGPLACE = "CY";
        public const string REFERENCEID = "ID";
        public const string STARTPAGE = "SP";
        public const string TITLE = "TI";
        public const string TITLECONTAINER = "T2";
        public const string TITLESHORT = "ST";
        public const string TITLETRANSLATED = "TT";
        public const string URL = "UR";
        public const string VOLUME = "VL";
        public const string YEAR = "PY";
        public const string ENDOFREFERENCE = "ER";
    }
}
