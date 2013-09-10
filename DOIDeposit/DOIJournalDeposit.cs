using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
				<full_title>Applied Physics Letters</full_title>
				<abbrev_title>Appl. Phys. Lett.</abbrev_title>
				<issn media_type="print">0003-6951</issn>
				<coden>applab</coden>
			</journal_metadata>
			<journal_issue>
				<publication_date media_type="print">
					<year>1999</year>
				</publication_date>
				<journal_volume>
					<volume>74</volume>
				</journal_volume>
				<issue>16</issue>
			</journal_issue>
             */

            throw new NotImplementedException();
        }
    }
}
