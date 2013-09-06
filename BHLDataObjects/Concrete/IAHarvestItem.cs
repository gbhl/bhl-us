using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.DataObjects
{
    public class IAHarvestItem
    {
        private int _itemId = 0;

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        private string _iaIdentifier = string.Empty;

        public string IAIdentifier
        {
            get { return _iaIdentifier; }
            set { _iaIdentifier = value; }
        }

        private string _sponsor = string.Empty;

        public string Sponsor
        {
            get { return _sponsor; }
            set { _sponsor = value; }
        }

        private string _scanningCenter = string.Empty;

        public string ScanningCenter
        {
            get { return _scanningCenter; }
            set { _scanningCenter = value; }
        }

        private string _volume = string.Empty;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _scanDate = string.Empty;

        public string ScanDate
        {
            get { return _scanDate; }
            set { _scanDate = value; }
        }

        private string _externalStatus = string.Empty;

        public string ExternalStatus
        {
            get { return _externalStatus; }
            set { _externalStatus = value; }
        }

        private int _totalItems = 0;

        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; }
        }
    }
}
