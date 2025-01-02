using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace BHL.SiteServicesREST.v1.Services
{
    public class QueueService : IQueueService
    {
        public async Task AddQueueMessages(string queueName, List<string> messages)
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
                string apiResponse = await InvokeMQAPI(
                    string.Format("http://{0}:{1}/api/exchanges/%2f//publish",
                        System.Configuration.ConfigurationManager.AppSettings["MQHost"],
                        System.Configuration.ConfigurationManager.AppSettings["MQAPIPort"]),
                    "POST",
                    requestBody);

                JObject jsonResponse = JObject.Parse(apiResponse);
                if (!Convert.ToBoolean(jsonResponse["routed"]))
                {
                    throw new Exception(string.Format("Error queuing message: {0}", message));
                }
            }
        }

        public async Task<uint> GetQueueMessageCount(string queueName)
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

            string apiResponse = await InvokeMQAPI(string.Format("http://{0}:{1}/api/queues",
                System.Configuration.ConfigurationManager.AppSettings["MQHost"],
                System.Configuration.ConfigurationManager.AppSettings["MQAPIPort"]));

            JArray jsonResponse = JArray.Parse(apiResponse);
            foreach (var queue in jsonResponse)
            {
                if (string.Compare(queue["name"].ToString(), queueName, true) == 0)
                {
                    numMessages = Convert.ToUInt32(queue["messages"]);
                    break;
                }
            }

            return numMessages;
        }

        /// <summary>
        /// Invoke the Rabbit Management API.  Example usage:
        ///     InvokeMQAPI("http://localhost:15672/api/queues");
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private async Task<string> InvokeMQAPI(string uri, string method = "GET", string body = "")
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = method;
            req.Timeout = 60000;
            req.Headers.Add("Authorization", "Basic " +
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}",
                            System.Configuration.ConfigurationManager.AppSettings["MQUsername"],
                            System.Configuration.ConfigurationManager.AppSettings["MQPassword"]))));

            if (!string.IsNullOrWhiteSpace(body) && method.ToUpper() == "POST")
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(body);
                req.ContentType = "application/json";
                req.ContentLength = byteArray.Length;
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            string jsonResponse = string.Empty;
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader((Stream)resp.GetResponseStream()))
                {
                    jsonResponse = reader.ReadToEnd();
                }
            }

            return jsonResponse;
        }
    }
}
