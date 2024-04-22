using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
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
                (HttpResponseMessage resp, DateTime? lastMod) = Task.Run(() => HttpGetAsync(url, modifiedSince)).Result;

                if (resp != null)
                {
                    reader = new StreamReader(resp.Content.ReadAsStreamAsync().Result);

                    xml = new XmlDocument();
                    xml.Load(reader);
                }
                lastModified = lastMod;
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
                (HttpResponseMessage resp, DateTime ? lastMod) = Task.Run(() => HttpGetAsync(url, modifiedSince)).Result;

                if (resp != null)
                {
                    reader = new BinaryReader(resp.Content.ReadAsStreamAsync().Result);
                }
                lastModified = lastMod;
                return reader;
            }
            finally
            {
                //if (reader != null) reader.Dispose();
            }
        }

        static async Task<(HttpResponseMessage, DateTime?)> HttpGetAsync(string url, DateTime? modifiedSince)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(30);
                if (modifiedSince != null)
                    client.DefaultRequestHeaders.IfModifiedSince = (DateTimeOffset)modifiedSince;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    DateTime? lastModified = (DateTime?)null;
                    if (response.Content.Headers.LastModified != null) lastModified = response.Content.Headers.LastModified.Value.DateTime;
                    return (response, lastModified);
                }
                else if (response.StatusCode == HttpStatusCode.NotModified)
                {
                    return (null, modifiedSince);
                }
                else
                {
                    throw new HttpRequestException($"Request '{url}' failed with status code {response.StatusCode}");
                }
            }
        }
    }
}
