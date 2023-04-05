using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.DOIDeposit
{
    class DOIJournalXmlQuery : DOIQuery
    {
        #region Constructors

        public DOIJournalXmlQuery()
        {
        }

        public DOIJournalXmlQuery(DOIDepositData depositData)
        {
            Data = depositData;
        }

        public DOIJournalXmlQuery(string depositTemplate)
        {
            QueryTemplate = depositTemplate;
        }

        public DOIJournalXmlQuery(DOIDepositData depositData, string depositTemplate)
        {
            Data = depositData;
            QueryTemplate = depositTemplate;
        }

        #endregion Constructors

        public override string ToString()
        {
            return this.ToString(this.QueryTemplate);
        }

        public override string ToString(string template)
        {
            /*
            <?xml version="1.0"?>
            <query_batch version="2.0" xsi:schemaLocation="http://www.crossref.org/qschema/2.0 http://www.crossref.org/qschema/crossref_query_input2.0.xsd" xmlns="http://www.crossref.org/qschema/2.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"> 
              <head> 
                <email_address>support@crossref.org</email_address> 
                <doi_batch_id>ABC_123_fff</doi_batch_id> 
              </head> 
              <body> 
                <query key="1178517" enable-multiple-hits="false" forward-match="false"> 
                  <issn match="optional">15360075<issn>
                  <journal_title match="optional fuzzy">American Journal of Bioethics</journal_title>
                  <author match="fuzzy" search-all-authors="false">Agich</author> 
                  <first_page>1</first_page> 
                </query> 
              </body> 
            </query_batch>
             */

            StringBuilder content = new StringBuilder();

            // Add the header values
            template = template.Replace("{doi_batch_id}", XmlEncode(Data.BatchID));
            template = template.Replace("{depositor_email_address}", XmlEncode(Data.DepositorEmail));

            // Build the query_metadata content

            // Only one ISSN is allowed per query, so if there are mulitple just use the first one
            if (Data.Issn.Count > 0) content.Append("<issn match=\"optional\">" + XmlEncode(Data.Issn[0].Value) + "</issn>");

            string title = Data.Title;
            if (!string.IsNullOrEmpty(title))
            {
                content.Append("<journal_title match=\"optional fuzzy\">" + XmlEncode(title.Replace(':', ' ').Substring(0, (title.Length > 256 ? 256 : title.Length))) + "</journal_title>");
            }

            if (Data.Contributors.Count() > 0)
            {
                // The CrossRef query schema allows for only one author name
                DOIDepositData.Contributor contributor = Data.Contributors[0];

                string authorName = string.Empty;
                if (string.IsNullOrWhiteSpace(contributor.PersonName))
                {
                    authorName = contributor.OrganizationName ?? string.Empty;
                }
                else if (contributor.PersonName.IndexOf(',') >= 0)
                {
                    authorName = contributor.PersonName.Substring(0, contributor.PersonName.IndexOf(','));
                }
                else
                {
                    authorName = contributor.PersonName;
                }

                content.Append("<author match=\"fuzzy\" search-all-authors=\"true\">" + XmlEncode(authorName) + "</author>");
            }

            // Insert the query metadata into the template and return the result
            return template.Replace("{query_content}", HttpUtility.UrlEncode(content.ToString()));
        }

        private string XmlEncode(string content)
        {
            content = content.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;");
            return content;
        }
    }
}
