using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.OAI2
{
    public class OAIMetadataFormat
    {
        private String _metadataFormat = String.Empty;

        public String MetadataFormat
        {
            get { return _metadataFormat; }
            set { _metadataFormat = value; }
        }

        private String _metadataNamespace = String.Empty;

        public String MetadataNamespace
        {
            get { return _metadataNamespace; }
            set { _metadataNamespace = value; }
        }

        private String _metadataSchema = String.Empty;

        public String MetadataSchema
        {
            get { return _metadataSchema; }
            set { _metadataSchema = value; }
        }

        private String _metadataHandler = String.Empty;

        public String MetadataHandler
        {
            get { return _metadataHandler; }
            set { _metadataHandler = value; }
        }

        private bool _includeExtraDetail = false;

        public bool IncludeExtraDetail
        {
            get { return _includeExtraDetail; }
            set { _includeExtraDetail = value; }
        }

        private int _maxListRecords = 100;

        public int MaxListRecords
        {
            get { return _maxListRecords; }
            set { _maxListRecords = value; }
        }
    }
}
