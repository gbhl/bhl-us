using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ItemEdit : System.Web.UI.Page
	{
		private PageComparer.CompareEnum _sortColumn = PageComparer.CompareEnum.SequenceOrder;
		private SortOrder _sortOrder = SortOrder.Ascending;

        protected void Page_Load( object sender, EventArgs e )
		{
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectTitle", "<script language='javascript'>function selectTitle(titleId) { document.getElementById('" + selectedTitle.ClientID + "').value=titleId; overlay(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectSegment", "<script language='javascript'>function selectSegment(segmentId) { document.getElementById('" + selectedSegment.ClientID + "').value=segmentId; overlay(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearSegment", "<script language='javascript'>function clearSegment() { document.getElementById('" + selectedSegment.ClientID + "').value=''; __doPostBack('', '');}</script>");

			if ( !IsPostBack )
			{
				fillCombos();

				string idString = Request.QueryString["id"];
                string sid = Request.QueryString["sid"];

                int id = 0;
				if ( idString != null && int.TryParse( idString, out id ) )
				{
					itemIdTextBox.Text = id.ToString();
					search( id, null );
				}
                else if (sid != null)
                {
                    sourceIDTextBox.Text = sid;
                    search(null, sid);
                }
			}
			else
			{
                String selectedTitleId = this.selectedTitle.Value;
                if (selectedTitleId != "")
                {
                    List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + itemIdLabel.Text];
                    DataObjects.ItemTitle itemTitle = new DataObjects.ItemTitle();

                    // Get details for "selectedTitleId" from database
                    BHLProvider provider = new BHLProvider();
                    DataObjects.Title title = provider.TitleSelect(Convert.ToInt32(selectedTitleId));
                    itemTitle.TitleID = title.TitleID;
                    itemTitle.ShortTitle = title.ShortTitle;
                    itemTitle.TitlePublishReady = title.PublishReady;
                    itemTitle.IsPrimary = 0;
                    itemTitles.Add(itemTitle);
                    Session["ItemTitleList" + itemIdLabel.Text] = itemTitles;
                    this.selectedTitle.Value = "";
                    this.bindTitleData();
                }

                String selectedSegmentId = this.selectedSegment.Value;
                if (selectedSegmentId != "")
                {
                    Book item = (Book)Session["Item" + itemIdLabel.Text];

                    // Make sure the selected segment isn't already associated with this item
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                    bool segmentExists = false;
                    foreach (Segment existingSegment in item.Segments)
                    {
                        if (existingSegment.SegmentID.ToString() == selectedSegmentId)
                        {
                            // Re-eneable this segment, if necessary
                            if (existingSegment.BookID == null)
                            {
                                existingSegment.ItemID = item.ItemID;

                                // Adjust the sequence order of all segments "after" the one being re-enabled
                                foreach (Segment segment in item.Segments)
                                {
                                    if (segment.SequenceOrder >= existingSegment.SequenceOrder && 
                                        segment.SegmentID != existingSegment.SegmentID)
                                    {
                                        segment.SequenceOrder++;
                                        segment.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            segmentExists = true;
                            break;
                        }
                    }

                    if (!segmentExists)
                    {
                        // Get details for "selectedSegmentId" from database
                        Segment segment = new BHLProvider().SegmentSelectForSegmentID(Convert.ToInt32(selectedSegmentId));
                        segment.BookID = item.BookID;
                        segment.SequenceOrder = (short)(segmentsList.Rows.Count + 1);
                        item.Segments.Add(segment);
                    }

                    Session["Item" + item.BookID.ToString()] = item;
                    this.selectedSegment.Value = "";
                    this.bindSegmentData();
                }

				if ( ViewState[ "SortColumn" ] != null )
				{
					_sortColumn = (PageComparer.CompareEnum)ViewState[ "SortColumn" ];
					_sortOrder = (SortOrder)ViewState[ "SortOrder" ];
				}
			}

            litMessage.Text = "";
            errorControl.Visible = false;
            inactiveTitleWarning.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;

			Page.SetFocus( itemIdTextBox );
		}

		private void fillCombos()
		{
			BHLProvider bp = new BHLProvider();
            List<Institution> institutions = bp.InstituationSelectAll();

            ddlInst.DataSource = institutions;
            ddlInst.DataTextField = "InstitutionName";
            ddlInst.DataValueField = "InstitutionCode";
            ddlInst.DataBind();
            ddlInst.SelectedValue = "UNKNOWN";

            Institution emptyInstitution = new Institution();
            emptyInstitution.InstitutionCode = string.Empty;
            emptyInstitution.InstitutionName = string.Empty;
            institutions.Insert(0, emptyInstitution);

            ddlScanningInstitution.DataSource = institutions;
            ddlScanningInstitution.DataTextField = "InstitutionName";
            ddlScanningInstitution.DataValueField = "InstitutionCode";
            ddlScanningInstitution.DataBind();

            ddlRights.DataSource = institutions;
            ddlRights.DataTextField = "InstitutionName";
            ddlRights.DataValueField = "InstitutionCode";
            ddlRights.DataBind();

            List<Language> languages = bp.LanguageSelectAll();

			Language emptyLanguage = new Language();
			emptyLanguage.LanguageCode = "";
			emptyLanguage.LanguageName = "";
			languages.Insert( 0, emptyLanguage );

			ddlLang.DataSource = languages;
			ddlLang.DataTextField = "LanguageName";
			ddlLang.DataValueField = "LanguageCode";
			ddlLang.DataBind();

            List<Vault> vaults = bp.VaultSelectAll();

			Vault emptyVault = new Vault();
			emptyVault.VaultID = 0;
			emptyVault.Description = "";
			vaults.Insert( 0, emptyVault );

			ddlVault.DataSource = vaults;
			ddlVault.DataTextField = "Description";
			ddlVault.DataValueField = "VaultID";
			ddlVault.DataBind();

            List<ItemStatus> itemStatuses = bp.ItemStatusSelectAll();

			ddlItemStatus.DataSource = itemStatuses;
			ddlItemStatus.DataTextField = "ItemStatusName";
			ddlItemStatus.DataValueField = "ItemStatusID";
			ddlItemStatus.DataBind();
		}

		private void fillUI(string bookID)
		{
            Book book = (Book)Session["Item" + bookID];

            if (book != null)
            {

                itemIdLabel.Text = book.BookID.ToString();
                FlickrImage.Visible = book.HasFlickrImages;
                sourceLabel.Text = book.SourceName;
                sourceIDLabel.Text = book.BarCode;
                hidMarcItemID.Value = book.MARCItemID;
                hidVirtualVolumeKey.Value = book.VirtualVolumeKey;
                callNumberTextBox.Text = book.CallNumber;
                volumeTextBox.Text = book.Volume;
                itemDescriptionTextBox.Text = book.ItemDescription;
                notesTextBox.Text = book.Note;
                replacedByTextBox.Text = book.RedirectBookID.ToString();
                replacedByOrig.Value = book.RedirectBookID.ToString();
                startYearTextBox.Text = book.StartYear;
                endYearTextBox.Text = book.EndYear;
                identifierBibTextBox.Text = book.IdentifierBib;
                zQueryTextBox.Text = book.ZQuery;
                sponsorTextBox.Text = book.Sponsor;
                licenseUrlTextBox.Text = book.LicenseUrl;
                rightsTextBox.Text = book.Rights;
                dueDiligenceTextBox.Text = book.DueDiligence;
                ddlCopyrightIndicator.SelectedValue = book.CopyrightIndicator;
                copyrightStatusTextBox.Text = book.CopyrightStatus;
                copyrightRegionTextBox.Text = book.CopyrightRegion;
                copyrightCommentTextBox.Text = book.CopyrightComment;
                copyrightEvidenceTextBox.Text = book.CopyrightEvidence;
                externalUrlTextBox.Text = book.ExternalUrl;

                editHistoryControl.EntityName = "item";
                editHistoryControl.EntityId = book.BookID.ToString();

                List<DataObjects.ItemTitle> itemTitles = new List<DataObjects.ItemTitle>();
                foreach (DataObjects.ItemTitle it in book.ItemTitles)
                {
                    DataObjects.ItemTitle itemTitle = new DataObjects.ItemTitle();
                    itemTitle.TitleID = it.TitleID;
                    itemTitle.ShortTitle = it.ShortTitle;
                    itemTitle.IsPrimary = (short)(book.PrimaryTitleID == it.TitleID ? 1 : 0);
                    itemTitle.TitlePublishReady = it.TitlePublishReady;
                    itemTitles.Add(itemTitle);
                }
                Session["ItemTitleList" + bookID] = itemTitles;

                titleList.DataSource = itemTitles;// item.Titles;
                titleList.DataBind();

                languagesList.DataSource = book.ItemLanguages;
                languagesList.DataBind();

                collectionsList.DataSource = book.ItemCollections;
                collectionsList.DataBind();

                segmentsList.DataSource = book.Segments;
                segmentsList.DataBind();

                isVirtualLabel.Text = (book.IsVirtual == 1) ? "True" : "False";

                scannedByLabel.Text = book.ScanningUser;
                scannedDateLabel.Text = (book.ScanningDate.HasValue ? book.ScanningDate.Value.ToShortDateString() : "");
                creationDateLabel.Text = (book.CreationDate.HasValue ? book.CreationDate.Value.ToShortDateString() : "");
                lastModifiedDateLabel.Text =
                    (book.LastModifiedDate.HasValue ? book.LastModifiedDate.Value.ToShortDateString() : "");

                ddlInst.SelectedValue = "UNKNOWN";
                foreach (Institution contributor in book.Institutions)
                {
                    if (contributor.InstitutionRoleName == InstitutionRole.HoldingInstitution)
                    {
                        ddlInst.SelectedValue = contributor.InstitutionCode;
                        break;
                    }
                }

                ddlRights.SelectedValue = "";
                foreach (Institution contributor in book.Institutions)
                {
                    if (contributor.InstitutionRoleName == InstitutionRole.RightsHolder)
                    {
                        ddlRights.SelectedValue = contributor.InstitutionCode;
                        break;
                    }
                }

                ddlScanningInstitution.SelectedValue = "";
                foreach (Institution contributor in book.Institutions)
                {
                    if (contributor.InstitutionRoleName == InstitutionRole.ScanningInstitution)
                    {
                        ddlScanningInstitution.SelectedValue = contributor.InstitutionCode;
                        break;
                    }
                }

                // If necesssary, populate the thumbnail pages dropdown
                if (ddlThumbnailPageID.Items.Count == 0)
                {
                    ddlThumbnailPageID.Items.Add(new ListItem("(use default)", ""));
                    foreach(DataObjects.Page page in book.Pages)
                    {
                        if (page.Active)
                        {
                            ListItem li = new ListItem(
                                string.Format("{0} ({1})", page.PageID.ToString(), page.WebDisplay),
                                page.PageID.ToString()
                                );
                            ddlThumbnailPageID.Items.Add(li);
                        }
                    }
                }

                ddlThumbnailPageID.SelectedValue = "";
                if (book.ThumbnailPageID.HasValue)
                {
                    ddlThumbnailPageID.SelectedValue = book.ThumbnailPageID.Value.ToString();
                }
                else
                {
                    ddlThumbnailPageID.SelectedIndex = 0;
                }

                ddlPageProgression.SelectedValue = "";
                if (!string.IsNullOrWhiteSpace(book.PageProgression))
                    ddlPageProgression.SelectedValue = book.PageProgression;
                else
                    ddlPageProgression.SelectedIndex = 0;

                if (book.LanguageCode != null && book.LanguageCode.Length > 0)
                {
                    ddlLang.SelectedValue = book.LanguageCode.ToUpper();
                }
                else
                {
                    ddlLang.SelectedIndex = 0;
                }

                if (book.VaultID.HasValue)
                {
                    ddlVault.SelectedValue = book.VaultID.Value.ToString();
                }
                else
                {
                    ddlVault.SelectedIndex = 0;
                }

                if (book.ItemStatusID > 0)
                {
                    ddlItemStatus.SelectedValue = book.ItemStatusID.ToString();
                }
                else
                {
                    ddlItemStatus.SelectedIndex = 0;
                }
                itemStatusOrig.Value = book.ItemStatusID.ToString();

                if (book.Pages.Count > 0)
                {
                    pageFieldSet.Visible = true;
                    pageList.DataSource = book.Pages;
                    pageList.DataBind();
                }
                else
                {
                    pageFieldSet.Visible = false;
                }


                // See if we can display a link to the MARC file
                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                if (!string.IsNullOrWhiteSpace(client.GetMarcFile(book.BookID, "i")))
                {
                    hypMarc.Attributes["onclick"] = string.Format("javascript:window.open('TitleItemMarc.aspx?type=i&id={0}', '', 'width=600,height=600,location=0,status=0,scrollbars=1');", book.BookID.ToString());
                    hypMarc.Visible = true;                        
                }
            }
		}

		private void search( int? id, string barcode )
		{
			BHLProvider bp = new BHLProvider();
			Book book = bp.BookSelectByBarcodeOrItemID( id, barcode );

            string bookID = string.Empty;
            if (book != null)
            {
                // Look up flickr status of the item
                DataObjects.Item flickrItem = bp.ItemInFlickrByItemID(book.BookID);
                book.HasFlickrImages = (flickrItem != null) ? flickrItem.HasFlickrImages : false;
                bookID = book.BookID.ToString();
            }
            else
            {
                litMessage.Text = "Item not found";
                litMessage.Visible = true;
            }

            // Clear the thumbnail pages dropdown
            ddlThumbnailPageID.Items.Clear();

            Session["Item" + bookID] = book;
			fillUI(bookID);
		}

		private void bindPageData()
		{
            Book item = (Book)Session["Item" + itemIdLabel.Text];
            item.Pages.Sort((p1, p2) => (p1.SequenceOrder ?? 0).CompareTo(p2.SequenceOrder ?? 0));
            switch (_sortColumn)
            {
                case PageComparer.CompareEnum.FileNamePrefix:
                    if (_sortOrder == SortOrder.Ascending)
                        item.Pages.Sort((p1, p2) => (p1.FileNamePrefix).CompareTo(p2.FileNamePrefix));
                    else
                        item.Pages.Sort((p1, p2) => (p2.FileNamePrefix).CompareTo(p1.FileNamePrefix));
                    break;
                case PageComparer.CompareEnum.PageID:
                    if (_sortOrder == SortOrder.Ascending)
                        item.Pages.Sort((p1, p2) => (p1.PageID).CompareTo(p2.PageID));
                    else
                        item.Pages.Sort((p1, p2) => (p2.PageID).CompareTo(p1.PageID));
                    break;
                case PageComparer.CompareEnum.SequenceOrder:
                    if (_sortOrder == SortOrder.Ascending)
                        item.Pages.Sort((p1, p2) => (p1.SequenceOrder ?? 0).CompareTo(p2.SequenceOrder ?? 0));
                    else
                        item.Pages.Sort((p1, p2) => (p2.SequenceOrder ?? 0).CompareTo(p1.SequenceOrder ?? 0));
                    break;
            }
            pageList.DataSource = item.Pages;
            pageList.DataBind();
		}

        private void bindTitleData()
        {
            List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + itemIdLabel.Text];
            titleList.DataSource = itemTitles;
            titleList.DataBind();
        }

        private bool validate(Book book)
		{
			bool flag = false;

            // Make sure that one and only one title is designated as the primary title.
            int primaryTitleId = 0;
            int numPrimary = 0;
            List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + book.BookID.ToString()];
            foreach (DataObjects.ItemTitle itemTitle in itemTitles)
            {
                if (itemTitle.IsPrimary == 1)
                {
                    numPrimary++;
                    primaryTitleId = itemTitle.TitleID;
                }
            }
            if (numPrimary == 1)
            {
                book.PrimaryTitleID = primaryTitleId;
            }
            else
            {
                flag = true;
                errorControl.AddErrorText("One and only one title must be designated the Primary title for this item.");
            }

            if (startYearTextBox.Text.Trim().Length > 0)
            {
                if (DataCleaner.ValidateItemSimpleYear(startYearTextBox.Text.Trim().Replace(" ", "")))
                {
                    startYearTextBox.Text = startYearTextBox.Text.Trim().Replace(" ", "");   // Remove spaces
                }
                else
                {
                    flag = true;
                    errorControl.AddErrorText("Start Year must be formatted as 'YYYY'.");
                }
            }

            if (endYearTextBox.Text.Trim().Length > 0)
            {
                if (DataCleaner.ValidateItemSimpleYear(endYearTextBox.Text.Trim().Replace(" ", "")))
                {
                    endYearTextBox.Text = endYearTextBox.Text.Trim().Replace(" ", "");   // Remove spaces
                }
                else
                {
                    flag = true;
                    errorControl.AddErrorText("End Year must be formatted as 'YYYY'");
                }
            }

            // If a "replaced by" identifer was specified, make sure that it is a valid id
            if (replacedByTextBox.Text.Trim().Length > 0)
            {
                int itemID;
                if (Int32.TryParse(replacedByTextBox.Text, out itemID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().BookSelectAuto(itemID) == null)
                    {
                        flag = true;
                        errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Item ID.");
                    } 
                }
                else
                {
                    // Specified ID is not a valid integer value
                    flag = true;
                    errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Item ID.");
                }
            }

			// Check that all edits were completed
            if ( titleList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Titles has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (languagesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Languages has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (collectionsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Collections has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (segmentsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Segments has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            foreach (ItemLanguage il in book.ItemLanguages)
            {
                if (!il.IsDeleted)
                {
                    if (string.IsNullOrWhiteSpace(il.LanguageCode))
                    {
                        flag = true;
                        errorControl.AddErrorText("Languages cannot be blank.");
                        break;
                    }
                }
            }

            bool br = false;
            int ix = 0;
            foreach (ItemCollection ic in book.ItemCollections)
            {
                if (ic.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (ItemCollection ic2 in book.ItemCollections)
                    {
                        if (ic2.IsDeleted == false)
                        {
                            if ((ic.ItemCollectionID != ic2.ItemCollectionID && ic.CollectionID == ic2.CollectionID) ||
                                (ic.ItemCollectionID == 0 && ic2.ItemCollectionID == 0 && ic.CollectionID == ic2.CollectionID &&
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

			errorControl.Visible = flag;
            if (flag) ResetScrollPosition();

			return !flag;
		}

        #region ItemLanguage methods

        private void bindLanguageData()
        {
            Book item = (Book)Session["Item" + itemIdLabel.Text];

            // filter out deleted items
            List<ItemLanguage> itemLanguages = new List<ItemLanguage>();
            foreach (ItemLanguage il in item.ItemLanguages)
            {
                if (il.IsDeleted == false)
                {
                    itemLanguages.Add(il);
                }
            }

            languagesList.DataSource = itemLanguages;
            languagesList.DataBind();
        }

        List<Language> _itemLanguages = null;
        protected List<Language> GetLanguages()
        {
            BHLProvider bp = new BHLProvider();
            _itemLanguages = bp.LanguageSelectAll();

            return _itemLanguages;
        }

        protected int GetLanguageIndex(object dataItem)
        {
            String languageCode = DataBinder.Eval(dataItem, "LanguageCode").ToString();

            int ix = 0;
            foreach (Language language in _itemLanguages)
            {
                if (language.LanguageCode == languageCode)
                {
                    return ix;
                }
                ix++;
            }

            return 0;
        }

        private ItemLanguage findItemLanguage(List<ItemLanguage> itemLanguages,
            int itemLanguageId, string languageCode)
        {
            foreach (ItemLanguage il in itemLanguages)
            {
                if (il.IsDeleted)
                {
                    continue;
                }
                if (itemLanguageId == 0 && il.ItemLanguageID == 0 && languageCode == il.LanguageCode)
                {
                    return il;
                }
                else if (itemLanguageId > 0 && il.ItemLanguageID == itemLanguageId)
                {
                    return il;
                }
            }

            return null;
        }

        #endregion ItemLanguage methods

        #region Collection methods

        private void bindCollectionData()
        {
            Book item = (Book)Session["Item" + itemIdLabel.Text];

            // filter out deleted items
            List<ItemCollection> itemCollections = new List<ItemCollection>();
            foreach (ItemCollection ic in item.ItemCollections)
            {
                if (ic.IsDeleted == false)
                {
                    itemCollections.Add(ic);
                }
            }

            collectionsList.DataSource = itemCollections;
            collectionsList.DataBind();
        }

        List<Collection> _itemCollections = null;
        protected List<Collection> GetCollections()
        {
            BHLProvider bp = new BHLProvider();
            // Select collections that may contain items
            _itemCollections = bp.CollectionSelectByContents(0, 1);

            return _itemCollections;
        }

        protected int GetCollectionIndex(object dataItem)
        {
            string collectionIdString = DataBinder.Eval(dataItem, "CollectionID").ToString();

            if (!collectionIdString.Equals("0"))
            {
                int collectionId = int.Parse(collectionIdString);
                int ix = 0;
                foreach (Collection itemCollection in _itemCollections)
                {
                    if (itemCollection.CollectionID == collectionId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        private ItemCollection findCollection(List<ItemCollection> collections, int itemCollectionId,
            int collectionId)
        {
            foreach (ItemCollection ic in collections)
            {
                if (ic.IsDeleted)
                {
                    continue;
                }
                if (itemCollectionId == 0 && ic.ItemCollectionID == 0 && collectionId == ic.CollectionID)
                {
                    return ic;
                }
                else if (itemCollectionId > 0 && ic.ItemCollectionID == itemCollectionId)
                {
                    return ic;
                }
            }

            return null;
        }

        #endregion Collection methods

        #region Segment methods

        private void bindSegmentData()
        {
            Book item = (Book)Session["Item" + itemIdLabel.Text];

            // filter out deleted segments
            List<Segment> segments = new List<Segment>();
            foreach (Segment s in item.Segments)
            {
                if (s.BookID != null) segments.Add(s);
            }

            SegmentSequenceComparer comp = new SegmentSequenceComparer();
            segments.Sort(comp);
            segmentsList.DataSource = segments;
            segmentsList.DataBind();
        }

        private Segment findSegment(List<Segment> segments, int segmentId)
        {
            foreach (Segment s in segments)
            {
                if (s.ItemID <= 0)
                {
                    continue;
                }
                else if (segmentId > 0 && s.SegmentID == segmentId)
                {
                    return s;
                }
            }

            return null;
        }

        #endregion Segment methods

		#region Event handlers

        #region Title event handlers

        protected void titleList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            titleList.EditIndex = e.NewEditIndex;
            bindTitleData();
        }

        protected void titleList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        { 
            GridViewRow row = titleList.Rows[e.RowIndex];

            if (row != null)
            {
                CheckBox checkBox = row.FindControl("isPrimaryCheckBoxEdit") as CheckBox;
                if (checkBox != null)
                {
                    List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + itemIdLabel.Text];
                    bool isPrimary = checkBox.Checked;

                    String titleIdString = row.Cells[2].Text;
                    int titleId = 0;
                    int.TryParse(titleIdString, out titleId);

                    if (titleId > 0)
                    {
                        // Update primary title
                        foreach (DataObjects.ItemTitle itemTitle in itemTitles)
                        {
                            if (titleId == itemTitle.TitleID)
                            {
                                itemTitle.IsPrimary = (short)(isPrimary ? 1 : 0);
                                break;
                            }
                        }
                    }

                    // If the Primary title is not published, show a warning message
                    bool inactiveTitle = true;
                    int numTitles = 0;
                    int numPrimary = 0;
                    foreach (DataObjects.ItemTitle itemTitle in itemTitles)
                    {
                        if (!itemTitle.IsDeleted)
                        {
                            numTitles++;
                            if (itemTitle.IsPrimary == 1)
                            {
                                numPrimary++;
                                if (itemTitle.TitlePublishReady)
                                {
                                    inactiveTitle = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (numTitles == 0 || numPrimary == 0) inactiveTitle = false;
                    inactiveTitleWarning.Visible = inactiveTitle;
                }
            }

            titleList.EditIndex = -1;
            bindTitleData();
        }

        protected void titleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                titleList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                int selectedTitle = (int)titleList.DataKeys[rowNum].Values[0];
                List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + itemIdLabel.Text];

                for (int i = 0; i < itemTitles.Count; i++)
                {
                    if (itemTitles[i].TitleID == selectedTitle)
                    {
                        itemTitles.RemoveAt(i);
                        break;
                    }
                }

                bindTitleData();
            }
        }

        protected void titleList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            titleList.EditIndex = -1;
            bindTitleData();
        }

        #endregion Title event handlers

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
                    Book item = (Book)Session["Item" + itemIdLabel.Text];
                    String languageCode = ddlLanguageName.SelectedValue;

                    ItemLanguage itemLanguage = findItemLanguage(item.ItemLanguages,
                    (int)languagesList.DataKeys[e.RowIndex].Values[0],
                    languagesList.DataKeys[e.RowIndex].Values[1].ToString());

                    itemLanguage.LanguageCode = languageCode;
                    itemLanguage.LanguageName = ddlLanguageName.SelectedItem.Text;
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

            Book item = (Book)Session["Item" + itemIdLabel.Text];
            ItemLanguage il = new ItemLanguage(0, item.ItemID, "", DateTime.Now, userId);
            item.ItemLanguages.Add(il);
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
                Book item = (Book)Session["Item" + itemIdLabel.Text];

                ItemLanguage itemLanguage = findItemLanguage(item.ItemLanguages,
                    (int)languagesList.DataKeys[rowNum].Values[0],
                    languagesList.DataKeys[rowNum].Values[1].ToString());

                itemLanguage.IsDeleted = true;
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

        protected void collectionsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = collectionsList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlCollection = row.FindControl("ddlCollection") as DropDownList;
                if (ddlCollection != null)
                {
                    Book item = (Book)Session["Item" + itemIdLabel.Text];
                    int collectionId = int.Parse(ddlCollection.SelectedValue);

                    ItemCollection itemCollection = findCollection(item.ItemCollections,
                        (int)collectionsList.DataKeys[e.RowIndex].Values[0],
                        (int)collectionsList.DataKeys[e.RowIndex].Values[1]);

                    itemCollection.CollectionID = collectionId;
                    itemCollection.CollectionName = ddlCollection.SelectedItem.Text;
                }
            }

            collectionsList.EditIndex = -1;
            bindCollectionData();
        }

        protected void collectionsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            collectionsList.EditIndex = -1;
            bindCollectionData();
        }

        protected void addCollectionButton_Click(object sender, EventArgs e)
        {
            Book item = (Book)Session["Item" + itemIdLabel.Text];
            ItemCollection ic = new ItemCollection(0, item.ItemID, 0);
            item.ItemCollections.Add(ic);
            collectionsList.EditIndex = collectionsList.Rows.Count;
            bindCollectionData();
            collectionsList.Rows[collectionsList.EditIndex].FindControl("cancelCollectionButton").Visible = false;
        }

        protected void collectionsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                collectionsList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Book item = (Book)Session["Item" + itemIdLabel.Text];

                ItemCollection collection = findCollection(item.ItemCollections,
                    (int)collectionsList.DataKeys[rowNum].Values[0],
                    (int)collectionsList.DataKeys[rowNum].Values[1]);

                collection.IsDeleted = true;
                bindCollectionData();
            }
        }

        #endregion Collection event handlers

        #region Segment event handlers

        protected void segmentsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            segmentsList.EditIndex = e.NewEditIndex;
            bindSegmentData();
        }

        protected void segmentsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = segmentsList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox sequenceTextBox = row.FindControl("segmentSequenceTextBox") as TextBox;
                if (sequenceTextBox != null)
                {
                    Book item = (Book)Session["Item" + itemIdLabel.Text];

                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        // Find segment being changed
                        Segment changedSegment = findSegment(item.Segments,
                            (int)segmentsList.DataKeys[e.RowIndex].Values[0]);

                        int oldSeq = (int)changedSegment.SequenceOrder;

                        if (changedSegment != null)
                        {
                            // If sequence has been decreased
                            if (newSeq < oldSeq)
                            {
                                // Increment all sequences between the old and new sequence values
                                foreach (Segment segment in item.Segments)
                                {
                                    if (segment.SequenceOrder >= newSeq && segment.SequenceOrder < oldSeq)
                                    {
                                        segment.SequenceOrder++;
                                        segment.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            // If sequence has been increased
                            if (newSeq > oldSeq)
                            {
                                // Decrement all sequences between the old and new sequence values
                                foreach (Segment segment in item.Segments)
                                {
                                    if (segment.SequenceOrder <= newSeq && segment.SequenceOrder > oldSeq)
                                    {
                                        segment.SequenceOrder--;
                                        segment.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            changedSegment.SequenceOrder = newSeq;
                            changedSegment.LastModifiedUserID = userId;
                        }
                    }
                }
            }

            segmentsList.EditIndex = -1;
            bindSegmentData();
        }

        protected void segmentsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            segmentsList.EditIndex = -1;
            bindSegmentData();
        }

        protected void segmentsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                segmentsList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Book item = (Book)Session["Item" + itemIdLabel.Text];

                Segment currentSegment = findSegment(item.Segments,
                    (int)segmentsList.DataKeys[rowNum].Values[0]);

                currentSegment.BookID = null;

                // Adjust the sequence order of all segments "after" the one being "deleted"
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                foreach (Segment segment in item.Segments)
                {
                    if (segment.SequenceOrder > currentSegment.SequenceOrder)
                    {
                        segment.SequenceOrder--;
                        segment.LastModifiedUserID = userId;
                    }
                }
                Session["Item" + item.BookID.ToString()] = item;

                bindSegmentData();
            }
        }

        #endregion Segment event handlers

        #region Page event handlers

        protected void pageList_RowEditing( object sender, GridViewEditEventArgs e )
		{
		}

		protected void pageList_RowUpdating( object sender, GridViewUpdateEventArgs e )
		{
		}

		protected void pageList_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
		{
		}

		protected void pageList_Sorting( object sender, GridViewSortEventArgs e )
		{
			PageComparer.CompareEnum sortColumn = _sortColumn;

			if ( e.SortExpression.Equals( "PageID" ) )
			{
				_sortColumn = PageComparer.CompareEnum.PageID;
			}
			else if ( e.SortExpression.Equals( "FileNamePrefix" ) )
			{
				_sortColumn = PageComparer.CompareEnum.FileNamePrefix;
			}
			else if ( e.SortExpression.Equals( "SequenceOrder" ) )
			{
				_sortColumn = PageComparer.CompareEnum.SequenceOrder;
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

			bindPageData();
		}

		protected void pageList_RowDataBound( object sender, GridViewRowEventArgs e )
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

				int sortColumnIndex = 0;
				switch ( _sortColumn )
				{
					case PageComparer.CompareEnum.PageID:
						{
							sortColumnIndex = 1;
							break;
						}
					case PageComparer.CompareEnum.FileNamePrefix:
						{
							sortColumnIndex = 2;
							break;
						}
					case PageComparer.CompareEnum.SequenceOrder:
						{
							sortColumnIndex = 3;
							break;
						}
				}

				e.Row.Cells[ sortColumnIndex ].Controls.Add( new LiteralControl( " " ) );
				e.Row.Cells[ sortColumnIndex ].Controls.Add( img );
				e.Row.Cells[ sortColumnIndex ].Wrap = false;
			}
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BHL.DataObjects.Page page = (BHL.DataObjects.Page)e.Row.DataItem;
                ImageButton flickrLinkButton = (ImageButton)e.Row.FindControl("FlickrLinkButton");
                if (page.FlickrURL.Length == 0)
                    flickrLinkButton.Visible = false;
                else
                {
                    flickrLinkButton.ImageUrl = "images/flickr_sml.png";
                    flickrLinkButton.Attributes.Add("onclick", "window.open('" + page.FlickrURL + "');return false");
                }
            }
        }

        #endregion Page event handlers

        protected void searchButton_Click(object sender, EventArgs e)
        {
            int itemId = 0;
            if (int.TryParse(itemIdTextBox.Text.Trim(), out itemId))
            {
                search(itemId, null);
            }
            else if (sourceIDTextBox.Text.Trim().Length > 0)
            {
                search(null, sourceIDTextBox.Text.Trim());
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
		{
            Book book = (Book)Session["Item" + itemIdLabel.Text];

			if ( validate( book ) )
			{
                int? i = null;
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                // Gather up data on form
                book.RedirectBookID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
				book.MARCItemID = hidMarcItemID.Value;
                book.VirtualVolumeKey = hidVirtualVolumeKey.Value;
				book.CallNumber = callNumberTextBox.Text.Trim();
				book.Volume = volumeTextBox.Text.Trim();
				book.LanguageCode = ( ddlLang.SelectedValue.Length == 0 ? null : ddlLang.SelectedValue );
                book.ItemDescription = itemDescriptionTextBox.Text.Trim();
				book.Note = notesTextBox.Text.Trim();
                book.StartYear = startYearTextBox.Text.Trim();
                book.EndYear = endYearTextBox.Text.Trim();
                book.IdentifierBib = identifierBibTextBox.Text.Trim();
                book.ZQuery = zQueryTextBox.Text.Trim();
                book.Sponsor = sponsorTextBox.Text.Trim();
                book.LicenseUrl = licenseUrlTextBox.Text.Trim();
                book.Rights = rightsTextBox.Text.Trim();
                book.DueDiligence = dueDiligenceTextBox.Text.Trim();
                book.CopyrightIndicator = ddlCopyrightIndicator.SelectedValue;
                book.CopyrightStatus = copyrightStatusTextBox.Text.Trim();
                book.CopyrightRegion = copyrightRegionTextBox.Text.Trim();
                book.CopyrightComment = copyrightCommentTextBox.Text.Trim();
                book.CopyrightEvidence = copyrightEvidenceTextBox.Text.Trim();
                book.ExternalUrl = externalUrlTextBox.Text.Trim();
				book.VaultID = ( ddlVault.SelectedIndex == 0 ? i : int.Parse(ddlVault.SelectedValue) );
				book.ItemStatusID = int.Parse( ddlItemStatus.SelectedValue );
                book.ThumbnailPageID = ddlThumbnailPageID.SelectedValue == "" ? (int?)null : int.Parse(ddlThumbnailPageID.SelectedValue);
                book.PageProgression = ddlPageProgression.SelectedValue;

				book.IsNew = false;

                //----------------------------------------

                // Update the Item information
                if (book.Item != null)
                {
                    bool updated = false;
                    if (book.ItemStatusID != book.Item.ItemStatusID) { book.Item.ItemStatusID = book.ItemStatusID; updated = true; }
                    if (book.VaultID != book.Item.VaultID) { book.Item.VaultID = book.VaultID; updated = true; }
                    if (book.ItemDescription.CompareTo(book.Item.ItemDescription) != 0) { book.Item.ItemDescription = book.ItemDescription; updated = true; }
                    if (book.Note.CompareTo(book.Item.Note) != 0) { book.Item.Note = book.Note; updated = true; }
                    if (updated) book.Item.LastModifiedDate = DateTime.Now;
                }

                //----------------------------------------

                // Mark for deletion any existing institutions that have changed
                bool holdingInstitutionChanged = false;
                bool holdingInstitutionExists = false;
                bool rightsHolderChanged = false;
                bool rightsHolderExists = false;
                bool scanningInstitutionChanged = false;
                bool scanningInstitutionExists = false;
                foreach (Institution institution in book.Institutions)
                {
                    if (institution.InstitutionRoleName == InstitutionRole.HoldingInstitution)
                    {
                        holdingInstitutionExists = true;
                        if (institution.InstitutionCode != ddlInst.SelectedValue) { institution.IsDeleted = true; holdingInstitutionChanged = true; }
                    }
                    if (institution.InstitutionRoleName == InstitutionRole.RightsHolder)
                    {
                        rightsHolderExists = true;
                        if (institution.InstitutionCode != ddlRights.SelectedValue) { institution.IsDeleted = true; rightsHolderChanged = true; }
                    }
                    if (institution.InstitutionRoleName == InstitutionRole.ScanningInstitution)
                    {
                        scanningInstitutionExists = true;
                        if (institution.InstitutionCode != ddlScanningInstitution.SelectedValue) { institution.IsDeleted = true; scanningInstitutionChanged = true; }
                    }
                }

                // Add new institutions
                if ((holdingInstitutionChanged || !holdingInstitutionExists) && ddlInst.SelectedValue != string.Empty)
                {
                    Institution newHoldingInstitution = new Institution();
                    newHoldingInstitution.InstitutionCode = ddlInst.SelectedValue;
                    newHoldingInstitution.InstitutionRoleName = InstitutionRole.HoldingInstitution;
                    newHoldingInstitution.IsNew = true;
                    book.Institutions.Add(newHoldingInstitution);
                }

                if ((rightsHolderChanged || !rightsHolderExists) && ddlRights.SelectedValue != string.Empty)
                {
                    Institution newRightsHolder = new Institution();
                    newRightsHolder.InstitutionCode = ddlRights.SelectedValue;
                    newRightsHolder.InstitutionRoleName = InstitutionRole.RightsHolder;
                    newRightsHolder.IsNew = true;
                    book.Institutions.Add(newRightsHolder);
                }

                if ((scanningInstitutionChanged || !scanningInstitutionExists) && ddlScanningInstitution.SelectedValue != string.Empty)
                {
                    Institution newScanningInstitutution = new Institution();
                    newScanningInstitutution.InstitutionCode = ddlScanningInstitution.SelectedValue;
                    newScanningInstitutution.InstitutionRoleName = InstitutionRole.ScanningInstitution;
                    newScanningInstitutution.IsNew = true;
                    book.Institutions.Add(newScanningInstitutution);
                }

                //----------------------------------------
                // Update the ItemRelationship information
                foreach(Segment segment in book.Segments)
                {
                    //if (segment.IsDeleted || segment.IsDirty || segment.IsNew)
                    //{
                        //Book book = null;
                        //if (segment.BookID != null) book = bp.BookSelectAuto((int)segment.BookID);

                        bool existing = false;
                        foreach(ItemRelationship ir in book.ItemRelationships)
                        {
                            if (ir.IsParent == 1 && ir.ChildID == segment.ItemID)
                            {
                                existing = true;
                                if (segment.BookID == null)
                                    ir.IsDeleted = true;
                                else
                                    ir.SequenceOrder = segment.SequenceOrder ?? 0;

                                break;
                            }
                        }

                        if (!existing)// && book != null)
                        {
                            book.ItemRelationships.Add(
                                new ItemRelationship
                                {
                                    ParentID = book.ItemID,
                                    ChildID = segment.ItemID,
                                    SequenceOrder = segment.SequenceOrder ?? 0,
                                    IsNew = true
                                });
                        }
                    //}
                }

                //----------------------------------------
                // Update the title information
                List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + itemIdLabel.Text];

                // Add new titles
                foreach (DataObjects.ItemTitle itemTitle in itemTitles)
                {
                    bool found = false;
                    foreach (DataObjects.ItemTitle it in book.ItemTitles)
                    {
                        if (itemTitle.TitleID == it.TitleID)
                        {
                            found = true;
                            if (itemTitle.IsPrimary != it.IsPrimary)
                            {
                                // Set "LastModifiedDate" to force the "IsDirty" flag to "true"
                                it.LastModifiedDate = DateTime.Now;
                                it.IsPrimary = itemTitle.IsPrimary;
                            }
                            break;
                        }
                    }
                    if (!found)
                    {
                        DataObjects.ItemTitle newTitleItem = new DataObjects.ItemTitle();
                        newTitleItem.TitleID = itemTitle.TitleID;
                        newTitleItem.ItemID = (int)book.ItemID;
                        newTitleItem.IsPrimary = itemTitle.IsPrimary;
                        newTitleItem.ItemSequence = 10000;
                        newTitleItem.IsNew = true;
                        book.ItemTitles.Add(newTitleItem);
                    }
                }

                // Flag deleted titles
                foreach (DataObjects.ItemTitle it in book.ItemTitles)
                {
                    bool found = false;
                    foreach (DataObjects.ItemTitle itemTitle in itemTitles)
                    {
                        if (it.TitleID == itemTitle.TitleID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found) it.IsDeleted = true;
                }
                //----------------------------------------

				BHLProvider bp = new BHLProvider();
                // Don't catch errors... allow global error handler to take over
				bp.BookSave( book, (int)userId );

                // After a successful save operation, reload the item
                Book updatedBook = bp.BookSelectByBarcodeOrItemID(book.BookID, book.BarCode);
                Session["Item" + itemIdLabel.Text] = updatedBook;
                fillUI(updatedBook.BookID.ToString());
			}
			else
			{
				return;
			}

            litMessage.Text = "<span class='liveData'>Item Saved.</span>";
            ResetScrollPosition();
		}

        protected void PaginatorButton_Click(object sender, EventArgs e)
        {
            int titleId = 0;

            List<DataObjects.ItemTitle> itemTitles = (List<DataObjects.ItemTitle>)Session["ItemTitleList" + itemIdLabel.Text];
            foreach (DataObjects.ItemTitle itemTitle in itemTitles)
            {
                if (itemTitle.IsPrimary == 1) { titleId = itemTitle.TitleID; break; }
            }

            Response.Redirect("/Paginator.aspx?TitleID=" + titleId.ToString() + 
                "&ItemID=" + this.itemIdLabel.Text);
        }

		#endregion

        private void ResetScrollPosition()
        {
            if (!ClientScript.IsClientScriptBlockRegistered(GetType(), "CreateResetScrollPosition"))
            {
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
