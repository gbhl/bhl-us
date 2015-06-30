using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DOIDeposit
{
    public abstract class DOIQuery : iDOIQuery
    {
        private DOIDepositData _data;
        public DOIDepositData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private string _queryTemplate = string.Empty;
        public string QueryTemplate
        {
            get { return _queryTemplate; }
            set { _queryTemplate = value; }
        }

        #region iDOIQuery Members

        public new abstract string ToString();
        public abstract string ToString(string template);

        #endregion
    }
}
