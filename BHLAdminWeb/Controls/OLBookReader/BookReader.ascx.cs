using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using System.Text;
using System.Configuration;

namespace MOBOT.BHL.AdminWeb.Controls.OLBookReader
{
    public partial class BookReader : System.Web.UI.UserControl
    {
        private int? _itemID = null;
        public int? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private int? _numPages = null;
        public int? NumPages
        {
            get { return _numPages; }
            set { _numPages = value; }
        }

        private int? _startPage = null;
        public int? StartPage
        {
            get { return _startPage; }
            set { _startPage = value; }
        }

        private int _fixedImageWidth = 0;
        public int FixedImageWidth
        {
            get { return _fixedImageWidth; }
            set { _fixedImageWidth = value; }
        }

        private int _fixedImageHeight = 0;
        public int FixedImageHeight
        {
            get { return _fixedImageHeight; }
            set { _fixedImageHeight = value; }
        }

        private bool _showAnnotations = false;
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
            // Add references to the javascript and CSS needed by the book reader
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, ConfigurationManager.AppSettings["jQueryPath"]);
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, "/js/jquery.easing.1.3.js");
            ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, "/Controls/OLBookReader/BookReader/BookReader.js?v=1.3bhl");
            ControlGenerator.AddLinkControl(Page.Master.Page.Header.Controls, "/Controls/OLBookReader/BookReader/BookReader.css?v=1.2bhl");

            if (ShowAnnotations) setAnnotationContent();
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
            
            CustomGenericList< Annotation> annotationList = provider.AnnotationsSelectByItemID(_itemID.Value);
            
            if (annotationList != null && annotationList.Count > 0)
            {
                //this item has annotations, so set any flags to be used within the InitializeViewer javascript function
                _hasAnnotations = "true";

                ltlBookIndicator.Text = (provider.AnnotatedItemCheckForSurrogate(_itemID.Value) ? "Darwin's copy of this book" : "surrogate copy of this work");
                StringBuilder sbPageBlock = new StringBuilder(), 
                              sbScrollItems = new StringBuilder();

                int currentSequence = -1;
                foreach (Annotation _ann in annotationList)
                {
                    if (_ann.PageSequenceOrder != currentSequence)          //first or new page
                    {
                        if (currentSequence > 0)                            //already set, close current block before opening new one
                        {
                            sbPageBlock.Append("\n\t</div>\n\t</div>");
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
                plhAnnotations.Visible = true;
            }
        }
    }
}