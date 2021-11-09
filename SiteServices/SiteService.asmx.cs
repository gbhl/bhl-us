﻿using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using MOBOT.BHL.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using BHL.QueueUtility;
using Newtonsoft.Json.Linq;
using MOBOT.BHL.DataObjects.Enum;

namespace BHL.SiteServices
{
    /// <summary>
    /// Summary description for SiteService
    /// </summary>
    [WebService(Namespace = "https://biodiversitylibrary.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SiteService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetOcrText(int pageID)
        {
            try
            {
                return new BHLProvider().GetOcrText(pageID);
            }
            catch (Exception ex)
            {
                return new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).GetErrorInfo(this.Context.Request, ex);
            }
        }

        [WebMethod]
        public string GetItemText(int itemType, int itemID)
        {
            try
            {
                return new BHLProvider().GetItemText((ItemType)itemType, itemID);
            }
            catch (Exception ex)
            {
                return new DebugUtility(ConfigurationManager.AppSettings["DebugValue"]).GetErrorInfo(this.Context.Request, ex);
            }
        }

        #region MQ Methods

        [WebMethod]
        public uint GetQueueMessageCount(string queueName)
        {
            uint numMessages = 0;

            /*
             * THIS SHOULD WORK, BUT CAUSES 500 ERROR ON PROD/QA SERVER
            using (QueueInfo queueInfo = new QueueInfo(ConfigurationManager.AppSettings["MQHost"],
                Convert.ToInt32(ConfigurationManager.AppSettings["MQPort"]),
                ConfigurationManager.AppSettings["MQUsername"],
                ConfigurationManager.AppSettings["MQPassword"]))
            {
                numMessages = queueInfo.GetMessageCount(queueName);
            }
            */

            string apiResponse = InvokeMQAPI(string.Format("http://{0}:{1}/api/queues",
                ConfigurationManager.AppSettings["MQHost"],
                ConfigurationManager.AppSettings["MQAPIPort"]));

            JArray jsonResponse = JArray.Parse(apiResponse);
            foreach(var queue in jsonResponse)
            {
                if (string.Compare(queue["name"].ToString(), queueName, true) == 0)
                {
                    numMessages = Convert.ToUInt32(queue["messages"]);
                    break;
                }
            }

            return numMessages;
        }

        [WebMethod]
        public void QueueMessages(string queueName, List<string> messages)
        {
            /*
             * THIS SHOULD WORK, BUT CAUSES 500 ERROR ON PROD/QA SERVER
            string errorQueueName = string.Empty;
            string errorExchangeName = string.Empty;

            // Get the error queue and error exchange for the specified queue
            string[] messageQueues = ConfigurationManager.AppSettings["MQQueues"].Split('~');
            foreach(string messageQueue in messageQueues)
            {
                string[] queueDetails = messageQueue.Split('|');
                if (string.Compare(queueDetails[0], queueName, true) == 0) {
                    errorQueueName = queueDetails[1];
                    errorExchangeName = queueDetails[2];
                }
            }

            // Add each message to the queue
            using (QueueIO queueIO = new QueueIO(ConfigurationManager.AppSettings["MQHost"],
                Convert.ToInt32(ConfigurationManager.AppSettings["MQPort"]),
                ConfigurationManager.AppSettings["MQUsername"],
                ConfigurationManager.AppSettings["MQPassword"]))
            {
                foreach (string message in messages)
                {
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        queueIO.PutMessage(message,
                            queueName: queueName,
                            errorQueueName: errorQueueName,
                            errorExchangeName: errorExchangeName);
                    }
                }
            }
            */

            foreach (string message in messages)
            {
                string requestBody = string.Format(
                    "{{\"properties\":{{}},\"routing_key\":\"{0}\",\"payload\":\"{1}\",\"payload_encoding\":\"string\"}}",
                    queueName,
                    message
                    );

                /*
                 * Causing HTTP 405 (method not allowed) errors on Prod/QA server.
                 * https://stackoverflow.com/a/10890943
                 * https://stackoverflow.com/a/12170132
                 * https://stackoverflow.com/questions/4379674/httpwebrequest-url-escaping
                 * http://blogs.perpetuumsoft.com/dotnet/about-escaping-slashes-in-net/
                 */
                string apiResponse = InvokeMQAPI(
                    string.Format("http://{0}:{1}/api/exchanges/%2f//publish",
                        ConfigurationManager.AppSettings["MQHost"],
                        ConfigurationManager.AppSettings["MQAPIPort"]), 
                    "POST", 
                    requestBody);

                JObject jsonResponse = JObject.Parse(apiResponse);
                if (!Convert.ToBoolean(jsonResponse["routed"]))
                {
                    throw new Exception(string.Format("Error queuing message: {0}", message));
                }
            }
        }

        /// <summary>
        /// Invoke the Rabbit Management API.  Example usage:
        ///     InvokeMQAPI("http://localhost:15672/api/queues");
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private string InvokeMQAPI(string uri, string method = "GET", string body = null)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = method;
            req.Timeout = 60000;
            req.Headers.Add("Authorization", "Basic " + 
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}",
                            ConfigurationManager.AppSettings["MQUsername"],
                            ConfigurationManager.AppSettings["MQPassword"]))));

            if (!string.IsNullOrWhiteSpace(body) && method.ToUpper() == "POST")
            {
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(body);
                req.ContentType = "application/json";
                req.ContentLength = byteArray.Length;
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            string jsonResponse = string.Empty;
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader((System.IO.Stream)resp.GetResponseStream()))
                {
                    jsonResponse = reader.ReadToEnd();
                }
            }
            req = null;

            return jsonResponse;
        }

        class MessageQueue
        {
            public string Queue { get; set; }
            public string ErrorQueue { get; set; }
            public string ErrorExchange { get; set; }
        }

        #endregion MQ Methods

        #region DOI Methods

        [WebMethod]
        public string DOIGetFileContents(string batchId, string type)
        {
            return new BHLProvider().DOIGetFileContents(batchId, type);
        }

        #endregion DOI Methods

        #region OCR Job File Methods

        [WebMethod]
        public bool OcrJobExists(int itemID)
        {
            return new BHLProvider().OcrJobExists(itemID);
        }

        [WebMethod]
        public void OcrCreateJob(int itemID)
        {
            new BHLProvider().OcrCreateJob(itemID);
        }

        #endregion OCR Job File Methods

        #region MARC File Methods

        [WebMethod]
        public bool MARCFileExists(int id, string type)
        {
            string filePath = new BHLProvider().MarcFileExists(id, type);
            return !string.IsNullOrWhiteSpace(filePath);
        }

        [WebMethod]
        public string MARCGetFileContents(int id, string type)
        {
            return new BHLProvider().MarcGetFileContents(id, type);
        }

        [WebMethod]
        public void MarcCreateFile(string marcBibID, string content)
        {
            new BHLProvider().MarcCreateFile(marcBibID, content);
        }

        #endregion MARC File Methods

        /// <summary>
        /// Add a height and width to each element in the specified list of pages
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        [WebMethod]
        public List<BHLProvider.ViewerPage> PageGetImageDimensions(List<BHLProvider.ViewerPage> pages, int itemType, int itemID)
        {
            return new BHLProvider().PageGetImageDimensions(pages, (ItemType)itemType, itemID);
        }

        #region Email Methods

        [WebMethod]
        public bool SendEmail(String from, String[] to, String[] cc, String[] bcc, String subject,
            String body)
        {
            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(from);
            System.Net.Mail.MailAddress[] toAddresses = new System.Net.Mail.MailAddress[to.Length];
            int x = 0;
            foreach (String toAddress in to)
            {
                toAddresses[x] = new System.Net.Mail.MailAddress(toAddress);
                x++;
            }
            System.Net.Mail.MailAddress[] ccAddresses = null;
            if (cc != null)
            {
                ccAddresses = new System.Net.Mail.MailAddress[cc.Length];
                x = 0;
                foreach (string ccAddress in cc)
                {
                    ccAddresses[x] = new System.Net.Mail.MailAddress(ccAddress);
                    x++;
                }
            }
            System.Net.Mail.MailAddress[] bccAddresses = null;
            if (bcc != null)
            {
                bccAddresses = new System.Net.Mail.MailAddress[bcc.Length];
                x = 0;
                foreach (string bccAddress in bcc)
                {
                    bccAddresses[x] = new System.Net.Mail.MailAddress(bccAddress);
                    x++;
                }
            }

            EmailSupport emailSupport = new EmailSupport(
                ConfigurationManager.AppSettings["SMTPHost"]);
            return emailSupport.Send(toAddresses, fromAddress, subject, body, false, null,
                bccAddresses, ccAddresses, System.Net.Mail.MailPriority.Normal, null);
        }

        #endregion Email Methods

        [WebMethod]
        public byte[] GetItemPdf(int itemType, int itemID)
        {
            return new BHLProvider().GetItemPdf((ItemType)itemType, itemID);
        }
    }
}
