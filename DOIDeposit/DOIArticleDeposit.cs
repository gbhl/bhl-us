using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIArticleDeposit : DOIDeposit
    {

        #region Constructors

        public DOIArticleDeposit()
        {
        }

        public DOIArticleDeposit(DOIDepositData depositData)
        {
            Data = depositData;
        }

        public DOIArticleDeposit(string depositTemplate)
        {
            DepositTemplate = depositTemplate;
        }

        public DOIArticleDeposit(DOIDepositData depositData, string depositTemplate)
        {
            Data = depositData;
            DepositTemplate = depositTemplate;
        }

        #endregion Constructors

        public override string ToString()
        {
            return this.ToString(this.DepositTemplate);
        }

        public override string ToString(string template)
        {
            /*
			<journal_metadata>
				<full_title>Test Publication</full_title>
				<abbrev_title>TP</abbrev_title>
				<issn media_type="print">12345678</issn>
			</journal_metadata>
			<journal_issue>
				<publication_date media_type="print">
					<month>12</month>
					<day>1</day>
					<year>2005</year>
				</publication_date>
				<journal_volume>
					<volume>12</volume>
				</journal_volume>
				<issue>1</issue>
			</journal_issue>
			<!-- ====== This is the article's metadata ======== -->
			<journal_article publication_type="full_text">
				<titles>
					<title>Article 12292005 9:32</title>
				</titles>
				<contributors>
					<person_name sequence="first" contributor_role="author">
						<given_name>Chuck</given_name>
						<surname>Koscher</surname>
					</person_name>
				</contributors>
				<publication_date media_type="print">
					<month>12</month>
					<day>1</day>
					<year>2004</year>
				</publication_date>
				<pages>
					<first_page>100</first_page>
					<last_page>200</last_page>
				</pages>
				<doi_data>
					<doi>10.50505/test_20051229930</doi>
					<resource>http://www.crossref.org/</resource>
				</doi_data>
			</journal_article>
             */

            StringBuilder content = new StringBuilder();

            // Add the header values
            template = template.Replace("{doi_batch_id}", HttpUtility.HtmlEncode(Data.BatchID));
            template = template.Replace("{timestamp}", HttpUtility.HtmlEncode(Data.Timestamp));
            template = template.Replace("{depositor_name}", HttpUtility.HtmlEncode(Data.DepositorName));
            template = template.Replace("{depositor_email_address}", HttpUtility.HtmlEncode(Data.DepositorEmail));
            template = template.Replace("{registrant}", HttpUtility.HtmlEncode(Data.Registrant));

            // Build the journal_metadata content
            if (!string.IsNullOrEmpty(Data.Language))
                content.AppendLine("<journal_metadata language=\"" + HttpUtility.HtmlEncode(Data.Language) + "\">");
            else
                content.AppendLine("<journal_metadata>");

            if (!string.IsNullOrEmpty(Data.Title))
            {
                content.AppendLine("<full_title>" + HttpUtility.HtmlEncode(Data.Title) + "</full_title>");
            }
            if (!string.IsNullOrEmpty(Data.Issn))
            {
                content.AppendLine("<issn media_type=\"print\">" + HttpUtility.HtmlEncode(Data.Issn) + "</issn>");
            }
            else if (!string.IsNullOrEmpty(Data.TitleDOIName))
            {
                content.AppendLine("<doi_data>");
                content.AppendLine("<doi>" + HttpUtility.HtmlEncode(Data.TitleDOIName) + "</doi>");
                content.AppendLine("<resource>" + HttpUtility.HtmlEncode(Data.TitleDOIResource) + "</resource>");
                content.AppendLine("</doi_data>");
            }
            content.AppendLine("</journal_metadata>");

            // Build the journal_issue content
            content.AppendLine("<journal_issue>");
            if (!string.IsNullOrEmpty(Data.PublicationDate))
            {
                DOIDate doiDate = new DOIDate(Data.PublicationDate);

                content.AppendLine("<publication_date media_type=\"print\">");
                if (doiDate.Month != null) content.AppendLine("<month>" + HttpUtility.HtmlEncode(doiDate.Month) + "</month>");
                if (doiDate.Day != null) content.AppendLine("<day>" + HttpUtility.HtmlEncode(doiDate.Day) + "</dat>");
                content.AppendLine("<year>" + HttpUtility.HtmlEncode(doiDate.Year ?? doiDate.DateString) + "</year>");
                content.AppendLine("</publication_date>");
            }
            if (!string.IsNullOrWhiteSpace(Data.Volume))
            {
                content.AppendLine("<journal_volume>");
                content.AppendLine("<volume>" + HttpUtility.HtmlEncode(Data.Volume) + "</volume>");
                content.AppendLine("</journal_volume>");
            }
            if (!string.IsNullOrWhiteSpace(Data.Issue))
            {
                content.AppendLine("<issue>" + HttpUtility.HtmlEncode(Data.Issue) + "</issue>");
            }
            content.AppendLine("</journal_issue>");

            // Build the journal_article content
            content.AppendLine("<journal_article publication_type=\"full_text\">");
            content.AppendLine("<titles>");
            content.AppendLine("<title>" + HttpUtility.HtmlEncode(Data.ArticleTitle) + "</title>");
            content.AppendLine("</titles>");

            if (Data.Contributors.Count() > 0)
            {
                content.AppendLine("<contributors>");

                foreach (DOIDepositData.Contributor contributor in Data.Contributors)
                {
                    string sequence = this.GetSequenceString(contributor.Sequence);
                    string role = this.GetRoleString(contributor.Role);

                    if (!string.IsNullOrEmpty(contributor.PersonName))
                    {
                        string lastName = string.Empty;
                        string firstName = string.Empty;
                        if (contributor.PersonName.IndexOf(',') >= 0)
                        {
                            lastName = contributor.PersonName.Substring(0, contributor.PersonName.IndexOf(','));
                            firstName = contributor.PersonName.Substring(contributor.PersonName.IndexOf(',') + 1);
                        }
                        else
                        {
                            lastName = contributor.PersonName;
                        }

                        firstName = firstName.TrimEnd(',').Trim();

                        content.AppendLine("<person_name sequence=\"" + sequence + "\" contributor_role=\"" + role + "\">");
                        if (firstName.Length > 0) content.AppendLine("<given_name>" + HttpUtility.HtmlEncode(firstName) + "</given_name>");
                        content.AppendLine("<surname>" + HttpUtility.HtmlEncode(lastName) + "</surname>");
                        content.AppendLine("</person_name>");
                    }
                    else
                    {
                        content.Append("<organization sequence=\"" + sequence + "\" contributor_role=\"" + role + "\">");
                        content.Append(HttpUtility.HtmlEncode(contributor.OrganizationName));
                        content.Append("</organization>\n");
                    }
                }
                content.AppendLine("</contributors>");
            }

            if (!string.IsNullOrWhiteSpace(Data.ArticlePublicationDate))
            {
                DOIDate doiDate = new DOIDate(Data.ArticlePublicationDate);

                content.AppendLine("<publication_date media_type=\"print\">");
                if (doiDate.Month != null) content.AppendLine("<month>" + HttpUtility.HtmlEncode(doiDate.Month) + "</month>");
                if (doiDate.Day != null) content.AppendLine("<day>" + HttpUtility.HtmlEncode(doiDate.Day) + "</day>");
                content.AppendLine("<year>" + HttpUtility.HtmlEncode(doiDate.Year ?? doiDate.DateString) + "</year>");
                content.AppendLine("</publication_date>");
            }
            if (!string.IsNullOrWhiteSpace(Data.FirstPage) || !string.IsNullOrWhiteSpace(Data.LastPage))
            {
                content.AppendLine("<pages>");
                if (!string.IsNullOrWhiteSpace(Data.FirstPage)) content.AppendLine("<first_page>" + HttpUtility.HtmlEncode(Data.FirstPage) + "</first_page>");
                if (!string.IsNullOrWhiteSpace(Data.LastPage)) content.AppendLine("<last_page>" + HttpUtility.HtmlEncode(Data.LastPage) + "</last_page>");
                content.AppendLine("</pages>");
            }
            content.AppendLine("<doi_data>");
            content.AppendLine("<doi>" + HttpUtility.HtmlEncode(Data.DoiName) + "</doi>");
            content.AppendLine("<resource>" + HttpUtility.HtmlEncode(Data.DoiResource) + "</resource>");
            content.AppendLine("</doi_data>");
            content.AppendLine("</journal_article>");

            // Insert the book metadata into the template and return the result
            return template.Replace("{article_content}", content.ToString());
        }

        /// <summary>
        /// Return a string representation of the specified contributor sequence
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        private string GetSequenceString(DOIDepositData.PersonNameSequence sequence)
        {
            string sequenceString = string.Empty;

            switch (sequence)
            {
                case DOIDepositData.PersonNameSequence.First:
                    sequenceString = "first";
                    break;
                case DOIDepositData.PersonNameSequence.Additional:
                    sequenceString = "additional";
                    break;
            }

            return sequenceString;
        }

        /// <summary>
        /// Return a string representation of the specified contributor role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GetRoleString(DOIDepositData.ContributorRole role)
        {
            string roleString = string.Empty;

            switch (role)
            {
                case DOIDepositData.ContributorRole.Author:
                    roleString = "author";
                    break;
                case DOIDepositData.ContributorRole.Editor:
                    roleString = "editor";
                    break;
                case DOIDepositData.ContributorRole.Chair:
                    roleString = "chair";
                    break;
                case DOIDepositData.ContributorRole.Translator:
                    roleString = "translator";
                    break;
            }

            return roleString;
        }
    }
}
