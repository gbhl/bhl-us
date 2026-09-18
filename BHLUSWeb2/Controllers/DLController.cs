using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class DLController : Controller
    {
        [EnableThrottling]
        // GET: Concepts
        public ActionResult Concepts()
        {
            ViewBag.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Darwin's Library Concepts");

            // Get the concepts for the Darwin's Library annotations
            List<AnnotationConcept> concepts = new BHLProvider().AnnotationConceptSelectAll(1);
            //split into three columns
            List<AnnotationConcept> concepts1 = new List<AnnotationConcept>();
            List<AnnotationConcept> concepts2 = new List<AnnotationConcept>();
            List<AnnotationConcept> concepts3 = new List<AnnotationConcept>();
            float totalConceptCount = concepts.Count;
            int columncount = Convert.ToInt32(Math.Ceiling(totalConceptCount / 3));
            if (columncount >= 1)
            {
                for (int i = 0; i <= columncount - 1; i++)
                {
                    concepts1.Add(concepts[i]);
                }
            }
            if (columncount >= 2)
            {
                for (int j = columncount; j <= (columncount * 2) - 1; j++)
                {
                    concepts2.Add(concepts[j]);
                }
            }
            if (columncount >= 3)
            {
                for (int k = columncount * 2; k <= totalConceptCount - 1; k++)
                {
                    concepts3.Add(concepts[k]);
                }
            }
            ViewBag.concepts1 = concepts1;
            ViewBag.concepts2 = concepts2;
            ViewBag.concepts3 = concepts3;

            return View();
        }

        [EnableThrottling]
        // GET: IndexBrowse
        public ActionResult IndexBrowse()
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
                ViewBag.Pages = BuildPageList(conceptCode, annotationSubjectCategoryID, subjectId);
            }

            return View();
        }

        private string BuildPageList(string conceptCode, int annotationSubjectCategoryID, int subjectId)
        {
            BHLProvider bhlProvider = new BHLProvider();
            List<SearchBookResult> pages = null;
            string indexTerm = string.Empty;

            if (conceptCode == string.Empty)
            {
                AnnotationSubject sub = bhlProvider.AnnotationSubjectSelect(subjectId);
                AnnotationSubjectCategory subCat = bhlProvider.AnnotationSubjectCategorySelect(annotationSubjectCategoryID);
                if (sub != null && subCat != null) indexTerm = subCat.SubjectCategoryName + " - " + sub.SubjectText;
                pages = bhlProvider.SearchPageForAnnotationSubject(annotationSubjectCategoryID, subjectId);
            }
            else
            {
                AnnotationConcept concept = bhlProvider.AnnotationConceptSelectByCode(conceptCode);
                if (concept != null) indexTerm = concept.ConceptText;
                pages = bhlProvider.SearchPageForAnnotationConcept(conceptCode);
            }

            ViewBag.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Darwin's Library Concept - '" + indexTerm + "'");
            ViewBag.Header = BuildHeader(pages.Count, indexTerm);

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

                    htmlPages.Append("<a href='/page/").Append(page.PageID.ToString()).Append("' title='Page'>")
                        .Append(page.PagePrefix).Append(" ").Append(page.PageNumber)
                        .Append("</a>&nbsp;&nbsp;");
                }
                else
                {
                    // Add new page link
                    htmlPages.Append("<a href='/page/").Append(page.PageID.ToString()).Append("' title='Page'>")
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