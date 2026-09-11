using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MvcThrottle;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class RecentController : Controller
    {
        [EnableThrottling]
        // GET: Recent
        public ActionResult Index()
        {
            BHLProvider bhlProvider = new BHLProvider();
            string institutionCode = string.Empty;
            string languageCode = string.Empty;
            int top = 100;
            string paramTop = (string)RouteData.Values["top"];
            if (paramTop != null) Int32.TryParse(paramTop, out top);
            top = (top < 1 || top > 1000) ? 100 : top;

            string institutionName = ((string)RouteData.Values["inst"] ?? string.Empty).ToString();
            if (institutionName != String.Empty)
            {
                institutionCode = institutionName.ToUpper();
                Institution institution = bhlProvider.InstitutionSelectAuto(institutionCode);
                if (institution != null) institutionName = institution.InstitutionName.Replace("(archive.org)", "").Trim();
            }

            string languageName = ((string)RouteData.Values["lang"] ?? string.Empty).ToString();
            if (this.Request.QueryString["lang"] != null)
            {
                languageCode = languageName.ToUpper();
                DataObjects.Language language = bhlProvider.LanguageSelectAuto(languageCode);
                if (language != null) languageName = language.LanguageName;
            }

            ViewBag.BookList = bhlProvider.BookSelectRecent(top, languageCode, institutionCode);

            string recentLink = string.Format("http://{0}/RecentRss/{1}", Request.ServerVariables["HTTP_HOST"], top.ToString());
            if ((languageCode + institutionCode) != string.Empty)
            {
                if (languageCode == string.Empty) languageCode = "ALL";
                if (institutionCode == string.Empty) institutionCode = "ALL";
                recentLink += "/" + languageCode + "/" + institutionCode;
            }

            ViewBag.NumberDisplayedText = " (Last " + top.ToString() + ")";
            ViewBag.LanguageText = (!string.IsNullOrEmpty(languageName)) ? " Published In: " + languageName : string.Empty;
            ViewBag.ContributorText = (!string.IsNullOrEmpty(institutionName)) ? " For: " + institutionName : string.Empty;
            ViewBag.RecentLink = "/" + languageCode + "/" + institutionCode;
            ViewBag.RssRecentLink = recentLink;
            ViewBag.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], "Recent Additions");

            return View();
        }

        [EnableThrottling]
        // GET: Recent/Rss
        public ActionResult Rss()
        {
            int top = 100;
            String paramTop = (string)RouteData.Values["top"];
            if (paramTop != null)
                Int32.TryParse(paramTop, out top);
            top = (top < 1 || top > 2500) ? 100 : top;

            String institutionCode = (RouteData.Values["inst"] as String) ?? String.Empty;
            String languageCode = (RouteData.Values["lang"] as String) ?? String.Empty;

            StringBuilder response = new StringBuilder();
            string contentType = "text/xml";
            response.AppendLine("<?xml version='1.0' encoding='UTF-8'?>");
            response.AppendLine("<rss version=\"2.0\" xmlns:bhl=\"http://www.biodiversitylibrary.org/xsd/bhlrss.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            response.AppendLine("<channel>");
            response.AppendLine("<title>BHL Recent Updates</title>");
            response.AppendLine("<link>https://www.biodiversitylibrary.org/</link>");
            response.AppendLine("<description>Recently published digital volumes from the Biodiversity Heritage Library.</description>");
            response.AppendLine("<pubDate>" + DateTime.Now.ToUniversalTime().ToString() + "</pubDate>");
            response.AppendLine("<lastBuildDate>" + DateTime.Now.ToUniversalTime().ToString() + "</lastBuildDate>");
            response.AppendLine("<generator>https://www.biodiversitylibrary.org/</generator>");

            List<DataObjects.Book> books = new BHLProvider().BookSelectRecent(top, languageCode, institutionCode);
            foreach (DataObjects.Book book in books)
            {
                String description = String.Empty;
                if ((book.AuthorStrings.Length > 1) || (book.AuthorStrings.Length == 1 && !String.IsNullOrEmpty(book.AuthorStrings[0]))) description += "<b>By:</b><br/>";
                foreach (String creator in book.AuthorStrings)
                {
                    if (!String.IsNullOrEmpty(creator)) description += creator + "<br/>";
                }
                if (!String.IsNullOrEmpty(book.PublicationDetails)) description += "<b>Publication Info:</b><br/>" + book.PublicationDetails + "<br/>";
                if ((book.TagStrings.Length > 1) || (book.TagStrings.Length == 1 && !String.IsNullOrEmpty(book.TagStrings[0]))) description += "<b>Subjects:</b><br/>" + String.Join(", ", book.TagStrings) + "<br/>";
                if ((book.AssociationStrings.Length > 1) || (book.AssociationStrings.Length == 1 && !String.IsNullOrEmpty(book.AssociationStrings[0]))) description += "<b>Related Titles:</b><br/>";
                foreach (String association in book.AssociationStrings)
                {
                    if (!String.IsNullOrEmpty(association)) description += association + "<br/>";
                }
                if (book.InstitutionStrings.Count() > 1) description += "<b>Contributing Library:</b><br/>Multiple institutions<br/>";
                if (book.InstitutionStrings.Count() == 1) description += "<b>Contributing Library:</b><br/>" + book.InstitutionStrings[0] + "<br/>";
                if (!String.IsNullOrEmpty(book.Sponsor)) description += "<b>Sponsor:</b><br/>" + book.Sponsor + "<br/>";
                if (!String.IsNullOrEmpty(book.LicenseUrl)) description += "<b>License Type:</b><br/>" + book.LicenseUrl + "<br/>";
                if (!String.IsNullOrEmpty(book.Rights)) description += "<b>Rights:</b><br/>" + book.Rights + "<br/>";
                if (!String.IsNullOrEmpty(book.DueDiligence)) description += "<b>Due Diligence:</b><br/>" + book.DueDiligence + "<br/>";
                if (!String.IsNullOrEmpty(book.CopyrightStatus)) description += "<b>Copyright Status:</b><br/>" + book.CopyrightStatus + "<br/>";

                string itemElement = string.IsNullOrWhiteSpace(book.ExternalUrl) ? "<item>" : "<item bhl:externalcontent=\"true\">";

                response.AppendLine(itemElement);
                response.AppendLine("<title>" + Server.HtmlEncode(book.FullTitleExtended + " " + book.Volume) + " (added: " + DateTime.Parse(book.CreationDate.ToString()).ToString("MM/dd/yyyy") + ")</title>");
                response.AppendLine("<link>https://www.biodiversitylibrary.org/item/" + book.ItemID.ToString() + "</link>");
                response.AppendLine("<description>" + Server.HtmlEncode(description) + "</description>");
                response.AppendLine("<pubDate>" + book.CreationDate.ToString() + "</pubDate>");
                response.AppendLine("<guid>https://www.biodiversitylibrary.org/item/" + book.ItemID.ToString() + "</guid>");
                response.AppendLine("</item>");
            }

            response.AppendLine("</channel>");
            response.AppendLine("</rss>");
            return Content(response.ToString(), contentType);
        }
    }
}