using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIMonographDeposit : DOIDeposit
    {
        #region Constructors

        public DOIMonographDeposit()
        {
        }

        public DOIMonographDeposit(DOIDepositData depositData)
        {
            Data = depositData;
        }

        public DOIMonographDeposit(string depositTemplate)
        {
            DepositTemplate = depositTemplate;
        }

        public DOIMonographDeposit(DOIDepositData depositData, string depositTemplate)
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
            StringBuilder bookContent = new StringBuilder();

            // Add the header values
            template = template.Replace("{doi_batch_id}", HttpUtility.HtmlEncode(Data.BatchID));
            template = template.Replace("{timestamp}", HttpUtility.HtmlEncode(Data.Timestamp));
            template = template.Replace("{depositor_name}", HttpUtility.HtmlEncode(Data.DepositorName));
            template = template.Replace("{depositor_email_address}", HttpUtility.HtmlEncode(Data.DepositorEmail));
            template = template.Replace("{registrant}", HttpUtility.HtmlEncode(Data.Registrant));

            switch (Data.PublicationType)
            {
                case DOIDepositData.PublicationTypeValue.Monograph:
                    template = template.Replace("{book_type_value}", "monograph");
                    break;
                case DOIDepositData.PublicationTypeValue.EditedBook:
                    template = template.Replace("{book_type_value}", "edited_book");
                    break;
                case DOIDepositData.PublicationTypeValue.Reference:
                    template = template.Replace("{book_type_value}", "reference");
                    break;
                case DOIDepositData.PublicationTypeValue.Other:
                    template = template.Replace("{book_type_value}", "other");
                    break;
                default:
                    template = template.Replace("{book_type_value}", "monograph");
                    break;
            }

            // Build the book metadata content
            if (!string.IsNullOrEmpty(Data.Language))
                bookContent.Append("<book_metadata language=\"" + HttpUtility.HtmlEncode(Data.Language) + "\">\n");
            else
                bookContent.Append("<book_metadata>\n");

            if (Data.Contributors.Count() > 0)
            {
                bookContent.Append("<contributors>\n");

                Data.Contributors = this.IdentifyFirstContributor(Data.Contributors);

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

                        bookContent.Append("<person_name sequence=\"" + sequence + "\" contributor_role=\"" + role + "\">\n");
                        if (firstName.Length > 0) bookContent.Append("<given_name>" + HttpUtility.HtmlEncode(firstName) + "</given_name>\n");
                        bookContent.Append("<surname>" + HttpUtility.HtmlEncode(lastName) + "</surname>\n");
                        if (!string.IsNullOrWhiteSpace(contributor.Suffix)) bookContent.Append("<suffix>" + HttpUtility.HtmlEncode(contributor.Suffix) + "</suffix>\n");
                        if (!string.IsNullOrWhiteSpace(contributor.ORCID)) bookContent.Append("<ORCID authenticated=\"false\">" + HttpUtility.HtmlEncode(contributor.ORCID) + "</ORCID>\n");
                        bookContent.Append("</person_name>\n");
                    }
                    else
                    {
                        bookContent.Append("<organization sequence=\"" + sequence + "\" contributor_role=\"" + role + "\">");
                        bookContent.Append(HttpUtility.HtmlEncode(contributor.OrganizationName));
                        bookContent.Append("</organization>\n");
                    }
                }
                bookContent.Append("</contributors>\n");
            }

            bookContent.Append("<titles>\n");
            bookContent.Append("<title>" + HttpUtility.HtmlEncode(Data.Title) + "</title>\n");
            bookContent.Append("</titles>\n");

            if (!string.IsNullOrEmpty(Data.Edition))
            {
                bookContent.Append("<edition_number>" + Data.Edition + "</edition_number>\n");
            }

            if (!string.IsNullOrEmpty(Data.PublicationDate))
            {
                bookContent.Append("<publication_date>\n");
                bookContent.Append("<year>" + HttpUtility.HtmlEncode(Data.PublicationDate) + "</year>\n");
                bookContent.Append("</publication_date>\n");
            }

            if (!string.IsNullOrEmpty(Data.Isbn))
            {
                bookContent.Append("<isbn media_type=\"print\">" + HttpUtility.HtmlEncode(Data.Isbn) + "</isbn>\n");
            }
            else
            {
                bookContent.Append("<noisbn reason=\"monograph\"/>\n");
            }

            bookContent.Append("<publisher>\n");
            bookContent.Append("<publisher_name>" + 
                HttpUtility.HtmlEncode(string.IsNullOrEmpty(Data.PublisherName) ? "[s.n.]" : Data.PublisherName) + 
                "</publisher_name>\n");
            if (!string.IsNullOrEmpty(Data.PublisherPlace)) bookContent.Append("<publisher_place>" + HttpUtility.HtmlEncode(Data.PublisherPlace) + "</publisher_place>\n");
            bookContent.Append("</publisher>\n");

            bookContent.Append("<doi_data>\n");
            bookContent.Append("<doi>" + HttpUtility.HtmlEncode(Data.DoiName) + "</doi>\n");
            bookContent.Append("<resource>" + HttpUtility.HtmlEncode(Data.DoiResource) + "</resource>\n");
            bookContent.Append("</doi_data>\n");

            bookContent.Append("</book_metadata>\n");

            // Insert the book metadata into the template and return the result
            return template.Replace("{book_content}", bookContent.ToString());
        }

        /// <summary>
        /// Only one contributor (author) can be labeled as "First" in a deposit record.  So, examine the list
        /// of contributors.  If none are "First", make the first in the list "First".  If more than one are 
        /// "First", use the first one labeled "First".
        /// </summary>
        /// <param name="contributors"></param>
        /// <returns></returns>
        private List<DOIDepositData.Contributor> IdentifyFirstContributor(List<DOIDepositData.Contributor> contributors)
        {
            bool firstFound = false;
            foreach (DOIDepositData.Contributor contributor in contributors)
            {
                bool isThisFirst = (contributor.Sequence == DOIDepositData.PersonNameSequence.First);
                if (isThisFirst && firstFound) contributor.Sequence = DOIDepositData.PersonNameSequence.Additional;
                firstFound = (firstFound || isThisFirst);
            }
            if (!firstFound) contributors[0].Sequence = DOIDepositData.PersonNameSequence.First;

            return contributors;
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
