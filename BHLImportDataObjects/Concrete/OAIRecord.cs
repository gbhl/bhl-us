using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.DataObjects
{
    [Serializable]
	public class OAIRecord : __OAIRecord
	{
        private List<OAIRecordCreator> _creators = new List<OAIRecordCreator>();

        public List<OAIRecordCreator> Creators
        {
            get { return _creators; }
            set { _creators = value; }
        }

        private List<OAIRecordDCType> _dcTypes = new List<OAIRecordDCType>();

        public List<OAIRecordDCType> DcTypes
        {
            get { return _dcTypes; }
            set { _dcTypes = value; }
        }

        private List<OAIRecordRight> _rights = new List<OAIRecordRight>();

        public List<OAIRecordRight> Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }

        private List<OAIRecordSubject> _subjects = new List<OAIRecordSubject>();

        public List<OAIRecordSubject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private List<OAIRecordRelatedTitle> _relatedTitles = new List<OAIRecordRelatedTitle>();

        public List<OAIRecordRelatedTitle> RelatedTitles
        {
            get { return _relatedTitles; }
            set { _relatedTitles = value; }
        }
	}
}
