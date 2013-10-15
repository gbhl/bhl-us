using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.OAI2
{
    public class OAISet
    {
        private string _setSpec = string.Empty;

        public string SetSpec
        {
            get { return _setSpec; }
            set { _setSpec = value; }
        }

        private string _setName = string.Empty;

        public string SetName
        {
            get { return _setName; }
            set { _setName = value; }
        }
    }
}
