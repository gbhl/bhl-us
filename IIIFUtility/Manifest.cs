using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace BHL.IIIF
{
    public class Manifest
    {
        private string _rootUrl = "http://localhost";

        public Manifest()
        {

        }

        public Manifest (string url)
        {
            _rootUrl = url;
        }

        public string GetManifest(int itemId)
        {
            BHLProvider provider = new BHLProvider();
            Item item = provider.ItemSelectAuto(itemId);
            item.Institutions = provider.InstitutionSelectByItemID(itemId);
            Title title = provider.TitleSelectExtended(item.PrimaryTitleID);
            // Used to determine where to send people for more bibliographic information
            CustomGenericList<Title> titles = provider.TitleSelectByItem(itemId);

            CustomGenericList<Page> pages = provider.PageMetadataSelectByItemID(itemId);
            CustomGenericList<Segment> segments = provider.SegmentSelectByItemID(itemId);
            ScanData scanData = GetScanData(itemId, item.BarCode);

            string manifest = 
                "{" +
                  "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                  "\"@id\": \"http://iiif.archivelab.org/iiif/" + item.BarCode + "/manifest.json\"," +
                  "\"@type\": \"sc:Manifest\"," +
                  "\"attribution\": \"\"," +
                  "\"description\": \"\"," +
                  "\"logo\": \"\"," +
                  "\"label\": \"" + title.FullTitle + "\"," +
                  "\"viewingHint\": \"paged\"," +
                  "\"thumbnail\": {" +
                    "\"@id\": \"" + _rootUrl + "/pagethumb/" + item.ThumbnailPageID + "\"" +
                  "}," +
                  GetMetadata(title, item) +
                  GetSeeAlso(itemId) +
                  GetSequences(item.BarCode, pages, scanData) +
                  GetStructures(item.BarCode, segments, pages, scanData) +
                  GetRelated(titles.Count, item) +
                "}";

            return manifest;
        }

        private ScanData GetScanData(int itemId, string barCode)
        {
            Item item = new BHLProvider().ItemSelectFilenames(itemId);

            WebClient wc = new WebClient();
            XmlDocument xml = new XmlDocument();
            xml.Load(wc.OpenRead(string.Format("https://www.archive.org/download/{0}/{1}", barCode, item.ScandataFilename)));

            String nsPrefix = String.Empty;
            XmlNamespaceManager nsmgr = null;
            XmlNodeList pages = xml.SelectNodes("book/pageData/page");

            // If we didn't find any pages in the file, try again... after first
            // adding a namespace to the XML document
            if (pages.Count == 0)
            {
                nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("ns", "http://archive.org/scribe/xml");
                nsPrefix = "ns:";
                pages = xml.SelectNodes(nsPrefix + "book/" + nsPrefix + "pageData/" + nsPrefix + "page", nsmgr);
            }

            ScanData scanData = new ScanData();
            int sequence = 1;
            int displaySequence = 1;
            foreach (XmlNode page in pages)
            {
                if (sequence == 1) if (page.Attributes["leafNum"] != null) scanData.StartLeafNumber = Convert.ToInt32(page.Attributes["leafNum"].Value);

                PageScanData pageScanData = new PageScanData();
                pageScanData.Sequence = sequence;
                XmlNode addToAccessFormatsNode = page.SelectSingleNode(nsPrefix + "addToAccessFormats", nsmgr);
                if (addToAccessFormatsNode != null) pageScanData.Display = Convert.ToBoolean(addToAccessFormatsNode.InnerText);
                XmlNode heightNode = page.SelectSingleNode(nsPrefix + "origHeight", nsmgr);
                if (heightNode != null) pageScanData.Height = Convert.ToInt32(heightNode.InnerText);
                XmlNode widthNode = page.SelectSingleNode(nsPrefix + "origWidth", nsmgr);
                if (widthNode != null) pageScanData.Width = Convert.ToInt32(widthNode.InnerText);

                if (pageScanData.Display)
                {
                    scanData.ShowPageCount++;
                    pageScanData.DisplaySequence = displaySequence;
                    displaySequence++;
                }
                else
                {
                    scanData.HidePageCount++;
                }
                scanData.Pages.Add(pageScanData);
                sequence++;
            }

            return scanData;
        }

        private string GetMetadata(Title title, Item item)
        {
            string metadata = string.Empty;

            if (!string.IsNullOrWhiteSpace(item.Volume) ||
                title.TitleAuthors.Count > 0 ||
                !string.IsNullOrWhiteSpace(title.PublicationDetails) ||
                !string.IsNullOrWhiteSpace(item.Year) ||
                item.Institutions.Count > 0 ||
                !string.IsNullOrWhiteSpace(item.Sponsor) ||
                !string.IsNullOrWhiteSpace(item.LicenseUrl) ||
                !string.IsNullOrWhiteSpace(item.Rights) ||
                !string.IsNullOrWhiteSpace(item.CopyrightStatus))
            {

                if (!string.IsNullOrWhiteSpace(item.Volume))
                {
                    metadata += GetMetadataSingleValue("volume", item.Volume);
                }

                List<string> authors = new List<string>();
                foreach(TitleAuthor titleAuthor in title.TitleAuthors)
                {
                    titleAuthor.Author.FullName = titleAuthor.FullName;
                    titleAuthor.Author.FullerForm = titleAuthor.FullerForm;
                    string authorString = "<a target='_top' href='" + _rootUrl + "/creator/" + titleAuthor.AuthorID.ToString() + "'>" + titleAuthor.Author.NameExtended + "</a>";
                    authors.Add(authorString);
                }

                if (authors.Count > 0)
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataMultiValue("creator", authors);
                }

                if (!string.IsNullOrWhiteSpace(title.PublicationDetails))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("publisher", title.PublicationDetails);
                }

                if (!string.IsNullOrWhiteSpace(item.Year))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("date", item.Year);
                }

                foreach(Institution institution in item.Institutions)
                {
                    if (institution.InstitutionRoleName.ToLower() == "holding institution")
                    {
                        if (metadata.Length > 0) metadata += ",";
                        string institutionName = institution.InstitutionName;
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl)) institutionName = "<a target='_top' href='" + institution.InstitutionUrl + "'>" + institutionName + "</a>";
                        metadata += GetMetadataSingleValue("holding institution", institutionName);
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.Sponsor))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("sponsor", item.Sponsor);
                }

                if (!string.IsNullOrWhiteSpace(item.LicenseUrl))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("license type", "<a target='_top' href='" + item.LicenseUrl + "'>" + item.LicenseUrl + "</a>");
                }

                if (!string.IsNullOrWhiteSpace(item.Rights))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("rights", "<a target='_top' href='" + item.Rights+ "'>" + item.Rights+ "</a>");
                }

                if (!string.IsNullOrWhiteSpace(item.CopyrightStatus))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("copyright status", item.CopyrightStatus);
                }

                foreach (Institution institution in item.Institutions)
                {
                    if (institution.InstitutionRoleName.ToLower() == "rights holder")
                    {
                        if (metadata.Length > 0) metadata += ",";
                        string institutionName = institution.InstitutionName;
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl)) institutionName = "<a target='_top' href='" + institution.InstitutionUrl + "'>" + institutionName + "</a>";
                        metadata += GetMetadataSingleValue("rights holder", institutionName);
                        break;
                    }
                }

                metadata = "\"metadata\": [" + metadata +  "],";
            }

            return metadata;
        }

        private string GetMetadataSingleValue(string label, string value)
        {
            string metadata =
                "{" +
                  "\"label\": \"" + label + "\"," +
                  "\"value\": \"" + value + "\"" +
                "}";

            return metadata;
        }

        private string GetMetadataMultiValue(string label, List<string> values)
        {
            string metadata =
                  "{" +
                    "\"label\": \"creator\"," +
                    "\"value\": [\"" +
                        string.Join("\",\"", values) +
                      "\"]" +
                  "}";

            return metadata;
        }

        private string GetRelated(int numTitles, Item item)
        {
            string bibUrl = _rootUrl + "/bibliography/" + item.PrimaryTitleID.ToString();
            if (numTitles > 1) bibUrl = _rootUrl + "/biblioselect/" + item.ItemID.ToString();

            string related =
                "\"related\": [" +
                  "{" + 
                    "\"@id\": \"" + bibUrl + "\"," +
                       "\"label\": \"Additional Bibliographic Information\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"https://www.archive.org/details/" + item.BarCode + "\"," +
                      "\"label\": \"View at Internet Archive\"" +
                  "}" +
            "]";

            return related;
        }

        private string GetSeeAlso(int itemId)
        {
            string seeAlso = 
                "\"seeAlso\": [" +
                  "{" +
                      "\"@id\": \"" + _rootUrl + "/itempdf/" + itemId.ToString() + "\"," +
                      "\"label\": \"Download PDF\"," +
                      "\"format\": \"application/pdf\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"" + _rootUrl + "/itemimages/" + itemId.ToString() + "\"," +
                      "\"label\": \"Download JPEG 2000\"," +
                      "\"format\": \"application/zip\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"" + _rootUrl + "/itemtext/" + itemId.ToString() + "\"," +
                      "\"label\": \"Download Text\"," +
                      "\"format\": \"text/plain\"" +
                  "}" +
                "],";

            return seeAlso;
        }

        private string GetSequences(string barCode, CustomGenericList<Page> pages, ScanData scanData)
        {
            string sequences = string.Empty;

            if (pages.Count > 0)
            {
                sequences =
                    "\"sequences\": [" +
                      "{" +
                        "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                        "\"@id\": \"http://iiif.archivelab.org/iiif/" + barCode + "/canvas/default\"," +
                        "\"@type\": \"sc:Sequence\"," +
                        GetCanvases(barCode, pages, scanData) +
                        "\"label\": \"default\"" +
                      "}" +
                    "],";
            }

            return sequences;
        }


        private string GetCanvases(string barCode, CustomGenericList<Page> pages, ScanData scanData)
        {
            string canvases = "\"canvases\": [";

            int leafNum = scanData.StartLeafNumber;
            int scanPageCount = 0;
            int pageCount = 0;
            foreach (PageScanData pageScanData in scanData.Pages)
            {
                if (pageScanData.Display)
                {
                    if (pageCount > 0) canvases += ",";
                    canvases += GetCanvas(barCode, pages[pageCount], leafNum, scanData.Pages[scanPageCount].Height, scanData.Pages[scanPageCount].Width);
                    pageCount++;
                }

                scanPageCount++;
                leafNum++;
            }

            canvases += "],";

            return canvases;
        }

        private string GetCanvas(string barCode, Page page, int count, int height = 800, int width = 600)
        {
            string iiifRootAddress = "http://iiif.archivelab.org/iiif/" + barCode + "$" + count.ToString();

            string canvas =
                "{" +
                  "\"@id\": \"" + iiifRootAddress + "/canvas\"," +
                  "\"@type\": \"sc:Canvas\"," +
                  "\"height\": " + height.ToString() + "," +
                  "\"images\": [" +
                    "{" +
                      "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                      "\"@id\": \"" + iiifRootAddress + "/annotation\"," +
                      "\"@type\": \"oa:Annotation\"," +
                      "\"motivation\": \"sc:painting\"," +
                      "\"on\": \"" + iiifRootAddress + "/annotation\"," +
                      "\"resource\": {" +
                        "\"@id\": \"" + iiifRootAddress + "/full/full/0/default.jpg\"," +
                        "\"@type\": \"dctypes:Image\"," +
                        "\"format\": \"image/jpeg\"," +
                        "\"height\": " + height.ToString() + "," +
                        "\"service\": {" +
                          "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                          "\"@id\": \"" + iiifRootAddress + "\"," +
                          "\"profile\": \"https://iiif.io/api/image/2/profiles/level2.json\"" +
                        "}," +
                        "\"width\": " + width.ToString() +
                      "}" +
                    "}" +
                  "]," +
                  "\"label\": \"" + page.WebDisplay + "\"," +
                  "\"width\": " + width.ToString() +
                "}";

            return canvas;
        }

        private string GetStructures(string barCode, CustomGenericList<Segment> segments, CustomGenericList<Page> pages, ScanData scanData)
        {
            string structures = string.Empty;

            if (segments.Count > 0)
            {
                // Build the list of canvases for each segment
                Dictionary<int, List<String>> canvases = new Dictionary<int, List<string>>();
                foreach (Page page in pages)
                {
                    if (page.SegmentID != null)
                    {
                        if (!canvases.ContainsKey((int)page.SegmentID)) canvases.Add((int)page.SegmentID, new List<string>());

                        PageScanData pageScanData = scanData.GetScanDataForDisplaySequence((int)page.SequenceOrder);
                        string canvas = string.Format("http://iiif.archivelab.org/iiif/{0}${1}/canvas", barCode, (pageScanData == null ? 0 : pageScanData.Sequence - 1));
                        canvases[(int)page.SegmentID].Add(canvas);
                    }
                }

                // Build the manifest "structures" section
                structures = "\"structures\": [";

                int segmentCount = 0;
                foreach (Segment segment in segments)
                {
                    if (segmentCount > 0) structures += ",";
                    structures += GetStructure(segment, canvases[segment.SegmentID]);
                    segmentCount++;

                    segmentCount++;
                }

                structures += "],";
            }

            return structures;
        }

        private string GetStructure(Segment segment, List<string> canvasList)
        {
            string structure = 
            "{" +
              "\"@id\": \"" + _rootUrl + "/part/" + segment.SegmentID.ToString() + "\"," +
              "\"@type\": \"sc:Range\"," +
              "\"label\": \"" + segment.Title + "\"," +
              "\"canvases\": [\"" +
                string.Join("\",\"", canvasList) +
              "\"]" +
            "}";

            return structure;
        }
    }


    class ScanData
    {
        public ScanData()
        {
            ShowPageCount = 0;
            HidePageCount = 0;
            StartLeafNumber = 0;
            Pages = new List<PageScanData>();
        }

        public int ShowPageCount { get; set; }
        public int HidePageCount { get; set; }
        public int StartLeafNumber { get; set; }
        public List<PageScanData> Pages { get; set; }

        public PageScanData GetScanDataForDisplaySequence(int displaySequence)
        {
            PageScanData scanData = null;

            foreach(PageScanData pageScanData in Pages)
            {
                if (pageScanData.DisplaySequence == displaySequence) { scanData = pageScanData; break; }
            }

            return scanData;
        }
    }

    class PageScanData
    {
        public PageScanData()
        {
            DisplaySequence = null;
            Display = false;
            Height = 800;
            Width = 600;
        }

        public int Sequence { get; set; }
        public int? DisplaySequence { get; set; }
        public bool Display { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
