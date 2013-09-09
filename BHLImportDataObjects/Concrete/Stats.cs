#region Using

using System;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
    [Serializable]
    public class Stats
    {
        private int _numberOfItems;
        private int _ageInDays;
        private int _itemStatusID;
        private string _status;
        private string _description;
        private string _source;
        private string _type;

        public int NumberOfItems
        {
            get
            {
                return _numberOfItems;
            }
            set
            {
                _numberOfItems = value;
            }
        }

        public int AgeInDays
        {
            get
            {
                return _ageInDays;
            }
            set
            {
                _ageInDays = value;
            }
        }

        public int ItemStatusID
        {
            get
            {
                return _itemStatusID;
            }
            set
            {
                _itemStatusID = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
    }
}
