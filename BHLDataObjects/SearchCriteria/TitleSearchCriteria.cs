using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleSearchCriteria
	{
		private int? _titleID;
		private string _MARCBibId;
		private string _title;

		private int _pageSize = 1000;
		private long _startRow = 1;
		private TitleSearchOrderBy _orderBy = TitleSearchOrderBy.Title;
		private SortOrder _sortOrder = SortOrder.Ascending;

		public TitleSearchCriteria()
		{ }

		public int? TitleID
		{
			get { return this._titleID; }
			set { this._titleID = value; }
		}

		public string MARCBibID
		{
			get { return this._MARCBibId; }
			set { this._MARCBibId = value; }
		}

		public string Title
		{
			get { return this._title; }
			set { this._title = value; }
		}

		public int PageSize
		{
			get { return this._pageSize; }
			set { this._pageSize = value; }
		}

		public long StartRow
		{
			get { return this._startRow; }
			set { this._startRow = value; }
		}

		public TitleSearchOrderBy OrderBy
		{
			get { return this._orderBy; }
			set { this._orderBy = value; }
		}

		public SortOrder SortOrder
		{
			get { return this._sortOrder; }
			set { this._sortOrder = value; }
		}
	}
}