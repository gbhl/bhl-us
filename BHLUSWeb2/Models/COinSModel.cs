using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

namespace MOBOT.BHL.Web2.Models
{
    public class COinSModel
    {
        public int TitleID { get; set; } = 0;
        public int ItemID { get; set; } = 0;
        public int SegmentID { get; set; } = 0;
        public List<Title_Identifier> TitleIdentifiers { get; set; } = new List<Title_Identifier>();
        public List<ItemIdentifier> ItemIdentifiers { get; set; } = new List<ItemIdentifier>();
        public List<TitleKeyword> TitleKeywords { get; set; } = new List<TitleKeyword>();
        public List<ItemKeyword> ItemKeywords { get; set; } = new List<ItemKeyword>();
        public List<Author> TitleAuthors { get; set; }
        public List<ItemAuthor> ItemAuthors { get; set; }
        public string Genre { get; set; }
        public string MarcLeader { get; set; }
        public string Title { get; set; }
        public string ArticleTitle { get; set; }
        public string Volume { get; set; }
        public string Issue { get; set; }
        public string Publisher { get; set; }
        public string PublisherPlace { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
        public string Date { get; set; }
        public string StartPageNumber { get; set; }
        public string EndPageNumber { get; set; }
        public string PageRange { get; set; }
        public int? PageCount { get; set; }

        public string GetCOinS()
        {
            string coinsOutput = string.Empty;

            if ((TitleID != 0) || (ItemID != 0)) coinsOutput = GetCOinSForItem();
            if (SegmentID != 0) coinsOutput = GetCOinSForSegment();

            return coinsOutput;
        }

        /// <summary>
        /// Get the COinS string for the specified title/item
        /// </summary>
        /// <returns></returns>
        private string GetCOinSForItem()
        {
            StringBuilder output = new StringBuilder();

            // Build the COinS
            output.Append("ctx_ver=Z39.88-2004");

            // Add identifiers
            string doi = GetIdentifierValue(IdentifierTarget.Title, "DOI");
            if (doi != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("info:doi/" + doi));
            string oclc = GetIdentifierValue(IdentifierTarget.Title, "OCLC");
            if (oclc != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("info:oclcnum/" + oclc));
            string issn = GetIdentifierValue(IdentifierTarget.Title, "ISSN");
            if (issn != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("urn:ISSN:" + issn));
            string eissn = GetIdentifierValue(IdentifierTarget.Title, "eISSN");
            if (eissn != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("urn:ISSN:" + eissn));
            string isbn = GetIdentifierValue(IdentifierTarget.Title, "ISBN");
            if (isbn != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("urn:ISBN:" + isbn));
            string lccn = GetIdentifierValue(IdentifierTarget.Title, "DLC");
            if (lccn != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("info:lccn/" + lccn));
            if (TitleID != 0) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode(string.Format(ConfigurationManager.AppSettings["BibPageUrl"], TitleID)));
            if (ItemID != 0) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode(string.Format(ConfigurationManager.AppSettings["ItemPageUrl"], ItemID)));

