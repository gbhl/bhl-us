using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace BHL.IIIF
{
    public class Manifest
    {
        public string GetManifest(int itemId)
        {
            BHLProvider provider = new BHLProvider();
            Item item = provider.ItemSelectAuto(itemId);
            Title title = provider.TitleSelectExtended(item.PrimaryTitleID);
            CustomGenericList<Page> pages = provider.PageSelectByItemID(itemId);
            CustomGenericList<Segment> segments = provider.SegmentSelectByItemID(itemId);

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
                    "\"@id\": \"https://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID + "\"" +
                  "}," +
                  GetMetadata(title, item) +
                  GetSeeAlso(itemId) +
                  GetSequences(item.BarCode, pages) +
                  GetStructures() +
                  GetRelated() +
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
                foreach(Author author in title.TitleAuthors)
                {
                    string authorString = "<a href='https://www.biodiversitylibrary.org/creator/" + author.AuthorID.ToString() + "'>" + author.NameExtended + "</a>";
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
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl)) institutionName = "<a href='" + institution.InstitutionUrl + "'>" + institutionName + "</a>";
                        metadata += GetMetadataSingleValue("holding institution", institutionName);
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(item.Sponsor))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("sponsor", item.Sponsor);
                }

                if (!string.IsNullOrWhiteSpace(item.Sponsor))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("sponsor", item.Sponsor);
                }

                if (!string.IsNullOrWhiteSpace(item.LicenseUrl))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("license type", "<a href='" + item.LicenseUrl + "'>" + item.LicenseUrl + "</a>");
                }

                if (!string.IsNullOrWhiteSpace(item.Rights))
                {
                    if (metadata.Length > 0) metadata += ",";
                    metadata += GetMetadataSingleValue("rights", "<a href='" + item.Rights+ "'>" + item.Rights+ "</a>");
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
                        if (!string.IsNullOrWhiteSpace(institution.InstitutionUrl)) institutionName = "<a href='" + institution.InstitutionUrl + "'>" + institutionName + "</a>";
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
                  "},";

            return metadata;
        }

        private string GetRelated()
        {
            string related = 
                "\"related\": [" +
                  "{" +
                      "\"@id\": \"https://www.biodiversitylibrary.org/bibliography/43746\"," +
                      "\"label\": \"Additional Bibliographic Information\"" +
                  "}" +
                "]";

            return related;
        }

        private string GetSeeAlso(int itemId)
        {
            string seeAlso = 
                "\"seeAlso\": [" +
                  "{" +
                      "\"@id\": \"https://www.biodiversitylibrary.org/itempdf/" + itemId.ToString() + "\"," +
                      "\"label\": \"Download PDF\"," +
                      "\"format\": \"application/pdf\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"https://www.biodiversitylibrary.org/itemimages/" + itemId.ToString() + "\"," +
                      "\"label\": \"Download JPEG 2000\"," +
                      "\"format\": \"application/zip\"" +
                  "}," +
                  "{" +
                      "\"@id\": \"https://www.biodiversitylibrary.org/itemtext/" + itemId.ToString() + "\"," +
                      "\"label\": \"Download Text\"," +
                      "\"format\": \"text/plain\"" +
                  "}" +
                "],";

            return seeAlso;
        }

        private string GetSequences(string barCode, CustomGenericList<Page> pages)
        {
            // TODO:  Get scandata.xml and extract heights, widths, and sequence numbers of pages to display

            string sequences = string.Empty;

            if (pages.Count > 0)
            {
                sequences =
                    "\"sequences\": [" +
                      "{" +
                        "\"@context\": \"http://iiif.io/api/image/2/context.json\"," +
                        "\"@id\": \"http://iiif.archivelab.org/iiif/" + barCode + "/canvas/default\"," +
                        "\"@type\": \"sc:Sequence\"," +
                        GetCanvases(barCode, pages) +
                        "\"label\": \"default\"" +
                      "}" +
                    "],";
            }

            return sequences;
        }


        private string GetCanvases(string barCode, CustomGenericList<Page> pages)
        {
            string canvases = string.Empty;

            canvases = "\"canvases\": [";

            int count = 1;
            foreach (Page page in pages)
            {
                if (count > 1) canvases += ",";
                canvases += GetCanvas(barCode, page, count);
                count++;
            }
            canvases += "],";

            return canvases;
        }

        private string GetCanvas(string barCode, Page page, int count, int height = 2469, int width = 1724)
        {
            // TODO:  Get "real" values for height and width
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

        private string GetStructures()
        {
            string structures = string.Empty;


            // TODO:  Build segment information for IIIF manifest



            return structures;
        }
    }
}
