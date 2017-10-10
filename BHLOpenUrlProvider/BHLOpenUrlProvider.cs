using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using MOBOT.OpenUrl.Utilities;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DAL;
using MOBOT.BHL.Web.Utilities;
using CustomDataAccess;

namespace MOBOT.BHL.OpenUrlProvider
{
    public class BHLOpenUrlProvider : IOpenUrlProvider
    {
        private string _urlFormat = string.Empty;

        public string UrlFormat
        {
            get { return _urlFormat; }
            set { _urlFormat = value; }
        }

        private string _partUrlFormat = string.Empty;

        public string PartUrlFormat
        {
            get { return _partUrlFormat; }
            set { _partUrlFormat = value; }
        }

        private string _itemUrlFormat = string.Empty;

        public string ItemUrlFormat
        {
            get { return _itemUrlFormat; }
            set { _itemUrlFormat = value; }
        }

        private string _titleUrlFormat = string.Empty;

        public string TitleUrlFormat
        {
            get { return _titleUrlFormat; }
            set { _titleUrlFormat = value; }
        }

        private string _ipAddress = null;

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private bool _useFullTextSearch = false;

        public bool UseFullTextSearch
        {
            get { return _useFullTextSearch; }
            set { _useFullTextSearch = value; }
        }

        #region IOpenUrlProvider Members

