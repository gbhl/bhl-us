using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
	public partial class PagingUserControl : System.Web.UI.UserControl
	{
		private int _startIndex = 1;
		private int _endIndex = 100;
		private int _pageNumber = 1;
		private int _pageSize = 100;
		private int _totalRecords = 1;

		public event EventHandler<EventArgs> Search;

		protected void Page_Load( object sender, EventArgs e )
		{
			if ( IsPostBack )
			{
				if ( ViewState[ "PageNumber" ] != null && ViewState[ "TotalRecords" ] != null )
				{
					_pageNumber = (int)ViewState[ "PageNumber" ];
					_totalRecords = (int)ViewState[ "TotalRecords" ];
				}
			}

			pageTextBox.Attributes.Add( "onKeyPress",
				"javascript:if (event.keyCode == 13) __doPostBack('" + pageButton.UniqueID + "','')" );
		}

		public int TotalRecords
		{
			get
			{
				return _totalRecords;
			}
			set
			{
				_totalRecords = value;
				ViewState[ "TotalRecords" ] = _totalRecords;
			}
		}

		public int TotalPages
		{
			get
			{
				return calcTotalPages();
			}
		}

		public int PageNumber
		{
			get
			{
				return _pageNumber;
			}
			set
			{
				_pageNumber = value;
				ViewState[ "PageNumber" ] = value;
			}
		}

		public int StartIndex
		{
			get
			{
				return _startIndex;
			}
		}

		public int EndIndex
		{
			get
			{
				return _endIndex;
			}
		}

		public void UpdateDisplay()
		{
			pageTextBox.Text = _pageNumber.ToString();
			int totalPages = calcTotalPages();
			if ( totalPages == 0 )
			{
				pageLabel.Text = "";
				pageTextBox.Visible = false;
				totalPagesLabel.Text = "";
			}
			else
			{
				pageLabel.Text = "Page";
				pageTextBox.Visible = true;
				totalPagesLabel.Text = "of " + totalPages.ToString();
			}

			int ei = calcEndIndex();
			if ( ei > _totalRecords )
			{
				ei = _totalRecords;
			}
			if ( ei == 0 )
			{
				totalRecordsLabel.Text = "No records found";
			}
			else
			{
				totalRecordsLabel.Text = "Records " + calcStartIndex().ToString() + " - " + ei.ToString() + " of " +
					_totalRecords.ToString();
			}

			updateNavControlsUI();
			pageTextBox.Enabled = true;
		}

		private void updateNavControlsUI()
		{
			if ( _pageNumber == 1 )
			{
				prevPageButton.Visible = false;
				firstPageButton.Visible = false;
				int totalPages = calcTotalPages();
				nextPageButton.Visible = ( totalPages > 1 );
				lastPageButton.Visible = ( totalPages > 1 );
			}
			else
			{
				prevPageButton.Visible = true;
				firstPageButton.Visible = true;

				if ( ( _pageNumber * _pageSize ) >= _totalRecords )
				{
					nextPageButton.Visible = false;
					lastPageButton.Visible = false;
				}
				else
				{
					nextPageButton.Visible = true;
					lastPageButton.Visible = true;
				}
			}
		}

		private int calcStartIndex()
		{
			return ( ( _pageNumber - 1 ) * _pageSize ) + 1;
		}

		private int calcEndIndex()
		{
			return ( _pageNumber * _pageSize );
		}

		private int calcTotalPages()
		{
			int totalPages = _totalRecords / _pageSize;
			float ftotalPages = (float)_totalRecords / (float)_pageSize;

			if ( ftotalPages > totalPages )
			{
				totalPages++;
			}

			return totalPages;
		}

		private void raiseSearch()
		{
			ViewState[ "PageNumber" ] = _pageNumber;

			EventHandler<EventArgs> handler = Search;
			if ( handler != null )
			{
				handler( this, null );
			}
		}

		#region Event handlers

		protected void firstPageButton_Click( object sender, ImageClickEventArgs e )
		{
			_startIndex = 1;
			_pageNumber = 1;
			raiseSearch();
		}

		protected void prevPageButton_Click( object sender, ImageClickEventArgs e )
		{
			_pageNumber = (int)ViewState[ "PageNumber" ];
			_pageNumber = _pageNumber - 1;
			_startIndex = calcStartIndex();
			_endIndex = calcEndIndex();
			raiseSearch();
		}

		protected void nextPageButton_Click( object sender, ImageClickEventArgs e )
		{
			_pageNumber = (int)ViewState[ "PageNumber" ];
			_pageNumber = _pageNumber + 1;
			_startIndex = calcStartIndex();
			_endIndex = calcEndIndex();
			raiseSearch();
		}

		protected void lastPageButton_Click( object sender, ImageClickEventArgs e )
		{
			_pageNumber = calcTotalPages();
			_startIndex = calcStartIndex();
			_endIndex = _totalRecords;
			raiseSearch();
		}

		protected void pageButton_Click( object sender, EventArgs e )
		{
			if ( pageTextBox.Text.Trim().Length > 0 )
			{
				int page = 0;
				bool flag = int.TryParse( pageTextBox.Text.Trim(), out page );
				if ( flag )
				{
					_pageNumber = page;

					int totalPages = calcTotalPages();
					if ( _pageNumber > totalPages )
					{
						_pageNumber = totalPages;
					}
					_startIndex = calcStartIndex();
					_endIndex = calcEndIndex();
					raiseSearch();
				}
			}
		}

		#endregion
	}
}