using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace MOBOT.BHL.Web2
{
    public partial class COinSControl : UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string coinsOutput = string.Empty;

                if ((TitleID != 0) || (ItemID != 0)) coinsOutput = GetCOinSForItem();
                if (SegmentID != 0) coinsOutput = GetCOinSForSegment();

                // Render the COinS to the control
                Controls.Add(new LiteralControl("<span class=\"Z3988\" title=\"" + coinsOutput + "\"></span>"));
            }
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
            if (doi != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:doi/" + doi));
            string oclc = GetIdentifierValue(IdentifierTarget.Title, "OCLC");
            if (oclc != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:oclcnum/" + oclc));
            string issn = GetIdentifierValue(IdentifierTarget.Title, "ISSN");
            if (issn != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISSN:" + issn));
            string eissn = GetIdentifierValue(IdentifierTarget.Title, "eISSN");
            if (eissn != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISSN:" + eissn));
            string isbn = GetIdentifierValue(IdentifierTarget.Title, "ISBN");
            if (isbn != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISBN:" + isbn));
            string lccn = GetIdentifierValue(IdentifierTarget.Title, "DLC");
            if (lccn != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:lccn/" + lccn));
            if (TitleID != 0) output.Append("&amp;rft_id=" + Server.UrlEncode(string.Format(AppConfig.BibPageUrl, TitleID)));
            if (ItemID != 0) output.Append("&amp;rft_id=" + Server.UrlEncode(string.Format(AppConfig.ItemPageUrl, ItemID)));

            // Add format-specific attributes
            switch (GetGenre())
            {
                // Journal COinS do not work with Mendeley or Zotero unless they represent journal articles.
                // Per discussion with BHL technical director, decided to represent full journal volumes as books.
                case "book":    // book
                case "journal":     // journal
                    {
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:book"));
                        output.Append("&amp;rft.genre=book");
                        if (!string.IsNullOrWhiteSpace(Title)) output.Append("&amp;rft.btitle=" + Server.UrlEncode(Title));

                        // Rft_stitle, Rft_volume, and Rft_coden were added for journals, even 
                        // though they don't technically make sense for book COinS
                        string stitle = GetIdentifierValue(IdentifierTarget.Title, "Abbreviation");
                        if (stitle != string.Empty) output.Append("&amp;rft.stitle=" + Server.UrlEncode(stitle));
                        if (!string.IsNullOrWhiteSpace(Volume) && ItemID != 0) output.Append("&amp;rft.volume=" + Server.UrlEncode(Volume));
                        string coden = GetIdentifierValue(IdentifierTarget.Title, "CODEN");
                        if (coden != string.Empty) output.Append("&amp;rft.coden=" + Server.UrlEncode(coden));

                        if (!string.IsNullOrWhiteSpace(PublisherPlace)) output.Append("&amp;rft.place=" + Server.UrlEncode(PublisherPlace));
                        if (!string.IsNullOrWhiteSpace(Publisher)) output.Append("&amp;rft.pub=" + Server.UrlEncode(Publisher));
                        if (!string.IsNullOrWhiteSpace(Edition)) output.Append("&amp;rft.edition=" + Server.UrlEncode(Edition));

                        if (issn != string.Empty) output.Append("&amp;rft.issn=" + Server.UrlEncode(issn));
                        if (eissn != string.Empty) output.Append("&amp;rft.eissn=" + Server.UrlEncode(eissn));
                        if (isbn != string.Empty) output.Append("&amp;rft.isbn=" + Server.UrlEncode(isbn));
                        bool isFirst = true;
                        foreach (Author author in TitleAuthors)
                        {
                            if (isFirst)
                            {
                                string firstName = GetAuthorFirstName(author.FullName, author.AuthorRoleID);
                                if (firstName != string.Empty) output.Append("&amp;rft.aufirst=" + Server.UrlEncode(firstName));
                                string lastName = GetAuthorLastName(author.FullName, author.AuthorRoleID);
                                if (lastName != string.Empty) output.Append("&amp;rft.aulast=" + Server.UrlEncode(lastName));
                                string corpName = GetAuthorCorpName(author.FullName, author.AuthorRoleID);
                                if (corpName != string.Empty) output.Append("&amp;rft.aucorp=" + Server.UrlEncode(corpName));
                                isFirst = false;
                            }
                            output.Append("&amp;rft.au=" + Server.UrlEncode(author.FullName));
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
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:dc"));
                        output.Append("&amp;rft.source=" + Server.UrlEncode("Biodiversity Heritage Library"));
                        output.Append("&amp;rft.rights=" + Server.UrlEncode("Creative Commons Attribution 3.0"));
                        if (TitleID != 0) output.Append("&amp;rft.identifier=" + Server.UrlEncode(string.Format(AppConfig.BibPageUrl, TitleID)));
                        if (ItemID != 0) output.Append("&amp;rft.identifier=" + Server.UrlEncode(string.Format(AppConfig.ItemPageUrl, ItemID)));
                        if (Title != string.Empty) output.Append("&amp;rft.title=" + Server.UrlEncode(Title));
                        if (Language != string.Empty) output.Append("&amp;rft.language=" + Server.UrlEncode(Language));
                        foreach (Author author in TitleAuthors)
                        {
                            output.Append("&amp;rft.creator=" + Server.UrlEncode(author.FullName));
                        }
                        if (!string.IsNullOrWhiteSpace(Publisher)) output.Append("&amp;rft.publisher=" + Server.UrlEncode(Publisher));
                        foreach (TitleKeyword keyword in TitleKeywords)
                        {
                            output.Append("&amp;rft.subject=" + Server.UrlEncode(keyword.Keyword));
                        }

                        break;
                    }
            }

            // Add additional elements common to all formats
            if (Date != string.Empty) output.Append("&amp;rft.date=" + Server.UrlEncode(Date));

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
            if (doi != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("info:doi/" + doi));
            string issn = GetIdentifierValue(IdentifierTarget.Title, "ISSN");
            if (issn != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISSN:" + issn));
            string eissn = GetIdentifierValue(IdentifierTarget.Title, "eISSN");
            if (eissn != string.Empty) output.Append("&amp;rft_id=" + Server.UrlEncode("urn:ISSN:" + eissn));
            output.Append("&amp;rft_id=" + Server.UrlEncode(string.Format(AppConfig.PartPageUrl, SegmentID)));

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
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:journal"));
                        output.Append("&amp;rft.genre=" + Server.UrlEncode(GetGenre()));
                        if (!string.IsNullOrWhiteSpace(ArticleTitle)) output.Append("&amp;rft.atitle=" + Server.UrlEncode(ArticleTitle));
                        if (!string.IsNullOrWhiteSpace(Title)) output.Append("&amp;rft.jtitle=" + Server.UrlEncode(Title));
                        if (!string.IsNullOrWhiteSpace(Volume)) output.Append("&amp;rft.volume=" + Server.UrlEncode(Volume));
                        if (!string.IsNullOrWhiteSpace(Issue)) output.Append("&amp;rft.issue=" + Server.UrlEncode(Issue));
                        if (!string.IsNullOrWhiteSpace(StartPageNumber)) output.Append("&amp;rft.spage=" + Server.UrlEncode(StartPageNumber));
                        if (!string.IsNullOrWhiteSpace(EndPageNumber)) output.Append("&amp;rft.epage=" + Server.UrlEncode(EndPageNumber));
                        if (!string.IsNullOrWhiteSpace(PageRange)) output.Append("&amp;rft.pages=" + Server.UrlEncode(PageRange));
                        if (issn != string.Empty) output.Append("&amp;rft.issn=" + Server.UrlEncode(issn));
                        if (eissn != string.Empty) output.Append("&amp;rft.eissn=" + Server.UrlEncode(eissn));
                        bool isFirst = true;
                        foreach(ItemAuthor author in ItemAuthors)
                        {
                            if (isFirst)
                            {
                                string firstName = GetAuthorFirstName(author.FullName);
                                if (firstName != string.Empty) output.Append("&amp;rft.aufirst=" + Server.UrlEncode(firstName));
                                string lastName = GetAuthorLastName(author.FullName);
                                if (lastName != string.Empty) output.Append("&amp;rft.aulast=" + Server.UrlEncode(lastName));
                                isFirst = false;
                            }
                            output.Append("&amp;rft.au=" + Server.UrlEncode(author.FullName));
                        }

                        break;
                    }
                default:    // dublin core
                    {
                        output.Append("&amp;rft_val_fmt=" + Server.UrlEncode("info:ofi/fmt:kev:mtx:dc"));
                        output.Append("&amp;rft.source=" + Server.UrlEncode("Biodiversity Heritage Library"));
                        output.Append("&amp;rft.rights=" + Server.UrlEncode("Creative Commons Attribution 3.0"));
                        output.Append("&amp;rft.identifier=" + Server.UrlEncode(string.Format(AppConfig.PartPageUrl, SegmentID)));
                        if (!string.IsNullOrWhiteSpace(ArticleTitle)) output.Append("&amp;rft.title=" + Server.UrlEncode(ArticleTitle));
                        if (!string.IsNullOrWhiteSpace(Language)) output.Append("&amp;rft.language=" + Server.UrlEncode(Language));
                        foreach(ItemAuthor author in ItemAuthors)
                        {
                            output.Append("&amp;rft.creator=" + Server.UrlEncode(author.FullName));
                        }
                        foreach(ItemKeyword keyword in ItemKeywords)
                        { 
                            output.Append("&amp;rft.subject=" + Server.UrlEncode(keyword.Keyword));
                        }

                        break;
                    }
            }

            // Add additional elements common to all formats
            if (Date != string.Empty) output.Append("&amp;rft.date=" + Server.UrlEncode(Date));

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
                if (MarcLeader.Length >= 8)
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