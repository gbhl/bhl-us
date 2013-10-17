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

            // TODO: Parse the supplied Dublin Core and store the values in _oaiRecord

            XDocument xml = XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(dcRecord)));
            XElement root = xml.Root;
            XNamespace ns = root.Name.Namespace;




            throw new NotImplementedException();
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
            if (!String.IsNullOrEmpty(_oaiRecord.Contributor)) sb.Append("<dc:contributor>" + HttpUtility.HtmlEncode(_oaiRecord.Contributor) + "</dc:contributor>\n");

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
            if (!String.IsNullOrEmpty(_oaiRecord.Url)) sb.Append("<dc:identifier>" + HttpUtility.HtmlEncode(_oaiRecord.Url) + "</dc:identifier>\n");

            // Source
            // No mapping for this DataSet

            // Language
            foreach (String language in _oaiRecord.Languages)
            {
                sb.Append("<dc:language>" + HttpUtility.HtmlEncode(language) + "</dc:language>\n");
            }

            // Relation
            if (!String.IsNullOrEmpty(_oaiRecord.Title) && !String.IsNullOrEmpty(_oaiRecord.JournalTitle))
            {
                string relation = _oaiRecord.JournalTitle;
                if (!string.IsNullOrEmpty(_oaiRecord.JournalVolume)) relation += ", " + _oaiRecord.JournalVolume;
                sb.Append("<dc:relation type='IsChildOf'>" + HttpUtility.HtmlEncode(relation) + "</dc:relation>\n");
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