        public IOpenUrlResponse FindCitation(IOpenUrlQuery query)
        {
            IOpenUrlResponse response = new OpenUrlResponse();

            try
            {
                if (query == null) throw new Exception("Query cannot be null.");

                if (query.ValidateQuery())  // Validate the query
                {
                    bool foundCitation = false;
                    OpenUrlCitationDAL ouDAL = new OpenUrlCitationDAL();
                    int pageID = 0;
                    if (query.Version == "1.0")
                    {
                        pageID = this.GetBHLIDFromIdentifierList(query, "page");
                    }
                    else
                    {
                        String pageIDString = this.GetIDFromIdentifierList(query, "page");
                        Int32.TryParse(pageIDString, out pageID);
                    }

                    // If we've got a page id, submit a query to database
                    CustomGenericList<OpenUrlCitation> citations = new CustomGenericList<OpenUrlCitation>();
                    if (pageID > 0) citations = ouDAL.OpenUrlCitationSelectByPageID(null, null, pageID);
                    if (citations.Count > 0) foundCitation = true;

                    int segmentID = 0;
                    if (!foundCitation)
                    {
                        if (query.Version == "1.0")
                        {
                            segmentID = this.GetBHLIDFromIdentifierList(query, "part");
                        }
                        else
                        {
                            String segmentIDString = this.GetIDFromIdentifierList(query, "part");
                            Int32.TryParse(segmentIDString, out segmentID);
                        }

                        // If we've got a segment id, submit a query to database
                        if (segmentID > 0) citations = ouDAL.OpenUrlCitationSelectBySegmentID(null, null, segmentID);
                        if (citations.Count > 0) foundCitation = true;
                    }

                    string doi = string.Empty;
                    if (!foundCitation)
                    {
                        // Get DOI, if specified
                        doi = this.GetIDFromIdentifierList(query, "doi");
                        /*
                        if (!string.IsNullOrEmpty(doi))
                        {
                            citations = ouDAL.OpenUrlCitationSelectByDOI(null, null, doi);
                            if (citations.Count > 0) foundCitation = true;
                        }
                         */
                    }

                    // If we haven't found a citation, try finding the title using title, item, and page criteria
                    int titleID = 0;
                    int itemID = 0;
                    if (!foundCitation)
                    {
                        // Get the title ID, if one was specified
                        if (query.Version == "1.0")
                        {
                            titleID = this.GetBHLIDFromIdentifierList(query, "bibliography");
                        }
                        else
                        {
                            String titleIDString = this.GetIDFromIdentifierList(query, "title");
                            Int32.TryParse(titleIDString, out titleID);
                        }

                        // Get the item ID, if one was specified
                        if (query.Version == "1.0")
                        {
                            itemID = this.GetBHLIDFromIdentifierList(query, "item");
                        }
                        else
                        {
                            String itemIDString = this.GetIDFromIdentifierList(query, "item");
                            Int32.TryParse(itemIDString, out itemID);
                        }

                        // Look for citations
                        if (UseFullTextSearch)
                        {
                            citations = ouDAL.OpenUrlCitationSelectByCitationDetailsFT(null, null, titleID, itemID, 
                                doi, (query.BookTitle != String.Empty ? query.BookTitle : query.JournalTitle),
                                query.ArticleTitle, query.AuthorLast, query.AuthorFirst, 
                                query.Volume, query.Issue, query.Date.Year(), query.StartPage);
                        }
                        else
                        {
                            citations = ouDAL.OpenUrlCitationSelectByCitationDetails(null, null, titleID, itemID, 
                                doi, (query.BookTitle != String.Empty ? query.BookTitle : query.JournalTitle),
                                query.ArticleTitle, query.AuthorLast, query.AuthorFirst, 
                                query.Volume, query.Issue, query.Date.Year(), query.StartPage);
                        }

                        if (citations.Count > 0) foundCitation = true;
                    }

                    // Log the citation resolution results
                    try
                    {
                        this.LogRequest(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}|found:{12}",
                            (titleID == 0 ? string.Empty : "&pid=title:" + titleID.ToString()),
                            (itemID == 0 ? string.Empty : "&pid=item:" + itemID.ToString()),
                            (pageID == 0 ? string.Empty : "&pid=page:" + pageID.ToString()),
                            (segmentID == 0 ? string.Empty : "&pid=part:" + segmentID.ToString()),
                            (string.IsNullOrEmpty(doi) ? string.Empty : "&id=doi:" + doi),
                            (string.IsNullOrEmpty((query.BookTitle != String.Empty ? query.BookTitle : query.JournalTitle)) ?
                                string.Empty :
                                "&title=" + (query.BookTitle != String.Empty ? query.BookTitle : query.JournalTitle)),
                            (string.IsNullOrEmpty(query.AuthorLast) ? string.Empty : "&aulast=" + query.AuthorLast),
                            (string.IsNullOrEmpty(query.AuthorFirst) ? string.Empty : "&aufirst=" + query.AuthorFirst),
                            (string.IsNullOrEmpty(query.Volume) ? string.Empty : "&volume=" + query.Volume),
                            (string.IsNullOrEmpty(query.Issue) ? string.Empty : "&issue=" + query.Issue),
                            (string.IsNullOrEmpty(query.Date.Year()) ? string.Empty : "&date=" + query.Date.Year()),
                            (string.IsNullOrEmpty(query.StartPage) ? string.Empty : "&spage=" + query.StartPage),
                            citations.Count.ToString()));
                    }
                    catch
                    {
                        // Do nothing.  Just suppress errors that occur during the logging process.
                    }

                    if (foundCitation)
                    {
                        // For each citation we got back from the database, add a citation to the response
                        foreach (OpenUrlCitation citation in citations)
                        {
                            OpenUrlResponseCitation responseCitation = new OpenUrlResponseCitation();
                            responseCitation.ATitle = citation.ArticleTitle;
                            responseCitation.Title = (string.IsNullOrEmpty(responseCitation.ATitle) ? citation.FullTitle : citation.JournalTitle);
                            responseCitation.PublisherName = citation.PublisherName;
                            responseCitation.PublisherPlace = citation.PublisherPlace;
                            responseCitation.Date = citation.Date;
                            responseCitation.Language = citation.LanguageName;
                            responseCitation.Volume = citation.Volume;
                            responseCitation.Genre = citation.Genre;
                            if (citation.PageID > 0) responseCitation.Url = String.Format(UrlFormat, citation.PageID.ToString());
                            if (citation.PartID > 0) responseCitation.PartUrl = String.Format(PartUrlFormat, citation.PartID.ToString());
                            if (citation.ItemID > 0) responseCitation.ItemUrl = String.Format(ItemUrlFormat, citation.ItemID.ToString());
                            if (citation.TitleID > 0) responseCitation.TitleUrl = String.Format(TitleUrlFormat, citation.TitleID.ToString());
                            responseCitation.Oclc = citation.Oclc;
                            responseCitation.Issn = citation.Issn;
                            responseCitation.Isbn = citation.Isbn;
                            responseCitation.Lccn = citation.Lccn;
                            responseCitation.STitle = citation.Abbreviation;
                            responseCitation.PublicationFrequency = citation.CurrentPublicationFrequency;
                            responseCitation.Edition = citation.EditionStatement;
                            responseCitation.SPage = citation.StartPage;
                            responseCitation.EPage = citation.EndPage;
                            responseCitation.Pages = citation.Pages;

                            if (citation.Authors.Length > 0)
                            {
                                string[] authors = citation.Authors.Split('|');
                                foreach (string author in authors)
                                {
                                    responseCitation.Authors.Add(author);
                                }
                            }

                            if (citation.Subjects.Length > 0)
                            {
                                string[] subjects = citation.Subjects.Split('|');
                                foreach (string subject in subjects)
                                {
                                    responseCitation.Subjects.Add(subject);
                                }
                            }

                            response.citations.Add(responseCitation);
                        }
                    }

                    // Data for testing
                    //this.AddTestData(response);

                    response.Status = ResponseStatus.Success;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = query.ValidationError;
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            // Return the response
            return response;
        }

