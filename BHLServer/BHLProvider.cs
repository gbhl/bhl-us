using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.FileAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider : IBHLProvider
    {
        public MOBOT.FileAccess.IFileAccessProvider GetFileAccessProvider()
        {
            return new MOBOT.FileAccess.FileAccessProvider();
        }

        public string GetTextUrl(string textLocation)
        {
            try
            {
                if (GetFileAccessProvider().FileExists(textLocation))
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
        public List<BHLProvider.ViewerPage> PageGetImageDimensions(List<BHLProvider.ViewerPage> pages, ItemType itemType, int entityID)
        {
            try
            {
                var xml = new XDocument();
                bool scanDataLoaded = false;
                string scanData = this.ScandataGetFileContents(itemType, entityID);
                if (!string.IsNullOrWhiteSpace(scanData))
                {
                    try
                    {
                        xml = XDocument.Load(new StringReader(scanData));
                        scanDataLoaded = true;
                    }
                    catch
                    {
                        // Could not load, try a remote copy instead
                    }
                }

                if (!scanDataLoaded)
                {
                    // Local file not loaded; look for a remote copy (at Internet Archive)
                    Item item = this.ItemSelectFilenames(itemType, entityID);
                    xml = XDocument.Load(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.ScandataFilename));
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
        public List<NameFinderResponse> GetNamesFromOcr(string resolverName, int pageID, bool usePreferredResults, int maxReadAttempts)
        {
            List<NameFinderResponse> nameFinderResponses = new List<NameFinderResponse>();

            switch (resolverName)
            {
                case "TaxonFinder":
                    nameFinderResponses = this.GetNamesFromOcrTaxonFinder(pageID, usePreferredResults, maxReadAttempts);
                    break;
                case "GNFinder":
                    nameFinderResponses = this.GetNamesFromOcrGNFinderService(pageID, usePreferredResults, maxReadAttempts);
                    break;
                case "GNFinderLocal":
                default:
                    nameFinderResponses = this.GetNamesFromOcrGNFinder(pageID);
                    break;
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
        private List<NameFinderResponse> GetNamesFromOcrTaxonFinder(int pageID, bool usePreferredResults, int maxReadAttempts)
        {
            string webServiceUrl = string.Empty;

            PageSummaryView ps = new BHLProvider().PageSummarySelectByPageId(pageID);
            if (ps == null) ps = new BHLProvider().PageSummarySegmentSelectByPageID(pageID);
            string filepath = ps.OcrTextLocation;

            // Get the OCR text
            string ocrText = this.GetFileAccessProvider().GetFileText(filepath);

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
                List<NamePage> namePages = new BHLProvider().NamePageSelectByPageID(pageID);
                foreach (NamePage namePage in namePages)
                {
                    // NameSourceID 1 = "User Reported"
                    if (namePage.NameSourceID == 1) ocrText += "  " + namePage.NameString;
                }

                // Get the name finding service url and POST body
                // Data source identifiers listed at http://resolver.globalnames.org/data_sources
                // Use preferred data sources of NameBank (ID: 169) and EOL (ID: 12).
                // The GET url for the service is: http://gnrd.globalnames.org/name_finder.json?text={0}&all_data_sources=true&best_match_only=true&preferred_data_sources=12|169
                webServiceUrl = ConfigurationManager.AppSettings["GNRDTaxonFinderBaseAddress"];
                ocrText = string.Format(ConfigurationManager.AppSettings["GNRDTaxonFinderRequestContent"], System.Web.HttpUtility.UrlEncode(ocrText));

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
                        throw new Exception("No response received from GNRD TaxonFinder name finding service.");
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
        /// Use the GNRD service to extract names from the OCR for a page.
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="useRemoteFileAccessProvider"></param>
        /// <param name="usePreferredResults"></param>
        /// <param name="maxReadAttempts"></param>
        /// <returns></returns>
        private List<NameFinderResponse> GetNamesFromOcrGNFinderService(int pageID, bool usePreferredResults, int maxReadAttempts)
        {
            string webServiceUrl = string.Empty;

            PageSummaryView ps = new BHLProvider().PageSummarySelectByPageId(pageID);
            if (ps == null) ps = new BHLProvider().PageSummarySegmentSelectByPageID(pageID);
            string filepath = ps.OcrTextLocation;

            // Get the OCR text
            string ocrText = this.GetFileAccessProvider().GetFileText(filepath);

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
                List<NamePage> namePages = new BHLProvider().NamePageSelectByPageID(pageID);
                foreach (NamePage namePage in namePages)
                {
                    // NameSourceID 1 = "User Reported"
                    if (namePage.NameSourceID == 1) ocrText += "  " + namePage.NameString;
                }

                // Get the name finding service url and POST body
                // Data source identifiers listed at http://resolver.globalnames.org/data_sources
                // Use preferred data sources of NameBank (ID: 169) and EOL (ID: 12).
                // The GET url for the service is: http://gnrd.globalnames.org/name_finder.json?text={0}&all_data_sources=true&best_match_only=true&preferred_data_sources=12|169
                webServiceUrl = ConfigurationManager.AppSettings["GNRDGNFinderBaseAddress"];
                ocrText = string.Format(ConfigurationManager.AppSettings["GNRDGNFinderRequestContent"], System.Web.HttpUtility.UrlEncode(ocrText));

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
                            foreach (JToken name in jsonResponse["verified_names"])
                            {
                                bool usePreferredIds = false;
                                JToken nameDetail = null;

                                if (usePreferredResults && name["preferred_results"] != null)
                                {
                                    // Read the metadata from the "preferred_results" section of the response.  This
                                    // is the best result returned by the preferred source of data (ie. "EOL", 
                                    // "Namebank", ect).
                                    if (name["preferred_results"].HasValues)
                                    {
                                        nameDetail = name["preferred_results"][0];
                                        usePreferredIds = true;
                                    }
                                }
                                if (nameDetail == null && name["results"] != null)
                                {
                                    // Read the metadata from the "results" section of the response.  This is the best
                                    // result returned from any data source.
                                    if (name["results"].HasValues) nameDetail = name["results"];
                                }

                                string nameString = (string)(name["supplied_name_string"] ?? string.Empty);
                                string nameResolvedString = string.Empty;
                                string canonicalName = string.Empty;
                                List<string> identifiers = new List<string>();

                                if (nameDetail != null)   // If the name was resolved, get the details
                                {
                                    if (nameDetail.HasValues)
                                    {
                                        string matchType = null;
                                        if (nameDetail["match_type"] != null) matchType = nameDetail["match_type"].ToString();
                                        if (matchType == null) matchType = (nameDetail["match_value"] ?? "NONE").ToString();

                                        // Possible match_type values
                                        // NONE
                                        // EXACT - exact string match
                                        // FUZZY - fuzzy match of canonical form
                                        // PARTIAL_EXACT - partial match on trinomial, or exact match on genus but no match on species (binomial) part (mostly good results here)
                                        // PARTIAL_FUZZY - fuzzy partial match on trinomial
                                        if (matchType != "NONE")  // Don't use questionable matches
                                        {
                                            nameResolvedString = (string)(nameDetail["matched_name"] ?? string.Empty);
                                            canonicalName = (string)(nameDetail["matched_canonical"] ?? string.Empty);

                                            // Get the identifiers
                                            string identifier = string.Empty;

                                            if (usePreferredIds)
                                            {
                                                foreach(JToken identifierDetail in name["preferred_results"])
                                                {
                                                    identifier = this.GetIdentifierFromGNFinder(identifierDetail, "taxon_id", "data_source_id");
                                                    if (!string.IsNullOrWhiteSpace(identifier)) identifiers.Add(identifier);
                                                }
                                            }
                                            else
                                            {
                                                identifier = this.GetIdentifierFromGNFinder(nameDetail, "taxon_id", "data_source_id");
                                                if (!string.IsNullOrWhiteSpace(identifier)) identifiers.Add(identifier);
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
                        throw new Exception("No response received from GNRD GNFinder name finding service.");
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
        /// Use the gnfinder tool to extract names from the OCR for a page.
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="useRemoteFileAccessProvider"></param>
        /// <param name="maxReadAttempts"></param>
        /// <returns></returns>
        private List<NameFinderResponse> GetNamesFromOcrGNFinder(int pageID)
        {
            List<NameFinderResponse> nameResponseList = new List<NameFinderResponse>();

            // Only continue if the gnfinder tool exists
            string toolPath = AppDomain.CurrentDomain.BaseDirectory + "bin\\gnfinder.exe";
            if (this.GetFileAccessProvider().FileExists(toolPath))
            {
                PageSummaryView ps = new BHLProvider().PageSummarySelectByPageId(pageID);
                if (ps == null) ps = new BHLProvider().PageSummarySegmentSelectByPageID(pageID);
                string filepath = ps.OcrTextLocation;

                if (this.GetFileAccessProvider().FileExists(filepath))
                {
                    try
                    {
                        string gnfinderOutput = string.Empty;
                        using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                        {
                            process.StartInfo.FileName = toolPath;
                            process.StartInfo.Arguments = "-v -f pretty \"" + filepath + "\"";
                            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = true;
                            process.Start();

                            // Synchronously read the standard output of the spawned process.
                            gnfinderOutput = process.StandardOutput.ReadToEnd();
                            process.WaitForExit();
                        }

                        if (!string.IsNullOrWhiteSpace(gnfinderOutput))
                        {
                            JObject jsonResponse = JObject.Parse(gnfinderOutput);
                            JToken metadata = jsonResponse["metadata"];

                            // Did we get name data?
                            if (metadata["totalNames"].ToString() != "0")
                            {
                                // Read the name data from the JSON response
                                foreach (JToken name in jsonResponse["names"])
                                {
                                    string nameString = (string)(name["name"] ?? string.Empty);
                                    string nameResolvedString = string.Empty;
                                    string canonicalName = string.Empty;
                                    List<string> identifiers = new List<string>();
                                    string matchType = string.Empty;
                                    double odds;
                                    try
                                    {
                                        odds = (double)name["oddsLog10"];
                                    }
                                    catch (System.OverflowException)
                                    {
                                        odds = double.MaxValue;
                                    }
                                    string curation = string.Empty;

                                    JToken verification = name["verification"];
                                    if (verification != null)   // If the name was resolved, get the details
                                    {
                                        // *** Possible curation values ***
                                        // NotCurated - no DataSources were curated sufficiently
                                        // AutoCurated - at least one of the DataSources invested significantly in curating their data by scripts
                                        // Curated - at least one DataSource is sufficiently curated
                                        curation = (string)(verification["curation"] ?? string.Empty);

                                        JToken bestResult = verification["bestResult"];
                                        if (bestResult != null)
                                        {
                                            matchType = (bestResult["matchType"] != null) ? bestResult["matchType"].ToString() : "";

                                            // *** Possible match_type values *** 
                                            //  (blank) - unknown; likely due to no matching name
                                            //  NoMatch - no match
                                            //  Exact - exact string match
                                            //  PartialExact - partial match on trinomial, or exact match on genus but no match on species(binomial) part(mostly good results here)
                                            //  Fuzzy - fuzzy match of canonical form
                                            //  PartialFuzzy - fuzzy partial match on trinomial
                                            // *** These are also possible, but not supported by BHL ***
                                            //  Virus - matches of viruses names
                                            //  ExactSpeciesGroup - optional match for autonyms for botany or coordinated names in zoology
                                            //  FacetedSearch - only happens when advanced search Language option is used (https://github.com/gnames/gnverifier#advanced-search-query-language)
                                            if (matchType != "" && matchType != "NoMatch" && matchType != "Virus" &&
                                                matchType != "ExactSpeciesGroup" && matchType != "FacetedSearch")
                                            {
                                                nameResolvedString = (string)(bestResult["matchedName"] ?? string.Empty);
                                                canonicalName = (string)(bestResult["matchedCanonicalFull"] ?? string.Empty);

                                                // Get the identifiers
                                                string identifier = string.Empty;
                                                identifier = this.GetIdentifierFromGNFinder(bestResult, "recordId", "dataSourceId");
                                                if (!string.IsNullOrWhiteSpace(identifier)) identifiers.Add(identifier);
                                            }
                                        }
                                    }

                                    // Add the data from the JSON response to our list of names to return 
                                    bool keepName = false;
                                    if (matchType == "Exact" && curation != "NotCurated") keepName = true;
                                    if ((matchType == "Fuzzy" || matchType == "PartialFuzzy" || matchType == "NoMatch" || matchType == "") && odds > 6) keepName = true;
                                    if (matchType == "PartialExact") keepName = true;

                                    if (keepName)
                                    {
                                        NameFinderResponse nameFinderResponse = new NameFinderResponse();
                                        nameFinderResponse.Name = nameString;
                                        nameFinderResponse.NameResolved = nameResolvedString;
                                        nameFinderResponse.CanonicalName = canonicalName;
                                        nameFinderResponse.MatchType = matchType;
                                        nameFinderResponse.Curation = curation;
                                        nameFinderResponse.Identifiers = identifiers;
                                        nameResponseList.Add(nameFinderResponse);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception(string.Format("File {0} not found.", filepath));
                }
            }
            else
            {
                throw new Exception("Name-finding utility (gnfinder.exe) not found.");
            }

            return (nameResponseList);
        }

        /// <summary>
        /// Read a name identifier type and value from the supplied JSON node of a GNFinder response
        /// </summary>
        /// <param name="nameDetail"></param>
        /// <returns></returns>
        public string GetIdentifierFromGNFinder(JToken nameDetail, string recordIdField, string dataSourceIdField)
        {
            string identifier = string.Empty;
            string identifierValue = (string)(nameDetail[recordIdField] ?? string.Empty);
            if (nameDetail[dataSourceIdField] != null && !string.IsNullOrWhiteSpace(identifierValue))
            {
                int dataSourceID = (int)nameDetail[dataSourceIdField];

                // Full list of data sources at http://resolver.globalnames.org/data_sources
                Identifier id = this.IdentifierSelectByGNFinderDataSource(dataSourceID);
                if (id != null) identifier = id.IdentifierName + "|" + identifierValue;
            }
            return identifier;
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
                JObject jsonResponse = null;
                string cacheKey = string.Format("NameSource-{0}", name);

                string json = ApplicationCacheGet(cacheKey);
                if (json == null)
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webServiceUrl);
                    req.Method = "GET";
                    req.Timeout = 60000;

                    using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader((System.IO.Stream)resp.GetResponseStream()))
                        {
                            jsonResponse = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                        }
                    }

                    ApplicationCacheAdd(cacheKey, jsonResponse.ToString(), expiration: DateTime.Now.AddDays(7));
                    req = null;
                }
                else
                {
                    jsonResponse = JObject.Parse(json);
                }

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

            // Update name details with name source metadata from database
            if (nameDetails.Count > 0)
            {
                List<NameSourceGNFinder> nameSources = new BHLProvider().NameSourceGNFinderSelectAll();

                foreach(GNResolverResponse nameDetail in nameDetails)
                {
                    NameSourceGNFinder nameSource = nameSources.Find(delegate(NameSourceGNFinder x) { return x.DataSourceID == nameDetail.DataSourceID; });
                    if (nameSource != null)
                    {
                        nameDetail.DataSourceTitle = nameSource.GNDataSourceLabel;
                        if (nameSource.GNDataSourceURLFormat == null) nameDetail.Url = string.Empty;
                        if (!string.IsNullOrWhiteSpace(nameSource.GNDataSourceURLFormat))
                        {
                            nameDetail.Url = string.Format(nameSource.GNDataSourceURLFormat,
                                !string.IsNullOrWhiteSpace(nameDetail.LocalID) ? nameDetail.LocalID : nameDetail.TaxonID
                                );
                        }
                    }
                }
            }

            // Format the Global Names response

            // Get the distinct name strings in the response, along with the 'best' match type for each
            // name.  Match type is a value from 1 to 6, with 1 being the best (most definite) match,
            // and 6 being the worst (most questionable) match.  Order the list by match type and by
            // name string.
            var distinctSources = from n in nameDetails
                                  group n by n.NameString into g
                                  let MatchType = g.Min(n => n.MatchType)
                                  orderby g.Key
                                  select new { NameString = g.Key, MatchType };

            // For each name string, accumulate every result for that string and add it to the final 
            // ordered result set.
            List<GNResolverResponse> displayNames = new List<GNResolverResponse>();
            foreach (var nameString in distinctSources)
            {
                var sourceNames = from n in nameDetails
                                  where n.NameString == nameString.NameString
                                  orderby n.MatchType, n.DataSourceTitle, n.Score
                                  select n;

                foreach (GNResolverResponse nameDetail in sourceNames)
                {
                    displayNames.Add(nameDetail);
                }
            }

            return displayNames;
        }

        /// <summary>
        /// Determine if an OCR job file exists for the specified item
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public bool OcrJobExists(int itemID)
        {
            IFileAccessProvider fileAccessProvider = this.GetFileAccessProvider();
            string fileName = string.Format("{0}{1}", ConfigurationManager.AppSettings["OCRJobNewPath"], itemID.ToString());
            return fileAccessProvider.FileExists(fileName);
        }

        /// <summary>
        /// Create an OCR job file for the specified item
        /// </summary>
        /// <param name="itemID"></param>
        public void OcrCreateJob(int itemID)
        {
            IFileAccessProvider fileAccessProvider = this.GetFileAccessProvider();
            string fileName = string.Format("{0}{1}", ConfigurationManager.AppSettings["OCRJobNewPath"], itemID.ToString());
            byte[] fileContent = new byte[] { 0x20 };
            fileAccessProvider.SaveFile(fileContent, fileName);
        }

        public string GetOcrText(int pageID)
        {
            Page page = this.PageSelectOcrPathForPageID(pageID);
            String ocrText = "Text unavailable for this page.";

            // Make sure we found an active page
            if (page != null)
            {
                IFileAccessProvider fileAccessProvider = GetFileAccessProvider();
                String ocrTextLocation = String.Format(ConfigurationManager.AppSettings["OCRTextLocation"],
                    page.OcrFolderShare, page.FileRootFolder, page.BarCode, page.FileNamePrefix);
                if (fileAccessProvider.FileExists(ocrTextLocation)) ocrText = fileAccessProvider.GetFileText(ocrTextLocation);
            }

            return ocrText;
        }

        public string GetItemText(ItemType itemType, int entityID)
        {
            Book book = null;
            if (itemType == ItemType.Book)
            {
                book = this.BookSelectTextPathForItemID(entityID);
            }
            else if (itemType == ItemType.Segment)
            {
                book = this.BookSelectTextPathForSegmentID(entityID);
            }
            string itemText = "Text unavailable for this item.";

            // Make sure we found an active item
            if (book != null)
            {
                IFileAccessProvider fileAccessProvider = GetFileAccessProvider();
                String ocrTextLocation = String.Format(ConfigurationManager.AppSettings["ItemTextLocation"], book.OcrFolderShare, book.FileRootFolder, book.BarCode);

                string[] files = fileAccessProvider.GetFiles(ocrTextLocation);
                Array.Sort(files);
                StringBuilder sb = new StringBuilder();
                foreach(string file in files)
                {
                    sb.Append(fileAccessProvider.GetFileText(file));
                    sb.AppendLine();
                }

                itemText = sb.ToString();
            }

            return itemText;
        }

        public byte[] GetItemPdf(ItemType itemType, int entityID)
        {
            byte[] pdf = null;

            if (itemType == ItemType.Book)
            {
                throw new NotImplementedException();
            }
            else if (itemType == ItemType.Segment)
            {
                IFileAccessProvider fileAccessProvider = GetFileAccessProvider();
                string pdfLocation = GetItemPdfPath(itemType, entityID);
                pdf = fileAccessProvider.ReadAllBytes(pdfLocation);
            }

            return pdf;
        }

        public string GetItemPdfPath(ItemType itemType, int entityID)
        {
            string pdfPath = string.Empty;

            if (itemType == ItemType.Book)
            {
                throw new NotImplementedException();
            }
            else if (itemType == ItemType.Segment)
            {
                // PDF are stored in the following folder structure
                // Segment 1
                //  root\1\bhl-segment-1.pdf
                // Segment 10
                //  root\1\0\bhl-segment-10.pdf
                // Segment 110 and Segment 1123
                //  root\1\1\bhl-segment-110.pdf
                //  root\1\1\bhl-segment-1123.pdf
                string folder1 = entityID.ToString()[0].ToString() + "\\";
                string folder2 = entityID > 9 ? entityID.ToString()[1].ToString() + "\\" : "";
                pdfPath = String.Format(ConfigurationManager.AppSettings["PregeneratedPdfLocation"], folder1, folder2, entityID.ToString());
            }

            return pdfPath;
        }

        /// <summary>
        /// Determine if a MARC file exists for the specified title or item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">"t" for title, "i" for item</param>
        /// <returns>Path to the file</returns>
        public string MarcFileExists(int id, string type)
        {
            string filepath = string.Empty;
            IFileAccessProvider fileAccessProvider = this.GetFileAccessProvider();

            if (type == "t")
            {
                // Check vaults for imported MARC file
                Title title = this.TitleSelectAuto(id);
                List<Vault> vaults = this.VaultSelectAll();
                foreach (Vault vault in vaults)
                {
                    if (fileAccessProvider.FileExists(String.Format(ConfigurationManager.AppSettings["MARCXmlLocation"], vault.OCRFolderShare, title.MARCBibID, title.MARCBibID)))
                    {
                        filepath = String.Format(ConfigurationManager.AppSettings["MARCXmlLocation"], vault.OCRFolderShare, title.MARCBibID, title.MARCBibID);
                        break;
                    }
                }
            }

            if (type == "i")
            {
                PageSummaryView ps = this.PageSummarySelectByItemId(id, false);
                if (ps != null)
                {
                    if (fileAccessProvider.FileExists(ps.MarcXmlLocation)) filepath = ps.MarcXmlLocation;
                }
            }

            return filepath;
        }

        /// <summary>
        /// Get the contents of the MARC file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string MarcGetFileContents(int id, string type)
        {
            string fileContents = "MARC not found.";
            string filepath = string.Empty;

            IFileAccessProvider fileAccessProvider = this.GetFileAccessProvider();
            filepath = this.MarcFileExists(id, type);
            if (!string.IsNullOrWhiteSpace(filepath))
            {
                fileContents = fileAccessProvider.GetFileText(filepath);
            }

            return fileContents;
        }

        /// <summary>
        /// Create a MARC file for the specified item
        /// </summary>
        /// <param name="itemID"></param>
        public void MarcCreateFile(string marcBibID, string content)
        {
            MOBOT.BHL.DataObjects.Configuration config = this.ConfigurationSelectByName(
                System.Configuration.ConfigurationManager.AppSettings["ConfigNameCurrentIAVault"]);
            if (config != null)
            {
                Vault vault = this.VaultSelect(Convert.ToInt32(config.ConfigurationValue));
                if (vault != null)
                {
                    String destinationFile = string.Format("{0}\\{1}\\{2}_marc.xml", vault.OCRFolderShare, marcBibID, marcBibID);
                    MOBOT.FileAccess.IFileAccessProvider fileAccess =
                        this.GetFileAccessProvider();
                    fileAccess.SaveFile(Encoding.ASCII.GetBytes(content), destinationFile);
                }
            }
        }

        /// <summary>
        /// Determine if a Scandata file exists for the specified item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Path to the file</returns>
        public string ScandataFileExists(ItemType itemType, int id)
        {
            string filepath = string.Empty;
            IFileAccessProvider fileAccessProvider = this.GetFileAccessProvider();

            PageSummaryView ps = null;
            if (itemType == ItemType.Book)
            {
                ps = this.PageSummarySelectByItemId(id, false);
            }
            else if (itemType == ItemType.Segment)
            {
                var pages = this.PageSummarySegmentSelectBySegmentID(id);
                if (pages.Count > 0) ps = pages[0];
            }
            if (ps != null)
            {
                if (fileAccessProvider.FileExists(ps.ScandataXmlLocation)) filepath = ps.ScandataXmlLocation;
            }

            return filepath;
        }

        /// <summary>
        /// Get the contents of the Scandata file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ScandataGetFileContents(ItemType itemType, int id)
        {
            string fileContents = string.Empty;
            string filepath = string.Empty;

            IFileAccessProvider fileAccessProvider = this.GetFileAccessProvider();
            filepath = this.ScandataFileExists(itemType, id);
            if (!string.IsNullOrWhiteSpace(filepath))
            {
                fileContents = fileAccessProvider.GetFileText(filepath);
            }

            return fileContents;
        }

        /// <summary>
        /// Produce a list of key-value pairs that represent the metadata required to allow the
        /// specified item to be indexed by Google Scholar.
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetGoogleScholarMetadataForItem(int itemID, string uriFormat)
        {
            List<KeyValuePair<string, string>> tags = new List<KeyValuePair<string, string>>();

            BHLProvider service = new BHLProvider();
            Book book = service.BookSelectByBarcodeOrItemID(itemID, null);

            if (book != null)
            {
                // Return no tags for external segments
                if (string.IsNullOrWhiteSpace(book.ExternalUrl))
                {
                    Title title = service.TitleSelectAuto((int)book.PrimaryTitleID);
                    string itemDate = string.IsNullOrWhiteSpace(book.StartYear) ? title.StartYear.ToString() : book.StartYear;

                    AddGoogleScholarTag(tags, "citation_title", book.TitleName);
                    AddGoogleScholarTag(tags, "citation_publication_date", itemDate);
                    AddGoogleScholarTag(tags, "citation_publisher", title.Datafield_260_b);
                    AddGoogleScholarTag(tags, "citation_language", book.LanguageCode);
                    AddGoogleScholarTag(tags, "citation_volume", book.Volume);

                    AddGoogleScholarTag(tags, "DC.title", book.TitleName);
                    AddGoogleScholarTag(tags, "DC.issued", itemDate);
                    AddGoogleScholarTag(tags, "DC.publisher", title.Datafield_260_b);
                    AddGoogleScholarTag(tags, "DC.language", book.LanguageCode);
                    AddGoogleScholarTag(tags, "DC.citation.volume", book.Volume);
                    AddGoogleScholarTag(tags, "DC.identifier.URI", string.Format(uriFormat, book.BookID.ToString()));

                    List<TitleAuthor> authors = service.TitleAuthorSelectByTitle((int)book.PrimaryTitleID);
                    foreach (TitleAuthor author in authors)
                    {
                        AddGoogleScholarTag(tags, "citation_author", author.FullName);
                        AddGoogleScholarTag(tags, "DC.creator", author.FullName);
                    }

                    List<Title_Identifier> identifiers = service.Title_IdentifierSelectByTitleID((int)book.PrimaryTitleID);
                    foreach (Title_Identifier identifier in identifiers)
                    {
                        AddGoogleScholarTag(tags, "citation_" + identifier.IdentifierName.ToLower(), identifier.IdentifierValue);
                    }

                    if (book.Pages.Count > 0)
                    {
                        AddGoogleScholarTag(tags, "citation_pdf_url", string.Format(ConfigurationManager.AppSettings["ItemPdfUrl"], book.BookID.ToString()));
                    }

                }
            }

            return tags;
        }

        /// <summary>
        /// Produce a list of key-value pairs that represent the metadata required to allow the
        /// specified segment to be indexed by Google Scholar.
        /// </summary>
        /// <param name="segmentID"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetGoogleScholarMetadataForSegment(int segmentID, string uriFormat)
        {
            List<KeyValuePair<string, string>> tags = new List<KeyValuePair<string, string>>();

            BHLProvider service = new BHLProvider();
            Segment segment = service.SegmentSelectExtended(segmentID);

            if (segment != null)
            {
                // Return no tags for external segments
                if (segment.StartPageID != null && segment.StartPageID > 0)
                {
                    AddGoogleScholarTag(tags, "citation_title", segment.Title);
                    AddGoogleScholarTag(tags, "citation_publication_date", segment.Date);
                    AddGoogleScholarTag(tags, "citation_publisher", segment.PublisherName);
                    AddGoogleScholarTag(tags, "citation_language", segment.LanguageName);
                    AddGoogleScholarTag(tags, "citation_journal_title", segment.ContainerTitle);
                    AddGoogleScholarTag(tags, "citation_volume", segment.Volume);
                    AddGoogleScholarTag(tags, "citation_issue", segment.Issue);
                    AddGoogleScholarTag(tags, "citation_firstpage", segment.StartPageNumber);
                    AddGoogleScholarTag(tags, "citation_lastpage", segment.EndPageNumber);

                    AddGoogleScholarTag(tags, "DC.title", segment.Title);
                    AddGoogleScholarTag(tags, "DC.issued", segment.Date);
                    AddGoogleScholarTag(tags, "DC.publisher", segment.PublisherName);
                    AddGoogleScholarTag(tags, "DC.language", segment.LanguageName);
                    AddGoogleScholarTag(tags, "DC.relation.ispartof", segment.ContainerTitle);
                    AddGoogleScholarTag(tags, "DC.citation.volume", segment.Volume);
                    AddGoogleScholarTag(tags, "DC.citation.issue", segment.Issue);
                    AddGoogleScholarTag(tags, "DC.citation.spage", segment.StartPageNumber);
                    AddGoogleScholarTag(tags, "DC.citation.epage", segment.EndPageNumber);
                    AddGoogleScholarTag(tags, "DC.identifier.URI", string.Format(uriFormat, segment.SegmentID.ToString()));

                    foreach (ItemAuthor author in segment.AuthorList)
                    {
                        AddGoogleScholarTag(tags, "citation_author", author.FullName);
                        AddGoogleScholarTag(tags, "DC.creator", author.FullName);
                    }

                    foreach (ItemKeyword keyword in segment.KeywordList)
                    {
                        AddGoogleScholarTag(tags, "citation_keywords", keyword.Keyword);
                        AddGoogleScholarTag(tags, "DC.subject", keyword.Keyword);
                    }

                    foreach (ItemIdentifier identifier in segment.IdentifierList)
                    {
                        AddGoogleScholarTag(tags, "citation_" + identifier.IdentifierName.ToLower(), identifier.IdentifierValue);
                    }

                    List<ItemIdentifier> dois = service.DOISelectValidForSegment(segmentID);
                    foreach (ItemIdentifier doi in dois)
                    {
                        AddGoogleScholarTag(tags, "citation_doi", doi.IdentifierValue);
                    }

                    if (segment.PageList.Count > 0 && ConfigurationManager.AppSettings["UsePregeneratedPDFs"].ToLower() == "true")
                    {
                        AddGoogleScholarTag(tags, "citation_pdf_url", string.Format(ConfigurationManager.AppSettings["PartPdfUrl"], segment.SegmentID.ToString()));
                    }
                }
            }

            return tags;
        }

        private void AddGoogleScholarTag(List<KeyValuePair<string, string>> tags, string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                tags.Add(new KeyValuePair<string, string>(key, value));
            }
        }

        public List<Tuple<string, string, string>> LinkSelectToExternalContent()
        {
            return new DownloadDAL().LinkSelectToExternalContent(null, null);
        }

        public List<KBART> ExportKBART(string urlRoot)
        {
            return new KBARTDAL().Export(null, null, urlRoot);
        }

        [Serializable]
        public class ViewerPage
        {
            public string ExternalBaseUrl = string.Empty;
            public string AltExternalUrl = string.Empty;
            public string BarCode = string.Empty;
            public string FlickrUrl = string.Empty;
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
