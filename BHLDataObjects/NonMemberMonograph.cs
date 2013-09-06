using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class NonMemberMonograph : ISetValues
    {
        private int _titleID;
        public int TitleID
        {
            get { return _titleID; }
            set { _titleID = value; }
        }

        private string _oclc;
        public string Oclc
        {
            get { return _oclc; }
            set { _oclc = value; }
        }

        private string _fullTitle;
        public string FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        private string _authors;
        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private string _startYear;
        public string StartYear
        {
            get { return _startYear; }
            set { _startYear = value; }
        }

        private string _callNumber;
        public string CallNumber
        {
            get { return _callNumber; }
            set { _callNumber = value; }
        }

        private string _publisher;
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        private string _publisherPlace;
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        private int _itemID;
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private string _volume;
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _identifierBib;
        public string IdentifierBib
        {
            get { return _identifierBib; }
            set { _identifierBib = value; }
        }

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleID":
                        {
                            TitleID = (int)column.Value;
                            break;
                        }
                    case "OCLC":
                        {
                            Oclc = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FullTitle":
                        {
                            FullTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            Authors = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "StartYear":
                        {
                            _startYear = Utility.EmptyIfNull(column.Value.ToString());
                            break;
                        }
                    case "CallNumber":
                        {
                            _callNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Publisher":
                        {
                            _publisher = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublisherPlace":
                        {
                            _publisherPlace = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ItemID":
                        {
                            ItemID = (int)column.Value;
                            break;
                        }
                    case "Volume":
                        {
                            Volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "IdentifierBib":
                        {
                            _identifierBib = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
