using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIJournalDeposit : DOIDeposit
    {

        #region Constructors

        public DOIJournalDeposit()
        {
        }

        public DOIJournalDeposit(DOIDepositData depositData)
        {
            Data = depositData;
        }

        public DOIJournalDeposit(string depositTemplate)
        {
            DepositTemplate = depositTemplate;
        }

        public DOIJournalDeposit(DOIDepositData depositData, string depositTemplate)
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
			<journal_metadata language="en">
				<full_title>Smithsonian contributions to botany</full_title>
				<abbrev_title>Smithson. contrib. bot</abbrev_title>
				<issn media_type="print">0081-024X</issn>
				<issn media_type="electronic">1938-2812</issn>
				<coden>SCBYAJ</coden>
				<doi_data>
						<doi>10.5962/t.100822</doi>
						<resource>https://www.biodiversitylibrary.org/bibliography/100822</resource>
				</doi_data>
			</journal_metadata>
             */

            StringBuilder content = new StringBuilder();

            // Add the header values
            template = template.Replace("{doi_batch_id}", HttpUtility.HtmlEncode(Data.BatchID));
            template = template.Replace("{timestamp}", HttpUtility.HtmlEncode(Data.Timestamp));
            template = template.Replace("{depositor_name}", HttpUtility.HtmlEncode(Data.DepositorName));
            template = template.Replace("{depositor_email_address}", HttpUtility.HtmlEncode(Data.DepositorEmail));
            template = template.Replace("{registrant}", HttpUtility.HtmlEncode(Data.Registrant));

            // Build the journal_metadata content
            if (!string.IsNullOrWhiteSpace(Data.Language))
                content.AppendLine("<journal_metadata language=\"" + HttpUtility.HtmlEncode(Data.Language) + "\">");
            else
                content.AppendLine("<journal_metadata>");

            if (!string.IsNullOrWhiteSpace(Data.Title))
            {
                content.AppendLine("<full_title>" + HttpUtility.HtmlEncode(Data.Title) + "</full_title>");
            }
            if (!string.IsNullOrWhiteSpace(Data.AbbreviatedTitle))
            {
                content.AppendLine("<abbrev_title>" + HttpUtility.HtmlEncode(Data.AbbreviatedTitle) + "</abbrev_title>");
            }
            foreach ((string MediaType, string Value) issn in Data.Issn)
            {
                content.AppendLine("<issn media_type=\"" + HttpUtility.HtmlEncode(issn.MediaType) + "\">" + HttpUtility.HtmlEncode(issn.Value) + "</issn>");
            }
            if (!string.IsNullOrWhiteSpace(Data.Coden))
            {
                content.AppendLine("<coden>" + HttpUtility.HtmlEncode(Data.Coden) + "</coden>");
            }
            content.AppendLine("<doi_data>");
            content.AppendLine("<doi>" + HttpUtility.HtmlEncode(Data.DoiName) + "</doi>");
            content.AppendLine("<resource>" + HttpUtility.HtmlEncode(Data.DoiResource) + "</resource>");
            content.AppendLine("</doi_data>");
            content.AppendLine("</journal_metadata>");

            // Insert the journal metadata into the template and return the result
            return template.Replace("{journal_content}", content.ToString());
        }
    }
}
