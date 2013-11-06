using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.OAI2
{
    public class OAIIdentifier
    {
        private string _identifier = string.Empty;

        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        private DateTime _datestamp;

        public DateTime Datestamp
        {
            get { return _datestamp; }
            set { _datestamp = value; }
        }

        private List<OAISet> _sets;

        public List<OAISet> Sets
        {
            get { return _sets; }
            set { _sets = value; }
        }
    }
}
