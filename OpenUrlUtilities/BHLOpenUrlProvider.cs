using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    public class BHLOpenUrlProvider : IOpenUrlProvider
    {
        #region IOpenUrlProvider Members

        public IOpenUrlResponse FindCitation(IOpenUrlQuery query)
        {
            IOpenUrlResponse response = new OpenUrlResponse();

            try
            {
                if (query == null) throw new Exception("Query cannot be null.");

                if (query.ValidateQuery())  // Validate the query
                {
                    // TODO: Submit query to database


                    // TODO: For each citation we got back from the database, add a citation to the reponse


                    // TODO: Detect some resolution errors here and set the Status/Message fields appropriately.  Do we need additional statuses?


                    // TODO: Test data; remove this when done
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
                    citation.Url = "http://www.biodiversitylibrary.org/page/4354945";
                    citation.TitleUrl = "http://www.biodiversitylibary.org/title/5435";
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
                    //citation.Url = "http://www.biodiversitylibrary.org/item/25578";
                    citation.Url = "http://www.biodiversitylibrary.org/page/4354939";
                    citation.TitleUrl = "http://www.biodiversitylibary.org/title/5435";
                    citation.Oclc = "179674112";
                    response.citations.Add(citation);

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
    }
}
