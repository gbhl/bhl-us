using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.BHLAuditExport
{
    public class AuditEntry
    {
        private string _type = string.Empty;

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _table = string.Empty;

        public string table
        {
            get { return _table; }
            set { _table = value; }
        }

        private string _primaryKeyName0 = string.Empty;

        public string primaryKeyName0
        {
            get { return _primaryKeyName0; }
            set { _primaryKeyName0 = value; }
        }

        private string _primaryKeyName1 = string.Empty;

        public string primaryKeyName1
        {
            get { return _primaryKeyName1; }
            set { _primaryKeyName1 = value; }
        }

        private string _primaryKeyName2 = string.Empty;

        public string primaryKeyName2
        {
            get { return _primaryKeyName2; }
            set { _primaryKeyName2 = value; }
        }

        private Dictionary<string, string> _row = new Dictionary<string, string>();

        public Dictionary<string, string> row
        {
            get { return _row; }
            set { _row = value; }
        }
    }
}
