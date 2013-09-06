using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public class OpenUrlQueryv10 : OpenUrlQuery
    {
        #region Constructors

        public OpenUrlQueryv10()
        {
        }

        #endregion

        public override void SetQuery(string queryString)
        {
            this.Version = "1.0";

            String[] qsKeyValuePairs = queryString.ToLower().Split('&');
            foreach (String qsKeyValuePair in qsKeyValuePairs)
            {
                // Set each of the attributes found in the querystring
                String[] keyValue = qsKeyValuePair.Split('=');
                if (keyValue.Length == 2)
                {
                    switch (keyValue[0])
                    {
                        case "rft_val_fmt":
                            this.Format = keyValue[1];
                            break;
                        case "rft.genre":
                            this.Genre = keyValue[1];
                            break;
                        case "rft.au":
                            // Use the first "au" tag, if "aulast" not specified
                            this.AuthorLast = (this.AuthorLast == String.Empty) ? keyValue[1] : this.AuthorLast;
                            break;
                        case "rft.aulast":
                            this.AuthorLast = keyValue[1];
                            break;
                        case "rft.aufirst":
                            this.AuthorFirst = keyValue[1];
                            break;
                        case "rft.auinit":
                            this.AuthorInitial = keyValue[1];
                            break;
                        case "rft.auinit1":
                            this.AuthorInitial1 = keyValue[1];
                            break;
                        case "rft.auinitm":
                            this.AuthorInitialMiddle = keyValue[1];
                            break;
                        case "rft.ausuffix":
                            this.AuthorSuffix = keyValue[1];
                            break;
                        case "rft.aucorp":
                            this.AuthorCorporation = keyValue[1];
                            break;
                        case "rft.btitle":
                            this.BookTitle = keyValue[1];
                            break;
                        case "rft.jtitle":
                            this.JournalTitle = keyValue[1];
                            break;
                        case "rft.stitle":
                            this.ShortTitle = keyValue[1];
                            break;
                        case "rft.atitle":
                            this.ArticleTitle = keyValue[1];
                            break;
                        case "rft.publisher":
                            this.Publisher = keyValue[1];
                            break;
                        case "rft.pub":
                            this.PublisherName = keyValue[1];
                            break;
                        case "rft.place":
                            this.PublisherPlace = keyValue[1];
                            break;
                        case "rft.issn":
                            this.Issn = keyValue[1];
                            break;
                        case "rft.eissn":
                            this.Eissn = keyValue[1];
                            break;
                        case "rft.coden":
                            this.Coden = keyValue[1];
                            break;
                        case "rft.isbn":
                            this.Isbn = keyValue[1];
                            break;
                        case "rft.volume":
                            this.Volume = keyValue[1];
                            break;
                        case "rft.issue":
                            this.Issue = keyValue[1];
                            break;
                        case "rft.part":
                            this.Part = keyValue[1];
                            break;
                        case "rft.spage":
                            this.StartPage = keyValue[1];
                            break;
                        case "rft.epage":
                            this.EndPage = keyValue[1];
                            break;
                        case "rft.pages":
                            this.Pages = keyValue[1];
                            break;
                        case "rft.artnum":
                            this.ArticleNumber = keyValue[1];
                            break;
                        case "rft.ssn":
                            this.Season = keyValue[1];
                            break;
                        case "rft.quarter":
                            this.Quarter = keyValue[1];
                            break;
                        case "rft.date":
                            this.Date = new OpenUrlQueryDate(keyValue[1]);
                            break;
                        case "rft_id":
                            if (keyValue[1].StartsWith("info:doi/")) this.Identifiers.Add("doi", keyValue[1].Replace("info:doi/", ""));
                            if (keyValue[1].StartsWith("info:pmid/")) this.Identifiers.Add("pmid", keyValue[1].Replace("info:pmid/", ""));
                            if (keyValue[1].StartsWith("info:bibcode/")) this.Identifiers.Add("bibcode", keyValue[1].Replace("info:bibcode/", ""));
                            if (keyValue[1].StartsWith("info:oai/")) this.Identifiers.Add("oai", keyValue[1].Replace("info:oai/", ""));
                            if (keyValue[1].StartsWith("info:hdl/")) this.Identifiers.Add("hdl", keyValue[1].Replace("info:hdl/", ""));
                            if (keyValue[1].StartsWith("info:lccn/")) this.Identifiers.Add("lccn", keyValue[1].Replace("info:lccn/", ""));
                            if (keyValue[1].StartsWith("info:oclcnum/")) this.Identifiers.Add("oclcnum", keyValue[1].Replace("info:oclcnum/", ""));
                            if (keyValue[1].StartsWith("http://") || keyValue[1].StartsWith("https://")) this.Identifiers.Add("url", keyValue[1]);
                            break;
                        case "rft_dat":
                            // Other identifiers could be embedded here.  It is up to the OpenUrlProvider
                            // to which an instance of this class is passed to intepret the information 
                            // stored here.
                            this.PrivateData = keyValue[1];
                            break;
                    }
                }
            }
        }

        public override bool ValidateQuery()
        {
            if (this.Format != "info:ofi/fmt:kev:mtx:book" &&
                this.Format != "info:ofi/fmt:kev:mtx:journal")
            {
                if (this.ValidationError != string.Empty) this.ValidationError += "|";
                this.ValidationError += "Invalid Format (use info:ofi/fmt:kev:mtx:book or info:ofi/fmt:kev:mtx:journal)";
            }

            if (this.Format == "info:ofi/fmt:kev:mtx:book")
            {
                if (this.Genre != "book" &&
                    this.Genre != "bookitem" &&
                    this.Genre != "conference" &&
                    this.Genre != "proceeding" &&
                    this.Genre != "report" &&
                    this.Genre != "document" &&
                    this.Genre != "unknown" &&
                    this.Genre != string.Empty)
                {
                    if (this.ValidationError != string.Empty) this.ValidationError += "|";
                    this.ValidationError += "Invalid Genre (use book, bookitem, conference, proceeding, report, document, or unknown)";
                }
            }

            if (this.Format == "info:ofi/fmt:kev:mtx:journal")
            {
                if (this.Genre != "journal" &&
                    this.Genre != "issue" &&
                    this.Genre != "article" &&
                    this.Genre != "conference" &&
                    this.Genre != "proceeding" &&
                    this.Genre != "preprint" &&
                    this.Genre != "unknown" &&
                    this.Genre != string.Empty)
                {
                    if (this.ValidationError != string.Empty) this.ValidationError += "|";
                    this.ValidationError += "Invalid Genre (use journal, issue, article, conference, proceeding, preprint, or unknown)";
                }
            }

            if (this.Season != "winter" &&
                this.Season != "spring" &&
                this.Season != "summer" &&
                this.Season != "fall" &&
                this.Season != string.Empty)
            {
                if (this.ValidationError != string.Empty) this.ValidationError += "|";
                this.ValidationError += "Invalid Season (use winter, spring, summer, or fall)";
            }

            if (this.Quarter != "1" &&
                this.Quarter != "2" &&
                this.Quarter != "3" &&
                this.Quarter != "4" &&
                this.Quarter != string.Empty)
            {
                if (this.ValidationError != string.Empty) this.ValidationError += "|";
                this.ValidationError += "Invalid Quarter (use 1, 2, 3, or 4)";
            }

            if (this.Date != null)
            {
                string dateValidMsg = String.Empty;
                if (!this.Date.IsValid(out dateValidMsg))
                {
                    if (this.ValidationError != string.Empty) this.ValidationError += "|";
                    this.ValidationError += dateValidMsg;
                }
            }

            return (this.ValidationError == string.Empty);
        }
    }
}
