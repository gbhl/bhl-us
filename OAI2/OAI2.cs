#region License

/* Open Source License

Copyright 2005 by Frank McCown
All rights reserved.

Developed by:  Frank McCown
               Old Dominion University
               http://www.cs.odu.edu/~fmccown

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal with the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

         - Redistributions of source code must retain the above copyright
           notice, this list of conditions and the following disclaimers.
         - Redistributions in binary form must reproduce the above copyright
           notice, this list of conditions and the following disclaimers in the
           documentation and/or other materials provided with the distribution.
         - Neither the names of Open Archives Initiative Metadata Harvesting
           Project, Old Dominion University, nor the names of
           its contributors may be used to endorse or promote products derived
           from this Software without specific prior written permission.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
THE CONTRIBUTORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS WITH THE SOFTWARE.

Updated:        Mike Lichtenberg
                Missouri Botanical Garden
                February 12, 2010
                Significant rewrite and update of the original source code.

*/

#endregion License

using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace MOBOT.BHL.OAI2
{
    public class OAI2Publisher
    {
        private String _protocolVersion = "2.0";
        private String _dateGranularity = "YYYY-MM-DD";

        private String _baseUrl = String.Empty;
        private String _repositoryName = String.Empty;
        private String _adminEmail = String.Empty;
        private String _identifierNamespace = String.Empty;
        private List<OAIMetadataFormat> _metadataFormats = new List<OAIMetadataFormat>();

        private int _maxListSets = 500;
        private int _maxListRecords = 100;
        private int _maxListIdentifiers = 200;

        #region Constructors

        public OAI2Publisher(String baseUrl, String repositoryName, String adminEmail, String identifierNamespace, 
            String formats, int maxListSets, int maxListIdentifiers, int maxListRecords)
        {
            InitializeMetadataFormats(formats);

            _baseUrl = baseUrl;
            _repositoryName = repositoryName;
            _adminEmail = adminEmail;
            _identifierNamespace = identifierNamespace;
            _maxListSets = maxListSets;
            _maxListIdentifiers = maxListIdentifiers;
            _maxListRecords = maxListRecords;
        }

        private void InitializeMetadataFormats(String formats)
        {
            // Set up the metadata formats
            String[] formatStrings = formats.Split('\n');

            foreach (String formatString in formatStrings)
            {
                String[] formatSpecs = formatString.Split('|');

                OAIMetadataFormat metadataFormat = new OAIMetadataFormat();
                metadataFormat.MetadataFormat = formatSpecs[0].Trim();
                metadataFormat.MetadataNamespace = formatSpecs[1].Trim();
                metadataFormat.MetadataSchema = formatSpecs[2].Trim();
                metadataFormat.MetadataHandler = formatSpecs[3].Trim();
                metadataFormat.IncludeExtraDetail = (formatSpecs[4].Trim() == "true");
                metadataFormat.MaxListRecords = Convert.ToInt32(formatSpecs[5].Trim());
                _metadataFormats.Add(metadataFormat);
            }
        }

        #endregion Constructors

        #region Request handler

        public String Request(NameValueCollection queryString)
        {
            String errorMessage = String.Empty;
            StringBuilder response = new StringBuilder();

            // OAI header
            response.Append(GetOAIHeader());

            // Call the appropriate method to get the body of the response
            string verb = queryString["verb"];
            if (verb == null)
            {
                errorMessage = "OAI verb is missing";
            }
            else if (verb.Trim().Length == 0)
            {
                errorMessage = "OAI verb is empty";
            }
            else
            {
                verb = verb.Trim();

                switch (verb)
                {
                    case "GetRecord":
                        response.Append(GetRecord(queryString));
                        break;
                    case "Identify":
                        response.Append(Identify(queryString));
                        break;
                    case "ListMetadataFormats":
                        response.Append(ListMetadataFormats(queryString));
                        break;
                    case "ListIdentifiers":
                        response.Append(ListIdentifiers(queryString));
                        break;
                    case "ListRecords":
                        response.Append(ListRecords(queryString));
                        break;
                    case "ListSets":
                        response.Append(ListSets(queryString));
                        break;
                    case "Document":
                        break;
                    default: 
                        errorMessage = "Illegal OAI verb=" + verb;
                        break;
                }
            }

            // Report errors
            if (errorMessage != String.Empty)
            {
                response.Append("<request>" + _baseUrl + "</request>\n<error code=\"badVerb\">" + errorMessage + "</error>\n");
            }

            // OAI footer
            response.Append(GetOAIFooter());

            return response.ToString();
        }

        #endregion Request handler

        #region OAI requests

        private String GetRecord(NameValueCollection args)
        {
            String errorMessage = String.Empty;
            String identifier = String.Empty;
            String metadataPrefix = String.Empty;
            String lastModDate = String.Empty;
            StringBuilder response = new StringBuilder();

            try
            {
                // GetRecord MUST have identifier AND metadataPrefix
                int numKeys = args.Count;
                if (numKeys > 3)
                {
                    errorMessage = @"<error code=""badArgument"">Only the required arguments 'identifier' and 'metadataPrefix' may be given.</error>";
                }

                // Check for identifier
                identifier = args["identifier"];
                if (identifier == null)
                {
                    // Missing identifier
                    errorMessage += @"<error code=""badArgument"">Required argument 'identifier' is missing.</error>";
                }
                else if (identifier.Length == 0)
                {
                    errorMessage += @"<error code=""badArgument"">Content of argument 'identifier' is missing.</error>";
                }
                else if (!OAI2Util.CheckId(identifier))
                {
                    errorMessage += @"<error code=""badArgument"">Identifier '" + identifier + "' is not a valid URI.</error>";
                }

                // Check for metadataPrefix
                metadataPrefix = args["metadataPrefix"];
                if (metadataPrefix == null)
                {
                    // Missing metadataPrefix
                    errorMessage += @"<error code=""badArgument"">Required argument 'metadataPrefix' is missing.</error>";
                }
                else if (metadataPrefix.Length == 0)
                {
                    errorMessage += @"<error code=""badArgument"">Content of argument 'metadataPrefix' is missing.</error>";
                }
                else if (!OAI2Util.CheckMetadataPrefix(metadataPrefix, _metadataFormats))
                {
                    errorMessage += @"<error code=""cannotDisseminateFormat"">'" + metadataPrefix + "' is not a valid metadataPrefix.</error>";
                }
                else
                {
                    // Must have valid identifier and metadataPrefix
                    errorMessage = OAI2Util.VerifyIdentifier(identifier, _identifierNamespace, String.Empty, out lastModDate);
                }


                // Argument verification complete.  Build the response.
                response.Append("<request verb=\"GetRecord\"");
                if (metadataPrefix != String.Empty) 
                { 
                    response.Append(" metadataPrefix=\"" + metadataPrefix +"\"");
                }
                if (identifier != String.Empty) 
                { 
                    response.Append(" identifier=\"" + identifier + "\"");
                }
                response.Append(">\n");
                response.Append(_baseUrl + "\n");
                response.Append("</request>\n");

                if (errorMessage != String.Empty) {
            	    response.Append(errorMessage);
                }
                else 
                {
                    response.Append("<GetRecord>\n");
                    response.Append("\t<record>\n");

                    // Add the header information to the output
                    String scheme, namespaceIdentifier, localIdentifier;
                    OAI2Util.ParseOAIIdentifier(identifier, out scheme, out namespaceIdentifier, out localIdentifier);

                    response.Append("\t\t<header>\n");
                    response.Append("\t\t\t<identifier>" + identifier + "</identifier>\n");
                    response.Append("\t\t\t<datestamp>" + OAI2Util.GetDate(lastModDate.ToString()) + "</datestamp>\n"); 
                    response.Append("\t\t\t<setSpec>" + localIdentifier.Split('/')[0] + "</setSpec>\n");
                    response.Append("\t\t</header>\n");

                    // Add the record metadata to the output
                    response.Append("\t\t<metadata>\n");
                    OAI2.OAIRecord oaiRecord = new OAI2.OAIRecord(OAI2Util.IncludeExtraDetail(metadataPrefix, _metadataFormats));
                    if (oaiRecord.Load(identifier))
                    {
                        response.Append(new OAIMetadataFactory(metadataPrefix, _metadataFormats).GetMetadata(oaiRecord));
                    }
                    else
                    {
                        throw new Exception(oaiRecord.ErrorMessage);
                    }
                    response.Append("\t\t</metadata>\n");
                    response.Append("\t</record>\n");
                    response.Append("</GetRecord>");
                }
            }
            catch (Exception ex)
            {
                response = new StringBuilder();
                response.Append(GetOAIErrorResponse("GetRecord", ex.Message));
            }

            return response.ToString();
        }

        private String Identify(NameValueCollection args)
        {
            StringBuilder response = new StringBuilder();

            try
            {
                // Should read datestamps and identifiers from the database, but hardcode them for now
                String earliestDatestamp = "2006-01-01";
                String deletedRecord = "no";
                String sampleIdentifier = "oai:" + _identifierNamespace + ":item/1000";

                // Build the body of the response
                response.Append("<request verb=\"Identify\">");
                response.Append(_baseUrl);
                response.Append("</request>\n");
                response.Append("<Identify>\n");
                response.Append("\t<repositoryName>");
                response.Append(_repositoryName);
                response.Append("</repositoryName>\n");
                response.Append("\t<baseURL>");
                response.Append(_baseUrl);
                response.Append("</baseURL>\n");
                response.Append("\t<protocolVersion>");
                response.Append(_protocolVersion);
                response.Append("</protocolVersion>\n");
                response.Append("\t<adminEmail>");
                response.Append(_adminEmail);
                response.Append("</adminEmail>\n");
                response.Append("\t<earliestDatestamp>");
                response.Append(earliestDatestamp);
                response.Append("</earliestDatestamp>\n");
                response.Append("\t<deletedRecord>");
                response.Append(deletedRecord);
                response.Append("</deletedRecord>\n");
                response.Append("\t<granularity>");
                response.Append(_dateGranularity);
                response.Append("</granularity>\n");
                response.Append("\t<description>\n");
                response.Append("\t\t<oai-identifier xmlns=\"http://www.openarchives.org/OAI/2.0/oai-identifier\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
                response.Append("\t\txsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai-identifier http://www.openarchives.org/OAI/2.0/oai-identifier.xsd\">\n");
                response.Append("\t\t\t<scheme>oai</scheme>\n");
                response.Append("\t\t\t<repositoryIdentifier>");
                response.Append(_identifierNamespace);
                response.Append("</repositoryIdentifier>\n");
                response.Append("\t\t\t<delimiter>:</delimiter>\n");
                response.Append("\t\t\t<sampleIdentifier>");
                response.Append(sampleIdentifier);
                response.Append("</sampleIdentifier>\n");
                response.Append("\t\t</oai-identifier>\n");
                response.Append("\t</description>\n");
                response.Append("</Identify>\n");
            }
            catch (Exception ex)
            {
                response = new StringBuilder();
                response.Append(GetOAIErrorResponse("Identify", ex.Message));
            }

            return response.ToString();
        }

        private String ListIdentifiers(NameValueCollection args)
        {
            String errorMessage = String.Empty;
            String startAtID = String.Empty;
            String startAtSet = String.Empty;
            System.Text.StringBuilder identifierList = new StringBuilder();
            StringBuilder response = new StringBuilder();

            try
            {
                // Verify all parameters
                String resumptionToken, fromDate, untilDate, setSpec, metadataPrefix;
                errorMessage = OAI2Util.VerifyListDataParameters(args, _metadataFormats,  out resumptionToken, 
                    out fromDate, out untilDate, out setSpec, out metadataPrefix);

                // Get the list of identifiers.

                if (!String.IsNullOrEmpty(resumptionToken) && String.IsNullOrEmpty(errorMessage))
                {
                    // This is a "resumed" query, so get the starting point

                    // Use 12/31/2111 as far-off date for expiration
                    // NOTE: This should probably be an off-set from the current time
                    errorMessage = OAI2Util.ParseResumptionTokenWithOaiId("12/31/2111", resumptionToken,
                        out untilDate, out fromDate, out setSpec, out metadataPrefix, out startAtID, 
                        out startAtSet);
                }


                List<DataObjects.OAIIdentifier> oaiIDs = null;

                if (String.IsNullOrEmpty(errorMessage))
                {
                    BHLProvider provider = new BHLProvider();
                    DateTime? nullDate = null;
                    switch (setSpec)
                    {
                        case OAI2Util.SetSpecification.ITEMSET:
                            // Select items
                            oaiIDs = provider.OAIIdentifierSelectItems(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                1, 0);
                            break;
                        case OAI2Util.SetSpecification.ITEMEXTSET:
                            // Select external items
                            oaiIDs = provider.OAIIdentifierSelectItems(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                0, 1);
                            break;
                        case OAI2Util.SetSpecification.TITLESET:
                            // Select titles
                            oaiIDs = provider.OAIIdentifierSelectTitles(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        case OAI2Util.SetSpecification.SEGMENTSET:
                            // Select segments
                            oaiIDs = provider.OAIIdentifierSelectSegments(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                1, 0);
                            break;
                        case OAI2Util.SetSpecification.SEGMENTEXTSET:
                            // Select external segments
                            oaiIDs = provider.OAIIdentifierSelectSegments(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                0, 1);
                            break;
                        default:
                            // Select full list
                            oaiIDs = provider.OAIIdentifierSelectAll(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (startAtSet ?? String.Empty),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                    }
                }

                if (oaiIDs == null && String.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = @"<error code=""noRecordsMatch"">No records match the given set, from, or until parameters.</error>";
                }
                else if (String.IsNullOrEmpty(errorMessage))
                {
                    foreach (DataObjects.OAIIdentifier oaiID in oaiIDs)
                    {
                        string date = OAI2Util.GetDate(oaiID.LastModifiedDate.ToString());

                        identifierList.Append("\t\t<header>\n");
                        identifierList.Append("\t\t\t<identifier>oai:" + _identifierNamespace + ":" + oaiID.SetSpec + "/" + oaiID.Id.ToString() + "</identifier>\n");
                        identifierList.Append("\t\t\t<datestamp>" + date + "</datestamp>\n");
                        identifierList.Append("\t\t\t<setSpec>" + oaiID.SetSpec + "</setSpec>\n");
                        identifierList.Append("\t\t</header>\n");
                    }

                    // Produce a resumption token if not all records were presented
                    if (oaiIDs.Count == _maxListIdentifiers)
                    {
                        string nextStartAtID = oaiIDs[oaiIDs.Count - 1].Id.ToString();
                        string nextStartAtSet = oaiIDs[oaiIDs.Count - 1].SetSpec;
                        identifierList.Append(OAI2Util.BuildResumptionTokenWithOaiId(untilDate, fromDate, setSpec,
                            metadataPrefix, nextStartAtID, nextStartAtSet, OAI2Util.NowUTC(), "", "", ""));
                    }
                }


                // Build the final response
                response.Append("<request verb=\"ListIdentifiers\"");
                if (!String.IsNullOrEmpty(metadataPrefix)) 
                { 
                    response.Append(" metadataPrefix=\"" + metadataPrefix + "\"");
                } 
                if (!String.IsNullOrEmpty(resumptionToken)) 
                { 
                    response.Append(" resumptionToken=\"" + resumptionToken + "\"");
                }
                if (!String.IsNullOrEmpty(fromDate))
                { 
                    response.Append(" from=\"" + fromDate + "\"");
                }
                if (!String.IsNullOrEmpty(untilDate)) 
                { 
                    response.Append(" until=\"" + untilDate + "\"");
                }
                if (!String.IsNullOrEmpty(setSpec)) 
                { 
                    response.Append(" set=\"" + setSpec + "\"");
                }
                response.Append(">\n");
                response.Append(_baseUrl + "\n");
                response.Append("</request>\n");

                if (!String.IsNullOrEmpty(errorMessage))
                {
                    response.Append(errorMessage);
                }
                else
                {
                    response.Append("<ListIdentifiers>\n");
                    response.Append(identifierList.ToString() + "\n");
                    response.Append("</ListIdentifiers>");
                }
            }
            catch (Exception ex)
            {
                response = new StringBuilder();
                response.Append(GetOAIErrorResponse("ListIdentifiers", ex.Message));
            }

            return response.ToString();
        }

        private String ListMetadataFormats(NameValueCollection args)
        {
            String errorMessage = String.Empty;
            String identifier = String.Empty;
            StringBuilder response = new StringBuilder();

            try
            {
                // See if an identifier was specified.  If so, verify it.
                int numKeys = args.Count;
                if (numKeys == 2)
                {
                    // One of these must be the identifier
                    identifier = args["identifier"];
                    if (identifier == null)
                    {
                        // Must be something besides "identifier" and "verb"
                        errorMessage = @"<error code=""badArgument"">Illegal argument.  Only identifier may be supplied with verb.</error>";
                    }
                    else if (identifier.Length == 0)
                    {
                        errorMessage = @"<error code=""badArgument"">Content of argument 'identifier' is missing.</error>";
                    }
                    else if (!OAI2Util.CheckId(identifier))
                    {
                        errorMessage = @"<error code=""badArgument"">Identifier '" + identifier + "' is not a valid URI.</error>";
                    }
                    else
                    {
                        string lastModDate;
                        errorMessage = OAI2Util.VerifyIdentifier(identifier, _identifierNamespace, String.Empty, out lastModDate);
                    }
                }
                else if (numKeys > 2)
                {
                    // Additional garbage was supplied
                    errorMessage = @"<error code=""badArgument"">Illegal argument.  Only identifier may be supplied with verb.</error>";
                }

                // Argument verification complete.  Build the response.
                response.Append("<request verb=\"ListMetadataFormats\"");
                if (identifier != String.Empty)
                {
                    response.Append(" identifier=\"" + identifier + "\"");
                }
                response.Append(">\n");
                response.Append(_baseUrl + "\n");
                response.Append("</request>\n");

                if (errorMessage != String.Empty)
                {
                    response.Append(errorMessage + "\n");
                }
                else
                {
                    response.Append("<ListMetadataFormats>\n");

                    foreach (OAIMetadataFormat metadataFormat in _metadataFormats)
                    {
                        response.Append("\t<metadataFormat>\n");
                        response.Append("\t\t<metadataPrefix>" + metadataFormat.MetadataFormat + "</metadataPrefix>\n");
                        response.Append("\t\t<schema>" + metadataFormat.MetadataSchema + "</schema>\n");
                        response.Append("\t\t<metadataNamespace>" + metadataFormat.MetadataNamespace + "</metadataNamespace>\n");
                        response.Append("\t</metadataFormat>\n");
                    }

                    response.Append("</ListMetadataFormats>");
                }
            }
            catch (Exception ex)
            {
                response = new StringBuilder();
                response.Append(GetOAIErrorResponse("ListMetadataFormats", ex.Message));
            }

            return response.ToString();
        }

        private String ListRecords(NameValueCollection args)
        {
            String errorMessage = String.Empty;
            String startAtID = String.Empty;
            String startAtSet = String.Empty;
            System.Text.StringBuilder recordList = new StringBuilder();
            StringBuilder response = new StringBuilder();

            try
            {
                // Verify all parameters
                String resumptionToken, fromDate, untilDate, setSpec, metadataPrefix;
                errorMessage = OAI2Util.VerifyListDataParameters(args, _metadataFormats, out resumptionToken, 
                    out fromDate, out untilDate, out setSpec, out metadataPrefix);

                // Get the list of identifiers.

                if (!String.IsNullOrEmpty(resumptionToken) && String.IsNullOrEmpty(errorMessage))
                {
                    // This is a "resumed" query, so get the starting point

                    // Use 12/31/2111 as far-off date for expiration
                    // NOTE: This should probably be an off-set from the current time
                    errorMessage = OAI2Util.ParseResumptionTokenWithOaiId("12/31/2111", resumptionToken,
                        out untilDate, out fromDate, out setSpec, out metadataPrefix, out startAtID, 
                        out startAtSet);
                }


                List<DataObjects.OAIIdentifier> oaiIDs = null;

                if (String.IsNullOrEmpty(errorMessage))
                {
                    BHLProvider provider = new BHLProvider();
                    DateTime? nullDate = null;
                    switch (setSpec)
                    {
                        case OAI2Util.SetSpecification.ITEMSET:
                            // Select items
                            oaiIDs = provider.OAIIdentifierSelectItems(OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats),
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                1, 0);
                            break;
                        case OAI2Util.SetSpecification.ITEMEXTSET:
                            // Select external items
                            oaiIDs = provider.OAIIdentifierSelectItems(OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats),
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                0, 1);
                            break;
                        case OAI2Util.SetSpecification.TITLESET:
                            // Select titles
                            oaiIDs = provider.OAIIdentifierSelectTitles(OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats),
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        case OAI2Util.SetSpecification.SEGMENTSET:
                            // Select segments
                            oaiIDs = provider.OAIIdentifierSelectSegments(OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats),
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                1, 0);
                            break;
                        case OAI2Util.SetSpecification.SEGMENTEXTSET:
                            // Select external segments
                            oaiIDs = provider.OAIIdentifierSelectSegments(OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats),
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)),
                                0, 1);
                            break;
                        default:
                            // Select full list
                            oaiIDs = provider.OAIIdentifierSelectAll(OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats),
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (startAtSet ?? String.Empty),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                    }
                }
                
                if (oaiIDs == null && String.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = @"<error code=""noRecordsMatch"">No records match the given set, from, or until parameters.</error>";
                }
                else if (String.IsNullOrEmpty(errorMessage))
                {
                    OAIMetadataFactory metadataFactory = new OAIMetadataFactory(metadataPrefix, _metadataFormats);

                    foreach (DataObjects.OAIIdentifier oaiID in oaiIDs)
                    {
                        String date = OAI2Util.GetDate(oaiID.LastModifiedDate.ToString());
                        String oaiIDString = "oai:" + _identifierNamespace + ":" + oaiID.SetSpec + "/" + oaiID.Id.ToString();

                        recordList.Append("\t\t<record>\n");

                        recordList.Append("\t\t<header>\n");
                        recordList.Append("\t\t\t<identifier>" + oaiIDString + "</identifier>\n");
                        recordList.Append("\t\t\t<datestamp>" + date + "</datestamp>\n");
                        recordList.Append("\t\t\t<setSpec>" + oaiID.SetSpec + "</setSpec>\n");
                        recordList.Append("\t\t</header>\n");

                        recordList.Append("\t\t<metadata>\n");
                        OAI2.OAIRecord oaiRecord = new OAI2.OAIRecord(OAI2Util.IncludeExtraDetail(metadataPrefix, _metadataFormats));
                        if (oaiRecord.Load(oaiIDString))
                        {
                            recordList.Append(metadataFactory.GetMetadata(oaiRecord));
                        }
                        else
                        {
                            throw new Exception(oaiRecord.ErrorMessage);
                        }
                        recordList.Append("\t\t</metadata>\n");

                        recordList.Append("\t\t</record>\n");
                    }

                    // Produce a resumption token if not all records were presented
                    if (oaiIDs.Count == OAI2Util.GetMaxListRecords(metadataPrefix, _metadataFormats))
                    {
                        string nextStartAtID = oaiIDs[oaiIDs.Count - 1].Id.ToString();
                        string nextStartAtSet = oaiIDs[oaiIDs.Count - 1].SetSpec;
                        recordList.Append(OAI2Util.BuildResumptionTokenWithOaiId(untilDate, fromDate, setSpec,
                            metadataPrefix, nextStartAtID, nextStartAtSet, OAI2Util.NowUTC(), "", "", ""));
                    }
                }


                // Build the final response
                response.Append("<request verb=\"ListRecords\"");
                if (!String.IsNullOrEmpty(metadataPrefix))
                {
                    response.Append(" metadataPrefix=\"" + metadataPrefix + "\"");
                }
                if (!String.IsNullOrEmpty(resumptionToken))
                {
                    response.Append(" resumptionToken=\"" + resumptionToken + "\"");
                }
                if (!String.IsNullOrEmpty(fromDate))
                {
                    response.Append(" from=\"" + fromDate + "\"");
                }
                if (!String.IsNullOrEmpty(untilDate))
                {
                    response.Append(" until=\"" + untilDate + "\"");
                }
                if (!String.IsNullOrEmpty(setSpec))
                {
                    response.Append(" set=\"" + setSpec + "\"");
                }
                response.Append(">\n");
                response.Append(_baseUrl + "\n");
                response.Append("</request>\n");

                if (!String.IsNullOrEmpty(errorMessage))
                {
                    response.Append(errorMessage);
                }
                else
                {
                    response.Append("<ListRecords>\n");
                    response.Append(recordList.ToString() + "\n");
                    response.Append("</ListRecords>");
                }
            }
            catch (Exception ex)
            {
                response = new StringBuilder();
                response.Append(GetOAIErrorResponse("ListRecords", ex.Message));
            }

            return response.ToString();
        }

        private String ListSets(NameValueCollection args)
        {
            string errorMessage = String.Empty;
            StringBuilder response = new StringBuilder();

            try
            {

                // Check for resumptionToken
                String resumptionToken = args["resumptionToken"];
                if (resumptionToken != null)
                {
                    if (resumptionToken.Length == 0)
                    {
                        errorMessage += @"<error code=""badArgument"">Content of argument 'resumptionToken' is missing.</error>";
                    }
                    if (args.Count > 2)
                    {
                        errorMessage += @"<error code=""badArgument"">'resumptionToken' must be the only parameter.</error>";
                    }
                }
                else if (args.Count > 1)
                {
                    errorMessage += @"<error code=""badArgument"">Illegal arguments given.</error>";
                }


                // Argument verification complete.  Build the response.
                response.Append("<request verb=\"ListSets\"");
                if (resumptionToken != null)
                {
                    response.Append(" resumptionToken=\"" + resumptionToken + "\"");
                }
                response.Append(">\n");
                response.Append(_baseUrl + "\n");
                response.Append("</request>\n");

                if (errorMessage != String.Empty)
                {
                    response.Append(errorMessage + "\n");
                }
                else
                {
                    response.Append("<ListSets>\n");

                    /*
                    foreach (Set set in sets)
                    {
                        response.Append("\t<set>\n");
                        response.Append("\t\t<setSpec>" + set.spec + "</setSpec>\n");
                        response.Append("\t\t<setName>" + set.Name + "</setName>\n");
                        response.Append("\t</set>\n");
                    }
                    response.Append(OAI2Util.BuildResumptionTokenWithOaiId("", "",
                        "", "", "501", OAI2Util.NowUTC(), "", "", ""));
                    */

                    // HARDCODE sets for now  (Feb 18 1010)
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.ITEMSET + "</setSpec>\n");
                    response.Append("\t\t<setName>BHL-hosted volumes</setName>\n");
                    response.Append("\t\t<setDescription>\n");
                    response.Append("\t\t\t<oai_dc:dc\n"); 
                    response.Append("\t\t\t\txmlns:oai_dc=\"http://www.openarchives.org/OAI/2.0/oai_dc/\"\n");
                    response.Append("\t\t\t\txmlns:dc=\"http://purl.org/dc/elements/1.1/\"\n");
                    response.Append("\t\t\t\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
                    response.Append("\t\t\t\txsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai_dc/ http://www.openarchives.org/OAI/2.0/oai_dc.xsd\">\n");
                    response.Append("\t\t\t\t<dc:description>This set contains individual volumes hosted by BHL.  The content is viewable in BHL.</dc:description>\n");
                    response.Append("\t\t\t</oai_dc:dc>\n");
                    response.Append("\t\t</setDescription>\n");
                    response.Append("\t</set>\n");
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.ITEMEXTSET + "</setSpec>\n");
                    response.Append("\t\t<setName>External volumes</setName>\n");
                    response.Append("\t\t<setDescription>\n");
                    response.Append("\t\t\t<oai_dc:dc\n");
                    response.Append("\t\t\t\txmlns:oai_dc=\"http://www.openarchives.org/OAI/2.0/oai_dc/\"\n");
                    response.Append("\t\t\t\txmlns:dc=\"http://purl.org/dc/elements/1.1/\"\n");
                    response.Append("\t\t\t\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
                    response.Append("\t\t\t\txsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai_dc/ http://www.openarchives.org/OAI/2.0/oai_dc.xsd\">\n");
                    response.Append("\t\t\t\t<dc:description>This set contains individual volumes not hosted by BHL.  The content must be viewed on a site not maintained by BHL.</dc:description>\n");
                    response.Append("\t\t\t</oai_dc:dc>\n");
                    response.Append("\t\t</setDescription>\n");
                    response.Append("\t</set>\n");
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.TITLESET + "</setSpec>\n");
                    response.Append("\t\t<setName>Monographs/Journals</setName>\n");
                    response.Append("\t\t<setDescription>\n");
                    response.Append("\t\t\t<oai_dc:dc\n");
                    response.Append("\t\t\t\txmlns:oai_dc=\"http://www.openarchives.org/OAI/2.0/oai_dc/\"\n");
                    response.Append("\t\t\t\txmlns:dc=\"http://purl.org/dc/elements/1.1/\"\n");
                    response.Append("\t\t\t\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
                    response.Append("\t\t\t\txsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai_dc/ http://www.openarchives.org/OAI/2.0/oai_dc.xsd\">\n");
                    response.Append("\t\t\t\t<dc:description>This set contains the monographs and journals represented in BHL.</dc:description>\n");
                    response.Append("\t\t\t</oai_dc:dc>\n");
                    response.Append("\t\t</setDescription>\n");
                    response.Append("\t</set>\n");
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.SEGMENTSET + "</setSpec>\n");
                    response.Append("\t\t<setName>BHL-hosted articles/chapters/treatments/etc</setName>\n");
                    response.Append("\t\t<setDescription>\n");
                    response.Append("\t\t\t<oai_dc:dc\n");
                    response.Append("\t\t\t\txmlns:oai_dc=\"http://www.openarchives.org/OAI/2.0/oai_dc/\"\n");
                    response.Append("\t\t\t\txmlns:dc=\"http://purl.org/dc/elements/1.1/\"\n");
                    response.Append("\t\t\t\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
                    response.Append("\t\t\t\txsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai_dc/ http://www.openarchives.org/OAI/2.0/oai_dc.xsd\">\n");
                    response.Append("\t\t\t\t<dc:description>This set contains articles/chapters/treatments/etc hosted by BHL.  The content is viewable in BHL.</dc:description>\n");
                    response.Append("\t\t\t</oai_dc:dc>\n");
                    response.Append("\t\t</setDescription>\n");
                    response.Append("\t</set>\n");
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.SEGMENTEXTSET + "</setSpec>\n");
                    response.Append("\t\t<setName>External articles/chapters/treatments/etc</setName>\n");
                    response.Append("\t\t<setDescription>\n");
                    response.Append("\t\t\t<oai_dc:dc\n");
                    response.Append("\t\t\t\txmlns:oai_dc=\"http://www.openarchives.org/OAI/2.0/oai_dc/\"\n");
                    response.Append("\t\t\t\txmlns:dc=\"http://purl.org/dc/elements/1.1/\"\n");
                    response.Append("\t\t\t\txmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
                    response.Append("\t\t\t\txsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai_dc/ http://www.openarchives.org/OAI/2.0/oai_dc.xsd\">\n");
                    response.Append("\t\t\t\t<dc:description>This set contains articles/chapters/treatments/etc not hosted by BHL.  The content must be viewed on a site not maintained by BHL.</dc:description>\n");
                    response.Append("\t\t\t</oai_dc:dc>\n");
                    response.Append("\t\t</setDescription>\n");
                    response.Append("\t</set>\n");

                    response.Append("</ListSets>");
                }
            }
            catch (Exception ex)
            {
                response = new StringBuilder();
                response.Append(GetOAIErrorResponse("ListSets", ex.Message));
            }

            return response.ToString();
        }

        #endregion OAI requests

        #region Supporting methods

        private String GetOAIHeader()
        {
            String header = String.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append("<OAI-PMH\n");
            sb.Append("xmlns=\"http://www.openarchives.org/OAI/2.0/\"\n");
            sb.Append("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
            sb.Append("xsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/ http://www.openarchives.org/OAI/2.0/OAI-PMH.xsd\">\n");
            sb.Append("<responseDate>" + OAI2Util.NowUTC() + "</responseDate>\n");

            return sb.ToString();
        }

        private String GetOAIFooter()
        {
            return "</OAI-PMH>";
        }

        private String GetOAIErrorResponse(String verb, String message)
        {
            StringBuilder error = new StringBuilder();
            error.Append("<request verb=\"" + verb + "\">\n");
            error.Append("\t" + _baseUrl + "\n");
            error.Append("</request>\n");
            error.Append("<ApplicationError>\n");
            error.Append("\t" + message + "\n");
            error.Append("</ApplicationError>\n");
            return error.ToString();
        }

        #endregion Supporting methods
    }

    public class OAI2Harvester
    {
        private string _baseUrl = string.Empty;
        private string _userAgent = string.Empty; // BHL OAI Harvester
        private string _fromEmail = string.Empty; // biodiversitylibrary@gmail.com
        private List<OAIMetadataFormat> _metadataFormats = new List<OAIMetadataFormat>();

        #region Constructors

        public OAI2Harvester(string baseUrl, string userAgent, string fromEmail, Dictionary<string, string> formats)
        {
            InitializeMetadataFormats(formats);

            _baseUrl = baseUrl;
            _userAgent = userAgent;
            _fromEmail = fromEmail;
        }

        private void InitializeMetadataFormats(Dictionary<string, string> formats)
        {
            // Set up the metadata formats
            foreach (KeyValuePair<string, string> format in formats)
            {
                OAIMetadataFormat metadataFormat = new OAIMetadataFormat();
                metadataFormat.MetadataFormat = format.Key;     // prefix
                metadataFormat.MetadataHandler = format.Value;  // assembly name
                _metadataFormats.Add(metadataFormat);
            }
        }

        #endregion Constructors

        #region OAI requests

        /// <summary>
        /// Submits a Identify command to the OAI repository
        /// </summary>
        /// <returns>An OAIHarvestResult object containing a OAIIdentity object</returns>
        public OAIHarvestResult Identify()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListSets command to the OAI repository.
        /// </summary>
        /// <returns>An OAIHarvestResult object containing a list of OAISet objects</returns>
        public OAIHarvestResult ListSets()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListSets command to the OAI repository.
        /// </summary>
        /// <param name="resumptionToken"></param>
        /// <returns>An OAIHarvestResult object containing a list of OAISet objects</returns>
        public OAIHarvestResult ListSets(string resumptionToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListMetadataFormats command to the OAI repository.
        /// </summary>
        /// <returns>An OAIHarvestResult object containing a list of OAIMetadataFormats</returns>
        public OAIHarvestResult ListMetadataFormats()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListMetadataFormats command to the OAI repository.
        /// </summary>
        /// <returns>An OAIHarvestResult object containing a list of OAIMetadataFormats</returns>
        public OAIHarvestResult ListMetadataFormats(string identifier)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListIdentifiers command to the OAI repository.
        /// </summary>
        /// <param name="metadataFormat"></param>
        /// <param name="set"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <returns>An OAIHarvestResult class containing a list of OAIIdentifiers</returns>
        public OAIHarvestResult ListIdentifiers(string metadataFormat, string set = "", string from = "", string until = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListIdentifiers command to the OAI repository.
        /// </summary>
        /// <param name="resumptionToken"></param>
        /// <returns>An OAIHarvestResult class containing a list of OAIIdentifiers</returns>
        public OAIHarvestResult ListIdentifiers(string resumptionToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submits a ListRecords command to the OAI repository.
        /// </summary>
        /// <param name="metadataFormat"></param>
        /// <param name="set"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <returns>An OAIHarvestResult object containing a list of OAIRecords</returns>
        public OAIHarvestResult ListRecords(string metadataFormat, string set = "", string from = "", string until = "")
        {
            return ListRecords(metadataFormat, set, from, until, string.Empty);
        }

        /// <summary>
        /// Submits a ListRecords command to the OAI repository.
        /// </summary>
        /// <param name="resumptionToken"></param>
        /// <returns>An OAIHarvestResult object containing a list of OAIRecords</returns>
        public OAIHarvestResult ListRecords(string metadataFormat, string resumptionToken)
        {
            return ListRecords(metadataFormat, string.Empty, string.Empty, string.Empty, resumptionToken);
        }

        private OAIHarvestResult ListRecords(string metadataFormat, string set, string from, string until, string resumptionToken)
        {
            OAIHarvestResult harvestResult = new OAIHarvestResult();
            string url = string.Empty;
            DateTime responseDate = DateTime.Now.ToUniversalTime();
            string responseMessage = "ok";
            string newResumptionToken = string.Empty;
            DateTime resumptionExpiration = DateTime.Now.ToUniversalTime().AddHours(1);
            int completeListSize = 0;
            int cursor = 0;
            List<OAI2.OAIRecord> content = new List<OAI2.OAIRecord>();

            // 1. Build the URL
            if (!string.IsNullOrWhiteSpace(resumptionToken))
            {
                url = string.Format("{0}?verb=ListRecords&resumptionToken={1}", _baseUrl, HttpUtility.UrlEncode(resumptionToken));
            }
            else
            {
                url = string.Format("{0}?verb=ListRecords&metadataPrefix={1}", _baseUrl, HttpUtility.UrlEncode(metadataFormat));
                if (!string.IsNullOrWhiteSpace(set)) url += "&set=" + HttpUtility.UrlEncode(set);
                if (!string.IsNullOrWhiteSpace(from)) url += "&from=" + HttpUtility.UrlEncode(from);
                if (!string.IsNullOrWhiteSpace(until)) url += "&until=" + HttpUtility.UrlEncode(until);
            }

            // 2. Make the request
            try
            {
                XDocument response = SubmitRequest(url);
                XElement root = response.Root;
                XNamespace ns = root.Name.Namespace;

                // 3. Parse the response metadata (date, resumption token, list size, etc)
                XElement error = root.Element(ns + "error");
                responseDate = Convert.ToDateTime(root.Element(ns + "responseDate").Value);
                if (error != null)
                {
                    XAttribute errorCode = error.Attribute("code");
                    responseMessage = (errorCode != null) ? errorCode.Value + ": " : string.Empty;
                    responseMessage += error.Value;
                }
                else
                {
                    // Change the "root" of the document we're evaluating to be the "ListRecords" element
                    root = root.Element(ns + "ListRecords");

                    XElement resumptionTokenElement = root.Element(ns + "resumptionToken");
                    if (resumptionTokenElement != null)
                    {
                        newResumptionToken = resumptionTokenElement.Value;
                        if (resumptionTokenElement.Attribute("expirationDate") != null)
                        {
                            resumptionExpiration = Convert.ToDateTime(resumptionTokenElement.Attribute("expirationDate").Value).ToUniversalTime();
                        }
                        if (resumptionTokenElement.Attribute("completeListSize") != null)
                        {
                            completeListSize = Convert.ToInt32(resumptionTokenElement.Attribute("completeListSize").Value);
                        }
                        if (resumptionTokenElement.Attribute("cursor") != null)
                        {
                            cursor = Convert.ToInt32(resumptionTokenElement.Attribute("cursor").Value);
                        }
                    }

                    // 4. Parse the returned records
                    IEnumerable<XElement> records = from r in root.Descendants(ns + "record") select r;

                    foreach (XElement record in records)
                    {
                        XElement header = record.Element(ns + "header");

                        string oaiStatus = string.Empty;
                        if (header.Attribute("status") != null) oaiStatus = header.Attribute("status").Value;

                        OAI2.OAIRecord oaiRecord = null;
                        if (oaiStatus == "deleted")
                        {
                            // No metadata exists for deleted records, so just create an empty
                            // OAIRecord object to hold the OAI header information
                            oaiRecord = new OAI2.OAIRecord();
                        }
                        else
                        {
                            // Harvest the metadata for this record
                            string metadataString = record.Element(ns + "metadata").FirstNode.ToString();
                            oaiRecord = new OAIMetadataFactory(metadataFormat, _metadataFormats).GetMetadata(metadataString);
                        }

                        // Capture the OAI header info
                        oaiRecord.OaiStatus = oaiStatus;
                        oaiRecord.OaiIdentifier = header.Element(ns + "identifier").Value;
                        oaiRecord.OaiDateStamp = header.Element(ns + "datestamp").Value;

                        content.Add(oaiRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                completeListSize = 0;
                cursor = 0;
                content = null;
                responseDate = DateTime.Now.ToUniversalTime();
                responseMessage = ex.Message;
                resumptionExpiration = DateTime.Now.ToUniversalTime().AddHours(1);
                newResumptionToken = string.Empty;
            }

            // 5. Return the OAIHarvestResult
            harvestResult.CompleteListSize = completeListSize;
            harvestResult.Cursor = cursor;
            harvestResult.Content = content;
            harvestResult.ResponseDate = responseDate;
            harvestResult.ResponseMessage = responseMessage;
            harvestResult.ResumptionExpiration = resumptionExpiration;
            harvestResult.ResumptionToken = newResumptionToken;
            return harvestResult;
        }

        /// <summary>
        /// Submits a GetRecord command to the OAI respository.
        /// </summary>
        /// <param name="metadataFormat"></param>
        /// <param name="identifier"></param>
        /// <returns>An OAIHarvestResult object containing an OAIRecord object</returns>
        public OAIHarvestResult GetRecord(string metadataFormat, string identifier)
        {
            throw new NotImplementedException();
        }

        #endregion OAI requests

        #region Supporting methods

        /// <summary>
        /// Submit the OAI request, retrying up to three times on failure.
        /// </summary>
        /// <param name="url"></param>
        private XDocument SubmitRequest(string url)
        {
            int retryLimit = 3;
            string response = string.Empty;

            int retry = 1;
            while (retry <= retryLimit)
            {
                try
                {
                    response = HttpRequest(url, "GET");
                    break;
                }
                catch
                {
                    if (retry >= retryLimit) throw;
                }
                retry++;
            }

            return XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(response)));
        }

        private string HttpRequest(string url, string method)
        {
            StringBuilder sb = new StringBuilder();
            StreamReader reader = null;

            try
            {
                // Prepare the web request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = method;
                req.Timeout = 300000;    // 5 minutes
                req.UserAgent = _userAgent;
                req.Headers.Add("From", _fromEmail);

                // Make sure we were successful
                using (WebResponse webresponse = req.GetResponse())
                {
                    HttpWebResponse response = (HttpWebResponse)webresponse;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Error getting response from " + url + ".  HTTP status: " + response.StatusCode.ToString() + "\r\n");
                    }
                    else
                    {
                        // Read the response
                        reader = new StreamReader((Stream)response.GetResponseStream());

                        Char[] read = new Char[256];
                        int count = reader.Read(read, 0, 256);
                        while (count > 0)
                        {
                            sb.Append(new string(read, 0, count));
                            count = reader.Read(read, 0, 256);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return sb.ToString();
        }

        #endregion Supporting methods
    }
}
