using System;
using CustomDataAccess;

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class OAIRecord : __OAIRecord
	{
        private CustomGenericList<OAIRecordCreator> _creators = new CustomGenericList<OAIRecordCreator>();

        public CustomGenericList<OAIRecordCreator> Creators
        {
            get { return _creators; }
            set { _creators = value; }
        }

        private CustomGenericList<OAIRecordDCType> _dcTypes = new CustomGenericList<OAIRecordDCType>();

        public CustomGenericList<OAIRecordDCType> DcTypes
        {
            get { return _dcTypes; }
            set { _dcTypes = value; }
        }

        private CustomGenericList<OAIRecordRight> _rights = new CustomGenericList<OAIRecordRight>();

        public CustomGenericList<OAIRecordRight> Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }

        private CustomGenericList<OAIRecordSubject> _subjects = new CustomGenericList<OAIRecordSubject>();

        public CustomGenericList<OAIRecordSubject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }
	}
}
