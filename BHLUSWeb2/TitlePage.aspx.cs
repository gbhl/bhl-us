using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Linq;
using MOBOT.BHL.Server;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;
using RestSharp;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Web2
{
    public partial class TitlePage : BasePage
    {
        protected PageSummaryView PageSummary { get; set; }
        protected CustomGenericList<Title> Titles { get; set; }
        protected CustomGenericList<Segment> Segments { get; set; }
        protected int StartPage { get; set; }
        protected int PageCount { get; set; }
        protected int SegmentCount { get; set; }
        protected string Pages = String.Empty;
        protected string PageTitle { get; set; }
        protected int CurrentItemID { get; set; }

        //Page Annotation additions
        private bool _showAnnotations = true;
        public bool ShowAnnotations
        {
            get { return _showAnnotations; }
            set { _showAnnotations = value; }
        }

        private string _hasAnnotations = "false";
        public string HasAnnotations
        {
            get { return _hasAnnotations; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool getFirstPage = true;
                bool titleSearch = false;

                int pageid = int.MinValue;
                int? segmentid = null;

                if (RouteData.Values["pageid"] != null)
                {
                    if (int.TryParse((string)RouteData.Values["pageid"], out pageid))
                    {
                        PageSummary = bhlProvider.PageSummarySelectByPageId(pageid, true);
                        getFirstPage = false;

                        // Check to make sure this item hasn't been replaced.  If it has, redirect
                        // to the appropriate itemid.  That won't find the correct page, but at
                        // least puts the user in the correct item... better than "not found".
                        if (PageSummary != null)
                        {
                            if (PageSummary.RedirectItemID != null)
                            {
                                Response.Redirect("~/item/" + PageSummary.RedirectItemID);
                            }
                        }
                    }
                }
                else if (RouteData.Values["titleid"] != null)
                {
                    int titleid;
                    titleSearch = true;
                    if (int.TryParse((string)RouteData.Values["titleid"], out titleid))
                    {
                        // Check to make sure this title hasn't been replaced.  If it has, redirect
                        // to the appropriate titleid.
                        Title title = bhlProvider.TitleSelect(titleid);
                        if (title != null)
                        {
                            if (title.RedirectTitleID != null)
                            {
                                Response.Redirect("~/title/" + title.RedirectTitleID);
                            }

                            // Make sure the title is published
                            if (title.PublishReady)
                            {
                                PageSummary = bhlProvider.PageSummarySelectByTitleId(titleid);
                            }
                        }
                    }
                }
                else if (RouteData.Values["itemid"] != null)
                {
                    int itemid;
                    if (int.TryParse((string)RouteData.Values["itemid"], out itemid))
                    {
                        // If we came from the bibliography page, get the title id
                        int titleId = 0;
                        String referer = Request.ServerVariables["HTTP_REFERER"];
                        if (referer != null)
                        {
                            String host = Request.ServerVariables["HTTP_HOST"];
                            String bibPath = "http://" + (host ?? String.Empty) + "/bibliography/";
                            if (referer.StartsWith(bibPath, true, null))
                            {
                                referer = referer.Replace(bibPath, String.Empty);
                                Int32.TryParse(referer, out titleId);
                            }
                        }

                        if (titleId != 0)
                        {
                            // Include details of a specific title if we have a titleid
                            PageSummary = bhlProvider.PageSummarySelectByItemIdAndTitleId(itemid, titleId);
                        }
                        else
                        {
                            // Include details of the primary title for the item
                            PageSummary = bhlProvider.PageSummarySelectByItemId(itemid, true);
                        }

                        // Check to make sure this item hasn't been replaced.  If it has, redirect
                        // to the appropriate itemid.
                        if (PageSummary != null)
                        {
                            if (PageSummary.RedirectItemID != null)
                            {
                                Response.Redirect("~/item/" + PageSummary.RedirectItemID);
                            }
                        }
                        else
                        {
                            // If no pages then see if we should redirect to an external url
                            Item item = bhlProvider.ItemSelectAuto(itemid);
                            if (item != null)
                            {
                                if (!string.IsNullOrWhiteSpace(item.ExternalUrl)) Response.Redirect(item.ExternalUrl);
                            }
                        }
                    }
                }
                else if (RouteData.Values["iabarcode"] != null)
                {
                    String iabarcode = (string)RouteData.Values["iabarcode"];

                        // Include details of the primary title for the item
                        PageSummary = bhlProvider.PageSummarySelectByBarcode(iabarcode, true);

                        // Check to make sure this item hasn't been replaced.  If it has, redirect
                        // to the appropriate itemid.
                        if (PageSummary != null)
                        {
                            if (PageSummary.RedirectItemID != null)
                            {
                                Response.Redirect("~/item/" + PageSummary.RedirectItemID);
                            }
                        }
                }

                if (PageSummary == null)
                {
                    // if our PageSummaryView is still null, then redirect because we couldn't find 
                    // the requested title or item.
                    if (titleSearch)
                    {
                        Response.Redirect("~/titlenotfound");
                    }
                    else
                    {
                        Response.Redirect("~/itemnotfound");
                    }
                }
                else
                {
                    // Make sure the item is published
                    if (PageSummary.ItemStatusID != 40)
                    {
                        Response.Redirect("~/itemunavailable");
                    }

                    Page firstPage = null;
                    int? sequenceOrder = PageSummary.SequenceOrder;
                    mendeley.ItemID = PageSummary.ItemID;

                    if (getFirstPage)
                    {
                        firstPage = bhlProvider.PageSelectFirstPageForItem(PageSummary.ItemID);
                        sequenceOrder = firstPage.SequenceOrder;
                        pageid = firstPage.PageID;
                    }

                    Page.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], (String.IsNullOrEmpty(PageSummary.Volume) ? String.Empty : PageSummary.Volume + " - ") + PageSummary.ShortTitle);

                    // Set the item for the COinS
                    COinS.ItemID = PageSummary.ItemID;

                    // Set Volume drop down list
                    IList<Item> items = bhlProvider
                        .ItemSelectByTitleId(PageSummary.TitleID)
                        //.ToList()
                        //.Where(x => !string.IsNullOrWhiteSpace(x.Volume))
                        .ToList();

                    foreach (Item item in items) if (string.IsNullOrWhiteSpace(item.Volume)) item.Volume = "Volume details";

                    ddlVolumes.DataSource = items;
                    ddlVolumes.DataTextField = "DisplayedShortVolume";
                    ddlVolumes.DataValueField = "ItemID";
                    ddlVolumes.DataBind();
                    ddlVolumes.SelectedValue = PageSummary.ItemID.ToString();

                    // Show contributing institution
                    CustomGenericList<Institution> institutions = bhlProvider.ItemHoldingInstitutionSelectByItemID(PageSummary.ItemID);
                    if (institutions.Count > 0)
                    {
                        Institution institution = institutions[0];
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl))
                        {
                            HyperLink link = new HyperLink();
                            link.Text = institution.InstitutionName;
                            link.NavigateUrl = institution.InstitutionUrl;
                            link.Target = "_blank";
                            attributionPlaceHolder.Controls.Add(link);
                        }
                        else
                        {
                            Literal literal = new Literal();
                            literal.Text = institution.InstitutionName;
                            attributionPlaceHolder.Controls.Add(literal);
                        }

                        ((Book)this.Master).holdingInstitution = institution.InstitutionCode.Replace("\"", "");
                    }

                    // Used to determine where to send people for bibliographic curiosity
                    Titles = bhlProvider.TitleSelectByItem(PageSummary.ItemID);

                    // Set the Book Reader properties
                    StartPage = sequenceOrder.Value; // Why is this a nullable int? it is never checked for null...
                    
                    CustomGenericList<Page> pages = bhlProvider.PageMetadataSelectByItemID(PageSummary.ItemID);

                    //SCS Set the Pages drop down list   
                    lstPages.DataSource = pages;
                    lstPages.DataTextField = "WebDisplay";
                    lstPages.DataValueField = "PageID";
                    lstPages.DataBind();
                    PageCount = pages.Count;

                    // Add text to display when hovering over a listbox row
                    foreach (ListItem item in lstPages.Items)
                        item.Attributes["title"] = item.Text;

                    Segments = bhlProvider.SegmentSelectByItemID(PageSummary.ItemID);
                    SegmentCount = Segments.Count;

                    // Listbox used by iDevices
                    lbSegments.DataSource = Segments;
                    lbSegments.DataTextField = "Title";
                    lbSegments.DataValueField = "StartPageID";
                    lbSegments.DataBind();

                    CurrentItemID = PageSummary.ItemID;
                    ((Book)this.Master).itemID = CurrentItemID.ToString();
                    ((Book)this.Master).sponsor = 
                        PageSummary.Sponsor == null ? 
                        string.Empty : 
                        PageSummary.Sponsor.Replace("\"", "");

                    // Check and set up Annotations SCS
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAnnotations"]))
                    {
                        setAnnotationContent();
                    }

                    // Get any segment ID associated with the current page
                    segmentid = GetSegmentID(pages, pageid);

                    // Add Google Scholar metadata to the page headers
                    SetGoogleScholarTags(PageSummary.ItemID);

                    // Serialize only the information we need
                    List<BHLProvider.ViewerPage> viewerPages = new List<BHLProvider.ViewerPage>();
                    CustomGenericList<PageSummaryView> pageviews = bhlProvider.PageSummarySelectForViewerByItemID(PageSummary.ItemID);
                    foreach (PageSummaryView pageview in pageviews)
                    {
                        BHLProvider.ViewerPage viewerPage = new BHLProvider.ViewerPage
                        {
                            ExternalBaseUrl = pageview.ExternalBaseURL,
                            BarCode = pageview.BarCode,
                            FlickrUrl = pageview.FlickrUrl,
                            SequenceOrder = pageview.SequenceOrder
                        };
                        viewerPages.Add(viewerPage);
                    }

                    viewerPages = bhlProvider.PageGetImageDimensions(viewerPages, PageSummary.ItemID);

                    Pages = JsonConvert.SerializeObject(pages.ToList().Join(viewerPages,
                                                    p => p.SequenceOrder,
                                                    vp => vp.SequenceOrder,
                                                    (p, vp) => new
                                                    {
                                                        p.PageID,
                                                        p.WebDisplay,
                                                        p.SequenceOrder,
                                                        p.BarCode,
                                                        p.SegmentID,
                                                        vp.ExternalBaseUrl,
                                                        vp.Height,
                                                        vp.Width,
                                                        vp.FlickrUrl
                                                    }));
                }
            }
        }

        /// <summary>
        /// Get the segment id associated with the current page
        /// </summary>
        /// <param name="pages">List of all pages</param>
        /// <param name="currentPageID">Identifier of the current page</param>
        /// <returns>Segment ID associated with the page, or null</returns>
        private int? GetSegmentID(CustomGenericList<Page> pages, int currentPageID)
        {
            int? segmentid = null;

            foreach (Page page in pages)
            {
                if (page.PageID == currentPageID)
                {
                    segmentid = page.SegmentID;
                    break;
                }
            }

            return segmentid;
        }

        /// <summary>
        /// Set the Google Scholar tags for the page
        /// </summary>
        private void SetGoogleScholarTags(int itemid)
        {
            List<KeyValuePair<string, string>> tags = new List<KeyValuePair<string, string>>();
            tags = bhlProvider.GetGoogleScholarMetadataForItem(itemid, ConfigurationManager.AppSettings["ItemPageUrl"]);

            foreach (KeyValuePair<string, string> tag in tags)
            {
                HtmlMeta htmlMetaTag = new HtmlMeta();
                htmlMetaTag.Name = tag.Key;
                htmlMetaTag.Content = Server.HtmlEncode(tag.Value);
                this.Page.Header.Controls.Add(htmlMetaTag);
            }
        }

        /// <summary>
        /// Get the entire list of annotations for this item
        /// For each page, build the html for its annotations, and write to the page in hidden divs
        /// JavaScript on the front end will handle the show/hide functionality
        /// 
        /// To accommodate the overlapping of the Annotation Viewer with the page image,
        /// we left justify the page image.  Changes to this and any other desired front-end 
        /// adjustments should be done within the InitializeViewer javascript function.
        /// </summary>
        private void setAnnotationContent()
        {
            //Set Annotation content
            BHLProvider provider = new BHLProvider();

            CustomGenericList<Annotation> annotationList = provider.AnnotationsSelectByItemID(CurrentItemID);

            if (annotationList != null && annotationList.Count > 0)
            {
                //this item has annotations, so set any flags to be used within the InitializeViewer javascript function
                _hasAnnotations = "true";

                ltlBookIndicator.Text = (provider.AnnotatedItemCheckForSurrogate(CurrentItemID) ? "Darwin's copy of this book" : "surrogate copy of this work");
                StringBuilder sbPageBlock = new StringBuilder(),
                              sbScrollItems = new StringBuilder();

                int currentSequence = -1;
                foreach (Annotation _ann in annotationList)
                {
                    if (_ann.PageSequenceOrder != currentSequence)          //first or new page
                    {
                        if (currentSequence > 0)                            //already set, close current block before opening new one
                        {
                            //sbPageBlock.Append("\n\t</div>\n\t</div>");
                            sbPageBlock.Append("\n\t</div>");
                        }

                        //open new block
                        #region set Page Header
                        sbPageBlock.Append("<div id=\"pageAnnotations_").Append(_ann.PageSequenceOrder).Append("\" class=\"page-data\">\n\t\n");

                        //open page header
                        sbPageBlock.Append("<div class=\"page-header\">");

                        //get Page Type
                        AnnotatedPageType _apt = provider.AnnotatedPageTypeSelectByPageID(_ann.PageID);
                        if (_apt != null)
                            sbPageBlock.Append("<span>").Append(_apt.AnnotatedPageTypeName).Append("</span>");

                        //get Page Number
                        AnnotatedPage _ap = provider.AnnotatedPageSelectByPageID(_ann.PageID);
                        if (_ap != null)
                            sbPageBlock.Append("&nbsp;<span>").Append(_ap.PageNumber).Append("</span>");

                        //get Page Characteristic
                        AnnotatedPageCharacteristic _apc = provider.AnnotatedPageCharacteristicByPageID(_ann.PageID);
                        if (_apc != null)
                            sbPageBlock.Append("<div id=\"page-characteristics\">")
                                       .Append(_apc.CharacteristicDetailClean).Append("</div>");

                        //close page header
                        sbPageBlock.Append("</div>");
                        sbPageBlock.Append("<hr>"); //separate header from notes
                        #endregion

                        #region build Page Sequence

                        ///Build list of id's for annotated pages,
                        ///to navigate back and forth between via annotation viewer
                        if (sbScrollItems.Length > 0)
                            sbScrollItems.Append(",");
                        sbScrollItems.Append(_ann.PageSequenceOrder);

                        #endregion

                        currentSequence = _ann.PageSequenceOrder;
                    }

                    #region Get Text Display
                    sbPageBlock.Append("\t\t<div id=\"Annotation_")
                               .Append(_ann.AnnotationID)
                               .Append("\">")
                               .Append(_ann.AnnotationTextDisplay)
                               .Append("\n\t\t</div>");
                    #endregion

                    #region Get Notes
                    ///Get tnotes, which are referred to in Annotation Display
                    CustomGenericList<AnnotationNote> note_list = provider.AnnotationNoteSelectByAnnotationID(_ann.AnnotationID);
                    if (note_list.Count > 0)
                    {
                        sbPageBlock.Append("<div class=\"tnote\">");
                        foreach (AnnotationNote _note in note_list)
                        {
                            sbPageBlock.Append("<div>").
                                        Append(_note.NoteTextDisplay).
                                        Append("</div>");
                        }
                        sbPageBlock.Append("</div>");
                    }
                    #endregion

                    #region Get Subjects
                    CustomGenericList<CustomDataRow> subjects = provider.AnnotationSubjectSelectByAnnotationID(_ann.AnnotationID);
                    if (subjects.Count > 0)
                    {
                        int keywordTargetID = (int)subjects[0]["AnnotationKeywordTargetID"].Value;
                        sbPageBlock.Append("\n\t\t<div id=\"subjects_").Append(_ann.AnnotationID).Append("\" class=\"subject-list\">").
Append("<a href=\"javascript:void(0);\" onClick=\"toggleSubjectSection(").Append(_ann.AnnotationID).Append(");\" title=\"Hide\">").
    Append("<img id=\"hide-subjects").Append(_ann.AnnotationID).Append("\" src=\"../Images/bib_minus.gif\" alt=\"hide subjects\" style=\"display:none\" alt=\"hide subjects\"/>").
Append("</a>").

Append("<a href=\"javascript:void(0);\" onClick=\"toggleSubjectSection(").Append(_ann.AnnotationID).Append(");\" title=\"Show\">").
    Append("<img id=\"show-subjects").Append(_ann.AnnotationID).Append("\" src=\"../Images/bib_plus.gif\" alt=\"show subjects\" alt=\"show subjects\"/>").
Append("</a>").
                                    Append("\n\t\t\t<span class=\"title\">subjects</span>").
                                    Append("<div id=\"subject-section-").Append(_ann.AnnotationID).Append("\" style=\"display:none;\">"). //section wrapper for toggle
                                    Append("\n\t\t\t<div class=\"target-section\">").Append(subjects[0]["KeywordTargetName"].Value).Append("</div>"); ;
                        foreach (CustomDataRow row in subjects)
                        {
                            if ((int)row["AnnotationKeywordTargetID"].Value != keywordTargetID)
                            {
                                keywordTargetID = (int)row["AnnotationKeywordTargetID"].Value;
                                sbPageBlock.Append("\n\t\t\t<div class=\"target-section\">").Append(row["KeywordTargetName"].Value).Append("</div>");
                            }
                            sbPageBlock.Append("\n\t\t\t\t<div id=\"subject_").Append(row["AnnotationSubjectID"].Value).Append("\" class=\"subject-item\">").
                                        Append("<a href=\"/DLIndexBrowse.aspx?cat=").Append(row["AnnotationSubjectCategoryID"].Value).Append("&sub=").Append(Server.UrlEncode(row["AnnotationSubjectID"].Value.ToString())).Append("\" title=\"Index Browse\">").
                                        Append(row["SubjectCategoryName"].Value).Append(" - ").Append(row["SubjectText"].Value).
                                        Append("</a></div>");
                        }
                        sbPageBlock.Append("</div>"). //close section wrapper
                        Append("\n\t\t</div>");
                    }
                    #endregion

                    #region Get Concepts
                    CustomGenericList<CustomDataRow> concepts = provider.Annotation_AnnotationConceptSelectByAnnotationID(_ann.AnnotationID);
                    if (concepts.Count > 0)
                    {
                        int keywordTargetID = (int)concepts[0]["AnnotationKeywordTargetID"].Value;
                        sbPageBlock.Append("\n\t\t<div id=\"concepts_").Append(_ann.AnnotationID).Append("\" class=\"concept-list\">").
Append("<a href=\"javascript:void(0);\" onClick=\"toggleConceptSection(").Append(_ann.AnnotationID).Append(");\" title=\"Hide\">").
    Append("<img id=\"hide-concepts").Append(_ann.AnnotationID).Append("\" src=\"../Images/bib_minus.gif\" alt=\"hide subjects\" style=\"display:none\"/>").
Append("</a>").

Append("<a href=\"javascript:void(0);\" onClick=\"toggleConceptSection(").Append(_ann.AnnotationID).Append(");\" title=\"Show\">").
    Append("<img id=\"show-concepts").Append(_ann.AnnotationID).Append("\" src=\"../Images/bib_plus.gif\" alt=\"show subjects\"/>").
Append("</a>").
                                    Append("\n\t\t\t<span class=\"title\">concepts</span>").
                                    Append("<div id=\"concept-section-").Append(_ann.AnnotationID).Append("\" style=\"display:none;\">"). //section wrapper for toggling
                                    Append("\n\t\t\t<div class=\"target-section\">").Append(concepts[0]["KeywordTargetName"].Value).Append("</div>");
                        foreach (CustomDataRow row in concepts)
                        {
                            if ((int)row["AnnotationKeywordTargetID"].Value != keywordTargetID)
                            {
                                keywordTargetID = (int)row["AnnotationKeywordTargetID"].Value;
                                sbPageBlock.Append("\n\t\t\t<div class=\"target-section\">").Append(row["KeywordTargetName"].Value).Append("</div>");
                            }
                            sbPageBlock.Append("\n\t\t\t\t<div id=\"concept_").Append(_ann.AnnotationID).Append(row["AnnotationConceptCode"].Value).Append("\" class=\"concept-item\">").
                                        Append("<a href=\"/DLIndexBrowse.aspx?concept=").Append(row["AnnotationConceptCode"].Value).Append("\" title=\"Index Browse\">").
                                        Append(row["ConceptText"].Value).
                                        Append("</a></div>");
                        }
                        sbPageBlock.Append("</div>"). //close section wrapper
                        Append("\n\t\t</div>");
                    }
                    #endregion

                    #region Get Related Annotations
                    StringBuilder sbRelatedAnnotations = new StringBuilder();
                    foreach (CustomDataRow i in provider.AnnotationRelationSelectByAnnotationID(_ann.AnnotationID))
                    {
                        if (sbRelatedAnnotations.Length > 0) //more than one item, delimit
                            sbRelatedAnnotations.Append(",");
                        sbRelatedAnnotations.Append("<a href=\"/page/").Append(i["PageID"].Value.ToString()).Append("\" title=\"Page\">")
                                            .Append("<span id=\"related-item\">").Append(i["IndicatedPage"].Value.ToString()).Append("</span>")
                                            .Append("</a>");
                    }
                    if (sbRelatedAnnotations.Length > 0)
                    {
                        sbRelatedAnnotations.Insert(0, "\n\t\t<div id =\"related-annotations\"><span>Related Annotations:</span>&nbsp;");
                        sbPageBlock.Append(sbRelatedAnnotations.ToString()).Append("</div>");
                    }
                    #endregion
                    sbPageBlock.Append("\n\t<hr/>\n");              //separator for annotations
                }

                sbPageBlock.Insert(0, "<div id=\"AnnotationRepository\">");
                sbPageBlock.Append("</div>");

                ltlAnnotationContent.Text = sbPageBlock.ToString();
                ltlPageSequence.Text = sbScrollItems.ToString();
            }
        }
    
    }
}