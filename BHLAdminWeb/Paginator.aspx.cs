using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;
using SortOrder = CustomDataAccess.SortOrder;
using Page = MOBOT.BHL.DataObjects.Page;
using FlickrUtility;

namespace MOBOT.BHL.AdminWeb
{
	public partial class Paginator : System.Web.UI.Page
	{
		private BHLProvider bp = new BHLProvider();
		private bool _maintainScrollPos = true;
		private string _lockEditStatus = "Lock For Editing";
		private string _completeStatus = "Complete";
		private string _unlockStatus = "Unlock";
		private string _pendingStatus = "Incomplete";

        public string GetOAuthToken { get { return hidAuthToken.Value; } }
        public string GetOAuthTokenSecret { get { return hidAuthTokenSecret.Value; } }

		private void fillItemsDropDown( int titleId )
		{
			CustomGenericList<Item> items = bp.ItemSelectByTitleId( titleId );

            itemDropDownList.DataSource = items;
			itemDropDownList.DataTextField = "Volume";
			itemDropDownList.DataValueField = "ItemID";
			itemDropDownList.DataBind();

			if ( items.Count == 0 )
			{
				detailGridView.DataSource = null;
				detailGridView.DataBind();
				clearAll();
			}
			else
			{
				fillPageList( items[ 0 ].ItemID );
			}
		}

		private void fillPageList( int itemId )
		{
			checkPaginationStatus();
			CustomGenericList<Page> pages = bp.PageMetadataSelectByItemID( itemId );

			detailGridView.DataSource = pages;
			detailGridView.DataBind();
			clearAll();
		}

		private void loadPageTypes()
		{
			CustomGenericList<PageType> pageTypes = bp.PageTypeSelectAll();
			pageTypeCombo.DataSource = pageTypes;
			pageTypeCombo.DataValueField = "PageTypeID";
			pageTypeCombo.DataTextField = "PageTypeName";
			pageTypeCombo.DataBind();
		}

		private List<int> getSelectedPageIds()
		{
			List<int> pageIds = new List<int>();
			int ix = 0;
			foreach ( GridViewRow gvr in detailGridView.Rows )
			{
				CheckBox cb = (CheckBox)gvr.FindControl( "pageCheckBox" );
				if ( cb.Checked )
				{
					pageIds.Add( (int)detailGridView.DataKeys[ ix ].Value );
				}
				ix++;
			}

			return pageIds;
		}

		private void clearAll()
		{
			//pageFrame.Attributes.Add( "src", "" );
			ViewState[ "RowNum" ] = null;
			_maintainScrollPos = false;
		}

		private void clearInputs()
		{
			yearTextBox.Text = "";
			volumeTextBox.Text = "";
			issueTextBox.Text = "";
			issuePrefixCombo.SelectedIndex = 0;
			pageTypeCombo.SelectedIndex = 0;
		}

		private void checkPaginationStatus()
		{
			int itemId = int.Parse( itemDropDownList.SelectedValue );
			Item item = bp.ItemSelectPagination( itemId );

			if ( item != null )
			{
				paginationStatusLabel.Text = PaginationStatus.GetStatusString( item.PaginationStatusID );

				if ( item.PaginationStatusID != 5 )
				{
					DateTime paginationStatusDate = (DateTime)item.PaginationStatusDate;
					paginationDetailStatusLabel.Text = "Pagination status set by " + item.PaginationUserName + " on " +
						paginationStatusDate.ToShortDateString() + " at " + paginationStatusDate.ToShortTimeString();
					if ( item.PaginationStatusID == (int)PaginationStatus.InProgress )
					{
						// Look up userid based on token string
                        int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                        configurePaginationStatusButtons( item, ( item.PaginationStatusUserID == userId ) );
						toggleButtons( item.PaginationStatusUserID == userId );
					}
					else
					{
						configurePaginationStatusButtons( item, true );
						toggleButtons( false );
					}
				}
				else
				{
					paginationDetailStatusLabel.Text = "Pagination status has not been manually updated";
					configurePaginationStatusButtons( item, true );
					toggleButtons( false );
				}
			}
		}

