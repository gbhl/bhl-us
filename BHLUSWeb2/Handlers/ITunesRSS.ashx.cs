using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;

namespace MOBOT.BHL.Web2.Handlers
{
    /// <summary>
    /// Summary description for ITunesRSS
    /// </summary>
    public class ITunesRSS : IHttpHandler
    {
        private string _itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        private string _itunesUNamespace = "http://www.itunesu.com/feed";

        // iTunes U RSS feed specs: http://deimos.apple.com/rsrc/doc/UsingiTunesUPublicSiteManager/AboutTheFeedEditor/chapter_10_section_10.html#//apple_ref/doc/uid/iTUPSM-CH10-SW7
        public void ProcessRequest(HttpContext context)
        {
            string domainRoot = ConfigurationManager.AppSettings["BaseUrl"];

            StringBuilder sb = new StringBuilder();
            int collectionID;
            bool gotCollection = false;
            bool isValid = true;
            List<Collection> collections =new BHLProvider().CollectionSelectByUrl((string)context.Request.RequestContext.RouteData.Values["id"]);

            if (collections.Count > 0)
            {
                int.TryParse(collections[0].CollectionID.ToString(), out collectionID);
            }
            else
            {
                if (!int.TryParse((string)context.Request.RequestContext.RouteData.Values["id"], out collectionID))
                {
                    isValid = false;
                }
            }

            if (isValid)
            {
                Collection collection = new BHLProvider().CollectionSelectAuto(collectionID);
                if (!(collection == null)) gotCollection = (collection.Active == 1 && (collection.CollectionTarget == "iTunes" || collection.CollectionTarget == "All"));
                if (gotCollection)
                {
                    gotCollection = true;

                    TextSyndicationContent feedTitle = new TextSyndicationContent(collection.CollectionName);
                    TextSyndicationContent feedDescription = new TextSyndicationContent(collection.CollectionDescription);

                    SyndicationFeed feed = new SyndicationFeed(
                        feedTitle.Text, 
                        feedDescription.Text, 
                        new Uri(domainRoot + "browse/collection/" + collectionID.ToString()));
                    feed.Language = "en-us";
                    feed.Generator = domainRoot;
                    if (!string.IsNullOrEmpty(collection.ITunesImageURL)) feed.ImageUrl = new Uri(collection.ITunesImageURL);
                    feed.Categories.Add(new SyndicationCategory("Literature"));
                    feed.Categories.Add(new SyndicationCategory("Natural Sciences"));
                    feed.Authors.Add(new SyndicationPerson("biodiversitylibrary@gmail.com", "Biodiversity Heritage Library", domainRoot));

                    // Add a custom attribute for the iTunes namespace
                    XmlQualifiedName key = new XmlQualifiedName("itunes", "http://www.w3.org/2000/xmlns/");
                    feed.AttributeExtensions.Add(key, _itunesNamespace);

                    // Add a custom attribute for the iTunesU namespace
                    key = new XmlQualifiedName("itunesu", "http://www.w3.org/2000/xmlns/");
                    feed.AttributeExtensions.Add(key, _itunesUNamespace);

                    // Add iTunes custom elements
                    XmlDocument doc = new XmlDocument();
                    XmlElement feedElement = doc.CreateElement("itunes", "author", _itunesNamespace);
                    feedElement.InnerText = domainRoot;
                    feed.ElementExtensions.Add(feedElement);

                    feedElement = doc.CreateElement("itunes", "summary", _itunesNamespace);
                    feedElement.InnerText = feedDescription.Text;
                    feed.ElementExtensions.Add(feedElement);

                    feedElement = doc.CreateElement("itunes", "owner", _itunesNamespace);
                    XmlElement feedSubElement = doc.CreateElement("itunes", "name", _itunesNamespace);
                    feedSubElement.InnerText = "Biodiversity Heritage Library";
                    feedElement.AppendChild(feedSubElement);
                    feedSubElement = doc.CreateElement("itunes", "email", _itunesNamespace);
                    feedSubElement.InnerText = "biodiversitylibrary@gmail.com";
                    feedElement.AppendChild(feedSubElement);
                    feed.ElementExtensions.Add(feedElement);

                    if (!string.IsNullOrEmpty(collection.ITunesImageURL))
                    {
                        feedElement = doc.CreateElement("itunes", "image", _itunesNamespace);
                        XmlAttribute xmlAttribute = doc.CreateAttribute("href");
                        xmlAttribute.Value = collection.ITunesImageURL;
                        feedElement.Attributes.Append(xmlAttribute);
                        feed.ElementExtensions.Add(feedElement);
                    }

                    feedElement = doc.CreateElement("itunes", "category", _itunesNamespace);
                    XmlAttribute catAttribute = doc.CreateAttribute("text");
                    catAttribute.Value = "Literature";
                    feedElement.Attributes.Append(catAttribute);
                    feed.ElementExtensions.Add(feedElement);

                    feedElement = doc.CreateElement("itunes", "category", _itunesNamespace);
                    catAttribute = doc.CreateAttribute("text");
                    catAttribute.Value = "Natural Sciences";
                    feedElement.Attributes.Append(catAttribute);
                    feed.ElementExtensions.Add(feedElement);

                    // Add the items in the collection
                    List<Item> items = new BHLProvider().ItemSelectByCollection(collectionID);
                    List<SyndicationItem> syndItems = new List<SyndicationItem>();
                    foreach (Item item in items)
                    {
                        if (!string.IsNullOrWhiteSpace(item.PdfFilename))
                        {
                            string content = string.Empty;
                            if (!string.IsNullOrEmpty(item.Volume)) content += "Volume: " + item.Volume + "<br>";
                            if (item.AuthorStrings.Length > 0) content += "By: " + String.Join(" - ", item.AuthorStrings) + "<br>";
                            if (!string.IsNullOrEmpty(item.PublicationDetails)) content += "Publication Details: " + item.PublicationDetails + "<br>";
                            if (item.InstitutionStrings.Count() > 1) content += "Contributed By: Multiple institutions";
                            if (item.InstitutionStrings.Count() == 1) content += "Contributed By: " + item.InstitutionStrings[0];

                            // Set up item with standard RSS information
                            SyndicationItem syndItem = new SyndicationItem(
                                new TextSyndicationContent((item.ShortTitle + (string.IsNullOrEmpty(item.Volume) ? string.Empty : ", " + item.Volume)).Trim()).Text,
                                new TextSyndicationContent(content).Text,
                                new Uri(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.PdfFilename)));
                            //new Uri(domainRoot + "itempdf/" + item.ItemID.ToString()));
                            syndItem.PublishDate = DateTime.SpecifyKind((item.CreationDate ?? DateTime.Now), DateTimeKind.Local);
                            syndItem.AddPermalink(new Uri(domainRoot + "item/" + item.ItemID.ToString()));

                            // Add iTunes custom elements
                            string iTunesContent = string.Empty;
                            if (!string.IsNullOrEmpty(item.Volume)) iTunesContent += "Volume: " + item.Volume + ".  ";
                            if (!string.IsNullOrEmpty(item.PublicationDetails)) iTunesContent += "Publication Details: " + item.PublicationDetails + ".  ";
                            if (item.InstitutionStrings.Count() > 1) iTunesContent += "Contributed By: Multiple institutions";
                            if (item.InstitutionStrings.Count() == 1) iTunesContent += "Contributed By: " + item.InstitutionStrings[0];

                            XmlElement itemElement = doc.CreateElement("itunes", "subtitle", _itunesNamespace);
                            itemElement.InnerText = iTunesContent.Substring(0, (iTunesContent.Length > 255) ? 255 : iTunesContent.Length);
                            syndItem.ElementExtensions.Add(itemElement);

                            itemElement = doc.CreateElement("itunes", "summary", _itunesNamespace);
                            itemElement.InnerText = iTunesContent.Substring(0, (iTunesContent.Length > 4000) ? 4000 : iTunesContent.Length);
                            syndItem.ElementExtensions.Add(itemElement);

                            if (string.Join("", item.AuthorStrings).Trim().Length > 0)
                            {
                                itemElement = doc.CreateElement("itunes", "author", _itunesNamespace);
                                itemElement.InnerText = string.Join(" - ", item.AuthorStrings);
                                syndItem.ElementExtensions.Add(itemElement);
                            }

                            if (string.Join("", item.TagStrings).Trim().Length > 0)
                            {
                                itemElement = doc.CreateElement("itunes", "keywords", _itunesNamespace);
                                itemElement.InnerText = string.Join(", ", item.TagStrings);
                                syndItem.ElementExtensions.Add(itemElement);
                            }

                            // Add itunesU category
                            // List of category codes at http://deimos.apple.com/rsrc/doc/UsingiTunesUPublicSiteManager/AboutCategoryPages/chapter_8_section_3.html#//apple_ref/doc/uid/iTUPSM-CH8-SW3
                            itemElement = doc.CreateElement("itunesu", "category", _itunesUNamespace);
                            catAttribute = doc.CreateAttribute("itunesu", "code", _itunesUNamespace);
                            catAttribute.Value = "109103";  // Biology
                            itemElement.Attributes.Append(catAttribute);
                            syndItem.ElementExtensions.Add(itemElement);

                            // Add link to PDF
                            itemElement = doc.CreateElement("enclosure");
                            XmlAttribute itemAttribute = doc.CreateAttribute("type");
                            itemAttribute.Value = "application/pdf";
                            itemElement.Attributes.Append(itemAttribute);
                            itemAttribute = doc.CreateAttribute("url");
                            itemAttribute.Value = string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.PdfFilename);
                            itemElement.Attributes.Append(itemAttribute);
                            syndItem.ElementExtensions.Add(itemElement);

                            syndItems.Add(syndItem);
                        }
                    }
                    feed.Items = syndItems;
                    feed.LastUpdatedTime = DateTime.Now;

                    // Serialize the data into a RSS 2.0 XML string
                    XmlWriter rssWriter = XmlWriter.Create(sb);
                    Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed);
                    rssFormatter.SerializeExtensionsAsAtom = false;
                    rssFormatter.WriteTo(rssWriter);
                    rssWriter.Close();
                }
            }

            if (!gotCollection) sb.Append("Invalid collection");
            
            context.Response.ContentType = "text/xml";
            // Flush the RSS XML string, replacing the utf-16 declaration with utf-8
            context.Response.Write(sb.ToString().Replace(Encoding.Unicode.WebName, Encoding.UTF8.WebName));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        // Subclass the StringWriter class and override the default encoding.  This
        // allows us to produce XML encoded as UTF-8.
        private class StringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }
        }
    }
}