using BHL.SiteServiceREST.v1.Client;
using BHL.SiteServicesREST.v1;
using Countersoft.Gemini.Commons.Entity;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ItemController : Controller
    {
        [EnableThrottling]
        [HttpGet]
        public ActionResult Index(string id, string idtype)
        {
            ViewerModel model = new ViewerModel();
            PageSummaryView pageSummary = new PageSummaryView();
            BHLProvider bhlProvider = new BHLProvider();
            bool getFirstPage = true;
            int pageid = int.MinValue;
            string qsTitleID = (string)Request.QueryString["t"];

            if (idtype == "page" && !string.IsNullOrWhiteSpace(id))
            {
                getFirstPage = false;
                pageSummary = GetPageSummaryForPageID(model, id, qsTitleID);
            }
            else if (idtype == "item" && !string.IsNullOrWhiteSpace(id))
            {
                pageSummary = GetPageSummaryForItemID(model, id, qsTitleID);
            }
            else if (idtype == "ia" && !string.IsNullOrWhiteSpace(id))
            {
                pageSummary = GetPageSummaryForBarcode(id, qsTitleID);
            }

            // Make sure something was found
            if (pageSummary == null) Response.Redirect("~/itemnotfound");

            // Get the publication details
            model = GetPublicationDetail(model, pageSummary);

            ViewBag.COinS = @"<span class=""Z3988"" title=""" + model.COinS.GetCOinS() + "\"></span>";

            // Make sure the item is published
            if (model.Status != 30 && model.Status != 40) Response.Redirect("~/itemunavailable");

            // IIIF toggle action
            if (ViewerRedirect()) Response.Redirect("/iiif" + Request.Url.AbsolutePath);

            // Set up for IIIF toggle
            ViewBag.IIIFLinkText = "Use the IIIF Book Viewer";
            if (model.Type == ItemType.Book)
                ViewBag.IIIFLinkTarget = "/iiif/item/" + model.ID + "?iiif=1";
            else if (model.Type == ItemType.Segment)
                ViewBag.IIIFLinkTarget = "/iiif/page/" + model.StartPageID + "?iiif=1";

            if (getFirstPage)
            {
                DataObjects.Page firstPage = bhlProvider.PageSelectFirstPageForItem(model.ItemID);
                model.PageSequence = firstPage.SequenceOrder ?? model.PageSequence;
                pageid = firstPage.PageID;
            }

            ViewBag.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], (String.IsNullOrEmpty(model.Volume) ? String.Empty : model.Volume + " - ") + model.ShortTitle);

            // Set Volume drop down list
            List<DataObjects.Book> books = bhlProvider
                .BookSelectByTitleId(model.TitleID)
                .ToList();

            int selectedIndex = 0;
            int bookIndex = 0;
            foreach (DataObjects.Book book in books)
            {
                //if (PublicationDetail.ItemID == book.ItemID) CurrentBook = book;
                if (string.IsNullOrWhiteSpace(book.Volume)) book.Volume = "Volume details";

                model.Volumes.Add(book.DisplayedShortVolume, book.IsVirtual.ToString() + "|" + book.BookID.ToString() + "|" + book.FirstSegmentStartPageID.ToString());

                if (book.IsVirtual == 1 && book.BookID == model.ContainerID) selectedIndex = bookIndex;
                else if (book.IsVirtual == 0 && book.BookID == model.ID) selectedIndex = bookIndex;
                bookIndex++;
            }

            model.VolumeSelectedIndex = selectedIndex;

            // Show contributing institution
            foreach (Institution institution in model.Institutions)
            {
                if ((institution.InstitutionRoleName == "Holding Institution" && model.Type == ItemType.Book) ||
                    (institution.InstitutionRoleName == "Contributor" && model.Type == ItemType.Segment))
                {
                    if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl))
                    {
                        model.ContributorName = institution.InstitutionName;
                        model.ContributorUrl = institution.InstitutionUrl;

                        HyperLink link = new HyperLink();
                        link.Text = institution.InstitutionName;
                        link.NavigateUrl = institution.InstitutionUrl;
                        link.Target = "_blank";
                        link.Attributes.Add("rel", "noopener noreferrer");
                    }
                    else
                    {
                        model.ContributorName = institution.InstitutionName;
                    }

                    ViewBag.holdingInstitution = institution.InstitutionCode.Replace("\"", "");
                    break;
                }
            }

            // Set the Pages drop down list   
            List<Page> pages = new List<Page>();
            if (model.Type == ItemType.Book)
                pages = bhlProvider.PageMetadataSelectByItemID(model.ID);
            else if (model.Type == ItemType.Segment)
                pages = bhlProvider.PageMetadataSelectBySegmentID(model.ID);

            foreach(var page in pages)
            {
                model.PageList.Add(new Tuple<string, string>(page.PageID.ToString(), page.WebDisplay));
            }
            model.PageCount = pages.Count;

            // Set the list of related Segments (listbox used by iDevices)
            foreach(var segment in model.Children)
            {
                model.SegmentList.Add(new Tuple<string, string>(segment.StartPageID.ToString(), segment.Title));
            }

            ViewBag.bookID = (model.Type == ItemType.Book ? model.ID.ToString() : "s" + model.ID.ToString());
            ViewBag.sponsor =
                model.Sponsor == null ?
                string.Empty :
                model.Sponsor.Replace("\"", "");

            // Check and set up Annotations
            if (model.Type == ItemType.Book)
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAnnotations"])) setAnnotationContent(model);
            }

            // Add Google Scholar metadata to the page headers
            model.ScholarTags = SetGoogleScholarTags(model.Type, model.ID);

            // Serialize only the information we need
            List<ViewerPageModel> viewerPages = new List<ViewerPageModel>();

            List<PageSummaryView> pageviews = new List<PageSummaryView>();
            if (model.Type == ItemType.Book)
            {
                pageviews = bhlProvider.PageSummarySelectForViewerByItemID(model.ID);
            }
            else if (model.Type == ItemType.Segment)
            {
                pageviews = bhlProvider.PageSummarySelectForViewerBySegmentID(model.ID);
            }

            if (pageviews.Count > 0)
            {
                model.PageProgression = pageviews[0].PageProgression;
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
            if (model.Type == ItemType.Book)
            {
                viewerPages = client.GetItemPageImageDimensions(model.ID, viewerPages).ToList<ViewerPageModel>();
            }
            else
            {
                viewerPages = client.GetSegmentPageImageDimensions(model.ID, viewerPages).ToList<ViewerPageModel>();
            }

            model.Pages = JsonConvert.SerializeObject(pages.ToList().Join(viewerPages,
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

            return View(model);
        }

        /// <summary>
        /// Set the Google Scholar tags for the page
        /// </summary>
        private List<KeyValuePair<string, string>> SetGoogleScholarTags(ItemType type, int entityid)
        {
            BHLProvider bhlProvider = new BHLProvider();
            List<KeyValuePair<string, string>> tags = new List<KeyValuePair<string, string>>();

            if (type == ItemType.Book)
            {
                tags = bhlProvider.GetGoogleScholarMetadataForItem(entityid, ConfigurationManager.AppSettings["ItemPageUrl"]);
            }
            else if (type == ItemType.Segment)
            {
                tags = bhlProvider.GetGoogleScholarMetadataForSegment(entityid, ConfigurationManager.AppSettings["PartPageUrl"]);
            }

            return tags;
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
        private void setAnnotationContent(ViewerModel model)
        {
            //Set Annotation content
            BHLProvider provider = new BHLProvider();

            List<Annotation> annotationList = provider.AnnotationsSelectByItemID(model.ItemID);

            if (annotationList != null && annotationList.Count > 0)
            {
                //this item has annotations, so set any flags to be used within the InitializeViewer javascript function
                model.HasAnnotations = "true";

                model.SurrogateText = (provider.AnnotatedItemCheckForSurrogate(model.ItemID) ? "Darwin's copy of this book" : "surrogate copy of this work");
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

                model.AnnotationContent = sbPageBlock.ToString();
                model.PageSequenceString = sbScrollItems.ToString();
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

        private PageSummaryView GetPageSummaryForPageID(ViewerModel model, string pageID, string titleID)
        {
            BHLProvider bhlProvider = new BHLProvider();
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

                    model.Type = ItemType.Book;
                    psv = bhlProvider.PageSummarySelectByPageId(pageid, titleid);
                    if (psv != null)
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

                    model.Type = ItemType.Segment;
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

        private PageSummaryView GetPageSummaryForItemID(ViewerModel model, string itemID, string titleID)
        {
            BHLProvider bhlProvider = new BHLProvider();
            PageSummaryView psv = null;

            model.Type = ItemType.Book;
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

        private PageSummaryView GetPageSummaryForSegmentID(ViewerModel model, string segmentID, string titleID)
        {
            BHLProvider bhlProvider = new BHLProvider();
            PageSummaryView psv = null; ;

            model.Type = ItemType.Segment;
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
            BHLProvider bhlProvider = new BHLProvider();
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

        private ViewerModel GetPublicationDetail(ViewerModel publicationDetail, PageSummaryView pageSummary)
        {
            BHLProvider bhlProvider = new BHLProvider();

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
                List<DataObjects.Author> authorList = bhlProvider.AuthorSelectByTitleId(publicationDetail.TitleID);
                foreach (DataObjects.Author author in authorList)
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
                publicationDetail.Children = bhlProvider.SegmentSelectByBookID(publicationDetail.ID);

                // Set the data for the COinS output
                publicationDetail.COinS.ItemID = publicationDetail.ID;
                publicationDetail.COinS.TitleAuthors = authorList;
                //publicationDetail.COinS.TitleKeywords = bhlProvider.TitleKeywordSelectByTitleID(pageSummary.TitleID);
                publicationDetail.COinS.Title = pageSummary.FullTitleExtended;
                publicationDetail.COinS.Volume = pageSummary.Volume;
                publicationDetail.COinS.PageCount = bhlProvider.PageSelectCountByItemID(publicationDetail.ID);
                publicationDetail.COinS.Date = book.StartYear;
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
                }

                // Get the list of related Segments
                publicationDetail.Children = bhlProvider.SegmentSelectSiblingSegmentsBySegmentID(publicationDetail.ID);

                // Set the data for the COinS output
                publicationDetail.COinS.SegmentID = publicationDetail.ID;
                publicationDetail.COinS.ItemAuthors = authorList;
                //publicationDetail.COinS.ItemKeywords = bhlProvider.SegmentKeywordSelectBySegmentID(publicationDetail.ID);
                publicationDetail.COinS.Genre = segment.GenreName;
                publicationDetail.COinS.ArticleTitle = segment.Title;
                publicationDetail.COinS.Title = segment.ContainerTitle;
                publicationDetail.COinS.Volume = segment.Volume;
                publicationDetail.COinS.Issue = segment.Issue;
                publicationDetail.COinS.StartPageNumber = segment.StartPageNumber;
                publicationDetail.COinS.EndPageNumber = segment.EndPageNumber;
                publicationDetail.COinS.PageRange = segment.PageRange;
                publicationDetail.COinS.Language = segment.LanguageCode;
                publicationDetail.COinS.Date = segment.Date;
            }

            // Used for "Select title" links
            publicationDetail.Titles = bhlProvider.TitleSelectByItem(publicationDetail.ID);

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
                    publicationDetail.COinS.MarcLeader = title.MARCLeader;
                    publicationDetail.COinS.Publisher = title.Datafield_260_b;
                    publicationDetail.COinS.PublisherPlace = title.Datafield_260_a;
                    publicationDetail.COinS.Edition = title.EditionStatement;
                    publicationDetail.COinS.Language = title.LanguageCode;
                    if (string.IsNullOrWhiteSpace(publicationDetail.COinS.Date)) publicationDetail.COinS.Date = title.StartYear.ToString();
                }
            }

            // Get institutions
            publicationDetail.Institutions = GetPublicationInstitutions(publicationDetail);

            // Get DOI
            publicationDetail.DOI = GetPublicationDOI(publicationDetail);

            return publicationDetail;
        }

        // Get the institutions (Holding Institution, Rights Holder) to be displayed in the "Show Info" tab.  For segments that are part 
        // of virtual items, these should include institutions associated with both the item and the segment.
        private List<Institution> GetPublicationInstitutions(ViewerModel publicationDetail)
        {
            BHLProvider bhlProvider = new BHLProvider();

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

        private string GetPublicationDOI(ViewerModel publicationDetail)
        {
            BHLProvider bhlProvider = new BHLProvider();
            string doiName = string.Empty;

            if (publicationDetail.Type == ItemType.Book)
            {
                List<Title_Identifier> identifierList = bhlProvider.Title_IdentifierSelectByTitleID(publicationDetail.TitleID);
                foreach (Title_Identifier identifier in identifierList)
                {
                    if (identifier.IdentifierName == "DOI") { doiName = identifier.IdentifierValueDisplay; break; }
                }
                publicationDetail.COinS.TitleIdentifiers = identifierList;
            }
            else if (publicationDetail.Type == ItemType.Segment)
            {
                List<ItemIdentifier> dois = bhlProvider.ItemIdentifierSelectByNameAndID("DOI", publicationDetail.ID);
                if (dois.Count > 0) doiName = dois[0].IdentifierValueDisplay;
                publicationDetail.COinS.ItemIdentifiers = dois;
            }

            return doiName;
        }

        [EnableThrottling]
        // GET: /Item/Parts
        public ActionResult Parts()
        {
            DataObjects.Book BhlBook = new DataObjects.Book();
            Title BhlTitle = new Title();
            int itemID = 0;
            BHLProvider bhlProvider = new BHLProvider();

            if (!int.TryParse((string)RouteData.Values["itemid"], out itemID))
            {
                Response.Redirect("~/pagenotfound");
            }

            ViewBag.BhlBook = bhlProvider.BookSelectByBarcodeOrItemID(itemID, null);
            if (ViewBag.BhlBook == null)
            {
                Response.Redirect("~/pagenotfound");
            }
            else
            {
                ViewBag.BhlTitle = bhlProvider.TitleSelect((int)ViewBag.BhlBook.PrimaryTitleID);
                List<Segment> segments = bhlProvider.SegmentSelectByBookID(ViewBag.BhlBook.BookID);
                if (!(segments == null)) ViewBag.SegmentList = segments;
            }

            return View();
        }


        [EnableThrottling]
        public ActionResult GetItemText(int? itemid)
        {
            if (!itemid.HasValue)
            {
                return Redirect("~/pagenotfound");
            }
            else
            {
                BHLProvider provider = new BHLProvider();

                Item item = provider.ItemSelectFilenames(ItemType.Book, (int)itemid);
                string itemtextPath = provider.GetRemoteFilePath(RemoteFileType.ItemText, item.BarCode, item.TextFilename, pathItemType: PathItemType.Item, itemID: itemid);

                if (itemtextPath.Contains("archive.org"))
                {
                    // Before redirecting to archive.org, make sure BHL doesn't have an updated version of the text
                    if (provider.ItemSelectHasNonOcrText(ItemType.Book, (int)itemid))
                    {
                        // Read from local BHL storage if the text includes non-OCR sources
                        string itemText;
                        string cacheKey = "ItemText" + itemid.ToString();
                        System.Web.Caching.Cache cache = new System.Web.Caching.Cache();

                        if (cache[cacheKey] != null)
                        {
                            // Use cached version
                            itemText = cache[cacheKey].ToString();
                        }
                        else
                        {
                            // Refresh cache
                            Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                            itemText = client.GetItemText((int)itemid);
                            cache.Add(cacheKey, itemText, null, DateTime.Now.AddMinutes(
                                Convert.ToDouble(ConfigurationManager.AppSettings["ItemTextCacheTime"])),
                                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                        }

                        Response.Cache.SetNoTransforms();
                        ContentResult content = new ContentResult();
                        content.Content = itemText;
                        content.ContentType = "text/plain";
                        return content;
                    }
                    else
                    {
                        // No updated copy of the text, so redirect to remote storage at archive.org
                        return Redirect(itemtextPath);
                    }
                }
                else
                {
                    // Remote path is not archive.org, so redirect to it
                    return Redirect(itemtextPath);
                }
            }
        }

        [EnableThrottling]
        public ActionResult GetItemPdf(int itemid)
        {
            BHLProvider provider = new BHLProvider();
            DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Book, itemid);

            if (!string.IsNullOrWhiteSpace(item.PdfFilename))
            {
                try
                {
                    var filePath = provider.GetRemoteFilePath(RemoteFileType.Pdf, item.BarCode, item.PdfFilename);

                    // Check if the file exists before redirecting to it
                    var exists = false;
                    using (var client = new HttpClient())
                    {
                        var request = new HttpRequestMessage(HttpMethod.Head, filePath);
                        var response = client.SendAsync(request).GetAwaiter().GetResult();
                        exists = response.IsSuccessStatusCode;
                    }
                    if (exists)
                    {
                        return Redirect(filePath);
                    }
                    else
                    {
                        return Redirect("~/pagenotfound");
                    }
                }
                catch (WebException wex)
                {
                    if (wex.Message.Contains("404"))
                    {
                        return Redirect("~/pagenotfound");
                    }
                    else
                    {
                        ExceptionUtility.LogException(wex, "ItemController.GetItemPdf");
                        return Redirect("~/error");
                    }
                }
            }
            else
            {
                return Redirect("~/pagenotfound");
            }
        }

        [EnableThrottling]
        public ActionResult GetItemImages(int itemid)
        {
            BHLProvider provider = new BHLProvider();
            DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Book, itemid);

            if (!string.IsNullOrWhiteSpace(item.ImagesFilename))
            {
                var filePath = provider.GetRemoteFilePath(RemoteFileType.ImageZip, item.BarCode, item.ImagesFilename);
                return Redirect(filePath);
            }
            else
            {
                return Redirect("~/pagenotfound");
            }
        }
    }
}