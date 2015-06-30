using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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
            template = template.Replace("{doi_batch_id}", HttpUtility.UrlEncode(Data.BatchID));
            template = template.Replace("{depositor_email_address}", HttpUtility.UrlEncode(Data.DepositorEmail));

            // Build the query_metadata content
            if (!string.IsNullOrEmpty(Data.Issn))
            {
                content.Append("<issn match=\"optional\">" + HttpUtility.UrlEncode(Data.Issn) + "</issn>");
            }

            if (!string.IsNullOrEmpty(Data.Title))
            {
                content.Append("<journal_title match=\"fuzzy\">" + HttpUtility.UrlEncode(Data.Title) + "</journal_title>");
            }

            if (Data.Contributors.Count() > 0)
            {
                foreach (DOIDepositData.Contributor contributor in Data.Contributors)
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

                    content.Append("<author match=\"fuzzy\">" + HttpUtility.UrlEncode(lastName) + "</author>");
                }
            }

            if (!string.IsNullOrWhiteSpace(Data.Volume))
            {
                content.Append("<volume match=\"fuzzy\">" + HttpUtility.UrlEncode(Data.Volume) + "</volume>");
            }

            if (!string.IsNullOrWhiteSpace(Data.FirstPage))
            {
                content.Append("<first_page match=\"optional\">" + HttpUtility.UrlEncode(Data.FirstPage) + "</first_page>");
            }

            if (!string.IsNullOrEmpty(Data.ArticlePublicationDate))
            {
                content.Append("<year match=\"optional\">" + HttpUtility.UrlEncode(Data.ArticlePublicationDate) + "</year>");
            }

            if (!string.IsNullOrWhiteSpace(Data.ArticleTitle))
            {
                content.Append("<article_title match=\"fuzzy\">" + HttpUtility.UrlEncode(Data.ArticleTitle) + "</article_title>");
            }

            // Insert the query metadata into the template and return the result
            return template.Replace("{query_content}", content.ToString());
        }
    }
}
