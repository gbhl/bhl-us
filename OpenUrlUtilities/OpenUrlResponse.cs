using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace MOBOT.OpenUrl.Utilities
{
    [Serializable]
    public class OpenUrlResponse : IOpenUrlResponse
    {
        #region IOpenUrlResponse Methods

        public string ToXml()
        {
            System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(typeof(OpenUrlResponse));
            StringWriterUtf8 text = new StringWriterUtf8();
            xml.Serialize(text, this);
            return text.ToString();
        }

        public string ToJson()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(this);
        }

        // Subclass the StringWriter class and override the default encoding.  This
        // allows us to produce XML encoded as UTF-8.
        private class StringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }
        }

        #endregion

        #region IOpenUrlResponse Attributes

        private ResponseStatus _status = ResponseStatus.Undefined;
        public ResponseStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private String _errorMessage = String.Empty;
        public string Message
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        private List<OpenUrlResponseCitation> _citations = new List<OpenUrlResponseCitation>();

        public List<OpenUrlResponseCitation> citations
        {
            get { return _citations; }
            set { _citations = value; }
        }

        #endregion
    }
}
