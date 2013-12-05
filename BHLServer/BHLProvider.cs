using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public MOBOT.FileAccess.IFileAccessProvider GetFileAccessProvider(bool useRemoteProvider)
        {
            if (useRemoteProvider)
            {
                return MOBOT.FileAccess.RemotingUtilities.RemotingHelper.GetRemotedFileAccessProvider();
            }
            else
            {
                return new MOBOT.FileAccess.FileAccessProvider();
            }
        }

        public string GetOcrText(bool useRemoteProvider, string textLocation)
        {
            string ocrText = string.Empty;

            MOBOT.FileAccess.IFileAccessProvider fileAccessProvider = GetFileAccessProvider(useRemoteProvider);
            if (fileAccessProvider.FileExists(textLocation))
            {
                ocrText = fileAccessProvider.GetFileText(textLocation);
            }

            return ocrText;
        }

        public string GetTextUrl(bool useRemoteProvider, string textLocation)
        {
            try
            {
                if (GetFileAccessProvider(useRemoteProvider).FileExists(textLocation))
                    return (textLocation);
                else
                    return ("");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Add a height and width to each element in the specified list of pages
        /// </summary>
        /// <remarks>
        /// This method only works for Internet Archive items for which we have a valid scandata.xml file.
        /// For performance reasons related to how the OpenLibrary open source book viewer works, we need 
        /// to read the dimensions of all pages at once, rather than getting the page dimensions "on the fly".
        /// The scandata.xml file provides us a way to do that for Internet Archive items.
        /// </remarks>
        /// <param name="pages"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public List<BHLProvider.ViewerPage> PageGetImageDimensions(List<BHLProvider.ViewerPage> pages, int itemID)
        {
            try
            {
                // Load the scandata.xml file
                PageSummaryView psv = this.PageSummarySelectByItemId(itemID, true);
                string filePath = psv.OCRFolderShare + "\\" + psv.FileRootFolder + "\\" + psv.BarCode + "_scandata.xml";
                var xml = new XDocument();
                try
                {
                    // Local for a local copy first
                    System.IO.StringReader reader = new System.IO.StringReader(
                        new BHLProvider().GetFileAccessProvider(ConfigurationManager.AppSettings["UseRemoteFileAccessProvider"] == "true").GetFileText(filePath)
                        );
                    xml = XDocument.Load(reader);
                }
                catch
                {
                    // No local file found; look for a remote copy (at Internet Archive)
                    if (string.IsNullOrEmpty(psv.AltExternalURL))
                    {
                        // No external images, so don't look for external scandata file
                        return pages;
                    }
                    else
                    {
                        filePath = String.Format("http://www.archive.org/download/{0}/{1}_scandata.xml", psv.BarCode, psv.BarCode);
                        xml = XDocument.Load(filePath);
                    }
                }

                XNamespace ns = string.Empty;

                // Some scandata.xml files specify a namespace for the XML document, and some do not.
                // Try reading the file without a namespace first; if we can't select an element, then
                // add a namespace to the query and try again.
                if (xml.Element(ns + "book") == null)
                {
                    XAttribute nsAttrib = xml.Root.Attribute("xmlns");
                    if (nsAttrib != null)
                    {
                        // Use the namespace specified on the root element, if one exists
                        ns = nsAttrib.Value;
                    }
                    else
                    {
                        // Try this default namespace if none found on the root element
                        ns = "http://archive.org/scribe/xml";
                    }
                }

                // Read the page elements from the XML document
                var pageElements = from page in xml.Element(ns + "book").
                                       Element(ns + "pageData").
                                       Descendants(ns + "page")
                            where (string)page.Element(ns + "addToAccessFormats") == "true"
                            select page;

                int count = 0;
                foreach (XElement page in pageElements)
                {
                    // Read the height and width of each page and update the list of ViewerPage objects
                    XElement cropBox = page.Element(ns + "cropBox");
                    if (cropBox != null)
                    {
                        XElement widthElement = cropBox.Element(ns + "w");
                        XElement heightElement = cropBox.Element(ns + "h");

                        pages[count].Width = (widthElement == null ? pages[count].Width : (int)Convert.ToDouble(widthElement.Value));
                        pages[count].Height = (heightElement == null ? pages[count].Height : (int)Convert.ToDouble(heightElement.Value));
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Do nothing, just leave height and width at their default values
            }

            return pages;
        }

        /// <summary>
        /// Send OCR for a page to an external service that returns a list of names found in the OCR.
        /// </summary>
        /// <param name="resolverName">Name of the resolver to use.  Valid values are "TaxonFinder" and "GNRD".</param>
        /// <param name="pageID">Identifier of the page</param>
        /// <param name="useRemoteFileAccessProvider">True to use the remote file access service.</param>
        /// <param name="usePreferredResults">True to use the "preferred" results.  Only applies to GNRD service.</param>
        /// <param name="maxReadAttempts">Maximum number of times to attempt to read service results.  Only applies to GNRD service.</param>
        /// <returns>Array of NameFinderResponse objects.</returns>
        public List<NameFinderResponse> GetNamesFromOcr(string resolverName, int pageID, bool useRemoteFileAccessProvider, bool usePreferredResults, int maxReadAttempts)
        {
            List<NameFinderResponse> nameFinderResponses = new List<NameFinderResponse>();

            switch (resolverName)
            {
                case "GNRD":
                    nameFinderResponses = this.GetNamesFromOcrGNRD(pageID, useRemoteFileAccessProvider, usePreferredResults, maxReadAttempts);
                    break;
                case "TaxonFinder":
                default:
                    nameFinderResponses = this.GetNamesFromOcrTaxonFinder(pageID, useRemoteFileAccessProvider);
                    break;
            }

            return nameFinderResponses;
        }

        /// <summary>
        /// Use the TaxonFinder service to extract names from the OCR for a page.
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="useRemoteFileAccessProvider"></param>
        /// <returns></returns>
        private List<NameFinderResponse> GetNamesFromOcrTaxonFinder(int pageID, bool useRemoteFileAccessProvider)
        {
            bool tooLarge = false;
            string webServiceUrl = string.Empty;

            PageSummaryView ps = this.PageSummarySelectByPageId(pageID);
            string filepath = ps.OcrTextLocation;

            if (this.GetFileAccessProvider(useRemoteFileAccessProvider).GetFileSizeInKB(filepath) <= 5)
            {
                // OCR text not too large for URI, so send it in the UBIO request

                // Get the OCR text and start to build the UBIO url
                string ocrText = this.GetFileAccessProvider(useRemoteFileAccessProvider).GetFileText(filepath);
                StringBuilder webServiceUrlSB = new StringBuilder();
                webServiceUrlSB.Append("http://www.ubio.org/webservices/service.php?function=taxonFinder&includeLinks=0&freeText=");
                webServiceUrlSB.Append(System.Web.HttpUtility.UrlEncode(ocrText));

                // Add the existing page names for this Page to the UBIO url
                CustomGenericList<NamePage> namePages = this.NamePageSelectByPageID(pageID);
                foreach (NamePage namePage in namePages)
                {
                    webServiceUrlSB.Append(System.Web.HttpUtility.UrlEncode("  " + namePage.NameString));
                }

                // Get the final UBIO url
                webServiceUrl = webServiceUrlSB.ToString();

                // If the url is too large after all UrlEncoding is complete, just send the file path
                if (((long)webServiceUrl.Length / 1024) > 5)
                    tooLarge = true;
            }
            else
            {
                tooLarge = true;
            }

            // OCR text is too large, so just send the file path
            if (tooLarge)
                webServiceUrl = String.Format("http://www.ubio.org/webservices/service.php?function=taxonFinder&includeLinks=0&url=http://www.biodiversitylibrary.org/pageocr/{0}", pageID.ToString());

            List<NameFinderResponse> nameFinderResponses = new List<NameFinderResponse>();
            XmlTextReader reader = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webServiceUrl);
                req.Method = "POST";
                req.Timeout = 15000;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                reader = new XmlTextReader((System.IO.Stream)resp.GetResponseStream());
                StringBuilder sb = new StringBuilder();

                NameFinderResponse nameFinderResponse = null;
                string currentStage = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Whitespace)
                        continue;

                    if (reader.HasValue)
                        sb.Append(reader.Value);
                    if (currentStage == "nameString" && reader.Value != "")
                    {
                        nameFinderResponse.Name = reader.Value;
                    }
                    if (currentStage == "namebankID" && reader.Value != "")
                    {
                        nameFinderResponse.NameResolved = nameFinderResponse.Name;
                        nameFinderResponse.Identifiers.Add("NameBank|" + reader.Value);
                    }
                    else if (reader.NodeType != XmlNodeType.EndElement)
                    {
                        sb.Append("\n" + reader.Name + ": ");
                        currentStage = reader.Name;
                        if (reader.Name == "entity")
                        {
                            nameFinderResponse = new NameFinderResponse();
                        }
                    }
                    else
                    {
                        if (reader.Name == "entity")
                        {
                            nameFinderResponses.Add(nameFinderResponse);
                        }
                    }
                }
            }
            catch (XmlException xex)
            {
                // Catch and ignore xml exceptions, as they likely represent a 
                // malformed response from the uBio service.  Just return what 
                // we were able to read before the problem was found.
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return nameFinderResponses;
        }

        /// <summary>
        /// Use the GNRD service to extract names from the OCR for a page.
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="useRemoteFileAccessProvider"></param>
        /// <param name="usePreferredResults"></param>
        /// <param name="maxReadAttempts"></param>
        /// <returns></returns>
        private List<NameFinderResponse> GetNamesFromOcrGNRD(int pageID, bool useRemoteFileAccessProvider, bool usePreferredResults, int maxReadAttempts)
        {
            string webServiceUrl = string.Empty;

            PageSummaryView ps = new BHLProvider().PageSummarySelectByPageId(pageID);
            string filepath = ps.OcrTextLocation;

            // Get the OCR text
            string ocrText = this.GetFileAccessProvider(useRemoteFileAccessProvider).GetFileText(filepath);

            // Replace non-printing control characters (tabs, line feeds, etc) with spaces.
            // The GNRD service doesn't like 'empty' strings that contain no printable characters
            StringBuilder cleanOcrText = new StringBuilder();
            for (int i = 0; i < ocrText.Length; i++)
            {
                if ((byte)ocrText[i] < 32)
                    cleanOcrText.Append(" ");
                else
                    cleanOcrText.Append(ocrText[i]);
            }
            ocrText = cleanOcrText.ToString().Trim();

            // Only continue if we have OCR with printable characters.  Otherwise just return.
            List<NameFinderResponse> nameResponseList = new List<NameFinderResponse>();
            if (ocrText.Length > 0)
            {
                // Add the user-reported page names for this Page to the ocrText
                CustomGenericList<NamePage> namePages = new BHLProvider().NamePageSelectByPageID(pageID);
                foreach (NamePage namePage in namePages)
                {
                    // NameSourceID 1 = "User Reported"
                    if (namePage.NameSourceID == 1) ocrText += "  " + namePage.NameString;
                }

                // Get the name finding service url and POST body
                // Data source identifiers listed at http://resolver.globalnames.org/data_sources
                // Use preferred data sources of NameBank (ID: 169) and EOL (ID: 12).
                // The GET url for the service is: http://gnrd.globalnames.org/name_finder.json?text={0}&all_data_sources=true&best_match_only=true&preferred_data_sources=12|169
                //webServiceUrl = "http://gnrd.globalnames.org/name_finder.json";
                webServiceUrl = "http://128.128.164.213/name_finder.json";
                ocrText = string.Format("text={0}&all_data_sources=true&best_match_only=true&preferred_data_sources=12|169", System.Web.HttpUtility.UrlEncode(ocrText));

                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webServiceUrl);
                    req.Method = "POST";
                    req.Timeout = 60000;
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(ocrText);
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = byteArray.Length;
                    Stream dataStream = req.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    JObject jsonResponse = null;
                    using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader((System.IO.Stream)resp.GetResponseStream()))
                        {
                            jsonResponse = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                        }
                    }
                    req = null;

                    // First response from name service may contain a redirect url and no name data.
                    // Might also be true for subsequent requests.  Continue re-requesting until we
                    // get a valid response.  If no response after specified number of attempts, just 
                    // move on.
                    int retryCount = 0;
                    while (jsonResponse["total"] == null && retryCount < maxReadAttempts)
                    {
                        Thread.Sleep(1500);     //  A 1.5 SECOND DELAY SEEMS TO BE IDEAL (1.0, 1.5, and 2.0 tested)
                        req = (HttpWebRequest)WebRequest.Create((string)jsonResponse["token_url"]);
                        req.Method = "GET";
                        req.Timeout = 60000;
                        using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                        {
                            using (StreamReader reader = new StreamReader((System.IO.Stream)resp.GetResponseStream()))
                            {
                                jsonResponse = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                            }
                        }

                        retryCount++;
                    }


                    // Did we get name data?
                    if (jsonResponse["total"] != null)
                    {
                        // Read the name data from the JSON response
                        if (jsonResponse["total"].ToString() != "0")
                        {
                            foreach (JToken name in jsonResponse["resolved_names"])
                            {
                                JToken nameDetail = null;

                                if (usePreferredResults && name["preferred_results"] != null)
                                {
                                    // Read the metadata from the "preferred_results" section of the response.  This
                                    // is the best result returned by the preferred source of data (ie. "EOL", 
                                    // "Namebank", ect).
                                    if (name["preferred_results"].HasValues) nameDetail = name["preferred_results"][0];
                                }
                                if (nameDetail == null && name["results"] != null)
                                {
                                    // Read the metadata from the "results" section of the response.  This is the best
                                    // result returned from any data source.
                                    if (name["results"].HasValues) nameDetail = name["results"][0];
                                }

                                string nameString = (string)(name["supplied_name_string"] ?? string.Empty);
                                string nameResolvedString = string.Empty;
                                string canonicalName = string.Empty;
                                List<string> identifiers = new List<string>();

                                if (nameDetail != null)   // If the name was resolved, get the details
                                {
                                    if (nameDetail.HasValues)
                                    {
                                        string matchType = (nameDetail["match_type"] ?? "0").ToString();

                                        // Possible match_type values
                                        // 1 - exact string match
                                        // 2 - exact string match of canonical form
                                        // 3 - fuzzy match of canonical form
                                        // 4 - partial match on trinomial (perhaps bionomial portion matched; mostly good results here)
                                        // 5 - fuzzy partial match on trinomial
                                        // 6 - exact match on genus, but no match on species (binomial) part  (most questionable results here... usually IS a name, but maybe not the identified name)
                                        if (matchType != "6")  // Don't use questionable matches
                                        {
                                            nameResolvedString = (string)(nameDetail["name_string"] ?? string.Empty);
                                            canonicalName = (string)(nameDetail["canonical_form"] ?? string.Empty);

                                            // Get the identifiers
                                            string identifier = string.Empty;
                                            if (nameDetail["gni_uuid"] != null)
                                            {
                                                identifier = "GNI|" + (string)nameDetail["gni_uuid"];
                                                identifiers.Add(identifier);
                                            }

                                            string identifierValue = (string)(nameDetail["local_id"] ?? string.Empty);
                                            if (string.IsNullOrWhiteSpace(identifierValue)) identifierValue = (string)(nameDetail["taxon_id"] ?? string.Empty);
                                            if (nameDetail["data_source_title"] != null && !string.IsNullOrWhiteSpace(identifierValue))
                                            {
                                                identifier = (string)nameDetail["data_source_title"] + "|" + identifierValue;
                                                identifiers.Add(identifier);
                                            }
                                        }
                                    }
                                }

                                // Add the data from the JSON response to our list of names to return 
                                NameFinderResponse nameFinderResponse = new NameFinderResponse();
                                nameFinderResponse.Name = nameString;
                                nameFinderResponse.NameResolved = nameResolvedString;
                                nameFinderResponse.CanonicalName = canonicalName;
                                nameFinderResponse.Identifiers = identifiers;
                                nameResponseList.Add(nameFinderResponse);
                            }
                        }
                    }
                    else if (retryCount >= maxReadAttempts)
                    {
                        // No response available.  Throw an error.
                        throw new Exception("No response received from GNRD name finding service.");
                    }
                }
                catch
                {
                    throw;
                }
            }

            return (nameResponseList);
        }

        /// <summary>
        /// Get details about the specified name from the Global Names resolver service.
        /// </summary>
        /// <example>
        /// http://resolver.globalnames.org/name_resolvers.xml?names=Poa+annua+ssp.+exilis+(Tomm.+ex+Freyn)+Asch.+%26+Graebn.
        /// http://resolver.globalnames.org/name_resolvers.xml?names=Poa+annua
        /// </example>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<GNResolverResponse> GetNameDetailFromGNResolver(string name)
        {
            List<GNResolverResponse> nameDetails = new List<GNResolverResponse>();

            // Get the name finding service url
            string webServiceUrl = string.Format("http://resolver.globalnames.org/name_resolvers.json?names={0}", name);

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webServiceUrl);
                req.Method = "GET";
                req.Timeout = 60000;

                JObject jsonResponse = null;
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader reader = new StreamReader((System.IO.Stream)resp.GetResponseStream()))
                    {
                        jsonResponse = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    }
                }
                req = null;

                // Did the service successfully evaluate the name?
                string status = (string)jsonResponse.SelectToken("status");
                if (status == "success")
                {
                    JToken data = jsonResponse["data"];

                    // Read the name details from the JSON response
                    if (data != null)
                    {
                        JObject results = (JObject)data[0];
                        foreach (JToken result in results["results"])
                        {
                            int matchType = (int)(result["match_type"] ?? "0");

                            // Possible match_type values
                            // 1 - exact string match
                            // 2 - exact string match of canonical form
                            // 3 - fuzzy match of canonical form
                            // 4 - partial match on trinomial (perhaps bionomial portion matched; mostly good results here)
                            // 5 - fuzzy partial match on trinomial
                            // 6 - exact match on genus, but no match on species (binomial) part  (most questionable results here... usually IS a name, but maybe not the identified name)
                            if (matchType != 6)  // Don't use questionable matches
                            {
                                int dataSourceID = (int)(result["data_source_id"] ?? "0");
                                string dataSourceTitle = (string)(result["data_source_title"] ?? string.Empty);
                                string gniUUID = (string)(result["gni_uuid"] ?? string.Empty);
                                string nameString = (string)(result["name_string"] ?? string.Empty);
                                string canonicalForm = (string)(result["canonical_form"] ?? string.Empty);
                                string classificationPath = (string)(result["classification_path"] ?? string.Empty);
                                string classificationPathRanks = (string)(result["classification_path_ranks"] ?? string.Empty);
                                string classificationPathIDs = (string)(result["classification_path_ids"] ?? string.Empty);
                                string taxonID = (string)(result["taxon_id"] ?? string.Empty);
                                string localID = (string)(result["local_id"] ?? string.Empty);
                                string globalID = (string)(result["global_id"] ?? string.Empty);
                                string url = (string)(result["url"] ?? string.Empty);
                                double score = (double)(result["score"] ?? "0");

                                GNResolverResponse nameDetail = new GNResolverResponse();
                                nameDetail.DataSourceID = dataSourceID;
                                nameDetail.DataSourceTitle = dataSourceTitle;
                                nameDetail.GniUUID = gniUUID;
                                nameDetail.NameString = nameString;
                                nameDetail.CanonicalForm = canonicalForm;
                                nameDetail.ClassificationPath = classificationPath;
                                nameDetail.ClassificationPathRanks = classificationPathRanks;
                                nameDetail.ClassificationPathIDs = classificationPathIDs;
                                nameDetail.TaxonID = taxonID;
                                nameDetail.LocalID = localID;
                                nameDetail.GlobalID = globalID;
                                nameDetail.Url = url;
                                nameDetail.MatchType = matchType;
                                nameDetail.Score = score;
                                nameDetails.Add(nameDetail);
                            }
                        }
                    }
                }
                else
                {
                    // Service failed.  Throw an error.
                    throw new Exception("No response received from GNI name resolution service.");
                }
            }
            catch
            {
                throw;
            }

            return nameDetails;
        }

        [Serializable]
        public class ViewerPage
        {
            public string ExternalBaseUrl = string.Empty;
            public string AltExternalUrl = string.Empty;
            public string BarCode = string.Empty;
            public int? SequenceOrder = null;
            public int Width = 1600;
            public int Height = 2400;
        }

        public static Dictionary<string, string> T_NOTES
        {
            get
            {
                return new Dictionary<string, string>
                {
                     {@"f01a", "pale pencil"}, 
                     {@"f01b", "pencil"}, 
                     {@"f01c", "dark pencil"}, 
                     {@"f02c", "reddish-brown crayon"}, 
                     {@"f04a", "brown ink"}, 
                     {@"f05a", "grey ink"}, 
                     {@"f06a", "dark brown ink"}, 
                     {@"f06g", "dark grey ink"}, 
                     {@"f12e", "erased"}, 
                     {@"f10d", "deleted"}, 
                     {@"f10c", "horizontally crossed"}, 
                     {@"f10V", "vertically crossed"}, 
                     {@"f10C", "'wavily' crossed"}, 
                     {@"f10i", "interlined"}, 
                     {@"f10I", "inserted"}, 
                     {@"f10R", "retouched"}, 
                     {@"f10O", "overwritten"}, 
                     {@"f10q", "doubtful"}, 
                     {@"f11V", "vertical"}, 
                     {@"f14h", "not Darwin's hand"}, 
                     {@"f14q", "doubtfully Darwin's hand"}, 
                     {@"f14H", "Darwin's hand"}
                };
            }
        }


        public static Dictionary<string, string> MARKUPS
        {
            get
            {
                return new Dictionary<string, string>
                {
                {@"\\tx\\m4a\\2~\\td\\m1a", "not in Darwin's hand, in brown ink, cancelled in Darwin's hand in pale pencil"},
                {@"\\tx\\m4a\\2~\\td\\m1b", "not in Darwin's hand, in brown ink, cancelled in Darwin's hand in pencil"},
                {@"\\tx\\m4a\\2~\\td\\m3",  "not in Darwin's hand, in brown ink, cancelled in Darwin's hand in blue crayon"},
                {@"\\tx\\m4a\\2h\\td\\m1b", "not in Darwin's hand, in brown ink, horizontally crossed in Darwin's hand in pencil"},
                {@"\\tx\\m4a\\2v\\m1b\\td", "not in Darwin's hand, in brown ink, vertically crossed in Darwin's hand in pencil"},
                {@"\\tx\\m6a\\2~\\td\\m1b", "not in Darwin's hand, in dark brown ink, cancelled in Darwin's hand in pencil"},
                {@"\\tx\\m6a\\2h\\td\\m1b", "not in Darwin's hand, in dark brown ink, horizontally crossed in Darwin's hand in pencil"},
                {@"\\tx\\m6a\\2v\\td\\m1b", "not in Darwin's hand, in dark brown ink, vertically crossed in Darwin's hand in pencil"},
                {@"\\tx\\2v\\td\\m1b",      "not in Darwin's hand, vertically crossed in Darwin's hand in pencil"},
                {@"\\2~\\td\\m1a",          "cancelled in Darwin's hand in pale pencil"},
                {@"\\2~\\td\\m1b",          "cancelled in Darwin's hand in pencil"},
                {@"\\2~\\td\\m3",           "cancelled in Darwin's hand in blue crayon"},
                {@"\\2h\\td\\m1b",          "horizontally crossed in Darwin's hand in pencil"},
                {@"\\2v\\td\\m1b",          "vertically crossed in Darwin's hand in pencil"},
                {@"\\m1a\\2v\\m1b",         "in pale pencil, vertically crossed in pencil"},
                {@"\\m1b\\2v\\m4a",         "in pale pencil, vertically crossed in brown ink"},
                {@"\\m1c\\2~\\m1b",         "in dark pencil, cancelled in pencil"},
                {@"\\m1c\\o\\m1b",          "in dark pencil over pencil"},
                {@"\\m2b\\2~\\m1b",         "in reddish-orange crayon, cancelled in pencil"},
                {@"\\m2b\\o\\m1b",          "in reddish-orange crayon over pencil"},
                {@"\\m2c\\o\\m1a",          "in reddish-brown crayon over pale pencil"},
                {@"\\m2c\\o\\m1b",          "in reddish-brown crayon over pencil"},
                {@"\\m3\\o\\m1a",           "in blue crayon over pale pencil"},
                {@"\\m4a\\2h\\m1b",         "in brown ink, horizontally crossed in pencil"},
                {@"\\m4a\\2v\\m1c",         "in brown ink, vertically crossed in dark pencil"},
                {@"\\m4a\\o\\m1b",          "in brown ink over pencil"},
                {@"\\m6a\\2v\\m1b",         "in dark brown ink, vertically crossed in pencil"},
                {@"\\m6c\\o\\m1a",          "in black ink over pale pencil"},
                {@"\\m6g\\2v\\m1b",         "in dark grey ink, vertically crossed in pencil"},
                {@"\\m6g\\2v\\m1c",         "in dark grey ink, vertcially crossed in dark pencil"},
                {@"\\m6g\\o\\m1b",          "in dark grey ink over pencil"},
                {@"\\1\\m1c",               "deleted in dark pencil"}, 
                {@"\\2~\\m1b",              "cancelled in pencil"},
                {@"\\2h\\m6g",              "horizontally crossed in dark grey ink"},
                {@"\\2h\\td",               "horizontally crossed in Darwin's hand"},
                {@"\\2v\\m1c",              "vertically crossed in dark pencil"},
                {@"\\2v\\m3",               "vertically crossed in blue crayon"},
                {@"\\m1a\\2~",              "in pale pencil, cancelled"},
                {@"\\m2c\\2~",              "in reddish-brown crayon, cancelled"},
                {@"\\m5d\\2~",              "in pale brown ink, cancelled"},
                {@"\\td\\m1a",              "in Darwin's hand, in pale pencil"},
                {@"\\td\\m1b",              "in Darwin's hand, in pencil"},
                {@"\\td\\m1c",              "in Darwin's hand, in dark pencil"},
                {@"\\td\\m2b",              "in Darwin's hand, in reddish-orange crayon"},
                {@"\\td\\m3",               "in Darwin's hand, in blue crayon"},
                {@"\\td\\m4a",              "in Darwin's hand, in brown ink"},
                {@"\\td\\m6a",              "in Darwin's hand, in dark brown ink"},
                {@"\\td\\m6g",              "in Darwin's hand, in dark grey ink"},
                {@"\\tq\\m6a",              "maybe not in Darwin's hand, in dark brown ink"},
                {@"\\tx\\m5c",              "not in Darwin's hand, in mid-grey ink"},
                {@"\\tx\\m5d",              "not in Darwin's hand, in pale brown ink"},
				{@"\\0",    "erased"},
				{@"\\1", "  deleted"},
				{@"\\2h",   "horizontally crossed"},
				{@"\\2v",   "vertically crossed"},
				{@"\\2~",   "cancelled"},
				{@"\\3",    "lines counted from bottom of text"},
				{@"\\4",    "word illegible "},
				{@"\\5",    "doubtful transcription"},
				{@"\\6",    "word partly illegible"},
				{@"\\8",    "vertical"},
				{@"\\9",    "line across page"},
				{@"\\m1a",  "in pale pencil"},
				{@"\\m1b",  "in pencil"},
				{@"\\m1c",  "in dark pencil"},
				{@"\\m2b",  "in reddish-orange crayon"},
				{@"\\m2c",  "in reddish-brown crayon"},
				{@"\\m3",   "in blue crayon"},
				{@"\\m4a",  "in brown ink"},
                {@"\\m5c",   "in light grey ink"},
                {@"\\m5C",   "in mid-grey ink"},
                {@"\\m5d",   "in pale brown ink"},
                {@"\\m6a",   "in dark brown ink"},
                {@"\\m6c",   "in black ink"},
                {@"\\m6g",   "in dark grey ink"},
				{@"\\m",    "medium"},
				{@"\\p0",   "ringed"},
				{@"\\H",    "headnote"},
				{@"\\o",    "overwrite"},
				{@"\\q",    "arrow"},
                {@"\\td",    "in Darwin's hand"},
                {@"\\tq",    "maybe not in Darwin's hand"},
                {@"\\tx",    "not in Darwin's hand"},
				{@"\\T",    "in Darwin\'s hand"},
				{@"\\A",    "appendum"},
                };
            }
        }

        #region ICONS
        public static Dictionary<string, string> ICONS
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"icon01"," pale pencil "},
                    {"icon02"," pencil "},
                    {"icon03"," dark pencil "},
                    {"icon04"," reddish-brown crayon "},
                    {"icon05"," brown ink "},
                    {"icon06"," grey ink "},
                    {"icon07"," dark brown ink "},
                    {"icon08"," dark grey ink "},

                    //['overlays']
                    {"icon09"," erased "},
                    {"icon10"," deleted "},
                    {"icon11"," horizontally crossed "},
                    {"icon12"," vertically crossed "},
                    {"icon13"," \'wavily\' crossed "},
                    {"icon14"," interlined "},
                    {"icon15"," inserted "},
                    {"icon16"," retouched "},
                    {"icon17"," overwritten "},

                    //[misc]
                    {"icon18"," doubtful "},
                    {"icon19"," vertical "},
                    {"icon20"," not Darwin\'s hand "},
                    {"icon21"," doubtfully Darwin\'s hand "},
                    {"icon22"," Darwin\'s hand "}
                };
            }
        }
        #endregion
    }
}
