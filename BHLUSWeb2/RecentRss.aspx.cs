using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHL.Web2
{
    public partial class RecentRss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int top = 100;
            String paramTop = (string)RouteData.Values["top"];
            if (paramTop != null)
                Int32.TryParse(paramTop, out top);
            top = (top < 1 || top > 2500) ? 100 : top;

            String institutionCode = (RouteData.Values["inst"] as String) ?? String.Empty;
            String languageCode = (RouteData.Values["lang"] as String) ?? String.Empty;

            Response.ContentType = "text/xml";
            WriteLine("<?xml version='1.0' encoding='UTF-8'?>");
            WriteLine("<rss version=\"2.0\" xmlns:bhl=\"http://www.biodiversitylibrary.org/xsd/bhlrss.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            WriteLine("<channel>");
            WriteLine("<title>BHL Recent Updates</title>");
            WriteLine("<link>https://www.biodiversitylibrary.org/</link>");
            WriteLine("<description>Recently published digital volumes from the Biodiversity Heritage Library.</description>");
            WriteLine("<pubDate>" + DateTime.Now.ToUniversalTime().ToString() + "</pubDate>");
            WriteLine("<lastBuildDate>" + DateTime.Now.ToUniversalTime().ToString() + "</lastBuildDate>");
            WriteLine("<generator>https://www.biodiversitylibrary.org/</generator>");

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

                WriteLine(itemElement);
                WriteLine("<title>" + Server.HtmlEncode(book.FullTitle + " " + book.PartNumber + " " + book.PartName + " " + book.Volume) + " (added: " + DateTime.Parse(book.CreationDate.ToString()).ToString("MM/dd/yyyy") + ")</title>");
                WriteLine("<link>https://www.biodiversitylibrary.org/item/" + book.ItemID.ToString() + "</link>");
                WriteLine("<description>" + Server.HtmlEncode(description) + "</description>");
                WriteLine("<pubDate>" + book.CreationDate.ToString() + "</pubDate>");
                WriteLine("<guid>https://www.biodiversitylibrary.org/item/" + book.ItemID.ToString() + "</guid>");
                WriteLine("</item>");
            }

            WriteLine("</channel>");
            WriteLine("</rss>");
            Response.End();
        }

        private void WriteLine(string text)
        {
            Response.Write(text + "\n");
        }
    }
}