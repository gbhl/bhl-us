using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.OAI2
{
    public class OAIIdentity
    {
        private string _repositoryName = string.Empty;

        public string RepositoryName
        {
            get { return _repositoryName; }
            set { _repositoryName = value; }
        }

        private string _baseUrl = string.Empty;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        private string _protocolVersion = string.Empty;

        public string ProtocolVersion
        {
            get { return _protocolVersion; }
            set { _protocolVersion = value; }
        }

        private string _adminEmail = string.Empty;

        public string AdminEmail
        {
            get { return _adminEmail; }
            set { _adminEmail = value; }
        }

        private string _earliestDatestamp = string.Empty;

        public string EarliestDatestamp
        {
            get { return _earliestDatestamp; }
            set { _earliestDatestamp = value; }
        }

        private string _deletedRecord = string.Empty;

        public string DeletedRecord
        {
            get { return _deletedRecord; }
            set { _deletedRecord = value; }
        }

        private string _granularity = string.Empty;

        public string Granularity
        {
            get { return _granularity; }
            set { _granularity = value; }
        }
    }
}
