﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.Security.Client.MOBOTSecurity;
using SortOrder = CustomDataAccess.SortOrder;

namespace MOBOT.BHL.AdminWeb
{
    public partial class SegmentEdit : System.Web.UI.Page
    {
        private SegmentAuthorComparer.CompareEnum _sortColumn = SegmentAuthorComparer.CompareEnum.SequenceOrder;
        private SortOrder _sortOrder = SortOrder.Ascending;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectItem", "<script language='javascript'>function selectItem(itemId) { if (itemId != '') document.getElementById('" + selectedItem.ClientID + "').value=itemId; overlay(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearItem", "<script language='javascript'>function clearItem() { document.getElementById('" + selectedItem.ClientID + "').value=''; __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearAuthor", "<script language='javascript'>function clearAuthor() { document.getElementById('" + selectedAuthor.ClientID + "').value=''; overlayAuthorSearch(); __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectAuthor", "<script language='javascript'>function selectAuthor(authorId) { document.getElementById('" + selectedAuthor.ClientID + "').value=authorId; overlayAuthorSearch(); __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptCancelPage", "<script language='javascript'>function cancelPages() { __doPostBack('',''); }</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectPage", "<script language='javascript'>function selectPage(pageId) { document.getElementById('" + selectedPage.ClientID + "').value+=pageId+'|';}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptClearRelatedSegment", "<script language='javascript'>function clearRelatedSegment() { document.getElementById('" + selectedRelatedSegments.ClientID + "').value=''; __doPostBack('', '');}</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scptSelectRelatedSegment", "<script language='javascript'>function selectRelatedSegment(segmentId) { document.getElementById('" + selectedRelatedSegments.ClientID + "').value+=segmentId+'|';}</script>");

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
                    itemIDLabel.Text = "";
                    itemDescLabel.Text = "Not Selected";
                }
                else if (itemIDLabel.Text != selectedItemId)
                {
                    // Get the description of the newly selected item
                    Item item = provider.ItemSelectAuto(Convert.ToInt32(selectedItemId));
                    Title title = provider.TitleSelectAuto(item.PrimaryTitleID);

                    itemIDLabel.Text = selectedItemId;
                    itemDescLabel.Text = title.ShortTitle + " " + item.Volume;

                    // Use Title/Item metadata to set the journal details for this segment
                    ddlContributor.SelectedValue = item.InstitutionCode ?? string.Empty;
                    containerTitleTextBox.Text = title.FullTitle ?? string.Empty;
                    publicationDetailsTextBox.Text = title.PublicationDetails ?? string.Empty;
                    publisherNameTextBox.Text = title.Datafield_260_b ?? string.Empty;
                    publisherPlaceTextBox.Text = title.Datafield_260_a ?? string.Empty;
                    ddlLanguage.SelectedValue = item.LanguageCode ?? string.Empty;
                    volumeTextBox.Text = item.Volume ?? string.Empty;
                    dateTextBox.Text = string.IsNullOrWhiteSpace(item.Year) ? (title.StartYear == null ? string.Empty : title.StartYear.ToString()) : item.Year;
                }

                // Check for newly added authors
                String selectedAuthorId = this.selectedAuthor.Value;
                if (selectedAuthorId != "")
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];

                    // Make sure the selected author isn't already associated with this title
                    bool authorExists = false;
                    foreach (SegmentAuthor existingAuthor in segment.AuthorList)
                    {
                        if (existingAuthor.AuthorID.ToString() == selectedAuthorId)
                        {
                            authorExists = true;
                            break;
                        }
                    }

                    if (!authorExists)
                    {
                        SegmentAuthor newSegmentAuthor = new SegmentAuthor();

                        // Get details for "selectedAuthorId" from database
                        Author author = provider.AuthorSelectWithNameByAuthorId(Convert.ToInt32(selectedAuthorId));
                        newSegmentAuthor.AuthorID = Convert.ToInt32(selectedAuthorId);
                        newSegmentAuthor.SegmentID = segment.SegmentID;
                        newSegmentAuthor.FullName = author.FullName;
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
                    CustomGenericList<MOBOT.BHL.DataObjects.Segment> relatedSegments = provider.SegmentSelectRelated(Convert.ToInt32(itemIDLabel.Text));

                    if (selectedRelatedSegmentIds.EndsWith("|")) selectedRelatedSegmentIds = selectedRelatedSegmentIds.Substring(0, selectedRelatedSegmentIds.Length - 1);
                    String[] selectedRelatedSegmentsList = selectedRelatedSegmentIds.Split('|');

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
                    CustomGenericList<MOBOT.BHL.DataObjects.Page> pages = provider.PageSelectByItemID(Convert.ToInt32(itemIDLabel.Text));

                    if (selectedPageIds.EndsWith("|")) selectedPageIds = selectedPageIds.Substring(0, selectedPageIds.Length - 1);
                    String[] selectedPageIdList = selectedPageIds.Split('|');
                    foreach (String selectedPageId in selectedPageIdList)
                    {
                        // Make sure the selected page isn't already associated with this segment
                        bool pageExists = false;
                        foreach (SegmentPage existingPage in segment.PageList)
                        {
                            if (existingPage.PageID.ToString() == selectedPageId)
                            {
                                pageExists = true;
                                break;
                            }
                        }

                        if (!pageExists)
                        {
                            SegmentPage newPage = new SegmentPage();

                            // Get the current maximum itemsequence value
                            short pageSequence = 0;
                            foreach (SegmentPage segmentPage in segment.PageList)
                            {
                                if (segmentPage.SequenceOrder > pageSequence) pageSequence = segmentPage.SequenceOrder;
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
                            newPage.SegmentID = segment.SegmentID;
                            newPage.SequenceOrder = ++pageSequence;
                            newPage.PageSequenceOrder = pageSequenceOrder;
                            newPage.PageTypes = types;
                            newPage.IndicatedPages = numbers;
                            newPage.IsNew = true;
                            segment.PageList.Add(newPage);
                        }
                    }

                    // Re-sort the list of pages, using the order of the scanned pages as a guide
                    SegmentPageComparer comp = new SegmentPageComparer(
                        (SegmentPageComparer.CompareEnum)SegmentPageComparer.CompareEnum.PageSequenceOrder, _sortOrder);
                    segment.PageList.Sort(comp);
                    short newSeqOrder = 1;
                    SecUser secUser = getSecUser();
                    foreach (SegmentPage segmentPage in segment.PageList)
                    {
                        if (segmentPage.SequenceOrder != newSeqOrder)
                        {
                            segmentPage.SequenceOrder = newSeqOrder;
                            segmentPage.LastModifiedUserID = secUser.UserID;
                        }
                        newSeqOrder++;
                    }

                    Session["Segment" + segment.SegmentID.ToString()] = segment;
                    this.selectedPage.Value = "";
                    this.bindSegmentPageData();
                }
            }

            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Fill methods

        private void fillCombos()
        {
            BHLProvider bp = new BHLProvider();

            CustomGenericList<SegmentStatus> statuses = bp.SegmentStatusSelectAll();
            ddlSegmentStatus.DataSource = statuses;
            ddlSegmentStatus.DataBind();

            CustomGenericList<SegmentGenre> genres= bp.SegmentGenreSelectAll();
            ddlSegmentGenre.DataSource = genres;
            ddlSegmentGenre.DataBind();

            CustomGenericList<Institution> institutions = bp.InstituationSelectAll();

            Institution emptyInstitution = new Institution();
            emptyInstitution.InstitutionCode = string.Empty;
            emptyInstitution.InstitutionName = string.Empty;
            institutions.Insert(0, emptyInstitution);

            ddlContributor.DataSource = institutions;
            ddlContributor.DataBind();

            CustomGenericList<Language> languages = bp.LanguageSelectAll();

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
                ddlSegmentStatus.SelectedValue = "10";  // New
            }
            else
            {
                // Look up segment
                segment = bp.SegmentSelectExtended(id);
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
                    selectedItem.Value = segment.ItemID.ToString();
                    itemIDLabel.Text = segment.ItemID.ToString();
                    itemDescLabel.Text = segment.TitleShortTitle + " " + segment.ItemVolume;
                }
                replacedByTextBox.Text = segment.RedirectSegmentID.ToString();
                lblDOIName.Text = segment.DOIName;
                contributorSegmentIDTextBox.Text = segment.ContributorSegmentID;
                titleTextBox.Text = segment.Title;
                sortTitleTextBox.Text = segment.SortTitle;
                translatedTitleTextBox.Text = segment.TranslatedTitle;
                containerTitleTextBox.Text = segment.ContainerTitle;
                publicationDetailsTextBox.Text = segment.PublicationDetails;
                publisherPlaceTextBox.Text = segment.PublisherPlace;
                publisherNameTextBox.Text = segment.PublisherName;
                notesTextBox.Text = segment.Notes;
                volumeTextBox.Text = segment.Volume;
                seriesTextBox.Text = segment.Series;
                issueTextBox.Text = segment.Issue;
                dateTextBox.Text = segment.Date;
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

                if (segment.ContributorCode != null && segment.ContributorCode.Length > 0)
                {
                    ddlContributor.SelectedValue = segment.ContributorCode;
                }
                else
                {
                    ddlContributor.SelectedIndex = 0;
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

        #endregion Fill methods

        #region SegmentAuthor methods

        private void bindSegmentAuthorData()
        {
            Segment segment = (Segment)Session["Segment" + idLabel.Text];

            // filter out deleted items
            CustomGenericList<SegmentAuthor> segmentAuthors = new CustomGenericList<SegmentAuthor>();
            foreach (SegmentAuthor sa in segment.AuthorList)
            {
                if (sa.IsDeleted == false)
                {
                    segmentAuthors.Add(sa);
                }
            }

            SegmentAuthorComparer comp = new SegmentAuthorComparer((SegmentAuthorComparer.CompareEnum)_sortColumn, _sortOrder);
            segmentAuthors.Sort(comp);
            authorsList.DataSource = segmentAuthors;
            authorsList.DataBind();
        }

        private SegmentAuthor findSegmentAuthor(CustomGenericList<SegmentAuthor> segmentAuthors, int segmentAuthorId,
            int authorId)
        {
            foreach (SegmentAuthor sa in segmentAuthors)
            {
                if (sa.IsDeleted)
                {
                    continue;
                }
                if (segmentAuthorId == 0 && sa.SegmentAuthorID == 0 && authorId == sa.AuthorID)
                {
                    return sa;
                }
                else if (segmentAuthorId > 0 && sa.SegmentAuthorID == segmentAuthorId)
                {
                    return sa;
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
            CustomGenericList<SegmentKeyword> titleKeywords = new CustomGenericList<SegmentKeyword>();
            foreach (SegmentKeyword sk in segment.KeywordList)
            {
                if (sk.IsDeleted == false)
                {
                    titleKeywords.Add(sk);
                }
            }

            keywordsList.DataSource = titleKeywords;
            keywordsList.DataBind();
        }

        private SegmentKeyword findSegmentKeyword(CustomGenericList<SegmentKeyword> segmentKeywords,
            int segmentKeywordId, int keywordID, string keyword)
        {
            foreach (SegmentKeyword sk in segmentKeywords)
            {
                if (sk.IsDeleted)
                {
                    continue;
                }
                if (segmentKeywordId == sk.SegmentKeywordID &&
                    keywordID == sk.KeywordID &&
                    keyword == sk.Keyword)
                {
                    return sk;
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
            CustomGenericList<SegmentIdentifier> segmentIdentifiers = new CustomGenericList<SegmentIdentifier>();
            foreach (SegmentIdentifier si in segment.IdentifierList)
            {
                if (si.IsDeleted == false)
                {
                    segmentIdentifiers.Add(si);
                }
            }

            identifiersList.DataSource = segmentIdentifiers;
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

        private SegmentIdentifier findSegmentIdentifier(CustomGenericList<SegmentIdentifier> segmentIdentifiers,
            int segmentIdentifierId, int identifierID, string identifierValue)
        {
            foreach (SegmentIdentifier si in segmentIdentifiers)
            {
                if (si.IsDeleted)
                {
                    continue;
                }
                if (segmentIdentifierId == 0 && si.SegmentIdentifierID == 0 &&
                    identifierID == 0 && si.IdentifierID == 0 &&
                    identifierValue == "" && si.IdentifierValue == "")
                {
                    return si;
                }
                if (segmentIdentifierId == 0 && si.SegmentIdentifierID == 0 &&
                    identifierID > 0 && identifierID == si.IdentifierID &&
                    identifierValue == si.IdentifierValue)
                {
                    return si;
                }
                else if (segmentIdentifierId > 0 && si.SegmentIdentifierID == segmentIdentifierId)
                {
                    return si;
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
            CustomGenericList<SegmentPage> segmentPages = new CustomGenericList<SegmentPage>();
            foreach (SegmentPage sp in segment.PageList)
            {
                if (sp.IsDeleted == false)
                {
                    segmentPages.Add(sp);
                }
            }

            SegmentPageComparer comp = new SegmentPageComparer(
                (SegmentPageComparer.CompareEnum)SegmentPageComparer.CompareEnum.SequenceOrder, _sortOrder);
            segmentPages.Sort(comp);
            pagesList.DataSource = segmentPages;
            pagesList.DataBind();
        }

        private SegmentPage findSegmentPage(CustomGenericList<SegmentPage> segmentPages, int segmentPageId,
            int pageId)
        {
            foreach (SegmentPage sp in segmentPages)
            {
                if (sp.IsDeleted)
                {
                    continue;
                }
                if (segmentPageId == 0 && sp.SegmentPageID == 0 && pageId == sp.PageID)
                {
                    return sp;
                }
                else if (segmentPageId > 0 && sp.SegmentPageID == segmentPageId)
                {
                    return sp;
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
            CustomGenericList<Segment> relatedSegments = new CustomGenericList<Segment>();
            foreach (Segment s in segment.RelatedSegmentList)
            {
                if (!s.IsDeleted)
                {
                    relatedSegments.Add(s);
                    relatedPrimary = relatedPrimary || (s.IsPrimary == 1);
                }
            }

            SegmentComparer comp = new SegmentComparer((SegmentComparer.CompareEnum)SegmentComparer.CompareEnum.SequenceOrder, _sortOrder);
            relatedSegments.Sort(comp);

            litPrimaryMsg.Visible = (relatedSegments.Count > 0);
            chkPrimary.Visible = litPrimaryMsg.Visible;
            if (relatedPrimary) chkPrimary.Checked = false;

            relatedSegmentsList.DataSource = relatedSegments;
            relatedSegmentsList.DataBind();
        }

        private Segment findRelatedSegment(CustomGenericList<Segment> relatedSegments, int segmentId)
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
                    SecUser secUser = getSecUser();

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        // Find item being changed
                        SegmentAuthor changedAuthor = findSegmentAuthor(segment.AuthorList,
                            (int)authorsList.DataKeys[e.RowIndex].Values[0],
                            (int)authorsList.DataKeys[e.RowIndex].Values[1]);

                        short oldSeq = changedAuthor.SequenceOrder;

                        if (changedAuthor != null)
                        {
                            // If sequence has been decreased
                            if (newSeq < oldSeq)
                            {
                                // Increment all item sequences between the old and new sequence values
                                foreach (SegmentAuthor author in segment.AuthorList)
                                {
                                    if (author.SequenceOrder >= newSeq && author.SequenceOrder < oldSeq)
                                    {
                                        author.SequenceOrder++;
                                        author.LastModifiedUserID = secUser.UserID;
                                    }
                                }
                            }

                            // If sequence has been increased
                            if (newSeq > oldSeq)
                            {
                                // Decrement all item sequences between the old and new sequence values
                                foreach (SegmentAuthor author in segment.AuthorList)
                                {
                                    if (author.SequenceOrder <= newSeq && author.SequenceOrder > oldSeq)
                                    {
                                        author.SequenceOrder--;
                                        author.LastModifiedUserID = secUser.UserID;
                                    }
                                }
                            }

                            changedAuthor.SequenceOrder = newSeq;
                            changedAuthor.LastModifiedUserID = secUser.UserID;
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

                SegmentAuthor segmentAuthor = findSegmentAuthor(segment.AuthorList,
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

                    SegmentKeyword segmentKeyword = findSegmentKeyword(segment.KeywordList,
                        (int)keywordsList.DataKeys[e.RowIndex].Values[0],
                        (int)keywordsList.DataKeys[e.RowIndex].Values[1],
                        keywordsList.DataKeys[e.RowIndex].Values[2].ToString());

                    segmentKeyword.SegmentID = segment.SegmentID;
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
            SegmentKeyword segmentKeyword = new SegmentKeyword();
            segmentKeyword.SegmentID = segment.SegmentID;
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

                SegmentKeyword segmentKeyword = findSegmentKeyword(segment.KeywordList,
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

                    SegmentIdentifier segmentIdentifier = findSegmentIdentifier(segment.IdentifierList,
                        (int)identifiersList.DataKeys[e.RowIndex].Values[0],
                        (int)identifiersList.DataKeys[e.RowIndex].Values[1],
                        identifiersList.DataKeys[e.RowIndex].Values[2].ToString());

                    segmentIdentifier.SegmentID = segment.SegmentID;
                    segmentIdentifier.IdentifierID = identifierId;
                    segmentIdentifier.IdentifierName = ddlIdentifierName.SelectedItem.Text;
                    segmentIdentifier.IdentifierValue = identifierValue;
                    segmentIdentifier.IsContainerIdentifier = (short)(cbIsContainerIdentifier.Checked ? 1 : 0);
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
            SegmentIdentifier si = new SegmentIdentifier();
            si.SegmentID = segment.SegmentID;
            si.IsContainerIdentifier = (short)0;
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

                SegmentIdentifier segmentIdentifier = findSegmentIdentifier(segment.IdentifierList,
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
                    SecUser secUser = getSecUser();

                    string newSeqString = sequenceTextBox.Text.Trim();
                    short newSeq = 0;
                    short.TryParse(newSeqString, out newSeq);

                    if (newSeq > 0)
                    {
                        // Find item being changed
                        SegmentPage changedPage = findSegmentPage(segment.PageList,
                            (int)pagesList.DataKeys[e.RowIndex].Values[0],
                            (int)pagesList.DataKeys[e.RowIndex].Values[1]);

                        short oldSeq = changedPage.SequenceOrder;

                        if (changedPage != null)
                        {
                            // If sequence has been decreased
                            if (newSeq < oldSeq)
                            {
                                // Increment all item sequences between the old and new sequence values
                                foreach (SegmentPage page in segment.PageList)
                                {
                                    if (page.SequenceOrder >= newSeq && page.SequenceOrder < oldSeq)
                                    {
                                        page.SequenceOrder++;
                                        page.LastModifiedUserID = secUser.UserID;
                                    }
                                }
                            }

                            // If sequence has been increased
                            if (newSeq > oldSeq)
                            {
                                // Decrement all item sequences between the old and new sequence values
                                foreach (SegmentPage page in segment.PageList)
                                {
                                    if (page.SequenceOrder <= newSeq && page.SequenceOrder > oldSeq)
                                    {
                                        page.SequenceOrder--;
                                        page.LastModifiedUserID = secUser.UserID;
                                    }
                                }
                            }

                            changedPage.SequenceOrder = newSeq;
                            changedPage.LastModifiedUserID = secUser.UserID;
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

                SegmentPage segmentPage = findSegmentPage(segment.PageList,
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
                CustomGenericList<Segment> relatedSegments = new CustomGenericList<Segment>();
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

        protected void relatedSegmentsList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = relatedSegmentsList.Rows[e.RowIndex];

            if (row != null)
            {
                CheckBox checkBox = row.FindControl("isPrimaryCheckBoxEdit") as CheckBox;
                if (checkBox != null)
                {
                    Segment segment = (Segment)Session["Segment" + idLabel.Text];
                    CustomGenericList<Segment> relatedSegments = segment.RelatedSegmentList;
                    
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
                // Set the id of the editing user
                SecUser secUser = this.getSecUser();
                userId = secUser.UserID;

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
                bool isItemChanged = (segment.ItemID ?? 0) != (itemIDLabel.Text == "" ? 0 : Convert.ToInt32(itemIDLabel.Text));
                segment.ItemID = (itemIDLabel.Text == "" ? (int?)null : Convert.ToInt32(itemIDLabel.Text));
                segment.RedirectSegmentID = (replacedByTextBox.Text.Trim().Length == 0 ? (int?)null : Convert.ToInt32(replacedByTextBox.Text));
                segment.SegmentStatusID = Convert.ToInt32(ddlSegmentStatus.SelectedValue);
                segment.SegmentGenreID = Convert.ToInt32(ddlSegmentGenre.SelectedValue);
                segment.ContributorCode = (ddlContributor.SelectedValue.Length == 0 ? null : ddlContributor.SelectedValue);
                segment.ContributorSegmentID = contributorSegmentIDTextBox.Text.Trim();
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
                segment.IsPrimary = (short)(chkPrimary.Checked ? 1 : 0);
                segment.IsNew = (segment.SegmentID == 0);

                // Forces deletes to happen first
                segment.IdentifierList.Sort(SortOrder.Descending, "IsDeleted");
                segment.AuthorList.Sort(SortOrder.Descending, "IsDeleted");
                segment.KeywordList.Sort(SortOrder.Descending, "IsDeleted");
                segment.PageList.Sort(SortOrder.Descending, "IsDeleted");

                BHLProvider bp = new BHLProvider();

                // If the ItemID has been modified, then reset the sequenceorder.  If other segments exist on the selected
                // Item, make this segment the last one (with the highest sequence number).
                if (isItemChanged)
                {
                    segment.SequenceOrder = (short)((segment.ItemID == null) ? 1 : (bp.SegmentSelectByItemID((int)segment.ItemID).Count + 1));
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

        private SecUser getSecUser()
        {
            HttpCookie tokenCookie = Request.Cookies["MOBOTSecurityToken"];
            return Helper.GetSecProvider().SecUserSelect(tokenCookie.Value);
        }

        private bool validate(Segment segment)
        {
            bool flag = false;

            // Check that all edits were completed
            if (authorsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Authors has an edit pending");
            }

            if (identifiersList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Identifiers has an edit pending");
            }

            if (keywordsList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Keywords has an edit pending");
            }

            if (pagesList.EditIndex != -1)
            {
                flag = true;
                errorControl.AddErrorText("Pages has an edit pending");
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
            foreach (SegmentAuthor sa in segment.AuthorList)
            {
                if (sa.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (SegmentAuthor sa2 in segment.AuthorList)
                    {
                        if (sa2.IsDeleted == false)
                        {
                            if ((sa.SegmentAuthorID != sa2.SegmentAuthorID && sa.AuthorID == sa2.AuthorID) ||
                                (sa.SegmentAuthorID == 0 && sa.SegmentAuthorID == 0 && sa.AuthorID == sa2.AuthorID && ix != iy))
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
            foreach (SegmentPage sp in segment.PageList)
            {
                if (sp.IsDeleted == false)
                {
                    int iy = 0;
                    foreach (SegmentPage sp2 in segment.PageList)
                    {
                        if (sp2.IsDeleted == false)
                        {
                            if ((sp.SegmentPageID != sp2.SegmentPageID && sp.PageID == sp2.PageID) ||
                                (sp.SegmentPageID == 0 && sp.SegmentPageID == 0 && sp.PageID == sp2.PageID && ix != iy))
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