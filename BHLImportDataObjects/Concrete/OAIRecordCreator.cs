
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class OAIRecordCreator : __OAIRecordCreator
	{
        private List<OAIRecordCreatorIdentifier> _identifiers = new List<OAIRecordCreatorIdentifier>();

        public List<OAIRecordCreatorIdentifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }
    }
}
