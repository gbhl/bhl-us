using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace MOBOT.BHL.DOIDeposit
{
    class DOIArticleXmlQuery : DOIQuery
    {
        #region Constructors

        public DOIArticleXmlQuery()
        {
        }

        public DOIArticleXmlQuery(DOIDepositData depositData)
        {
            Data = depositData;
        }

        public DOIArticleXmlQuery(string depositTemplate)
        {
            QueryTemplate = depositTemplate;
        }

        public DOIArticleXmlQuery(DOIDepositData depositData, string depositTemplate)
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
                  <journal_title match="exact">American Journal of Bioethics</journal_title>
                  <author match="fuzzy" search-all-authors="false">Agich</author> 
                  <volume match="fuzzy">1</volume> 
                  <issue>1</issue>
                  <first_page>50</first_page> 
                  <year>2001</year>
                  <article_title>The Salience of Narrative for Bioethics</article_title>
                </query> 
              </body> 
            </query_batch>
             */

            StringBuilder content = new StringBuilder();

            // Add the header values
            template = template.Replace("{doi_batch_id}", XmlEncode(Data.BatchID));
            template = template.Replace("{depositor_email_address}", XmlEncode(Data.DepositorEmail));

            // Build the query_metadata content
            if (!string.IsNullOrEmpty(Data.Issn))
            {
                content.Append("<issn match=\"optional\">" + XmlEncode(Data.Issn) + "</issn>");
            }

            string title = Data.Title;
            string pubDate = Data.ArticlePublicationDate;
            string articleTitle = Data.ArticleTitle;
            if (Data.PublicationType != DOIDepositData.PublicationTypeValue.Article)
            {
                title = string.Empty;
                pubDate = Data.PublicationDate;
                articleTitle = Data.Title;
            }

            if (!string.IsNullOrEmpty(title))
            {
                content.Append("<journal_title match=\"optional fuzzy\">" + XmlEncode(title.Replace(':', ' ').Substring(0, (title.Length > 256 ? 256 : title.Length))) + "</journal_title>");
            }

            foreach(DOIDepositData.Contributor contributor in Data.Contributors)
            {
                if (!string.IsNullOrWhiteSpace(contributor.PersonName))
                {
                    string lastName = string.Empty;
                    if (contributor.PersonName.IndexOf(',') >= 0)
                    {
                        lastName = contributor.PersonName.Substring(0, contributor.PersonName.IndexOf(','));
                    }
                    else
                    {
                        lastName = contributor.PersonName;
                    }

                    content.Append("<author match=\"fuzzy\" search-all-authors=\"true\">" + XmlEncode(lastName) + "</author>");

                    // The CrossRef query schema allows for only one author name
                    break;
                }
            }

            if (!string.IsNullOrWhiteSpace(Data.Volume))
            {
                content.Append("<volume match=\"fuzzy\">" + XmlEncode(Data.Volume) + "</volume>");
            }

            if (!string.IsNullOrWhiteSpace(Data.FirstPage))
            {
                content.Append("<first_page match=\"optional\">" + XmlEncode(Data.FirstPage) + "</first_page>");
            }

            if (!string.IsNullOrEmpty(pubDate))
            {
                DOIDate doiDate = new DOIDate(pubDate);
                content.Append("<year match=\"optional\">" + XmlEncode(doiDate.Year ?? doiDate.DateString) + "</year>");
            }

            if (!string.IsNullOrWhiteSpace(articleTitle))
            {
                content.Append("<article_title match=\"fuzzy\">" + XmlEncode(articleTitle.Replace(':', ' ').Substring(0, (articleTitle.Length > 256 ? 256 : articleTitle.Length))) + "</article_title>");
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
