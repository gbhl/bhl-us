using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DOIDeposit
{
    public abstract class DOIDeposit : iDOIDeposit
    {
        private DOIDepositData _data;
        public DOIDepositData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private string _depositTemplate = string.Empty;
        public string DepositTemplate
        {
            get { return _depositTemplate; }
            set { _depositTemplate = value; }
        }

        #region iDOIDeposit Members

        public new abstract string ToString();
        public new abstract string ToString(string template);

        #endregion
    }
}