		private void configurePaginationStatusButtons( Item selectedItem, bool enabled )
		{
			if ( selectedItem.PaginationStatusID.HasValue == false ||
                selectedItem.PaginationStatusID.Value == PaginationStatus.New ||
				selectedItem.PaginationStatusID.Value == PaginationStatus.Pending )
			{
				lockButton.Text = _lockEditStatus;
				statusButton.Text = _completeStatus;
			}
			else if ( selectedItem.PaginationStatusID.Value == PaginationStatus.InProgress )
			{
				lockButton.Text = _unlockStatus;
				statusButton.Text = _completeStatus;
			}
			else if ( selectedItem.PaginationStatusID.Value == PaginationStatus.Complete )
			{
				lockButton.Text = _pendingStatus;
				statusButton.Text = _lockEditStatus;
			}
			lockButton.Enabled = enabled;
			statusButton.Enabled = enabled;
		}

		private void toggleButtons( bool enabled )
		{
			clearIndicatedPageButton.Enabled = enabled;
			showIndPageDialogButton.Disabled = !enabled;
			assignIssueButton.Enabled = enabled;
			assignPageTypeButton.Enabled = enabled;
            replacePageTypeButton.Enabled = enabled;
			clearPageTypeButton.Enabled = enabled;
			assignVolumeButton.Enabled = enabled;
			assignYearAndVolumeButton.Enabled = enabled;
			assignYearButton.Enabled = enabled;
		}

		private void displayPaginationStatusInvalid()
		{
			checkPaginationStatus();
			errorControl.AddErrorText( "The pagination status you attempted to set is no longer valid." );
			errorControl.Visible = true;
		}

		private void updatePaginationStatus( int itemId, int paginationStatusId, int userId )
		{
			bp.ItemUpdatePaginationStatus( itemId, paginationStatusId, userId );
			checkPaginationStatus();
		}

		#region Event handlers

		protected void Page_Load( object sender, EventArgs e )
		{
            // Hardcoding an old version of jQuery here because the bookviewer code requires it
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, "http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js");

			if ( !IsPostBack )
			{
				loadPageTypes();

                String titleIDString = Request.QueryString["TitleID"] as String;
                int titleID = 0;
                if (titleIDString != null) Int32.TryParse(titleIDString, out titleID);
                if (titleID != 0)
                {
                    Title title = bp.TitleSelectAuto(titleID);
                    litTitle.Text = title.DisplayedShortTitle;
                }

                fillItemsDropDown(titleID);

                String itemIDString = Request.QueryString["ItemID"] as String;
                int itemID = 0;
                if (itemIDString != null) Int32.TryParse(itemIDString, out itemID);
                if (itemID != 0)
                {
                    itemDropDownList.SelectedValue = itemID.ToString();
                    fillPageList(itemID);
                }
			}

			errorControl.Visible = false;
		}

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

			#region Used to maintain scroll position in gridview

			string js;
			ClientScriptManager sm = Page.ClientScript;
			if ( !sm.IsOnSubmitStatementRegistered( this.GetType(), "SaveScrollPosition" ) )
			{
				js = "var hfScrollPos = document.getElementById('" + hidScrollPosition.ClientID + "');\n\r";
				js += "var gridPanel = document.getElementById('" + gridPanel.ClientID + "');\n\r";
				js += "hfScrollPos.value = gridPanel.scrollTop;\n\r";
				sm.RegisterOnSubmitStatement( this.GetType(), "SaveScrollPosition", js );
			}

			if ( _maintainScrollPos )
			{
				if ( !sm.IsStartupScriptRegistered( this.GetType(), "RetrieveScrollPosition" ) )
				{
					js = "var hfScrollPos = document.getElementById('" + hidScrollPosition.ClientID + "');\n\r";
					js += "var gridPanel = document.getElementById('" + gridPanel.ClientID + "');\n\r";
					js += "if ( hfScrollPos.value != '' )\n\r";
					js += "{\n\r";
					js += "  gridPanel.scrollTop = hfScrollPos.value;\n\r";
					js += "}\n\r";

					sm.RegisterStartupScript( this.GetType(), "RetrieveScrollPosition", js, true );
				}
			}

