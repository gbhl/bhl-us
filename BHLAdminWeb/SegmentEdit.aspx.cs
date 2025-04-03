using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentEdit : System.Web.UI.Page
    {
        protected string _virtualOnly = "false";
        protected List<string> _warnings = new List<string>();

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
            litWarning.Text = "";
            errorControl.Visible = false;

            if (!IsPostBack)
            {
                string idString = Request.QueryString["id"];
                if (idString != null && int.TryParse(idString, out int id))
                {
                    FillCombos();
                    FillUI(id);
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
                        RemoveItem(idLabel.Text, itemIDLabel.Text);
                        itemIDLabel.Text = "";
                        itemDescLabel.Text = "Not Selected";
                        FillItemDetails("", "", "", "");
                        ddlPreferredContainerTitleID.Items.Clear();
                        FillPreferredContainerDD(null, null);
                    }
                }
                else if (itemIDLabel.Text != selectedItemId)
                {
                    // Get the description of the newly selected item
                    Book book = provider.BookSelectByBarcodeOrItemID(Convert.ToInt32(selectedItemId), null);
                    List<Institution> institutions = provider.InstitutionSelectByItemIDAndRole(Convert.ToInt32(book.ItemID), InstitutionRole.Contributor);
                    DataObjects.Title title = provider.TitleSelectAuto((int)book.PrimaryTitleID);

                    string oldBookID = itemIDLabel.Text;
                    itemIDLabel.Text = selectedItemId;
                    itemDescLabel.Text = title.ShortTitle + " " + book.Volume;

                    // Use Title/Item metadata to set the journal details for this segment
                    ddlPreferredContainerTitleID.Items.Clear();

                    if (institutions.Count > 0) ddlContributor.SelectedValue = institutions[0].InstitutionCode;
                    FillPreferredContainerDD(Convert.ToInt32(selectedItemId), null);
                    containerTitleTextBox.Text = title.FullTitle ?? string.Empty;
                    publicationDetailsTextBox.Text = title.PublicationDetails ?? string.Empty;
                    publisherNameTextBox.Text = title.Datafield_260_b ?? string.Empty;
                    publisherPlaceTextBox.Text = title.Datafield_260_a ?? string.Empty;
                    ddlLanguage.SelectedValue = book.LanguageCode ?? string.Empty;

                    FillItemDetails((book.Volume ?? string.Empty),
                        string.Join("-", (new string[] { book.StartIssue ?? string.Empty, book.EndIssue ?? string.Empty }).Where(s => !string.IsNullOrEmpty(s))),
                        string.Join("-", (new string[] { book.StartSeries ?? string.Empty, book.EndSeries ?? string.Empty }).Where(s => !string.IsNullOrEmpty(s))),
                        string.Join("-", (new string[] { book.StartYear ?? string.Empty, book.EndYear ?? string.Empty }).Where(s => !string.IsNullOrEmpty(s))));

                    // Update the list of segments associated with this item
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    if (!string.IsNullOrWhiteSpace(oldBookID)) RemoveItem(idLabel.Text, oldBookID);
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
                    this.BindSegmentAuthorData();
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
                    this.BindRelatedSegmentData();
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
                            foreach (DataObjects.Page page in pages)
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
                    this.BindSegmentPageData();
                }
            }

            _virtualOnly = string.IsNullOrWhiteSpace(sourceIdLabel.Text) ? "false" : "true";

            editHistoryControl.EntityName = "segment";
            editHistoryControl.EntityId = idLabel.Text;

            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill methods

        private void FillCombos()
        {
            BHLProvider bp = new BHLProvider();

            List<ItemStatus> statuses = bp.SegmentStatusSelectAll();
            ddlSegmentStatus.DataSource = statuses;
            ddlSegmentStatus.DataBind();

            List<SegmentGenre> genres= bp.SegmentGenreSelectAll();
            ddlSegmentGenre.DataSource = genres;
            ddlSegmentGenre.DataBind();

            List<Institution> institutions = bp.InstituationSelectAll();

            Institution emptyInstitution = new Institution
            {
                InstitutionCode = string.Empty,
                InstitutionName = string.Empty
            };
            institutions.Insert(0, emptyInstitution);

            ddlContributor.DataSource = institutions;
            ddlContributor.DataBind();

            ddlContributor2.DataSource = institutions;
            ddlContributor2.DataBind();

            List<Language> languages = bp.LanguageSelectAll();

            Language emptyLanguage = new Language
            {
                LanguageCode = string.Empty,
                LanguageName = string.Empty
            };
            languages.Insert(0, emptyLanguage);

            ddlLanguage.DataSource = languages;
            ddlLanguage.DataBind();
        }

        private void FillPreferredContainerDD(int? bookID, int? preferredContainerTitleID)
        {
            if (bookID != null)
            {
                // If necesssary, populate the preferred container id dropdown
                if (ddlPreferredContainerTitleID.Items.Count == 0)
                {
                    ddlPreferredContainerTitleID.Items.Add(new ListItem("(Primary Title of associated Item)", ""));
                    List<ItemTitle> titles = new BHLProvider().ItemTitleSelectByItem((int)bookID);
                    foreach (ItemTitle title in titles)
                    {
                        if (title.TitlePublishReady)
                        {
                            ListItem li = new ListItem(
                                string.Format("{0} - {1}{2}", title.TitleID.ToString(), title.ShortTitle, title.IsPrimary == 1 ? " [PRIMARY]" : ""),
                                title.TitleID.ToString()
                                );
                            ddlPreferredContainerTitleID.Items.Add(li);
                        }
                    }
                }

                ddlPreferredContainerTitleID.SelectedValue = "";
                if (preferredContainerTitleID != null)
                {
                    ddlPreferredContainerTitleID.SelectedValue = preferredContainerTitleID.ToString();
                }
                else
                {
                    ddlPreferredContainerTitleID.SelectedIndex = 0;
                }

                containerTitleTextBox.Visible = false;
                ddlPreferredContainerTitleID.Visible= true;
            }
            else
            {
                containerTitleTextBox.Visible = true;
                ddlPreferredContainerTitleID.Visible = false;
            }
        }

        private void FillUI(int id)
        {
            BHLProvider bp = new BHLProvider();

            idLabel.Text = id.ToString();

            // Look up segment
            Segment segment;
            if (id == 0)
            {
                // Create new segment
                segment = new Segment
                {
                    SequenceOrder = 1
                };
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

                if (segment.BookID != null)
                {
                    selectedItem.Value = segment.BookID.ToString();
                    itemIDLabel.Text = segment.BookID.ToString();
                    itemDescLabel.Text = segment.TitleShortTitle + " " + segment.ItemVolume;
                }
                sourceIdLabel.Text = segment.BarCode;
                replacedByTextBox.Text = segment.RedirectSegmentID.ToString();
                titleTextBox.Text = segment.Title;
                sortTitleTextBox.Text = segment.SortTitle;
                translatedTitleTextBox.Text = segment.TranslatedTitle;
                FillPreferredContainerDD(segment.BookID, segment.PreferredContainerTitleID);
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
                FillItemDetails(segment.ItemVolume, segment.ItemIssue, segment.ItemSeries, segment.ItemYear);
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
                        ddlContributor2.SelectedIndex = 0;
                        break;
                    case 1:
                        ddlContributor.SelectedValue = segment.ContributorList[0].InstitutionCode;
                        ddlContributor2.SelectedIndex = 0;
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

                resourcesList.DataSource = segment.SegmentExternalResources;
                resourcesList.DataBind();

                identifiersList.DataSource = segment.IdentifierList;
                identifiersList.DataBind();

                pagesList.Columns[0].Visible = string.IsNullOrWhiteSpace(segment.BarCode);  // Delete link
                pagesList.Columns[5].Visible = string.IsNullOrWhiteSpace(segment.BarCode);  // Edit link
                pagesList.Columns[6].Visible = !string.IsNullOrWhiteSpace(segment.BarCode); // Edit Names link
                btnAddPage.Visible = string.IsNullOrWhiteSpace(segment.BarCode);
                btnPaginator.Visible = !string.IsNullOrWhiteSpace(segment.BarCode);
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

        private void FillItemDetails(string volume, string issue, string series, string date)
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
        protected void RemoveItem(string segmentID, string bookID)
        {
            Segment segment = (Segment)Session["Segment" + segmentID];
            foreach (ItemRelationship relationship in segment.RelationshipList)
            {
                if (segment.ItemID == relationship.ChildID && bookID == relationship.BookID.ToString()) relationship.IsDeleted = true;
            }

            // If this segment is not based on an IA item, then remove the pages
            if (string.IsNullOrWhiteSpace(segment.BarCode))
            {
                foreach (ItemPage page in segment.PageList) page.IsDeleted = true;
                BindSegmentPageData();
            }

            Session["Segment" + segment.SegmentID.ToString()] = segment;
        }

        #region SegmentAuthor methods

        private void BindSegmentAuthorData()
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

        private ItemAuthor FindSegmentAuthor(List<ItemAuthor> segmentAuthors, int segmentAuthorId, int authorId)
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

        private void BindKeywordData()
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

        private ItemKeyword FindSegmentKeyword(List<ItemKeyword> segmentKeywords,
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

        #region SegmentExternalResource methods

        private void bindExternalResourceData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            List<SegmentExternalResource> segmentExternalResources = new List<SegmentExternalResource>();
            foreach (SegmentExternalResource sr in segment.SegmentExternalResources)
            {
                if (sr.IsDeleted == false)
                {
                    segmentExternalResources.Add(sr);
                }
            }

            resourcesList.DataSource = segmentExternalResources;
            resourcesList.DataBind();
        }

        private SegmentExternalResource findSegmentExternalResource(List<SegmentExternalResource> segmentExternalResources,
            int segmentExternalResourceId, int externalResourceTypeID, string urlText, string url)
        {
            foreach (SegmentExternalResource sr in segmentExternalResources)
            {
                if (sr.IsDeleted)
                {
                    continue;
                }
                if (segmentExternalResourceId == sr.SegmentExternalResourceID &&
                    externalResourceTypeID == sr.ExternalResourceTypeID &&
                    urlText == sr.UrlText &&
                    url == sr.Url)
                {
                    return sr;
                }
            }

            return null;
        }

        protected short GetMaxExternalResourceSequence()
        {
            short maxSeq = 0;

            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            foreach (SegmentExternalResource segmentExternalResource in segment.SegmentExternalResources)
            {
                if (!segmentExternalResource.IsDeleted)
                {
                    if (segmentExternalResource.SequenceOrder > maxSeq) maxSeq = segmentExternalResource.SequenceOrder;
                }
            }

            return maxSeq;
        }

        List<ExternalResourceType> _externalResourceTypes = null;
        protected List<ExternalResourceType> GetExternalResourceTypes()
        {
            BHLProvider bp = new BHLProvider();
            _externalResourceTypes = bp.ExternalResourceTypeSelectByIDType("segment");

            return _externalResourceTypes;
        }

        protected int GetExternalResourceIndex(object dataItem)
        {
            string externalResourceTypeIdString = DataBinder.Eval(dataItem, "ExternalResourceTypeID").ToString();

            if (!externalResourceTypeIdString.Equals("0"))
            {
                int externalResourceTypeId = int.Parse(externalResourceTypeIdString);
                int ix = 0;
                foreach (ExternalResourceType externalResourceType in _externalResourceTypes)
                {
                    if (externalResourceType.ExternalResourceTypeID == externalResourceTypeId)
                    {
                        return ix;
                    }
                    ix++;
                }
            }

            return 0;
        }

        #endregion

        #region SegmentIdentifier methods

        private void BindSegmentIdentifierData()
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

        private ItemIdentifier FindSegmentIdentifier(List<ItemIdentifier> segmentIdentifiers,
            int itemIdentifierId, int identifierID, string identifierValue)
        {
            foreach (ItemIdentifier ii in segmentIdentifiers)
            {
                if (ii.IsDeleted)
                {
                    continue;
                }
                if (itemIdentifierId == ii.ItemIdentifierID &&
                    identifierID == ii.IdentifierID &&
                    identifierValue == ii.IdentifierValue)
                {
                    return ii;
                }
            }

            return null;
        }

        #endregion

        #region SegmentPage methods

        private void BindSegmentPageData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            List<ItemPage> segmentPages = new List<ItemPage>();
            foreach (ItemPage ip in segment.PageList)
            {
                if (ip.IsDeleted == false) segmentPages.Add(ip);
            }

            ItemPageSequenceComparer comp = new ItemPageSequenceComparer();
            segmentPages.Sort(comp);
            pagesList.DataSource = segmentPages;
            pagesList.DataBind();
        }

        private ItemPage FindSegmentPage(List<ItemPage> segmentPages, int itemPageId, int pageId)
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

        private void BindRelatedSegmentData()
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

        private Segment FindRelatedSegment(List<Segment> relatedSegments, int segmentId)
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

        protected void AuthorsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            authorsList.EditIndex = e.NewEditIndex;
            BindSegmentAuthorData();
        }

        protected void AuthorsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = authorsList.Rows[e.RowIndex];

            if (row != null)
            {
                if (row.FindControl("authorSequenceTextBox") is TextBox sequenceTextBox)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short.TryParse(newSeqString, out short newSeq);

                    if (newSeq > 0)
                    {
                        // Find item being changed
                        ItemAuthor changedAuthor = FindSegmentAuthor(segment.AuthorList,
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
            BindSegmentAuthorData();
        }

        protected void AuthorsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            authorsList.EditIndex = -1;
            BindSegmentAuthorData();
        }

        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                authorsList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemAuthor segmentAuthor = FindSegmentAuthor(segment.AuthorList,
                    (int)authorsList.DataKeys[rowNum].Values[0],
                    (int)authorsList.DataKeys[rowNum].Values[1]);

                segmentAuthor.IsDeleted = true;
                BindSegmentAuthorData();
            }
        }

        #endregion Author event handlers

        #region SegmentKeyword event handlers

        protected void KeywordsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            keywordsList.EditIndex = e.NewEditIndex;
            BindKeywordData();
        }

        protected void KeywordsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = keywordsList.Rows[e.RowIndex];

            if (row != null)
            {
                if (row.FindControl("txtKeyword") is TextBox txtKeyword)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    string keyword = txtKeyword.Text;

                    ItemKeyword segmentKeyword = FindSegmentKeyword(segment.KeywordList,
                        (int)keywordsList.DataKeys[e.RowIndex].Values[0],
                        (int)keywordsList.DataKeys[e.RowIndex].Values[1],
                        keywordsList.DataKeys[e.RowIndex].Values[2].ToString());

                    segmentKeyword.ItemID = segment.ItemID;
                    segmentKeyword.Keyword = keyword;
                }
            }

            keywordsList.EditIndex = -1;
            BindKeywordData();
        }

        protected void KeywordsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            keywordsList.EditIndex = -1;
            BindKeywordData();
        }

        protected void AddKeywordButton_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            ItemKeyword segmentKeyword = new ItemKeyword
            {
                ItemID = segment.ItemID
            };
            segment.KeywordList.Add(segmentKeyword);
            keywordsList.EditIndex = keywordsList.Rows.Count;
            BindKeywordData();
            keywordsList.Rows[keywordsList.EditIndex].FindControl("cancelKeywordCreatorButton").Visible = false;
        }

        protected void KeywordsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                keywordsList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemKeyword segmentKeyword = FindSegmentKeyword(segment.KeywordList,
                    (int)keywordsList.DataKeys[rowNum].Values[0],
                    (int)keywordsList.DataKeys[rowNum].Values[1],
                    keywordsList.DataKeys[rowNum].Values[2].ToString());

                segmentKeyword.IsDeleted = true;
                BindKeywordData();
            }
        }

        #endregion SegmentKeyword event handlers

        #region SegmentExternalResource event handlers

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
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];

                    string urlText = txtUrlText.Text;
                    string url = txtUrl.Text;
                    string resourceSequenceText = txtExternalResourceSequence.Text;
                    if (!short.TryParse(resourceSequenceText, out sequenceOrder)) sequenceOrder = 0;
                    int resourceTypeID;
                    if (!Int32.TryParse(ddlExternalResourceType.Text, out resourceTypeID)) resourceTypeID = 1;

                    SegmentExternalResource segmentExternalResource = findSegmentExternalResource(segment.SegmentExternalResources,
                        (int)resourcesList.DataKeys[e.RowIndex].Values[0],
                        (int)resourcesList.DataKeys[e.RowIndex].Values[1],
                        resourcesList.DataKeys[e.RowIndex].Values[2].ToString(),
                        resourcesList.DataKeys[e.RowIndex].Values[3].ToString());

                    // Update all sequences if necessary
                    short oldSeq = segmentExternalResource.SequenceOrder;

                    // If sequence has been decreased
                    if (sequenceOrder < oldSeq)
                    {
                        // Increment all sequences between the old and new sequence values
                        foreach (SegmentExternalResource resource in segment.SegmentExternalResources)
                        {
                            if (resource.SequenceOrder >= sequenceOrder && resource.SequenceOrder < oldSeq) resource.SequenceOrder++;
                        }
                    }

                    // If sequence has been increased
                    if (sequenceOrder > oldSeq)
                    {
                        // Decrement all sequences between the old and new sequence values
                        foreach (SegmentExternalResource resource in segment.SegmentExternalResources)
                        {
                            if (resource.SequenceOrder <= sequenceOrder && resource.SequenceOrder > oldSeq)
                            {
                                resource.SequenceOrder--;
                            }
                        }
                    }

                    // Update the external resource being edited
                    segmentExternalResource.SegmentID = segment.SegmentID;
                    segmentExternalResource.ExternalResourceTypeID = resourceTypeID;
                    segmentExternalResource.UrlText = urlText;
                    segmentExternalResource.Url = url;
                    segmentExternalResource.SequenceOrder = sequenceOrder;
                    segmentExternalResource.ExternalResourceTypeLabel = ddlExternalResourceType.SelectedItem.Text;
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
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                SegmentExternalResource externalResource = findSegmentExternalResource(segment.SegmentExternalResources,
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
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            SegmentExternalResource externalResource = new SegmentExternalResource();
            externalResource.SegmentID = segment.SegmentID;
            externalResource.ExternalResourceTypeID = 0;
            externalResource.SequenceOrder = GetMaxExternalResourceSequence();
            externalResource.SequenceOrder++;
            segment.SegmentExternalResources.Add(externalResource);
            resourcesList.EditIndex = resourcesList.Rows.Count;
            bindExternalResourceData();
            resourcesList.Rows[resourcesList.EditIndex].FindControl("cancelResourceEditButton").Visible = false;
        }

        #endregion

        #region SegmentIdentifier event handlers

        protected void IdentifiersList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            identifiersList.EditIndex = e.NewEditIndex;
            BindSegmentIdentifierData();
        }

        protected void IdentifiersList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = identifiersList.Rows[e.RowIndex];

            if (row != null)
            {
                if (row.FindControl("ddlIdentifierName") is DropDownList ddlIdentifierName && 
                    row.FindControl("txtIdentifierValue") is TextBox txtIdentifierValue)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    int identifierId = int.Parse(ddlIdentifierName.SelectedValue);
                    String identifierValue = txtIdentifierValue.Text;

                    ItemIdentifier segmentIdentifier = FindSegmentIdentifier(segment.IdentifierList,
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
            BindSegmentIdentifierData();
        }

        protected void IdentifiersList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            identifiersList.EditIndex = -1;
            BindSegmentIdentifierData();
        }

        protected void AddSegmentIdentifierButton_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            ItemIdentifier si = new ItemIdentifier
            {
                ItemID = segment.ItemID
            };
            segment.IdentifierList.Add(si);
            identifiersList.EditIndex = identifiersList.Rows.Count;
            BindSegmentIdentifierData();
            identifiersList.Rows[identifiersList.EditIndex].FindControl("cancelSegmentIdentifierButton").Visible = false;
        }

        protected void IdentifiersList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                identifiersList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemIdentifier segmentIdentifier = FindSegmentIdentifier(segment.IdentifierList,
                    (int)identifiersList.DataKeys[rowNum].Values[0],
                    (int)identifiersList.DataKeys[rowNum].Values[1],
                    identifiersList.DataKeys[rowNum].Values[2].ToString());

                segmentIdentifier.IsDeleted = true;
                BindSegmentIdentifierData();
            }
        }

        #endregion SegmentIdentifier event handlers

        #region SegmentPage event handlers

        protected void PagesList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            pagesList.EditIndex = e.NewEditIndex;
            BindSegmentPageData();
        }

        protected void PagesList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = pagesList.Rows[e.RowIndex];

            if (row != null)
            {
                if (row.FindControl("pageSequenceTextBox") is TextBox sequenceTextBox)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    int userId = Helper.GetCurrentUserUID(new HttpRequestWrapper(Request));

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short.TryParse(newSeqString, out short newSeq);

                    if (newSeq > 0)
                    {
                        // Find item being changed
                        ItemPage changedPage = FindSegmentPage(segment.PageList,
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
            BindSegmentPageData();
        }

        protected void PagesList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            pagesList.EditIndex = -1;
            BindSegmentPageData();
        }

        protected void PagesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                pagesList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                ItemPage segmentPage = FindSegmentPage(segment.PageList,
                    (int)pagesList.DataKeys[rowNum].Values[0],
                    (int)pagesList.DataKeys[rowNum].Values[1]);

                segmentPage.IsDeleted = true;
                BindSegmentPageData();
            }
        }

        #endregion SegmentPage event handlers

        #region chkPrimary event handlers

        protected void PrimaryCheckbox_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // if the chkPrimary checkbox has been selected, turn off the IsPrimary flag for all related segments
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                foreach (Segment s in segment.RelatedSegmentList) s.IsPrimary = 0;
            }

            BindRelatedSegmentData();
        }

        #endregion chkPrimary event handlers

        #region RelatedSegment event handlers

        protected void RelatedSegmentsList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            relatedSegmentsList.EditIndex = e.NewEditIndex;
            BindRelatedSegmentData();
        }

        protected void RelatedSegmentsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                if (e.Row.FindControl("ddlClusterType") is DropDownList dropDownList)
                {
                    Segment currentSegment = FindRelatedSegment(segment.RelatedSegmentList,
                        (int)relatedSegmentsList.DataKeys[e.Row.RowIndex].Values[0]);

                    dropDownList.SelectedValue = currentSegment.SegmentClusterTypeId.ToString();
                }
            }
        }

        protected void RelatedSegmentsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = relatedSegmentsList.Rows[e.RowIndex];

            if (row != null)
            {
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                if (row.FindControl("ddlClusterType") is DropDownList dropDownList)
                {
                    Segment currentSegment = FindRelatedSegment(segment.RelatedSegmentList,
                        (int)relatedSegmentsList.DataKeys[e.RowIndex].Values[0]);

                    if (currentSegment.SegmentClusterTypeId != Convert.ToInt32(dropDownList.SelectedValue))
                    {
                        currentSegment.SegmentClusterTypeId = Convert.ToInt32(dropDownList.SelectedValue);
                        currentSegment.SegmentClusterTypeLabel = dropDownList.Items[dropDownList.SelectedIndex].Text;
                        currentSegment.LastModifiedDate = DateTime.Now;
                    }
                }

                if (row.FindControl("isPrimaryCheckBoxEdit") is CheckBox checkBox)
                {
                    List<Segment> relatedSegments = segment.RelatedSegmentList;
                    
                    short isPrimary = (short)(checkBox.Checked ? 1 : 0);

                    String segmentIdString = row.Cells[1].Text;
                    int.TryParse(segmentIdString, out int segmentId);

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
            BindRelatedSegmentData();
        }

        protected void RelatedSegmentsList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            relatedSegmentsList.EditIndex = -1;
            BindRelatedSegmentData();
        }

        protected void RelatedSegmentsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("RemoveButton"))
            {
                relatedSegmentsList.EditIndex = -1;
                int rowNum = int.Parse(e.CommandArgument.ToString());
                Segment segment = (Segment)Session["Segment" + idLabel.Text];

                Segment currentSegment = FindRelatedSegment(segment.RelatedSegmentList,
                    (int)relatedSegmentsList.DataKeys[rowNum].Values[0]);

                currentSegment.IsDeleted = true;
                BindRelatedSegmentData();
            }
        }

        #endregion RelatedSegment event handlers

        protected void PaginatorButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginator.aspx?SegmentID=" + idLabel.Text);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];
            int? userId = null;

            try
            {
                if (Validate(segment))
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
                    segment.RedirectSegmentID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
                    segment.SegmentStatusID = Convert.ToInt32(ddlSegmentStatus.SelectedValue);
                    segment.SegmentGenreID = Convert.ToInt32(ddlSegmentGenre.SelectedValue);
                    string contributorCode = (ddlContributor.SelectedValue.Length == 0 ? null : ddlContributor.SelectedValue);
                    segment.Title = titleTextBox.Text.Trim();
                    segment.SortTitle = sortTitleTextBox.Text.Trim();
                    segment.TranslatedTitle = translatedTitleTextBox.Text.Trim();
                	segment.PreferredContainerTitleID = ddlPreferredContainerTitleID.SelectedValue == "" ? (int?)null : int.Parse(ddlPreferredContainerTitleID.SelectedValue);
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
                        segment.Item = new DataObjects.Item
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
                        foreach (ItemRelationship ir in segment.RelationshipList)
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
                    Institution newContributor = new Institution
                    {
                        InstitutionCode = ddlContributor.SelectedValue,
                        InstitutionRoleName = InstitutionRole.Contributor,
                        IsNew = true
                    };
                        segment.ContributorList.Add(newContributor);
                    }

                    if (!contributor2Exists && ddlContributor2.SelectedValue != string.Empty &&
                        ddlContributor.SelectedValue != ddlContributor2.SelectedValue)
                    {
                    Institution newContributor = new Institution
                    {
                        InstitutionCode = ddlContributor2.SelectedValue,
                        InstitutionRoleName = InstitutionRole.Contributor,
                        IsNew = true
                    };
                        segment.ContributorList.Add(newContributor);
                    }

                    //----------------------------------------

                    // Forces deletes to happen first
                    segment.ContributorList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                    segment.IdentifierList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                    segment.AuthorList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                    segment.KeywordList.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
                    segment.SegmentExternalResources.Sort((s1, s2) => s2.IsDeleted.CompareTo(s1.IsDeleted));
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
                    FillUI(segmentID);

                    litMessage.Text = "<span class='liveData'>Segment Saved.</span>";
                    if (_warnings.Count > 0)
                    {
                        string warningMessage = string.Empty;
                        foreach (string warning in _warnings) warningMessage += "<br/>" + warning;
                        litWarning.Text = string.Format("<span class='liveData'>{0}</span>", warningMessage);
                    }
                    ResetScrollPosition();
                }
            }
            finally
            {
                saveButton.Enabled = true;
            }
        }

        #endregion Event Handlers

        private bool Validate(Segment segment)
        {
            bool flag = false;
            _warnings.Clear();

            // Check that all edits were completed
            if (authorsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Authors has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (keywordsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Keywords has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (resourcesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("External Resources has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (relatedSegmentsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Related Segments has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            if (pagesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Pages has an edit pending.  Click the appropriate link to complete the edit (Update, Remove, or Cancel).");
            }

            // If a "replaced by" identifer was specified, make sure that it is a valid id
            if (replacedByTextBox.Text.Trim().Length > 0)
            {
                if (Int32.TryParse(replacedByTextBox.Text, out int segmentID))
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
                if (Int32.TryParse(bhlStartPageIDTextBox.Text, out int pageID))
                {
                    // Look up the specified ID to ensure that it exists
                    if (new BHLProvider().PageSelectAuto(pageID) == null)
                    {
                        flag = true;
                        errorControl.AddErrorText("Make sure the 'Start Page BHL ID' is a valid Page ID.");
                    }
                    else
                    {
                        // Make sure the StartpPageID is actually in the list of pages.  If it is not, replace the StartPageID with the Page ID of the first page.
                        if (pagesList.Rows.Count > 0)
                        {
                            bool pageFound = false;
                            foreach(DataKey page in pagesList.DataKeys)
                            {
                                if ((int)page.Values[1] == pageID) { pageFound = true; break; }
                            }
                            if (!pageFound)
                            {
                                _warnings.Add(String.Format("NOTE: The specified Start Page ID {0} is not part of this segment.  It has been replaced with {1}.", 
                                    pageID.ToString(), pagesList.DataKeys[0].Values[1].ToString()));
                                bhlStartPageIDTextBox.Text = pagesList.DataKeys[0].Values[1].ToString();
                            }
                        }
                    }
                }
                else
                {
                    // Specified ID is not a valid integer value
                    flag = true;
                    errorControl.AddErrorText("Make sure the 'Start Page BHL ID' is a valid Page ID.");
                }
            }

            if (volumeTextBox.Text.Trim().Length > 0)
            {
                if (DataCleaner.ValidateSegmentVolumeIssue(volumeTextBox.Text.Trim()))
                {
                    volumeTextBox.Text = volumeTextBox.Text.Trim();   // Remove spaces
                }
                else
                {
                    flag = true;
                    errorControl.AddErrorText("Volume must be formatted as 'NN' or 'NN-NN' or 'NN/NN'.");
                }
            }

            if (issueTextBox.Text.Trim().Length > 0)
            {
                if (DataCleaner.ValidateSegmentVolumeIssue(issueTextBox.Text.Trim()))
                {
                    issueTextBox.Text = issueTextBox.Text.Trim();   // Remove spaces
                }
                else
                {
                    flag = true;
                    errorControl.AddErrorText("Issue must be formatted as 'NN' or 'NN-NN' or 'NN/NN'.");
                }
            }

            // Make sure that at least one contributor has been specified
            if (ddlContributor.SelectedValue == "" && ddlContributor2.SelectedValue == "")
            {
                flag = true;
                errorControl.AddErrorText("At least one contributor must be selected");
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

            // Validate keywords
            foreach (ItemKeyword ik in segment.KeywordList)
            {
                if (!ik.IsDeleted)
                {
                    if (string.IsNullOrWhiteSpace(ik.Keyword))
                    {
                        flag = true;
                        errorControl.AddErrorText("Keywords cannot be blank");
                        break;
                    }
                }
            }

            foreach (SegmentExternalResource ser in segment.SegmentExternalResources)
            {
                if (!ser.IsDeleted)
                {
                    if (ser.ExternalResourceTypeID <= 0 || string.IsNullOrWhiteSpace(ser.UrlText))
                    {
                        flag = true;
                        errorControl.AddErrorText("External Resources must have a Type and Text.");
                        break;
                    }
                }
            }

            // Validate identifiers
            bool blankID = false;
            foreach (ItemIdentifier ii in segment.IdentifierList)
            {
                if (!ii.IsDeleted)
                {
                    if (ii.IdentifierID <= 0 || string.IsNullOrWhiteSpace(ii.IdentifierValue))
                    {
                        blankID = true;
                        flag = true;
                        errorControl.AddErrorText("Identifiers cannot be blank");
                        break;
                    }
                }
            }

            if (!blankID)
            {
                IdentifierValidationResult identifierValidationResult = new BHLProvider().ValidateIdentifiers(segment.IdentifierList);
                if (!identifierValidationResult.IsValid)
                {
                    flag = true;
                    foreach (string message in identifierValidationResult.Messages) errorControl.AddErrorText(message);
                }
                if (identifierValidationResult.IncludesNewBHLDOI)
                {
                    flag = true;
                    errorControl.AddErrorText("A BHL-managed DOI can only be added by submitting the Segment metadata to a DOI registrar (such as Crossref)");
                }
            }

            errorControl.Visible = flag;
            if (flag) ResetScrollPosition();

            return !flag;
        }

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