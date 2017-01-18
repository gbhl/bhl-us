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
using SortOrder = CustomDataAccess.SortOrder;
using Paige = MOBOT.BHL.DataObjects.Page;

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

				string idString = Request.QueryString[ "id" ];
				int id = 0;
				if ( idString != null && int.TryParse( idString, out id ) )
				{
					itemIdTextBox.Text = id.ToString();
					search( id, null );
				}
				else
				{
					// TODO: Inform user that title does not exist -- Perhaps redirect to unknown.aspx?type=title
				}
			}
			else
			{
                String selectedTitleId = this.selectedTitle.Value;
                if (selectedTitleId != "")
                {
                    CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];
                    ItemTitle itemTitle = new ItemTitle();

                    // Get details for "selectedTitleId" from database
                    BHLProvider provider = new BHLProvider();
                    Title title = provider.TitleSelect(Convert.ToInt32(selectedTitleId));
                    itemTitle.TitleID = title.TitleID;
                    itemTitle.ShortTitle = title.ShortTitle;
                    itemTitle.IsPrimary = false;
                    itemTitles.Add(itemTitle);
                    Session["ItemTitleList" + itemIdTextBox.Text] = itemTitles;
                    this.selectedTitle.Value = "";
                    this.bindTitleData();
                }

                String selectedSegmentId = this.selectedSegment.Value;
                if (selectedSegmentId != "")
                {
                    Item item = (Item)Session["Item" + itemIdTextBox.Text];

                    // Make sure the selected segment isn't already associated with this item
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                    bool segmentExists = false;
                    foreach (Segment existingSegment in item.Segments)
                    {
                        if (existingSegment.SegmentID.ToString() == selectedSegmentId)
                        {
                            // Re-eneable this segment, if necessary
                            if (existingSegment.ItemID == null)
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
                        segment.ItemID = item.ItemID;
                        segment.SequenceOrder = (short)(segmentsList.Rows.Count + 1);
                        item.Segments.Add(segment);
                    }

                    Session["Item" + item.ItemID.ToString()] = item;
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
			Page.MaintainScrollPositionOnPostBack = true;

			Page.SetFocus( itemIdTextBox );
		}

		private void fillCombos()
		{
			BHLProvider bp = new BHLProvider();
            CustomGenericList<Institution> institutions = bp.InstituationSelectAll();

            /*
            Institution emptyInstitution = new Institution();
            emptyInstitution.InstitutionCode = "";
            emptyInstitution.InstitutionName = "";
            institutions.Insert(0, emptyInstitution);
             */

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

            CustomGenericList<Language> languages = bp.LanguageSelectAll();

			Language emptyLanguage = new Language();
			emptyLanguage.LanguageCode = "";
			emptyLanguage.LanguageName = "";
			languages.Insert( 0, emptyLanguage );

			ddlLang.DataSource = languages;
			ddlLang.DataTextField = "LanguageName";
			ddlLang.DataValueField = "LanguageCode";
			ddlLang.DataBind();

			CustomGenericList<Vault> vaults = bp.VaultSelectAll();

			Vault emptyVault = new Vault();
			emptyVault.VaultID = 0;
			emptyVault.Description = "";
			vaults.Insert( 0, emptyVault );

			ddlVault.DataSource = vaults;
			ddlVault.DataTextField = "Description";
			ddlVault.DataValueField = "VaultID";
			ddlVault.DataBind();

			CustomGenericList<ItemStatus> itemStatuses = bp.ItemStatusSelectAll();

			ddlItemStatus.DataSource = itemStatuses;
			ddlItemStatus.DataTextField = "ItemStatusName";
			ddlItemStatus.DataValueField = "ItemStatusID";
			ddlItemStatus.DataBind();
		}

		private void fillUI()
		{
            Item item = (Item)Session["Item" + itemIdTextBox.Text];

            if (item != null)
            {

                itemIdLabel.Text = item.ItemID.ToString();
                FlickrImage.Visible = item.HasFlickrImages;
                barcodeLabel.Text = item.BarCode;
                marcItemIDTextBox.Text = item.MARCItemID;
                callNumberTextBox.Text = item.CallNumber;
                volumeTextBox.Text = item.Volume;
                itemDescriptionTextBox.Text = item.ItemDescription;
                notesTextBox.Text = item.Note;
                replacedByTextBox.Text = item.RedirectItemID.ToString();
                replacedByOrig.Value = item.RedirectItemID.ToString();
                yearTextBox.Text = item.Year;
                identifierBibTextBox.Text = item.IdentifierBib;
                zQueryTextBox.Text = item.ZQuery;
                sponsorTextBox.Text = item.Sponsor;
                licenseUrlTextBox.Text = item.LicenseUrl;
                rightsTextBox.Text = item.Rights;
                dueDiligenceTextBox.Text = item.DueDiligence;
                copyrightStatusTextBox.Text = item.CopyrightStatus;
                copyrightRegionTextBox.Text = item.CopyrightRegion;
                copyrightCommentTextBox.Text = item.CopyrightComment;
                copyrightEvidenceTextBox.Text = item.CopyrightEvidence;
                externalUrlTextBox.Text = item.ExternalUrl;

                CustomGenericList<ItemTitle> itemTitles = new CustomGenericList<ItemTitle>();
                foreach (TitleItem titleItem in item.TitleItems)
                {
                    ItemTitle itemTitle = new ItemTitle();
                    itemTitle.TitleID = titleItem.TitleID;
                    itemTitle.ShortTitle = titleItem.ShortTitle;
                    itemTitle.IsPrimary = (item.PrimaryTitleID == titleItem.TitleID);
                    itemTitles.Add(itemTitle);
                }
                Session["ItemTitleList" + itemIdTextBox.Text] = itemTitles;

                titleList.DataSource = itemTitles;// item.Titles;
                titleList.DataBind();

                languagesList.DataSource = item.ItemLanguages;
                languagesList.DataBind();

                collectionsList.DataSource = item.ItemCollections;
                collectionsList.DataBind();

                segmentsList.DataSource = item.Segments;
                segmentsList.DataBind();

                scannedByLabel.Text = item.ScanningUser;
                scannedDateLabel.Text = (item.ScanningDate.HasValue ? item.ScanningDate.Value.ToShortDateString() : "");
                creationDateLabel.Text = (item.CreationDate.HasValue ? item.CreationDate.Value.ToShortDateString() : "");
                lastModifiedDateLabel.Text =
                    (item.LastModifiedDate.HasValue ? item.LastModifiedDate.Value.ToShortDateString() : "");

                ddlInst.SelectedValue = "UNKNOWN";
                foreach (Institution contributor in item.Contributors)
                {
                    if (contributor.InstitutionRoleName == InstitutionRole.Contributor)
                    {
                        ddlInst.SelectedValue = contributor.InstitutionCode;
                        break;
                    }
                }

                ddlRights.SelectedValue = "";
                foreach (Institution contributor in item.Contributors)
                {
                    if (contributor.InstitutionRoleName == InstitutionRole.RightsHolder)
                    {
                        ddlRights.SelectedValue = contributor.InstitutionCode;
                        break;
                    }
                }

                ddlScanningInstitution.SelectedValue = "";
                foreach (Institution contributor in item.Contributors)
                {
                    if (contributor.InstitutionRoleName == InstitutionRole.ScanningInstitution)
                    {
                        ddlScanningInstitution.SelectedValue = contributor.InstitutionCode;
                        break;
                    }
                }

                if (item.LanguageCode != null && item.LanguageCode.Length > 0)
                {
                    ddlLang.SelectedValue = item.LanguageCode.ToUpper();
                }
                else
                {
                    ddlLang.SelectedIndex = 0;
                }

                if (item.VaultID.HasValue)
                {
                    ddlVault.SelectedValue = item.VaultID.Value.ToString();
                }
                else
                {
                    ddlVault.SelectedIndex = 0;
                }

                if (item.ItemStatusID > 0)
                {
                    ddlItemStatus.SelectedValue = item.ItemStatusID.ToString();
                }
                else
                {
                    ddlItemStatus.SelectedIndex = 0;
                }
                itemStatusOrig.Value = item.ItemStatusID.ToString();

                if (item.Pages.Count > 0)
                {
                    pageFieldSet.Visible = true;
                    pageList.DataSource = item.Pages;
                    pageList.DataBind();
                }
                else
                {
                    pageFieldSet.Visible = false;
                }


                // See if we can display a link to the MARC file
                SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                if (service.MARCFileExists(item.ItemID, "i"))
                {
                    hypMarc.Attributes["onclick"] = string.Format("javascript:window.open('TitleItemMarc.aspx?type=i&id={0}', '', 'width=600,height=600,location=0,status=0,scrollbars=1');", item.ItemID.ToString());
                    hypMarc.Visible = true;                        
                }
            }
		}

		private void search( int? id, string barcode )
		{
			BHLProvider bp = new BHLProvider();
			Item item = bp.ItemSelectByBarcodeOrItemID( id, barcode );

            if (item != null)
            {
                // Look up flickr status of the item
                Item flickrItem = bp.ItemInFlickrByItemID(item.ItemID);
                item.HasFlickrImages = (flickrItem != null) ? flickrItem.HasFlickrImages : false;
            }

            Session["Item" + itemIdTextBox.Text] = item;
			fillUI();
		}

		private void bindPageData()
		{
            Item item = (Item)Session["Item" + itemIdTextBox.Text];
            PageComparer comp = new PageComparer((PageComparer.CompareEnum)_sortColumn, _sortOrder);
            item.Pages.Sort(comp);
            pageList.DataSource = item.Pages;
            pageList.DataBind();
		}

        private void bindTitleData()
        {
            CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];
            titleList.DataSource = itemTitles;
            titleList.DataBind();
        }

        private bool validate(Item item)
		{
			bool flag = false;

            // Make sure that one and only one title is designated as the primary title.
            int primaryTitleId = 0;
            int numPrimary = 0;
            CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];
            foreach (ItemTitle itemTitle in itemTitles)
            {
                if (itemTitle.IsPrimary)
                {
                    numPrimary++;
                    primaryTitleId = itemTitle.TitleID;
                }
            }
            if (numPrimary == 1)
            {
                item.PrimaryTitleID = primaryTitleId;
            }
            else
            {
                flag = true;
                errorControl.AddErrorText("One and only one title must be designated the Primary title for this item.");
            }

            // If a "replaced by" identifer was specified, make sure that it is a valid id
            if (replacedByTextBox.Text.Trim().Length > 0)
            {
                int itemID;
                if (Int32.TryParse(replacedByTextBox.Text, out itemID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().ItemSelectAuto(itemID) == null)
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
			if ( pageList.EditIndex != -1 )
			{
			  flag = true;
			  errorControl.AddErrorText( "Items has an edit pending" );
			}

            if (collectionsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Collections has an edit pending");
            }

            if (segmentsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Segments has an edit pending");
            }

            bool br = false;
            int ix = 0;
            foreach (ItemCollection ic in item.ItemCollections)
            {
                if (ic.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (ItemCollection ic2 in item.ItemCollections)
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
			Page.MaintainScrollPositionOnPostBack = !flag;

			return !flag;
		}

        #region ItemLanguage methods

        private void bindLanguageData()
        {
            Item item = (Item)Session["Item" + itemIdTextBox.Text];

            // filter out deleted items
            CustomGenericList<ItemLanguage> itemLanguages = new CustomGenericList<ItemLanguage>();
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

        CustomGenericList<Language> _itemLanguages = null;
        protected CustomGenericList<Language> GetLanguages()
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

        private ItemLanguage findItemLanguage(CustomGenericList<ItemLanguage> itemLanguages,
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
            Item item = (Item)Session["Item" + itemIdTextBox.Text];

            // filter out deleted items
            CustomGenericList<ItemCollection> itemCollections = new CustomGenericList<ItemCollection>();
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

        CustomGenericList<Collection> _itemCollections = null;
        protected CustomGenericList<Collection> GetCollections()
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

        private ItemCollection findCollection(CustomGenericList<ItemCollection> collections, int itemCollectionId,
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
            Item item = (Item)Session["Item" + itemIdTextBox.Text];

            // filter out deleted segments
            CustomGenericList<Segment> segments = new CustomGenericList<Segment>();
            foreach (Segment s in item.Segments)
            {
                if (s.ItemID != null) segments.Add(s);
            }

            SegmentComparer comp = new SegmentComparer((SegmentComparer.CompareEnum)SegmentComparer.CompareEnum.SequenceOrder, _sortOrder);
            segments.Sort(comp);
            segmentsList.DataSource = segments;
            segmentsList.DataBind();
        }

        private Segment findSegment(CustomGenericList<Segment> segments, int segmentId)
        {
            foreach (Segment s in segments)
            {
                if (s.ItemID == null)
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
                    CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];
                    bool isPrimary = checkBox.Checked;

                    String titleIdString = row.Cells[1].Text;
                    int titleId = 0;
                    int.TryParse(titleIdString, out titleId);

                    if (titleId > 0)
                    {
                        // Update primary title
                        foreach (ItemTitle itemTitle in itemTitles)
                        {
                            if (titleId == itemTitle.TitleID)
                            {
                                itemTitle.IsPrimary = isPrimary;
                                break;
                            }
                        }
                    }
                }
            }

            titleList.EditIndex = -1;
            bindTitleData();
        }

        protected void titleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                int selectedTitle = (int)titleList.DataKeys[rowNum].Values[0];
                CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];

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
                    Item item = (Item)Session["Item" + itemIdTextBox.Text];
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

            Item item = (Item)Session["Item" + itemIdTextBox.Text];
            ItemLanguage il = new ItemLanguage(0, item.ItemID, "", DateTime.Now, userId);
            item.ItemLanguages.Add(il);
            languagesList.EditIndex = languagesList.Rows.Count;
            bindLanguageData();
        }

        protected void languagesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Item item = (Item)Session["Item" + itemIdTextBox.Text];

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
                    Item item = (Item)Session["Item" + itemIdTextBox.Text];
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
            Item item = (Item)Session["Item" + itemIdTextBox.Text];
            ItemCollection ic = new ItemCollection(0, item.ItemID, 0);
            item.ItemCollections.Add(ic);
            collectionsList.EditIndex = collectionsList.Rows.Count;
            bindCollectionData();
        }

        protected void collectionsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Item item = (Item)Session["Item" + itemIdTextBox.Text];

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
                    Item item = (Item)Session["Item" + itemIdTextBox.Text];

                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        // Find segment being changed
                        Segment changedSegment = findSegment(item.Segments,
                            (int)segmentsList.DataKeys[e.RowIndex].Values[0]);

                        short oldSeq = changedSegment.SequenceOrder;

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
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Item item = (Item)Session["Item" + itemIdTextBox.Text];

                Segment currentSegment = findSegment(item.Segments,
                    (int)segmentsList.DataKeys[rowNum].Values[0]);

                currentSegment.ItemID = null;

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
                Session["Item" + itemIdTextBox.Text] = item;

                bindSegmentData();
            }
        }

        #endregion Segment event handlers

        #region Page event handlers

        protected void pageList_RowEditing( object sender, GridViewEditEventArgs e )
		{
			pageList.EditIndex = e.NewEditIndex;
			bindPageData();
		}

		protected void pageList_RowUpdating( object sender, GridViewUpdateEventArgs e )
		{
			GridViewRow row = pageList.Rows[ e.RowIndex ];

			if ( row != null )
			{
				TextBox textBox = row.FindControl( "sequenceOrderTextBox" ) as TextBox;
				if ( textBox != null )
				{
                    Item item = (Item)Session["Item" + itemIdTextBox.Text];
					string seqOrderString = textBox.Text.Trim();

					int seqOrder = 0;
					int.TryParse( seqOrderString, out seqOrder );
					string pageIdString = row.Cells[ 0 ].Text;
					int pageId = 0;
					int.TryParse( pageIdString, out pageId );

					if ( seqOrder > 0 && pageId > 0 )
					{
						// Find current item sequence
						int? curSeqOrder = 0;
						foreach ( Paige page in item.Pages )
						{
							if ( page.PageID == pageId && page.SequenceOrder.HasValue )
							{
								curSeqOrder = page.SequenceOrder.Value;
								break;
							}
						}

						// Find item whose item sequence will be overwritten
						if ( curSeqOrder > 0 )
						{
							foreach ( Paige page in item.Pages )
							{
								if ( page.SequenceOrder == seqOrder )
								{
									// Change it to the changing item's item sequence
									page.SequenceOrder = curSeqOrder;
									break;
								}
							}
						}
						else // move all item sequences down by one
						{
							int id = pageId;
							int seqOrdert = seqOrder;
							foreach ( Paige page in item.Pages )
							{
								if ( page.SequenceOrder == seqOrdert && page.PageID != id )
								{
									if ( page.SequenceOrder.HasValue )
									{
										page.SequenceOrder = (int?)( page.SequenceOrder.Value + 1 );
										seqOrdert++;
									}
								}
								id = page.PageID;
							}
						}

						foreach ( Paige page in item.Pages )
						{
							if ( page.PageID == pageId )
							{
								page.SequenceOrder = seqOrder;
								break;
							}
						}
					}
				}
			}

			pageList.EditIndex = -1;
			bindPageData();
		}

		protected void pageList_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
		{
			pageList.EditIndex = -1;
			bindPageData();
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
							sortColumnIndex = 0;
							break;
						}
					case PageComparer.CompareEnum.FileNamePrefix:
						{
							sortColumnIndex = 1;
							break;
						}
					case PageComparer.CompareEnum.SequenceOrder:
						{
							sortColumnIndex = 2;
							break;
						}
				}

				e.Row.Cells[ sortColumnIndex ].Controls.Add( new LiteralControl( " " ) );
				e.Row.Cells[ sortColumnIndex ].Controls.Add( img );
				e.Row.Cells[ sortColumnIndex ].Wrap = false;
			}
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Paige page = (Paige)e.Row.DataItem;
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
            else if (barCodeTextBox.Text.Trim().Length > 0)
            {
                search(null, barCodeTextBox.Text.Trim());
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
		{
            Item item = (Item)Session["Item" + itemIdTextBox.Text];

			if ( validate( item ) )
			{
                int? i = null;
                int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                // Gather up data on form
                item.RedirectItemID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
				item.MARCItemID = marcItemIDTextBox.Text.Trim();
				item.CallNumber = callNumberTextBox.Text.Trim();
				item.Volume = volumeTextBox.Text.Trim();
				item.LanguageCode = ( ddlLang.SelectedValue.Length == 0 ? null : ddlLang.SelectedValue );
                item.ItemDescription = itemDescriptionTextBox.Text.Trim();
				item.Note = notesTextBox.Text.Trim();
                item.Year = yearTextBox.Text.Trim();
                item.IdentifierBib = identifierBibTextBox.Text.Trim();
                item.ZQuery = zQueryTextBox.Text.Trim();
                item.Sponsor = sponsorTextBox.Text.Trim();
                item.LicenseUrl = licenseUrlTextBox.Text.Trim();
                item.Rights = rightsTextBox.Text.Trim();
                item.DueDiligence = dueDiligenceTextBox.Text.Trim();
                item.CopyrightStatus = copyrightStatusTextBox.Text.Trim();
                item.CopyrightRegion = copyrightRegionTextBox.Text.Trim();
                item.CopyrightComment = copyrightCommentTextBox.Text.Trim();
                item.CopyrightEvidence = copyrightEvidenceTextBox.Text.Trim();
                item.ExternalUrl = externalUrlTextBox.Text.Trim();
				item.VaultID = ( ddlVault.SelectedIndex == 0 ? i : int.Parse(ddlVault.SelectedValue) );
				item.ItemStatusID = int.Parse( ddlItemStatus.SelectedValue );

				item.IsNew = false;

                //----------------------------------------

                // Mark for deletion any existing institutions that have changed
                bool contributorChanged = false;
                bool contributorExists = false;
                bool rightsHolderChanged = false;
                bool rightsHolderExists = false;
                bool scanningInstitutionChanged = false;
                bool scanningInstitutionExists = false;
                foreach (Institution institution in item.Contributors)
                {
                    if (institution.InstitutionRoleName == InstitutionRole.Contributor)
                    {
                        contributorExists = true;
                        if (institution.InstitutionCode != ddlInst.SelectedValue) { institution.IsDeleted = true; contributorChanged = true; }
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
                if ((contributorChanged || !contributorExists) && ddlInst.SelectedValue != string.Empty)
                {
                    Institution newContributor = new Institution();
                    newContributor.InstitutionCode = ddlInst.SelectedValue;
                    newContributor.InstitutionRoleName = InstitutionRole.Contributor;
                    newContributor.IsNew = true;
                    item.Contributors.Add(newContributor);
                }

                if ((rightsHolderChanged || !rightsHolderExists) && ddlRights.SelectedValue != string.Empty)
                {
                    Institution newRightsHolder = new Institution();
                    newRightsHolder.InstitutionCode = ddlRights.SelectedValue;
                    newRightsHolder.InstitutionRoleName = InstitutionRole.RightsHolder;
                    newRightsHolder.IsNew = true;
                    item.Contributors.Add(newRightsHolder);
                }

                if ((scanningInstitutionChanged || !scanningInstitutionExists) && ddlScanningInstitution.SelectedValue != string.Empty)
                {
                    Institution newScanningInstitutution = new Institution();
                    newScanningInstitutution.InstitutionCode = ddlScanningInstitution.SelectedValue;
                    newScanningInstitutution.InstitutionRoleName = InstitutionRole.ScanningInstitution;
                    newScanningInstitutution.IsNew = true;
                    item.Contributors.Add(newScanningInstitutution);
                }

                //----------------------------------------
                // Update the title information
                CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];

                // Add new titles
                foreach (ItemTitle itemTitle in itemTitles)
                {
                    bool found = false;
                    foreach (TitleItem titleItem in item.TitleItems)
                    {
                        if (itemTitle.TitleID == titleItem.TitleID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        TitleItem newTitleItem = new TitleItem();
                        newTitleItem.TitleID = itemTitle.TitleID;
                        newTitleItem.ItemID = item.ItemID;
                        newTitleItem.ItemSequence = 10000;
                        newTitleItem.IsNew = true;
                        item.TitleItems.Add(newTitleItem);
                    }
                }

                // Flag deleted titles
                foreach (TitleItem titleItem in item.TitleItems)
                {
                    bool found = false;
                    foreach (ItemTitle itemTitle in itemTitles)
                    {
                        if (titleItem.TitleID == itemTitle.TitleID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found) titleItem.IsDeleted = true;
                }
                //----------------------------------------

				BHLProvider bp = new BHLProvider();
                // Don't catch errors... allow global error handler to take over
				bp.ItemSave( item, (int)userId );

                // After a successful save operation, reload the item
                Session["Item" + itemIdTextBox.Text] = bp.ItemSelectByBarcodeOrItemID(item.ItemID, item.BarCode);
                fillUI();
			}
			else
			{
				return;
			}

            litMessage.Text = "<span class='liveData'>Item Saved.</span>";
            Page.MaintainScrollPositionOnPostBack = false;
            //Response.Redirect("/");
		}

        protected void PaginatorButton_Click(object sender, EventArgs e)
        {
            int titleId = 0;

            CustomGenericList<ItemTitle> itemTitles = (CustomGenericList<ItemTitle>)Session["ItemTitleList" + itemIdTextBox.Text];
            foreach (ItemTitle itemTitle in itemTitles)
            {
                if (itemTitle.IsPrimary) { titleId = itemTitle.TitleID; break; }
            }

            Response.Redirect("/Paginator.aspx?TitleID=" + titleId.ToString() + 
                "&ItemID=" + this.itemIdLabel.Text);
        }

		#endregion

        #region TitleItem

        [Serializable]
        private class ItemTitle : Title
        {
            private int _titleID;

            public int TitleID
            {
                get { return _titleID; }
                set { _titleID = value; }
            }

            private String _shortTitle;

            public String ShortTitle
            {
                get { return _shortTitle; }
                set { _shortTitle = value; }
            }

            private bool _isPrimary;

            public bool IsPrimary
            {
                get { return _isPrimary; }
                set { _isPrimary = value; }
            }
        }

        #endregion
    }
}
