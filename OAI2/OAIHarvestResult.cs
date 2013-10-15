using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.OAI2
{
    public class OAIHarvestResult
    {
        private DateTime _responseDate;

        public DateTime ResponseDate
        {
            get { return _responseDate; }
            set { _responseDate = value; }
        }

        private string _responseMessage = string.Empty;

        public string ResponseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        private DateTime _resumptionExpiration;

        public DateTime ResumptionExpiration
        {
            get { return _resumptionExpiration; }
            set { _resumptionExpiration = value; }
        }

        private int _completeListSize;

        public int CompleteListSize
        {
            get { return _completeListSize; }
            set { _completeListSize = value; }
        }

        private int _cursor;

        public int Cursor
        {
            get { return _cursor; }
            set { _cursor = value; }
        }

        private string _resumptionToken = string.Empty;

        public string ResumptionToken
        {
            get { return _resumptionToken; }
            set { _resumptionToken = value; }
        }

        private object _content = null;

        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
}
