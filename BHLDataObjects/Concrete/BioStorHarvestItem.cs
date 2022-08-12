using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.DataObjects
{
    public class BioStorHarvestItem
    {
        private int _itemId = 0;

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        private int? _bhlItemId = 0;

        public int? BHLItemId
        {
            get { return _bhlItemId; }
            set { _bhlItemId = value; }
        }

        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _volume = string.Empty;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private int _totalSegments = 0;

        public int TotalSegments
        {
            get { return _totalSegments; }
            set { _totalSegments = value; }
        }

        private int _publishedSegments = 0;

        public int PublishedSegments
        {
            get { return _publishedSegments; }
            set { _publishedSegments = value; }
        }

        private int _skippedSegments = 0;

        public int SkippedSegments
        {
            get { return _skippedSegments; }
            set { _skippedSegments = value; }
        }

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        private int _totalItems = 0;

        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; }
        }
    }
}
