using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MOBOT.IAAnalysis.DataObjects;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
    {
        /// <summary>
        /// Return the data at the specified Url in an XML document
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public XmlDocument GetIAXmlData(string url)
        {
            return this.GetIAXmlData(url, null, out _);
        }

        /// <summary>
        /// Return the data at the specified Url in an XML document if it has
        /// been modified after the specified date.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="modifiedSince"></param>
        /// <param name="lastModified"></param>
        /// <returns></returns>
        public XmlDocument GetIAXmlData(string url, DateTime? modifiedSince, out DateTime? lastModified)
        {
            TextReader reader = null;
            try
            {
                XmlDocument xml = null;
                HttpWebResponse resp = this.HttpGet(url, modifiedSince, out lastModified);

                if (resp != null)
                {
                    reader = new StreamReader((System.IO.Stream)resp.GetResponseStream());

                    xml = new XmlDocument();
                    xml.Load(reader);
                }
                return xml;
            }
            finally
            {
                reader?.Dispose();
            }
        }

        /// <summary>
        /// Return the raw data stream from the specified Url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public BinaryReader GetIARawData(string url)
        {
            return this.GetIARawData(url, null, out _);
        }

        /// <summary>
        /// Return the raw data stream from the specified Url if it has 
        /// been modified after the specified date.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="modifiedSince"></param>
        /// <param name="lastModified"></param>
        /// <returns></returns>
        public BinaryReader GetIARawData(string url, DateTime? modifiedSince, out DateTime? lastModified)
        {
            BinaryReader reader = null;
            try
            {
                HttpWebResponse resp = this.HttpGet(url, modifiedSince, out lastModified);

                if (resp != null)
                {
                    reader = new BinaryReader(resp.GetResponseStream());
                }
                return reader;
            }
            finally
            {
                //if (reader != null) reader.Dispose();
            }
        }

        private HttpWebResponse HttpGet(string url)
        {
            return this.HttpGet(url, null, out _);

            //StreamReader reader = new StreamReader((System.IO.Stream)resp.GetResponseStream());
            //StringBuilder sb = new StringBuilder(reader.ReadToEnd());
            //return sb.ToString();
        }

        private HttpWebResponse HttpGet(string url, DateTime? modifiedSince, out DateTime? lastModified)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.Timeout = 60000;    // 60 seconds
                if (modifiedSince != null) req.IfModifiedSince = (DateTime)modifiedSince;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                lastModified = resp.LastModified;
                return resp;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    HttpWebResponse errResp = wex.Response as HttpWebResponse;
                    if (errResp.StatusCode == HttpStatusCode.NotModified)
                    {
                        lastModified = modifiedSince;
                        return null;
                    }
                }
                throw;
            }
        }
    }
}