        #endregion

        /// <summary>
        /// Look for a url identifier of the specified type in the identifiers collection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="idType">"page" or "bibliography"</param>
        /// <returns>The numeric identifier portion of the url</returns>
        private int GetBHLIDFromIdentifierList(IOpenUrlQuery query, string idType)
        {
            int id = 0;

            if (idType == "part" || idType == "page" || idType == "item" || idType == "bibliography")
            {
                // Check if we have an ID of the specified type
                if (query.Version == "1.0")
                {
                    for (int x = 0; x < query.Identifiers.Length; x++)
                    {
                        // Look for the ID in a url (https://www.biodiversitylibrary.org/page/1234)
                        if ((string)query.Identifiers[x].Key == "url")
                        {
                            string url = (string)query.Identifiers[x].Value;
                            if (url.Contains(idType + "/"))
                            {
                                Int32.TryParse(url.Substring(url.LastIndexOf('/') + 1), out id);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < query.Identifiers.Length; x++)
                    {
                        // Look for an identifier key of the appropriate type
                        if ((string)query.Identifiers[x].Key == idType)
                        {
                            Int32.TryParse((string)query.Identifiers[x].Value, out id);
                        }
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Look for an identifier of the specified type in the identifiers collection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="idType"></param>
        /// <returns></returns>
        private string GetIDFromIdentifierList(IOpenUrlQuery query, string idType)
        {
            string id = string.Empty;

            if (idType == "doi" || idType == "oclcnum" || idType == "lccn" || idType == "title" || idType == "item" || idType == "page" || idType == "part")
            {
                // Check if we have an ID of the specified type
                for (int x = 0; x < query.Identifiers.Length; x++)
                {
                    if ((string)query.Identifiers[x].Key == idType)
                    {
                        id = (string)query.Identifiers[x].Value;
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Log the OpenUrl request
        /// </summary>
        /// <param name="detail"></param>
        private void LogRequest(string detail)
        {
            // Log the request.  
            // First argument "3" corresponds to "BHL OpenUrl".  
            // Fourth argument "230" corresponds to "OpenUrl Endpoint Request"
            BHL.Web.Utilities.RequestLog requestLog = new BHL.Web.Utilities.RequestLog();
            requestLog.SaveRequestLog(3, this.IpAddress, null, 230, detail);
        }

        private void AddTestData(IOpenUrlResponse response)
        {
            OpenUrlResponseCitation citation = new OpenUrlResponseCitation();
            citation.Title = "The cannon-ball tree : the monkey-pots";
            citation.PublisherName = "Field Museum of Natural History,";
            citation.PublisherPlace = "Chicago:";
            citation.Date = "1924";
            citation.Language = "English";
            citation.Volume = "Fieldiana, Popular Series, Botany, no. 6";
            citation.Genre = "Book";
            citation.Authors.Add("Dahlgren, B. E.");
            citation.Authors.Add("Lang, H.");
            citation.Subjects.Add("Brazil nut");
            citation.Subjects.Add("Lecythidaceae");
            citation.Subjects.Add("South American");
            citation.Subjects.Add("Trees");
            citation.Url = "https://www.biodiversitylibrary.org/page/4354945";
            citation.TitleUrl = "https://www.biodiversitylibary.org/title/5435";
            citation.Oclc = "179674112";
            response.citations.Add(citation);
            citation = new OpenUrlResponseCitation();
            citation.Title = "The cannon-ball tree : the monkey-pots";
            citation.PublisherName = "Field Museum of Natural History,";
            citation.PublisherPlace = "Chicago:";
            citation.Date = "1924";
            citation.Language = "English";
            citation.Volume = "Fieldiana, Popular Series, Botany, no. 6";
            citation.Genre = "Book";
            citation.Authors.Add("Dahlgren, B. E.");
            citation.Authors.Add("Lang, H.");
            citation.Subjects.Add("Brazil nut");
            citation.Subjects.Add("Lecythidaceae");
            citation.Subjects.Add("South American");
            citation.Subjects.Add("Trees");
            citation.Url = "https://www.biodiversitylibrary.org/page/4354939";
            citation.TitleUrl = "https://www.biodiversitylibary.org/title/5435";
            citation.Oclc = "179674112";
            response.citations.Add(citation);
        }
    }
}