            // Add format-specific attributes
            switch (GetGenre())
            {
                // Journal COinS do not work with Mendeley or Zotero unless they represent journal articles.
                // Per discussion with BHL technical director, decided to represent full journal volumes as books.
                case "book":    // book
                case "journal":     // journal
                    {
                        output.Append("&amp;rft_val_fmt=" + HttpUtility.UrlEncode("info:ofi/fmt:kev:mtx:book"));
                        output.Append("&amp;rft.genre=book");
                        if (!string.IsNullOrWhiteSpace(Title)) output.Append("&amp;rft.btitle=" + HttpUtility.UrlEncode(Title));

                        // Rft_stitle, Rft_volume, and Rft_coden were added for journals, even 
                        // though they don't technically make sense for book COinS
                        string stitle = GetIdentifierValue(IdentifierTarget.Title, "Abbreviation");
                        if (stitle != string.Empty) output.Append("&amp;rft.stitle=" + HttpUtility.UrlEncode(stitle));
                        if (!string.IsNullOrWhiteSpace(Volume) && ItemID != 0) output.Append("&amp;rft.volume=" + HttpUtility.UrlEncode(Volume));
                        string coden = GetIdentifierValue(IdentifierTarget.Title, "CODEN");
                        if (coden != string.Empty) output.Append("&amp;rft.coden=" + HttpUtility.UrlEncode(coden));

                        if (!string.IsNullOrWhiteSpace(PublisherPlace)) output.Append("&amp;rft.place=" + HttpUtility.UrlEncode(PublisherPlace));
                        if (!string.IsNullOrWhiteSpace(Publisher)) output.Append("&amp;rft.pub=" + HttpUtility.UrlEncode(Publisher));
                        if (!string.IsNullOrWhiteSpace(Edition)) output.Append("&amp;rft.edition=" + HttpUtility.UrlEncode(Edition));

                        if (issn != string.Empty) output.Append("&amp;rft.issn=" + HttpUtility.UrlEncode(issn));
                        if (eissn != string.Empty) output.Append("&amp;rft.eissn=" + HttpUtility.UrlEncode(eissn));
                        if (isbn != string.Empty) output.Append("&amp;rft.isbn=" + HttpUtility.UrlEncode(isbn));
                        bool isFirst = true;
                        foreach (Author author in TitleAuthors)
                        {
                            if (isFirst)
                            {
                                string firstName = GetAuthorFirstName(author.FullName, author.AuthorRoleID);
                                if (firstName != string.Empty) output.Append("&amp;rft.aufirst=" + HttpUtility.UrlEncode(firstName));
                                string lastName = GetAuthorLastName(author.FullName, author.AuthorRoleID);
                                if (lastName != string.Empty) output.Append("&amp;rft.aulast=" + HttpUtility.UrlEncode(lastName));
                                string corpName = GetAuthorCorpName(author.FullName, author.AuthorRoleID);
                                if (corpName != string.Empty) output.Append("&amp;rft.aucorp=" + HttpUtility.UrlEncode(corpName));
                                isFirst = false;
                            }
                            output.Append("&amp;rft.au=" + HttpUtility.UrlEncode(author.FullName));
                        }
                        if ((PageCount ?? 0) != 0)
                        {
                            output.Append("&amp;rft.pages=1-" + PageCount.ToString());
                            output.Append("&amp;rft.tpages=" + PageCount.ToString());
                        }

                        break;
                    }
                default:    // dublin core
                    {
                        output.Append("&amp;rft_val_fmt=" + HttpUtility.UrlEncode("info:ofi/fmt:kev:mtx:dc"));
                        output.Append("&amp;rft.source=" + HttpUtility.UrlEncode("Biodiversity Heritage Library"));
                        output.Append("&amp;rft.rights=" + HttpUtility.UrlEncode("Creative Commons Attribution 3.0"));
                        if (TitleID != 0) output.Append("&amp;rft.identifier=" + HttpUtility.UrlEncode(string.Format(ConfigurationManager.AppSettings["BibPageUrl"], TitleID)));
                        if (ItemID != 0) output.Append("&amp;rft.identifier=" + HttpUtility.UrlEncode(string.Format(ConfigurationManager.AppSettings["ItemPageUrl"], ItemID)));
                        if (Title != string.Empty) output.Append("&amp;rft.title=" + HttpUtility.UrlEncode(Title));
                        if (Language != string.Empty) output.Append("&amp;rft.language=" + HttpUtility.UrlEncode(Language));
                        foreach (Author author in TitleAuthors)
                        {
                            output.Append("&amp;rft.creator=" + HttpUtility.UrlEncode(author.FullName));
                        }
                        if (!string.IsNullOrWhiteSpace(Publisher)) output.Append("&amp;rft.publisher=" + HttpUtility.UrlEncode(Publisher));
                        foreach (TitleKeyword keyword in TitleKeywords)
                        {
                            output.Append("&amp;rft.subject=" + HttpUtility.UrlEncode(keyword.Keyword));
                        }

                        break;
                    }
            }

            // Add additional elements common to all formats
            if (Date != string.Empty) output.Append("&amp;rft.date=" + HttpUtility.UrlEncode(Date));

            return output.ToString();
        }

