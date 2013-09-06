#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Stats
	{
		private int _titleCount;
		private int _titleTotal;
		private int _volumeTotal;
		private int _pageTotal;
        private int _segmentCount;
        private int _segmentTotal;
        private int _itemSegmentCount;
        private int _itemSegmentTotal;
        private int _nameCount;
        private int _nameTotal;
		private int _uniqueNameCount;
		private int _uniqueNameTotal;
        private int _verifiedNameCount;
        private int _verifiedNameTotal;
        private int _eolNameCount;
        private int _eolNameTotal;
        private int _eolPageCount;
        private int _eolPageTotal;

		public int TitleCount
		{
			get
			{
				return _titleCount;
			}
			set
			{
				_titleCount = value;
			}
		}

		private int _volumeCount;

		public int VolumeCount
		{
			get
			{
				return _volumeCount;
			}
			set
			{
				_volumeCount = value;
			}
		}

		private int _pageCount;

		public int PageCount
		{
			get
			{
				return _pageCount;
			}
			set
			{
				_pageCount = value;
			}
		}

		public int TitleTotal
		{
			get { return this._titleTotal; }
			set { this._titleTotal = value; }
		}

		public int VolumeTotal
		{
			get { return this._volumeTotal; }
			set { this._volumeTotal = value; }
		}

		public int PageTotal
		{
			get { return this._pageTotal; }
			set { this._pageTotal = value; }
		}

        public int SegmentCount
        {
            get { return this._segmentCount; }
            set { this._segmentCount = value; }
        }

        public int SegmentTotal
        {
            get { return this._segmentTotal; }
            set { this._segmentTotal = value; }
        }

        public int ItemSegmentCount
        {
            get { return this._itemSegmentCount; }
            set { this._itemSegmentCount = value; }
        }

        public int ItemSegmentTotal
        {
            get { return this._itemSegmentTotal; }
            set { this._itemSegmentTotal = value; }
        }

        public int NameCount
        {
            get { return this._nameCount; }
            set { this._nameCount = value; }
        }

        public int NameTotal
        {
            get { return _nameTotal; }
            set { _nameTotal = value; }
        }

		public int UniqueNameCount
		{
			get { return this._uniqueNameCount; }
			set { this._uniqueNameCount = value; }
		}

		public int UniqueNameTotal
		{
			get { return this._uniqueNameTotal; }
			set { this._uniqueNameTotal = value; }
		}

        public int VerifiedNameCount
        {
            get { return _verifiedNameCount; }
            set { _verifiedNameCount = value; }
        }

        public int VerifiedNameTotal
        {
            get { return _verifiedNameTotal; }
            set { _verifiedNameTotal = value; }
        }

        public int EolNameCount
        {
            get { return _eolNameCount; }
            set { _eolNameCount = value; }
        }

        public int EolNameTotal
        {
            get { return _eolNameTotal; }
            set { _eolNameTotal = value; }
        }

        public int EolPageCount
        {
            get { return _eolPageCount; }
            set { _eolPageCount = value; }
        }

        public int EolPageTotal
        {
            get { return _eolPageTotal; }
            set { _eolPageTotal = value; }
        }
    }
}