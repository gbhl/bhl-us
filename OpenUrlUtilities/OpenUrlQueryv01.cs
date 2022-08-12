using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public class OpenUrlQueryv01 : OpenUrlQuery
    {
        #region Constructors

        public OpenUrlQueryv01()
        {
        }

        #endregion

        public override void SetQuery(string queryString)
        {
            this.Version = "0.1";

            String[] qsKeyValuePairs = queryString.ToLower().Split('&');
            foreach (String qsKeyValuePair in qsKeyValuePairs)
            {
                // Set each of the attributes found in the querystring
                String[] keyValue = qsKeyValuePair.Split('=');
                if (keyValue.Length == 2)
                {
                    switch (keyValue[0])
                    {
                        case "genre":
                            this.Genre = keyValue[1];
                            break;
                        case "title":
                            this.BookTitle = keyValue[1];
                            break;
                        case "au":
                            // Use the first "au" tag, if "aulast" not specified
                            this.AuthorLast = (this.AuthorLast == String.Empty) ? keyValue[1] : this.AuthorLast;
                            break;
                        case "aulast":
                            this.AuthorLast = keyValue[1];
                            break;
                        case "aufirst":
                            this.AuthorFirst = keyValue[1];
                            break;
                        case "auinit":
                            this.AuthorInitial = keyValue[1];
                            break;
                        case "auinit1":
                            this.AuthorInitial1 = keyValue[1];
                            break;
                        case "auinitm":
                            this.AuthorInitialMiddle = keyValue[1];
                            break;
                        case "stitle":
                            this.ShortTitle = keyValue[1];
                            break;
                        case "atitle":
                            this.ArticleTitle = keyValue[1];
                            break;
                        case "publisher":
                            this.Publisher = keyValue[1];
                            break;
                        case "issn":
                            this.Issn = keyValue[1];
                            break;
                        case "eissn":
                            this.Eissn = keyValue[1];
                            break;
                        case "coden":
                            this.Coden = keyValue[1];
                            break;
                        case "isbn":
                            this.Isbn = keyValue[1];
                            break;
                        case "volume":
                            this.Volume = keyValue[1];
                            break;
                        case "issue":
                            this.Issue = keyValue[1];
                            break;
                        case "part":
                            this.Part = keyValue[1];
                            break;
                        case "spage":
                            this.StartPage = keyValue[1];
                            break;
                        case "epage":
                            this.EndPage = keyValue[1];
                            break;
                        case "pages":
                            this.Pages = keyValue[1];
                            break;
                        case "artnum":
                            this.ArticleNumber = keyValue[1];
                            break;
                        case "ssn":
                            this.Season = keyValue[1];
                            break;
                        case "quarter":
                            this.Quarter = keyValue[1];
                            break;
                        case "date":
                            this.Date = new OpenUrlQueryDate(keyValue[1]);
                            break;
                        case "id":
                            // id values should be in the form id=NAMESPACE:ID_VALUE, where
                            // the valid namespaces are "doi", "pmid", "bibcode", and "oai".
                            if (keyValue[1].Contains("doi:")) this.Identifiers.Add("doi", keyValue[1].Replace("doi:", ""));
                            else if (keyValue[1].Contains("pmid:")) this.Identifiers.Add("pmid", keyValue[1].Replace("pmid:", ""));
                            else if (keyValue[1].Contains("bibcode:")) this.Identifiers.Add("bibcode", keyValue[1].Replace("bibcode:", ""));
                            else if (keyValue[1].Contains("oai:")) this.Identifiers.Add("oai", keyValue[1].Replace("oai:", ""));
                            else this.ValidationError += (this.ValidationError == string.Empty) ? "Unknown 'id' value: " + keyValue[1] : "|Unknown 'id' value: " + keyValue[1];
                            break;
                        case "pid":
                            // pid values should be in the form pid=NAMESPACE:ID_VALUE
                            // For example, pid=oclcnum:12345678
                            // Recognized pid "namespaces" for BHL are oclcnum, lccn, title, item, and page
                            if (keyValue[1].Contains(":"))
                            {
                                string key = keyValue[1].Substring(0, keyValue[1].IndexOf(':'));
                                string value = keyValue[1].Replace(key + ":", "");
                                this.Identifiers.Add(key, value);
                            }
                            break;
                    }
                }
            }

            // Check the Genre value.  If it is "article" and "atitle" was not specified, then we need to move the
            // BookTitle value to ArticleTitle.
            if (this.Genre == "article")
            {
                if (string.IsNullOrWhiteSpace(this.ArticleTitle)) this.ArticleTitle = this.BookTitle;
                this.BookTitle = string.Empty;
            }
        }

        /// <summary>
        /// Make sure that valid values have been supplied
        /// </summary>
        /// <returns></returns>
        public override bool ValidateQuery()
        {
            if (this.Genre != "article" &&
                this.Genre != "bookitem" &&
                this.Genre != "book" &&
                this.Genre != "journal" &&
                this.Genre != "preprint" &&
                this.Genre != "proceeding" &&
                this.Genre != "conference" &&
                this.Genre != string.Empty)
            {
                if (this.ValidationError != string.Empty) this.ValidationError += "|";
                this.ValidationError += "Invalid Genre";
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

            // valid date formats are YYYY-MM-DD, YYYY-MM, and YYYY
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
