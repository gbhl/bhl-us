using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        /// <summary>
        /// Generate a RIS citation string from the RISCitation object
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        public string GenerateRISCitation(RISCitation citation)
        {
            string citationText = string.Empty;

            string citationType = string.Empty;
            switch (citation.Genre)
            {
                case "Article":
                    citationType = RISRefType.ARTICLE;
                    break;
                case "Chapter":
                case "Treatment":
                    citationType = RISRefType.BOOKSECTION;
                    break;
                case "Proceeding":
                    citationType = RISRefType.CONFERENCEPROCEEDING;
                    break;
                case "Conference":
                    citationType = RISRefType.CONFERENCEPAPER;
                    break;
                case "Thesis":
                    citationType = RISRefType.THESIS;
                    break;
                case "Letter":
                    citationType = RISRefType.UNPUBLISHED;
                    break;
                default:
                    citationType = RISRefType.BOOK;
                    break;
            }

            string title = citation.Title;
            string journal = citation.Journal;
            string volume = citation.Volume;
            string issue = citation.Issue;
            string url = citation.Url;
            string publisher = citation.Publisher;
            string publisherPlace = citation.PublicationPlace;
            string year = citation.Year;
            List<string> authors = citation.Authors;
            List<string> keywords = citation.Keywords;
            string callNumber = citation.CallNumber;
            string doi = citation.Doi;
            string edition = citation.Edition;
            string issnisbn = citation.IssnIsbn;
            string language = citation.Language;
            string notes = citation.Notes;
            string summary = citation.Abstract;
            string startPage = citation.StartPage;
            string endPage = citation.EndPage;

            List<Tuple<string, string>> elements = new List<Tuple<string, string>>();
            elements.Add(GetRISElement(RISElementName.TITLE, title));
            if (!string.IsNullOrWhiteSpace(journal)) elements.Add(GetRISElement(RISElementName.TITLECONTAINER, journal));
            if (!string.IsNullOrWhiteSpace(volume)) elements.Add(GetRISElement(RISElementName.VOLUME, volume));
            if (!string.IsNullOrWhiteSpace(issue)) elements.Add(GetRISElement(RISElementName.ISSUE, issue));
            if (!string.IsNullOrWhiteSpace(url)) elements.Add(GetRISElement(RISElementName.URL, url));
            if (!string.IsNullOrWhiteSpace(publisher)) elements.Add(GetRISElement(RISElementName.PUBLISHER, publisher));
            if (!string.IsNullOrWhiteSpace(publisherPlace)) elements.Add(GetRISElement(RISElementName.PUBLISHINGPLACE, publisherPlace));
            if (!string.IsNullOrWhiteSpace(year)) elements.Add(GetRISElement(RISElementName.YEAR, year));
            if (!string.IsNullOrWhiteSpace(startPage)) elements.Add(GetRISElement(RISElementName.STARTPAGE, startPage));
            if (!string.IsNullOrWhiteSpace(endPage)) elements.Add(GetRISElement(RISElementName.ENDPAGE, endPage));
            if (!string.IsNullOrWhiteSpace(callNumber)) elements.Add(GetRISElement(RISElementName.CALLNUMBER, callNumber));
            if (!string.IsNullOrWhiteSpace(doi)) elements.Add(GetRISElement(RISElementName.DOI, doi));
            if (!string.IsNullOrWhiteSpace(edition)) elements.Add(GetRISElement(RISElementName.EDITION, edition));
            if (!string.IsNullOrWhiteSpace(issnisbn)) elements.Add(GetRISElement(RISElementName.ISSNISBN, issnisbn));
            if (!string.IsNullOrWhiteSpace(language)) elements.Add(GetRISElement(RISElementName.LANGUAGE, language));
            if (!string.IsNullOrWhiteSpace(notes)) elements.Add(GetRISElement(RISElementName.NOTES, notes));
            if (!string.IsNullOrWhiteSpace(summary)) elements.Add(GetRISElement(RISElementName.ABSTRACT, summary));
            foreach (string author in authors) elements.Add(GetRISElement(RISElementName.AUTHOR, author));
            foreach (string keyword in keywords) elements.Add(GetRISElement(RISElementName.KEYWORD, keyword));

            RIS ris = new RIS(citationType, elements);
            citationText = ris.GenerateReference();

            return citationText;
        }

        private Tuple<string, string> GetRISElement(string name, string value)
        {
            return new Tuple<string, string>(name, value);
        }
    }
}
