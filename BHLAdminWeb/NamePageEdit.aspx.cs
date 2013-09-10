using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
using MOBOT.Security.Client.MOBOTSecurity;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
	public partial class NamePageEdit : System.Web.UI.Page
	{
		private NamePageComparer.CompareEnum _sortColumn = NamePageComparer.CompareEnum.NameString;
		private SortOrder _sortOrder = SortOrder.Ascending;

		protected void Page_Load( object sender, EventArgs e )
		{
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearName", "<script language='javascript'>function clearName() { document.getElementById('" + selectedName.ClientID + "').value=''; overlayNameSearch(); __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectName", "<script language='javascript'>function selectName(nameId) { document.getElementById('" + selectedName.ClientID + "').value=nameId; overlayNameSearch(); __doPostBack('',''); }</script>");
            
            if (!IsPostBack)
			{
				string idString = Request.QueryString[ "id" ];
				int id = 0;
				if ( idString != null && int.TryParse( idString, out id ) )
				{
					pageIdTextBox.Text = id.ToString();
					search( id );
					fillUI();
				}
			}
			else
			{
                // Check for newly added authors
                String selectedNameId = this.selectedName.Value;
                if (selectedNameId != "")
                {
                    CustomGenericList<NamePage> namePages = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];

                    // Make sure the selected name isn't already associated with this title
                    bool nameExists = false;
                    foreach (NamePage existingName in namePages)
                    {
                        if (existingName.NameID.ToString() == selectedNameId)
                        {
                            nameExists = true;
                            break;
                        }
                    }

                    if (!nameExists)
                    {
                        NamePage newNamePage = new NamePage();

                        // Get details for "selectedNameId" from database
                        Name name = new BHLProvider().NameSelectByNameID(Convert.ToInt32(selectedNameId));
                        newNamePage.NameID = Convert.ToInt32(selectedNameId);
                        newNamePage.PageID = Convert.ToInt32(pageIdTextBox.Text);
                        newNamePage.NameSourceID = 1; // user reported
                        newNamePage.NameString = name.NameString;
                        newNamePage.ResolvedNameString = name.ResolvedNameString;
                        newNamePage.IsActive = name.IsActive;
                        newNamePage.IsFirstOccurrence = 0;
                        newNamePage.NameBankID = name.NamebankID;
                        newNamePage.EOLID = name.EOLID;
                        newNamePage.IsNew = true;
                        namePages.Add(newNamePage);
                    }

                    Session["NamePages" + pageIdTextBox.Text] = namePages;

                    this.selectedName.Value = "";
                    this.bindNamePageData(true);
                }

               
                if ( ViewState[ "SortColumn" ] != null )
				{
					_sortColumn = (NamePageComparer.CompareEnum)ViewState[ "SortColumn" ];
					_sortOrder = (SortOrder)ViewState[ "SortOrder" ];
				}
			}

            litMessage.Text = "";
            errorControl.Visible = false;
			Page.MaintainScrollPositionOnPostBack = true;
		}

		private void fillUI()
		{
            PageSummaryView ps = (PageSummaryView)Session["Page" + pageIdTextBox.Text];

			pageIdLabel.Text = ps.PageID.ToString();
			titleLink.NavigateUrl = "/TitleEdit.aspx?id=" + ps.TitleID.ToString();
			titleLink.Text = ps.ShortTitle + " (" + ps.TitleID.ToString() + ")";
            itemLink.NavigateUrl = "/ItemEdit.aspx?id=" + ps.ItemID.ToString();
            itemLink.Text = (ps.Volume == "") ? "(click to edit volume)" : ps.Volume;
			descriptionLabel.Text = ps.PageDescription;
			addNamePageButton.Enabled = true;
            btnFindName.Disabled = false;
			saveButton.Enabled = true;

            namePageList.DataSource = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];
			namePageList.DataBind();

            string viewerUrl = ConfigurationManager.AppSettings["BaseUrl"] + "/pageimage/" + ps.PageID.ToString();
            imgPage.Attributes.Add("src", viewerUrl);
        }

		private void search( int id )
		{
			BHLProvider bp = new BHLProvider();

			PageSummaryView ps = bp.PageSummarySelectByPageId( id, true );
            if (ps == null) ps = new PageSummaryView();
            Session["Page" + pageIdTextBox.Text] = ps;

			CustomGenericList<NamePage> namePageList = bp.NamePageSelectByPageID( ps.PageID );
            Session["NamePages" + pageIdTextBox.Text] = namePageList;

			fillUI();
		}

		#region NamePageList methods

		private void bindNamePageData( bool sort )
		{
            CustomGenericList<NamePage> namePages = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];

			// filter out deleted items
            CustomGenericList<NamePage> pns = new CustomGenericList<NamePage>();
            foreach (NamePage pn in namePages)
			{
				if ( pn.IsDeleted == false )
				{
					pns.Add( pn );
				}
			}
			if ( sort )
			{
				NamePageComparer comp = new NamePageComparer( (NamePageComparer.CompareEnum)_sortColumn, _sortOrder );
				pns.Sort( comp );
			}
			namePageList.DataSource = pns;
			namePageList.DataBind();
		}

        private NamePage findNamePage(CustomGenericList<NamePage> namePages, int namePageId, string nameString,
			string resolvedNameString, string nameBankId )
		{
            foreach (NamePage pn in namePages)
			{
                if (pn.IsDeleted)
                {
                    continue;
                }
                if (namePageId == 0 && nameBankId == pn.NameBankID && nameString.ToLower().Equals(pn.NameString.ToLower()) &&
                    BHL.DataObjects.Utility.EmptyIfNull(resolvedNameString).ToLower().Equals(MOBOT.BHL.DataObjects.Utility.EmptyIfNull(pn.ResolvedNameString).ToLower()) )
				{
					return pn;
				}
				else if ( namePageId > 0 && namePageId == pn.NamePageID )
				{
					return pn;
				}
			}

			return null;
		}

		/*protected bool SetNameFoundRO( object dataItem )
		{
			int nameId = (int)DataBinder.Eval( dataItem, "NameID" );
			return ( nameId > 0 );
		}*/

		#endregion

		private NameFinderResponse ubioLookup( string name )
		{
			XmlTextReader reader = null;
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
					"http://www.ubio.org/webservices/service.php?function=taxonFinder&includeLinks=0&freeText=" +
					Server.UrlEncode( name ) );
				request.Timeout = 15000;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				reader = new XmlTextReader( (Stream)response.GetResponseStream() );
				XmlDocument doc = new XmlDocument();
				doc.Load( reader );
				NameFinderResponse result = null;
				// Taking this simple, straight forward approach because we're only expecting one result per check. Will
				// make parsing algorithm more robust later if needed.
				XmlNodeList nameList = doc.GetElementsByTagName( "nameString" );
				XmlNodeList idList = doc.GetElementsByTagName( "namebankID" );
				if ( nameList.Count == 1 )
				{
					result = new NameFinderResponse();
					result.Name = nameList[ 0 ].InnerText;
                    string idValue = string.Empty;
                    if (idList.Count == 1) idValue = idList[0].InnerText;
                    if (idValue != string.Empty) result.Identifiers.Add("NameBank|" + idValue);
				}
				return result;
			}
			catch
			{
				return null;
			}
			finally
			{
				if ( reader != null )
				{
					reader.Close();
				}
			}
		}

		private bool validate( CustomGenericList<NamePage> namePages )
		{
			bool flag = false;

			// Check that all edits were completed
			if ( namePageList.EditIndex != -1 )
			{
				flag = true;
				errorControl.AddErrorText( "Names list has an edit pending" );
			}

			foreach ( NamePage namePage in namePages )
			{
				if ( namePage.NameString.Trim().Length == 0 && namePage.IsDeleted == false )
				{
					flag = true;
					errorControl.AddErrorText("One or more of the names have an empty Name String. Please fill in missing data" );
					break;
				}

                if (!string.IsNullOrEmpty(namePage.NameBankID) && string.IsNullOrEmpty(namePage.ResolvedNameString))
                {
                    flag = true;
                    errorControl.AddErrorText("If a Namebank ID is specified, then a Resolved Name must also be specified.");
                    break;
                }

                if (!string.IsNullOrEmpty(namePage.ResolvedNameString) && string.IsNullOrEmpty(namePage.NameBankID) &&
                    string.IsNullOrEmpty(namePage.EOLID))
                {
                    flag = true;
                    errorControl.AddErrorText("If a Resolved Name is specified, then a NameBank ID or EOL ID must also be specified.");
                    break;
                }
			}

			errorControl.Visible = flag;
			Page.MaintainScrollPositionOnPostBack = !flag;

			return !flag;
		}

        private SecUser getSecUser()
        {
            HttpCookie tokenCookie = Request.Cookies["MOBOTSecurityToken"];
            return Helper.GetSecProvider().SecUserSelect(tokenCookie.Value);
        }

		#region Event handlers

		protected void namePageList_RowEditing( object sender, GridViewEditEventArgs e )
		{
			namePageList.EditIndex = e.NewEditIndex;
			bindNamePageData( true );
		}

		protected void namePageList_RowUpdating( object sender, GridViewUpdateEventArgs e )
		{
			GridViewRow row = namePageList.Rows[ e.RowIndex ];

			if ( row != null )
			{
                CheckBox firstCheckBox = row.FindControl( "firstCheckBox" ) as CheckBox;
				TextBox nameStringTextBox = row.FindControl( "nameStringTextBox" ) as TextBox;
                TextBox resolvedNameStringTextBox = row.FindControl("resolvedNameStringTextBox") as TextBox;
                TextBox namebankIDTextBox = row.FindControl("namebankIDTextBox") as TextBox;
                TextBox eolIDTextBox = row.FindControl("eolIDTextBox") as TextBox;

				if ( firstCheckBox != null && nameStringTextBox != null && resolvedNameStringTextBox != null && 
                    namebankIDTextBox != null && eolIDTextBox != null)
				{
                    CustomGenericList<NamePage> namePages = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];
                    bool first = firstCheckBox.Checked;
					string nameString = nameStringTextBox.Text.Trim();
                    string resolvedNameString = resolvedNameStringTextBox.Text.Trim();
                    string namebankId = namebankIDTextBox.Text.Trim();
                    string eolId = eolIDTextBox.Text.Trim();

                    NamePage namePage = findNamePage(namePages,
						(int)namePageList.DataKeys[ e.RowIndex ].Values[ 0 ],
						(string)namePageList.DataKeys[ e.RowIndex ].Values[ 1 ],
						( namePageList.DataKeys[ e.RowIndex ].Values[ 2 ] == null ?
							null : (string)namePageList.DataKeys[ e.RowIndex ].Values[ 2 ] ),
						( namePageList.DataKeys[ e.RowIndex ].Values[ 3 ] == null ?
							null : (string)namePageList.DataKeys[ e.RowIndex ].Values[ 3 ] ) );

                    namePage.IsFirstOccurrence = (short) (first ? 1 : 0);
					namePage.NameString = nameString;
                    namePage.ResolvedNameString = resolvedNameString;
                    namePage.NameBankID = namebankId;
                    namePage.EOLID = eolId;
				}
			}

			namePageList.EditIndex = -1;
			bindNamePageData( true );
		}

		protected void namePageList_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
		{
			namePageList.EditIndex = -1;
			bindNamePageData( true );
		}

		protected void addNamePageButton_Click( object sender, EventArgs e )
		{
            PageSummaryView ps = (PageSummaryView)Session["Page" + pageIdTextBox.Text];
            NamePage pn = new NamePage();
			pn.PageID = ps.PageID;
            pn.IsActive = 1;
            pn.NameSourceID = 1; // User reported
            CustomGenericList<NamePage> namePages = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];
			namePages.Add( pn );

            // Determine edit index by counting names that are not deleted
            int editIndex = -1;
            foreach (NamePage namePage in namePages)
            {
                if (!namePage.IsDeleted) editIndex++;
            }
            namePageList.EditIndex = editIndex;

			bindNamePageData( false );
		}

		protected void namePageList_RowCommand( object sender, GridViewCommandEventArgs e )
		{
			if ( e.CommandName.Equals( "RemoveButton" ) )
			{
				int rowNum = int.Parse( e.CommandArgument.ToString() );
                CustomGenericList<NamePage> namePages = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];

                NamePage namePage = findNamePage(namePages,
					(int)namePageList.DataKeys[ rowNum ].Values[ 0 ],
					(string)namePageList.DataKeys[ rowNum ].Values[ 1 ],
					( namePageList.DataKeys[ rowNum ].Values[ 2 ] == null ?
						null : (string)namePageList.DataKeys[ rowNum ].Values[ 2 ] ),
					( namePageList.DataKeys[ rowNum ].Values[ 3 ] == null ?
						null : (string)namePageList.DataKeys[ rowNum ].Values[ 3 ] ) );

				namePage.IsDeleted = true;
				bindNamePageData( true );
			}
		}

		protected void namePageList_Sorting( object sender, GridViewSortEventArgs e )
		{
			NamePageComparer.CompareEnum sortColumn = _sortColumn;

			if ( e.SortExpression.Equals( "NameString" ) )
			{
				_sortColumn = NamePageComparer.CompareEnum.NameString;
			}
			else if ( e.SortExpression.Equals( "ResolvedNameString" ) )
			{
				_sortColumn = NamePageComparer.CompareEnum.ResolvedNameString;
			}
			else if ( e.SortExpression.Equals( "NameBankID" ) )
			{
				_sortColumn = NamePageComparer.CompareEnum.NameBankID;
			}

			if ( sortColumn == _sortColumn )
			{
				if ( _sortOrder == SortOrder.Descending )
				{
					_sortOrder = SortOrder.Ascending;
				}
				else
				{
					_sortOrder = SortOrder.Descending;
				}
			}
			else
			{
				_sortOrder = SortOrder.Ascending;
			}

			ViewState[ "SortColumn" ] = _sortColumn;
			ViewState[ "SortOrder" ] = _sortOrder;

			bindNamePageData( true );
		}

		protected void namePageList_RowDataBound( object sender, GridViewRowEventArgs e )
		{
			if ( e.Row.RowType == DataControlRowType.Header )
			{
				Image img = new Image();
				img.Attributes[ "style" ] = "padding-bottom:2px";
				if ( _sortOrder == SortOrder.Ascending )
				{
					img.ImageUrl = "/images/up.gif";
				}
				else
				{
					img.ImageUrl = "/images/down.gif";
				}

				int sortColumnIndex = 1;
				switch ( _sortColumn )
				{
					case NamePageComparer.CompareEnum.NameString:
						{
							sortColumnIndex = 1;
							break;
						}
					case NamePageComparer.CompareEnum.ResolvedNameString:
						{
							sortColumnIndex = 2;
							break;
						}
					case NamePageComparer.CompareEnum.NameBankID:
						{
							sortColumnIndex = 3;
							break;
						}
				}

				e.Row.Cells[ sortColumnIndex ].Controls.Add( new LiteralControl( " " ) );
				e.Row.Cells[ sortColumnIndex ].Controls.Add( img );
				e.Row.Cells[ sortColumnIndex ].Wrap = false;
			}
		}

		protected void searchButton_Click( object sender, EventArgs e )
		{
			int pageId = 0;
			if ( int.TryParse( pageIdTextBox.Text.Trim(), out pageId ) )
			{
				search( pageId );
			}
		}

		protected void saveButton_Click( object sender, EventArgs e )
		{
            PageSummaryView ps = (PageSummaryView)Session["Page" + pageIdTextBox.Text];
            CustomGenericList<NamePage> namePages = (CustomGenericList<NamePage>)Session["NamePages" + pageIdTextBox.Text];

			if ( validate( namePages ) )
			{
				BHLProvider bp = new BHLProvider();

                foreach (NamePage namePage in namePages)
				{
                    // If this is a newly added row
					if (namePage.IsNew)
					{
                        // No resolved name or namebankid specified, so see if we can get them from uBio
                        if (namePage.ResolvedNameString == string.Empty)
                        {
                            NameFinderResponse uBioResult = ubioLookup(namePage.NameString);
                            if (uBioResult != null && uBioResult.Identifiers.Count > 0)
                            {
                                namePage.ResolvedNameString = uBioResult.Name;
                                string[] id = uBioResult.Identifiers[0].Split('|');
                                namePage.NameBankID = BHL.DataObjects.Utility.EmptyIfNull(id[1]);
                            }
                        }
					}
				}

                // Save the names
                SecUser secUser = this.getSecUser();
                int? userId = secUser.UserID;
				bp.NamePageSave( namePages, (int)userId);

                // After a successful save operation, reload the data
                Session["NamePages" + pageIdTextBox.Text] = bp.NamePageSelectByPageID(ps.PageID);
                fillUI();
			}
			else
			{
				return;
			}

            litMessage.Text = "<span class='liveData'>Item Saved.</span>";
            Page.MaintainScrollPositionOnPostBack = false;
		}

		#endregion

	}
}
