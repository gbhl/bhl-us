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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.OAI2
{
    public class OAI2Publisher
    {
        private String _protocolVersion = "2.0";
        private String _dateGranularity = "YYYY-MM-DDThh:mm:ssZ";

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
            String formats, String maxListSets, String maxListIdentifiers, String maxListRecords)
        {
            InitializeMetadataFormats(formats);

            _baseUrl = baseUrl;
            _repositoryName = repositoryName;
            _adminEmail = adminEmail;
            _identifierNamespace = identifierNamespace;
            _maxListSets = Convert.ToInt32(maxListSets);
            _maxListIdentifiers = Convert.ToInt32(maxListIdentifiers);
            _maxListRecords = Convert.ToInt32(maxListRecords);
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
                    OAIRecord oaiRecord = new OAIRecord(OAI2Util.IncludeExtraDetail(metadataPrefix, _metadataFormats));
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
                String earliestDatestamp = "2006-01-01T00:00:00Z";
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


                CustomDataAccess.CustomGenericList<OAIIdentifier> oaiIDs = null;

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
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        case OAI2Util.SetSpecification.TITLESET:
                            // Select titles
                            oaiIDs = provider.OAIIdentifierSelectTitles(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        /*
                        case OAI2Util.SetSpecification.ARTICLESET:
                            // Select pdfs
                            oaiIDs = provider.OAIIdentifierSelectPDFs(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                         */
                        case OAI2Util.SetSpecification.SEGMENTSET:
                            // Select segments
                            oaiIDs = provider.OAIIdentifierSelectSegments(_maxListIdentifiers,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
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
                    foreach (OAIIdentifier oaiID in oaiIDs)
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


                CustomDataAccess.CustomGenericList<OAIIdentifier> oaiIDs = null;

                if (String.IsNullOrEmpty(errorMessage))
                {
                    BHLProvider provider = new BHLProvider();
                    DateTime? nullDate = null;
                    switch (setSpec)
                    {
                        case OAI2Util.SetSpecification.ITEMSET:
                            // Select items
                            oaiIDs = provider.OAIIdentifierSelectItems(_maxListRecords,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        case OAI2Util.SetSpecification.TITLESET:
                            // Select titles
                            oaiIDs = provider.OAIIdentifierSelectTitles(_maxListRecords,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        /*
                        case OAI2Util.SetSpecification.ARTICLESET:
                            // Select pdfs
                            oaiIDs = provider.OAIIdentifierSelectPDFs(_maxListRecords,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                         */
                        case OAI2Util.SetSpecification.SEGMENTSET:
                            // Select segments
                            oaiIDs = provider.OAIIdentifierSelectSegments(_maxListRecords,
                                (String.IsNullOrEmpty(startAtID) ? 1 : Convert.ToInt32(startAtID)),
                                (String.IsNullOrEmpty(fromDate) ? nullDate : Convert.ToDateTime(fromDate)),
                                (String.IsNullOrEmpty(untilDate) ? nullDate : Convert.ToDateTime(untilDate)));
                            break;
                        default:
                            // Select full list
                            oaiIDs = provider.OAIIdentifierSelectAll(_maxListRecords,
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

                    foreach (OAIIdentifier oaiID in oaiIDs)
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
                        OAIRecord oaiRecord = new OAIRecord(OAI2Util.IncludeExtraDetail(metadataPrefix, _metadataFormats));
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
                    if (oaiIDs.Count == _maxListRecords)
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
                    response.Append("\t\t<setName>Volumes</setName>\n");
                    response.Append("\t</set>\n");
                    //response.Append("\t<set>\n");
                    //response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.ARTICLESET + "</setSpec>\n");
                    //response.Append("\t\t<setName>Article PDF collection</setName>\n");
                    //response.Append("\t</set>\n");
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.TITLESET + "</setSpec>\n");
                    response.Append("\t\t<setName>Monographs/Journals</setName>\n");
                    response.Append("\t</set>\n");
                    response.Append("\t<set>\n");
                    response.Append("\t\t<setSpec>" + OAI2Util.SetSpecification.SEGMENTSET + "</setSpec>\n");
                    response.Append("\t\t<setName>Articles/chapters/treatments/etc</setName>\n");
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
}