        /// <summary>
        /// Get the COinS string for the specified segment
        /// </summary>
        /// <returns></returns>
        private string GetCOinSForSegment()
        {
            StringBuilder output = new StringBuilder();

            // Build the COinS
            output.Append("ctx_ver=Z39.88-2004");

            // Add identifiers
            string doi = GetIdentifierValue(IdentifierTarget.Segment, "DOI");
            if (doi != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("info:doi/" + doi));
            string issn = GetIdentifierValue(IdentifierTarget.Title, "ISSN");
            if (issn != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("urn:ISSN:" + issn));
            string eissn = GetIdentifierValue(IdentifierTarget.Title, "eISSN");
            if (eissn != string.Empty) output.Append("&amp;rft_id=" + HttpUtility.UrlEncode("urn:ISSN:" + eissn));
            output.Append("&amp;rft_id=" + HttpUtility.UrlEncode(string.Format(ConfigurationManager.AppSettings["PartPageUrl"], SegmentID)));

            // Add format-specific attributes
            switch (GetGenre())
            {
                case "article":
                case "issue":
                case "proceeding":
                case "conference":
                case "preprint":
                case "unknown":
                    {
                        output.Append("&amp;rft_val_fmt=" + HttpUtility.UrlEncode("info:ofi/fmt:kev:mtx:journal"));
                        output.Append("&amp;rft.genre=" + HttpUtility.UrlEncode(GetGenre()));
                        if (!string.IsNullOrWhiteSpace(ArticleTitle)) output.Append("&amp;rft.atitle=" + HttpUtility.UrlEncode(ArticleTitle));
                        if (!string.IsNullOrWhiteSpace(Title)) output.Append("&amp;rft.jtitle=" + HttpUtility.UrlEncode(Title));
                        if (!string.IsNullOrWhiteSpace(Volume)) output.Append("&amp;rft.volume=" + HttpUtility.UrlEncode(Volume));
                        if (!string.IsNullOrWhiteSpace(Issue)) output.Append("&amp;rft.issue=" + HttpUtility.UrlEncode(Issue));
                        if (!string.IsNullOrWhiteSpace(StartPageNumber)) output.Append("&amp;rft.spage=" + HttpUtility.UrlEncode(StartPageNumber));
                        if (!string.IsNullOrWhiteSpace(EndPageNumber)) output.Append("&amp;rft.epage=" + HttpUtility.UrlEncode(EndPageNumber));
                        if (!string.IsNullOrWhiteSpace(PageRange)) output.Append("&amp;rft.pages=" + HttpUtility.UrlEncode(PageRange));
                        if (issn != string.Empty) output.Append("&amp;rft.issn=" + HttpUtility.UrlEncode(issn));
                        if (eissn != string.Empty) output.Append("&amp;rft.eissn=" + HttpUtility.UrlEncode(eissn));
                        bool isFirst = true;
                        foreach (ItemAuthor author in ItemAuthors)
                        {
                            if (isFirst)
                            {
                                string firstName = GetAuthorFirstName(author.FullName);
                                if (firstName != string.Empty) output.Append("&amp;rft.aufirst=" + HttpUtility.UrlEncode(firstName));
                                string lastName = GetAuthorLastName(author.FullName);
                                if (lastName != string.Empty) output.Append("&amp;rft.aulast=" + HttpUtility.UrlEncode(lastName));
                                isFirst = false;
                            }
                            output.Append("&amp;rft.au=" + HttpUtility.UrlEncode(author.FullName));
                        }

                        break;
                    }
                default:    // dublin core
                    {
                        output.Append("&amp;rft_val_fmt=" + HttpUtility.UrlEncode("info:ofi/fmt:kev:mtx:dc"));
                        output.Append("&amp;rft.source=" + HttpUtility.UrlEncode("Biodiversity Heritage Library"));
                        output.Append("&amp;rft.rights=" + HttpUtility.UrlEncode("Creative Commons Attribution 3.0"));
                        output.Append("&amp;rft.identifier=" + HttpUtility.UrlEncode(string.Format(ConfigurationManager.AppSettings["PartPageUrl"], SegmentID)));
                        if (!string.IsNullOrWhiteSpace(ArticleTitle)) output.Append("&amp;rft.title=" + HttpUtility.UrlEncode(ArticleTitle));
                        if (!string.IsNullOrWhiteSpace(Language)) output.Append("&amp;rft.language=" + HttpUtility.UrlEncode(Language));
                        foreach (ItemAuthor author in ItemAuthors)
                        {
                            output.Append("&amp;rft.creator=" + HttpUtility.UrlEncode(author.FullName));
                        }
                        foreach (ItemKeyword keyword in ItemKeywords)
                        {
                            output.Append("&amp;rft.subject=" + HttpUtility.UrlEncode(keyword.Keyword));
                        }

                        break;
                    }
            }

            // Add additional elements common to all formats
            if (Date != string.Empty) output.Append("&amp;rft.date=" + HttpUtility.UrlEncode(Date));

            return output.ToString();
        }

        private string GetGenre()
        {
            string genre = "unknown";
            if (!string.IsNullOrWhiteSpace(Genre))
            {
                genre = Genre;
            }
            else if (MarcLeader != null)
            {
                switch (MarcLeader.Substring(7, 1))
                {
                    case "s":
                    case "b":
                        genre = "journal"; break;
                    case "a":
                    case "m":
                        genre = "book"; break;
                }
            }
            return genre.ToLower();
        }

        private string GetAuthorFirstName(string fullName, int authorRoleID = 0)
        {
            string firstName = string.Empty;
            if (authorRoleID <= 1)
            {
                if (fullName.Contains(",")) firstName = fullName.Split(',')[1];
            }
            return firstName;
        }

        private string GetAuthorLastName(string fullName, int authorRoleID = 0)
        {
            string lastName = string.Empty;
            if (authorRoleID <= 1)
            {
                if (fullName.Contains(","))
                    lastName = fullName.Split(',')[0];
                else
                    lastName = fullName;
            }
            return lastName;
        }

        private string GetAuthorCorpName(string fullName, int authorRoleID = 0)
        {
            string name = string.Empty;
            if (authorRoleID > 1) name = fullName;
            return name;
        }

        private string GetIdentifierValue(IdentifierTarget idTarget, string identifierName)
        {
            string identifierValue = string.Empty;

            if (idTarget == IdentifierTarget.Title)
            {
                foreach (Title_Identifier titleIdentifier in TitleIdentifiers)
                {
                    if (titleIdentifier.IdentifierName == identifierName)
                    {
                        identifierValue = titleIdentifier.IdentifierValue;
                        break;
                    }
                }
            }
            else if (idTarget == IdentifierTarget.Segment)
            {
                foreach (ItemIdentifier itemIdentifier in ItemIdentifiers)
                {
                    if (itemIdentifier.IdentifierName == identifierName)
                    {
                        identifierValue = itemIdentifier.IdentifierValue;
                        break;
                    }
                }
            }

            return identifierValue;
        }

        private enum IdentifierTarget
        {
            Title,
            Segment
        }
    }
}