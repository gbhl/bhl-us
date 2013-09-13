using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class GNResolverResponse
    {
        public GNResolverResponse()
        {
        }

        private int _dataSourceID = 0;
        private string _dataSourceTitle = string.Empty;
        private string _gniUUID = string.Empty;
        private string _nameString = string.Empty;
        private string _canonicalForm = string.Empty;
        private string _classificationPath = string.Empty;
        private string _classificationPathRanks = string.Empty;
        private string _classificationPathIDs = string.Empty;
        private string _taxonID = string.Empty;
        private string _localID = string.Empty;
        private string _globalID = string.Empty;
        private string _currentTaxonID = string.Empty;
        private string _currentNameString = string.Empty;
        private string _url = string.Empty;
        private int _matchType = 0;
        private double _score = 0;

        public int DataSourceID
        {
            get { return _dataSourceID; }
            set { _dataSourceID = value; }
        }

        public string DataSourceTitle
        {
            get { return _dataSourceTitle; }
            set { _dataSourceTitle = value; }
        }

        public string GniUUID
        {
            get { return _gniUUID; }
            set { _gniUUID = value; }
        }

        public string NameString
        {
            get { return _nameString; }
            set { _nameString = value; }
        }

        public string CanonicalForm
        {
            get { return _canonicalForm; }
            set { _canonicalForm = value; }
        }

        public string ClassificationPath
        {
            get { return _classificationPath; }
            set { _classificationPath = value; }
        }

        public string ClassificationPathRanks
        {
            get { return _classificationPathRanks; }
            set { _classificationPathRanks = value; }
        }

        public string ClassificationPathIDs
        {
            get { return _classificationPathIDs; }
            set { _classificationPathIDs = value; }
        }

        public string TaxonID
        {
            get { return _taxonID; }
            set { _taxonID = value; }
        }

        public string LocalID
        {
            get { return _localID; }
            set { _localID = value; }
        }

        public string GlobalID
        {
            get { return _globalID; }
            set { _globalID = value; }
        }

        public string CurrentTaxonID
        {
            get { return _currentTaxonID; }
            set { _currentTaxonID = value; }
        }

        public string CurrentNameString
        {
            get { return _currentNameString; }
            set { _currentNameString = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public int MatchType
        {
            get { return _matchType; }
            set { _matchType = value; }
        }

        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }

    }
}
