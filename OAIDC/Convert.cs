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
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MOBOT.BHL.OAI2;

namespace MOBOT.BHL.OAIDC
{
    public class Convert
    {
        OAIRecord _oaiRecord;

        public Convert(OAIRecord oaiRecord)
        {
            _oaiRecord = oaiRecord;
        }

        public Convert(string dcRecord)
        {
            _oaiRecord = new OAIRecord();

            // Parse the supplied Dublin Core and store the values in _oaiRecord
            XDocument xml = XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(dcRecord)));
            XElement root = xml.Root;
            XNamespace ns = root.GetNamespaceOfPrefix(root.Name.LocalName);
            if (ns == null) ns = root.Name.Namespace;

            XElement title = root.Element(ns + "title");
            if (title != null) _oaiRecord.Title = title.Value;

            var creators = from c in root.Elements(ns + "creator") select c;
            foreach (XElement c in creators)
            {
                OAIRecord.Creator creator = new OAIRecord.Creator();
                creator.FullName = c.Value;
                _oaiRecord.Creators.Add(new KeyValuePair<string, OAIRecord.Creator>(string.Empty, creator));
            }

            var keywords = from k in root.Elements(ns + "subject") select k;
            List<string> subjectStrings = new List<string>();
            foreach (XElement k in keywords)
            {
                // If this is a subject string of the form "subject1 -- subject2 -- subject 3",
                // then split this into separate subjects (subject1, subject2, and subject3).
                // Only keep unique subject terms.
                if (k.Value.IndexOf("--") >= 0)
                {
                    string[] subjectList = k.Value.Replace("--", "~").Split('~');
                    foreach (string subject in subjectList)
                    {
                        if (!subjectStrings.Contains(subject.Trim())) subjectStrings.Add(subject.Trim());
                    }
                }
                else
                {
                    if (!subjectStrings.Contains(k.Value)) subjectStrings.Add(k.Value);
                }
            }
            // Save all of the accumulated subjects to the OAIRecord
            foreach (string subjectString in subjectStrings.Distinct().ToArray())
            {
                // Strip off trailing periods
                string cleanSubject = subjectString;
                if (cleanSubject.Substring(cleanSubject.Length - 1) == ".") cleanSubject = cleanSubject.Substring(0, cleanSubject.Length - 1);
                _oaiRecord.Subjects.Add(new KeyValuePair<string, string>(cleanSubject, cleanSubject));
            }

            XElement publisher = root.Element(ns + "publisher");
            if (publisher != null) _oaiRecord.PublicationDetails = publisher.Value;

            XElement contributor = root.Element(ns + "contributor");
            if (contributor != null) _oaiRecord.Contributors.Add(contributor.Value);

            var languages = from l in root.Elements(ns + "language") select l;
            foreach (XElement l in languages)
            {
                _oaiRecord.Languages.Add((Code: "", Name: l.Value));
            }

            XElement date = root.Element(ns + "date");
            if (date != null) _oaiRecord.Date = date.Value;

            var descriptions = from d in root.Elements(ns + "description") select d;
            foreach (XElement d in descriptions)
            {
                _oaiRecord.Descriptions.Add(d.Value);
            }

            var types = from t in root.Elements(ns + "type") select t;
            foreach (XElement t in types)
            {
                _oaiRecord.Types.Add(t.Value);
            }

            // Set the type for the record.
            _oaiRecord.Type = OAIRecord.RecordType.Unknown;
            if ((from t in _oaiRecord.Types where t.ToLower() != "text" select t).Count() > 0)
            {
                // We have something other than the basic value of "text".  Assume it indicates "segment"
                // unless we find a known "book/journal" dublin core type value.
                _oaiRecord.Type = OAIRecord.RecordType.Segment;
                if ((from t in _oaiRecord.Types
                     where t == "book" || t == "journal" || t == "monograph" || t == "series"
                     select t).Count() > 0)
                    _oaiRecord.Type = OAIRecord.RecordType.BookJournal;
            }

            var identifiers = from i in root.Elements(ns + "identifier") select i;
            foreach (XElement i in identifiers)
            {
                string identifier = i.Value;
                if (identifier.StartsWith("info:doi/"))
                    _oaiRecord.Doi = identifier.Replace("info:doi/", "");
                else if (identifier.StartsWith("urn:ISSN:"))
                    _oaiRecord.Issn = identifier.Replace("urn:ISSN:", "");
                else if (identifier.StartsWith("urn:ISBN:"))
                    _oaiRecord.Isbn = identifier.Replace("urn:ISBN:", "");
                else 
                    _oaiRecord.Url = identifier;
            }

            var formats = from f in root.Elements(ns + "format") select f;
            foreach (XElement f in formats)
            {
                _oaiRecord.Formats.Add(f.Value);
            }

