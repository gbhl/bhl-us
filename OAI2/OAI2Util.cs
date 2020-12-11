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

using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;

namespace MOBOT.BHL.OAI2
{
    internal class OAI2Util
    {
        public class SetSpecification
        {
            public const String ITEMSET = "item";
            public const String ITEMEXTSET = "itemexternal";
            public const String TITLESET = "title";
            public const String SEGMENTSET = "part";
            public const String SEGMENTEXTSET = "partexternal";
        }

        public class IDPrefix
        {
            public const String ITEM = "item";
            public const String TITLE = "title";
            public const String SEGMENT = "part";
        }

        public class SegmentGenre
        {
            public const String ARTICLE = "Article";
            public const String BOOK = "Book";
            public const String BOOKITEM = "BookItem";
            public const String CHAPTER = "Chapter";
            public const String JOURNAL = "Journal";
            public const String ISSUE = "Issue";
            public const String PROCEEDING = "Proceeding";
            public const String CONFERENCE = "Conference";
            public const String PREPRINT = "Preprint";
            public const String UNKNOWN = "Unknown";
            public const String TREATMENT = "Treatment";
        }

        /// <summary>
        /// Ensures id is a valid URI (doesn't contain any excluded characters).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if id is a valid URI, false otherwise.</returns>
        public static bool CheckId(string id)
        {
            try
            {
                Uri uri = new Uri(id);
                return true;
            }
            catch (UriFormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Parses an identifier into its scheme, namespaceIdentifier, and localIdentifier.
        /// Expected format for identifier => scheme ":" namespaceIdentifier ":" localIdentifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="scheme"></param>
        /// <param name="namespaceIdentifier"></param>
        /// <param name="localIdentifier"></param>
        /// <returns>true if parsing is successful, false otherwise.</returns>
        public static bool ParseOAIIdentifier(string identifier, out string scheme, out string namespaceIdentifier,
            out string localIdentifier)
        {
            char[] separator = { ':' };
            string[] parts = identifier.Split(separator);

            if (parts.Length != 3)
            {
                scheme = "";
                namespaceIdentifier = "";
                localIdentifier = "";
                return false;
            }

            scheme = parts[0];
            namespaceIdentifier = parts[1];
            localIdentifier = parts[2];

            return true;
        }

        /// <summary>
        /// Determines if date is in YYYY-MM-DD (ISO8601) format
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True if date is in YYYY-MM-DD format, false otherwise.</returns>
        public static bool ValidDate(string date)
        {
            string pattern = @"^\d\d\d\d-\d\d-\d\d$";
            Regex rx = new Regex(pattern);
            Match m = rx.Match(date);
            return m.Success;
        }

        /// <summary>
        /// According to http://www.openarchives.org/OAI/2.0/guidelines-static-repository.2002-11-11.htm
        /// The "datestamp" of records must be of the ISO8601 form YYYY-MM-DD.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Date in YYY-MM-DD format.</returns>
        public static string GetDate(string date)
        {
            if (date != String.Empty)
            {
                System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
                DateTime d = DateTime.Parse(date);
                return d.ToString("s", myDTFI) + "Z";
            }
            else
            {
                return date;
            }
        }

        /// <summary>
        /// Build a resumptionToken string from the given parts (with the unique ID).
        /// The resumptionToken syntax is:
        /// 	until|from|set|metadataPrefix|startingID|dateTimeStamp
        /// NOTE: cursor has to be a non-negative integer
        /// </summary>
        /// <param name="untilDate"></param>
        /// <param name="fromDate"></param>
        /// <param name="setSpec"></param>
        /// <param name="metadataPrefix"></param>
        /// <param name="startAtID"></param>
        /// <param name="timeStamp"></param>
        /// <param name="expDate"></param>
        /// <param name="size"></param>
        /// <param name="cursor"></param>
        /// <returns>A resumptionToken (including the tags and attributes)</returns>
        public static string BuildResumptionTokenWithOaiId(string untilDate, string fromDate, string setSpec,
            string metadataPrefix, string startAtID, string startAtSet, string timeStamp, string expDate, 
            string size, string cursor)
        {
            string token = "\t<resumptionToken";

            if (expDate.Length > 0) token += " expirationDate='" + expDate + "' ";
            if (size.Length > 0) token += " completeListSize='" + size + "' ";
            if (cursor.Length > 0) token += " cursor='" + cursor + "' ";

            // If there is no "startAtID", then an empty resumptionToken needed    
            if (startAtID.Length == 0)
                token += "/>\n";
            else
                token += ">" + untilDate + "|" + fromDate + "|" + setSpec + "|" + metadataPrefix + "|" +
                    startAtSet + ":" + startAtID + "|" + timeStamp + "</resumptionToken>\n";

            return token;
        }

        /// <summary>
        /// Parses a resumption token into its various parts:
        /// until|from|set|metadataPrefix|startingPosition|dateTimeStamp
        /// </summary>
        /// <param name="expTimeStamp"></param>
        /// <param name="resumptionToken"></param>
        /// <param name="untilDate"></param>
        /// <param name="fromDate"></param>
        /// <param name="setSpec"></param>
        /// <param name="metadataPrefix"></param>
        /// <param name="startAtID"></param>
        /// <returns>If the resumptionToken cannot be parsed or has expired, an error string is returned,
        /// otherwise an empty string is returned.</returns>
        public static string ParseResumptionTokenWithOaiId(string expTimeStamp, string resumptionToken, 
            out string untilDate, out string fromDate, out string setSpec, out string metadataPrefix, 
            out string startAtID, out string startAtSet)
        {
            string token = "";
            string compoundID = "";

            untilDate = null;
            fromDate = null;
            setSpec = null;
            metadataPrefix = null;
            startAtID = null;
            startAtSet = null;

            // Parse Resumption Token (contains all parameters plus counter for where left off last transaction)
            if (resumptionToken.Length > 0)
            {
                char[] separator = { '|' };
                string[] parts = resumptionToken.Split(separator);
                if (parts.Length != 6)
                {
                    token = @"<error code=""badResumptionToken"">'" + resumptionToken + "' is not a valid resumptionToken.</error>";
                }
                else
                {
                    untilDate = parts[0];
                    fromDate = parts[1];
                    setSpec = parts[2];
                    metadataPrefix = parts[3];
                    compoundID = parts[4];

                    // Get rid of T and Z
                    string rTIssue = parts[5].Replace("T", " ");
                    rTIssue = rTIssue.Substring(0, rTIssue.Length - 1);

                    // Split the compound ID into its set and ID parts
                    string[] compoundParts = compoundID.Split(':');
                    if (compoundParts.Length != 2)
                    {
                        token = @"<error code=""badResumptionToken"">'" + resumptionToken + "' is not a valid resumptionToken.</error>";
                    }
                    else
                    {
                        startAtSet = compoundParts[0];
                        startAtID = compoundParts[1];

                        try
                        {
                            // See if token has expired
                            DateTime issue = DateTime.Parse(rTIssue);
                            DateTime exp = DateTime.Parse(expTimeStamp);

                            if (DateTime.Compare(issue, exp) > 0)
                            {
                                token = @"<error code=""badResumptionToken"">'" + resumptionToken + "' has expired.</error>";
                            }
                            else if ((untilDate.Length > 0 && !ValidDate(untilDate)) ||
                                (fromDate.Length > 0 && !ValidDate(fromDate)))
                            {
                                token = @"<error code=""badResumptionToken"">'" + resumptionToken + "' is not a valid resumptionToken.</error>";
                            }
                        }
                        catch (Exception)
                        {
                            token = @"<error code=""badResumptionToken"">'" + resumptionToken + "' is not a valid resumptionToken.</error>";
                        }
                    }
                }
            }

            return token;
        }

        /// <summary>
        /// Returns a valid OAI timestamp based from current Universal Time.  
        /// SortableDateTimePattern (based on ISO 8601) using local time: yyyy'-'MM'-'ddTHH':'mm':'ss'
        /// Example: 2004-12-04T19:55:06Z
        /// </summary>
        /// <returns></returns>
        public static string NowUTC()
        {
            System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
            return DateTime.UtcNow.ToString("s", myDTFI) + "Z";
        }

        /// <summary>
        /// Checks to see if given metadataPrefix is supported.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns>Returns true if metadataPrefix is supported, false otherwise.</returns>
        public static bool CheckMetadataPrefix(string prefix, List<OAIMetadataFormat> metadataFormats)
        {
            foreach (OAIMetadataFormat metadataFormat in metadataFormats)
            {
                if (metadataFormat.MetadataFormat == prefix) return true;
            }
            return false;
        }

        public static bool IncludeExtraDetail(string prefix, List<OAIMetadataFormat> metadataFormats)
        {
            foreach (OAIMetadataFormat metadataFormat in metadataFormats)
            {
                if (metadataFormat.MetadataFormat == prefix) return metadataFormat.IncludeExtraDetail;
            }
            return false;
        }

        public static int GetMaxListRecords(string prefix, List<OAIMetadataFormat> metadataFormats)
        {
            foreach (OAIMetadataFormat metadataFormat in metadataFormats)
            {
                if (metadataFormat.MetadataFormat == prefix) return metadataFormat.MaxListRecords;
            }
            return 100;
        }

        /// <summary>
        /// Make sure that the identifier is correctly formatted and points to a valid item in the database.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="lastModDate"></param>
        /// <returns></returns>
        public static String VerifyIdentifier(String identifier, String identifierNamespace, String set, out String lastModDate)
        {
            String errorMessage = String.Empty;
            String scheme, namespaceIdentifier, localIdentifier;
            int idInt = int.MinValue;
            lastModDate = String.Empty;

            // Make sure the identifier is in the correct format
            bool ret = ParseOAIIdentifier(identifier, out scheme, out namespaceIdentifier, out localIdentifier);

            // "localIdentifier" should be in the form prefix/id (i.e. item/1000)
            String[] idSplit = localIdentifier.Split('/');

            if (idSplit.Length != 2)
            {
                errorMessage = @"<error code=""idDoesNotExist"">identifier is not valid.</error>";
            }
            else if (idSplit[0].ToLower() != OAI2Util.IDPrefix.TITLE && 
                    idSplit[0].ToLower() != OAI2Util.IDPrefix.ITEM && 
                    idSplit[0].ToLower() != OAI2Util.IDPrefix.SEGMENT)
            {
                // Valid prefixes for the localIdentifier are "title", "item", and "articlepdf"
                errorMessage = @"<error code=""idDoesNotExist"">identifier is not valid.</error>";
            }
            else if ((set != String.Empty) && (idSplit[0] != set))
            {
                // If a set was specified, the local identifier prefix must match the set.
                // For example, for a set value of "title", the local ID must be something like "title/1000".
                errorMessage = @"<error code=""idDoesNotExist"">identifier is not valid for the specified set.</error>";
            }
            else if (!ret || (scheme != "oai") || (namespaceIdentifier != identifierNamespace) ||
                String.IsNullOrEmpty(idSplit[1]) || !Int32.TryParse(idSplit[1], out idInt))
            {
                errorMessage = @"<error code=""idDoesNotExist"">identifier is not valid.</error>";
            }

            if (errorMessage == String.Empty)
            {
                BHLProvider provider = new BHLProvider();

                if (idSplit[0] == OAI2Util.IDPrefix.ITEM)
                {
                    // Validate ITEM identifier
                    Item item = provider.ItemSelectAuto(idInt);
                    if (item == null)
                    {
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else if (item.ItemStatusID != 40)
                    {
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else 
                    {
                        // Found an item, so get the last modified date
                        if (item.LastModifiedDate != null)
                        {
                            DateTime lastModDateTime = (DateTime)item.LastModifiedDate;
                            lastModDate = lastModDateTime.ToString("u");
                        }
                    }
                }
                else if (idSplit[0] == OAI2Util.IDPrefix.TITLE)
                {
                    // Validate TITLE identifier
                    Title title = provider.TitleSelectAuto(idInt);
                    if (title == null)
                    {
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else if (!title.PublishReady)
                    {
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else
                    {
                        // Found a title, so get the last modified date
                        if (title.LastModifiedDate != null)
                        {
                            DateTime lastModDateTime = (DateTime)title.LastModifiedDate;
                            lastModDate = lastModDateTime.ToString("u");
                        }
                    }
                }
                else if (idSplit[0] == OAI2Util.IDPrefix.SEGMENT)
                {
                    // Validate segment identifier
                    Segment segment = provider.SegmentSelectForSegmentID(idInt);

                    if (segment == null)
                    {
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else if (segment.SegmentStatusID != (int)ItemStatus.ItemStatusValue.New && segment.SegmentStatusID != (int)ItemStatus.ItemStatusValue.Published)
                    {
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else if (segment.ItemID == null && string.IsNullOrEmpty(segment.Url))
                    {
                        // Segment has no content, so don't include it in OAI output
                        errorMessage = @"<error code=""idDoesNotExist"">identifier '" + HttpUtility.HtmlEncode(identifier) + "' not found.</error>";
                    }
                    else
                    {
                        // Found a segment, so get the last modified date
                        if (segment.LastModifiedDate != null)
                        {
                            DateTime lastModDateTime = (DateTime)segment.LastModifiedDate;
                            lastModDate = lastModDateTime.ToString("u");
                        }
                    }
                }
            }

            return errorMessage;
        }

        public static String VerifyListDataParameters(NameValueCollection args, 
            List<OAIMetadataFormat> metadataFormats, out String resumptionToken, out String fromDate, 
            out String untilDate, out String setSpec, out String metadataPrefix)
        {
            String errorMessage = String.Empty;

            // Verify all parameters
            foreach (string key in args.Keys)
            {
                switch (key)
                {
                    case "verb": break;
                    case "from": break;
                    case "until": break;
                    case "set": break;
                    case "metadataPrefix": break;
                    case "resumptionToken": break;
                    default:
                        errorMessage += @"<error code=""badArgument"">Illegal argument '" + key + "'.</error>";
                        break;
                }
            }

            // Check for resumptionToken
            resumptionToken = args["resumptionToken"];
            if (resumptionToken != null)
            {
                if (resumptionToken.Length == 0)
                {
                    errorMessage += @"<error code=""badArgument"">Content of argument 'resumptionToken' is missing.</error>";
                }
                else if (args.Count > 2)
                {
                    errorMessage += @"<error code=""badArgument"">'resumptionToken' must be the only parameter</error>";
                }
            }

            // Check for from, until, set
            fromDate = args["from"];
            if (fromDate != null && fromDate.Length == 0)
            {
                errorMessage += @"<error code=""badArgument"">Content of argument 'from' is missing.</error>";
            }
            else if (fromDate != null && !OAI2Util.ValidDate(fromDate))
            {
                errorMessage += @"<error code=""badArgument"">Date of 'from' must be in YYYY-MM-DD format.</error>";
            }


            untilDate = args["until"];
            if (untilDate != null && untilDate.Length == 0)
            {
                errorMessage += @"<error code=""badArgument"">Content of argument 'until' is missing.</error>";
            }
            else if (untilDate != null && !OAI2Util.ValidDate(untilDate))
            {
                errorMessage += @"<error code=""badArgument"">Date of 'until' must be in YYYY-MM-DD format.</error>";
            }

            setSpec = args["set"];
            if (setSpec != null)
            {
                if (setSpec.Length == 0)
                {
                    errorMessage += @"<error code=""badArgument"">Content of argument 'set' is missing.</error>";
                }
                if (setSpec != OAI2Util.SetSpecification.TITLESET && 
                    setSpec != OAI2Util.SetSpecification.ITEMSET &&
                    setSpec != OAI2Util.SetSpecification.ITEMEXTSET &&
                    setSpec != OAI2Util.SetSpecification.SEGMENTSET &&
                    setSpec != OAI2Util.SetSpecification.SEGMENTEXTSET)
                {
                    errorMessage += @"<error code=""badArgument"">Invalid value for argument 'set'.</error>";
                }
            }

            // Check for metadataPrefix
            metadataPrefix = args["metadataPrefix"];
            if (metadataPrefix == null && resumptionToken == null)
            {
                // Missing metadataPrefix
                errorMessage += @"<error code=""badArgument"">Required argument 'metadataPrefix' is missing.</error>";
            }
            else if (metadataPrefix != null && metadataPrefix.Length == 0)
            {
                errorMessage += @"<error code=""badArgument"">Content of argument 'metadataPrefix' is missing.</error>";
            }
            else if (metadataPrefix != null && !OAI2Util.CheckMetadataPrefix(metadataPrefix, metadataFormats))
            {
                errorMessage += @"<error code=""cannotDisseminateFormat"">'" + metadataPrefix + "' is not a valid metadataPrefix.</error>";
            }

            return errorMessage;
        }
    }
}
