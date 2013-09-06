using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class dlindexbrowse : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            string annotationSubjectCategoryIDString = Request.QueryString["cat"] ?? string.Empty;
            int annotationSubjectCategoryID = 0;
            string subjectIdString = Request.QueryString["sub"] ?? string.Empty;
            int subjectId = 0;
            string conceptCode = Request.QueryString["concept"] ?? string.Empty;

            if (conceptCode != string.Empty ||
                (Int32.TryParse(annotationSubjectCategoryIDString, out annotationSubjectCategoryID) &&
                 Int32.TryParse(subjectIdString, out subjectId)))
            {
                litPages.Text = BuildPageList(conceptCode, annotationSubjectCategoryID, subjectId);
            }
        }

        private string BuildPageList(string conceptCode, int annotationSubjectCategoryID, int subjectId)
        {
            CustomGenericList<SearchBookResult> pages = null;
            string indexTerm = string.Empty;

            if (conceptCode == string.Empty)
            {
                AnnotationSubject sub = bhlProvider.AnnotationSubjectSelectAuto(subjectId);
                AnnotationSubjectCategory subCat = bhlProvider.AnnotationSubjectCategorySelectAuto(annotationSubjectCategoryID);
                if (sub != null && subCat != null) indexTerm = subCat.SubjectCategoryName + " - " + sub.SubjectText;
                pages = bhlProvider.SearchPageForAnnotationSubject(annotationSubjectCategoryID, subjectId);
            }
            else
            {
                AnnotationConcept concept = bhlProvider.AnnotationConceptSelectByCode(conceptCode);
                if (concept != null) indexTerm = concept.ConceptText;
                pages = bhlProvider.SearchPageForAnnotationConcept(conceptCode);
            }

            Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Darwin's Library Concept - '" + indexTerm + "'");
         // TODo   ((Main)Page.Master).SetTweetMessage(String.Format(ConfigurationManager.AppSettings["TweetMessage"], "Darwin's Library Concept - '" + indexTerm + "'"));

            litHeader.Text = BuildHeader(pages.Count, indexTerm);

            StringBuilder html = new StringBuilder();
            StringBuilder htmlPages = null;
            int lastTitleID = 0;

            if (pages.Count > 0) html.Append("<ol class=\"data titles\">");
            foreach (SearchBookResult page in pages)
            {
                if (page.TitleID != lastTitleID)
                {
                    // Add list of pages and close previous element
                    if (lastTitleID != 0) html.Append(htmlPages.ToString()).Append("</li>");

                    lastTitleID = page.TitleID;
                    htmlPages = new StringBuilder();

                    // Start new element
                    html.Append("<li>")
                        .Append("<span class='BlackHeading'>").Append(page.ShortTitle).Append("</span><br />")
                        .Append("By: ").Append(page.Authors.Replace("|", " - ")).Append("<br />")
                        .Append("Publication info: ").Append(page.PublicationDetails).Append("<br />");
                    if (!string.IsNullOrEmpty(page.Volume)) html.Append("Volume: ").Append(page.Volume).Append("<br />");
                    html.Append("Pages: ");

                    htmlPages.Append("<a href='page/").Append(page.PageID.ToString()).Append("' title='Page'>")
                        .Append(page.PagePrefix).Append(" ").Append(page.PageNumber)
                        .Append("</a>&nbsp;&nbsp;");
                }
                else
                {
                    // Add new page link
                    htmlPages.Append("<a href='page/").Append(page.PageID.ToString()).Append("' title='Page'>")
                        .Append(page.PagePrefix).Append(" ").Append(page.PageNumber)
                        .Append("</a>&nbsp;&nbsp;");
                }
            }
            // Add final closing elements
            if (pages.Count > 0) html.Append(htmlPages.ToString()).Append("</li></ol>");

            return html.ToString();
        }

        private string BuildHeader(int numPages, string indexTerm)
        {
            StringBuilder header = new StringBuilder();

            // Format the titles into an HTML fragment
       
            header.Append(numPages.ToString() + " Page");
            if (numPages != 1) header.Append("s");
            header.Append(" in  <a class=\"headinglink\" href=\"/collection/darwinlibrary\" title=\"Darwin's Library Homepage\">Charles Darwin's Library</a>  associated with \"" + indexTerm + "\"");
                      return header.ToString();
        }
    }
}