			#endregion
		}

		protected void itemDropDownList_SelectedIndexChanged( object sender, EventArgs e )
		{
			int itemId = int.Parse( itemDropDownList.SelectedValue );
			fillPageList( itemId );
		}

		protected void detailGridView_RowCommand( object sender, GridViewCommandEventArgs e )
		{
			if ( e.CommandName.Equals( "showPageLinkButton" ) )
			{
				int rowNum = int.Parse( e.CommandArgument.ToString() );
				int pageId = (int)detailGridView.DataKeys[ rowNum ].Value;
				PageSummaryView ps = bp.PageSummarySelectByPageId( pageId );

                // Set the Book Reader properties
                BookReader1.ItemID = ps.ItemID;
                BookReader1.NumPages = new BHLProvider().PageSelectCountByItemID(ps.ItemID);
                BookReader1.StartPage = ps.SequenceOrder;
                BookReader1.FixedImageHeight = 2400;
                BookReader1.FixedImageWidth = 1600;

				// Reset previous row
				if ( ViewState[ "RowNum" ] != null )
				{
					int oldRowNum = int.Parse( ViewState[ "RowNum" ].ToString() );
					if ( ( oldRowNum - 1 ) / 2 == oldRowNum / 2 )
					{
						detailGridView.Rows[ oldRowNum ].BackColor = System.Drawing.Color.FromArgb( 255, 255, 215 );
					}
					else
					{
						detailGridView.Rows[ oldRowNum ].BackColor = System.Drawing.Color.White;
					}
				}

				// Shade viewed row
				detailGridView.Rows[ rowNum ].BackColor = System.Drawing.Color.FromArgb( 228, 226, 213 );
				ViewState[ "RowNum" ] = rowNum;
			}
		}

		protected void detailGridView_RowDataBound( object sender, GridViewRowEventArgs e )
		{
			if ( e.Row.RowType == DataControlRowType.DataRow )
			{
				ImageButton imgButton = (ImageButton)e.Row.Cells[ 1 ].Controls[ 0 ];

				Page page = (Page)e.Row.DataItem;

				if ( page.PageTypes.ToLower().IndexOf( "blank" ) >= 0 )
				{
					imgButton.ImageUrl = "images/blankpage.png";
				}
				else if ( page.PageTypes.ToLower().IndexOf( "illustration" ) >= 0 ||
					page.PageTypes.ToLower().IndexOf( "map" ) >= 0 )
				{
					imgButton.ImageUrl = "images/illustration.png";
                }
				else if ( page.PageTypes.ToLower().IndexOf( "cover" ) >= 0 )
				{
					imgButton.ImageUrl = "images/cover.gif";
                }
				else
				{
					imgButton.ImageUrl = "images/textpage.png";
                }

                HiddenField hidPageId = (HiddenField)e.Row.FindControl("hidPageId");
                hidPageId.Value = page.PageID.ToString();

                ImageButton flickrLinkButton = (ImageButton)e.Row.FindControl("FlickrLinkButton");
                if (page.FlickrURL.Length == 0)
                    flickrLinkButton.Visible = false;
                else
                {
                    flickrLinkButton.ImageUrl = "images/flickr_sml.png";
                    flickrLinkButton.Attributes.Add("onclick", "window.open('" + page.FlickrURL +
                    "', null,'left=100, top=100, height=600, width=800, status=no, toolbar=no, location=no, menubar=no," +
                    "resizable=yes, scrollbars=yes');return false");
                }

                Literal sequenceOrder = (Literal)e.Row.FindControl("SequenceOrder");
                sequenceOrder.Text = (page.SequenceOrder.HasValue ? page.SequenceOrder.Value.ToString() : "");

                Literal year = (Literal)e.Row.FindControl("Year");
                year.Text = (page.Year ?? "");

                Literal volume = (Literal)e.Row.FindControl("Volume");
                volume.Text = (page.Volume ?? "");

                Literal issuePrefix = (Literal)e.Row.FindControl("IssuePrefix");
                issuePrefix.Text = (page.IssuePrefix ?? "");

                Literal issue = (Literal)e.Row.FindControl("Issue");
                issue.Text = (page.Issue ?? "");

                Literal indicatedPages = (Literal)e.Row.FindControl("IndicatedPages");
                indicatedPages.Text = (page.IndicatedPages ?? "");

                Literal pageTypes = (Literal)e.Row.FindControl("PageTypes");
                pageTypes.Text = (page.PageTypes ?? "");
			}
		}

		protected void selectAllButton_Click( object sender, EventArgs e )
		{
			foreach ( GridViewRow gvr in detailGridView.Rows )
			{
				CheckBox cb = (CheckBox)gvr.FindControl( "pageCheckBox" );
				cb.Checked = true;
			}
		}

		protected void selectNoneButton_Click( object sender, EventArgs e )
		{
			foreach ( GridViewRow gvr in detailGridView.Rows )
			{
				CheckBox cb = (CheckBox)gvr.FindControl( "pageCheckBox" );
				cb.Checked = false;
			}
		}

		protected void selectInverseButton_Click( object sender, EventArgs e )
		{
			foreach ( GridViewRow gvr in detailGridView.Rows )
			{
				CheckBox cb = (CheckBox)gvr.FindControl( "pageCheckBox" );
				cb.Checked = !cb.Checked;
			}
		}

		protected void selectBetweenButton_Click( object sender, EventArgs e )
		{
			// Find first gap between two checked checkboxs
			int ix = 0;
			int x = 0;
			int y = 0;
			bool flag = false;
			foreach ( GridViewRow gvr in detailGridView.Rows )
			{
				CheckBox cb = (CheckBox)gvr.FindControl( "pageCheckBox" );
				if ( cb.Checked )
				{
					if ( flag )
					{
						if ( ix == x + 1 )
						{
							x = ix;
						}
						else
						{
							y = ix;
							break;
						}
					}
					else
					{
						x = ix;
						flag = true;
					}
				}
				ix++;
			}

			if ( y > x )
			{
				for ( int i = x; i < y; i++ )
				{
					GridViewRow gvr = detailGridView.Rows[ i ];
					CheckBox cb = (CheckBox)gvr.FindControl( "pageCheckBox" );
					cb.Checked = true;
				}
			}
		}

		protected void assignYearButton_Click( object sender, EventArgs e )
		{
			bool flag = false;
			List<int> pages = getSelectedPageIds();
			if ( pages.Count == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "You must select at least one page to update." );
			}
			else
			{
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                int[] arrPages = new int[ pages.Count ];
				pages.CopyTo( arrPages );
				bp.PageUpdateYear( arrPages, yearTextBox.Text.Trim(), userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("Year")).Text = yearTextBox.Text.Trim();
                    }
                }

				clearInputs();
			}

			errorControl.Visible = flag;
		}

		protected void assignVolumeButton_Click( object sender, EventArgs e )
		{
			bool flag = false;
			List<int> pages = getSelectedPageIds();
			if ( pages.Count == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "You must select at least one page to update." );
			}
			else
			{
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                int[] arrPages = new int[ pages.Count ];
				pages.CopyTo( arrPages );
				bp.PageUpdateVolume( arrPages, volumeTextBox.Text.Trim(), userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("Volume")).Text = volumeTextBox.Text.Trim();
                    }
                }

				clearInputs();
			}

			errorControl.Visible = flag;
		}

		protected void assignYearAndVolumeButton_Click( object sender, EventArgs e )
		{
			bool flag = false;
			List<int> pages = getSelectedPageIds();
			if ( pages.Count == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "You must select at least one page to update." );
			}
			else
			{
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                int[] arrPages = new int[ pages.Count ];
				pages.CopyTo( arrPages );
				bp.PageUpdateYear( arrPages, yearTextBox.Text.Trim(), userId );
				bp.PageUpdateVolume( arrPages, volumeTextBox.Text.Trim(), userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("Year")).Text = yearTextBox.Text.Trim();
                        ((Literal)gvr.FindControl("Volume")).Text = volumeTextBox.Text.Trim();
                    }
                }

				clearInputs();
			}

			errorControl.Visible = flag;
		}

		protected void assignIssueButton_Click( object sender, EventArgs e )
		{
			bool flag = false;
			List<int> pages = getSelectedPageIds();
			if ( pages.Count == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "You must select at least one page to update." );
			}
			else
			{
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                int[] arrPages = new int[pages.Count];
				pages.CopyTo( arrPages );
				bp.PageUpdateIssue( arrPages, issuePrefixCombo.SelectedItem.Text, issueTextBox.Text.Trim(), userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("IssuePrefix")).Text = issuePrefixCombo.SelectedItem.Text;
                        ((Literal)gvr.FindControl("Issue")).Text = issueTextBox.Text.Trim();
                    }
                }

				clearInputs();
			}

			errorControl.Visible = flag;
		}

        protected void FlickrUpload_Click(object sender, EventArgs e)
		{
            //1 - check to see if current flickr cookie is valid, if so, then avoid login steps
            string oAuthAccessToken = "";
            if (Request.Cookies["oAuthAccessToken"] != null)
            {
                oAuthAccessToken = Request.Cookies["oAuthAccessToken"].Value;
            }
            string oAuthAccessTokenSecret = "";
            if (Request.Cookies["oAuthAccessTokenSecret"] != null)
            {
                oAuthAccessTokenSecret = Request.Cookies["oAuthAccessTokenSecret"].Value;
            }

            string qsParams = "&rotate=" + RotateImage.SelectedValue;
            qsParams += "&titleid=" + Request.QueryString["TitleID"];
            string pageIds = "";
            foreach (GridViewRow gvr in detailGridView.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                if (cb.Checked)
                {
                    HiddenField hidPageId = (HiddenField)gvr.FindControl("hidPageId");
                    pageIds += hidPageId.Value + "|";
                }
            }
            if (pageIds.Length == 0)
            {
                errorControl.AddErrorText("You must select at least one page to upload.");
                errorControl.Visible = true;
            }
            else
            {
                hidPageIds.Value = pageIds.Substring(0, pageIds.Length - 1);
                qsParams += "&pageids=" + hidPageIds.ClientID;

                errorControl.Visible = false;

                if (oAuthAccessToken.Length == 0 || oAuthAccessTokenSecret.Length == 0 || 
                    !FlickrTools.OAuthAccessTokenValid(oAuthAccessToken, oAuthAccessTokenSecret)
                    )
                {
                    //2 - login to flickr, if necessary
                    string oAuthToken = "";
                    string oAuthTokenSecret = "";
                    string oAuthUrl = "";
                    string returnUrl = ConfigurationManager.AppSettings["FlickrReturnUrl"];
                    returnUrl += "?oat=" + hidAuthToken.ClientID;
                    returnUrl += "&oats=" + hidAuthTokenSecret.ClientID;
                    returnUrl += qsParams;
                    FlickrTools.OAuthRequestToken(returnUrl, out oAuthToken, out oAuthTokenSecret, out oAuthUrl);

                    hidAuthToken.Value = oAuthToken;
                    hidAuthTokenSecret.Value = oAuthTokenSecret;

                    StringBuilder js = new StringBuilder("");
                    js.Append("window.open(\"" + oAuthUrl + "\")\n\r");
                    //js.Append("window.open(\"" + returnUrl + "\")\n\r");

                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFlickerLogin", js.ToString(), true);
                }
                else
                {
                    StringBuilder js = new StringBuilder("");
                    string oAuthUrl = ConfigurationManager.AppSettings["FlickrReturnUrl"] + "?oaat=" + oAuthAccessToken + "&oaats=" + oAuthAccessTokenSecret + qsParams;
                    js.Append("window.open(\"" + oAuthUrl + "\")\n\r");
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenFlickerLogin", js.ToString(), true);
                }
            }
		}

		protected void clearIndicatedPageButton_Click( object sender, EventArgs e )
		{
			int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
            List<int> pages = getSelectedPageIds();
			if ( pages.Count > 0 )
			{
				int[] arrPages = new int[ pages.Count ];
				pages.CopyTo( arrPages );
				bp.IndicatedPageDeleteAllForPage( arrPages, userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("IndicatedPages")).Text = "";
                    }
                }

				clearInputs();
			}
		}

		protected void assignPageTypeButton_Click( object sender, EventArgs e )
		{
			bool flag = false;
			List<int> pages = getSelectedPageIds();
			if ( pages.Count == 0 )
			{
				flag = true;
				errorControl.AddErrorText( "You must select at least one page to update." );
			}
			else
			{
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                int[] arrPages = new int[ pages.Count ];
				pages.CopyTo( arrPages );
				bp.Page_PageTypeSave( arrPages, int.Parse( pageTypeCombo.SelectedValue ), userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        String existingValue = ((Literal)gvr.FindControl("PageTypes")).Text;
                        if (!existingValue.Contains(pageTypeCombo.SelectedItem.Text))
                        {
                            if (existingValue == String.Empty)
                                ((Literal)gvr.FindControl("PageTypes")).Text = pageTypeCombo.SelectedItem.Text;
                            else
                                ((Literal)gvr.FindControl("PageTypes")).Text = pageTypeCombo.SelectedItem.Text + ", " + existingValue;
                        }
                    }
                }

				clearInputs();
			}

			errorControl.Visible = flag;
		}

        protected void replacePageTypeButton_Click(object sender, EventArgs e)
        {
            bool flag = false;
            List<int> pages = getSelectedPageIds();
            if (pages.Count == 0)
            {
                flag = true;
                errorControl.AddErrorText("You must select at least one page to update.");
            }
            else
            {
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                int[] arrPages = new int[pages.Count];
                pages.CopyTo(arrPages);
                bp.Page_PageTypeDeleteAllForPage(arrPages, userId);
                bp.Page_PageTypeSave(arrPages, int.Parse(pageTypeCombo.SelectedValue), userId);
                int itemId = int.Parse(itemDropDownList.SelectedValue);
                //fillPageList(itemId);

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("PageTypes")).Text = pageTypeCombo.SelectedItem.Text;
                    }
                }

                clearInputs();
            }

            errorControl.Visible = flag;
        }

		protected void clearPageTypeButton_Click( object sender, EventArgs e )
		{
            int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
            List<int> pages = getSelectedPageIds();
			if ( pages.Count > 0 )
			{
				int[] arrPages = new int[ pages.Count ];
				pages.CopyTo( arrPages );
				bp.Page_PageTypeDeleteAllForPage( arrPages, userId );
				int itemId = int.Parse( itemDropDownList.SelectedValue );
				//fillPageList( itemId );

                // By manually filling the cells in the datagrid (instead of just
                // refreshing/refilling the dataset, we are able to preserve the 
                // checked fields and the scroll position of the grid.
                foreach (GridViewRow gvr in detailGridView.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                    if (cb.Checked)
                    {
                        ((Literal)gvr.FindControl("PageTypes")).Text = "";
                    }
                }

				clearInputs();
			}
		}

		protected void lockButton_Click( object sender, EventArgs e )
		{
			// Validate the state of the button and the selected item before performing any updates
			int itemId = int.Parse( itemDropDownList.SelectedValue );
			Item item = bp.ItemSelectPagination( itemId );
			string paginationStatus = lockButton.Text;
            int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

			if ( item.PaginationStatusID.HasValue == false || 
                item.PaginationStatusID.Value == PaginationStatus.New ||
                item.PaginationStatusID.Value == PaginationStatus.Pending )
			{
				// if the status is pending, validate that the action will set it to "In Progress"
				if ( paginationStatus.Equals( _lockEditStatus ) == false )
				{
					displayPaginationStatusInvalid();
				}
				else
				{
					updatePaginationStatus( itemId, PaginationStatus.InProgress, userId );
				}
			}
			else if ( item.PaginationStatusID.Value == PaginationStatus.InProgress )
			{
				// If the status is "In Progress", validate that the action will set it to "Pending"
				// also make sure that the logged in user has rights to unlock this item
				if ( paginationStatus.Equals( _unlockStatus ) == false || item.PaginationStatusUserID != userId )
				{
					displayPaginationStatusInvalid();
				}
				else
				{
					updatePaginationStatus( itemId, PaginationStatus.Pending, userId );
				}
			}
			else if ( item.PaginationStatusID.Value == PaginationStatus.Complete )
			{
				// If the status is "Complete", validate that the action will set it to "Pending"
				if ( paginationStatus.Equals( _pendingStatus ) == false )
				{
					displayPaginationStatusInvalid();
				}
				else
				{
					updatePaginationStatus( itemId, PaginationStatus.Pending, userId );
				}
			}
		}

		protected void statusButton_Click( object sender, EventArgs e )
		{
			// Validate the state of the button and the selected item before performing any updates
			int itemId = int.Parse( itemDropDownList.SelectedValue );
			Item item = bp.ItemSelectPagination( itemId );
			string paginationStatus = statusButton.Text;
            int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

			if ( item.PaginationStatusID.HasValue == false || item.PaginationStatusID.Value == PaginationStatus.Pending )
			{
				// if the status is pending, validate that the action will set it to "Complete"
				if ( paginationStatus.Equals( _completeStatus ) == false )
				{
					displayPaginationStatusInvalid();
				}
				else
				{
					updatePaginationStatus( itemId, PaginationStatus.Complete, userId );
				}
			}
			else if ( item.PaginationStatusID.Value == PaginationStatus.InProgress )
			{
				// If the status is "In Progress", validate that the action will set it to "Complete"
				// also make sure that the logged in user has rights to unlock this item
				if ( paginationStatus.Equals( _completeStatus ) == false || item.PaginationStatusUserID != userId )
				{
					displayPaginationStatusInvalid();
				}
				else
				{
					updatePaginationStatus( itemId, PaginationStatus.Complete, userId );
				}
			}
			else if ( item.PaginationStatusID.Value == PaginationStatus.Complete )
			{
				// If the status is "Complete", validate that the action will set it to "In Progress"
				if ( paginationStatus.Equals( _lockEditStatus ) == false )
				{
					displayPaginationStatusInvalid();
				}
				else
				{
					updatePaginationStatus( itemId, PaginationStatus.InProgress, userId );
				}
			}
		}

        protected void replaceIndicatedPagesButton_Click(object sender, EventArgs e)
        {
            this.AssignOrReplaceIndicatedPages(true);
        }

		protected void saveIndicatedPagesButton_Click( object sender, EventArgs e )
		{
            this.AssignOrReplaceIndicatedPages(false);
        }

        private void AssignOrReplaceIndicatedPages(bool replace)
        {
            bool flag = false;
            List<int> pages = getSelectedPageIds();

            if (pages.Count == 0)
            {
                flag = true;
                errorControl.AddErrorText("You must select at least one page to update.");
            }
            else
            {
                IndicatedPageStyle style = (IndicatedPageStyle)int.Parse(styleDropDownList.SelectedValue);
                int i = 0;
                string start = startValueTextBox.Text.Trim();

                if (numStyleRadio.Checked)
                {
                    if (style == IndicatedPageStyle.Integer)
                    {
                        if (int.TryParse(incrementTextBox.Text, out i))
                        {
                            int s;
                            if (int.TryParse(startValueTextBox.Text, out s) == false)
                            {
                                flag = true;
                                errorControl.AddErrorText("The start value must be an integer.");
                            }
                        }
                        else
                        {
                            flag = true;
                            errorControl.AddErrorText("The increment value must be an integer.");
                        }
                    }
                    else if (style != IndicatedPageStyle.FreeForm)
                    {
                        if (int.TryParse(incrementTextBox.Text, out i))
                        {
                            try
                            {
                                RomanNumerals.FromRomanNumeral(startValueTextBox.Text.Trim());
                            }
                            catch
                            {
                                flag = true;
                                errorControl.AddErrorText("The start value must be a Roman numeral.");
                            }
                        }
                    }
                    else
                    {
                        flag = true;
                        errorControl.AddErrorText("The increment value must be an integer.");
                    }
                }
                else
                {
                    start = freeTextBox.Text.Trim();
                    style = IndicatedPageStyle.FreeForm;
                }

                if (!flag)
                {
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                    int[] arrPages = new int[pages.Count];
                    pages.CopyTo(arrPages);
                    if (replace) bp.IndicatedPageDeleteAllForPage(arrPages, userId);
                    bp.IndicatedPageSave(arrPages, prefixTextBox.Text.Trim(), style, start, i, impliedCheckBox.Checked,
                        userId);
                    int itemId = int.Parse(itemDropDownList.SelectedValue);
                    //fillPageList(itemId);

                    // By manually filling the cells in the datagrid (instead of just
                    // refreshing/refilling the dataset, we are able to preserve the 
                    // checked fields and the scroll position of the grid.
                    foreach (GridViewRow gvr in detailGridView.Rows)
                    {
                        CheckBox cb = (CheckBox)gvr.FindControl("pageCheckBox");
                        if (cb.Checked)
                        {
                            String existingValue = ((Literal)gvr.FindControl("IndicatedPages")).Text.Trim();
                            if (existingValue == "&nbsp;") existingValue = String.Empty;
                            ((Literal)gvr.FindControl("IndicatedPages")).Text = prefixTextBox.Text.Trim() + " " + (impliedCheckBox.Checked ? "[" + start + "]" : start);
                            if (!replace && (existingValue != String.Empty)) ((Literal)gvr.FindControl("IndicatedPages")).Text += ", " + existingValue;
                            start = this.GetNextPageNumber(style, start, i);
                        }
                    }

                    clearInputs();
                }
            }

            errorControl.Visible = flag;

            // Reset the indicated page fields to the initial values
            prefixTextBox.Text = "Page";
            freeTextStyleRadio.Checked = false;
            numStyleRadio.Checked = true;
            styleDropDownList.SelectedIndex = 0;
            startValueTextBox.Text = "1";
            incrementTextBox.Text = "1";
            impliedCheckBox.Checked = false;
            freeTextBox.Text = "";
        }

        private String GetNextPageNumber(IndicatedPageStyle style, String start, int increment)
        {
            int pageNumber;
            String nextPageNumber = start;

            switch (style)
            {
                case IndicatedPageStyle.Integer:
                    pageNumber = int.Parse(start);
                    pageNumber += increment;
                    nextPageNumber = pageNumber.ToString();
                    break;
                case IndicatedPageStyle.LowerRomanIV:
                    pageNumber = RomanNumerals.FromRomanNumeral(start);
                    pageNumber += increment;
                    nextPageNumber = RomanNumerals.ToRomanNumeral(pageNumber, true).ToLower();
                    break;
                case IndicatedPageStyle.UpperRomanIV:
                    pageNumber = RomanNumerals.FromRomanNumeral(start);
                    pageNumber += increment;
                    nextPageNumber = RomanNumerals.ToRomanNumeral(pageNumber, true);
                    break;
                case IndicatedPageStyle.LowerRomanIIII:
                    pageNumber = RomanNumerals.FromRomanNumeral(start);
                    pageNumber += increment;
                    nextPageNumber = RomanNumerals.ToRomanNumeral(pageNumber, false).ToLower();
                    break;
                case IndicatedPageStyle.UpperRomanIIII:
                    pageNumber = RomanNumerals.FromRomanNumeral(start);
                    pageNumber += increment;
                    nextPageNumber = RomanNumerals.ToRomanNumeral(pageNumber, false);
                    break;
            }

            return nextPageNumber;
        }

		protected void detailViewRadio_CheckedChanged( object sender, EventArgs e )
		{
			fillPageList( int.Parse( itemDropDownList.SelectedValue ) );
		}

		#endregion

	}
}