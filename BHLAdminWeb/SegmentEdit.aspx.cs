using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentEdit : System.Web.UI.Page
    {
        private SortOrder _sortOrder = SortOrder.Ascending;

        protected void Page_Load(object sender, EventArgs e)
        {
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectItem", "<script language='javascript'>function selectItem(itemId) { if (itemId != '') document.getElementById('" + selectedItem.ClientID + "').value=itemId; overlay(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearItem", "<script language='javascript'>function clearItem() { document.getElementById('" + selectedItem.ClientID + "').value=''; __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearAuthor", "<script language='javascript'>function clearAuthor() { document.getElementById('" + selectedAuthor.ClientID + "').value=''; overlayAuthorSearch(); __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectAuthor", "<script language='javascript'>function selectAuthor(authorId) { document.getElementById('" + selectedAuthor.ClientID + "').value=authorId; overlayAuthorSearch(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptCancelPage", "<script language='javascript'>function cancelPages() { __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectPage", "<script language='javascript'>function selectPage(pageId) { document.getElementById('" + selectedPage.ClientID + "').value+=pageId+'|';}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearRelatedSegment", "<script language='javascript'>function clearRelatedSegment() { document.getElementById('" + selectedRelatedSegments.ClientID + "').value=''; document.getElementById('" + selectedClusterType.ClientID + "').value=''; __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectRelatedSegment", "<script language='javascript'>function selectRelatedSegment(segmentId, clusterType, typeLabel) { document.getElementById('" + selectedRelatedSegments.ClientID + "').value+=segmentId+'|'; document.getElementById('" + selectedClusterType.ClientID + "').value=clusterType+'|'+typeLabel;}</script>");

            litMessage.Text = "";
            errorControl.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {
                string idString = Request.QueryString["id"];
                int id = 0;
                if (idString != null && int.TryParse(idString, out id))
                {
                    fillCombos();
                    fillUI(id);
                }
                else
                {
                    errorControl.AddErrorText("Invalid Segment ID");
                    errorControl.Visible = true;
                }
            }
            else
            {
                BHLProvider provider = new BHLProvider();

                // Check for a newly added item
                String selectedItemId = this.selectedItem.Value;
                if (selectedItemId == string.Empty)
                {
                    if (!string.IsNullOrWhiteSpace(itemIDLabel.Text))
                    {
                        // Item has been removed
                        removeItem(idLabel.Text, itemIDLabel.Text);
                        /*
                        Segment segment = (Segment)Session["Segment" + idLabel.Text];
                        foreach (ItemRelationship relationship in segment.RelationshipList)
                        {
                            if (segment.ItemID == relationship.ChildID && itemIDLabel.Text == relationship.BookID.ToString()) relationship.IsDeleted = true;
                        }
                        foreach (ItemPage page in segment.PageList) page.IsDeleted = true;
                        bindSegmentPageData();
                        Session["Segment" + segment.SegmentID.ToString()] = segment;
                        */
                        itemIDLabel.Text = "";
                        itemDescLabel.Text = "Not Selected";
                        fillItemDetails("", "", "", "");
                    }
                }
                else if (itemIDLabel.Text != selectedItemId)
                {
                    // Get the description of the newly selected item
                    Book book = provider.BookSelectByBarcodeOrItemID(Convert.ToInt32(selectedItemId), null);
                    List<Institution> institutions = provider.InstitutionSelectByItemIDAndRole(Convert.ToInt32(book.ItemID), InstitutionRole.Contributor);
                    Title title = provider.TitleSelectAuto((int)book.PrimaryTitleID);

                    string oldBookID = itemIDLabel.Text;
                    itemIDLabel.Text = selectedItemId;
                    itemDescLabel.Text = title.ShortTitle + " " + book.Volume;

                    // Use Title/Item metadata to set the journal details for this segment
                    if (institutions.Count > 0) ddlContributor.SelectedValue = institutions[0].InstitutionCode;
                    containerTitleTextBox.Text = title.FullTitle ?? string.Empty;
                    publicationDetailsTextBox.Text = title.PublicationDetails ?? string.Empty;
                    publisherNameTextBox.Text = title.Datafield_260_b ?? string.Empty;
                    publisherPlaceTextBox.Text = title.Datafield_260_a ?? string.Empty;
                    ddlLanguage.SelectedValue = book.LanguageCode ?? string.Empty;

                    fillItemDetails((book.Volume ?? string.Empty),
                        string.Join("-", (new string[] { book.StartIssue ?? string.Empty, book.EndIssue ?? string.Empty }).Where(s => !string.IsNullOrEmpty(s))),
                        string.Join("-", (new string[] { book.StartSeries ?? string.Empty, book.EndSeries ?? string.Empty }).Where(s => !string.IsNullOrEmpty(s))),
                        string.Join("-", (new string[] { book.StartYear ?? string.Empty, book.EndYear ?? string.Empty }).Where(s => !string.IsNullOrEmpty(s))));
                    //volumeTextBox.Text = book.Volume ?? string.Empty;
                    //dateTextBox.Text = string.IsNullOrWhiteSpace(book.StartYear) ? (title.StartYear == null ? string.Empty : title.StartYear.ToString()) : book.StartYear;

                    // Update the list of segments associated with this item
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    if (!string.IsNullOrWhiteSpace(oldBookID)) removeItem(idLabel.Text, oldBookID);
                    segment.RelationshipList.AddRange(provider.ItemRelationshipSelectByItemID(book.ItemID));
                    Session["Segment" + segment.SegmentID.ToString()] = segment;
                }

                // Check for newly added authors
                String selectedAuthorId = this.selectedAuthor.Value;
                if (selectedAuthorId != "")
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];

                    // Make sure the selected author isn't already associated with this title
                    bool authorExists = false;
                    foreach (ItemAuthor existingAuthor in segment.AuthorList)
                    {
                        if (existingAuthor.AuthorID.ToString() == selectedAuthorId)
                        {
                            authorExists = true;
                            break;
                        }
                    }

                    if (!authorExists)
                    {
                        ItemAuthor newSegmentAuthor = new ItemAuthor();

                        // Get details for "selectedAuthorId" from database
                        Author author = provider.AuthorSelectWithNameByAuthorId(Convert.ToInt32(selectedAuthorId));
                        newSegmentAuthor.AuthorID = Convert.ToInt32(selectedAuthorId);
                        newSegmentAuthor.ItemID = segment.ItemID;
                        newSegmentAuthor.FullName = author.FullName;
                        newSegmentAuthor.FullerForm = author.FullerForm;
                        newSegmentAuthor.Numeration = author.Numeration;
                        newSegmentAuthor.Unit = author.Unit;
                        newSegmentAuthor.Title = author.Title;
                        newSegmentAuthor.Location = author.Location;
                        newSegmentAuthor.SequenceOrder = (short)(authorsList.Rows.Count + 1);
                        newSegmentAuthor.Author = author;
                        newSegmentAuthor.IsNew = true;
                        segment.AuthorList.Add(newSegmentAuthor);
                    }

                    Session["Segment" + segment.SegmentID.ToString()] = segment;
                    this.selectedAuthor.Value = "";
                    this.bindSegmentAuthorData();
                }

                // Check for newly added related segments
                String selectedRelatedSegmentIds = this.selectedRelatedSegments.Value;
                if (selectedRelatedSegmentIds != string.Empty)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];

                    // Get a list of all of the segments for the item
                    List<Segment> relatedSegments = provider.SegmentSelectRelated(Convert.ToInt32(string.IsNullOrWhiteSpace(itemIDLabel.Text) ? "0" : itemIDLabel.Text));

                    if (selectedRelatedSegmentIds.EndsWith("|")) selectedRelatedSegmentIds = selectedRelatedSegmentIds.Substring(0, selectedRelatedSegmentIds.Length - 1);
                    String[] selectedRelatedSegmentsList = selectedRelatedSegmentIds.Split('|');

                    string selectedTypeID = "10";
                    string selectedTypeLabel = "Same as";
                    if (!string.IsNullOrWhiteSpace(selectedClusterType.Value))
                    {
                        selectedTypeID = selectedClusterType.Value.Split('|')[0];
                        selectedTypeLabel = selectedClusterType.Value.Split('|')[1];
                    }

                    foreach (String selectedSegmentId in selectedRelatedSegmentsList)
                    {
                        // Make sure the selected segment isn't already related to this segment
                        bool segmentExists = false;
                        foreach (Segment existingSegment in segment.RelatedSegmentList)
                        {
                            if (existingSegment.SegmentID.ToString() == selectedSegmentId)
                            {
                                segmentExists = true;
                                break;
                            }
                        }

                        if (!segmentExists)
                        {
                            Segment newRelatedSegment = provider.SegmentSelectForSegmentID(Convert.ToInt32(selectedSegmentId));

                            newRelatedSegment.SegmentClusterId = segment.SegmentClusterId;
                            newRelatedSegment.SegmentClusterTypeId = Convert.ToInt32(selectedTypeID);
                            newRelatedSegment.SegmentClusterTypeLabel = selectedTypeLabel;
                            newRelatedSegment.IsPrimary = 0;
                            newRelatedSegment.IsNew = true;
                            segment.RelatedSegmentList.Add(newRelatedSegment);
                        }
                    }

                    Session["Segment" + segment.SegmentID.ToString()] = segment;
                    this.selectedRelatedSegments.Value = "";
                    this.bindRelatedSegmentData();
                }

                // Check for newly added pages
                String selectedPageIds = this.selectedPage.Value;
                if (selectedPageIds != string.Empty)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];

                    // Get a list of all of the pages for the item
                    List<DataObjects.Page> pages = provider.PageSelectByBookID(Convert.ToInt32(itemIDLabel.Text));

                    if (selectedPageIds.EndsWith("|")) selectedPageIds = selectedPageIds.Substring(0, selectedPageIds.Length - 1);
                    String[] selectedPageIdList = selectedPageIds.Split('|');
                    foreach (String selectedPageId in selectedPageIdList)
                    {
                        // Make sure the selected page isn't already associated with this segment
                        bool pageExists = false;
                        foreach (ItemPage existingPage in segment.PageList)
                        {
                            if (existingPage.PageID.ToString() == selectedPageId)
                            {
                                pageExists = true;
                                break;
                            }
                        }

                        if (!pageExists)
                        {
                            ItemPage newPage = new ItemPage();

                            // Get the current maximum itemsequence value
                            int pageSequence = 0;
                            foreach (ItemPage segmentPage in segment.PageList)
                            {
                                if (segmentPage.SequenceOrder > pageSequence && !segmentPage.IsDeleted) pageSequence = segmentPage.SequenceOrder;
                            }

                            // Get the page types and numbers for the selected page
                            string types = string.Empty;
                            string numbers = string.Empty;
                            int? pageSequenceOrder = null;
                            foreach (MOBOT.BHL.DataObjects.Page page in pages)
                            {
                                if (page.PageID == Convert.ToInt32(selectedPageId))
                                {
                                    pageSequenceOrder = page.SequenceOrder;
                                    types = page.PageTypes;
                                    numbers = page.IndicatedPages;
                                    break;
                                }
                            }

                            newPage.PageID = Convert.ToInt32(selectedPageId);
                            newPage.ItemID = segment.ItemID;
                            newPage.SequenceOrder = ++pageSequence;
                            newPage.PageSequenceOrder = (int)pageSequenceOrder;
                            newPage.PageTypes = types;
                            newPage.IndicatedPages = numbers;
                            newPage.IsNew = true;
                            segment.PageList.Add(newPage);
                        }
                    }

                    // Re-sort the list of pages, using the order of the scanned pages as a guide
                    ItemPagePSequenceComparer comp = new ItemPagePSequenceComparer();
                    segment.PageList.Sort(comp);
                    short newSeqOrder = 1;
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));
                    foreach (ItemPage segmentPage in segment.PageList)
                    {
                        if (segmentPage.SequenceOrder != newSeqOrder)
                        {
                            segmentPage.SequenceOrder = newSeqOrder;
                            segmentPage.LastModifiedUserID = userId;
                        }
                        newSeqOrder++;
                    }

                    Session["Segment" + segment.SegmentID.ToString()] = segment;
                    this.selectedPage.Value = "";
                    this.bindSegmentPageData();
                }
            }

            editHistoryControl.EntityName = "segment";
            editHistoryControl.EntityId = idLabel.Text;

            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill methods

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();

            List<ItemStatus> statuses = bp.SegmentStatusSelectAll();
            ddlSegmentStatus.DataSource = statuses;
            ddlSegmentStatus.DataBind();

            List<SegmentGenre> genres= bp.SegmentGenreSelectAll();
            ddlSegmentGenre.DataSource = genres;
            ddlSegmentGenre.DataBind();

            List<Institution> institutions = bp.InstituationSelectAll();

            Institution emptyInstitution = new Institution();
            emptyInstitution.InstitutionCode = string.Empty;
            emptyInstitution.InstitutionName = string.Empty;
            institutions.Insert(0, emptyInstitution);

            ddlContributor.DataSource = institutions;
            ddlContributor.DataBind();

            ddlContributor2.DataSource = institutions;
            ddlContributor2.DataBind();

            List<Language> languages = bp.LanguageSelectAll();

            Language emptyLanguage = new Language();
            emptyLanguage.LanguageCode = string.Empty;
            emptyLanguage.LanguageName = string.Empty;
            languages.Insert(0, emptyLanguage);

            ddlLanguage.DataSource = languages;
            ddlLanguage.DataBind();
        }

        private void fillUI(int id)
        {
            BHLProvider bp = new BHLProvider();

            idLabel.Text = id.ToString();

            // Look up segment
            Segment segment = null;
            if (id == 0)
            {
                // Create new segment
                segment = new Segment();
                segment.SequenceOrder = 1;
                ddlSegmentStatus.SelectedValue = "30";  // New
            }
            else
            {
                // Look up segment
                segment = bp.SegmentSelectForEdit(id);
            }

            if (segment == null)
            {
                errorControl.AddErrorText("Segment Not Found");
                errorControl.Visible = true;
            }
            else
            {

                Session["Segment" + segment.SegmentID.ToString()] = segment;

                if (segment.ItemID != null)
                {
                    selectedItem.Value = segment.BookID.ToString();
                    itemIDLabel.Text = segment.BookID.ToString();
                    itemDescLabel.Text = segment.TitleShortTitle + " " + segment.ItemVolume;
                }
                replacedByTextBox.Text = segment.RedirectSegmentID.ToString();
                doiTextBox.Text = segment.DOIName;
                titleTextBox.Text = segment.Title;
                sortTitleTextBox.Text = segment.SortTitle;
                translatedTitleTextBox.Text = segment.TranslatedTitle;
                containerTitleTextBox.Text = segment.ContainerTitle;
                publicationDetailsTextBox.Text = segment.PublicationDetails;
                publisherPlaceTextBox.Text = segment.PublisherPlace;
                publisherNameTextBox.Text = segment.PublisherName;
                notesTextBox.Text = segment.Notes;
                summaryTextBox.Text = segment.Summary;
                volumeTextBox.Text = segment.Volume;
                seriesTextBox.Text = segment.Series;
                issueTextBox.Text = segment.Issue;
                dateTextBox.Text = segment.Date;
                fillItemDetails(segment.ItemVolume, segment.ItemIssue, segment.ItemSeries, segment.ItemYear);
                pageRangeTextBox.Text = segment.PageRange;
                startPageTextBox.Text = segment.StartPageNumber;
                endPageTextBox.Text = segment.EndPageNumber;
                bhlStartPageIDTextBox.Text = segment.StartPageID.ToString();
                urlTextBox.Text = segment.Url;
                downloadUrlTextBox.Text = segment.DownloadUrl;
                rightsStatusTextBox.Text = segment.RightsStatus;
                rightsStatementTextBox.Text = segment.RightsStatement;
                licenseNameTextBox.Text = segment.LicenseName;
                licenseUrlTextBox.Text = segment.LicenseUrl;
                chkPrimary.Checked = (segment.IsPrimary == 1);

                ddlSegmentStatus.SelectedValue = segment.SegmentStatusID.ToString();
                ddlSegmentGenre.SelectedValue = segment.SegmentGenreID.ToString();

                switch (segment.ContributorList.Count)
                {
                    case 0:
                        ddlContributor.SelectedIndex = 0;
                        break;
                    case 1:
                        ddlContributor.SelectedValue = segment.ContributorList[0].InstitutionCode;
                        break;
                    default:    // 2 or more contributors (should only be two at most)
                        ddlContributor.SelectedValue = segment.ContributorList[0].InstitutionCode;
                        ddlContributor2.SelectedValue = segment.ContributorList[1].InstitutionCode;
                        break;
                }

                if (segment.LanguageCode != null && segment.LanguageCode.Length > 0)
                {
                    ddlLanguage.SelectedValue = segment.LanguageCode;
                }
                else
                {
                    ddlLanguage.SelectedIndex = 0;
                }

                authorsList.DataSource = segment.AuthorList;
                authorsList.DataBind();

                keywordsList.DataSource = segment.KeywordList;
                keywordsList.DataBind();

                identifiersList.DataSource = segment.IdentifierList;
                identifiersList.DataBind();

                pagesList.DataSource = segment.PageList;
                pagesList.DataBind();

                if (segment.RelatedSegmentList.Count > 0)
                {
                    litPrimaryMsg.Visible = true;
                    chkPrimary.Visible = true;
                }

                relatedSegmentsList.DataSource = segment.RelatedSegmentList;
                relatedSegmentsList.DataBind();
            }
        }

        private void fillItemDetails(string volume, string issue, string series, string date)
        {
            litVolume.Text = string.Format("Item Volume: {0}", volume);
            litIssue.Text = string.Format("Item Issue: {0}", issue);
            litSeries.Text = string.Format("Item Series: {0}", series);
            litDate.Text = string.Format("Item Date: {0}", date);
        }

        #endregion Fill methods

        /// <summary>
        /// Remove the ItemRelationship and ItemPages related to the specified segment and book
        /// </summary>
        /// <param name="segmentID"></param>
        /// <param name="bookID"></param>
        protected void removeItem(string segmentID, string bookID)
        {
            Segment segment = (Segment)Session["Segment" + segmentID];
            foreach (ItemRelationship relationship in segment.RelationshipList)
            {
                if (segment.ItemID == relationship.ChildID && bookID == relationship.BookID.ToString()) relationship.IsDeleted = true;
            }
            foreach (ItemPage page in segment.PageList) page.IsDeleted = true;
            bindSegmentPageData();
            Session["Segment" + segment.SegmentID.ToString()] = segment;

        }

        #region SegmentAuthor methods

        private void bindSegmentAuthorData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            List<ItemAuthor> segmentAuthors = new List<ItemAuthor>();
            foreach (ItemAuthor sa in segment.AuthorList)
            {
                if (sa.IsDeleted == false)
                {
                    segmentAuthors.Add(sa);
                }
            }

            ItemAuthorSequenceComparer comp = new ItemAuthorSequenceComparer();
            segmentAuthors.Sort(comp);
            authorsList.DataSource = segmentAuthors;
            authorsList.DataBind();
        }

        private ItemAuthor findSegmentAuthor(List<ItemAuthor> segmentAuthors, int segmentAuthorId,
            int authorId)
        {
            foreach (ItemAuthor ia in segmentAuthors)
            {
                if (ia.IsDeleted)
                {
                    continue;
                }
                if (segmentAuthorId == 0 && ia.ItemAuthorID == 0 && authorId == ia.AuthorID)
                {
                    return ia;
                }
                else if (segmentAuthorId > 0 && ia.ItemAuthorID == segmentAuthorId)
                {
                    return ia;
                }
            }

            return null;
        }

        #endregion

        #region SegmentKeyword methods

        private void bindKeywordData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            List<ItemKeyword> titleKeywords = new List<ItemKeyword>();
            foreach (ItemKeyword ik in segment.KeywordList)
            {
                if (ik.IsDeleted == false)
                {
                    titleKeywords.Add(ik);
                }
            }

            keywordsList.DataSource = titleKeywords;
            keywordsList.DataBind();
        }

        private ItemKeyword findSegmentKeyword(List<ItemKeyword> segmentKeywords,
            int segmentKeywordId, int keywordID, string keyword)
        {
            foreach (ItemKeyword ik in segmentKeywords)
            {
                if (ik.IsDeleted)
                {
                    continue;
                }
                if (segmentKeywordId == ik.ItemKeywordID &&
                    keywordID == ik.KeywordID &&
                    keyword == ik.Keyword)
                {
                    return ik;
                }
            }

            return null;
        }

        #endregion SegmentKeyword methods

        #region SegmentIdentifier methods

        private void bindSegmentIdentifierData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            List<ItemIdentifier> segmentIdentifiers = new List<ItemIdentifier>();
            foreach (ItemIdentifier ii in segment.IdentifierList)
            {
                if (ii.IsDeleted == false)
                {
                    segmentIdentifiers.Add(ii);
                }
            }

            identifiersList.DataSource = segmentIdentifiers;
            identifiersList.DataBind();
        }

        List<Identifier> _identifiers = null;
        protected List<Identifier> GetIdentifiers()
        {
            BHLProvider bp = new BHLProvider();
            _identifiers = bp.IdentifierSelectByIDType("segment");

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

        private ItemIdentifier findSegmentIdentifier(List<ItemIdentifier> segmentIdentifiers,
            int itemIdentifierId, int identifierID, string identifierValue)
        {
            foreach (ItemIdentifier ii in segmentIdentifiers)
            {
                if (ii.IsDeleted)
                {
                    continue;
                }
                if (itemIdentifierId == 0 && ii.ItemIdentifierID == 0 &&
                    identifierID == 0 && ii.IdentifierID == 0 &&
                    identifierValue == "" && ii.IdentifierValue == "")
                {
                    return ii;
                }
                if (itemIdentifierId == 0 && ii.ItemIdentifierID == 0 &&
                    identifierID > 0 && identifierID == ii.IdentifierID &&
                    identifierValue == ii.IdentifierValue)
                {
                    return ii;
                }
                else if (itemIdentifierId > 0 && ii.ItemIdentifierID == itemIdentifierId)
                {
                    return ii;
                }
            }

            return null;
        }

        #endregion

        #region SegmentPage methods

        private void bindSegmentPageData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            List<ItemPage> segmentPages = new List<ItemPage>();
            foreach (ItemPage ip in segment.PageList)
            {
                if (ip.IsDeleted == false)
                {
                    segmentPages.Add(ip);
                }
            }

            ItemPageSequenceComparer comp = new ItemPageSequenceComparer();
            segmentPages.Sort(comp);
            pagesList.DataSource = segmentPages;
            pagesList.DataBind();
        }

        private ItemPage findSegmentPage(List<ItemPage> segmentPages, int itemPageId,
            int pageId)
        {
            foreach (ItemPage ip in segmentPages)
            {
                if (ip.IsDeleted)
                {
                    continue;
                }
                if (itemPageId == 0 && ip.ItemPageID == 0 && pageId == ip.PageID)
                {
                    return ip;
                }
                else if (itemPageId > 0 && ip.ItemPageID == itemPageId)
                {
                    return ip;
                }
            }

            return null;
        }

        #endregion SegmentPage methods

        #region RelatedSegment methods

        private void bindRelatedSegmentData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted segments
            bool relatedPrimary = false;
            List<Segment> relatedSegments = new List<Segment>();
            foreach (Segment s in segment.RelatedSegmentList)
            {
                if (!s.IsDeleted)
                {
                    relatedSegments.Add(s);
                    relatedPrimary = relatedPrimary || (s.IsPrimary == 1);
                }
            }

            SegmentSequenceComparer comp = new SegmentSequenceComparer();
            relatedSegments.Sort(comp);

            litPrimaryMsg.Visible = (relatedSegments.Count > 0);
            chkPrimary.Visible = litPrimaryMsg.Visible;
            if (relatedPrimary) chkPrimary.Checked = false;

            relatedSegmentsList.DataSource = relatedSegments;
            relatedSegmentsList.DataBind();
        }

        private Segment findRelatedSegment(List<Segment> relatedSegments, int segmentId)
        {
            foreach (Segment s in relatedSegments)
            {
                if (s.IsDeleted)
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

        #endregion RelatedSegment methods

        #region Event Handlers

        #region Author event handlers

        protected void authorsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            authorsList.EditIndex = e.NewEditIndex;
            bindSegmentAuthorData();
        }

        protected void authorsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = authorsList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox sequenceTextBox = row.FindControl("authorSequenceTextBox") as TextBox;
                if (sequenceTextBox != null)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        // Find item being changed
                        ItemAuthor changedAuthor = findSegmentAuthor(segment.AuthorList,
                            (int)authorsList.DataKeys[e.RowIndex].Values[0],
                            (int)authorsList.DataKeys[e.RowIndex].Values[1]);

                        short oldSeq = changedAuthor.SequenceOrder;

                        if (changedAuthor != null)
                        {
                            // If sequence has been decreased
                            if (newSeq < oldSeq)
                            {
                                // Increment all item sequences between the old and new sequence values
                                foreach (ItemAuthor author in segment.AuthorList)
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
                                foreach (ItemAuthor author in segment.AuthorList)
                                {
                                    if (author.SequenceOrder <= newSeq && author.SequenceOrder > oldSeq)
                                    {
                                        author.SequenceOrder--;
                                        author.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            changedAuthor.SequenceOrder = newSeq;
                            changedAuthor.LastModifiedUserID = userId;
                        }
                    }
                }
            }

            authorsList.EditIndex = -1;
            bindSegmentAuthorData();
        }

        protected void authorsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            authorsList.EditIndex = -1;
            bindSegmentAuthorData();
        }

        protected void authorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemAuthor segmentAuthor = findSegmentAuthor(segment.AuthorList,
                    (int)authorsList.DataKeys[rowNum].Values[0],
                    (int)authorsList.DataKeys[rowNum].Values[1]);

                segmentAuthor.IsDeleted = true;
                bindSegmentAuthorData();
            }
        }

        #endregion Author event handlers

        #region SegmentKeyword event handlers

        protected void keywordsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            keywordsList.EditIndex = e.NewEditIndex;
            bindKeywordData();
        }

        protected void keywordsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = keywordsList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox txtKeyword = row.FindControl("txtKeyword") as TextBox;
                if (txtKeyword != null)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    String keyword = txtKeyword.Text;

                    ItemKeyword segmentKeyword = findSegmentKeyword(segment.KeywordList,
                        (int)keywordsList.DataKeys[e.RowIndex].Values[0],
                        (int)keywordsList.DataKeys[e.RowIndex].Values[1],
                        keywordsList.DataKeys[e.RowIndex].Values[2].ToString());

                    segmentKeyword.ItemID = segment.ItemID;
                    segmentKeyword.Keyword = keyword;
                }
            }

            keywordsList.EditIndex = -1;
            bindKeywordData();
        }

        protected void keywordsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            keywordsList.EditIndex = -1;
            bindKeywordData();
        }

        protected void addKeywordButton_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            ItemKeyword segmentKeyword = new ItemKeyword();
            segmentKeyword.ItemID = segment.ItemID;
            segment.KeywordList.Add(segmentKeyword);
            keywordsList.EditIndex = keywordsList.Rows.Count;
            bindKeywordData();
        }

        protected void keywordsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemKeyword segmentKeyword = findSegmentKeyword(segment.KeywordList,
                    (int)keywordsList.DataKeys[rowNum].Values[0],
                    (int)keywordsList.DataKeys[rowNum].Values[1],
                    keywordsList.DataKeys[rowNum].Values[2].ToString());

                segmentKeyword.IsDeleted = true;
                bindKeywordData();
            }
        }

        #endregion SegmentKeyword event handlers

        #region SegmentIdentifier event handlers

        protected void identifiersList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            identifiersList.EditIndex = e.NewEditIndex;
            bindSegmentIdentifierData();
        }

        protected void identifiersList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = identifiersList.Rows[e.RowIndex];

            if (row != null)
            {
                DropDownList ddlIdentifierName = row.FindControl("ddlIdentifierName") as DropDownList;
                TextBox txtIdentifierValue = row.FindControl("txtIdentifierValue") as TextBox;
                CheckBox cbIsContainerIdentifier = row.FindControl("isContainerIdentifierCheckBoxEdit") as CheckBox;
                if (ddlIdentifierName != null && txtIdentifierValue != null)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    int identifierId = int.Parse(ddlIdentifierName.SelectedValue);
                    String identifierValue = txtIdentifierValue.Text;

                    ItemIdentifier segmentIdentifier = findSegmentIdentifier(segment.IdentifierList,
                        (int)identifiersList.DataKeys[e.RowIndex].Values[0],
                        (int)identifiersList.DataKeys[e.RowIndex].Values[1],
                        identifiersList.DataKeys[e.RowIndex].Values[2].ToString());

                    segmentIdentifier.ItemID = segment.ItemID;
                    segmentIdentifier.IdentifierID = identifierId;
                    segmentIdentifier.IdentifierName = ddlIdentifierName.SelectedItem.Text;
                    segmentIdentifier.IdentifierValue = identifierValue;
                }
            }

            identifiersList.EditIndex = -1;
            bindSegmentIdentifierData();
        }

        protected void identifiersList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            identifiersList.EditIndex = -1;
            bindSegmentIdentifierData();
        }

        protected void addSegmentIdentifierButton_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            ItemIdentifier si = new ItemIdentifier();
            si.ItemID = segment.ItemID;
            segment.IdentifierList.Add(si);
            identifiersList.EditIndex = identifiersList.Rows.Count;
            bindSegmentIdentifierData();
        }

        protected void identifiersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemIdentifier segmentIdentifier = findSegmentIdentifier(segment.IdentifierList,
                    (int)identifiersList.DataKeys[rowNum].Values[0],
                    (int)identifiersList.DataKeys[rowNum].Values[1],
                    identifiersList.DataKeys[rowNum].Values[2].ToString());

                segmentIdentifier.IsDeleted = true;
                bindSegmentIdentifierData();
            }
        }

        #endregion SegmentIdentifier event handlers

        #region SegmentPage event handlers

        protected void pagesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            pagesList.EditIndex = e.NewEditIndex;
            bindSegmentPageData();
        }

        protected void pagesList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = pagesList.Rows[e.RowIndex];

            if (row != null)
            {
                TextBox sequenceTextBox = row.FindControl("pageSequenceTextBox") as TextBox;
                if (sequenceTextBox != null)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        // Find item being changed
                        ItemPage changedPage = findSegmentPage(segment.PageList,
                            (int)pagesList.DataKeys[e.RowIndex].Values[0],
                            (int)pagesList.DataKeys[e.RowIndex].Values[1]);

                        int oldSeq = changedPage.SequenceOrder;

                        if (changedPage != null)
                        {
                            // If sequence has been decreased
                            if (newSeq < oldSeq)
                            {
                                // Increment all item sequences between the old and new sequence values
                                foreach (ItemPage page in segment.PageList)
                                {
                                    if (page.SequenceOrder >= newSeq && page.SequenceOrder < oldSeq)
                                    {
                                        page.SequenceOrder++;
                                        page.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            // If sequence has been increased
                            if (newSeq > oldSeq)
                            {
                                // Decrement all item sequences between the old and new sequence values
                                foreach (ItemPage page in segment.PageList)
                                {
                                    if (page.SequenceOrder <= newSeq && page.SequenceOrder > oldSeq)
                                    {
                                        page.SequenceOrder--;
                                        page.LastModifiedUserID = userId;
                                    }
                                }
                            }

                            changedPage.SequenceOrder = newSeq;
                            changedPage.LastModifiedUserID = userId;
                        }
                    }
                }
            }

            pagesList.EditIndex = -1;
            bindSegmentPageData();
        }

        protected void pagesList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            pagesList.EditIndex = -1;
            bindSegmentPageData();
        }

        protected void pagesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemPage segmentPage = findSegmentPage(segment.PageList,
                    (int)pagesList.DataKeys[rowNum].Values[0],
                    (int)pagesList.DataKeys[rowNum].Values[1]);

                segmentPage.IsDeleted = true;
                bindSegmentPageData();
            }
        }

        #endregion SegmentPage event handlers

        #region chkPrimary event handlers

        protected void chkPrimary_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // if the chkPrimary checkbox has been selected, turn off the IsPrimary flag for all related segments
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                List<Segment> relatedSegments = new List<Segment>();
                foreach (Segment s in segment.RelatedSegmentList)
                {
                    s.IsPrimary = 0;
                }
            }

            bindRelatedSegmentData();
        }

        #endregion chkPrimary event handlers

        #region RelatedSegment event handlers

        protected void relatedSegmentsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            relatedSegmentsList.EditIndex = e.NewEditIndex;
            bindRelatedSegmentData();
        }

        protected void relatedSegmentsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                DropDownList dropDownList = e.Row.FindControl("ddlClusterType") as DropDownList;
                if (dropDownList != null)
                {
                    Segment currentSegment = findRelatedSegment(segment.RelatedSegmentList,
                        (int)relatedSegmentsList.DataKeys[e.Row.RowIndex].Values[0]);

                    dropDownList.SelectedValue = currentSegment.SegmentClusterTypeId.ToString();
                }
            }
        }

        protected void relatedSegmentsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = relatedSegmentsList.Rows[e.RowIndex];

            if (row != null)
            {
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                DropDownList dropDownList = row.FindControl("ddlClusterType") as DropDownList;
                if (dropDownList != null)
                {
                    Segment currentSegment = findRelatedSegment(segment.RelatedSegmentList,
                        (int)relatedSegmentsList.DataKeys[e.RowIndex].Values[0]);

                    if (currentSegment.SegmentClusterTypeId != Convert.ToInt32(dropDownList.SelectedValue))
                    {
                        currentSegment.SegmentClusterTypeId = Convert.ToInt32(dropDownList.SelectedValue);
                        currentSegment.SegmentClusterTypeLabel = dropDownList.Items[dropDownList.SelectedIndex].Text;
                        currentSegment.LastModifiedDate = DateTime.Now;
                    }
                }

                CheckBox checkBox = row.FindControl("isPrimaryCheckBoxEdit") as CheckBox;
                if (checkBox != null)
                {
                    List<Segment> relatedSegments = segment.RelatedSegmentList;
                    
                    short isPrimary = (short)(checkBox.Checked ? 1 : 0);

                    String segmentIdString = row.Cells[1].Text;
                    int segmentId = 0;
                    int.TryParse(segmentIdString, out segmentId);

                    if (segmentId > 0)
                    {
                        // Update primary segment
                        foreach (Segment relatedSegment in relatedSegments)
                        {
                            // Set "LastModifiedDate" on changed segments to force the "IsDirty" flag to "true"
                            if (segmentId == relatedSegment.SegmentID)
                            {
                                // Update selected segment
                                if (relatedSegment.IsPrimary != isPrimary)
                                {
                                    relatedSegment.IsPrimary = isPrimary;
                                    relatedSegment.LastModifiedDate = DateTime.Now;
                                }
                            }
                            else if (isPrimary == 1)
                            {
                                // If selecting new primary segment, turn off primary flag for all other related segments
                                if (relatedSegment.IsPrimary != 0)
                                {
                                    relatedSegment.IsPrimary = 0;
                                    relatedSegment.LastModifiedDate = DateTime.Now;
                                }

                                // Also turn off primary flag for segment being editing
                                if (segment.IsPrimary != 0)
                                {
                                    segment.IsPrimary = 0;
                                    segment.LastModifiedDate = DateTime.Now;
                                }
                            }
                        }
                    }
                }
            }

            relatedSegmentsList.EditIndex = -1;
            bindRelatedSegmentData();
        }

        protected void relatedSegmentsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            relatedSegmentsList.EditIndex = -1;
            bindRelatedSegmentData();
        }

        protected void relatedSegmentsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                Segment currentSegment = findRelatedSegment(segment.RelatedSegmentList,
                    (int)relatedSegmentsList.DataKeys[rowNum].Values[0]);

                currentSegment.IsDeleted = true;
                bindRelatedSegmentData();
            }
        }

        #endregion RelatedSegment event handlers
        
        protected void saveButton_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            int? userId = null;

            if (validate(segment))
            {
                BHLProvider bp = new BHLProvider();

                // Set the id of the editing user
                userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                // Build PublicationDetails and PageRange (if necessary)
                if (publicationDetailsTextBox.Text.Trim() == string.Empty)
                {
                    if (publisherPlaceTextBox.Text.Trim() == string.Empty)
                        publicationDetailsTextBox.Text = publisherNameTextBox.Text.Trim();
                    else
                        publicationDetailsTextBox.Text = (publisherPlaceTextBox.Text.Trim() + ": " + publisherNameTextBox.Text.Trim()).Trim();
                }

                if (pageRangeTextBox.Text.Trim() == string.Empty)
                {
                    if (startPageTextBox.Text.Trim() != string.Empty)
                    {
                        pageRangeTextBox.Text = startPageTextBox.Text.Trim();
                        if (endPageTextBox.Text.Trim() != string.Empty) pageRangeTextBox.Text += "--" + endPageTextBox.Text.Trim();
                    }
                }

                // Gather up data on form
                bool isItemChanged = (segment.BookID ?? 0) != (itemIDLabel.Text == "" ? 0 : Convert.ToInt32(itemIDLabel.Text));
                segment.BookID = (itemIDLabel.Text == "" ? (int?)null : Convert.ToInt32(itemIDLabel.Text));
                segment.DOIName = doiTextBox.Text.Trim();
                segment.RedirectSegmentID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
                segment.SegmentStatusID = Convert.ToInt32(ddlSegmentStatus.SelectedValue);
                segment.SegmentGenreID = Convert.ToInt32(ddlSegmentGenre.SelectedValue);
                string contributorCode = (ddlContributor.SelectedValue.Length == 0 ? null : ddlContributor.SelectedValue);
                segment.Title = titleTextBox.Text.Trim();
                segment.SortTitle = sortTitleTextBox.Text.Trim();
                segment.TranslatedTitle = translatedTitleTextBox.Text.Trim();
                segment.ContainerTitle = containerTitleTextBox.Text.Trim();
                segment.PublicationDetails = publicationDetailsTextBox.Text.Trim();
                segment.PublisherPlace = publisherPlaceTextBox.Text.Trim();
                segment.PublisherName = publisherNameTextBox.Text.Trim();
                segment.Volume = volumeTextBox.Text.Trim();
                segment.Series = seriesTextBox.Text.Trim();
                segment.Issue = issueTextBox.Text.Trim();
                segment.Date = dateTextBox.Text.Trim();
                segment.PageRange = pageRangeTextBox.Text.Trim();
                segment.StartPageNumber = startPageTextBox.Text.Trim();
                segment.EndPageNumber = endPageTextBox.Text.Trim();
                segment.StartPageID = bhlStartPageIDTextBox.Text == "" ? (int?)null : Convert.ToInt32(bhlStartPageIDTextBox.Text);
                segment.LanguageCode = (ddlLanguage.SelectedValue.Length == 0 ? null : ddlLanguage.SelectedValue);
                segment.Url = urlTextBox.Text.Trim();
                segment.DownloadUrl = downloadUrlTextBox.Text.Trim();
                segment.RightsStatus = rightsStatusTextBox.Text.Trim();
                segment.RightsStatement = rightsStatementTextBox.Text.Trim();
                segment.LicenseName = licenseNameTextBox.Text.Trim();
                segment.LicenseUrl = licenseUrlTextBox.Text.Trim();
                segment.Notes = notesTextBox.Text.Trim();
                segment.Summary = summaryTextBox.Text.Trim();
                segment.IsPrimary = (short)(chkPrimary.Checked ? 1 : 0);
                segment.IsNew = (segment.SegmentID == 0);

                //----------------------------------------

                // Update the Item information
                if (segment.Item == null)
                {
                    segment.Item = new Item
                    {
                        ItemTypeID = 20,    // 10 = Book, 20 = Segment
                        ItemStatusID = segment.SegmentStatusID,
                        IsNew = true
                    };
                }
                bool updated = false;
                if (segment.SegmentStatusID != segment.Item.ItemStatusID) { segment.Item.ItemStatusID = segment.SegmentStatusID; updated = true; }
                if (segment.Notes.CompareTo(segment.Item.Note) != 0) { segment.Item.Note = segment.Notes; updated = true; }
                if (updated) segment.Item.LastModifiedDate = DateTime.Now;

                //----------------------------------------

                // Update the ItemRelationship records if the Item has changed
                if (isItemChanged)
                {
                    Book book = null;
                    if (segment.BookID != null) book = bp.BookSelectAuto((int)segment.BookID);

                    bool existing = false;
                    foreach(ItemRelationship ir in segment.RelationshipList)
                    {
                        if (ir.IsChild == 1 && !ir.IsDeleted)
                        {
                            existing = true;
                            if (segment.BookID == null)
                                ir.IsDeleted = true;
                            else
                                ir.ParentID = book.ItemID;
                        }
                    }

                    if (!existing && book != null)
                    {
                        int maxSeq = (from x in segment.RelationshipList where !(x.IsDeleted) select x.SequenceOrder).DefaultIfEmpty(0).Max();
                        segment.RelationshipList.Add(
                            new ItemRelationship
                            {
                                ParentID = book.ItemID,
                                ChildID = segment.ItemID,
                                SequenceOrder = maxSeq + 1,
                                IsNew = true
                            });
                    }
                }

                //----------------------------------------

                // Mark for deletion any contributors that have been removed
                bool contributorExists = false;
                bool contributor2Exists = false;
                foreach (Institution institution in segment.ContributorList)
                {
                    if (institution.InstitutionCode != ddlContributor.SelectedValue &&
                        institution.InstitutionCode != ddlContributor2.SelectedValue)
                    {
                        institution.IsDeleted = true;
                    }
                    else
                    {
                        if (institution.InstitutionCode == ddlContributor.SelectedValue) contributorExists = true;
                        if (institution.InstitutionCode == ddlContributor2.SelectedValue) contributor2Exists = true;
                    }
                }

                // Add new contributors
                if (!contributorExists && ddlContributor.SelectedValue != string.Empty)
                {
                    Institution newContributor = new Institution();
                    newContributor.InstitutionCode = ddlContributor.SelectedValue;
                    newContributor.InstitutionRoleName = InstitutionRole.Contributor;
                    newContributor.IsNew = true;
                    segment.ContributorList.Add(newContributor);
                }

                if (!contributor2Exists && ddlContributor2.SelectedValue != string.Empty && 
                    ddlContributor.SelectedValue != ddlContributor2.SelectedValue)
                {
                    Institution newContributor = new Institution();
                    newContributor.InstitutionCode = ddlContributor2.SelectedValue;
                    newContributor.InstitutionRoleName = InstitutionRole.Contributor;
                    newContributor.IsNew = true;
                    segment.ContributorList.Add(newContributor);
                }

                //----------------------------------------

                // Forces deletes to happen first
                segment.ContributorList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                segment.IdentifierList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                segment.AuthorList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                segment.KeywordList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                segment.PageList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));

                // If the ItemID has been modified, then reset the sequenceorder.  If other segments exist on the selected
                // Item, make this segment the last one (with the highest sequence number).
                if (isItemChanged)
                {
                    segment.SequenceOrder = (short)((segment.BookID == null) ? 1 : (bp.SegmentSelectByBookID((int)segment.BookID).Count + 1));
                }

                // Don't catch errors... allow global error handler to take over
                int segmentID = bp.SegmentSave(segment, (int)userId);

                // After a successful save operation, reload the title
                fillUI(segmentID);
            }
            else
            {
                return;
            }

            litMessage.Text = "<span class='liveData'>Segment Saved.</span>";
            Page.MaintainScrollPositionOnPostBack = false;
        }

        #endregion Event Handlers

        private bool validate(Segment segment)
        {
            bool flag = false;

            // Check that all edits were completed
            if (authorsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Authors has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (keywordsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Keywords has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            if (pagesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Pages has an edit pending.  Click \"Update\" to accept the change or \"Cancel\" to reject it.");
            }

            // If a "replaced by" identifer was specified, make sure that it is a valid id
            if (replacedByTextBox.Text.Trim().Length > 0)
            {
                int segmentID;
                if (Int32.TryParse(replacedByTextBox.Text, out segmentID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().SegmentSelectAuto(segmentID) == null)
                    {
                        flag = true;
                        errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Segment ID.");
                    }
                }
                else
                {
                    // Specified ID is not a valid integer value
                    flag = true;
                    errorControl.AddErrorText("Make sure the 'Replaced By' identifier is a valid Segment ID.");
                }
            }

            // If a BHL Page identifier was specified, make sure that it is a valid id
            if (bhlStartPageIDTextBox.Text.Trim().Length > 0)
            {
                int pageID;
                if (Int32.TryParse(bhlStartPageIDTextBox.Text, out pageID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().PageSelectAuto(pageID) == null)
                    {
                        flag = true;
                        errorControl.AddErrorText("Make sure the 'Start Page BHL ID' is a valid Page ID.");
                    }
                }
                else
                {
                    // Specified ID is not a valid integer value
                    flag = true;
                    errorControl.AddErrorText("Make sure the 'Start Page BHL ID' is a valid Page ID.");
                }
            }

            // Validate other inputs
            if (titleTextBox.Text.Trim().Length == 0)
            {
                flag = true;
                errorControl.AddErrorText("Title is missing");
            }

            // Check for duplicate authors
            bool br = false;
            int ix = 0;
            foreach (ItemAuthor ia in segment.AuthorList)
            {
                if (ia.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (ItemAuthor ia2 in segment.AuthorList)
                    {
                        if (ia2.IsDeleted == false)
                        {
                            if ((ia.ItemAuthorID != ia2.ItemAuthorID && ia.AuthorID == ia2.AuthorID) ||
                                (ia.ItemAuthorID == 0 && ia.ItemAuthorID == 0 && ia.AuthorID == ia2.AuthorID && ix != iy))
                            {
                                br = true;
                                flag = true;
                                errorControl.AddErrorText("Cannot duplicate segment authors");
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

            // Check for duplicate pages
            br = false;
            ix = 0;
            foreach (ItemPage sp in segment.PageList)
            {
                if (sp.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (ItemPage sp2 in segment.PageList)
                    {
                        if (sp2.IsDeleted == false)
                        {
                            if ((sp.ItemPageID != sp2.ItemPageID && sp.PageID == sp2.PageID) ||
                                (sp.ItemPageID == 0 && sp.ItemPageID == 0 && sp.PageID == sp2.PageID && ix != iy))
                            {
                                br = true;
                                flag = true;
                                errorControl.AddErrorText("Cannot duplicate segment pages");
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
    }
}