            var rights = from r in root.Elements(ns + "rights") select r;
            foreach (XElement r in rights)
            {
                _oaiRecord.Rights.Add(r.Value);
            }
        }

        #region ToOAIRecord

        public OAIRecord ToOAIRecord()
        {
            return _oaiRecord;
        }

        #endregion ToOAIRecord

        #region ToString

        public new String ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<oai_dc:dc xsi:schemaLocation=\"http://www.openarchives.org/OAI/2.0/oai_dc/ &#13;&#10;&#9;&#9;&#9;&#9;http://www.openarchives.org/OAI/2.0/oai_dc.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:oai_dc=\"http://www.openarchives.org/OAI/2.0/oai_dc/\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\">\n");

            // Title
            if (!String.IsNullOrEmpty(_oaiRecord.Title)) sb.Append("<dc:title>" + HttpUtility.HtmlEncode(_oaiRecord.Title) + "</dc:title>\n");

            // Creator
            foreach (KeyValuePair<string, OAIRecord.Creator> creatorData in _oaiRecord.Creators)
            {
                sb.Append("<dc:creator>" + HttpUtility.HtmlEncode(creatorData.Value.FullName) + "</dc:creator>\n");
            }

            // Subject
            foreach (KeyValuePair<string, string> subject in _oaiRecord.Subjects)
            {
                sb.Append("<dc:subject>" + HttpUtility.HtmlEncode(subject.Value) + "</dc:subject>\n");
            }

            // Description
            foreach (String description in _oaiRecord.Descriptions)
            {
                sb.Append("<dc:description>" + HttpUtility.HtmlEncode(description) + "</dc:description>\n");
            }

            // Publisher
            if (!String.IsNullOrEmpty(_oaiRecord.PublicationDetails)) sb.Append("<dc:publisher>" + HttpUtility.HtmlEncode(_oaiRecord.PublicationDetails) + "</dc:publisher>\n");

            // Contributor 
            foreach (string contributor in _oaiRecord.Contributors)
            {
                sb.Append("<dc:contributor>" + HttpUtility.HtmlEncode(contributor) + "</dc:contributor>\n");
            }

            // Date
            if (!String.IsNullOrEmpty(_oaiRecord.Date)) sb.Append("<dc:date>" + HttpUtility.HtmlEncode(_oaiRecord.Date) + "</dc:date>\n");

            // Type
            foreach (String type in _oaiRecord.Types)
            {
                sb.Append("<dc:type>" + HttpUtility.HtmlEncode(type) + "</dc:type>\n");
            }

            // Format
            foreach (String format in _oaiRecord.Formats)
            {
                sb.Append("<dc:format>" + HttpUtility.HtmlEncode(format) + "</dc:format>\n");
            }

            // Identifier
            if (!string.IsNullOrWhiteSpace(_oaiRecord.Url)) sb.Append("<dc:identifier>" + HttpUtility.HtmlEncode(_oaiRecord.Url) + "</dc:identifier>\n");
            if (!string.IsNullOrWhiteSpace(_oaiRecord.Doi)) sb.Append("<dc:identifier>info:doi/" + HttpUtility.HtmlEncode(_oaiRecord.Doi) + "</dc:identifier>\n");
            if (!string.IsNullOrWhiteSpace(_oaiRecord.Issn)) sb.Append("<dc:identifier>urn:ISSN:" + HttpUtility.HtmlEncode(_oaiRecord.Issn) + "</dc:identifier>\n");
            if (!string.IsNullOrWhiteSpace(_oaiRecord.EIssn)) sb.Append("<dc:identifier>urn:ISSN:" + HttpUtility.HtmlEncode(_oaiRecord.EIssn) + "</dc:identifier>\n");
            if (!string.IsNullOrWhiteSpace(_oaiRecord.Isbn)) sb.Append("<dc:identifier>urn:ISBN:" + HttpUtility.HtmlEncode(_oaiRecord.Isbn) + "</dc:identifier>\n");

            // Source
            // No mapping for this DataSet

            // Language
            foreach ((string Code, string Name) language in _oaiRecord.Languages)
            {
                sb.Append("<dc:language>" + HttpUtility.HtmlEncode(language.Name) + "</dc:language>\n");
            }

            // Relation
            if (!string.IsNullOrWhiteSpace(_oaiRecord.ParentUrl))
            {
                sb.Append("<dc:relation>" + HttpUtility.HtmlEncode(_oaiRecord.ParentUrl) + "</dc:relation>\n");
            }
            foreach(KeyValuePair<string, OAIRecord> relatedTitle in _oaiRecord.RelatedTitles)
            {
                OAIRecord relation = relatedTitle.Value;
                if ((relation.MarcTag == "490" || relation.MarcTag == "773" || relation.MarcTag == "830") &&
                    !string.IsNullOrWhiteSpace(relation.Url))
                {
                    sb.Append("<dc:relation>" + HttpUtility.HtmlEncode(relation.Url) + "</dc:relation>\n");
                }
            }

            // Coverage
            // No mapping for this DataSet

            // Rights
            foreach (String right in _oaiRecord.Rights)
            {
                sb.Append("<dc:rights>" + HttpUtility.HtmlEncode(right) + "</dc:rights>\n");
            }

            sb.Append("</oai_dc:dc>\n");

            return sb.ToString();
        }

        #endregion ToString
    }
}
