using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
    public partial class TitleEdit : System.Web.UI.Page
	{
        private ItemTitleComparer.CompareEnum _itemSortColumn = ItemTitleComparer.CompareEnum.ItemSequence;
		private SortOrder _sortOrder = SortOrder.Ascending;

        protected void Page_Load( object sender, EventArgs e )
		{
            HtmlLink cssLnk = new HtmlLink();
            cssLnk.Attributes.Add("rel", "stylesheet");
            cssLnk.Attributes.Add("type", "text/css");
            cssLnk.Href = ConfigurationManager.AppSettings["jQueryUICSSPath"];
            Page.Header.Controls.Add(cssLnk);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryUIPath"]);

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
                        newTitleAuthor.FullerForm = author.FullerForm;
                        newTitleAuthor.Numeration = author.Numeration;
                        newTitleAuthor.Unit = author.Unit;
                        newTitleAuthor.Title = author.Title;
                        newTitleAuthor.Location = author.Location;
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
                        foreach (ItemTitle existingItemTitle in title.ItemTitles)
                        {
                            if (existingItemTitle.BookID.ToString() == selectedItemId)
                            {
                                itemExists = true;
                                break;
                            }
                        }

                        if (!itemExists)
                        {
                            ItemTitle newItemTitle = new ItemTitle();

                            // Get the current maximum itemsequence value
                            short? itemSequence = 0;
                            foreach (ItemTitle itemTitle in title.ItemTitles)
                            {
                                if (itemTitle.ItemSequence > itemSequence) itemSequence = itemTitle.ItemSequence;
                            }

                            // Get details for "selectedItemId" from database
                            BHLProvider provider = new BHLProvider();
                            Book book = provider.BookSelectByBarcodeOrItemID(Convert.ToInt32(selectedItemId), null);
                            newItemTitle.BookID = book.BookID;
                            newItemTitle.ItemID = book.ItemID;
                            newItemTitle.TitleID = title.TitleID;
                            newItemTitle.ItemSequence = ++itemSequence;
                            newItemTitle.ItemStatusID = book.ItemStatusID;
                            newItemTitle.BarCode = book.BarCode;
                            newItemTitle.Volume = book.Volume;
                            newItemTitle.PrimaryTitleID = (makePrimary.Checked) ? title.TitleID : (int)book.PrimaryTitleID;
                            newItemTitle.IsNew = true;
                            title.ItemTitles.Add(newItemTitle);
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

                if (ViewState["ItemSortColumn"] != null)
				{
					_itemSortColumn = (ItemTitleComparer.CompareEnum)ViewState[ "ItemSortColumn" ];
					_sortOrder = (SortOrder)ViewState[ "SortOrder" ];
				}
            }

            editHistoryControl.EntityName = "title";
            editHistoryControl.EntityId = idLabel.Text;

            litMessage.Text = "";
			errorControl.Visible = false;
			Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill methods

        private void fillCombos()
		{
			BHLProvider bp = new BHLProvider();

            List<Institution> institutions = bp.InstituationSelectAll();

            Institution emptyInstitution = new Institution();
            emptyInstitution.InstitutionCode = string.Empty;
            emptyInstitution.InstitutionName = string.Empty;
            institutions.Insert(0, emptyInstitution);

            List<BibliographicLevel> bibliographicLevels = bp.BibliographicLevelSelectAll();

            BibliographicLevel emptyBibLevel = new BibliographicLevel();
            emptyBibLevel.BibliographicLevelID = 0;
            emptyBibLevel.BibliographicLevelName = string.Empty;
            bibliographicLevels.Insert(0, emptyBibLevel);

            ddlBibliographicLevel.DataSource = bibliographicLevels;
            ddlBibliographicLevel.DataTextField = "ExpandedLabel";
            ddlBibliographicLevel.DataValueField = "BibliographicLevelID";
            ddlBibliographicLevel.DataBind();

            List<MaterialType> materialTypes = bp.MaterialTypeSelectAll();

            MaterialType emptyMaterialType = new MaterialType();
            emptyMaterialType.MaterialTypeID = 0;
            emptyMaterialType.MaterialTypeLabel = string.Empty;
            materialTypes.Insert(0, emptyMaterialType);

            ddlMaterialType.DataSource = materialTypes;
            ddlMaterialType.DataTextField = "ExpandedLabel";
            ddlMaterialType.DataValueField = "MaterialTypeID";
            ddlMaterialType.DataBind();

            List<Language> languages = bp.LanguageSelectAll();

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

            // Look up flickr status of each item associated with the title
            List<Item> items = bp.ItemInFlickrByTitleID(id);
            foreach(ItemTitle item in title.ItemTitles)
            {
                foreach (Item flickrItem in items)
                {
                    if (flickrItem.ItemID == item.ItemID)
                    {
                        item.HasFlickrImages = flickrItem.HasFlickrImages;
                        break;
                    }
                }
            }

            // Enable segment download link if segments exist
            hypSegmentDownload.HRef = "~/downloads/SegmentsForTitle/" + title.TitleID.ToString();
            hypSegmentDownload.Visible = (bp.SegmentSelectByTitleID(title.TitleID).Count > 0 ? true : false);

            Session[ "Title" + title.TitleID.ToString()] = title;

            replacedByTextBox.Text = title.RedirectTitleID.ToString();
            replacedByOrig.Value = title.RedirectTitleID.ToString();
            String displayTitle = ((title.ShortTitle.Length > 30) ? title.ShortTitle.Substring(0, 30) + "..." : title.ShortTitle);
            makePrimary.Text = "Make Title " + title.TitleID.ToString() + " (\"" + displayTitle + "\") the Primary title for the items.";
            ddlBibliographicLevel.SelectedValue = (title.BibliographicLevelID ?? 0).ToString();
            ddlMaterialType.SelectedValue = (title.MaterialTypeID ?? 0).ToString();
            publishReadyCheckBox.Checked = title.PublishReady;
            publishReadyOrig.Value = title.PublishReady.ToString();
			marcBibIdLabel.Text = title.MARCBibID;
			marcLeaderLabel.Text = title.MARCLeader;
			fullTitleTextBox.Text = title.FullTitle;
			shortTitleTextBox.Text = title.ShortTitle;
			sortTitleTextBox.Text = title.SortTitle;
			uniformTitleTextBox.Text = title.UniformTitle;
            partNumberTextBox.Text = title.PartNumber;
            partNameTextBox.Text = title.PartName;
			callNumberTextBox.Text = title.CallNumber;

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

            resourcesList.DataSource = title.TitleExternalResources;
            resourcesList.DataBind();

            notesList.DataSource = title.TitleNotes;
            notesList.DataBind();

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
        }

        #endregion Fill methods

        #region TitleAuthor methods

        private void bindTitleAuthorData()
		{
			Title title = (Title)Session[ "Title" + idLabel.Text ];

            // filter out deleted items
            List<TitleAuthor> titleAuthors = new List<TitleAuthor>();
			foreach ( TitleAuthor tc in title.TitleAuthors )
			{
				if ( tc.IsDeleted == false )
				{
					titleAuthors.Add( tc );
				}
			}

            TitleAuthorSequenceComparer comp = new TitleAuthorSequenceComparer();
            titleAuthors.Sort(comp);
            creatorsList.DataSource = titleAuthors;
			creatorsList.DataBind();
		}

        List<AuthorRole> _authorRoles = null;
		protected List<AuthorRole> GetAuthorRoles()
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

		private TitleAuthor findTitleAuthor(List<TitleAuthor> titleAuthors, int titleAuthorId,
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
            List<TitleKeyword> titleKeywords = new List<TitleKeyword>();
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

        private TitleKeyword findTitleKeyword(List<TitleKeyword> titleKeywords, 
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

        #region TitleExternalResource methods

        private void bindExternalResourceData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            List<TitleExternalResource> titleExternalResources = new List<TitleExternalResource>();
            foreach (TitleExternalResource tr in title.TitleExternalResources)
            {
                if (tr.IsDeleted == false)
                {
                    titleExternalResources.Add(tr);
                }
            }

            resourcesList.DataSource = titleExternalResources;
            resourcesList.DataBind();
        }

        private TitleExternalResource findTitleExternalResource(List<TitleExternalResource> titleExternalResources,
            int titleExternalResourceId, int externalResourceTypeID, string urlText, string url)
        {
            foreach (TitleExternalResource tr in titleExternalResources)
            {
                if (tr.IsDeleted)
                {
                    continue;
                }
                if (titleExternalResourceId == tr.TitleExternalResourceID &&
                    externalResourceTypeID == tr.TitleExternalResourceTypeID &&
                    urlText == tr.UrlText &&
                    url == tr.Url)
                {
                    return tr;
                }
            }

            return null;
        }

        protected short GetMaxExternalResourceSequence()
        {
            short maxSeq = 0;

            Title title = (Title)Session["Title" + idLabel.Text];
            foreach (TitleExternalResource titleExternalResource in title.TitleExternalResources)
            {
                if (!titleExternalResource.IsDeleted)
                {
                    if (titleExternalResource.SequenceOrder  > maxSeq) maxSeq = titleExternalResource.SequenceOrder;
                }
            }

            return maxSeq;
        }

        List<TitleExternalResourceType> _externalResourceTypes = null;
        protected List<TitleExternalResourceType> GetExternalResourceTypes()
        {
            BHLProvider bp = new BHLProvider();
            _externalResourceTypes = bp.TitleExternalResourceTypeSelectAll();

            return _externalResourceTypes;
        }

        protected int GetExternalResourceIndex(object dataItem)
        {
            string externalResourceTypeIdString = DataBinder.Eval(dataItem, "TitleExternalResourceTypeID").ToString();

            if (!externalResourceTypeIdString.Equals("0"))
            {
                int externalResourceTypeId = int.Parse(externalResourceTypeIdString);
                int ix = 0;
                foreach (TitleExternalResourceType externalResourceType in _externalResourceTypes)
                {
                    if (externalResourceType.TitleExternalResourceTypeID == externalResourceTypeId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        #endregion

        #region TitleNote methods

        private void bindNotesData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            List<TitleNote> titleNotes = new List<TitleNote>();
            foreach (TitleNote tn in title.TitleNotes)
            {
                if (tn.IsDeleted == false)
                {
                    titleNotes.Add(tn);
                }
            }

            titleNotes.Sort((n1, n2) => (n1.NoteSequence ?? 0).CompareTo(n2.NoteSequence ?? 0));
            notesList.DataSource = titleNotes;
            notesList.DataBind();
        }

        private TitleNote findTitleNote(List<TitleNote> titleNotes,
            int titleNoteId, int noteTypeID, string noteText)
        {
            foreach (TitleNote tn in titleNotes)
            {
                if (tn.IsDeleted)
                {
                    continue;
                }
                if (titleNoteId == tn.TitleNoteID &&
                    noteTypeID == tn.NoteTypeID &&
                    noteText == tn.NoteText)
                {
                    return tn;
                }
            }

            return null;
        }

        protected short? GetMaxNoteSequence()
        {
            short? maxSeq = 0;

            Title title = (Title)Session["Title" + idLabel.Text];
            foreach (TitleNote titleNote in title.TitleNotes)
            {
                if (!titleNote.IsDeleted)
                {
                    if ((titleNote.NoteSequence ?? 0) > maxSeq) maxSeq = titleNote.NoteSequence;
                }
            }

            return maxSeq;
        }

        List<NoteType> _noteTypes = null;
        protected List<NoteType> GetNoteTypes()
        {
            BHLProvider bp = new BHLProvider();
            _noteTypes = bp.NoteTypeSelectAll();

            return _noteTypes;
        }

        protected int GetNoteTypeIndex(object dataItem)
        {
            string noteTypeIdString = DataBinder.Eval(dataItem, "NoteTypeID").ToString();

            if (!noteTypeIdString.Equals("0"))
            {
                int noteTypeId = int.Parse(noteTypeIdString);
                int ix = 0;
                foreach (NoteType noteType in _noteTypes)
                {
                    if (noteType.NoteTypeID == noteTypeId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        #endregion

        #region TitleVariant methods

        private void bindTitleVariantData()
        {
            Title title = (Title)Session["Title" + idLabel.Text];

            // filter out deleted items
            List<TitleVariant> titleVariants = new List<TitleVariant>();
            foreach (TitleVariant tv in title.TitleVariants)
            {
                if (!tv.IsDeleted) titleVariants.Add(tv);
            }

            variantsList.DataSource = titleVariants;
            variantsList.DataBind();
        }

        List<TitleVariantType> _titleVariantTypes = null;
        protected List<TitleVariantType> GetVariants()
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

        private TitleVariant findTitleVariant(List<TitleVariant> titleVariants,
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
            List<Title_Identifier> titleIdentifiers = new List<Title_Identifier>();
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

        List<Identifier> _identifiers = null;
        protected List<Identifier> GetIdentifiers()
        {
            BHLProvider bp = new BHLProvider();
            _identifiers = bp.IdentifierSelectByIDType("title");

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

        private Title_Identifier findTitle_Identifier(List<Title_Identifier> titleIdentifiers, 
            int titleIdentifierId, int identifierID, string identifierValue)
        {
            foreach (Title_Identifier tti in titleIdentifiers)
            {
                if (tti.IsDeleted)
                {
                    continue;
                }
                if (titleIdentifierId == tti.TitleIdentifierID &&
                    identifierID == tti.IdentifierID &&
                    identifierValue == tti.IdentifierValue)
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
            List<TitleAssociation> titleAssociations = new List<TitleAssociation>();
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

        private TitleAssociation findTitleAssociation(List<TitleAssociation> titleAssociations,
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
            List<TitleLanguage> titleLanguages = new List<TitleLanguage>();
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

        List<Language> _titleLanguages = null;
        protected List<Language> GetLanguages()
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

        private TitleLanguage findTitleLanguage(List<TitleLanguage> titleLanguages, 
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
            List<TitleCollection> titleCollections = new List<TitleCollection>();
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

        List<Collection> _titleCollections = null;
		protected List<Collection> GetCollections()
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

        private TitleCollection findCollection(List<TitleCollection> collections, int titleCollectionId,
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

            List<ItemTitle> items = new List<ItemTitle>();
			if ( showPubRadioButton.Checked || ( showAllRadioButton.Checked == false && showPubRadioButton.Checked == false ) )
			{
				foreach (ItemTitle item in title.ItemTitles)
				{
					if (( item.ItemStatusID == 40 ) && (!item.IsDeleted))
					{
						items.Add( item );
					}
				}
			}
			else if ( showAllRadioButton.Checked )
			{
                foreach(ItemTitle item in title.ItemTitles)
                {
                    if (!item.IsDeleted) items.Add(item);
                }
			}

            switch (_itemSortColumn)
            {
                case ItemTitleComparer.CompareEnum.BarCode:
                    if (_sortOrder == SortOrder.Ascending)
                        items.Sort((i1, i2) => (i1.BarCode).CompareTo(i2.BarCode));
                    else
                        items.Sort((i1, i2) => (i2.BarCode).CompareTo(i1.BarCode));
                    break;
                case ItemTitleComparer.CompareEnum.ItemID:
                    if (_sortOrder == SortOrder.Ascending)
                        items.Sort((i1, i2) => (i1.ItemID).CompareTo(i2.ItemID));
                    else
                        items.Sort((i1, i2) => (i2.ItemID).CompareTo(i1.ItemID));
                    break;
                case ItemTitleComparer.CompareEnum.ItemSequence:
                    if (_sortOrder == SortOrder.Ascending)
                        items.Sort((i1, i2) => (i1.ItemSequence ?? 0).CompareTo(i2.ItemSequence ?? 0));
                    else
                        items.Sort((i1, i2) => (i2.ItemSequence ?? 0).CompareTo(i1.ItemSequence ?? 0));
                    break;
                case ItemTitleComparer.CompareEnum.Volume:
                    if (_sortOrder == SortOrder.Ascending)
                        items.Sort((i1, i2) => (i1.Volume).CompareTo(i2.Volume));
                    else
                        items.Sort((i1, i2) => (i2.Volume).CompareTo(i1.Volume));
                    break;
            }

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
                Title title = (Title)Session["Title" + idLabel.Text];
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                TitleAuthor titleAuthor = null;
                if (title != null)
                {
                    titleAuthor = findTitleAuthor(title.TitleAuthors,
                        (int)creatorsList.DataKeys[e.RowIndex].Values[0],
                        (int)creatorsList.DataKeys[e.RowIndex].Values[1],
                        (int)creatorsList.DataKeys[e.RowIndex].Values[2]);
                }

                DropDownList ddlCreatorRole = row.FindControl( "ddlCreatorRole" ) as DropDownList;
                TextBox txtRelationship = row.FindControl("txtRelationship") as TextBox;
                TextBox txtTitleOfWork = row.FindControl("txtTitleOfWork") as TextBox;
                TextBox sequenceTextBox = row.FindControl("authorSequenceTextBox") as TextBox;

                if (ddlCreatorRole != null)
				{
					int authorRoleId = int.Parse( ddlCreatorRole.SelectedValue );
                    String relationship = txtRelationship.Text;
                    String titleOfWork = txtTitleOfWork.Text;

                    if (titleAuthor != null)
                    {
                        if (titleAuthor.AuthorRoleID != authorRoleId ||
                            titleAuthor.RoleDescription != ddlCreatorRole.SelectedItem.Text)
                        {
                            // Make sure something has actually changed before updating the lastmodifieduserid
                            titleAuthor.LastModifiedUserID = userId;
                        }
                        titleAuthor.AuthorRoleID = authorRoleId;
                        titleAuthor.RoleDescription = ddlCreatorRole.SelectedItem.Text;
                        titleAuthor.Relationship = relationship;
                        titleAuthor.TitleOfWork = titleOfWork;
                    }
				}

                if (sequenceTextBox != null)
                {
                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        if (titleAuthor != null)
                        {
                            short oldSeq = titleAuthor.SequenceOrder;

                            // If sequence has been decreased
                            if (newSeq < oldSeq)
                            {
                                // Increment all item sequences between the old and new sequence values
                                foreach (TitleAuthor author in title.TitleAuthors)
                                {
                                    if (author.SequenceOrder >= newSeq && author.SequenceOrder < oldSeq)
                                    {
                                        author.SequenceOrder++;
                                        author.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            // If sequence has been increased
                            if (newSeq > oldSeq)
                            {
                                // Decrement all item sequences between the old and new sequence values
                                foreach (TitleAuthor author in title.TitleAuthors)
                                {
                                    if (author.SequenceOrder <= newSeq && author.SequenceOrder > oldSeq && oldSeq > 0)
                                    {
                                        author.SequenceOrder--;
                                        author.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            titleAuthor.SequenceOrder = newSeq;
                            titleAuthor.LastModifiedUserID = userId;
                        }
                    }
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
                creatorsList.EditIndex = -1;
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
            subjectsList.Rows[subjectsList.EditIndex].FindControl("cancelSubjectCreatorButton").Visible = false;
        }

        protected void subjectsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                subjectsList.EditIndex = -1;
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

        #region TitleExternalResource event handlers

        protected void resourcesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            resourcesList.EditIndex = e.NewEditIndex;
            bindExternalResourceData();
        }

        protected void resourcesList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = resourcesList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlExternalResourceType = row.FindControl("ddlExternalResourceType") as DropDownList;
                TextBox txtUrlText = row.FindControl("txtUrlText") as TextBox;
                TextBox txtUrl = row.FindControl("txtUrl") as TextBox;
                TextBox txtExternalResourceSequence = row.FindControl("txtExternalResourceSequence") as TextBox;
                if (txtUrlText != null)
                {
                    short sequenceOrder = 0;
                    Title title = (Title)Session["Title" + idLabel.Text];

                    string urlText = txtUrlText.Text;
                    string url = txtUrl.Text;
                    string resourceSequenceText = txtExternalResourceSequence.Text;
                    if (!short.TryParse(resourceSequenceText, out sequenceOrder)) sequenceOrder = 0;
                    int resourceTypeID;
                    if (!Int32.TryParse(ddlExternalResourceType.Text, out resourceTypeID)) resourceTypeID = 1;

                    TitleExternalResource titleExternalResource = findTitleExternalResource(title.TitleExternalResources,
                        (int)resourcesList.DataKeys[e.RowIndex].Values[0],
                        (int)resourcesList.DataKeys[e.RowIndex].Values[1],
                        resourcesList.DataKeys[e.RowIndex].Values[2].ToString(),
                        resourcesList.DataKeys[e.RowIndex].Values[3].ToString());

                    // Update all sequences if necessary
                    short oldSeq = titleExternalResource.SequenceOrder;

                    // If sequence has been decreased
                    if (sequenceOrder < oldSeq)
                    {
                        // Increment all sequences between the old and new sequence values
                        foreach (TitleExternalResource resource in title.TitleExternalResources)
                        {
                            if (resource.SequenceOrder >= sequenceOrder && resource.SequenceOrder < oldSeq) resource.SequenceOrder++;
                        }
                    }

                    // If sequence has been increased
                    if (sequenceOrder > oldSeq)
                    {
                        // Decrement all sequences between the old and new sequence values
                        foreach (TitleExternalResource resource in title.TitleExternalResources)
                        {
                            if (resource.SequenceOrder <= sequenceOrder && resource.SequenceOrder > oldSeq)
                            {
                                resource.SequenceOrder--;
                            }
                        }
                    }

                    // Update the external resource being edited
                    //TitleExternalResourceType resourceType = new BHLProvider().NoteTypeSelectAuto(Convert.ToInt32(ddlNoteType.SelectedValue));

                    titleExternalResource.TitleID = title.TitleID;
                    titleExternalResource.TitleExternalResourceTypeID = resourceTypeID;
                    titleExternalResource.UrlText = urlText;
                    titleExternalResource.Url = url;
                    titleExternalResource.SequenceOrder = sequenceOrder;
                    titleExternalResource.ExternalResourceTypeLabel = ddlExternalResourceType.SelectedItem.Text;
                    //titleNote.NoteTypeName = noteType.NoteTypeName;
                    //titleNote.NoteTypeDisplay = noteType.NoteTypeDisplay;
                    //titleNote.MarcDataFieldTag = noteType.MarcDataFieldTag;
                    //titleNote.MarcIndicator1 = noteType.MarcIndicator1;
                }
            }

            resourcesList.EditIndex = -1;
            bindExternalResourceData();
        }

        protected void resourcesList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            resourcesList.EditIndex = -1;
            bindExternalResourceData();
        }

        protected void resourcesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                resourcesList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];
                                
                TitleExternalResource externalResource = findTitleExternalResource(title.TitleExternalResources,
                    (int)resourcesList.DataKeys[rowNum].Values[0],
                    (int)resourcesList.DataKeys[rowNum].Values[1],
                    resourcesList.DataKeys[rowNum].Values[2].ToString(),
                    resourcesList.DataKeys[rowNum].Values[3].ToString());
                
                externalResource.IsDeleted = true;                
                bindExternalResourceData();
            }
        }

        protected void addResourceButton_Click(object sender, EventArgs e)
        {
            Title title = (Title)Session["Title" + idLabel.Text];
            TitleExternalResource externalResource = new TitleExternalResource();
            externalResource.TitleID = title.TitleID;
            externalResource.TitleExternalResourceTypeID = 0;
            externalResource.SequenceOrder = GetMaxExternalResourceSequence();
            externalResource.SequenceOrder++;
            title.TitleExternalResources.Add(externalResource);
            resourcesList.EditIndex = resourcesList.Rows.Count;
            bindExternalResourceData();
            resourcesList.Rows[resourcesList.EditIndex].FindControl("cancelResourceEditButton").Visible = false;
        }

        #endregion

        #region TitleNote event handlers

        protected void notesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            notesList.EditIndex = e.NewEditIndex;
            bindNotesData();
        }

        protected void notesList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = notesList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox txtNoteText = row.FindControl("txtNoteText") as TextBox;
                TextBox txtNoteSequence = row.FindControl("txtNoteSequence") as TextBox;
                DropDownList ddlNoteType = row.FindControl("ddlNoteType") as DropDownList;
                if (txtNoteText != null)
                {
                    short noteSequence = 0;
                    Title title = (Title)Session["Title" + idLabel.Text];

                    String noteText = txtNoteText.Text;
                    String noteSequenceText = txtNoteSequence.Text;
                    if (!short.TryParse(noteSequenceText, out noteSequence)) noteSequence = 0;
                    int? noteTypeID = Convert.ToInt32(ddlNoteType.Text);

                    TitleNote titleNote = findTitleNote(title.TitleNotes,
                        (int)notesList.DataKeys[e.RowIndex].Values[0],
                        (int)notesList.DataKeys[e.RowIndex].Values[1],
                        notesList.DataKeys[e.RowIndex].Values[2].ToString());

                    // Update the sequences of all notes if necessary
                    short oldNoteSeq = titleNote.NoteSequence ?? 0;

                    // If sequence has been decreased
                    if (noteSequence < oldNoteSeq)
                    {
                        // Increment all note sequences between the old and new sequence values
                        foreach (TitleNote note in title.TitleNotes)
                        {
                            if (note.NoteSequence >= noteSequence && note.NoteSequence < oldNoteSeq) note.NoteSequence++;
                        }
                    }

                    // If sequence has been increased
                    if (noteSequence > oldNoteSeq)
                    {
                        // Decrement all note sequences between the old and new sequence values
                        foreach (TitleNote note in title.TitleNotes)
                        {
                            if (note.NoteSequence <= noteSequence && note.NoteSequence > oldNoteSeq)
                            {
                                note.NoteSequence--;
                            }
                        }
                    }

                    // Update the note being edited
                    NoteType noteType = new BHLProvider().NoteTypeSelectAuto(Convert.ToInt32(ddlNoteType.SelectedValue));

                    titleNote.TitleID = title.TitleID;
                    titleNote.NoteTypeID = (noteTypeID == 0 ? null : noteTypeID);
                    titleNote.NoteText = noteText;
                    titleNote.NoteSequence = noteSequence;
                    titleNote.NoteTypeName = noteType.NoteTypeName;
                    titleNote.NoteTypeDisplay = noteType.NoteTypeDisplay;
                    titleNote.MarcDataFieldTag = noteType.MarcDataFieldTag;
                    titleNote.MarcIndicator1 = noteType.MarcIndicator1;
                }
            }

            notesList.EditIndex = -1;
            bindNotesData();        
        }

        protected void notesList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            notesList.EditIndex = -1;
            bindNotesData();
        }

        protected void notesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                notesList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Title title = (Title)Session["Title" + idLabel.Text];

                TitleNote titleNote = findTitleNote(title.TitleNotes,
                    (int)notesList.DataKeys[rowNum].Values[0],
                    (int)notesList.DataKeys[rowNum].Values[1],
                    notesList.DataKeys[rowNum].Values[2].ToString());

                titleNote.IsDeleted = true;
                bindNotesData();
            }
        }

        protected void addTitleNoteButton_Click(object sender, EventArgs e)
        {
            Title title = (Title)Session["Title" + idLabel.Text];
            TitleNote titleNote = new TitleNote();
            titleNote.TitleID = title.TitleID;
            titleNote.NoteTypeID = 0;
            titleNote.NoteSequence = GetMaxNoteSequence();
            titleNote.NoteSequence++;
            title.TitleNotes.Add(titleNote);
            notesList.EditIndex = notesList.Rows.Count;
            bindNotesData();
            notesList.Rows[notesList.EditIndex].FindControl("cancelTitleNoteButton").Visible = false;
        }

        #endregion TitleNote event handlers

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
            variantsList.Rows[variantsList.EditIndex].FindControl("cancelVariantButton").Visible = false;
        }

        protected void variantsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                variantsList.EditIndex = -1;
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
            identifiersList.Rows[identifiersList.EditIndex].FindControl("cancelTitleIdentifierButton").Visible = false;
        }

        protected void identifiersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                identifiersList.EditIndex = -1;
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
                associationsList.EditIndex = -1;
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
            int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

            Title title = (Title)Session["Title" + idLabel.Text];
            TitleLanguage tl = new TitleLanguage(0, title.TitleID, "", DateTime.Now, userId);
            title.TitleLanguages.Add(tl);
            languagesList.EditIndex = languagesList.Rows.Count;
            bindLanguageData();
            languagesList.Rows[languagesList.EditIndex].FindControl("cancelLanguageButton").Visible = false;
        }

        protected void languagesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                languagesList.EditIndex = -1;
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
            collectionsList.Rows[collectionsList.EditIndex].FindControl("cancelCollectionButton").Visible = false;
        }

		protected void collectionsList_RowCommand( object sender, GridViewCommandEventArgs e )
		{
			if ( e.CommandName.Equals( "RemoveButton" ) )
			{
                collectionsList.EditIndex = -1;
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
                itemsList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                int selectedItem = (int)itemsList.DataKeys[rowNum].Values[0];
                Title title = (Title)Session["Title" + idLabel.Text];
                foreach (ItemTitle itemTitle in title.ItemTitles)
                {
                    if (selectedItem == itemTitle.BookID)
                    {
                        if ((title.TitleID == itemTitle.PrimaryTitleID) && (!itemTitle.IsNew))
                        {
                            errorControl.AddErrorText("Cannot delete previously saved items for which this is the primary title.");
                            errorControl.Visible = true;
                            ResetScrollPosition();
                        }
                        else
                        {
                            itemTitle.IsDeleted = true;
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

					string itemIdString = row.Cells[ 2 ].Text;
					int itemId = 0;
					int.TryParse( itemIdString, out itemId );

					if ( newItemSeq > 0 && itemId > 0 )
					{
						// Find item being changed
						short? oldItemSeq = 0;
                        ItemTitle changedItem = null;

						foreach (ItemTitle item in title.ItemTitles)
						{
							if ( item.BookID == itemId && item.ItemSequence.HasValue )
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
                                foreach (ItemTitle item in title.ItemTitles)
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
                                foreach (ItemTitle item in title.ItemTitles)
                                {
                                    if (item.ItemSequence <= newItemSeq && item.ItemSequence > oldItemSeq)
                                    {
                                        item.ItemSequence--;
                                    }
                                }
                            }

                            // Change the old sequence value to the new sequence value
                            changedItem.ItemSequence = newItemSeq;
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
			ItemTitleComparer.CompareEnum sortColumn = _itemSortColumn;

			if ( e.SortExpression.Equals( "ItemID" ) )
			{
                _itemSortColumn = ItemTitleComparer.CompareEnum.ItemID;
			}
			else if ( e.SortExpression.Equals( "BarCode" ) )
			{
                _itemSortColumn = ItemTitleComparer.CompareEnum.BarCode;
			}
			else if ( e.SortExpression.Equals( "ItemSequence" ) )
			{
                _itemSortColumn = ItemTitleComparer.CompareEnum.ItemSequence;
			}
			else if ( e.SortExpression.Equals( "Volume" ) )
			{
                _itemSortColumn = ItemTitleComparer.CompareEnum.Volume;
			}

			if ( sortColumn == _itemSortColumn)
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

			ViewState[ "ItemSortColumn" ] = _itemSortColumn;
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

                int sortColumnIndex = 2;
				switch (_itemSortColumn)
				{
					case ItemTitleComparer.CompareEnum.BarCode:
						{
							sortColumnIndex = 4;
							break;
						}
					case ItemTitleComparer.CompareEnum.ItemSequence:
						{
							sortColumnIndex = 5;
							break;
						}
					case ItemTitleComparer.CompareEnum.Volume:
						{
							sortColumnIndex = 6;
							break;
						}
				}

				e.Row.Cells[ sortColumnIndex ].Controls.Add( new LiteralControl( " " ) );
				e.Row.Cells[ sortColumnIndex ].Controls.Add( img );
				e.Row.Cells[ sortColumnIndex ].Wrap = false;
			}
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ItemTitle item = (ItemTitle)e.Row.DataItem;
                if (item != null)
                {
                    Image flickrImage = (Image)e.Row.FindControl("FlickrImage");
                    if (item.HasFlickrImages)
                    {
                        //flickrImage.ImageUrl = "images/flickr_sml.png";
                        flickrImage.Visible = true;
                    }
                    else
                        flickrImage.Visible = false;
                }
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

                int? materialTypeID = null;
                if (ddlMaterialType.SelectedValue != "0") materialTypeID = (int?)Convert.ToInt32(ddlMaterialType.SelectedValue);

                // Set the id of the editing user
                var user = Helper.GetCurrentUserDetail(new HttpRequestWrapper(Request));
                userId = user.Id;

                //----------------------------------------
                // Gather up data on form
                title.RedirectTitleID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
                title.PublishReady = publishReadyCheckBox.Checked;
                title.BibliographicLevelID = bibLevelID;
                title.MaterialTypeID = materialTypeID;
				title.FullTitle = fullTitleTextBox.Text.Trim();
				title.ShortTitle = shortTitleTextBox.Text.Trim();
				title.SortTitle = sortTitleTextBox.Text.Trim();
				title.UniformTitle = uniformTitleTextBox.Text.Trim();
                title.PartNumber = partNumberTextBox.Text.Trim();
                title.PartName = partNameTextBox.Text.Trim();
				title.CallNumber = callNumberTextBox.Text.Trim();
				title.LanguageCode = ( ddlLang.SelectedValue.Length == 0 ? null : ddlLang.SelectedValue );
				title.TitleDescription = descTextBox.Text.Trim();
				title.PublicationDetails = publicationPlaceTextBox.Text.Trim() + (publicationPlaceTextBox.Text.Trim().Length > 0 ? " " : "") + 
                    publisherNameTextBox.Text.Trim() + (publisherNameTextBox.Text.Trim().Length > 0 ? " " : "") +
                    publicationDateTextBox.Text.Trim();
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

                //----------------------------------------
                // Forces deletes to happen first
                title.TitleCollections.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.TitleIdentifiers.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.TitleAuthors.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.ItemTitles.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.TitleKeywords.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.TitleExternalResources.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.TitleAssociations.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                title.TitleVariants.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));

                //----------------------------------------
                BHLProvider bp = new BHLProvider();
                // Don't catch errors... allow global error handler to take over
				bp.TitleSave( title, (int)userId,
                    string.Format("{0} {1} ({2})", user.FirstName, user.LastName, user.Email));

                // After a successful save operation, reload the title
                fillUI(title.TitleID);
            }
			else
			{
				return;
			}

            litMessage.Text = "<span class='liveData'>Title Saved.</span>";
            ResetScrollPosition();
		}

		#endregion

        private bool validate(Title title)
        {
            bool flag = false;

            // Check that all edits were completed
            if (creatorsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Creators has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (subjectsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Subjects has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (resourcesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("External Resources has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.\"");
            }

            if (notesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Notes has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.\"");
            }

            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (variantsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Variants has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (collectionsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Collections has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (itemsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Items has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
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

            foreach(TitleKeyword tk in title.TitleKeywords)
            {
                if (!tk.IsDeleted)
                {
                    if (string.IsNullOrWhiteSpace(tk.Keyword))
                    {
                        flag = true;
                        errorControl.AddErrorText("Keywords cannot be blank");
                        break;
                    }
                }
            }

            foreach(TitleExternalResource ter in title.TitleExternalResources)
            {
                if (!ter.IsDeleted)
                {
                    if (ter.TitleExternalResourceTypeID <= 0 || string.IsNullOrWhiteSpace(ter.UrlText))
                    {
                        flag = true;
                        errorControl.AddErrorText("External Resources must have a Type and Text.");
                        break;
                    }
                }
            }

            foreach(TitleNote tn in title.TitleNotes)
            {
                if (!tn.IsDeleted)
                {
                    if (tn.NoteTypeID <= 0 || string.IsNullOrWhiteSpace(tn.NoteText))
                    {
                        flag = true;
                        errorControl.AddErrorText("Notes must have a Type and Text.");
                        break;
                    }
                }
            }

            foreach(TitleLanguage tl in title.TitleLanguages)
            {
                if (!tl.IsDeleted)
                {
                    if (string.IsNullOrWhiteSpace(tl.LanguageCode))
                    {
                        flag = true;
                        errorControl.AddErrorText("Languages cannot be blank.");
                        break;
                    }
                }
            }

            foreach(TitleVariant tv in title.TitleVariants)
            {
                if (!tv.IsDeleted)
                {
                    if (tv.TitleVariantTypeID <= 0 || string.IsNullOrWhiteSpace(tv.Title))
                    {
                        flag = true;
                        errorControl.AddErrorText("Variants must have a Type and Title.");
                        break;
                    }
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

            // Validate identifiers
            bool blankID = false;
            foreach (Title_Identifier ti in title.TitleIdentifiers)
            {
                if (!ti.IsDeleted)
                {
                    if (ti.IdentifierID <= 0 || string.IsNullOrWhiteSpace(ti.IdentifierValue))
                    {
                        blankID = true;
                        flag = true;
                        errorControl.AddErrorText("Identifiers cannot be blank");
                    }
                }
            }

            if (!blankID)
            {
                IdentifierValidationResult identifierValidationResult = new BHLProvider().ValidateIdentifiers(title.TitleIdentifiers);
                if (!identifierValidationResult.IsValid)
                {
                    flag = true;
                    foreach (string message in identifierValidationResult.Messages) errorControl.AddErrorText(message);
                }
                if (identifierValidationResult.IncludesNewBHLDOI)
                {
                    flag = true;
                    errorControl.AddErrorText("A BHL-created DOI can only be added by submitting the Title metadata to a DOI registrar (such as Crossref)");
                }
            }

            errorControl.Visible = flag;
            if (flag) ResetScrollPosition();

            return !flag;
        }

        private void ResetScrollPosition()
        {
            if (!ClientScript.IsClientScriptBlockRegistered(GetType(), "CreateResetScrollPosition")) {
                // Create the ResetScrollPosition() function
                ClientScript.RegisterClientScriptBlock(GetType(), "CreateResetScrollPosition",
                        "function ResetScrollPosition() {\r\n" +
                        " var scrollX = document.getElementById('__SCROLLPOSITIONX');\r\n" +
                        " var scrollY = document.getElementById('__SCROLLPOSITIONY');\r\n" +
                        " if (scrollX && scrollY) {\r\n" +
                        "    scrollX.value = 0;\r\n" +
                        "    scrollY.value = 0;\r\n" +
                        " }\r\n" +
                        "}", true);

                // Add the call to the ResetScrollPosition() function
                ClientScript.RegisterStartupScript(GetType(), "CallResetScrollPosition", "ResetScrollPosition();", true);
            }
        }
    }
}
