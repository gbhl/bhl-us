using System;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.DOIDeposit
{
    class DOIMonographXmlQuery : DOIQuery
    {
        #region Constructors

        public DOIMonographXmlQuery()
        {
        }

        public DOIMonographXmlQuery(DOIDepositData depositData)
        {
            Data = depositData;
        }

        public DOIMonographXmlQuery(string depositTemplate)
        {
            QueryTemplate = depositTemplate;
        }

        public DOIMonographXmlQuery(DOIDepositData depositData, string depositTemplate)
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
                  <isbn match="optional">15360075<isbn>
                  <volume_title match="exact">American Journal of Bioethics</volume_title>
                  <year>2001</year>
                  <volume match="fuzzy">1</volume> 
                  <author match="fuzzy" search-all-authors="false">Agich</author> 
                  <edition_number>1</edition_number>
                </query> 
              </body> 
            </query_batch>
             */

            StringBuilder content = new StringBuilder();

            // Add the header values
            template = template.Replace("{doi_batch_id}", XmlEncode(Data.BatchID));
            template = template.Replace("{depositor_email_address}", XmlEncode(Data.DepositorEmail));

            // Build the query_metadata content
            if (!string.IsNullOrWhiteSpace(Data.Isbn))
            {
                content.Append("<isbn match=\"optional\">" + XmlEncode(Data.Isbn) + "</isbn>");
            }

            string title = Data.Title;
            string pubDate = Data.PublicationDate;
            if (Data.PublicationType == DOIDepositData.PublicationTypeValue.Article || Data.PublicationType == DOIDepositData.PublicationTypeValue.Chapter)
            {
                title = Data.ArticleTitle;
                pubDate = Data.ArticlePublicationDate;
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                content.Append("<volume_title match=\"fuzzy\">" + 
                    XmlEncode(title.Replace(':', ' ').Substring(0, (title.Length > 256 ? 256 : title.Length))) + 
                    "</volume_title>");
            }

            if (Data.PublicationType == DOIDepositData.PublicationTypeValue.Chapter)
            {
                content.Append("<first_page match=\"optional\">" + XmlEncode(Data.FirstPage) + "</first_page>");
            }

            if (!string.IsNullOrWhiteSpace(pubDate))
            {
                DOIDate doiDate = new DOIDate(pubDate);
                content.Append("<year match=\"optional\">" + XmlEncode(doiDate.Year ?? doiDate.DateString) + "</year>");
            }

            if (!string.IsNullOrWhiteSpace(Data.Volume))
            {
                content.Append("<volume match=\"fuzzy\">" + XmlEncode(Data.Volume) + "</volume>");
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

            if (!string.IsNullOrWhiteSpace(Data.Edition))
            {
                content.Append("<edition_number match=\"fuzzy\">" + XmlEncode(Data.Edition) + "</edition_number>");
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
