using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.Security.Client.MOBOTSecurity;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
	public partial class TitleEdit : System.Web.UI.Page
	{
		private TitleItemComparer.CompareEnum _sortColumn = TitleItemComparer.CompareEnum.ItemSequence;
		private SortOrder _sortOrder = SortOrder.Ascending;

		protected void Page_Load( object sender, EventArgs e )
		{
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectItem", "<script language='javascript'>function selectItem(itemId) { document.getElementById('" + selectedItem.ClientID + "').value+=itemId+'|';}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearItem", "<script language='javascript'>function clearItems() { document.getElementById('" + selectedItem.ClientID + "').value=''; overlay(); __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearAuthor", "<script language='javascript'>function clearAuthor() { document.getElementById('" + selectedItem.ClientID + "').value=''; overlayAuthorSearch(); __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptUpdateAssoc", "<script language='javascript'>function updateAssociations() { document.getElementById('" + associationsUpdated.ClientID + "').value='true'; __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectAuthor", "<script language='javascript'>function selectAuthor(authorId) { document.getElementById('" + selectedAuthor.ClientID + "').value=authorId; overlayAuthorSearch(); __doPostBack('',''); }</script>");

            if (!IsPostBack)
			{
				string idString = Request.QueryString[ "id" ];
				int id = 0;
				if ( idString != null && int.TryParse( idString, out id ) )
				{
					fillCombos();
					fillUI( id );
				}
				else
				{
					// TODO: Inform user that title does not exist -- Perhaps redirect to unknown.aspx?type=title
				}
				showPubRadioButton.Checked = true;
			}
			else
			{
                String selectedAuthorId = this.selectedAuthor.Value;
                if (selectedAuthorId != "")
                {
                    Title title = (Title)Session["Title" + idLabel.Text];

                    // Make sure the selected author isn't already associated with this title
                    bool authorExists = false;
                    foreach (TitleAuthor existingAuthor in title.TitleAuthors)
                    {
                        if (existingAuthor.AuthorID.ToString() == selectedAuthorId)
                        {
                            authorExists = true;
                            break;
                        }
                    }

                    if (!authorExists)
                    {
                        TitleAuthor newTitleAuthor = new TitleAuthor();

                        // Get details for "selectedAuthorId" from database
                        BHLProvider provider = new BHLProvider();
                        Author author = provider.AuthorSelectWithNameByAuthorId(Convert.ToInt32(selectedAuthorId));
                        AuthorRole authorRole = provider.AuthorRoleSelectAuto(1);
                        newTitleAuthor.AuthorID = Convert.ToInt32(selectedAuthorId);
                        newTitleAuthor.TitleID = title.TitleID;
                        newTitleAuthor.FullName = author.FullName;
                        newTitleAuthor.AuthorRoleID = 1;
                        newTitleAuthor.RoleDescription = authorRole.RoleDescription;
                        newTitleAuthor.Author = author;
                        newTitleAuthor.IsNew = true;
                        title.TitleAuthors.Add(newTitleAuthor);
                    }

                    Session["Title" + title.TitleID.ToString()] = title;
                    this.selectedAuthor.Value = "";
                    this.bindTitleAuthorData();
                }

                String selectedItemIds = this.selectedItem.Value;
                if (selectedItemIds != "")
                {
                    Title title = (Title)Session["Title" + idLabel.Text];

                    if (selectedItemIds.EndsWith("|")) selectedItemIds = selectedItemIds.Substring(0, selectedItemIds.Length - 1);
                    String[] selectedItemIdList = selectedItemIds.Split('|');
                    foreach (String selectedItemId in selectedItemIdList)
                    {
                        // Make sure the selected item isn't already associated with this title
                        bool itemExists = false;
                        foreach (TitleItem existingTitleItem in title.TitleItems)
                        {
                            if (existingTitleItem.ItemID.ToString() == selectedItemId)
                            {
                                itemExists = true;
                                break;
                            }
                        }

                        if (!itemExists)
                        {
                            TitleItem newTitleItem = new TitleItem();

                            // Get the current maximum itemsequence value
                            short? itemSequence = 0;
                            foreach (TitleItem titleItem in title.TitleItems)
                            {
                                if (titleItem.ItemSequence > itemSequence) itemSequence = titleItem.ItemSequence;
                            }

                            // Get details for "selectedItemId" from database
                            BHLProvider provider = new BHLProvider();
                            Item item = provider.ItemSelectAuto(Convert.ToInt32(selectedItemId));
                            newTitleItem.ItemID = Convert.ToInt32(selectedItemId);
                            newTitleItem.TitleID = title.TitleID;
                            newTitleItem.ItemSequence = ++itemSequence;
                            newTitleItem.ItemStatusID = item.ItemStatusID;
                            newTitleItem.BarCode = item.BarCode;
                            newTitleItem.Volume = item.Volume;
                            newTitleItem.PrimaryTitleID = (makePrimary.Checked) ? title.TitleID : item.PrimaryTitleID;
                            newTitleItem.IsNew = true;
                            title.TitleItems.Add(newTitleItem);
                        }
                    }

                    Session["Title" + title.TitleID.ToString()] = title;
                    this.selectedItem.Value = "";
                    this.bindItemData();
                }

                if (this.associationsUpdated.Value == "true")
                {
                    this.associationsUpdated.Value = String.Empty;
                    this.bindTitleAssociationData();
                }

                if (ViewState["SortColumn"] != null)
				{
					_sortColumn = (TitleItemComparer.CompareEnum)ViewState[ "SortColumn" ];
					_sortOrder = (SortOrder)ViewState[ "SortOrder" ];
				}
			}

            litMessage.Text = "";
			errorControl.Visible = false;
			Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill methods

        private void fillCombos()
		{
			BHLProvider bp = new BHLProvider();

            CustomGenericList<BibliographicLevel> bibliographicLevels = bp.BibliographicLevelSelectAll();

            BibliographicLevel emptyBibLevel = new BibliographicLevel();
            emptyBibLevel.BibliographicLevelID = 0;
            emptyBibLevel.BibliographicLevelName = string.Empty;
            bibliographicLevels.Insert(0, emptyBibLevel);

            ddlBibliographicLevel.DataSource = bibliographicLevels;
            ddlBibliographicLevel.DataTextField = "BibliographicLevelName";
            ddlBibliographicLevel.DataValueField = "BibliographicLevelID";
            ddlBibliographicLevel.DataBind();

            CustomGenericList<Institution> institutions = bp.InstituationSelectAll();

            /*
            Institution emptyInstitution = new Institution();
            emptyInstitution.InstitutionCode = string.Empty;
            emptyInstitution.InstitutionName = string.Empty;
            institutions.Insert(0, emptyInstitution);
             */

            ddlInst.DataSource = institutions;
            ddlInst.DataTextField = "InstitutionName";
            ddlInst.DataValueField = "InstitutionCode";
            ddlInst.DataBind();
            ddlInst.SelectedValue = "UNKNOWN";

            CustomGenericList<Language> languages = bp.LanguageSelectAll();

			Language emptyLanguage = new Language();
            emptyLanguage.LanguageCode = string.Empty;
            emptyLanguage.LanguageName = string.Empty;
			languages.Insert( 0, emptyLanguage );

			ddlLang.DataSource = languages;
			ddlLang.DataTextField = "LanguageName";
			ddlLang.DataValueField = "LanguageCode";
			ddlLang.DataBind();
		}

		private void fillUI( int id )
		{
			BHLProvider bp = new BHLProvider();

			idLabel.Text = id.ToString();
            btnTitleAssociationAdd.Attributes["onclick"] = String.Format(btnTitleAssociationAdd.Attributes["onclick"], id.ToString());

			// Look up title
			Title title = bp.TitleSelectExtended( id );

			Session[ "Title" + title.TitleID.ToString()] = title;

            replacedByTextBox.Text = title.RedirectTitleID.ToString();
            String displayTitle = ((title.ShortTitle.Length > 30) ? title.ShortTitle.Substring(0, 30) + "..." : title.ShortTitle);
            makePrimary.Text = "Make Title " + title.TitleID.ToString() + " (\"" + displayTitle + "\") the Primary title for the items.";
            ddlBibliographicLevel.SelectedValue = (title.BibliographicLevelID ?? 0).ToString();
            publishReadyCheckBox.Checked = title.PublishReady;
			marcBibIdLabel.Text = title.MARCBibID;
			marcLeaderLabel.Text = title.MARCLeader;
			fullTitleTextBox.Text = title.FullTitle;
			shortTitleTextBox.Text = title.ShortTitle;
			sortTitleTextBox.Text = title.SortTitle;
			uniformTitleTextBox.Text = title.UniformTitle;
            partNumberTextBox.Text = title.PartNumber;
            partNameTextBox.Text = title.PartName;
			callNumberTextBox.Text = title.CallNumber;
            if (title.InstitutionCode != null && title.InstitutionCode.Length > 0)
            {
                ddlInst.SelectedValue = title.InstitutionCode;
            }
            else
            {
                ddlInst.SelectedValue = "UNKNOWN";
            }

			if ( title.LanguageCode != null && title.LanguageCode.Length > 0 )
			{
				ddlLang.SelectedValue = title.LanguageCode;
			}
			else
			{
				ddlLang.SelectedIndex = 0;
			}

			descTextBox.Text = title.TitleDescription;
            publicationPlaceTextBox.Text = title.Datafield_260_a;
            publisherNameTextBox.Text = title.Datafield_260_b;
            publicationDateTextBox.Text = title.Datafield_260_c;
			startYearTextBox.Text = ( title.StartYear.HasValue ? title.StartYear.Value.ToString() : "" );
			endYearTextBox.Text = ( title.EndYear.HasValue ? title.EndYear.Value.ToString() : "" );
            OrigCatalogSourceTextBox.Text = title.OriginalCatalogingSource;
            EditionStatementTextBox.Text = title.EditionStatement;
            PubFrequencyTextBox.Text = title.CurrentPublicationFrequency;
			notesTextBox.Text = title.Note;

			creatorsList.DataSource = title.TitleAuthors;
			creatorsList.DataBind();

            subjectsList.DataSource = title.TitleKeywords;
            subjectsList.DataBind();

            identifiersList.DataSource = title.TitleIdentifiers;
            identifiersList.DataBind();

            associationsList.DataSource = title.TitleAssociations;
            associationsList.DataBind();

            variantsList.DataSource = title.TitleVariants;
            variantsList.DataBind();

			collectionsList.DataSource = title.TitleCollections;
			collectionsList.DataBind();

            languagesList.DataSource = title.TitleLanguages;
            languagesList.DataBind();

			bindItemData();
			//itemsList.DataSource = title.Items;
			//itemsList.DataBind();

            // See if we can display a link to the MARC file
            bool marcFound = false;
            String filepath = string.Empty;

            // Check vaults for imported MARC file
            CustomGenericList<Vault> vaults = new BHLProvider().VaultSelectAll();
            foreach(Vault vault in vaults)
            {
                filepath = String.Format(ConfigurationManager.AppSettings["MARCXmlLocation"], vault.OCRFolderShare, title.MARCBibID, title.MARCBibID);
                if (new BHLProvider().GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true").FileExists(filepath))
                {
                    marcFound = true;
                    break;
                }
            }

            if (marcFound)
            {
                hypMarc.Attributes["onclick"] = String.Format(hypMarc.Attributes["onclick"], title.TitleID.ToString());
                hypMarc.Visible = true;
            }
        }

        #endregion Fill methods

        #region TitleAuthor methods

        private void bindTitleAuthorData()
		{
			Title title = (Title)Session[ "Title" + idLabel.Text ];

			// filter out deleted items
			CustomGenericList<TitleAuthor> titleAuthors = new CustomGenericList<TitleAuthor>();
			foreach ( TitleAuthor tc in title.TitleAuthors )
			{
				if ( tc.IsDeleted == false )
				{
					titleAuthors.Add( tc );
				}
			}

			creatorsList.DataSource = titleAuthors;
			creatorsList.DataBind();
		}

		CustomGenericList<AuthorRole> _authorRoles = null;
		protected CustomGenericList<AuthorRole> GetAuthorRoles()
		{
			BHLProvider bp = new BHLProvider();
			_authorRoles = bp.AuthorRoleSelectAll();

			return _authorRoles;
		}

		protected int GetAuthorRoleIndex( object dataItem )
		{
			string authorRoleIdString = DataBinder.Eval( dataItem, "AuthorRoleID" ).ToString();

			if ( !authorRoleIdString.Equals( "0" ) )
			{
				int aurthorRoleId = int.Parse( authorRoleIdString );
				int ix = 0;
				foreach ( AuthorRole authorRole in _authorRoles )
				{
					if ( authorRole.AuthorRoleID == aurthorRoleId )
					{
						return ix;
					}
					ix++;
				}
			}

			return 0;
		}

		private TitleAuthor findTitleAuthor( CustomGenericList<TitleAuthor> titleAuthors, int titleAuthorId,
			int authorId, int authorRoleId )
		{
			foreach ( TitleAuthor tc in titleAuthors )
			{
				if ( tc.IsDeleted )
				{
					continue;
				}
				if ( titleAuthorId == 0 && tc.TitleAuthorID == 0 && authorRoleId == tc.AuthorRoleID &&
					authorId == tc.AuthorID )
				{
					return tc;
				}
				else if ( titleAuthorId > 0 && tc.TitleAuthorID == titleAuthorId )
				{
					return tc;
				}
			}

			return null;
		}

		#endregion

        #region TitleKeyword methods

        private void bindSubjectData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            CustomGenericList<TitleKeyword> titleKeywords = new CustomGenericList<TitleKeyword>();
            foreach (TitleKeyword tk in title.TitleKeywords)
            {
                if (tk.IsDeleted == false)
                {
                    titleKeywords.Add(tk);
                }
            }

            subjectsList.DataSource = titleKeywords;
            subjectsList.DataBind();
        }

        private TitleKeyword findTitleKeyword(CustomGenericList<TitleKeyword> titleKeywords, 
            int titleKeywordId, int keywordID, string keyword)
        {
            foreach (TitleKeyword tk in titleKeywords)
            {
                if (tk.IsDeleted)
                {
                    continue;
                }
                if (titleKeywordId == tk.TitleKeywordID && 
                    keywordID == tk.KeywordID &&
                    keyword == tk.Keyword)
                {
                    return tk;
                }
            }

            return null;
        }

        #endregion TitleKeyword methods

        #region TitleVariant methods

        private void bindTitleVariantData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            CustomGenericList<TitleVariant> titleVariants = new CustomGenericList<TitleVariant>();
            foreach (TitleVariant tv in title.TitleVariants)
            {
                if (!tv.IsDeleted) titleVariants.Add(tv);
            }

            variantsList.DataSource = titleVariants;
            variantsList.DataBind();
        }

        CustomGenericList<TitleVariantType> _titleVariantTypes = null;
        protected CustomGenericList<TitleVariantType> GetVariants()
        {
            BHLProvider bp = new BHLProvider();
            _titleVariantTypes = bp.TitleVariantTypeSelectAll();
            return _titleVariantTypes;
        }

        protected int GetVariantIndex(object dataItem)
        {
            string titleVariantTypeIdString = DataBinder.Eval(dataItem, "TitleVariantTypeID").ToString();

            if (!titleVariantTypeIdString.Equals("0"))
            {
                int titleVariantTypeId = int.Parse(titleVariantTypeIdString);
                int ix = 0;
                foreach (TitleVariantType titleVariantType in _titleVariantTypes)
                {
                    if (titleVariantType.TitleVariantTypeID == titleVariantTypeId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        private TitleVariant findTitleVariant(CustomGenericList<TitleVariant> titleVariants,
            int titleVariantId)
        {
            foreach (TitleVariant tv in titleVariants)
            {
                if (tv.IsDeleted)
                {
                    continue;
                }
                if (titleVariantId == 0 && tv.TitleVariantID == 0)
                {
                    return tv;
                }
                else if (titleVariantId > 0 && tv.TitleVariantID == titleVariantId)
                {
                    return tv;
                }
            }

            return null;
        }

        #endregion TitleVariant methods

        #region TitleIdentifier methods

        private void bindTitleIdentifierData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            CustomGenericList<Title_Identifier> titleIdentifiers = new CustomGenericList<Title_Identifier>();
            foreach (Title_Identifier ti in title.TitleIdentifiers)
            {
                if (ti.IsDeleted == false)
                {
                    titleIdentifiers.Add(ti);
                }
            }

            identifiersList.DataSource = titleIdentifiers;
            identifiersList.DataBind();
        }

        CustomGenericList<Identifier> _identifiers = null;
        protected CustomGenericList<Identifier> GetIdentifiers()
        {
            BHLProvider bp = new BHLProvider();
            _identifiers = bp.IdentifierSelectAll();

            return _identifiers;
        }

        protected int GetIdentifierIndex(object dataItem)
        {
            string identifierIdString = DataBinder.Eval(dataItem, "IdentifierID").ToString();

            if (!identifierIdString.Equals("0"))
            {
                int identifierId = int.Parse(identifierIdString);
                int ix = 0;
                foreach (Identifier identifier in _identifiers)
                {
                    if (identifier.IdentifierID == identifierId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        private Title_Identifier findTitle_Identifier(CustomGenericList<Title_Identifier> titleIdentifiers, 
            int titleIdentifierId, int identifierID, string identifierValue)
        {
            foreach (Title_Identifier tti in titleIdentifiers)
            {
                if (tti.IsDeleted)
                {
                    continue;
                }
                if (titleIdentifierId == 0 && tti.TitleIdentifierID == 0 && 
                    identifierID == 0 && tti.TitleIdentifierID == 0 &&
                    identifierValue == "" && tti.IdentifierValue == "")
                {
                    return tti;
                }
                else if (titleIdentifierId > 0 && tti.TitleIdentifierID == titleIdentifierId)
                {
                    return tti;
                }
            }

            return null;
        }

        #endregion

        #region TitleAssociation methods

        private void bindTitleAssociationData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            CustomGenericList<TitleAssociation> titleAssociations = new CustomGenericList<TitleAssociation>();
            foreach (TitleAssociation ta in title.TitleAssociations)
            {
                if (ta.IsDeleted == false)
                {
                    titleAssociations.Add(ta);
                }
            }

            associationsList.DataSource = titleAssociations;
            associationsList.DataBind();
        }

        private TitleAssociation findTitleAssociation(CustomGenericList<TitleAssociation> titleAssociations,
            int titleAssociationId)
        {
            foreach (TitleAssociation ta in titleAssociations)
            {
                if (ta.IsDeleted)
                {
                    continue;
                }
                if (titleAssociationId == 0 && ta.TitleAssociationID == 0)
                {
                    return ta;
                }
                else if (titleAssociationId > 0 && ta.TitleAssociationID == titleAssociationId)
                {
                    return ta;
                }
            }

            return null;
        }

        #endregion TitleAssociation methods

        #region TitleLanguage methods

        private void bindLanguageData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            CustomGenericList<TitleLanguage> titleLanguages = new CustomGenericList<TitleLanguage>();
            foreach (TitleLanguage tl in title.TitleLanguages)
            {
                if (tl.IsDeleted == false)
                {
                    titleLanguages.Add(tl);
                }
            }

            languagesList.DataSource = titleLanguages;
            languagesList.DataBind();
        }

        CustomGenericList<Language> _titleLanguages = null;
        protected CustomGenericList<Language> GetLanguages()
        {
            BHLProvider bp = new BHLProvider();
            _titleLanguages = bp.LanguageSelectAll();

            return _titleLanguages;
        }

        protected int GetLanguageIndex(object dataItem)
        {
            String languageCode = DataBinder.Eval(dataItem, "LanguageCode").ToString();

            int ix = 0;
            foreach (Language language in _titleLanguages)
            {
                if (language.LanguageCode == languageCode)
                {
                    return ix;
                }
                ix++;
            }

            return 0;
        }

        private TitleLanguage findTitleLanguage(CustomGenericList<TitleLanguage> titleLanguages, 
            int titleLanguageId, string languageCode)
        {
            foreach (TitleLanguage tl in titleLanguages)
            {
                if (tl.IsDeleted)
                {
                    continue;
                }
                if (titleLanguageId == 0 && tl.TitleLanguageID == 0 && languageCode == tl.LanguageCode)
                {
                    return tl;
                }
                else if (titleLanguageId > 0 && tl.TitleLanguageID == titleLanguageId)
                {
                    return tl;
                }
            }

            return null;
        }

        #endregion TitleLanguage methods

        #region Collection methods

        private void bindCollectionData()
		{
            Title title = (Title)Session["Title" + idLabel.Text];

			// filter out deleted items
            CustomGenericList<TitleCollection> titleCollections = new CustomGenericList<TitleCollection>();
            foreach (TitleCollection tc in title.TitleCollections)
			{
				if ( tc.IsDeleted == false )
				{
					titleCollections.Add( tc );
				}
			}

			collectionsList.DataSource = titleCollections;
			collectionsList.DataBind();
		}

		CustomGenericList<Collection> _titleCollections = null;
		protected CustomGenericList<Collection> GetCollections()
		{
			BHLProvider bp = new BHLProvider();
            // Select collections that may contain titles
			_titleCollections = bp.CollectionSelectByContents(1, 0);

			return _titleCollections;
		}

		protected int GetCollectionIndex( object dataItem )
		{
			string collectionIdString = DataBinder.Eval( dataItem, "CollectionID" ).ToString();

			if ( !collectionIdString.Equals( "0" ) )
			{
				int collectionId = int.Parse( collectionIdString );
				int ix = 0;
				foreach ( Collection titleCollection in _titleCollections )
				{
					if ( titleCollection.CollectionID == collectionId )
					{
						return ix;
					}
					ix++;
				}
			}

			return 0;
		}

        private TitleCollection findCollection(CustomGenericList<TitleCollection> collections, int titleCollectionId,
			int collectionId )
		{
            foreach (TitleCollection tc in collections)
			{
				if ( tc.IsDeleted )
				{
					continue;
				}
				if ( titleCollectionId == 0 && tc.TitleCollectionID == 0 && collectionId == tc.CollectionID )
				{
					return tc;
				}
				else if ( titleCollectionId > 0 && tc.TitleCollectionID == titleCollectionId )
				{
					return tc;
				}
			}

			return null;
		}

		#endregion Collection methods

        #region Item methods

        private void bindItemData()
		{
            Title title = (Title)Session["Title" + idLabel.Text];

			CustomGenericList<TitleItem> items = new CustomGenericList<TitleItem>();
			if ( showPubRadioButton.Checked || ( showAllRadioButton.Checked == false && showPubRadioButton.Checked == false ) )
			{
				foreach ( TitleItem item in title.TitleItems )
				{
					if (( item.ItemStatusID == 40 ) && (!item.IsDeleted))
					{
						items.Add( item );
					}
				}
			}
			else if ( showAllRadioButton.Checked )
			{
                foreach(TitleItem item in title.TitleItems)
                {
                    if (!item.IsDeleted) items.Add(item);
                }
			}

			TitleItemComparer comp = new TitleItemComparer( (TitleItemComparer.CompareEnum)_sortColumn, _sortOrder );
			items.Sort( comp );
			itemsList.DataSource = items;
			itemsList.DataBind();
        }

        #endregion Item methods

		#region Event handlers

        #region Author event handlers

        protected void creatorsList_RowEditing( object sender, GridViewEditEventArgs e )
		{
			creatorsList.EditIndex = e.NewEditIndex;
			bindTitleAuthorData();
		}

		protected void creatorsList_RowUpdating( object sender, GridViewUpdateEventArgs e )
		{
			GridViewRow row = creatorsList.Rows[ e.RowIndex ];

			if ( row != null )
			{
				DropDownList ddlCreatorRole = row.FindControl( "ddlCreatorRole" ) as DropDownList;
                TextBox txtRelationship = row.FindControl("txtRelationship") as TextBox;
                TextBox txtTitleOfWork = row.FindControl("txtTitleOfWork") as TextBox;
                if (ddlCreatorRole != null)
				{
                    Title title = (Title)Session["Title" + idLabel.Text];
					int authorRoleId = int.Parse( ddlCreatorRole.SelectedValue );
                    String relationship = txtRelationship.Text;
                    String titleOfWork = txtTitleOfWork.Text;

                    SecUser secUser = getSecUser();
                    TitleAuthor titleAuthor = findTitleAuthor(title.TitleAuthors,
						(int)creatorsList.DataKeys[ e.RowIndex ].Values[ 0 ],
						(int)creatorsList.DataKeys[ e.RowIndex ].Values[ 1 ],
						(int)creatorsList.DataKeys[ e.RowIndex ].Values[ 2 ] );

                    if (titleAuthor.AuthorRoleID != authorRoleId ||
                        titleAuthor.RoleDescription != ddlCreatorRole.SelectedItem.Text)
                    {
                        // Make sure something has actually changed before updating the lastmodifieduserid
                        titleAuthor.LastModifiedUserID = secUser.UserID;
                    }
					titleAuthor.AuthorRoleID = authorRoleId;
					titleAuthor.RoleDescription = ddlCreatorRole.SelectedItem.Text;
                    titleAuthor.Relationship = relationship;
                    titleAuthor.TitleOfWork = titleOfWork;
				}
			}

			creatorsList.EditIndex = -1;
			bindTitleAuthorData();
		}

		protected void creatorsList_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
		{
			creatorsList.EditIndex = -1;
			bindTitleAuthorData();
		}

		protected void creatorsList_RowCommand( object sender, GridViewCommandEventArgs e )
		{
			if ( e.CommandName.Equals( "RemoveButton" ) )
			{
				int rowNum = int.Parse( e.CommandArgument.ToString() );
                Title title = (Title)Session["Title" + idLabel.Text];

				TitleAuthor titleAuthor = findTitleAuthor( title.TitleAuthors,
					(int)creatorsList.DataKeys[ rowNum ].Values[ 0 ],
					(int)creatorsList.DataKeys[ rowNum ].Values[ 1 ],
					(int)creatorsList.DataKeys[ rowNum ].Values[ 2 ] );

				titleAuthor.IsDeleted = true;
				bindTitleAuthorData();
			}
        }

        #endregion Author event handlers

        #region TitleKeyword event handlers

        protected void subjectsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            subjectsList.EditIndex = e.NewEditIndex;
            bindSubjectData();
        }

        protected void subjectsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = subjectsList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox txtKeyword = row.FindControl("txtKeyword") as TextBox;
                TextBox txtMarcDataFieldTag = row.FindControl("txtMarcDataFieldTag") as TextBox;
                TextBox txtMarcSubFieldCode = row.FindControl("txtMarcSubFieldCode") as TextBox;
                if (txtKeyword != null)
                {
                    Title title = (Title)Session["Title" + idLabel.Text];
                    String keyword = txtKeyword.Text;
                    String marcDataFieldTag = txtMarcDataFieldTag.Text;
                    String marcSubFieldCode = txtMarcSubFieldCode.Text;

                    TitleKeyword titleKeyword = findTitleKeyword(title.TitleKeywords,
                        (int)subjectsList.DataKeys[e.RowIndex].Values[0],
                        (int)subjectsList.DataKeys[e.RowIndex].Values[1],
                        subjectsList.DataKeys[e.RowIndex].Values[2].ToString());

                    titleKeyword.TitleID = title.TitleID;
                    titleKeyword.Keyword = keyword;
                    titleKeyword.MarcDataFieldTag = marcDataFieldTag;
                    titleKeyword.MarcSubFieldCode = marcSubFieldCode;
                }
            }

            subjectsList.EditIndex = -1;
            bindSubjectData();
        }

        protected void subjectsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            subjectsList.EditIndex = -1;
            bindSubjectData();
        }

        protected void addSubjectButton_Click(object sender, EventArgs e)
        {
            Title title = (Title)Session["Title" + idLabel.Text];
            TitleKeyword titleKeyword = new TitleKeyword();
            titleKeyword.TitleID = title.TitleID;
            title.TitleKeywords.Add(titleKeyword);
            subjectsList.EditIndex = subjectsList.Rows.Count;
            bindSubjectData();
        }

        protected void subjectsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];

                TitleKeyword titleKeyword = findTitleKeyword(title.TitleKeywords,
                    (int)subjectsList.DataKeys[rowNum].Values[0],
                    (int)subjectsList.DataKeys[rowNum].Values[1],
                    subjectsList.DataKeys[rowNum].Values[2].ToString());

                titleKeyword.IsDeleted = true;
                bindSubjectData();
            }
        }

        #endregion TitleKeyword event handlers

        #region TitleVariant event handlers

        protected void variantsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            variantsList.EditIndex = e.NewEditIndex;
            bindTitleVariantData();
        }

        protected void variantsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = variantsList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlVariantTypeName = row.FindControl("ddlVariantTypeName") as DropDownList;
                TextBox txtVariantTitle = row.FindControl("txtVariantTitle") as TextBox;
                TextBox txtVariantTitleRemainder = row.FindControl("txtVariantTitleRemainder") as TextBox;
                TextBox txtVariantPartNumber = row.FindControl("txtVariantPartNumber") as TextBox;
                TextBox txtVariantPartName = row.FindControl("txtVariantPartName") as TextBox;
                if (ddlVariantTypeName != null && txtVariantTitle != null &&
                    txtVariantTitleRemainder != null && txtVariantPartNumber != null &&
                    txtVariantPartName != null)
                {
                    Title title = (Title)Session["Title" + idLabel.Text];
                    int titleVariantTypeId = int.Parse(ddlVariantTypeName.SelectedValue);
                    String variantTitle = txtVariantTitle.Text;
                    String variantTitleRemainder = txtVariantTitleRemainder.Text;
                    String variantPartNumber = txtVariantPartNumber.Text;
                    String variantPartName = txtVariantPartName.Text;

                    TitleVariant titleVariant = findTitleVariant(title.TitleVariants,
                        (int)variantsList.DataKeys[e.RowIndex].Values[0]);

                    titleVariant.TitleID = title.TitleID;
                    titleVariant.TitleVariantTypeID = titleVariantTypeId;
                    titleVariant.TitleVariantTypeName = ddlVariantTypeName.SelectedItem.Text;
                    titleVariant.Title = variantTitle;
                    titleVariant.TitleRemainder = variantTitleRemainder;
                    titleVariant.PartNumber = variantPartNumber;
                    titleVariant.PartName = variantPartName;
                }
            }

            variantsList.EditIndex = -1;
            bindTitleVariantData();
        }

        protected void variantsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            variantsList.EditIndex = -1;
            bindTitleVariantData();
        }

        protected void addTitleVariantButton_Click(object sender, EventArgs e)
        {
            Title title = (Title)Session["Title" + idLabel.Text];
            TitleVariant tv = new TitleVariant();
            tv.TitleID = title.TitleID;
            title.TitleVariants.Add(tv);
            variantsList.EditIndex = variantsList.Rows.Count;
            bindTitleVariantData();
        }

        protected void variantsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];

                TitleVariant titleVariant = findTitleVariant(title.TitleVariants,
                    (int)variantsList.DataKeys[rowNum].Values[0]);

                titleVariant.IsDeleted = true;
                bindTitleVariantData();
            }
        }

        #endregion TitleVariant event handlers

        #region TitleIdentifier event handlers

        protected void identifiersList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            identifiersList.EditIndex = e.NewEditIndex;
            bindTitleIdentifierData();
        }

        protected void identifiersList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = identifiersList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlIdentifierName = row.FindControl("ddlIdentifierName") as DropDownList;
                TextBox txtIdentifierValue = row.FindControl("txtIdentifierValue") as TextBox;
                if (ddlIdentifierName != null && txtIdentifierValue != null)
                {
                    Title title = (Title)Session["Title" + idLabel.Text];
                    int identifierId = int.Parse(ddlIdentifierName.SelectedValue);
                    String identifierValue = txtIdentifierValue.Text;

                    Title_Identifier titleIdentifier = findTitle_Identifier(title.TitleIdentifiers,
                        (int)identifiersList.DataKeys[e.RowIndex].Values[0],
                        (int)identifiersList.DataKeys[e.RowIndex].Values[1],
                        identifiersList.DataKeys[e.RowIndex].Values[2].ToString());

                    titleIdentifier.TitleID = title.TitleID;
                    titleIdentifier.IdentifierID = identifierId;
                    titleIdentifier.IdentifierName = ddlIdentifierName.SelectedItem.Text;
                    titleIdentifier.IdentifierValue = identifierValue;
                }
            }

            identifiersList.EditIndex = -1;
            bindTitleIdentifierData();
        }

        protected void identifiersList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            identifiersList.EditIndex = -1;
            bindTitleIdentifierData();
        }

        protected void addTitleIdentifierButton_Click(object sender, EventArgs e)
        {
            Title title = (Title)Session["Title" + idLabel.Text];
            Title_Identifier ti = new Title_Identifier();
            ti.TitleID = title.TitleID;
            title.TitleIdentifiers.Add(ti);
            identifiersList.EditIndex = identifiersList.Rows.Count;
            bindTitleIdentifierData();
        }

        protected void identifiersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];

                Title_Identifier titleIdentifier = findTitle_Identifier(title.TitleIdentifiers,
                    (int)identifiersList.DataKeys[rowNum].Values[0],
                    (int)identifiersList.DataKeys[rowNum].Values[1],
                    identifiersList.DataKeys[rowNum].Values[2].ToString());

                titleIdentifier.IsDeleted = true;
                bindTitleIdentifierData();
            }
        }

        #endregion TitleIdentifier event handlers

        #region Association event handlers

        protected void associationsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];

                TitleAssociation titleAssociation = findTitleAssociation(title.TitleAssociations,
                    (int)associationsList.DataKeys[rowNum].Values[0]);

                // Delete the association and any related title identifiers
                titleAssociation.IsDeleted = true;
                foreach(TitleAssociation_TitleIdentifier tati in titleAssociation.TitleAssociationIdentifiers)
                {
                    tati.IsDeleted = true;
                }
                bindTitleAssociationData();
            }
        }

        #endregion Association event handlers

        #region TitleLanguage event handlers

        protected void languagesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            languagesList.EditIndex = e.NewEditIndex;
            bindLanguageData();
        }

        protected void languagesList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = languagesList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlLanguageName = row.FindControl("ddlLanguageName") as DropDownList;
                if (ddlLanguageName != null)
                {
                    Title title = (Title)Session["Title" + idLabel.Text];
                    String languageCode = ddlLanguageName.SelectedValue;

                    TitleLanguage titleLanguage = findTitleLanguage(title.TitleLanguages,
                    (int)languagesList.DataKeys[e.RowIndex].Values[0],
                    languagesList.DataKeys[e.RowIndex].Values[1].ToString());

                    titleLanguage.LanguageCode = languageCode;
                    titleLanguage.LanguageName = ddlLanguageName.SelectedItem.Text;
                }
            }

            languagesList.EditIndex = -1;
            bindLanguageData();
        }

        protected void languagesList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            languagesList.EditIndex = -1;
            bindLanguageData();
        }

        protected void addLanguageButton_Click(object sender, EventArgs e)
        {
            SecUser secUser = this.getSecUser();
            int userId = secUser.UserID;

            Title title = (Title)Session["Title" + idLabel.Text];
            TitleLanguage tl = new TitleLanguage(0, title.TitleID, "", DateTime.Now, userId);
            title.TitleLanguages.Add(tl);
            languagesList.EditIndex = languagesList.Rows.Count;
            bindLanguageData();
        }

        protected void languagesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];

                TitleLanguage titleLanguage = findTitleLanguage(title.TitleLanguages,
                    (int)languagesList.DataKeys[rowNum].Values[0],
                    languagesList.DataKeys[rowNum].Values[1].ToString());

                titleLanguage.IsDeleted = true;
                bindLanguageData();
            }
        }

        #endregion TitleLanguage event handlers

        #region Collection event handlers

        protected void collectionsList_RowEditing(object sender, GridViewEditEventArgs e)
		{
			collectionsList.EditIndex = e.NewEditIndex;
			bindCollectionData();
		}

		protected void collectionsList_RowUpdating( object sender, GridViewUpdateEventArgs e )
		{
			GridViewRow row = collectionsList.Rows[ e.RowIndex ];

			if ( row != null )
			{
				DropDownList ddlCollection = row.FindControl( "ddlCollection" ) as DropDownList;
				if ( ddlCollection != null )
				{
                    Title title = (Title)Session["Title" + idLabel.Text];
					int collectionId = int.Parse( ddlCollection.SelectedValue );

					TitleCollection titleCollection = findCollection( title.TitleCollections,
					    (int)collectionsList.DataKeys[ e.RowIndex ].Values[ 0 ],
					    (int)collectionsList.DataKeys[ e.RowIndex ].Values[ 1 ] );

					titleCollection.CollectionID = collectionId;
					titleCollection.CollectionName = ddlCollection.SelectedItem.Text;
				}
			}

			collectionsList.EditIndex = -1;
			bindCollectionData();
		}

		protected void collectionsList_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
		{
			collectionsList.EditIndex = -1;
			bindCollectionData();
		}

		protected void addCollectionButton_Click( object sender, EventArgs e )
		{
            Title title = (Title)Session["Title" + idLabel.Text];
            TitleCollection tc = new TitleCollection(0, title.TitleID, 0);
			title.TitleCollections.Add( tc );
			collectionsList.EditIndex = collectionsList.Rows.Count;
			bindCollectionData();
		}

		protected void collectionsList_RowCommand( object sender, GridViewCommandEventArgs e )
		{
			if ( e.CommandName.Equals( "RemoveButton" ) )
			{
				int rowNum = int.Parse( e.CommandArgument.ToString() );
                Title title = (Title)Session["Title" + idLabel.Text];

				TitleCollection collection = findCollection( title.TitleCollections,
					(int)collectionsList.DataKeys[ rowNum ].Values[ 0 ],
					(int)collectionsList.DataKeys[ rowNum ].Values[ 1 ] );

				collection.IsDeleted = true;
				bindCollectionData();
			}
		}

        #endregion Collection event handlers

        #region Item event handlers

        protected void itemsList_RowEditing(object sender, GridViewEditEventArgs e
            )
		{
			itemsList.EditIndex = e.NewEditIndex;
			bindItemData();
		}

        protected void itemsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                int selectedItem = (int)itemsList.DataKeys[rowNum].Values[0];
                Title title = (Title)Session["Title" + idLabel.Text];
                foreach (TitleItem titleItem in title.TitleItems)
                {
                    if (selectedItem == titleItem.ItemID)
                    {
                        if ((title.TitleID == titleItem.PrimaryTitleID) && (!titleItem.IsNew))
                        {
                            errorControl.AddErrorText("Cannot delete previously saved items for which this is the primary title.");
                            errorControl.Visible = true;
                            Page.MaintainScrollPositionOnPostBack = false;
                        }
                        else
                        {
                            titleItem.IsDeleted = true;
                        }
                        break;
                    }
                }
                bindItemData();
            }
        }

        protected void itemsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow row = itemsList.Rows[ e.RowIndex ];

			if ( row != null )
			{
				TextBox textBox = row.FindControl( "itemSequenceTextBox" ) as TextBox;
				if ( textBox != null )
				{
                    Title title = (Title)Session["Title" + idLabel.Text];

                    string newItemSeqString = textBox.Text.Trim();
                    short newItemSeq = 0;
					short.TryParse( newItemSeqString, out newItemSeq );

					string itemIdString = row.Cells[ 1 ].Text;
					int itemId = 0;
					int.TryParse( itemIdString, out itemId );

					if ( newItemSeq > 0 && itemId > 0 )
					{
						// Find item being changed
						short? oldItemSeq = 0;
                        TitleItem changedItem = null;

						foreach ( TitleItem item in title.TitleItems )
						{
							if ( item.ItemID == itemId && item.ItemSequence.HasValue )
							{
                                changedItem = item;
								oldItemSeq = changedItem.ItemSequence.Value;
								break;
							}
						}

                        if (changedItem != null)
                        {
                            // If sequence has been decreased
                            if (newItemSeq < oldItemSeq)
                            {
                                // Increment all item sequences between the old and new sequence values
                                foreach (TitleItem item in title.TitleItems)
                                {
                                    if (item.ItemSequence >= newItemSeq && item.ItemSequence < oldItemSeq)
                                    {
                                        item.ItemSequence++;
                                    }
                                }
                            }

                            // If sequence has been increased
                            if (newItemSeq > oldItemSeq)
                            {
                                // Decrement all item sequences between the old and new sequence values
                                foreach (TitleItem item in title.TitleItems)
                                {
                                    if (item.ItemSequence <= newItemSeq && item.ItemSequence > oldItemSeq)
                                    {
                                        item.ItemSequence--;
                                    }
                                }
                            }

                            // Change the old sequence value to the new sequence value
                            changedItem.ItemSequence = newItemSeq;
                            /*
                            foreach ( TitleItem item in title.TitleItems )
                            {
                                if ( item.ItemID == itemId )
                                {
                                    item.ItemSequence = newItemSeq;
                                    break;
                                }
                            }
                            */
                        }
					}
				}
			}

			itemsList.EditIndex = -1;
			bindItemData();
		}

		protected void itemsList_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
		{
			itemsList.EditIndex = -1;
			bindItemData();
		}

		protected void itemsList_Sorting( object sender, GridViewSortEventArgs e )
		{
			TitleItemComparer.CompareEnum sortColumn = _sortColumn;

			if ( e.SortExpression.Equals( "ItemID" ) )
			{
				_sortColumn = TitleItemComparer.CompareEnum.ItemID;
			}
			else if ( e.SortExpression.Equals( "BarCode" ) )
			{
				_sortColumn = TitleItemComparer.CompareEnum.BarCode;
			}
			else if ( e.SortExpression.Equals( "ItemSequence" ) )
			{
				_sortColumn = TitleItemComparer.CompareEnum.ItemSequence;
			}
			else if ( e.SortExpression.Equals( "Volume" ) )
			{
				_sortColumn = TitleItemComparer.CompareEnum.Volume;
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

			bindItemData();
		}

		protected void itemsList_RowDataBound( object sender, GridViewRowEventArgs e )
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
					case TitleItemComparer.CompareEnum.BarCode:
						{
							sortColumnIndex = 3;
							break;
						}
					case TitleItemComparer.CompareEnum.ItemSequence:
						{
							sortColumnIndex = 4;
							break;
						}
					case TitleItemComparer.CompareEnum.Volume:
						{
							sortColumnIndex = 5;
							break;
						}
				}

				e.Row.Cells[ sortColumnIndex ].Controls.Add( new LiteralControl( " " ) );
				e.Row.Cells[ sortColumnIndex ].Controls.Add( img );
				e.Row.Cells[ sortColumnIndex ].Wrap = false;
			}
        }

        protected void itemFilter_CheckedChanged(object sender, EventArgs e)
        {
            bindItemData();
        }

        #endregion Item event handlers

        protected void saveButton_Click( object sender, EventArgs e )
		{
            Title title = (Title)Session["Title" + idLabel.Text];
            short? s = null;
            int? userId = null;

			if ( validate( title ) )
			{
                int? bibLevelID = null;
                if (ddlBibliographicLevel.SelectedValue != "0") bibLevelID = (int?)Convert.ToInt32(ddlBibliographicLevel.SelectedValue);

                // Set the id of the editing user
                SecUser secUser = this.getSecUser();
                userId = secUser.UserID;

				// Gather up data on form
                title.RedirectTitleID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
                title.PublishReady = publishReadyCheckBox.Checked;
                title.BibliographicLevelID = bibLevelID;
				title.FullTitle = fullTitleTextBox.Text.Trim();
				title.ShortTitle = shortTitleTextBox.Text.Trim();
				title.SortTitle = sortTitleTextBox.Text.Trim();
				title.UniformTitle = uniformTitleTextBox.Text.Trim();
                title.PartNumber = partNumberTextBox.Text.Trim();
                title.PartName = partNameTextBox.Text.Trim();
				title.CallNumber = callNumberTextBox.Text.Trim();
                title.InstitutionCode = (ddlInst.SelectedValue.Length == 0 ? null : ddlInst.SelectedValue);
				title.LanguageCode = ( ddlLang.SelectedValue.Length == 0 ? null : ddlLang.SelectedValue );
				title.TitleDescription = descTextBox.Text.Trim();
				title.PublicationDetails = publicationPlaceTextBox.Text.Trim() + publisherNameTextBox.Text.Trim() + publicationDateTextBox.Text.Trim();
                title.Datafield_260_a = publicationPlaceTextBox.Text.Trim();
                title.Datafield_260_b = publisherNameTextBox.Text.Trim();
                title.Datafield_260_c = publicationDateTextBox.Text.Trim();
				title.StartYear = ( startYearTextBox.Text.Trim().Length == 0 ? s : short.Parse( startYearTextBox.Text.Trim() ) );
				title.EndYear = ( endYearTextBox.Text.Trim().Length == 0 ? s : short.Parse( endYearTextBox.Text.Trim() ) );
                title.OriginalCatalogingSource = OrigCatalogSourceTextBox.Text.Trim();
                title.EditionStatement = EditionStatementTextBox.Text.Trim();
                title.CurrentPublicationFrequency = PubFrequencyTextBox.Text.Trim();
				title.Note = notesTextBox.Text.Trim();

				title.IsNew = false;

				// Forces deletes to happen first
				title.TitleCollections.Sort( SortOrder.Descending, "IsDeleted" );
                title.TitleIdentifiers.Sort(SortOrder.Descending, "IsDeleted");
				title.TitleAuthors.Sort( SortOrder.Descending, "IsDeleted" );
                title.TitleItems.Sort(SortOrder.Descending, "IsDeleted");
                title.TitleKeywords.Sort(SortOrder.Descending, "IsDeleted");
                title.TitleAssociations.Sort(SortOrder.Descending, "IsDeleted");
                title.TitleVariants.Sort(SortOrder.Descending, "IsDeleted");

				BHLProvider bp = new BHLProvider();
                // Don't catch errors... allow global error handler to take over
				bp.TitleSave( title, (int)userId );

                // After a successful save operation, reload the title
                fillUI(title.TitleID);
            }
			else
			{
				return;
			}

            litMessage.Text = "<span class='liveData'>Title Saved.</span>";
            Page.MaintainScrollPositionOnPostBack = false;
		}

		#endregion

        private SecUser getSecUser()
        {
            HttpCookie tokenCookie = Request.Cookies["MOBOTSecurityToken"];
            return Helper.GetSecProvider().SecUserSelect(tokenCookie.Value);
        }

        private bool validate(Title title)
        {
            bool flag = false;

            // Check that all edits were completed
            if (creatorsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Creators has an edit pending");
            }

            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending");
            }

            if (variantsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Variants has an edit pending");
            }

            if (collectionsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Collections has an edit pending");
            }

            if (itemsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Items has an edit pending");
            }

            // If a "replaced by" identifer was specified, make sure that it is a valid id
            if (replacedByTextBox.Text.Trim().Length > 0)
            {
                int titleID;
                if (Int32.TryParse(replacedByTextBox.Text, out titleID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().TitleSelectAuto(titleID) == null)
                    {
                        flag = true;
                        errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Title ID.");
                    }
                }
                else
                {
                    // Specified ID is not a valid integer value
                    flag = true;
                    errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Title ID.");
                }
            }

            // Validate other inputs
            if (fullTitleTextBox.Text.Trim().Length == 0)
            {
                flag = true;
                errorControl.AddErrorText("Full title is missing");
            }

            short x = 0;
            if (startYearTextBox.Text.Trim().Length > 0)
            {
                if (short.TryParse(startYearTextBox.Text.Trim(), out x) == false)
                {
                    flag = true;
                    errorControl.AddErrorText("Start Year must be a numeric value between 0 and 32767");
                }
            }

            if (endYearTextBox.Text.Trim().Length > 0)
            {
                if (short.TryParse(endYearTextBox.Text.Trim(), out x) == false)
                {
                    flag = true;
                    errorControl.AddErrorText("End Year must be a numeric value between 0 and 32767");
                }
            }

            bool br = false;
            int ix = 0;
            foreach (TitleCollection tc in title.TitleCollections)
            {
                if (tc.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (TitleCollection tc2 in title.TitleCollections)
                    {
                        if (tc2.IsDeleted == false)
                        {
                            if ((tc.TitleCollectionID != tc2.TitleCollectionID && tc.CollectionID == tc2.CollectionID) ||
                                (tc.TitleCollectionID == 0 && tc2.TitleCollectionID == 0 && tc.CollectionID == tc2.CollectionID &&
                                ix != iy))
                            {
                                br = true;
                                flag = true;
                                errorControl.AddErrorText("Cannot duplicate collections");
                                break;
                            }
                        }
                        iy++;
                    }
                    if (br)
                    {
                        break;
                    }
                }
                ix++;
            }

            /*
            br = false;
            ix = 0;
            foreach (TitleAuthor tc in title.TitleAuthors)
            {
                if (tc.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (TitleAuthor tc2 in title.TitleAuthors)
                    {
                        if (tc2.IsDeleted == false)
                        {
                            if ((tc.TitleAuthorID != tc2.TitleAuthorID && tc.AuthorID == tc2.AuthorID &&
                                tc.AuthorRoleID == tc2.AuthorRoleID) ||
                                (tc.TitleAuthorID == 0 && tc.TitleAuthorID == 0 && tc.AuthorID == tc2.AuthorID &&
                                tc.AuthorRoleID == tc2.AuthorRoleID && ix != iy))
                            {
                                br = true;
                                flag = true;
                                errorControl.AddErrorText("Cannot duplicate title creators");
                            }
                        }
                        iy++;
                    }
                    if (br)
                    {
                        break;
                    }
                }
                ix++;
            }
             */

            errorControl.Visible = flag;
            Page.MaintainScrollPositionOnPostBack = !flag;

            return !flag;
        }
	}
}
