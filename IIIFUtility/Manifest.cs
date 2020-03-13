﻿using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

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

            string thumbnailAttr = string.Empty;
            int? thumbnailPageID = item.ThumbnailPageID;
            if (thumbnailPageID == null)
            {
                Page firstPage = provider.PageSelectFirstPageForItem(Convert.ToInt32(itemId));
                thumbnailPageID = (firstPage == null ? thumbnailPageID : firstPage.PageID);
            }
            if (thumbnailPageID != null) thumbnailAttr = 
                  "\"thumbnail\": {" +
                    "\"@id\": \"" + _rootUrl + "/pagethumb/" + (int)thumbnailPageID + "\"" +
                  "},";

            // Used to determine where to send people for more bibliographic information
            List< Title> titles = provider.TitleSelectByItem(itemId);

            List<Page> pages = provider.PageMetadataSelectByItemID(itemId);
            List<Segment> segments = provider.SegmentSelectByItemID(itemId);
            ScanData scanData = new Helper().GetScanData(itemId, item.BarCode);

            string manifest = 
                "{" +
                  "\"@context\": \"http://iiif.io/api/presentation/2/context.json\"," +
                  "\"@id\": \"" + _rootUrl + "/iiif/" + itemId.ToString() + "/manifest\"," +
                  "\"@type\": \"sc:Manifest\"," +
                  "\"attribution\": \"\"," +
                  "\"description\": \"\"," +
                  "\"logo\": \"\"," +
                  "\"label\": \"" + Helper.CleanManifestData(title.FullTitle) + "\"," +
                  thumbnailAttr +
                  GetMetadata(title, item) +
                  GetSeeAlso(itemId) +
                  GetSequences(itemId, item.BarCode, pages, scanData) +
                  GetStructures(itemId, segments, pages, scanData) +
                  GetRelated(titles.Count, item) +
                "}";

            return manifest;
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
                    string authorString = "<a target='_top' href='" + _rootUrl + "/creator/" + titleAuthor.AuthorID.ToString() + "'>" + Helper.CleanManifestData(titleAuthor.Author.NameExtended) + "</a>";
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
                  "\"label\": \"" + Helper.CleanManifestData(label) + "\"," +
                  "\"value\": \"" + Helper.CleanManifestData(value) + "\"" +
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
                      "\"label\": \"Additional Bibliographic Information\"," +
                      "\"format\": \"text/html\"" + 
                  "}," +
                  "{" +
                      "\"@id\": \"https://www.archive.org/details/" + item.BarCode + "\"," +
                      "\"label\": \"View at Internet Archive\"," +
                      "\"format\": \"text/html\"" +
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
                      "\"label\": \"PDF\"," +
                      "\"format\": \"application/pdf\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"" + _rootUrl + "/itemimages/" + itemId.ToString() + "\"," +
                      "\"label\": \"JPEG 2000 (Images)\"," +
                      "\"format\": \"application/zip\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"" + _rootUrl + "/itemtext/" + itemId.ToString() + "\"," +
                      "\"label\": \"Text\"," +
                      "\"format\": \"text/plain\"" +
                  "}" +
                "],";

            return seeAlso;
        }

        private string GetSequences(int itemId, string barCode, List<Page> pages, ScanData scanData)
        {
            string sequences = string.Empty;

            if (pages.Count > 0)
            {
                sequences =
                    "\"sequences\": [" +
                      "{" +
//                        "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                        "\"@id\": \"" + _rootUrl + "/iiif/" + itemId.ToString() + "/canvas/default\"," +
                        "\"@type\": \"sc:Sequence\"," +
                        "\"viewingDirection\": \"left-to-right\"," +
                        "\"viewingHint\": \"paged\"," +
                        GetCanvases(itemId, barCode, pages, scanData) +
                        "\"label\": \"default\"" +
                      "}" +
                    "],";
            }

            return sequences;
        }


        private string GetCanvases(int itemId, string barCode, List<Page> pages, ScanData scanData)
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
                    canvases += GetCanvas(itemId, barCode, pages[pageCount], leafNum, scanData.Pages[scanPageCount].Height, scanData.Pages[scanPageCount].Width);
                    pageCount++;
                }

                scanPageCount++;
                leafNum++;
            }

            canvases += "],";

            return canvases;
        }

        private string GetCanvas(int itemId, string barCode, Page page, int count, int height = 800, int width = 600)
        {
            string iiifRootAddress = _rootUrl + "/iiif/" + itemId.ToString() + "$" + count.ToString();
            string imageRootAddress = "https://iiif.archivelab.org/iiif/" + barCode + "$" + count.ToString();

            string canvas =
                "{" +
                  "\"@id\": \"" + iiifRootAddress + "/canvas\"," +
                  "\"@type\": \"sc:Canvas\"," +
                  "\"label\": \"" + Helper.CleanManifestData(page.WebDisplay) + "\"," +
                  "\"height\": " + height.ToString() + "," +
                  "\"width\": " + width.ToString() + "," +
                  "\"metadata\": [" +
                    GetMetadataSingleValue("permalink to this page", _rootUrl + "/iiif/page/" + page.PageID.ToString()) +
                  "]," + 
                  "\"images\": [" +
                    "{" +
                      "\"@type\": \"oa:Annotation\"," +
                      "\"motivation\": \"sc:painting\"," +
                      "\"on\": \"" + iiifRootAddress + "/canvas\"," +
                      "\"resource\": {" +
                        "\"@id\": \"" + imageRootAddress + "/full/full/0/default.jpg\"," +
                        "\"@type\": \"dctypes:Image\"," +
                        "\"format\": \"image/jpeg\"," +
                        "\"height\": " + height.ToString() + "," +
                        "\"width\": " + width.ToString() + "," +
                        "\"service\": {" +
                          "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                          "\"@id\": \"" + imageRootAddress + "\"," +
                          "\"profile\": \"https://iiif.io/api/image/2/profiles/level2.json\"" +
                        "}" +
                      "}" +
                    "}" +
                  "]," +
                  "\"otherContent\": [" +
                    "{" +
                      "\"@id\": \"" + _rootUrl + "/iiif/" + itemId.ToString() + "/text/" + count.ToString() + "\"," +
                      "\"@type\": \"sc:AnnotationList\"," +
                      "\"label\": \"Fulltext\"" +
                    "}," +
                    "{" +
                      "\"@id\": \"" + _rootUrl + "/iiif/" + itemId.ToString() + "/names/" + count.ToString() + "\"," +
                      "\"@type\": \"sc:AnnotationList\"," +
                      "\"label\": \"Names\"" +
                    "}" +
                  "]" +
                "}";

            return canvas;
        }

        private string GetStructures(int itemId, List<Segment> segments, List<Page> pages, ScanData scanData)
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
                        string canvas = string.Format("{0}/iiif/{1}${2}/canvas", _rootUrl, itemId.ToString(), (pageScanData == null ? 0 : pageScanData.Sequence - 1));
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
              "\"label\": \"" + Helper.CleanManifestData(segment.Title) + "\"," +
              "\"canvases\": [\"" +
                string.Join("\",\"", canvasList) +
              "\"]" +
            "}";

            return structure;
        }
    }
}
