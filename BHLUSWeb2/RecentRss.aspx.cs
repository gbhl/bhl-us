using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomDataAccess;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

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
            WriteLine("<link>http://www.biodiversitylibrary.org/</link>");
            WriteLine("<description>Recently published digital volumes from the Biodiversity Heritage Library.</description>");
            WriteLine("<pubDate>" + DateTime.Now.ToUniversalTime().ToString() + "</pubDate>");
            WriteLine("<lastBuildDate>" + DateTime.Now.ToUniversalTime().ToString() + "</lastBuildDate>");
            WriteLine("<generator>http://www.biodiversitylibrary.org/</generator>");

            CustomGenericList<Item> list = new BHLProvider().ItemSelectRecent(top, languageCode, institutionCode);
            foreach (Item item in list)
            {
                String description = String.Empty;
                if ((item.AuthorStrings.Length > 1) || (item.AuthorStrings.Length == 1 && !String.IsNullOrEmpty(item.AuthorStrings[0]))) description += "<b>By:</b><br/>";
                foreach (String creator in item.AuthorStrings)
                {
                    if (!String.IsNullOrEmpty(creator)) description += creator + "<br/>";
                }
                if (!String.IsNullOrEmpty(item.PublicationDetails)) description += "<b>Publication Info:</b><br/>" + item.PublicationDetails + "<br/>";
                if ((item.TagStrings.Length > 1) || (item.TagStrings.Length == 1 && !String.IsNullOrEmpty(item.TagStrings[0]))) description += "<b>Subjects:</b><br/>" + String.Join(", ", item.TagStrings) + "<br/>";
                if ((item.AssociationStrings.Length > 1) || (item.AssociationStrings.Length == 1 && !String.IsNullOrEmpty(item.AssociationStrings[0]))) description += "<b>Related Titles:</b><br/>";
                foreach (String association in item.AssociationStrings)
                {
                    if (!String.IsNullOrEmpty(association)) description += association + "<br/>";
                }
                if (item.ContributorStrings.Count() > 1) description += "<b>Contributing Library:</b><br/>Multiple institutions<br/>";
                if (item.ContributorStrings.Count() == 1) description += "<b>Contributing Library:</b><br/>" + item.ContributorStrings[0] + "<br/>";
                if (!String.IsNullOrEmpty(item.Sponsor)) description += "<b>Sponsor:</b><br/>" + item.Sponsor + "<br/>";
                if (!String.IsNullOrEmpty(item.LicenseUrl)) description += "<b>License Type:</b><br/>" + item.LicenseUrl + "<br/>";
                if (!String.IsNullOrEmpty(item.Rights)) description += "<b>Rights:</b><br/>" + item.Rights + "<br/>";
                if (!String.IsNullOrEmpty(item.DueDiligence)) description += "<b>Due Diligence:</b><br/>" + item.DueDiligence + "<br/>";
                if (!String.IsNullOrEmpty(item.CopyrightStatus)) description += "<b>Copyright Status:</b><br/>" + item.CopyrightStatus + "<br/>";
                if (!String.IsNullOrEmpty(item.CopyrightRegion)) description += "<b>Copyright Region:</b><br/>" + item.CopyrightRegion + "<br/>";
                if (!String.IsNullOrEmpty(item.CopyrightComment)) description += "<b>Copyright Comments:</b><br/>" + item.CopyrightComment + "<br/>";
                if (!String.IsNullOrEmpty(item.CopyrightEvidence)) description += "<b>Copyright Evidence:</b><br/>" + item.CopyrightEvidence + "<br/>";

                string itemElement = string.IsNullOrWhiteSpace(item.ExternalUrl) ? "<item>" : "<item bhl:externalcontent=\"true\">";

                WriteLine(itemElement);
                WriteLine("<title>" + Server.HtmlEncode(item.FullTitle + " " + item.PartNumber + " " + item.PartName + " " + item.Volume) + " (added: " + DateTime.Parse(item.CreationDate.ToString()).ToString("MM/dd/yyyy") + ")</title>");
                WriteLine("<link>http://www.biodiversitylibrary.org/item/" + item.ItemID.ToString() + "</link>");
                WriteLine("<description>" + Server.HtmlEncode(description) + "</description>");
                WriteLine("<pubDate>" + item.CreationDate.ToString() + "</pubDate>");
                WriteLine("<guid>http://www.biodiversitylibrary.org/item/" + item.ItemID.ToString() + "</guid>");
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