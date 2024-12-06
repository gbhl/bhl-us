using BHL.SiteServiceREST.v1.Client;
using BHL.SiteServicesREST.v1;
using Countersoft.Foundation.Commons.Core;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.Web2
{
    public partial class TitlePage : BasePage
    {
        protected Publication PublicationDetail { get; set; } = new Publication();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSummaryView pageSummary = new PageSummaryView();

            if (!Page.IsPostBack)
            {
                bool getFirstPage = true;

                int pageid = int.MinValue;

                if (RouteData.Values["pageid"] != null)
                {
                    getFirstPage = false;
                    pageSummary = GetPageSummaryForPageID((string)RouteData.Values["pageid"], (string)Context.Request.QueryString["t"]);
                }
                else if (RouteData.Values["titleid"] != null)
                {
                    Response.Redirect("~/bibliography/" + (string)RouteData.Values["titleid"]);
                }
                else if (RouteData.Values["itemid"] != null)
                {
                    pageSummary = GetPageSummaryForItemID((string)RouteData.Values["itemid"], (string)Context.Request.QueryString["t"]);
                }
                else if (RouteData.Values["segmentid"] != null)
                {
                    pageSummary = GetPageSummaryForSegmentID((string)RouteData.Values["segmentid"], (string)Context.Request.QueryString["t"]);
                }
                else if (RouteData.Values["iabarcode"] != null)
                {
                    pageSummary = GetPageSummaryForBarcode((string)RouteData.Values["iabarcode"], (string)Context.Request.QueryString["t"]);
                }

                // Make sure something was found
                if (pageSummary == null) Response.Redirect("~/itemnotfound");

                // Get the publication details
                PublicationDetail = GetPublicationDetail(PublicationDetail, pageSummary);

                // Make sure the item is published
                if (PublicationDetail.Status != 30 && PublicationDetail.Status != 40) Response.Redirect("~/itemunavailable");

                // IIIF toggle action
                if (ViewerRedirect()) Response.Redirect("/iiif" + Request.Url.AbsolutePath);

                if (getFirstPage)
                {
                    Page firstPage = bhlProvider.PageSelectFirstPageForItem(PublicationDetail.ItemID);
                    PublicationDetail.PageSequence = firstPage.SequenceOrder ?? PublicationDetail.PageSequence;
                    pageid = firstPage.PageID;
                }

                Page.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], (String.IsNullOrEmpty(PublicationDetail.Volume) ? String.Empty : PublicationDetail.Volume + " - ") + PublicationDetail.ShortTitle);

                // Set Volume drop down list
                List<DataObjects.Book> books = bhlProvider
                    .BookSelectByTitleId(PublicationDetail.TitleID)
                    .ToList();

                int selectedIndex = 0;
                int bookIndex = 0;
                foreach (DataObjects.Book book in books)
                {
                    //if (PublicationDetail.ItemID == book.ItemID) CurrentBook = book;
                    if (string.IsNullOrWhiteSpace(book.Volume)) book.Volume = "Volume details";

                    ddlVolumes.Items.Add(new ListItem(book.DisplayedShortVolume, book.IsVirtual.ToString() + "|" + book.BookID.ToString() + "|" + book.FirstSegmentStartPageID.ToString()));

                    if (book.IsVirtual == 1 && book.BookID == PublicationDetail.ContainerID) selectedIndex = bookIndex;
                    else if (book.IsVirtual == 0 && book.BookID == PublicationDetail.ID) selectedIndex = bookIndex;
                    bookIndex++;
                }

                ddlVolumes.SelectedIndex = selectedIndex;

                // Show contributing institution
                foreach (Institution institution in PublicationDetail.Institutions)
                {
                    if ((institution.InstitutionRoleName == "Holding Institution" && PublicationDetail.Type == ItemType.Book) ||
                        (institution.InstitutionRoleName == "Contributor" && PublicationDetail.Type == ItemType.Segment))
                    {
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl))
                        {
                            HyperLink link = new HyperLink();
                            link.Text = institution.InstitutionName;
                            link.NavigateUrl = institution.InstitutionUrl;
                            link.Target = "_blank";
                            link.Attributes.Add("rel", "noopener noreferrer");
                            attributionPlaceHolder.Controls.Add(link);
                        }
                        else
                        {
                            Literal literal = new Literal();
                            literal.Text = institution.InstitutionName;
                            attributionPlaceHolder.Controls.Add(literal);
                        }

                        ((Book)this.Master).holdingInstitution = institution.InstitutionCode.Replace("\"", "");
                        break;
                    }
                }

                // Set the Pages drop down list   
                List<Page> pages = new List<Page>();
                if (PublicationDetail.Type == ItemType.Book)
                    pages = bhlProvider.PageMetadataSelectByItemID(PublicationDetail.ID);
                else if (PublicationDetail.Type == ItemType.Segment)
                    pages = bhlProvider.PageMetadataSelectBySegmentID(PublicationDetail.ID);

                lstPages.DataSource = pages;
                lstPages.DataTextField = "WebDisplay";
                lstPages.DataValueField = "PageID";
                lstPages.DataBind();
                PublicationDetail.PageCount = pages.Count;

                // Add text to display when hovering over a listbox row
                foreach (ListItem item in lstPages.Items)
                    item.Attributes["title"] = item.Text;

                // Set the list of related Segments (listbox used by iDevices)
                lbSegments.DataSource = PublicationDetail.Children;
                lbSegments.DataTextField = "Title";
                lbSegments.DataValueField = "StartPageID";
                lbSegments.DataBind();

                ((Book)this.Master).bookID = (PublicationDetail.Type == ItemType.Book ? PublicationDetail.ID.ToString() : "s" + PublicationDetail.ID.ToString());
                ((Book)this.Master).sponsor =
                    PublicationDetail.Sponsor == null ?
                    string.Empty :
                    PublicationDetail.Sponsor.Replace("\"", "");

                // Check and set up Annotations
                if (PublicationDetail.Type == ItemType.Book)
                {
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAnnotations"])) setAnnotationContent(PublicationDetail.ItemID);
                }

                // Add Google Scholar metadata to the page headers
                SetGoogleScholarTags(PublicationDetail.Type, PublicationDetail.ID);

                // Serialize only the information we need
                List<ViewerPageModel> viewerPages = new List<ViewerPageModel>();

                List<PageSummaryView> pageviews = new List<PageSummaryView>();
                if (PublicationDetail.Type == ItemType.Book)
                {
                    pageviews = bhlProvider.PageSummarySelectForViewerByItemID(PublicationDetail.ID);
                }
                else if (PublicationDetail.Type == ItemType.Segment)
                {
                    pageviews = bhlProvider.PageSummarySelectForViewerBySegmentID(PublicationDetail.ID);
                }

                if (pageviews.Count > 0)
                {
                    PublicationDetail.PageProgression = pageviews[0].PageProgression;
                    foreach (PageSummaryView pageview in pageviews)
                    {
                        ViewerPageModel viewerPage = new ViewerPageModel
                        {
                            ExternalBaseUrl = pageview.ExternalBaseURL,
                            BarCode = pageview.BarCode,
                            FlickrUrl = pageview.FlickrUrl,
                            SequenceOrder = pageview.SequenceOrder
                        };
                        viewerPages.Add(viewerPage);
                    }
                }

                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                if (PublicationDetail.Type == ItemType.Book)
                {
                    viewerPages = client.GetItemPageImageDimensions(PublicationDetail.ID, viewerPages).ToList<ViewerPageModel>();
                }
                else
                {
                    viewerPages = client.GetSegmentPageImageDimensions(PublicationDetail.ID, viewerPages).ToList<ViewerPageModel>();
                }

                PublicationDetail.Pages = JsonConvert.SerializeObject(pages.ToList().Join(viewerPages,
                                                p => p.SequenceOrder,
                                                vp => vp.SequenceOrder,
                                                (p, vp) => new
                                                {
                                                    p.PageID,
                                                    p.WebDisplay,
                                                    p.SequenceOrder,
                                                    p.BarCode,
                                                    p.SegmentID,
                                                    p.GenreName,
                                                    p.TextSource,
                                                    vp.ExternalBaseUrl,
                                                    vp.Height,
                                                    vp.Width,
                                                    vp.FlickrUrl
                                                }));
            }
        }

        /// <summary>
        /// Set the Google Scholar tags for the page
        /// </summary>
        private void SetGoogleScholarTags(ItemType type, int entityid)
        {
            List<KeyValuePair<string, string>> tags = new List<KeyValuePair<string, string>>();

            if (type == ItemType.Book)
            {
                tags = bhlProvider.GetGoogleScholarMetadataForItem(entityid, ConfigurationManager.AppSettings["ItemPageUrl"]);
            }
            else if (type == ItemType.Segment)
            {
                tags = bhlProvider.GetGoogleScholarMetadataForSegment(entityid, ConfigurationManager.AppSettings["PartPageUrl"]);
            }

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
        private void setAnnotationContent(int itemID)
        {
            //Set Annotation content
            BHLProvider provider = new BHLProvider();

            List<Annotation> annotationList = provider.AnnotationsSelectByItemID(itemID);

            if (annotationList != null && annotationList.Count > 0)
            {
                //this item has annotations, so set any flags to be used within the InitializeViewer javascript function
                PublicationDetail.HasAnnotations = "true";

                ltlBookIndicator.Text = (provider.AnnotatedItemCheckForSurrogate(itemID) ? "Darwin's copy of this book" : "surrogate copy of this work");
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
                    List<AnnotationNote> note_list = provider.AnnotationNoteSelectByAnnotationID(_ann.AnnotationID);
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
                    List<CustomDataRow> subjects = provider.AnnotationSubjectSelectByAnnotationID(_ann.AnnotationID);
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
                    List<CustomDataRow> concepts = provider.Annotation_AnnotationConceptSelectByAnnotationID(_ann.AnnotationID);
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

        private bool ListContainsAuthor(IList<Author> list, int authorID, string relationship)
        {
            bool containsAuthor = false;

            foreach (Author author in list)
            {
                if (author.AuthorID == authorID && author.Relationship == relationship)
                {
                    containsAuthor = true;
                    break;
                }
            }

            return containsAuthor;
        }

        /// <summary>
        /// Toggle IIIF behavior
        /// </summary>
        /// <returns></returns>
        private bool ViewerRedirect()
        {
            bool redirect = false;
            bool switchViewer = false;

            // If IIIF usage is turned on, immediately redirect to the original search
            if (ConfigurationManager.AppSettings["IIIFState"] == "on") return true;

            // If IIIF usage is turned off, never redirect
            if (ConfigurationManager.AppSettings["IIIFState"] == "off") return false;

            // Toggle mode, so need to see if user switched to or from IIIF viewing

            // User requested to switch to iiif book viewer, so set cookie
            if (Request.QueryString["iiif"] == "0")
            {
                // Set cookie to use the iiif viewer
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("iiifviewer");
                cookie.Value = "0";
                cookie.Expires = DateTime.Now.AddDays(7);
                cookie.Domain = ".biodiversitylibrary.org";
                Response.Cookies.Add(cookie);

                switchViewer = true;
            }

            // If IIIF viewer cookie exists, then check its value to determine if redirect is needed
            if (Request.Cookies["iiifviewer"] != null && !switchViewer)
            {
                if (Request.Cookies["iiifviewer"].Value == "1") redirect = true;
            }

            return redirect;
        }

        private PageSummaryView GetPageSummaryForPageID(string pageID, string titleID)
        {
            PageSummaryView psv = null;

            if (int.TryParse(pageID, out int pageid))
            {
                int? titleid = int.TryParse(titleID, out int tmp) ? (int?)tmp : null;

                Page page = bhlProvider.PageSelectAuto(pageid);
                if (page == null) Response.Redirect("~/pagenotfound");  // Page ID does not exist

                DataObjects.Book book = bhlProvider.BookSelectByPageID(pageid);

                // Get the data for book/segment.  If book/segment has been replaced, redirect to the target book/segment.  That will
                // not find the correct page, but at least puts the user in the correct book/segment... better than "not found".
                if (book != null)
                {
                    if (!page.Active)   // Page ID exists, but is inactive
                    {
                        if (book.RedirectBookID != null)
                            Response.Redirect("~/item/" + book.RedirectBookID); // Follow container item redirect
                        else
                            Response.Redirect("~/item/" + book.BookID + (string.IsNullOrWhiteSpace(titleID) ? "" : "?t=" + titleID));     // Show container item
                    }

                    PublicationDetail.Type = ItemType.Book;
                    psv = bhlProvider.PageSummarySelectByPageId(pageid, titleid);
                    if (psv!= null)
                    {
                        // Page active, but container item redirected
                        if (psv.RedirectBookID != null) Response.Redirect("~/item/" + psv.RedirectBookID);
                    }
                }
                else
                {
                    Segment segment = bhlProvider.SegmentSelectByPageID(pageid);

                    if (!page.Active)   // Page ID exists, but is inactive
                    {
                        if (segment.RedirectSegmentID != null)
                            Response.Redirect("~/part/" + segment.RedirectSegmentID); // Follow container item redirect to landing page
                        else
                            Response.Redirect("~/part/" + segment.SegmentID);     // Show container item landing page
                    }

                    PublicationDetail.Type = ItemType.Segment;
                    psv = bhlProvider.PageSummarySegmentSelectByPageID(pageid, titleid);
                    if (psv != null)
                    {
                        // Page active, but container item redirected
                        if (psv.RedirectBookID != null) Response.Redirect("~/part/" + psv.RedirectBookID);
                    }
                }
            }

            return psv;
        }

        private PageSummaryView GetPageSummaryForItemID(string itemID, string titleID)
        {
            PageSummaryView psv = null;

            PublicationDetail.Type = ItemType.Book;
            int itemid;
            if (int.TryParse(itemID, out itemid))
            {
                int? qsTitleId = int.TryParse(titleID, out int tmp) ? (int?)tmp : null;

                // If we came from the bibliography page, get the title id
                int? refererTitleId = null;
                String referer = Request.ServerVariables["HTTP_REFERER"];
                if (referer != null)
                {
                    String host = Request.ServerVariables["HTTP_HOST"];
                    String bibPath = "http://" + (host ?? String.Empty) + "/bibliography/";
                    if (referer.StartsWith(bibPath, true, null))
                    {
                        referer = referer.Replace(bibPath, String.Empty);
                        refererTitleId = int.TryParse(referer, out int tmpid) ? (int?)tmpid : null;
                    }
                }

                int? titleid = qsTitleId ?? refererTitleId;
                psv = bhlProvider.PageSummarySelectByItemId(itemid, titleid);

                // Check to make sure this item hasn't been replaced.  If it has, redirect to the appropriate itemid.
                if (psv != null)
                {
                    if (psv.RedirectBookID != null) Response.Redirect("~/item/" + psv.RedirectBookID);
                }
                else
                {
                    // If no pages then see if this is a virtual item (redirect to itemdetails) or 
                    // an external item (redirect to the external url)
                    DataObjects.Book book = bhlProvider.BookSelectAuto(itemid);
                    if (book != null)
                    {
                        if (book.IsVirtual == 1) Response.Redirect("~/itemdetails/" + book.BookID);
                        if (!string.IsNullOrWhiteSpace(book.ExternalUrl)) Response.Redirect(book.ExternalUrl);
                    }
                }
            }

            return psv;
        }

        private PageSummaryView GetPageSummaryForSegmentID(string segmentID, string titleID)
        {
            PageSummaryView psv = null; ;

            PublicationDetail.Type = ItemType.Segment;
            if (int.TryParse(segmentID, out int segmentid))
            {
                int? titleid = int.TryParse(titleID, out int tmp) ? (int?)tmp : null;

                psv = bhlProvider.PageSummarySegmentSelectBySegmentID(segmentid, titleid);
                if (psv == null)
                {
                    // If no pages then see if this is an external segment (redirect to the url)
                    Segment segment = bhlProvider.SegmentSelectAuto(segmentid);
                    if (segment != null)
                    {
                        if (!string.IsNullOrWhiteSpace(segment.Url)) Response.Redirect(segment.Url);
                    }
                }
                else if (psv.IsVirtual == 0)
                {
                    // Associated with a non-virtual item, so redirect to the start page
                    Response.Redirect("~/page/" + psv.PageID.ToString() + (string.IsNullOrWhiteSpace(titleID) ? "" : "?t=" + titleID));
                }
            }

            return psv;
        }

        private PageSummaryView GetPageSummaryForBarcode(string barcode, string titleID)
        {
            PageSummaryView psv = null;

            DataObjects.Book book = bhlProvider.BookSelectByBarcodeOrItemID(null, barcode);
            if (book != null)
            {
                Response.Redirect("~/item/" + book.BookID + (string.IsNullOrWhiteSpace(titleID) ? "" : "?t=" + titleID));
            }
            else
            {
                Segment segment = bhlProvider.SegmentSelectByBarCode(barcode);
                if (segment != null) Response.Redirect("~/page/" + segment.StartPageID + (string.IsNullOrWhiteSpace(titleID) ? "" : "?t=" + titleID));
            }

            return psv;
        }

        private Publication GetPublicationDetail(Publication publicationDetail, PageSummaryView pageSummary)
        {
            publicationDetail.Status = pageSummary.ItemStatusID;
            publicationDetail.ID = pageSummary.BookID;
            publicationDetail.ItemID = pageSummary.ItemID;
            publicationDetail.TitleID = pageSummary.TitleID;
            if (pageSummary.TitleID != pageSummary.PrimaryTitleID) publicationDetail.RequestedTitleID = pageSummary.TitleID.ToString();
            publicationDetail.BarCode = pageSummary.BarCode;
            publicationDetail.FullTitle = pageSummary.FullTitleExtended;
            publicationDetail.ShortTitle = pageSummary.ShortTitle;
            publicationDetail.Volume = pageSummary.Volume;
            publicationDetail.Sponsor = pageSummary.Sponsor;
            publicationDetail.PageSequence = pageSummary.SequenceOrder;
            publicationDetail.PageProgression = pageSummary.PageProgression;
            publicationDetail.DownloadUrl = pageSummary.DownloadUrl;

            if (publicationDetail.Type == ItemType.Book)
            {
                // Get Details
                DataObjects.Book book = bhlProvider.BookSelectByBarcodeOrItemID(publicationDetail.ID, null);
                publicationDetail.StartYear = book.StartYear;
                publicationDetail.EndYear = book.EndYear;
                publicationDetail.Description = book.ItemDescription;
                publicationDetail.LicenseUrl = book.LicenseUrl;
                publicationDetail.Rights = book.Rights;
                publicationDetail.DueDiligence = book.DueDiligence;
                publicationDetail.CopyrightStatus = book.CopyrightStatus;

                // Get Authors
                List<Author> authorList = bhlProvider.AuthorSelectByTitleId(publicationDetail.TitleID);
                foreach (Author author in authorList)
                {
                    if (author.AuthorRoleID >= 1 && author.AuthorRoleID <= 3)
                    {
                        if (!ListContainsAuthor(publicationDetail.Authors, author.AuthorID, author.Relationship)) publicationDetail.Authors.Add(author);
                    }
                    else
                    {
                        if (!ListContainsAuthor(publicationDetail.Authors, author.AuthorID, author.Relationship) &&
                            !ListContainsAuthor(publicationDetail.AdditionalAuthors, author.AuthorID, author.Relationship)) publicationDetail.AdditionalAuthors.Add(author);
                    }
                }

                // Get the list of related Segments
                PublicationDetail.Children = bhlProvider.SegmentSelectByBookID(PublicationDetail.ID);

                // Set the data for the COinS output
                COinS.ItemID = PublicationDetail.ID;
                COinS.TitleAuthors = authorList;
                //COinS.TitleKeywords = bhlProvider.TitleKeywordSelectByTitleID(pageSummary.TitleID);
                COinS.Title = pageSummary.FullTitleExtended;
                COinS.Volume = pageSummary.Volume;
                COinS.PageCount = bhlProvider.PageSelectCountByItemID(publicationDetail.ID);
                COinS.Date = book.StartYear;
            }
            else if (publicationDetail.Type == ItemType.Segment)
            {
                // Get Details
                Segment segment = bhlProvider.SegmentSelectForSegmentID(publicationDetail.ID);
                publicationDetail.ArticleTitle = segment.Title;
                publicationDetail.Genre = segment.GenreName;
                publicationDetail.ContainerID = segment.BookID;
                publicationDetail.StartYear = segment.Date;
                publicationDetail.PublicationDetails = segment.PublicationDetails;
                publicationDetail.LicenseUrl = segment.LicenseUrl;
                publicationDetail.Rights = segment.RightsStatement;
                publicationDetail.CopyrightStatus = segment.RightsStatus;
                publicationDetail.StartPageID = segment.StartPageID;

                // Get Authors
                List<ItemAuthor> authorList = bhlProvider.SegmentAuthorSelectBySegmentID(publicationDetail.ID);
                foreach (ItemAuthor author in authorList)
                {
                    Author itemAuthor = new Author(
                        author.AuthorID, null, author.StartDate, author.EndDate, author.Numeration, author.Title, 
                        string.Empty, author.Unit, author.Location, string.Empty, 1, null, null, null, null, null
                        )
                    {
                        FullName = author.FullName,
                        FullerForm = author.FullerForm
                    };
                    publicationDetail.Authors.Add(itemAuthor);

                    /*
                    publicationDetail.Authors.Add(new Author()
                    {
                        AuthorID = author.AuthorID,
                        FullName = author.FullName,
                        Numeration = author.Numeration,
                        Unit = author.Unit,
                        Title = author.Title,
                        Location = author.Location,
                        FullerForm = author.FullerForm,
                        StartDate = author.StartDate,
                        EndDate = author.EndDate
                    });
                    */
                }

                // Get the list of related Segments
                PublicationDetail.Children = bhlProvider.SegmentSelectSiblingSegmentsBySegmentID(publicationDetail.ID);

                // Set the data for the COinS output
                COinS.SegmentID = PublicationDetail.ID;
                COinS.ItemAuthors = authorList;
                //COinS.ItemKeywords = bhlProvider.SegmentKeywordSelectBySegmentID(publicationDetail.ID);
                COinS.Genre = segment.GenreName;
                COinS.ArticleTitle = segment.Title;
                COinS.Title = segment.ContainerTitle;
                COinS.Volume = segment.Volume;
                COinS.Issue = segment.Issue;
                COinS.StartPageNumber = segment.StartPageNumber;
                COinS.EndPageNumber = segment.EndPageNumber;
                COinS.PageRange = segment.PageRange;
                COinS.Language = segment.LanguageCode;
                COinS.Date = segment.Date;
            }

            // Used to set up the bibliogaphy link
            publicationDetail.TitleCount = bhlProvider.TitleSelectByItem(PublicationDetail.ID).Count;

            // Get the title genre
            Title title = bhlProvider.TitleSelectAuto(publicationDetail.TitleID);
            if (title != null)
            {
                if (publicationDetail.Type == ItemType.Book) publicationDetail.PublicationDetails = title.PublicationDetails;
                BibliographicLevel bibliographicLevel = bhlProvider.BibliographicLevelSelect(title.BibliographicLevelID ?? 0);
                publicationDetail.TitleGenre = (bibliographicLevel == null) ? string.Empty : bibliographicLevel.BibliographicLevelLabel;

                // Set the data for the COinS output
                if (publicationDetail.Type == ItemType.Book)
                {
                    COinS.MarcLeader = title.MARCLeader;
                    COinS.Publisher = title.Datafield_260_b;
                    COinS.PublisherPlace = title.Datafield_260_a;
                    COinS.Edition = title.EditionStatement;
                    COinS.Language = title.LanguageCode;
                    if (string.IsNullOrWhiteSpace(COinS.Date)) COinS.Date = title.StartYear.ToString();
                }
            }

            // Get institutions
            publicationDetail.Institutions = GetPublicationInstitutions(PublicationDetail);

            // Get DOI
            publicationDetail.DOI = GetPublicationDOI(PublicationDetail);

            return publicationDetail;
        }

        // Get the institutions (Holding Institution, Rights Holder) to be displayed in the "Show Info" tab.  For segments that are part 
        // of virtual items, these should include institutions associated with both the item and the segment.
        private List<Institution> GetPublicationInstitutions(Publication publicationDetail)
        {
            // Get the institutions directly associated with the publication
            List<Institution> institutions = bhlProvider.InstitutionSelectByItemID(publicationDetail.ItemID);

            // If this is a segment, then add institutions related to the container
            if (publicationDetail.Type == ItemType.Segment && publicationDetail.ContainerID != null)
            {
                DataObjects.Book book = bhlProvider.BookSelectAuto((int)publicationDetail.ContainerID);
                institutions.AddRange(bhlProvider.InstitutionSelectByItemID(book.ItemID));
            }

            return institutions;
        }

        private string GetPublicationDOI(Publication publicationDetail)
        {
            string doiName = string.Empty;

            if (publicationDetail.Type == ItemType.Book)
            {
                List<Title_Identifier> identifierList = bhlProvider.Title_IdentifierSelectByTitleID(publicationDetail.TitleID);
                foreach (Title_Identifier identifier in identifierList)
                {
                    if (identifier.IdentifierName == "DOI") { doiName = identifier.IdentifierValueDisplay; break; }
                }
                COinS.TitleIdentifiers = identifierList;
            }
            else if (publicationDetail.Type == ItemType.Segment)
            {
                List<ItemIdentifier> dois = bhlProvider.ItemIdentifierSelectByNameAndID("DOI", publicationDetail.ID);
                if (dois.Count > 0) doiName = dois[0].IdentifierValueDisplay;
                COinS.ItemIdentifiers = dois;
            }

            return doiName;
        }


        protected class Publication
        {
            public ItemType Type { get; set; }
            public int Status { get; set; }
            public int ID { get; set; }
            public int ItemID { get; set; }
            public int? ContainerID { get; set; }
            public int ContainerItemID { get; set; }
            public int? StartPageID { get; set; }
            public int TitleCount { get; set; }
            public int TitleID { get; set; }
            public string RequestedTitleID { get; set; }
            public string TitleGenre { get; set; }
            public string Genre { get; set; }
            public string DOI { get; set; }
            public string BarCode { get; set; }
            public string FullTitle { get; set; }
            public string ShortTitle { get; set; }
            public string ArticleTitle { get; set; }
            public string Volume { get; set; }
            public string PublicationDetails { get; set; }
            public string StartYear { get; set; }
            public string EndYear { get; set; }
            public string Description { get; set; }
            public string Sponsor { get; set; }
            public string LicenseUrl { get; set; }
            public string Rights { get; set; }
            public string DueDiligence { get; set; }
            public string CopyrightStatus { get; set; }
            public string DownloadUrl { get; set; }
            public string PageProgression { get; set; } = string.Empty;
            public string HasAnnotations { get; set; } = "false";
            public int PageSequence { get; set; }
            public int PageCount { get; set; }
            public string Pages { get; set; }
            public List<Author> Authors { get; set; } = new List<Author>();
            public List<Author> AdditionalAuthors { get; set; } = new List<Author>();
            public List<Segment> Children { get; set; } = new List<Segment>();
            public List<Institution> Institutions { get; set; } = new List<Institution>();
        }
    }
}